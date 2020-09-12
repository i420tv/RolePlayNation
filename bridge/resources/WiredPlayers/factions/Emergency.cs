using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.house;
using WiredPlayers.vehicles;
using WiredPlayers.business;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.messages.success;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace WiredPlayers.factions
{

    public class Emergency : Script
    {
        public static List<PoliceControlModel> policeControlList;
        public static List<BloodModel> bloodList;
        public Emergency()
        {
            policeControlList = new List<PoliceControlModel>();
            // Initialize reinforces updater
            //  reinforcesTimer = new Timer(UpdateReinforcesRequests, null, 250, 250);

            // Create all the equipment places
            /*foreach (Vector3 pos in Constants.EQUIPMENT_MEDIC_POSITIONS)
             {
                 NAPI.TextLabel.CreateTextLabel("/" + Commands.COM_EQUIP, pos, 10.0f, 0.5f, 4, new Color(190, 235, 100), false, 0);
                 NAPI.TextLabel.CreateTextLabel(GenRes.equipment_help, new Vector3(pos.X, pos.Y, pos.Z - 0.1f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);

                 // Create blips
                 Blip policeBlip = NAPI.Blip.CreateBlip(pos);
                 policeBlip.Name = GenRes.police_station;
                 policeBlip.ShortRange = true;
                 policeBlip.Sprite = 60;

             }*/
            CreateJobLocation();
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(328.666f, -559.6287f, 28.74379f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/mdveh~w~ to get your vehicle and ~y~/mdreturn~w~ to return your vehicle.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
                                                                                                                                                                                               //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }
        private void CreateEmergencyReport(DeathModel death)
        {

            if (death.killer.Value == Constants.UNDEFINED_VALUE)
            {
                // Check if the player was dead
                int databaseKiller = death.player.GetSharedData(EntityData.PLAYER_KILLED);

                if (databaseKiller == 0)
                {
                    // There's no killer, we set the environment as killer
                    death.player.SetSharedData(EntityData.PLAYER_KILLED, Constants.UNDEFINED_VALUE);
                }
            }
            else
            {
                int killerId = death.killer.GetData(EntityData.PLAYER_SQL_ID);
                death.player.SetSharedData(EntityData.PLAYER_KILLED, killerId);
            }

            // Warn the player
            death.player.SendChatMessage(Constants.COLOR_INFO + InfoRes.emergency_warn);


        }

        private int GetRemainingBlood()
        {
            int remaining = 0;
            foreach (BloodModel blood in bloodList)
            {
                if (blood.used)
                {
                    remaining--;
                }
                else
                {
                    remaining++;
                }
            }
            return remaining;
        }

        public static void CancelPlayerDeath(Client player)
        {
            NAPI.Player.SpawnPlayer(player, player.Position);
            player.SetSharedData(EntityData.PLAYER_KILLED, 0);
            player.ResetData(EntityData.TIME_HOSPITAL_RESPAWN);

            // Get the death warning
            FactionWarningModel factionWarn = Faction.GetFactionWarnByTarget(player.Value, Constants.FACTION_EMERGENCY);

            if (factionWarn != null)
            {
                if (factionWarn.takenBy >= 0)
                {
                    // Tell the player who attended the report it's been canceled
                    Client doctor = Globals.GetPlayerById(factionWarn.takenBy);
                    doctor.SendChatMessage(Constants.COLOR_INFO + InfoRes.faction_warn_canceled);
                }

                // Remove the report from the list
                Faction.factionWarningList.Remove(factionWarn);
            }

            // Change the death state
            player.TriggerEvent("togglePlayerDead", false);
        }

        private void TeleportPlayerToHospital(Client player)
        {
            NAPI.Player.SpawnPlayer(player, new Vector3(298.4254f, -584.3168f, 43.26083f));
            player.Dimension = 0;

            player.ResetData(EntityData.TIME_HOSPITAL_RESPAWN);
            player.SetSharedData(EntityData.PLAYER_KILLED, 0);
            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);

            // Change the death state
            player.TriggerEvent("togglePlayerDead", false);
        }

        [ServerEvent(Event.PlayerDeath)]
        public void OnPlayerDeath(Client player, Client killer, uint weapon)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) == 0)
            {
                DeathModel death = new DeathModel(player, killer, weapon);

                Vector3 deathPosition = null;
                string deathPlace = string.Empty;
                string deathHour = DateTime.Now.ToString("h:mm:ss tt");

                // Checking if player died into a house or business
                if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
                {
                    int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    HouseModel house = House.GetHouseById(houseId);
                    deathPosition = house.position;
                    deathPlace = house.name;
                }
                else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                {
                    int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                    BusinessModel business = Business.GetBusinessById(businessId);
                    deathPosition = business.position;
                    deathPlace = business.name;
                }
                else
                {
                    deathPosition = player.Position;
                }

                if (killer.Value == Constants.UNDEFINED_VALUE || killer == player)
                {
                    // We add the report to the list
                    FactionWarningModel factionWarning = new FactionWarningModel(Constants.FACTION_EMERGENCY, player.Value, deathPlace, deathPosition, -1, deathHour);
                    Faction.factionWarningList.Add(factionWarning);

                    // Report message
                    string warnMessage = string.Format(InfoRes.emergency_warning, Faction.factionWarningList.Count - 1);

                    // Sending the report to all the emergency department's members
                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                    {
                        if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY && target.GetData(EntityData.PLAYER_ON_DUTY) > 0)
                        {
                            target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
                        }
                    }

                    // Create the emergency report
                    CreateEmergencyReport(death);
                }
                else
                {
                    int killerId = killer.GetData(EntityData.PLAYER_SQL_ID);
                    player.SetSharedData(EntityData.PLAYER_KILLED, killerId);
                }

                // Time to let player accept his dead
                player.SetData(EntityData.TIME_HOSPITAL_RESPAWN, Globals.GetTotalSeconds() + 240); //////// PUT BACK TO 240 WHEN ON MAIN SERVER

                // Set the player into dead state
                player.TriggerEvent("togglePlayerDead", true);
            }
        }

        [Command(Commands.COM_HEAL, Commands.HLP_HEAL_COMMAND)]
        public void HealCommand(Client player, string targetString)
        {
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target == null)
            {
                // The player is not connected
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                return;
            }

            if (player.Position.DistanceTo(target.Position) > 2.5f)
            {
                // Need to be closer to the patient
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                return;
            }

            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_EMERGENCY)
            {
                // The player is not a medic
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_emergency_faction);
                return;
            }

            if (target.Health >= 100)
            {
                // The target player is not injured
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_hurt);
                return;
            }

            // We heal the character
            target.Health = 100;

            foreach (Client targetPlayer in NAPI.Pools.GetAllPlayers())
            {
                if (targetPlayer.Position.DistanceTo(player.Position) < 20.0f)
                {
                    string message = string.Format(InfoRes.medic_reanimated, player.Name, target.Name);
                    targetPlayer.SendChatMessage(Constants.COLOR_CHAT_ME + message);
                }
            }

            // Send the confirmation message to both players
            player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.medic_healed_player, target.Name));
            target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.player_healed_medic, player.Name));
        }

        [Command(Commands.COM_REANIMATE, Commands.HLP_REANIMATE_COMMAND)]
        public void ReanimateCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_EMERGENCY)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_emergency_faction);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null || player.Position.DistanceTo(target.Position) > 2.5f)
                {
                    // Need to be closer to the patient
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    return;
                }

                if (target.GetSharedData(EntityData.PLAYER_KILLED) == 0)
                {
                    // The player is not dead
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_dead);
                    return;
                }

                if (GetRemainingBlood() == 0)
                {
                    // There's no blood left
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_blood_left);
                    return;

                }

                // We create blood model
                BloodModel bloodModel = new BloodModel();
                {
                    bloodModel.doctor = player.GetData(EntityData.PLAYER_SQL_ID);
                    bloodModel.patient = target.GetData(EntityData.PLAYER_SQL_ID);
                    bloodModel.type = string.Empty;
                    bloodModel.used = true;
                }

                // Cancel the player's death
                CancelPlayerDeath(target);

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // Add the blood consumption to the database
                        bloodModel.id = Database.AddBloodTransaction(bloodModel);
                        bloodList.Add(bloodModel);

                        // Send the confirmation message to both players
                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + string.Format(InfoRes.player_reanimated, target.Name));
                        target.SendChatMessage(Constants.COLOR_SUCCESS + string.Format(SuccRes.target_reanimated, player.Name));
                    });
                });
            }
        }
        [Command(Commands.COM_BLS, Commands.HLP_MD_BLS_COMMAND)]
        public void BLScommand(Client player, string targetString)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsMedicMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are not a member of the Medic Department");
            }
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target != null)
            {
                if (player.Position.DistanceTo(target.Position) > 1.5f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + "You cant use it on yourself.");
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_INFO + "You stabilised " + target);
                    target.SetData(EntityData.TIME_HOSPITAL_RESPAWN, 600);
                }
            }
        }
        [Command(Commands.COM_MDREVIVE, Commands.HLP_MD_REVIVE_COMMAND)]
        public void ReviveCommand(Client player, string targetString)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsMedicMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are not a member of the Medic Department");
            }
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target != null)
            {
                if (player.Position.DistanceTo(target.Position) > 1.5f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + "You cant use this on yourself.");
                }
                else
                {
                    target.SendChatMessage(Constants.COLOR_INFO + player + " patched you up.");
                    player.SendChatMessage(Constants.COLOR_INFO + "You patched up " + target);
                    CancelPlayerDeath(target);

                }
            }
        }
        [Command(Commands.COM_MDSLAY, Commands.HLP_MD_SLAY_COMMAND)]
        public void SlayCommand(Client player, string targetString)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsMedicMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are not a member of the Medic Department");
            }
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target != null)
            {
                if (player.Position.DistanceTo(target.Position) > 1.5f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + "You cant use this to kill yourself.");
                }
                else
                {
                    target.TriggerEvent("togglePlayerDead", true);
                    TeleportPlayerToHospital(target);
                }
            }
        }

        [Command(Commands.COM_EXTRACT, Commands.HLP_EXTRACT_COMMAND)]
        public void ExtractCommand(Client player, string targetString)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_EMERGENCY)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_emergency_faction);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null || player.Position.DistanceTo(target.Position) > 5.0f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    return;
                }

                if (target.Health <= 15)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.low_blood);
                    return;
                }

                // Substract the blood from the player
                target.Health -= 15;

                // We create the blood model
                BloodModel blood = new BloodModel();
                {
                    blood.doctor = player.GetData(EntityData.PLAYER_SQL_ID);
                    blood.patient = target.GetData(EntityData.PLAYER_SQL_ID);
                    blood.type = string.Empty;
                    blood.used = false;
                }

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                            // We add the blood unit to the database
                            blood.id = Database.AddBloodTransaction(blood);
                        bloodList.Add(blood);

                        player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.blood_extracted, target.Name));
                        target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.blood_given, player.Name));
                    });
                });
            }
        }

        [Command(Commands.COM_DIE)]
        public void DieCommand(Client player)
        {
            // Check if the player is dead
            if (player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) != null)
            {
                int totalSeconds = Globals.GetTotalSeconds();

                if (player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) <= totalSeconds)
                {
                    // Move player to the hospital
                    TeleportPlayerToHospital(player);

                    // Get the report generated with the death
                    FactionWarningModel factionWarn = Faction.GetFactionWarnByTarget(player.Value, Constants.FACTION_EMERGENCY);

                    if (factionWarn != null)
                    {
                        if (factionWarn.takenBy >= 0)
                        {
                            // Tell the player who attended the report it's been canceled
                            Client doctor = Globals.GetPlayerById(factionWarn.takenBy);
                            doctor.SendChatMessage(Constants.COLOR_INFO + InfoRes.faction_warn_canceled);
                        }

                        // Remove the report from the list
                        Faction.factionWarningList.Remove(factionWarn);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.death_time_not_passed);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_dead);
            }
        }
        #region Vehicle Creation and Returning
        [Command("mdreturn", GreedyArg = true)]
        public void Command_Freturn(Client player)
        {
            Vector3 triggerPosition = new Vector3(328.666f, -559.6287f, 28.74379f);

            if (triggerPosition.DistanceTo(player.Position) > 5f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at Pillbox Hopsital");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) != 2)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are not a medic.");
                    return;

                }
                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are not on duty");
                    return;
                }
                string playerName = player.GetData(EntityData.PLAYER_NAME);
                List<VehicleModel> vehicles = Database.LoadAllVehicles();
                Vehicle veh = null;
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Ambulance))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.FBI))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Police4))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.RIOT2))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Insurgent2))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.KAMACHO))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Police))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.PoliceT))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.NightShark))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.FBI2))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Policeb))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Where is your vehicle?");
                            return;
                        }

                        if (veh != null)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            return;
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "No vehicle is registered to you.");
                        }
                    }
                }
            }
        }

        [Command("mdveh", GreedyArg = true)]
        public void Command_FactionV(Client player, string args)
        {
            int playerJob = player.GetData(EntityData.PLAYER_FACTION);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(328.666f, -559.6287f, 28.74379f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {

            }
            else
            {

                if (playerDuty == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Your not on duty");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName)
                    {
                        if (v.model == (uint)VehicleHash.Ambulance)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.Police2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.Police3)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.Police4))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.FBI)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.FBI2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.Insurgent2)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.KAMACHO))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.RIOT2)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.NightShark))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.Policeb)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                        }
                        if (v.model == (uint)VehicleHash.PoliceT)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.FBI2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                    }

                }
                int vehicleId = 0;
                VehicleModel vehicle = new VehicleModel();
                if (args.Trim().Length > 0)
                {
                    string[] arguments = args.Split(' ');
                    switch (arguments[0].ToLower())
                    {
                        case Commands.ARG_AMB:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsMedicMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Ambulance;
                                        vehicle.faction = Constants.FACTION_EMERGENCY;
                                        vehicle.position = new Vector3(330.469f, -549.5257f, 28.74378f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "0,0,0";
                                        vehicle.pearlescent = 0;
                                        vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                        vehicle.plate = player.GetData(EntityData.PLAYER_POLICE_UNIT);
                                        vehicle.price = 0;
                                        vehicle.parking = 0;
                                        vehicle.parked = 0;
                                        vehicle.gas = 50.0f;
                                        vehicle.kms = 0.0f;


                                        // Create the vehicle
                                        Vehicles.CreateVehicle(player, vehicle, true);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Medic");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;

                        case Commands.ARG_MC:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsMedicMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Policeb;
                                        vehicle.faction = Constants.FACTION_EMERGENCY;
                                        vehicle.position = new Vector3(330.469f, -549.5257f, 28.74378f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "214, 29, 19";
                                        vehicle.secondColor = "0,0,0";
                                        vehicle.pearlescent = 0;
                                        vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                        vehicle.plate = player.GetData(EntityData.PLAYER_POLICE_UNIT);
                                        vehicle.price = 0;
                                        vehicle.parking = 0;
                                        vehicle.parked = 0;
                                        vehicle.gas = 50.0f;
                                        vehicle.kms = 0.0f;


                                        // Create the vehicle
                                        Vehicles.CreateVehicle(player, vehicle, true);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Medic");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;

                        case Commands.ARG_FBI:
                            if (player.GetData(EntityData.PLAYER_RANK) > 4)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsMedicMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.FBI;
                                        vehicle.faction = Constants.FACTION_EMERGENCY;
                                        vehicle.position = new Vector3(330.469f, -549.5257f, 28.74378f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "214, 29, 19";
                                        vehicle.secondColor = "0,0,0";
                                        vehicle.pearlescent = 0;
                                        vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                        vehicle.plate = player.GetData(EntityData.PLAYER_POLICE_UNIT);
                                        vehicle.price = 0;
                                        vehicle.parking = 0;
                                        vehicle.parked = 0;
                                        vehicle.gas = 50.0f;
                                        vehicle.kms = 0.0f;


                                        // Create the vehicle
                                        Vehicles.CreateVehicle(player, vehicle, true);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Medic");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_FBI2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsMedicMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.FBI2;
                                        vehicle.faction = Constants.FACTION_POLICE;
                                        vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "214, 29, 19";
                                        vehicle.secondColor = "214, 29, 19";
                                        vehicle.pearlescent = 1;
                                        vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                        vehicle.plate = player.GetData(EntityData.PLAYER_POLICE_UNIT);
                                        vehicle.price = 0;
                                        vehicle.parking = 0;
                                        vehicle.parked = 0;
                                        vehicle.gas = 50.0f;
                                        vehicle.kms = 0.0f;


                                        // Create the vehicle
                                        Vehicles.CreateVehicle(player, vehicle, true);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Medic");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                            /*  case Commands.ARG_KAMACHO:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 3)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.KAMACHO;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "256,256,256";
                                          vehicle.secondColor = "256,256,256";
                                          vehicle.pearlescent = 0;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              //
                              case Commands.ARG_NIGHTSHARK:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 3)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.NightShark;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "256,256,256";
                                          vehicle.secondColor = "256,256,256";
                                          vehicle.pearlescent = 0;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              case Commands.ARG_FBI:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 3)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.FBI;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "256,256,256";
                                          vehicle.secondColor = "0,0,0";
                                          vehicle.pearlescent = 1;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              case Commands.ARG_FBI2:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 3)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.FBI2;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "0,0,0";
                                          vehicle.secondColor = "0,0,0";
                                          vehicle.pearlescent = 0;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              case Commands.ARG_POLICE:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 0)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.Police;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "255,255,255";
                                          vehicle.secondColor = "0,0,0";
                                          vehicle.pearlescent = 0;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              case Commands.ARG_RIOT2:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 3)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.RIOT2;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "256,256,256";
                                          vehicle.secondColor = "256,256,256";
                                          vehicle.pearlescent = 1;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;
                              case Commands.ARG_POLICE2:
                                  if (player.GetData(EntityData.PLAYER_RANK) > 0)
                                  {
                                      if (Faction.IsPoliceMember(player))
                                      {
                                          // Basic data for vehicle creation
                                          vehicle.model = (uint)VehicleHash.Police2;
                                          vehicle.faction = Constants.FACTION_POLICE;
                                          vehicle.position = new Vector3(440.3717f, -1019.958f, 28.68561f);
                                          vehicle.rotation = player.Rotation;
                                          vehicle.dimension = player.Dimension;
                                          vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                          vehicle.firstColor = "255,255,255";
                                          vehicle.secondColor = "0,0,0";
                                          vehicle.pearlescent = 0;
                                          vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
                                          vehicle.plate = string.Empty;
                                          vehicle.price = 0;
                                          vehicle.parking = 0;
                                          vehicle.parked = 0;
                                          vehicle.gas = 50.0f;
                                          vehicle.kms = 0.0f;


                                          // Create the vehicle
                                          Vehicles.CreateVehicle(player, vehicle, true);
                                      }
                                      else
                                      {
                                          player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                      }
                                  }
                                  break;*/
                    }
                }
            }
        }
        #endregion

    }
}
