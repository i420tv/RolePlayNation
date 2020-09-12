using GTANetworkAPI;
using System.Linq;
using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.database;
using WiredPlayers.business;
using WiredPlayers.parking;
using WiredPlayers.house;
using WiredPlayers.weapons;
using WiredPlayers.factions;
using WiredPlayers.character;
using WiredPlayers.messages.information;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.administration;
using WiredPlayers.messages.success;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.jobs
{
    public class Missions : Script
    {
        private string NPCRollan = Constants.COLOR_YELLOW + "Rollan: ";
        public Missions()
        {
            CreateJobLocation();
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(-44.04705f, -2519.901f, 7.394626f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Press ~y~E~w~to talk ", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Guy at train station
        }

        public void OpenUI(Client player)
        {
            player.TriggerEvent("Mission1");
        }
        

  
        

        private int GetOrderAmount(Client player)
        {
            int amount = 0;
            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
            foreach (TruckingOrderModel order in Globals.truckingOrderList)
            {
                if (order.id == orderId)
                {
                    amount += order.pizzas * Constants.PRICE_PACKAGES;
                    amount += order.hamburgers * Constants.PRICE_PACKAGES;
                    amount += order.sandwitches * Constants.PRICE_PACKAGES;
                    break;
                }
            }
            return amount;
        }

        private TruckingOrderModel GetTruckingOrderFromId(int orderId)
        {
            // Get the fastfood order from the specified identifier
            return Globals.truckingOrderList.Where(orderModel => orderModel.id == orderId).FirstOrDefault();
        }

        private static void RespawnTruckingVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            // vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            // vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnFastFoodTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                // Vehicle respawn
              //  RespawnFastfoodVehicle(vehicle);

                // Cancel the order
                player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                player.ResetData(EntityData.PLAYER_JOB_WON);

                // Delete map blip
                player.TriggerEvent("truckingDeliverFinished");

                // Send the message to the player
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
            });
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_TRUCKER + Constants.MAX_FACTION_VEHICLES)
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

                    if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                    {
                        int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
                        TruckingOrderModel order = GetTruckingOrderFromId(orderId);

                        player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);

                        player.TriggerEvent("truckingDestinationCheckPoint", order.position);
                    }
                }
            }
        }

        [RemoteEvent("takeTruckingOrder")]
        public void TakeTruckingOrderEvent(Client player, int orderId)
        {
            foreach (TruckingOrderModel order in Globals.truckingOrderList)
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

        [RemoteEvent("truckingCheckpointReached")]
        public void TruckingCheckpointReachedEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_DELIVER_START) != null)
            {
                if (!player.IsInVehicle)
                {
                    Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                    Vector3 vehiclePosition = vehicle.GetData(EntityData.VEHICLE_POSITION);

                    int elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_DELIVER_START);
                    int extra = (int)Math.Round((player.GetData(EntityData.PLAYER_DELIVER_TIME) - elapsed) / 2.0f);
                    int amount = GetOrderAmount(player) + extra;

                    player.ResetData(EntityData.PLAYER_DELIVER_START);
                    player.SetData(EntityData.PLAYER_JOB_WON, amount > 0 ? amount : 25);

                    player.TriggerEvent("truckingDeliverBack", vehiclePosition);

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.deliver_completed);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.deliver_in_postal);
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
                    Globals.truckerOrderList.RemoveAll(order => order.id == orderId);

                    //   player.WarpOutOfVehicle();

                    player.SetSharedData(EntityData.PLAYER_MONEY, money + won);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                    player.SendNotification("You gained 5 xp in Trucking");
                    player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                    //    player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                    player.ResetData(EntityData.PLAYER_JOB_WON);

                    player.TriggerEvent("postalDeliverFinished");

                    // We get the motorcycle to its spawn point
                    // RespawnFastfoodVehicle(vehicle);

                    List<SkillsModel> PlayerSkillList = Database.LoadSkills();

                    foreach (SkillsModel skills in PlayerSkillList)
                    {
                        if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                        {
                            skills.postalexp = skills.truckingexp + 5;

                            Database.SaveSkills(PlayerSkillList);
                        }
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                }
            }
        }
    }
}
