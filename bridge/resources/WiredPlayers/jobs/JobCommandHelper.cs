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
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class JobCommandHelper : Script
    {
        public Vector3 OceanCleanerJobPos = new Vector3(288.0206f, -2981.255f, 5.862742f);
        public Vector3 OceanCleanerScrapYard = new Vector3(2355.144f, 3133.385f, 48.20871f);

        public Vector3 GarbageJobPos = new Vector3(-322.0652f, -1545.77f, 31.01992f);
        public Vector3 GarbageDumpPos = new Vector3(-352.2358f, -1560.054f, 25.21113f);

        public Vector3 TruckerJobPos = new Vector3(-44.04705f, -2519.901f, 7.394626f);

        public Vector3 PostalGQJobPos = new Vector3(499.7191f, -651.9603f, 24.90868f);


        public JobCommandHelper()
        {
            // Initialize the variables
            //CreateJobLocations();
            //CreateNPC();
        }
        //PUT THIS IN A UI
        [Command("info", GreedyArg = true)]
        public void Command_Jobs_Info(Client player)
        {
            if (OceanCleanerJobPos.DistanceTo(player.Position) < 1.75)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "We've got so much rubbish in the ocean that needs to be cleaned. Are you able to help out?");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "We'll provide you the equipment and vehicles to get the job done.");
            }
            if (GarbageJobPos.DistanceTo(player.Position) < 1.75)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "We have garbage bins all over the city. Go collect them!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "We provide you with the truck.");
            }
            if (TruckerJobPos.DistanceTo(player.Position) < 1.75)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"We have businesses all over the city that needs products.");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"We provide you with the truck.");
            }
            if (PostalGQJobPos.DistanceTo(player.Position) < 1.75)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"We have orders from people that have ordered stuff online. Amazon expects pronto delivery!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"We provide you with the truck.");
            }
            else NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at a job location.");
        }

        [Command("join", GreedyArg = true)]
        [RemoteEvent("JoinJob")]
        public void Command_Jobs_Join(Client player)
        {
            if (OceanCleanerJobPos.DistanceTo(player.Position) < 1.75)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 11)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "You already work here.");
                    return;
                }

                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Welcome to the Ocean Cleaning Department of Los Santos!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Head over to Peter at Logistics to get your vehicle, also remember to let me know you're working today. " + Constants.COLOR_YELLOW + "/duty");

                player.SetData(EntityData.PLAYER_JOB, Constants.JOB_OCEANCLEANER);
            }
            if (GarbageJobPos.DistanceTo(player.Position) < 1.75)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 12)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "You already work here.");
                    return;
                }

                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "Welcome to the Garbage Department of Los Santos!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "Go on duty in order to bring out your vehicle. " + Constants.COLOR_YELLOW + "/duty");

                //player.SetData(EntityData.PLAYER_JOB, Constants.JOB_GARBAGEMAN);
            }
            if (TruckerJobPos.DistanceTo(player.Position) < 1.75)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 8)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Keith Dumper: " + Constants.COLOR_WHITE + "You already work here.");
                    return;
                }

                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Keith Dumper: " + Constants.COLOR_WHITE + "Welcome to the Truckers Union of Los Santos!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Keith Dumper: " + Constants.COLOR_WHITE + "Go on duty in order to bring out your vehicle. " + Constants.COLOR_YELLOW + "/duty");

                player.SetData(EntityData.PLAYER_JOB, Constants.JOB_TRUCKER);
            }
            if (PostalGQJobPos.DistanceTo(player.Position) < 1.75)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 12)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Jimmy: " + Constants.COLOR_WHITE + "You already work here.");
                    return;
                }

                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Jimmy: " + Constants.COLOR_WHITE + "Welcome to PostalGQ Los Santos!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Jimmy: " + Constants.COLOR_WHITE + "Go on duty in order to bring out your vehicle. " + Constants.COLOR_YELLOW + "/duty");

                player.SetData(EntityData.PLAYER_JOB, Constants.JOB_POSTALGQ);
            }
            else
                return;
        }

        [Command("quitjob", GreedyArg = true)]
        [RemoteEvent("Quitjob")]
        public void Command_Jobs_Quit(Client player)
        {
            if (player.GetData(EntityData.PLAYER_JOB) == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You don't have a job.");
                return;
            }           

            if (OceanCleanerJobPos.DistanceTo(player.Position) < 1.75)
            {

                if (player.GetData(EntityData.PLAYER_JOB) != 11)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Uhh, you don't work for us buddy..");
                    return;
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Sorry to hear that, and thank you for your service in making the ocean, a better place.");
                }

            }
            if (TruckerJobPos.DistanceTo(player.Position) < 1.75)
            {

                if (player.GetData(EntityData.PLAYER_JOB) != 8)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Keith Dumper: " + Constants.COLOR_WHITE + "Uhh, you don't work here, or do you?");
                    return;
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Keith Dumper: " + Constants.COLOR_WHITE + "Sorry to hear that, but thank you for your service.");
                }

            }
            if (PostalGQJobPos.DistanceTo(player.Position) < 1.75)
            {

                if (player.GetData(EntityData.PLAYER_JOB) != 12)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Jimmy: " + Constants.COLOR_WHITE + "Uhh, you don't work here, or do you?");
                    return;
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Jimmy: " + Constants.COLOR_WHITE + "Sorry to hear that, but thank you for your service.");
                }

            }

            if (GarbageJobPos.DistanceTo(player.Position) < 1.75)
            {

                if (player.GetData(EntityData.PLAYER_JOB) != 4)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "You don't work for us. Get lost!");
                    return;
                }
                else
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thomas: " + Constants.COLOR_WHITE + "Sorry to hear that.");
                }
            }

            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You have quit your job.");
            player.SetData(EntityData.PLAYER_JOB, Constants.JOB_NONE);
            player.SetData(EntityData.PLAYER_ON_DUTY, 0);

            Vehicle veh = null;
            string playerName = player.GetData(EntityData.PLAYER_NAME);

            List<VehicleModel> vehicles = Database.LoadAllVehicles();

            foreach (VehicleModel v in vehicles)
            {
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Submersible2))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Delete();
                        Database.RemoveVehicle(vehicleId);
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Scrap))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Delete();
                        Database.RemoveVehicle(vehicleId);
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Phantom))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Delete();
                        Database.RemoveVehicle(vehicleId);
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Trailers2))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Delete();
                        Database.RemoveVehicle(vehicleId);
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Boxville2))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Delete();
                        Database.RemoveVehicle(vehicleId);
                    }
                }
            }
            Customization.ApplyPlayerClothes(player);

        }
    }
}


