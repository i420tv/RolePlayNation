using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class FastFood : Script
    {
        private static Dictionary<int, Timer> fastFoodTimerList;

        public FastFood()
        {
            // Initialize the class data
            fastFoodTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            // Check if the player is fastfood deliverer
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_FASTFOOD) return;

            if (fastFoodTimerList.TryGetValue(player.Value, out Timer fastFoodTimer) == true)
            {
                // Destroy the timer
                fastFoodTimer.Dispose();
                fastFoodTimerList.Remove(player.Value);

                // Check if the player had a vehicle
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                if(vehicle != null && vehicle.Exists)
                {
                    // Respawn the job vehicle
                    RespawnFastfoodVehicle(vehicle);
                }
            }
        }

        public static void CheckFastfoodOrders(Client player)
        {
            // Get the deliverable orders
            List<FastfoodOrderModel> fastFoodOrders = Globals.fastFoodOrderList.Where(o => !o.taken).ToList();

            if (fastFoodOrders.Count == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_none);
                return;
            }

            List<float> distancesList = new List<float>();

            foreach (FastfoodOrderModel order in fastFoodOrders)
            {
                float distance = player.Position.DistanceTo(order.position);
                distancesList.Add(distance);
            }

            player.TriggerEvent("showFastfoodOrders", NAPI.Util.ToJson(fastFoodOrders), NAPI.Util.ToJson(distancesList));
        }

        private int GetFastFoodOrderAmount(Client player)
        {
            int amount = 0;
            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
            foreach (FastfoodOrderModel order in Globals.fastFoodOrderList)
            {
                if (order.id == orderId)
                {
                    amount += order.pizzas * Constants.PRICE_PIZZA;
                    amount += order.hamburgers * Constants.PRICE_HAMBURGER;
                    amount += order.sandwitches * Constants.PRICE_SANDWICH;
                    break;
                }
            }
            return amount;
        }

        private FastfoodOrderModel GetFastfoodOrderFromId(int orderId)
        {
            // Get the fastfood order from the specified identifier
            return Globals.fastFoodOrderList.Where(orderModel => orderModel.id == orderId).FirstOrDefault();
        }

        private static void RespawnFastfoodVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnFastFoodTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                // Vehicle respawn
                RespawnFastfoodVehicle(vehicle);

                // Cancel the order
                player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                player.ResetData(EntityData.PLAYER_JOB_WON);

                // Delete map blip
                player.TriggerEvent("fastFoodDeliverFinished");

                // Remove timer from the list
                Timer fastFoodTimer = fastFoodTimerList[player.Value];
                if (fastFoodTimer != null)
                {
                    fastFoodTimer.Dispose();
                    fastFoodTimerList.Remove(player.Value);
                }

                // Send the message to the player
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
            });
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_FASTFOOD + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.GetData(EntityData.PLAYER_DELIVER_ORDER) == null && player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_delivering_order);
                }
                else if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) != null && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != vehicle)
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                }
                else
                {
                    if (fastFoodTimerList.TryGetValue(player.Value, out Timer fastFoodTimer) == true)
                    {
                        fastFoodTimer.Dispose();
                        fastFoodTimerList.Remove(player.Value);
                    }

                    if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                    {
                        int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                        FastfoodOrderModel order = GetFastfoodOrderFromId(orderId);

                        player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);

                        player.TriggerEvent("fastFoodDestinationCheckPoint", order.position);
                    }
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_FASTFOOD + Constants.MAX_FACTION_VEHICLES && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != null)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle)
                {
                    string warn = string.Format(InfoRes.job_vehicle_left, 60);
                    player.SendChatMessage(Constants.COLOR_INFO + warn);

                    // Timer with the time left to get into the vehicle
                    Timer fastFoodTimer = new Timer(OnFastFoodTimer, player, 60000, Timeout.Infinite);
                    fastFoodTimerList.Add(player.Value, fastFoodTimer);
                }
            }
        }

        [RemoteEvent("takeFastFoodOrder")]
        public void TakeFastFoodOrderEvent(Client player, int orderId)
        {
            foreach (FastfoodOrderModel order in Globals.fastFoodOrderList)
            {
                if (order.id == orderId)
                {
                    if (order.taken)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_taken);
                    }
                    else
                    {
                        // Get the time to reach the destination
                        int start = Globals.GetTotalSeconds();
                        int time = (int)Math.Round(player.Position.DistanceTo(order.position) / 9.5f);

                        // We take the order
                        order.taken = true;

                        player.SetData(EntityData.PLAYER_DELIVER_ORDER, orderId);
                        player.SetData(EntityData.PLAYER_DELIVER_START, start);
                        player.SetData(EntityData.PLAYER_DELIVER_TIME, time);

                        // Information message sent to the player
                        string orderMessage = string.Format(InfoRes.deliver_order, time);
                        player.SendChatMessage(Constants.COLOR_INFO + orderMessage);
                    }
                    return;
                }
            }

            // Order has been deleted
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_timeout);
        }

        [RemoteEvent("fastfoodCheckpointReached")]
        public void FastfoodCheckpointReachedEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_DELIVER_START) != null)
            {
                if (!player.IsInVehicle)
                {
                    Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                    Vector3 vehiclePosition = vehicle.GetData(EntityData.VEHICLE_POSITION);

                    int elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_DELIVER_START);
                    int extra = (int)Math.Round((player.GetData(EntityData.PLAYER_DELIVER_TIME) - elapsed) / 2.0f);
                    int amount = GetFastFoodOrderAmount(player) + extra;

                    player.ResetData(EntityData.PLAYER_DELIVER_START);
                    player.SetData(EntityData.PLAYER_JOB_WON, amount > 0 ? amount : 25);

                    player.TriggerEvent("fastFoodDeliverBack", vehiclePosition);

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.deliver_completed);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.deliver_in_vehicle);
                }
            }
            else
            {
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                if (player.Vehicle == vehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    int won = player.GetData(EntityData.PLAYER_JOB_WON);
                    int money = player.GetSharedData(EntityData.PLAYER_MONEY);
                    int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                    string message = string.Format(InfoRes.job_won, won);
                    Globals.fastFoodOrderList.RemoveAll(order => order.id == orderId);

                    player.WarpOutOfVehicle();

                    player.SetSharedData(EntityData.PLAYER_MONEY, money + won);
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                    player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                    player.ResetData(EntityData.PLAYER_JOB_WON);

                    player.TriggerEvent("fastFoodDeliverFinished");

                    // We get the motorcycle to its spawn point
                    RespawnFastfoodVehicle(vehicle);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                }
            }
        }
    }
}
