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
{/*
    public class BurgerFlopper : Script
    {
        private static Dictionary<int, Timer> burgerFlopperTimerList;

        public BurgerFlopper()
        {
            // Initialize the class data
            burgerFlopperTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            // Check if the player is fastfood deliverer
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_BURGERFLOPPER) return;

            if (burgerFlopperTimerList.TryGetValue(player.Value, out Timer burgerFlopperTimer) == true)
            {
                // Destroy the timer
                burgerFlopperTimer.Dispose();
                burgerFlopperTimerList.Remove(player.Value);

                // Check if the player had a vehicle
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                if(vehicle != null && vehicle.Exists)
                {
                    // Respawn the job vehicle
                    RespawnBurgerFlopperVehicle(vehicle);
                }
            }
        }

        public static void CheckBurgerFlopperOrders(Client player)
        {
            // Get the deliverable orders
            List<BurgerFlopperOrderModel> burgerFlopperOrders = Globals.burgerFlopperOrderList.Where(o => !o.taken).ToList();

            if (burgerFlopperOrders.Count == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_none);
                return;
            }

            List<float> distancesList = new List<float>();

            foreach (BurgerFlopperOrderModel order in burgerFlopperOrders)
            {
                float distance = player.Position.DistanceTo(order.position);
                distancesList.Add(distance);
            }

            player.TriggerEvent("showBurgerFlopperOrders", NAPI.Util.ToJson(burgerFlopperOrders), NAPI.Util.ToJson(distancesList));
        }

        private int GetBurgerFLopperOrderAmount(Client player)
        {
            int amount = 0;
            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
            foreach (BurgerFlopperOrderModel order in Globals.burgerFlopperOrderList)
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

        private BurgerFlopperOrderModel GetBurgerFlopperOrderFromId(int orderId)
        {
            // Get the fastfood order from the specified identifier
            return Globals.burgerFlopperOrderList.Where(orderModel => orderModel.id == orderId).FirstOrDefault();
        }

        private static void RespawnBurgerFlopperVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnBurgerFlopperTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                // Vehicle respawn
                RespawnBurgerFlopperVehicle(vehicle);

                // Cancel the order
                player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                player.ResetData(EntityData.PLAYER_JOB_WON);

                // Delete map blip
                player.TriggerEvent("fastBurgerFlopperFinished");

                // Remove timer from the list
                Timer burgerFlopperTimer = burgerFlopperTimerList[player.Value];
                if (burgerFlopperTimer != null)
                {
                    burgerFlopperTimer.Dispose();
                    burgerFlopperTimerList.Remove(player.Value);
                }

                // Send the message to the player
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
            });
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_BURGERFLOPPER + Constants.MAX_FACTION_VEHICLES)
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
                    if (burgerFlopperTimerList.TryGetValue(player.Value, out Timer burgerFlopperTimer) == true)
                    {
                        burgerFlopperTimer.Dispose();
                        burgerFlopperTimerList.Remove(player.Value);
                    }

                    if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                    {
                        int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                        BurgerFlopperOrderModel order = GetBurgerFlopperOrderFromId(orderId);

                        player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);

                        player.TriggerEvent("BurgerFlopperDestinationCheckPoint", order.position);
                    }
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_BURGERFLOPPER + Constants.MAX_FACTION_VEHICLES && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != null)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle)
                {
                    string warn = string.Format(InfoRes.job_vehicle_left, 60);
                    player.SendChatMessage(Constants.COLOR_INFO + warn);

                    // Timer with the time left to get into the vehicle
                    Timer burgerFlopperTimer = new Timer(OnBurgerFlopperTimer, player, 60000, Timeout.Infinite);
                    burgerFlopperTimerList.Add(player.Value, burgerFlopperTimer);
                }
            }
        }

        [RemoteEvent("takeBurgerFlopperOrder")]
        public void TakeBurgerFlopperOrderEvent(Client player, int orderId)
        {
            foreach (BurgerFlopperOrderModel order in Globals.burgerFlopperOrderList)
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

        [RemoteEvent("BurgerFlopperCheckpointReached")]
        public void BurgerFlopperCheckpointReachedEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_DELIVER_START) != null)
            {
                if (!player.IsInVehicle)
                {
                    Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                    Vector3 vehiclePosition = vehicle.GetData(EntityData.VEHICLE_POSITION);

                    int elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_DELIVER_START);
                    int extra = (int)Math.Round((player.GetData(EntityData.PLAYER_DELIVER_TIME) - elapsed) / 2.0f);
                    int amount = GetBurgerFLopperOrderAmount(player) + extra;

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
                    Globals.burgerFlopperOrderList.RemoveAll(order => order.id == orderId);

                    player.WarpOutOfVehicle();

                    player.SetSharedData(EntityData.PLAYER_MONEY, money + won);
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                    player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                    player.ResetData(EntityData.PLAYER_JOB_WON);

                    player.TriggerEvent("BurgerFlopperDeliverFinished");

                    // We get the motorcycle to its spawn point
                    RespawnBurgerFlopperVehicle(vehicle);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                }
            }
        }
    }*/
}
