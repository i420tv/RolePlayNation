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
    public class Trucker : Script
    {
        private string NPCPeter = Constants.COLOR_YELLOW + "Peter: ";
        private string NPCMarcus = Constants.COLOR_YELLOW + "Marcus: ";
        private string NPCRay = Constants.COLOR_YELLOW + "Ray: ";
        public Trucker()
        {
            CreateJobLocation();
            CreateNPC();
        }
        public static void CheckTruckerOrders(Client player)
        {
            // Get the deliverable orders
            List<OrderModel> truckerOrders = Globals.truckerOrderList.Where(o => !o.taken).ToList();

            if (truckerOrders.Count == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_none);
                return;
            }

            List<float> distancesList = new List<float>();

            foreach (OrderModel order in truckerOrders)
            {
                float distance = player.Position.DistanceTo(order.position);
                distancesList.Add(distance);
            }

            player.TriggerEvent("showTruckerOrders", NAPI.Util.ToJson(truckerOrders), NAPI.Util.ToJson(distancesList));
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(-44.04705f, -2519.901f, 7.394626f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/gettruck~w~ to get your truck and ~y~/returntruck~w~ to return your sub.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~r~Recover Price: ~w~$50", new Vector3(-44.04705f, -2519.901f, 7.094626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        public void CreateNPC()
        {
            Vector3 npcPos = new Vector3(-44.04705f, -2519.901f, 7.394626f);
            Vector3 npcReturnPos = new Vector3(-126.7936f, -2530.553f, 6.59571f);

            NAPI.TextLabel.CreateTextLabel("~r~Trucking Union", npcPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Keith Dumper", new Vector3(-44.04705f, -2519.901f, 7.194626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~b~/duty~w~ to go on duty.", new Vector3(-44.04705f, -2519.901f, 6.594626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~b~/duty~w~ to go on duty.", new Vector3(-44.04705f, -2519.901f, 6.394626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/gettruck~w~ to get rent a truck", new Vector3(-44.04705f, -2519.901f, 6.194626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~r~/quitjob~w~ to leave the company.", new Vector3(-44.04705f, -2519.901f, 5.994626f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name


            NAPI.TextLabel.CreateTextLabel("~r~Logistics Operator", npcReturnPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Tom Harold", new Vector3(-126.7936f, -2530.553f, 6.49571f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/gettrailer  ~w~ to get your trailer.", new Vector3(-126.7936f, -2530.553f, 5.89571f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/returntrailer ~w~to park the trailer.", new Vector3(-126.7936f, -2530.553f, 5.79571f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        public void JobStarted(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "Your vehicle is ready and fueled.");

            CreateNewVehicle(player);

            //NAPI.Player.SetPlayerClothes(player, 3, 289, 0);          

            NAPI.Chat.SendChatMessageToPlayer(player, NPCPeter + Constants.COLOR_WHITE + "Your Order list and GPS is in the Phantom, drive safe and see you soon!");

            //EquipJobUniformWithoutTank(player);
        }

        public void TrailerSpawn(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "Your trailer is filled up and ready.");

            CreateNewTrailer(player);

            //NAPI.Player.SetPlayerClothes(player, 3, 289, 0);          

            //EquipJobUniformWithoutTank(player);
        }
        [Command("gettruck", GreedyArg = true)]
        public void Command_Job_Trucker_RentTruck(Client player)
        {
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(-44.04705f, -2519.901f, 7.394626f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {

            }
            else
            {

                if (playerDuty == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"You need to sign in for the day");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName)
                    {
                        if (v.model == (uint)VehicleHash.Phantom)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a truck out for you.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.Phantom2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a vehicle in the system registered to you.");
                            return;
                        }
                        if (v.model == (uint)(VehicleHash.Phantom3))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a vehicle in the system registered to you.");
                            return;
                        }

                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (veh != null)
                        {
                            List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                          /*  foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                                {
                                    if (ov.scrap > 0)
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Seems there are still things in your sub, go sell that first.");
                                        return;
                                    }
                                }
                            }*/
                        }
                    }

                }
                JobStarted(player);
            }
        }
        [Command("returntruck", GreedyArg = true)]
        public void Command_Job_OceanCleaner_EndWork(Client player)
        {
            Vector3 triggerPosition = new Vector3(-44.04705f, -2519.901f, 7.394626f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at the Trucking Union");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_JOB) != 8)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You don't work for us");
                    return;

                }
                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You should probably sign in for the day");
                    return;
                }
                string playerName = player.GetData(EntityData.PLAYER_NAME);
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();
                List<VehicleModel> vehicles = Database.LoadAllVehicles();
                Vehicle veh = null;
                foreach (VehicleModel v in vehicles)
                {

                    if (veh.GetData(EntityData.VEHICLE_ID) == 0)
                    {
                        return;
                    }
                }
                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Phantom))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                        veh.GetData(EntityData.VEHICLE_PRICE);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You need to bring your vehicle here");
                            return;
                        }

                        if (veh != null)
                        {
                            foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                                {
                                    string owner = ov.owner;

                                    if (ov != null)
                                    {

                                        if (ov.scrap == 0)
                                        {
                                            veh.Delete();
                                            Database.RemoveVehicle(vehicleId);
                                            Database.RemoveOceanVehicle(player);
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Thanks for returning the vehicle.");
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Mhm, it seems we were unable to find a vehicle by your name on the system.");
                                
                                }
                            }
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Mhm, it seems we were unable to find a vehicle by your name on the system.");
                        }
                    }
                }
            }
        }
        [Command("gettrailer")]
        public void RemoteEvent_Job_Trucker_Trailer(Client player)
        {
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(-126.0471f, -2530.999f, 6.095711f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {

            }
            else
            {

                   if (playerDuty == 0)
                   {
                       NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW +"You need to sign in for the day");
                       return;
                   }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName)
                    {
                        if (v.model == (uint)VehicleHash.Tanker)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a trailer out for you.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.TrailerLogs))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a trailer in the system registered to you.");
                            return;
                        }
                        if (v.model == (uint)(VehicleHash.Trailers2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a trailer in the system registered to you.");
                            return;
                        }

                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (veh != null)
                        {
                            List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                            /*  foreach (OceanVehicleModel ov in oceanVehicles)
                              {
                                  if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                                  {
                                      if (ov.scrap > 0)
                                      {
                                          NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Seems there are still things in your sub, go sell that first.");
                                          return;
                                      }
                                  }
                              }*/
                        }
                    }

                }
                TrailerSpawn(player);
            }
        }
        [Command(Commands.COM_DELIVER)]
        public void DeliverCommand(Client player)
        {
            if(player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_TRUCKER)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_trucker);
                return;
            }
            // Create the delivery crates
            player.TriggerEvent("TruckerJob1");
        }
        public void CreateNewTrailer(Client player)
        {
            VehicleModel vehicle = new VehicleModel();

            vehicle.id = 0;
            vehicle.model = (uint)VehicleHash.Trailers2;
            vehicle.faction = Constants.FACTION_NONE;
            vehicle.position = new Vector3(-103.8325f, -2526.336f, 6f);
            vehicle.rotation = new Quaternion(0, 90, 0, 0);
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
            int scrapValue = 0;

            Vehicles.CreateVehicle(player, vehicle, false);
            Database.AddNewOceanVehicle(vehicle, scrap, vehicle.model, scrapValue);
        }
        public void CreateNewVehicle(Client player)
        {
            VehicleModel vehicle = new VehicleModel();

            vehicle.id = 0;
            vehicle.model = (uint)VehicleHash.Phantom;
            vehicle.faction = 108;
            vehicle.position = new Vector3(-40.60789f, -2524.409f, 6.010003f);
            vehicle.rotation = new Quaternion(45, 0, 0, 0);
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
            int scrapValue = 0;

            Vehicles.CreateVehicle(player, vehicle, false);
            Database.AddNewOceanVehicle(vehicle, scrap, vehicle.model, scrapValue);
        }
        public static void CheckTruckingOrders(Client player)
        {
            // Get the deliverable orders
            List<TruckingOrderModel> truckingOrders = Globals.truckingOrderList.Where(o => !o.taken).ToList();

            if (truckingOrders.Count == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_none);
                return;
            }

            List<float> distancesList = new List<float>();

            foreach (TruckingOrderModel order in truckingOrders)
            {
                float distance = player.Position.DistanceTo(order.position);
                distancesList.Add(distance);
            }

            player.TriggerEvent("showTruckingOrders", NAPI.Util.ToJson(truckingOrders), NAPI.Util.ToJson(distancesList));
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
