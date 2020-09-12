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

        public JobCommandHelper()
        {
            // Initialize the variables
            //CreateJobLocations();
            //CreateNPC();
        }

        [Command("info", GreedyArg = true)]
        public void Command_Jobs_Info(Client player)
        {
            if (OceanCleanerJobPos.DistanceTo(player.Position) < 1.75)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "We've got so much rubbish in the ocean that needs to be cleaned. Are you able to help out?");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "We'll provide you the equipment and vehicles to get the job done.");
            }
            else NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at a job location.");
        }
        [Command("join", GreedyArg = true)]
        public void Command_Jobs_Join(Client player)
        {
            if (OceanCleanerJobPos.DistanceTo(player.Position) < 1.75)
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 11)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "You already work for us buddy, did you forget? Haha.");
                    return;
                }

                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Welcome to the Ocean Cleaning Department of Los Santos!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Marcus: " + Constants.COLOR_WHITE + "Head over to Peter at Logistics to get your vehicle, also remember to let me know you're working today. " + Constants.COLOR_YELLOW + "/duty");

                player.SetData(EntityData.PLAYER_JOB, Constants.JOB_OCEANCLEANER);
            }
            else NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at a job location.");
        }

        [Command("quitjob", GreedyArg = true)]
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
            }
            Customization.SetDefaultClothes(player);

        }
    }
}


