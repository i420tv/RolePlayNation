using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class Garbage : Script
    {
        private static Dictionary<int, Timer> garbageTimerList;

        public Garbage()
        {
            // Initialize the variables
            garbageTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
            {
                garbageTimer.Dispose();
                garbageTimerList.Remove(player.Value);
            }
        }

        private void RespawnGarbageVehicle(Vehicle vehicle)
        {
            vehicle.Repair();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnGarbageTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                RespawnGarbageVehicle(vehicle);

                // Cancel the garbage route
                player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
                target.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);

                if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
                {
                    // Remove the timer
                    garbageTimer.Dispose();
                    garbageTimerList.Remove(player.Value);
                }

                // Send the message to both players
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
                target.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
            });
        }

        private void OnGarbageCollectedTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Client driver = player.GetData(EntityData.PLAYER_JOB_PARTNER);

                if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
                {
                    garbageTimer.Dispose();
                    garbageTimerList.Remove(player.Value);
                }

                // Get garbage bag
                GTANetworkAPI.Object garbageBag = player.GetData(EntityData.PLAYER_GARBAGE_BAG);
                player.StopAnimation();
                garbageBag.Delete();

                // Get the remaining checkpoints
                int route = driver.GetData(EntityData.PLAYER_JOB_ROUTE);
                int checkPoint = driver.GetData(EntityData.PLAYER_JOB_CHECKPOINT) + 1;
                int totalCheckPoints = Constants.GARBAGE_LIST.Where(x => x.route == route).Count();

                if (checkPoint < totalCheckPoints)
                {
                    Vector3 currentGarbagePosition = GetGarbageCheckPointPosition(route, checkPoint);
                    Vector3 nextGarbagePosition = GetGarbageCheckPointPosition(route, checkPoint + 1);

                    driver.SetData(EntityData.PLAYER_JOB_CHECKPOINT, checkPoint);
                    player.SetData(EntityData.PLAYER_JOB_CHECKPOINT, checkPoint);

                    // Add the garbage bag
                    garbageBag = NAPI.Object.CreateObject(628215202, currentGarbagePosition, new Vector3(0.0f, 0.0f, 0.0f));
                    player.SetData(EntityData.PLAYER_GARBAGE_BAG, garbageBag);

                    // Create the checkpoints
                    driver.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition, nextGarbagePosition, CheckpointType.CylinderSingleArrow);
                    player.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition, nextGarbagePosition, CheckpointType.CylinderSingleArrow);
                }
                else
                {
                    driver.SendChatMessage(Constants.COLOR_INFO + InfoRes.route_finished);

                    driver.TriggerEvent("showGarbageCheckPoint", new Vector3(49.44239f, 6558.004f, 32.18963f), new Vector3(), CheckpointType.CylinderCheckerboard);
                    player.TriggerEvent("deleteGarbageCheckPoint");
                }

                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.garbage_collected);
            });
        }

        private Vector3 GetGarbageCheckPointPosition(int route, int checkPoint)
        {
            // Get the garbage position
            return Constants.GARBAGE_LIST.Where(garbageModel => garbageModel.route == route && garbageModel.checkPoint == checkPoint).FirstOrDefault()?.position;
        }

        private void FinishGarbageRoute(Client driver, bool canceled = false)
        {
            Client partner = driver.GetData(EntityData.PLAYER_JOB_PARTNER);
            
            RespawnGarbageVehicle(driver.Vehicle);

            // Destroy the previous checkpoint
            driver.TriggerEvent("deleteGarbageCheckPoint");

            // Entity data reset
            driver.ResetData(EntityData.PLAYER_JOB_PARTNER);
            driver.ResetData(EntityData.PLAYER_JOB_ROUTE);
            driver.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            driver.ResetData(EntityData.PLAYER_JOB_VEHICLE);

            partner.ResetData(EntityData.PLAYER_JOB_PARTNER);
            partner.ResetData(EntityData.PLAYER_GARBAGE_BAG);
            partner.ResetData(EntityData.PLAYER_JOB_ROUTE);
            partner.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
            partner.ResetData(EntityData.PLAYER_JOB_VEHICLE);
            partner.ResetData(EntityData.PLAYER_ANIMATION);

            if (!canceled)
            {
                // Pay the earnings to both players
                int driverMoney = driver.GetSharedData(EntityData.PLAYER_MONEY);
                int partnerMoney = partner.GetSharedData(EntityData.PLAYER_MONEY);
                driver.SetSharedData(EntityData.PLAYER_MONEY, driverMoney + Constants.MONEY_GARBAGE_ROUTE);
                partner.SetSharedData(EntityData.PLAYER_MONEY, partnerMoney + Constants.MONEY_GARBAGE_ROUTE);

                // Send the message with the earnings
                string message = string.Format(InfoRes.garbage_earnings, Constants.MONEY_GARBAGE_ROUTE);
                driver.SendChatMessage(Constants.COLOR_INFO + message);
                partner.SendChatMessage(Constants.COLOR_INFO + message);
            }

            // Remove players from the vehicle
            driver.WarpOutOfVehicle();
            partner.WarpOutOfVehicle();
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_GARBAGE + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    if (player.GetData(EntityData.PLAYER_JOB_ROUTE) == null && player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_route);
                    }
                    else if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) != null && player.GetData(EntityData.PLAYER_JOB_VEHICLE) != vehicle)
                    {
                        player.WarpOutOfVehicle();
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                    }
                    else
                    {
                        if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == true)
                        {
                            garbageTimer.Dispose();
                            garbageTimerList.Remove(player.Value);
                        }

                        // Check whether route starts or he's returning to the truck
                        if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == null)
                        {
                            player.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_waiting_partner);
                        }
                        else
                        {
                            // We continue with the previous route
                            Client partner = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int garbageRoute = player.GetData(EntityData.PLAYER_JOB_ROUTE);
                            int checkPoint = player.GetData(EntityData.PLAYER_JOB_CHECKPOINT);
                            int totalCheckPoints = Constants.GARBAGE_LIST.Where(x => x.route == garbageRoute).Count();
                            Vector3 garbagePosition = GetGarbageCheckPointPosition(garbageRoute, checkPoint);

                            if(checkPoint < totalCheckPoints)
                            {
                                Vector3 nextPosition = GetGarbageCheckPointPosition(garbageRoute, checkPoint + 1);
                                player.TriggerEvent("showGarbageCheckPoint", garbagePosition, nextPosition, CheckpointType.CylinderSingleArrow);
                                partner.TriggerEvent("showGarbageCheckPoint", garbagePosition, nextPosition, CheckpointType.CylinderSingleArrow);
                            }
                            else
                            {
                                // Show the last checkpoint
                                player.TriggerEvent("showGarbageCheckPoint", garbagePosition, new Vector3(), CheckpointType.CylinderCheckerboard);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Client driver in vehicle.Occupants)
                    {
                        if (driver.GetData(EntityData.PLAYER_JOB_PARTNER) != null && driver.VehicleSeat == (int)VehicleSeat.Driver)
                        {
                            Client partner = driver.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (partner == driver)
                            {
                                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                                {
                                    // Link both players as partners
                                    player.SetData(EntityData.PLAYER_JOB_PARTNER, driver);
                                    driver.SetData(EntityData.PLAYER_JOB_PARTNER, player);

                                    // Set the route to the passenger
                                    int garbageRoute = driver.GetData(EntityData.PLAYER_JOB_ROUTE);
                                    player.SetData(EntityData.PLAYER_JOB_ROUTE, garbageRoute);
                                    driver.SetData(EntityData.PLAYER_JOB_CHECKPOINT, 0);
                                    player.SetData(EntityData.PLAYER_JOB_CHECKPOINT, 0);

                                    // Create the first checkpoint
                                    Vector3 currentGarbagePosition = GetGarbageCheckPointPosition(garbageRoute, 0);
                                    Vector3 nextGarbagePosition = GetGarbageCheckPointPosition(garbageRoute, 1);

                                    // Add garbage bag
                                    GTANetworkAPI.Object trashBag = NAPI.Object.CreateObject(628215202, currentGarbagePosition, new Vector3(0.0f, 0.0f, 0.0f));
                                    player.SetData(EntityData.PLAYER_GARBAGE_BAG, trashBag);

                                    driver.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition, nextGarbagePosition, CheckpointType.CylinderSingleArrow);
                                    player.TriggerEvent("showGarbageCheckPoint", currentGarbagePosition, nextGarbagePosition, CheckpointType.CylinderSingleArrow);
                                }
                                else
                                {
                                    player.WarpOutOfVehicle();
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                                }
                            }
                            return;
                        }
                    }

                    // There's no player driving, kick the passenger
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.wait_garbage_driver);
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) != null && vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_GARBAGE + Constants.MAX_FACTION_VEHICLES)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == vehicle && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                    string warn = string.Format(InfoRes.job_vehicle_left, 45);
                    player.SendChatMessage(Constants.COLOR_INFO + warn);
                    player.TriggerEvent("deleteGarbageCheckPoint");
                    target.TriggerEvent("deleteGarbageCheckPoint");

                    // Create the timer for driver to get into the vehicle
                    Timer garbageTimer = new Timer(OnGarbageTimer, player, 45000, Timeout.Infinite);
                    garbageTimerList.Add(player.Value, garbageTimer);
                }
            }
        }

        [RemoteEvent("garbageCheckpointEntered")]
        public void OnPlayerEnterCheckpoint(Client player)
        {
            int route = player.GetData(EntityData.PLAYER_JOB_ROUTE);
            int checkPoint = player.GetData(EntityData.PLAYER_JOB_CHECKPOINT);
            int totalCheckPoints = Constants.GARBAGE_LIST.Where(x => x.route == route).Count();

            if (player.VehicleSeat == (int)VehicleSeat.Driver && checkPoint == totalCheckPoints - 1)
            {
                if (player.GetData(EntityData.PLAYER_JOB_VEHICLE) == player.Vehicle)
                {
                    // Finish the route
                    FinishGarbageRoute(player);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle_job);
                }
            }
        }

        [Command(Commands.COM_GARBAGE, Commands.HLP_GARBAGE_JOB_COMMAND)]
        public void GarbageCommand(Client player, string action)
        {
            player.SendChatMessage(Constants.COLOR_ERROR + "Trabajo deshabilitado temporalmente.");
            return;

            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_GARBAGE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_garbage);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else
            {
                switch (action.ToLower())
                {
                    case Commands.ARG_ROUTE:
                        if (player.GetData(EntityData.PLAYER_JOB_ROUTE) != null)
                        {
                            player.SendChatMessage(ErrRes.already_in_route);
                        }
                        else
                        {
                            Random random = new Random();
                            int garbageRoute = random.Next(Constants.MAX_GARBAGE_ROUTES);
                            player.SetData(EntityData.PLAYER_JOB_ROUTE, garbageRoute);
                            switch (garbageRoute)
                            {
                                case 0:
                                    player.SendChatMessage(Constants.COLOR_INFO + GenRes.route_north);
                                    break;
                                case 1:
                                    player.SendChatMessage(Constants.COLOR_INFO + GenRes.route_south);
                                    break;
                                case 2:
                                    player.SendChatMessage(Constants.COLOR_INFO + GenRes.route_east);
                                    break;
                                case 3:
                                    player.SendChatMessage(Constants.COLOR_INFO + GenRes.route_west);
                                    break;
                            }
                        }
                        break;
                    case Commands.ARG_PICKUP:
                        if (player.IsInVehicle)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.garbage_in_vehicle);
                        }
                        else if (player.GetData(EntityData.PLAYER_GARBAGE_BAG) == null)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_garbage_near);
                        }
                        else
                        {
                            // Get the closest garbage bag
                            GTANetworkAPI.Object trashBag = player.GetData(EntityData.PLAYER_GARBAGE_BAG);

                            if (player.Position.DistanceTo(trashBag.Position) < 3.5f)
                            {
                                if (garbageTimerList.TryGetValue(player.Value, out Timer garbageTimer) == false)
                                {
                                    player.PlayAnimation("anim@move_m@trash", "pickup", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));
                                    player.SetData(EntityData.PLAYER_ANIMATION, true);

                                    // Make the timer for garbage collection
                                    garbageTimer = new Timer(OnGarbageCollectedTimer, player, 15000, Timeout.Infinite);
                                    garbageTimerList.Add(player.Value, garbageTimer);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_garbage);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_garbage_near);
                            }
                        }
                        break;
                    case Commands.ARG_CANCEL:
                        if (player.GetData(EntityData.PLAYER_JOB_PARTNER) != null)
                        {
                            Client partner = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            if (partner != player)
                            {
                                GTANetworkAPI.Object trashBag = null;

                                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                                {
                                    // Driver canceled
                                    trashBag = player.GetData(EntityData.PLAYER_GARBAGE_BAG);
                                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.route_finished);
                                    partner.TriggerEvent("deleteGarbageCheckPoint");

                                    // Create finish checkpoint
                                    player.TriggerEvent("showGarbageCheckPoint", new Vector3(49.44239f, 6558.004f, 32.18963f), new Vector3(), CheckpointType.CylinderCheckerboard);
                                }
                                else
                                {
                                    // Passenger canceled
                                    trashBag = partner.GetData(EntityData.PLAYER_GARBAGE_BAG);
                                    player.TriggerEvent("deleteGarbageCheckPoint");

                                    // Create finish checkpoint
                                    partner.TriggerEvent("showGarbageCheckPoint", new Vector3(49.44239f, 6558.004f, 32.18963f), new Vector3(), CheckpointType.CylinderCheckerboard);
                                }

                                // Delete the garbage bag
                                trashBag.Delete();
                            }
                            else
                            {
                                // Player doesn't have any partner
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.route_canceled);
                            }

                            // Remove player from partner search
                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        }
                        else if (player.GetData(EntityData.PLAYER_JOB_ROUTE) != null)
                        {
                            // Cancel the route
                            player.ResetData(EntityData.PLAYER_JOB_ROUTE);
                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.garbage_route_canceled);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_route);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_GARBAGE_JOB_COMMAND);
                        break;
                }
            }
        }
    }
}