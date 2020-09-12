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
{/*
    public class Deliveryman : Script
    {
        public List<Vector3> allLocations = new List<Vector3>();
        public List<Vector3> planeWreck = new List<Vector3>();
        public List<uint> rareInformationsObjects = new List<uint>();

        public Deliveryman()
        {
            CreateJobLocation();
            CreateNPC();
            //   GenerateObjects();

        }

        //  public void AddWeaponcrateObjects()
        // {
        //    rareInformationsObjects.Add(2249224369);
        //  }

        // public void GenerateObjects()
        //{
        // #region All Coordinates in the Ocean
        // allLocations.Add(new Vector3(35.48105f, 6662.523f, 32.1904f));
        // allLocations.Add(new Vector3(-229.8452f, 6445.398f, 31.19744f));
        // allLocations.Add(new Vector3(-447,6555f, 6271,316f, 33,33003f));
        // allLocations.Add(new Vector3(-11.06648, -3344.253, -21.419));
        // allLocations.Add(new Vector3(391.7362, -3350.827, -23.7604));
        // allLocations.Add(new Vector3(380.4103, -3191.509, -20.63347));
        // allLocations.Add(new Vector3(577.8287, -3398.531, -27.2825));
        // allLocations.Add(new Vector3(773.7352, -3436.287, -23.63369));
        // allLocations.Add(new Vector3(863.4482, -3483.535, -20.07652));
        // allLocations.Add(new Vector3(895.7641, -3473.618, -25.42162));
        // allLocations.Add(new Vector3(899.8591, -3534.702, -42.41016));
        // #endregion

        //  #region Plane Crash in Ocean

        //   planeWreck.Add(new Vector3(1754.578, -2990.057, -52.74268));
        //  planeWreck.Add(new Vector3(1814.195, -2956.95, -43.92405));
        //  planeWreck.Add(new Vector3(1830.879, -2971.892, -51.30523));
        //  planeWreck.Add(new Vector3(1862.498, -2938.683, -44.04823));
        //  planeWreck.Add(new Vector3(1826.038, -2918.893, -35.34026));

        //   #endregion

        // Creating Solid Objects
        // for (int i = 0; i < allLocations.Count; i++)
        // {
        //    NAPI.Object.CreateObject(1723871309, allLocations[i], new Vector3(0, 0, 0)); //Spawned object is visible in all Dimensions
        //   NAPI.TextLabel.CreateTextLabel("~y~Information             ~n~~y~/collect", allLocations[i], 40.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
        //}

        // Creating Non-Object Locations
        // for (int i = 0; i < planeWreck.Count; i++)
        // {
        //   NAPI.TextLabel.CreateTextLabel("~y~Information: ~w~Plane Wreckage         ~n~~y~/collect", planeWreck[i], 40.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
        // }
        //  }

        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(58.92189, 6333.011, 31.24424f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/startdelivery~w~ to get your vehicle and ~y~/stopdelivery~w~ to return your vehicle.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~r~Penalty Price: ~w~$500", new Vector3(58.92189, 6333.011, 31.00424f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            ColShape trigger = NAPI.ColShape.CreateCylinderColShape(new Vector3(285.238, -2982.185, 5.564789), 2.5f, 5.0f);
            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        public void CreateNPC()
        {
            Vector3 npcPos = new Vector3(95.79912, 6367.596, 33.07588f);
            Vector3 npcDockPos = new Vector3(58.92189, 6333.011, 32.24424f);

            NAPI.TextLabel.CreateTextLabel("~r~Delivery supervisor", npcPos, 4.0f, 2.0f, 2, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Johnny", new Vector3(95.79912f, 6367.596f, 32.37588f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~b~/duty~w~ to go on duty.", new Vector3(95.79912f, 6367.596f, 30.87588f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/info~w~ for more information on this job", new Vector3(95.79912f, 6367.596f, 30.74588f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~g~/join~w~ to become a part of the ~b~GQ Delivery Organisation~w~ of Los Santos.", new Vector3(95.79912f, 6367.596f, 30.64588f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~r~/quitjob~w~ to leave the company.", new Vector3(95.79912f, 6367.596f, 30.57588f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name


            NAPI.TextLabel.CreateTextLabel("~r~Delivery Vehicles", npcDockPos, 4.0f, 2.0f, 2, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Rogger", new Vector3(58.92189, 6333.011, 32.37424f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/recovervehicle  ~w~ to get your vehicle retrieved back by logistics.", new Vector3(58.92189, 6333.011, 31.47424f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name


            ColShape trigger = NAPI.ColShape.CreateCylinderColShape(new Vector3(285.238, -2982.185, 5.564789), 2.5f, 5.0f);
            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape shape, Client player)
        {
            //NAPI.Chat.SendChatMessageToPlayer(player, "~b~Ocean Cleaner Freelancer: ~w~In this job, you will clean up the ocean and be paid.");
            //NAPI.Chat.SendChatMessageToPlayer(player, "~w~When you find objects on the ocean floor, use ~y~/collect");
            //NAPI.Chat.SendChatMessageToPlayer(player, "~w~Use ~y~/sellscrap~w~ at the scrapyard to get paid.");
        }

        [Command("recovervehicle", GreedyArg = true)]
        public void Command_Job_Deliveryman_Bringvehicle(Client player)
        {
            Vector3 triggerPosition = new Vector3(58.92189f, 6333.011f, 31.37424f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~r~You are not at the GQ Delivery HQ.");
                return;
            }

            if (player.GetData(EntityData.PLAYER_JOB) != 12)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~INFO: ~w~You are not part of this company.");
            }

            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter: ~w~ You're not on duty, go let Johnny know first.");
            }


            Vehicle veh = null;
            VehicleHash vehHash = VehicleHash.Boxville2;
            string playerName = player.GetData(EntityData.PLAYER_NAME);

            List<VehicleModel> vehicles = Database.LoadAllVehicles();

            foreach (VehicleModel v in vehicles)
            {
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Boxville2))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Position = new Vector3(80.81574, 6365.107, 31.22811);
                        NAPI.Chat.SendChatMessageToPlayer(player, "~y~Rogger: ~w~We managed to find your vehicle, try not to lose it again.");
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Boxville2) == null)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "~y~Rogger: ~w~You need to rent a Vehicle first, there's nothing here for me to find..");
                }
            }
        }

        [Command("startdelivery", GreedyArg = true)]
        public void Command_Job_Deliveryman_Startdelivery(Client player)
        {
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(95.79912f, 6367.596f, 31.37588f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                if (playerJob != 12)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "~y~INFO: ~w~You are not a part of the ~b~GQ Delivery Organisation.");
                    return;
                }
                if (playerJob == 12)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "~y~INFO: ~w~You are not at the GQ HQ.");
                    return;
                }
            }
            if (playerJob == 1)
            {

            }
            if (playerDuty == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~y~Rogger: ~w~You need to let Johnny know you're working today first..");
                return;
            }
        }

        /* Vehicle veh = null;
         VehicleHash vehHash = VehicleHash.Boxville2;
         string playerName = player.GetData(EntityData.PLAYER_NAME);

         List<VehicleModel> vehicles = Database.LoadAllVehicles();

           foreach (VehicleModel v in vehicles)
         {
             if (v.owner == playerName && v.model == (uint)(VehicleHash.boxville2))
             {
                 int id = v.id;
                 veh = Vehicles.GetVehicleById(id);
                 int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                if (veh != null)
                {
                    List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                     foreach (OceanVehicleModel ov in oceanVehicles)
                     {
                         if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                         {
                             if (ov.scrap > 0 || ov.info > 0 || ov.rareInfo > 0)
                             {
                                 NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter: ~w~Seems you still have some things in your previous vehicle. Go sell it first.");
                                 NAPI.Chat.SendChatMessageToPlayer(player, "~y~INFO: ~w~Your stored vehicle currently has: " .");
                                 return;
                            }
                             if (ov.scrap == 0 || ov.info == 0 || ov.rareInfo == 0)
                             {
                                 NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter~w~: You already had a sub, but it seems you had nothing in it. We'll provide another.");
                                 veh.Delete();
                                 Database.RemoveVehicle(vehicleId);
                                 Database.RemoveOceanVehicle(player, ov.scrap, ov.info, ov.rareInfo);
                             }
                         }
                     }
                 }
             }
         }*/


        /*    JobStarted(player);
            }

           public void JobStarted(Client player)
           {
               NAPI.Chat.SendChatMessageToPlayer(player, "~y~INFO:~w~ Your vehicle is ready and fueled.");

               CreateNewVehicle(player);

               //NAPI.Player.SetPlayerClothes(player, 3, 289, 0);          

               NAPI.Chat.SendChatMessageToPlayer(player, "~y~Rogger: ~w~Your packages are ready in the Boxville, stay safe and see you soon!");

               // EquipJobUniform(player);
           }

           /* public void EquipJobUniformWithoutTank(Client player)
             {
                 var clothDictionary = new Dictionary<int, ComponentVariation>();

                 if (player.GetData(EntityData.PLAYER_SEX) == 0)
                 {
                     clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                     clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                     clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 });

                     clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });
                 }
                 if (player.GetData(EntityData.PLAYER_SEX) == 1)
                 {
                     clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                     clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                     clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });

                     clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });
                 }

                 NAPI.Player.SetPlayerClothes(player, clothDictionary); 
        //  }


        [Command("stopdelivery", GreedyArg = true)]
        public void Command_Job_Deliveryman_EndWork(Client player)
        {
            Vector3 triggerPosition = new Vector3(95.79912f, 6367.596f, 31.37588f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~r~You are not at the Delivery job.");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "~y~Rogger: ~w~You should probably let Marcus know first that you're working today..");
                    return;
                }

                Vehicle veh = null;
                VehicleHash vehHash = VehicleHash.Boxville2;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Boxville2))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                        veh.GetData(EntityData.VEHICLE_PRICE);

                        if (veh != null)
                        {
                            veh.Delete();
                            Database.RemoveVehicle(vehicleId);
                            NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter: ~w~Thanks for returning the vehicle buddy.");

                            foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == player.GetData(EntityData.OCEANVEHICLE_OWNER))
                                {
                                    string owner = ov.owner;
                                    if (veh != null)
                                    {
                                    }
                                    else
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter: ~w~Mhm, I don't seem to see that you a vehicle on our record..");
                                    }
                                }
                            }
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, "~y~Peter: ~w~Mhm, I don't seem to see that you a vehicle on our record..");
                        }
                    }
                }
            }
        }
    }
} 

  /*  [Command("collect", GreedyArg = true)]
    public void Command_Job_Deliveryman_Collect(Client player)
    {
        foreach (Vector3 prop in allLocations)
        {
            if (prop.DistanceTo(player.Position) < 5)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "~w~You collected 1x ~y~~h~Scrap");

                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (OceanVehicleModel ov in oceanVehicles)
                {
                    if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                    {
                        int scrap = ov.scrap + 1;
                        int info = ov.info;
                        int rareInfo = ov.rareInfo;

                        ov.id = ov.id;
                        ov.owner = ov.owner;
                        ov.scrap = scrap;
                        ov.info = info;
                        ov.rareInfo = rareInfo;

                        Database.SaveOceanVehicle(ov);
                    }
                }
            }
        }




        //player.SetData(EntityData.OCEANVEHICLE_SCRAP, 7);
        // player.SetData(EntityData.OCEANVEHICLE_INFO, 4);
        // player.SetData(EntityData.OCEANVEHICLE_RAREINFO, 1);
    }

   /* [Command("foundshit", GreedyArg = true)]
    public void Command_Job_OceanCleaner_Debug(Client player)
    {
        player.SetData(EntityData.OCEANVEHICLE_SCRAP, 7);
        player.SetData(EntityData.OCEANVEHICLE_INFO, 4);
        player.SetData(EntityData.OCEANVEHICLE_RAREINFO, 1);
    }

    [Command("checksub", GreedyArg = true)]
    public void Command_Job_OceanCleaner_Inventory(Client player)
    {
        player.SendChatMessage(player.GetData(EntityData.OCEANVEHICLE_SCRAP.ToString() + " Scrap Found"));
        player.SendChatMessage(player.GetData(EntityData.OCEANVEHICLE_INFO.ToString() + " Info Found"));
        player.SendChatMessage(player.GetData(EntityData.OCEANVEHICLE_RAREINFO.ToString() + " Rare Info Found"));

        player.SendChatMessage("Checked");
    }
*/

  /*  public void SpawnVehicle(Client player)
    {
        //Vehicle vehicle = NAPI.Vehicle.CreateVehicle(VehicleHash.Submersible2, new Vector3(317.3724, -2965.611, -0.1664049), 0f, 0, 0);
    }

    public void CreateNewVehicle(Client player)
    {
        VehicleModel vehicle = new VehicleModel();

        vehicle.id = 0;
        vehicle.model = (uint)VehicleHash.Boxville2;
        vehicle.faction = Constants.FACTION_NONE;
        vehicle.position = new Vector3(317.3724, -2965.611, -0.1664049);
        vehicle.rotation = new Quaternion(0, 0, 0, 0);
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

        int scrap = 0;
        int information = 0;
        int rareInformation = 0;

        Vehicles.CreateVehicle(player, vehicle, false);
        Database.AddNewOceanVehicle(vehicle, scrap, information, rareInformation);
    }

    [ServerEvent(Event.PlayerEnterVehicle)]
    public void OnPlayerEnterVehicle(Client player, Vehicle vehicleLocal, sbyte seat)
    {
        if (player.GetData(EntityData.PLAYER_JOB) != 12)
        {
            player.SendChatMessage("~y~INFO: ~w~You do not have the keys for this.");
            player.Vehicle.EngineStatus = false;
            return;
        }

        if (vehicleLocal.DisplayName == "SUBMERS2")
        {
            if (player.GetData(EntityData.PLAYER_JOB) == 12 && player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
            {
                player.SendNotification("You put on your vest and oxygen tank.");

                var clothDictionary = new Dictionary<int, ComponentVariation>();
            }
        }
    }*/




}