using GTANetworkAPI;
using WiredPlayers.jobs;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.model;
using WiredPlayers.drivingschool;
using WiredPlayers.weapons;
using WiredPlayers.vehicles;
using WiredPlayers.character;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using WiredPlayers.chat;

namespace WiredPlayers.factions
{
    public class Prison_Guards : Script
    {
        public static List<CrimeModel> crimeList;
        public static List<PoliceControlModel> policeControlList;
        private static Timer reinforcesTimer;

        public Prison_Guards()
        {
            // Initialize reinforces updater
            reinforcesTimer = new Timer(UpdateReinforcesRequests, null, 250, 250);

            // Create all the equipment places
            foreach(Vector3 pos in Constants.EQUIPMENT_DOC)
            {
                NAPI.TextLabel.CreateTextLabel("/" + Commands.COM_EQUIP, pos, 10.0f, 0.5f, 4, new Color(190, 235, 100), false, 0);
                NAPI.TextLabel.CreateTextLabel("Type the command to get Doc equipment.", new Vector3(pos.X, pos.Y, pos.Z - 0.1f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);

                // Create blips
                Blip policeBlip = NAPI.Blip.CreateBlip(pos);
                policeBlip.Name = "Prison";
                policeBlip.ShortRange = true;
                policeBlip.Sprite = 60;
            }
            CreateJobLocation();
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(1840.681f, 2541.803f, 45.83076f);
            Vector3 jobPosDesc = jobPos;
            Vector3 dutypos = new Vector3(268.8305f, -1363.443f, 24.53779f);
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/duty~w~To go on and off duty.", dutypos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/Dveh~w~ to get your vehicle and ~y~/Dreturn~w~ to return your vehicle.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
           //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }
        #region Vehicle Creation and Returning
        [Command("dreturn", GreedyArg = true)]
        public void Command_Freturn(Client player)
        {
            Vector3 triggerPosition = new Vector3(1840.681f, 2541.803f, 45.83076f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at the Prison Vehicle Depo");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) != 21)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are not a Prison Guard.");
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Police2))
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Police3))
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
            }
        }

        [Command("dveh", GreedyArg = true)]
        public void Command_FactionV(Client player, string args)
        {
            int playerJob = player.GetData(EntityData.PLAYER_FACTION);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(1840.681f, 2541.803f, 45.83076f);

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
                        if (v.model == (uint)VehicleHash.Police)
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
                        case Commands.ARG_POLICE3:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Police3;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_POLICE4:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Police4;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "0,0,0";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_POLICET:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.PoliceT;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_INSURGENT2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Insurgent2;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "256,256,256";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_KAMACHO:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.KAMACHO;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "256,256,256";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        //
                        case Commands.ARG_PRISONB:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.PBus;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "256,256,256";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_FBI:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.FBI;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "0,0,0";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
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
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.FBI2;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "0,0,0";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_POLICE:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Police;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "255,255,255";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_RIOT2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.RIOT2;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = "256,256,256";
                                        vehicle.secondColor = "256,256,256";
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                        case Commands.ARG_POLICE2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
                                {
                                    if (Faction.IsDocMember(player))
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = (uint)VehicleHash.Police2;
                                        vehicle.faction = Constants.FACTION_DOC;
                                        vehicle.position = new Vector3(1854.487f, 2545.65f, 45.67207f);
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
                                        player.SendChatMessage(Constants.COLOR_HELP + "You are not a Prison Guard");
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
                                }
                            }
                            break;
                    }
                }
            }
        }
        #endregion
        private void RemoveClosestPoliceControlItem(Client player, int hash)
        {
            // Get the closest police control item
            PoliceControlModel policeControl = policeControlList.Where(control => control.controlObject != null && control.controlObject.Position.DistanceTo(player.Position) < 2.0f && control.item == hash).FirstOrDefault();

            if (policeControl != null)
            {
                policeControl.controlObject.Delete();
                policeControl.controlObject = null;
            }
        }

        private void UpdateReinforcesRequests(object unused)
        {
            NAPI.Task.Run(() =>
            {
                List<ReinforcesModel> docReinforces = new List<ReinforcesModel>();
                List<Client> docMembers = NAPI.Pools.GetAllPlayers().Where(x => x.GetData(EntityData.PLAYER_PLAYING) != null && Faction.IsPoliceMember(x)).ToList();

                foreach (Client doc in docMembers)
                {
                    if (doc.GetData(EntityData.PLAYER_REINFORCES) != null)
                    {
                        ReinforcesModel reinforces = new ReinforcesModel(doc.Value, doc.Position);
                        docReinforces.Add(reinforces);
                    }
                }

                string reinforcesJsonList = NAPI.Util.ToJson(docReinforces);

                foreach (Client doc in docMembers)
                {
                    if (doc.GetData(EntityData.PLAYER_PLAYING) != null)
                    {
                        // Update reinforces position for each policeman
                        doc.TriggerEvent("updatePoliceReinforces", reinforcesJsonList);
                    }
                }
            });
        }

        private bool IsCloseToEquipmentLockers(Client player)
        {
            // Check if the player is close to any equipment label
            return Constants.EQUIPMENT_DOC.Where(p => player.Position.DistanceTo(p) < 2.0f).Count() > 0;
        }

        private bool IsPlayerInJailArea(Client player)
        {
            // Check if the player is in any of the jail areas
            return Constants.EQUIPMENT_DOC.Where(p => player.Position.DistanceTo(p) < 5.0f).Count() > 0;
        }

        [RemoteEvent("applyCrimesToPlayer")]
        public void ApplyCrimesToPlayerEvent(Client player, string crimeJson)
        {
            int fine = 0, jail = 0;
            Client target = player.GetData(EntityData.PLAYER_INCRIMINATED_TARGET);
            List<CrimeModel> crimeList = NAPI.Util.FromJson<List<CrimeModel>>(crimeJson);

            // Calculate fine amount and jail time
            foreach (CrimeModel crime in crimeList)
            {
                fine += crime.fine;
                jail += crime.jail;
            }
            
            Random random = new Random();
            target.Position = Constants.PRISON_SPAWNS[random.Next(3)];
            player.SetData(EntityData.PLAYER_INCRIMINATED_TARGET, target);

            // Remove money and jail the player
            int money = target.GetSharedData(EntityData.PLAYER_MONEY);
            target.SetSharedData(EntityData.PLAYER_MONEY, money - fine);
            target.SetData(EntityData.PLAYER_JAIL_TYPE, Constants.JAIL_TYPE_IC);
            target.SetData(EntityData.PLAYER_JAILED, jail);
        }
        [Command(Commands.COM_CHECK_TIME)]
        public void CheckTimeCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsDocMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                }
            
        }

        [Command(Commands.COM_FRISK, Commands.HLP_FRISK_COMMAND)]
        public void FriskCommand(Client player, string targetString)
        {
            string timeString = "[" + DateTime.Now.ToString("HH:mm");
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsPoliceMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_searched_himself);
                    }
                    else
                    {
                        List<InventoryModel> inventory = Globals.GetPlayerInventoryAndWeapons(target);
                        player.SetData(EntityData.PLAYER_SEARCHED_TARGET, target);

                        string message = string.Format(InfoRes.player_frisk, player.Name, target.Name);
                        Chat.SendMessageToNearbyPlayers(timeString, player, message, Constants.MESSAGE_ME, 20.0f, true);

                        // Show target's inventory to the player
                        player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_PLAYER);
                    }
                }
                else
                {

                   
                
                }
            
                    {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                    }
            }
        }

        [Command(Commands.COM_DJAIL, Commands.HLP_DJAIL_COMMAND)]
        public void IncriminateCommand(Client player, string targetString)
        {
            if (!IsPlayerInJailArea(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_jail_area);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (!Faction.IsDocMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_incriminated_himself);
                    }
                    else
                    {
                        player.SetData(EntityData.PLAYER_INCRIMINATED_TARGET, target);
                        player.TriggerEvent("showCrimesMenu", NAPI.Util.ToJson(crimeList));
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_HANDCUFF, Commands.HLP_HANDCUFF_COMMAND)]
        public void HandcuffCommand(Client player, string targetString)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsDocMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
            else
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (player.Position.DistanceTo(target.Position) > 1.5f)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    }
                    else if (target == player)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_handcuffed_himself);
                    }
                    else if (target.GetSharedData(EntityData.PLAYER_HANDCUFFED) == null)
                    {
                        if(target.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
                        {
                            // Remove the item on the player's hand
                            Globals.StoreItemOnHand(target);
                        }

                        // Remove the player weapon
                        player.GiveWeapon(WeaponHash.Unarmed, 0);

                        // Handcuff the player
                        Globals.AttachItemToPlayer(target, 0, NAPI.Util.GetHashKey("prop_cs_cuffs_01").ToString(), "IK_R_Hand", new Vector3(), new Vector3(), EntityData.PLAYER_HANDCUFFED);
                        target.PlayAnimation("mp_arresting", "idle", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.OnlyAnimateUpperBody | Constants.AnimationFlags.AllowPlayerControl));
                        
                        string playerMessage = string.Format(InfoRes.cuffed, target.Name);
                        string targetMessage = string.Format(InfoRes.cuffed_by, player.Name);
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                    }
                    else
                    {
                        // Remove the cuffs from the player
                        NAPI.ClientEvent.TriggerClientEventInDimension(target.Dimension, "dettachItemFromPlayer", target.Value);
                        target.SetSharedData(EntityData.PLAYER_HANDCUFFED, null);
                        target.StopAnimation();

                        string playerMessage = string.Format(InfoRes.uncuffed, target.Name);
                        string targetMessage = string.Format(InfoRes.uncuffed_by, player.Name);
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_EQUIP, Commands.HLP_EQUIP_COMMAND, GreedyArg = true)]
        public void EquipmentCommand(Client player, string action, string type = "")
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (!IsCloseToEquipmentLockers(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_room_lockers);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (Faction.IsDocMember(player))
            {
                switch (action.ToLower())
                {
                    case Commands.ARG_BASIC:
                        player.Armor = 100;

                        Weapons.GivePlayerNewWeapon(player, WeaponHash.Flashlight, 0, false);
                        Weapons.GivePlayerNewWeapon(player, WeaponHash.Nightstick, 0, true);
                        Weapons.GivePlayerNewWeapon(player, WeaponHash.StunGun, 0, true);

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.equip_basic_received);
                        break;
                /*    case Commands.ARG_AMMUNITION:
                        if (player.GetData(EntityData.PLAYER_RANK) > 1)
                        {
                            // Get the player weapons and ammunition
                            player.TriggerEvent("getPlayerWeapons", "");

                            WeaponHash[] playerWeaps = player.Weapons;
                            foreach (WeaponHash playerWeap in playerWeaps)
                            {
                                string ammunition = Weapons.GetGunAmmunitionType(playerWeap);
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel bulletItem = Globals.GetPlayerItemModelFromHash(playerId, ammunition);
                                if (bulletItem != null)
                                {
                                    switch (playerWeap)
                                    {
                                        case WeaponHash.CombatPistol:
                                            bulletItem.amount += Constants.STACK_PISTOL_CAPACITY;
                                            break;
                                        case WeaponHash.SMG:
                                            bulletItem.amount += Constants.STACK_MACHINEGUN_CAPACITY;
                                            break;
                                        case WeaponHash.CarbineRifle:
                                            bulletItem.amount += Constants.STACK_ASSAULTRIFLE_CAPACITY;
                                            break;
                                        case WeaponHash.PumpShotgun:
                                            bulletItem.amount += Constants.STACK_SHOTGUN_CAPACITY;
                                            break;
                                        case WeaponHash.SniperRifle:
                                            bulletItem.amount += Constants.STACK_SNIPERRIFLE_CAPACITY;
                                            break;
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the bullet's amount
                                            Database.UpdateItem(bulletItem);
                                        });
                                    });
                                }
                                else
                                {
                                    bulletItem = new ItemModel();
                                    {
                                        bulletItem.hash = ammunition;
                                        bulletItem.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                        bulletItem.ownerIdentifier = playerId;
                                        bulletItem.amount = 30;
                                        bulletItem.position = new Vector3(0.0f, 0.0f, 0.0f);
                                        bulletItem.dimension = 0;
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            bulletItem.id = Database.AddNewItem(bulletItem);
                                            Globals.itemList.Add(bulletItem);
                                        });
                                    });
                                }
                            }
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.equip_ammo_received);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_police_rank);
                        }
                        break;*/
                    case Commands.ARG_WEAPON:
                        if (player.GetData(EntityData.PLAYER_RANK) > 1)
                        {
                            // Check if the player typed any weapon
                            if(type == null || type.Length == 0)
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_EQUIP_WEAP_COMMAND);
                                return;
                            }

                            WeaponHash selectedWeap = new WeaponHash();
                            switch (type.ToLower())
                            {
                                case Commands.ARG_PISTOL:
                                    selectedWeap = WeaponHash.CombatPistol;
                                    break;
                                case Commands.ARG_REVOLVER:
                                    selectedWeap = WeaponHash.Revolver;
                                    break;
                                case Commands.ARG_MACHINE_GUN:
                                    selectedWeap = WeaponHash.SMG;
                                    break;
                                case Commands.ARG_ASSAULT:
                                    selectedWeap = WeaponHash.CarbineRifle;
                                    break;
                                case Commands.ARG_SNIPER:
                                    selectedWeap = WeaponHash.SniperRifle;
                                    break;
                                case Commands.ARG_SHOTGUN:
                                    selectedWeap = WeaponHash.PumpShotgun;
                                    break;
                                default:
                                    selectedWeap = 0;
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_EQUIP_WEAP_COMMAND);
                                    break;
                            }

                            if (selectedWeap != 0)
                            {
                                Weapons.GivePlayerNewWeapon(player, selectedWeap, 0, true);
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.equip_weap_received);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_police_rank);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_EQUIP_COMMAND);
                        break;
                }
            }
        }

        [Command(Commands.COM_PUT, Commands.HLP_POLICE_PUT_COMMAND)]
        public void PutCommand(Client player, string item)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }

            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                return;
            }

            if(!Faction.IsDocMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
                return;
            }

            PoliceControlModel policeControl;

            switch (item.ToLower())
            {
                case Commands.ARG_CONE:
                    policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_CONE, player.Position, player.Rotation);
                    policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                    policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_CONE, policeControl.position, policeControl.rotation);
                    policeControlList.Add(policeControl);
                    break;
                case Commands.ARG_BEACON:
                    policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BEACON, player.Position, player.Rotation);
                    policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                    policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BEACON, policeControl.position, policeControl.rotation);
                    policeControlList.Add(policeControl);
                    break;
                case Commands.ARG_BARRIER:
                    policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BARRIER, player.Position, player.Rotation);
                    policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                    policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BARRIER, policeControl.position, policeControl.rotation);
                    policeControlList.Add(policeControl);
                    break;
                case Commands.ARG_SPIKES:
                    policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_SPIKES, player.Position, player.Rotation);
                    policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                    policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_SPIKES, policeControl.position, policeControl.rotation);
                    policeControlList.Add(policeControl);
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_PUT_COMMAND);
                    break;
            }
        }

        [Command(Commands.COM_REMOVE, Commands.HLP_POLICE_REMOVE_COMMAND)]
        public void RemoveCommand(Client player, string item)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (!Faction.IsPoliceMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
            else
            {
                switch (item.ToLower())
                {
                    case Commands.ARG_CONE:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_CONE);
                        break;
                    case Commands.ARG_BEACON:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BEACON);
                        break;
                    case Commands.ARG_BARRIER:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BARRIER);
                        break;
                    case Commands.ARG_SPIKES:
                        RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_SPIKES);
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_REMOVE_COMMAND);
                        break;
                }
            }
        }

        [Command(Commands.COM_REINFORCEMENTS, Alias = Commands.COM_RF)]
        public void ReinforcesCommand(Client player)
        {
            if (!Faction.IsPoliceMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
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
                // Get police department's members
                List<Client> policeMembers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_ON_DUTY) == 1 && Faction.IsPoliceMember(p)).ToList();

                if (player.GetData(EntityData.PLAYER_REINFORCES) != null)
                {
                    string targetMessage = string.Format(InfoRes.target_reinforces_canceled, player.Name);

                    foreach (Client target in policeMembers)
                    {
                        // Remove the blip from the map
                        target.TriggerEvent("reinforcesRemove", player.Value);
                            
                        if (player == target)
                        {
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_reinforces_canceled);
                        }
                        else
                        {
                            target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                        }
                    }

                    // Remove player's reinforces
                    player.ResetData(EntityData.PLAYER_REINFORCES);
                }
                else
                {
                    string targetMessage = string.Format(InfoRes.target_reinforces_asked, player.Name);

                    foreach (Client target in policeMembers)
                    {
                        if (player == target)
                        {
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_reinforces_asked);
                        }
                        else
                        {
                            target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                        }
                    }

                    // Ask for reinforces
                    player.SetData(EntityData.PLAYER_REINFORCES, true);
                }
            }
        }

        [Command(Commands.COM_LICENSE, Commands.HLP_LICENSE_COMMAND, GreedyArg = true)]
        public void LicenseCommand(Client player, string args)
        {
            if (Faction.IsPoliceMember(player) && player.GetData(EntityData.PLAYER_RANK) == 6)
            {
                string[] arguments = args.Trim().Split(' ');
                if (arguments.Length == 3 || arguments.Length == 4)
                {
                    Client target;

                    // Get the target player
                    if (int.TryParse(arguments[2], out int targetId) && arguments.Length == 3)
                    {
                        target = Globals.GetPlayerById(targetId);
                    }
                    else
                    {
                        target = NAPI.Player.GetPlayerFromName(arguments[2] + arguments[3]);
                    }

                    // Check whether the target player is connected
                    if (target == null || target.GetData(EntityData.PLAYER_PLAYING) == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                    }
                    else if (player.Position.DistanceTo(target.Position) > 2.5f)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    }
                    else
                    {
                        string playerMessage;
                        string targetMessage;

                        switch (arguments[0].ToLower())
                        {
                            case Commands.ARG_GIVE:
                                switch (arguments[1].ToLower())
                                {
                                    case Commands.ARG_WEAPON:
                                        // Add one month to the license
                                        target.SetData(EntityData.PLAYER_WEAPON_LICENSE, Globals.GetTotalSeconds() + 2628000);
                                        
                                        playerMessage = string.Format(InfoRes.weapon_license_given, target.Name);
                                        targetMessage = string.Format(InfoRes.weapon_license_received, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_LICENSE_COMMAND);
                                        break;
                                }
                                break;
                            case Commands.ARG_REMOVE:
                                switch (arguments[1].ToLower())
                                {
                                    case Commands.ARG_WEAPON:
                                        // Adjust the date to the current one
                                        target.SetData(EntityData.PLAYER_WEAPON_LICENSE, Globals.GetTotalSeconds());
                                        
                                        playerMessage = string.Format(InfoRes.weapon_license_removed, target.Name);
                                        targetMessage = string.Format(InfoRes.weapon_license_lost, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    case Commands.ARG_CAR:
                                        // Remove car license
                                        DrivingSchool.SetPlayerLicense(target, Constants.LICENSE_CAR, -1);
                                        
                                        playerMessage = string.Format(InfoRes.car_license_removed, target.Name);
                                        targetMessage = string.Format(InfoRes.car_license_lost, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    case Commands.ARG_MOTORCYCLE:
                                        // Remove motorcycle license
                                        DrivingSchool.SetPlayerLicense(target, Constants.LICENSE_MOTORCYCLE, -1);
                                        
                                        playerMessage = string.Format(InfoRes.moto_license_removed, target.Name);
                                        targetMessage = string.Format(InfoRes.moto_license_lost, player.Name);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                       target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_LICENSE_COMMAND);
                                break;
                        }
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_LICENSE_COMMAND);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_police_chief);
            }
        }

        [Command(Commands.COM_BREATHALYZER, Commands.HLP_ALCOHOLIMETER_COMMAND)]
        public void BreathalyzerCommand(Client player, string targetString)
        {
            if (Faction.IsPoliceMember(player) && player.GetData(EntityData.PLAYER_RANK) > 0)
            {
                float alcoholLevel = 0.0f;
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target.GetData(EntityData.PLAYER_DRUNK_LEVEL) != null)
                {
                    alcoholLevel = target.GetData(EntityData.PLAYER_DRUNK_LEVEL);
                }
                
                string playerMessage = string.Format(InfoRes.alcoholimeter_test, target.Name, alcoholLevel);
                string targetMessage = string.Format(InfoRes.alcoholimeter_receptor, player.Name, alcoholLevel);
                player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
               target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
            }
        }

        [Command(Commands.COM_COMPUTER)]
        public void ComputerCommand(Client player, string targetString)
        {
            if(!Faction.IsPoliceMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
                return;
            }

            if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                return;
            }

            if (player.Vehicle.GetData(EntityData.VEHICLE_FACTION) != Constants.FACTION_POLICE && player.Vehicle.GetData(EntityData.VEHICLE_FACTION) != Constants.FACTION_SHERIFF)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_your_job_vehicle);
                return;
            }

            // Get the player from the input string
            Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            // Show the data from the player
            PlayerData.RetrieveBasicDataEvent(player, target.Value);
        }
    }
}
