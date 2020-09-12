using GTANetworkAPI;
using WiredPlayers.business;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.house;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.factions;
using WiredPlayers.messages.success;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using WiredPlayers.messages.error;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;

/*namespace WiredPlayers.jobs
{
    public class Thief : Script
    {
        private static Dictionary<int, Timer> robberyTimerList;

        public Thief()
        {
            foreach (Vector3 pawnShop in Constants.PAWN_SHOP)
            {
                // Create pawn shops
                NAPI.TextLabel.CreateTextLabel(GenRes.pawn_shop, pawnShop, 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
            }

            // Initialize the variables
            robberyTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player)
        {
            if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
            {
                robberyTimer.Dispose();
                robberyTimerList.Remove(player.Value);
            }
        }

        private void OnLockpickTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;

                Vehicle vehicle = player.GetData(EntityData.PLAYER_LOCKPICKING);
                vehicle.Locked = false;

                player.StopAnimation();
                player.ResetData(EntityData.PLAYER_LOCKPICKING);
                player.ResetData(EntityData.PLAYER_ANIMATION);

                if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
                {
                    robberyTimer.Dispose();
                    robberyTimerList.Remove(player.Value);
                }

                player.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.lockpicked);
            });
        }

        private void OnHotwireTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;

                Vehicle vehicle = player.GetData(EntityData.PLAYER_HOTWIRING);
                vehicle.EngineStatus = true;

                player.StopAnimation();
                player.ResetData(EntityData.PLAYER_HOTWIRING);
                player.ResetData(EntityData.PLAYER_ANIMATION);

                if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
                {
                    robberyTimer.Dispose();
                    robberyTimerList.Remove(player.Value);
                }

                // Get all the members from any police faction
                List<Client> members = NAPI.Pools.GetAllPlayers().Where(m => Faction.IsPoliceMember(m) && m.GetData(EntityData.PLAYER_ON_DUTY) == 1).ToList();

                foreach (Client target in members)
                {
                    target.SendChatMessage(Constants.COLOR_INFO + InfoRes.police_warning);
                    target.SetData(EntityData.PLAYER_EMERGENCY_WITH_WARN, player.Position);
                }

                player.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.veh_hotwireed);
            });
        }

        private void OnPlayerRob(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                int playerSqlId = player.GetData(EntityData.PLAYER_SQL_ID);
                int timeElapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_ROBBERY_START);
                decimal stolenItemsDecimal = timeElapsed / Constants.ITEMS_ROBBED_PER_TIME;
                int totalStolenItems = (int)Math.Round(stolenItemsDecimal);

                // Check if the player has stolen items
                ItemModel stolenItemModel = Globals.GetPlayerItemModelFromHash(playerSqlId, Constants.ITEM_HASH_STOLEN_OBJECTS);

                if (stolenItemModel == null)
                {
                    stolenItemModel = new ItemModel();
                    {
                        stolenItemModel.amount = totalStolenItems;
                        stolenItemModel.hash = Constants.ITEM_HASH_STOLEN_OBJECTS;
                        stolenItemModel.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                        stolenItemModel.ownerIdentifier = playerSqlId;
                        stolenItemModel.dimension = 0;
                        stolenItemModel.position = new Vector3(0.0f, 0.0f, 0.0f);
                    }

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            stolenItemModel.id = Database.AddNewItem(stolenItemModel);
                            Globals.itemList.Add(stolenItemModel);
                        });
                    });
                }
                else
                {
                    stolenItemModel.amount += totalStolenItems;

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the amount into the database
                            Database.UpdateItem(stolenItemModel);
                        });
                    });
                }

                // Allow player movement
                player.Freeze(false);
                player.StopAnimation();
                player.ResetData(EntityData.PLAYER_ANIMATION);
                player.ResetData(EntityData.PLAYER_ROBBERY_START);

                if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
                {
                    robberyTimer.Dispose();
                    robberyTimerList.Remove(player.Value);
                }

                // Avisamos de los objetos robados
                string message = string.Format(InfoRes.player_robbed, totalStolenItems);
                player.SendChatMessage(Constants.COLOR_INFO + message);

                // Check if the player commited the maximum thefts allowed
                int totalThefts = player.GetData(EntityData.PLAYER_JOB_DELIVER);
                if (Constants.MAX_THEFTS_IN_ROW == totalThefts)
                {
                    // Apply a cooldown to the player
                    player.SetData(EntityData.PLAYER_JOB_DELIVER, 0);
                    player.SetData(EntityData.PLAYER_JOB_COOLDOWN, 60);
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_rob_pressure);
                }
                else
                {
                    player.SetData(EntityData.PLAYER_JOB_DELIVER, totalThefts + 1);
                }
            });
        }

        private void GeneratePoliceRobberyWarning(Client player)
        {
            Vector3 robberyPosition;
            string robberyPlace = string.Empty;
            string robberyHour = DateTime.Now.ToString("h:mm:ss tt");

            // Check if he robbed into a house or business
            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = House.GetHouseById(houseId);
                robberyPosition = house.position;
                robberyPlace = house.name;
            }
            else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
            {
                int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                BusinessModel business = Business.GetBusinessById(businessId);
                robberyPosition = business.position;
                robberyPlace = business.name;
            }
            else
            {
                robberyPosition = player.Position;
            }

            // Create the police report
            FactionWarningModel policeWarning = new FactionWarningModel(Constants.FACTION_POLICE, player.Value, robberyPlace, robberyPosition, -1, robberyHour);
            FactionWarningModel sheriffWarning = new FactionWarningModel(Constants.FACTION_SHERIFF, player.Value, robberyPlace, robberyPosition, -1, robberyHour);
            Faction.factionWarningList.Add(policeWarning);
            Faction.factionWarningList.Add(sheriffWarning); 

            string warnMessage = string.Format(InfoRes.emergency_warning, Faction.factionWarningList.Count - 1);

            // Get all the members from any police faction
            List<Client> members = NAPI.Pools.GetAllPlayers().Where(m => Faction.IsPoliceMember(m) && m.GetData(EntityData.PLAYER_ON_DUTY) == 1).ToList();

            foreach (Client target in members)
            {
                // Send the warning
                target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (player.GetData(EntityData.PLAYER_JOB) == Constants.JOB_THIEF)
            {
                if (player.GetData(EntityData.PLAYER_HOTWIRING) != null)
                {
                    // Remove player's hotwire
                    player.ResetData(EntityData.PLAYER_HOTWIRING);
                    player.StopAnimation();
                    
                    if (robberyTimerList.TryGetValue(player.Value, out Timer robberyTimer) == true)
                    {
                        robberyTimer.Dispose();
                        robberyTimerList.Remove(player.Value);
                    }
                    
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_stopped_hotwire);
                }
                else if (player.GetData(EntityData.PLAYER_ROBBERY_START) != null)
                {
                    OnPlayerRob(player);
                }
            }
        }

        [Command(Commands.COM_FORCE)]
        public void ForceCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_thief);
                }
                else if (player.GetData(EntityData.PLAYER_LOCKPICKING) != null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_lockpicking);
                }
                else
                {
                    Vehicle vehicle = Vehicles.GetClosestVehicle(player);
                    if (vehicle == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                    }
                    else if (Vehicles.HasPlayerVehicleKeys(player, vehicle, false))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_lockpick_own_vehicle);
                    }
                    else if (!vehicle.Locked)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.veh_already_unlocked);
                    }
                    else
                    {
                        // Generate police report
                        GeneratePoliceRobberyWarning(player);

                        player.SetData(EntityData.PLAYER_LOCKPICKING, vehicle);
                        player.PlayAnimation("missheistfbisetup1", "hassle_intro_loop_f", (int)Constants.AnimationFlags.Loop);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);

                        // Timer to finish forcing the door
                        Timer robberyTimer = new Timer(OnLockpickTimer, player, 10000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);

                    }
                }
            }
        }

        [Command(Commands.COM_STEAL)]
        public void StealCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_thief);
            }
            else if (player.GetData(EntityData.PLAYER_ROBBERY_START) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_stealing);
            }
            else if (player.GetData(EntityData.PLAYER_JOB_COOLDOWN) > 0)
            {
                int timeLeft = player.GetData(EntityData.PLAYER_JOB_COOLDOWN) - Globals.GetTotalSeconds();
                string message = string.Format(ErrRes.player_cooldown_thief, timeLeft);
                player.SendChatMessage(Constants.COLOR_ERROR + message);
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0 || player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                {
                    int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    HouseModel house = House.GetHouseById(houseId);
                    if (house != null && House.HasPlayerHouseKeys(player, house) == true)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_rob_own_house);
                    }
                    else
                    {
                        // Generate the police report
                        GeneratePoliceRobberyWarning(player);

                        // Start stealing items
                        player.PlayAnimation("misscarstealfinalecar_5_ig_3", "crouchloop", (int)Constants.AnimationFlags.Loop);
                        player.SetData(EntityData.PLAYER_ROBBERY_START, Globals.GetTotalSeconds());
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.searching_value_items);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);
                        player.Freeze(true);

                        // Timer to finish the robbery
                        Timer robberyTimer = new Timer(OnPlayerRob, player, 20000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);
                    }
                }
                else if (player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    if (Vehicles.HasPlayerVehicleKeys(player, player.Vehicle, false))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_rob_own_vehicle);
                    }
                    else if (player.Vehicle.EngineStatus)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.engine_on);
                    }
                    else
                    {
                        // Generate the police report
                        GeneratePoliceRobberyWarning(player);

                        // Start stealing items
                        player.PlayAnimation("veh@plane@cuban@front@ds@base", "hotwire", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));
                        player.SetData(EntityData.PLAYER_ROBBERY_START, Globals.GetTotalSeconds());
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.searching_value_items);
                        player.SetData(EntityData.PLAYER_ANIMATION, true);

                        // Timer to finish the robbery
                        Timer robberyTimer = new Timer(OnPlayerRob, player, 35000, Timeout.Infinite);
                        robberyTimerList.Add(player.Value, robberyTimer);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_rob);
                }
            }
        }

        [Command(Commands.COM_HOTWIRE)]
        public void HotwireCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_thief);
            }
            else if (player.GetData(EntityData.PLAYER_HOTWIRING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_hotwiring);
            }
            else if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
            }
            else
            {
                if (player.VehicleSeat != (int)VehicleSeat.Driver)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_driving);
                }
                else if (Vehicles.HasPlayerVehicleKeys(player, player.Vehicle, false))
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_hotwire_own_vehicle);
                }
                else if (player.Vehicle.EngineStatus)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.engine_already_started);
                }
                else
                {
                    int vehicleId = player.Vehicle.GetData(EntityData.VEHICLE_ID);
                    Vector3 position = player.Vehicle.Position;

                    player.SetData(EntityData.PLAYER_ANIMATION, true);
                    player.SetData(EntityData.PLAYER_HOTWIRING, player.Vehicle);
                    player.PlayAnimation("veh@plane@cuban@front@ds@base", "hotwire", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.AllowPlayerControl));

                    // Create timer to finish the hotwire
                    Timer robberyTimer = new Timer(OnHotwireTimer, player, 15000, Timeout.Infinite);
                    robberyTimerList.Add(player.Value, robberyTimer);
                    
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.hotwire_started);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Add hotwire log to the database
                            Database.LogHotwire(player.Name, vehicleId, position);
                        });
                    });
                }
            }
        }

        [Command(Commands.COM_PAWN)]
        public void PawnCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_THIEF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_thief);
            }
            else
            {
                foreach (Vector3 pawnShop in Constants.PAWN_SHOP)
                {
                    if (player.Position.DistanceTo(pawnShop) < 1.5f)
                    {
                        int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel stolenItems = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_STOLEN_OBJECTS);
                        if (stolenItems != null)
                        {
                            // Calculate the earnings
                            int wonAmount = stolenItems.amount * Constants.PRICE_STOLEN;
                            string message = string.Format(InfoRes.player_pawned_items, wonAmount);
                            int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;

                            Task.Factory.StartNew(() =>
                            {
                                NAPI.Task.Run(() =>
                                {
                                    // Delete stolen items
                                    Database.RemoveItem(stolenItems.id);
                                    Globals.itemList.Remove(stolenItems);
                                });
                            });

                            player.SetSharedData(EntityData.PLAYER_MONEY, money);
                            player.SendChatMessage(Constants.COLOR_INFO + message);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_stolen_items);
                        }
                        return;
                    }
                }
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_pawn_show);
            }
        }
    }
}
*/