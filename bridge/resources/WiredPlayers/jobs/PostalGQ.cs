using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.database;
using WiredPlayers.vehicles;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class PostalGQ : Script
    {
        private string NPCPeter = Constants.COLOR_YELLOW + "Peter: ";
        private string NPCMarcus = Constants.COLOR_YELLOW + "Marcus: ";
        private string NPCRay = Constants.COLOR_YELLOW + "Ray: ";
        public PostalGQ()
        {
            CreateJobLocation();
            CreateNPC();
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(499.7191f, -651.9603f, 24.90868f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/gettruck~w~ to get your tricl and ~y~/returnpostal~w~ to return your truck.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
         //   NAPI.TextLabel.CreateTextLabel("~r~Recover Price: ~w~$50", new Vector3(312.4634f, -2961.938f, 5.752742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }
        public void CreateNPC()
        {
            Vector3 npcPos = new Vector3(499.7191f, -651.9603f, 24.90868f);

            NAPI.TextLabel.CreateTextLabel("~r~Operations Manager", new Vector3(499.7191f, -651.9603f, 25.30868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Jimmy", new Vector3(499.7191f, -651.9603f, 25.20868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~b~/duty~w~ to go on duty.", new Vector3(499.7191f, -651.9603f, 24.90868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/info~w~ for more information on this job", new Vector3(499.7191f, -651.9603f, 24.80868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~g~/join~w~ to become a part of the ~o~PostalGQ~w~ of Los Santos.", new Vector3(499.7191f, -651.9603f, 24.70868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~r~/quitjob~w~ to leave the company.", new Vector3(499.7191f, -651.9603f, 24.60868f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
        }
        public void JobStarted(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "Your vehicle is ready and fueled.");

            CreateNewVehicle(player);

            //NAPI.Player.SetPlayerClothes(player, 3, 289, 0);          

            NAPI.Chat.SendChatMessageToPlayer(player, NPCPeter + Constants.COLOR_WHITE + "GPS and order list is in the truck, stay safe and see you soon!");

            //EquipJobUniformWithoutTank(player);
        }

        public void EquipJobUniformWithoutTank(Client player)
        {
            var clothDictionary = new Dictionary<int, ComponentVariation>();

            if (player.GetData(EntityData.PLAYER_SEX) == 0)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 }); //Mask
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 }); // Legs
                clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 }); // Shoes
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask

                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });

            }
            if (player.GetData(EntityData.PLAYER_SEX) == 1)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 });
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask
                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });

            }

            NAPI.Player.SetPlayerClothes(player, clothDictionary);
        }
        [Command("gettruck", GreedyArg = true)]
        public void Command_Job_Trucker_RentTruck(Client player)
        {
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(499.7191f, -651.9603f, 24.90868f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {

            }
            else
            {

                if (playerDuty == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You need to sign in for the day");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName)
                    {
                        if (v.model == (uint)VehicleHash.Boxville2)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "We already have a truck out for you.");
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
        [Command("returnPostal", GreedyArg = true)]
        public void Command_Job_OceanCleaner_EndWork(Client player)
        {
            Vector3 triggerPosition = new Vector3(499.7191f, -651.9603f, 24.90868f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at PostalGQ");
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Boxville2))
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
        public void CreateNewVehicle(Client player)
        {
            VehicleModel vehicle = new VehicleModel();

            vehicle.id = 0;
            vehicle.model = (uint)VehicleHash.Boxville2;
            vehicle.faction = 112;
            vehicle.position = new Vector3(505.2486f, -636.1529f, 24.75073f);
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
        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            // Check if the player is fastfood deliverer
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_POSTALGQ) return;
            
        }

        public static void CheckFastfoodOrders(Client player)
        {
            // Get the deliverable orders
            List<FastfoodOrderModel> fastFoodOrders = Globals.fastFoodOrderList.Where(o => !o.taken).ToList();

            if (fastFoodOrders.Count == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.order_none);
                return;
            }

            List<float> distancesList = new List<float>();

            foreach (FastfoodOrderModel order in fastFoodOrders)
            {
                float distance = player.Position.DistanceTo(order.position);
                distancesList.Add(distance);
            }

            player.TriggerEvent("showPostalOrders", NAPI.Util.ToJson(fastFoodOrders), NAPI.Util.ToJson(distancesList));
        }

        private int GetFastFoodOrderAmount(Client player)
        {
            int amount = 0;
            int orderId = player.GetData(EntityData.PLAYER_DELIVER_ORDER);
            foreach (FastfoodOrderModel order in Globals.fastFoodOrderList)
            {
                if (order.id == orderId)
                {
                    amount += order.pizzas * Constants.PRICE_PACKAGES;
                    break;
                }
            }
            return amount;
        }

        private FastfoodOrderModel GetFastfoodOrderFromId(int orderId)
        {
            // Get the fastfood order from the specified identifier
            return Globals.fastFoodOrderList.Where(orderModel => orderModel.id == orderId).FirstOrDefault();
        }

        private static void RespawnFastfoodVehicle(Vehicle vehicle)
        {
            //vehicle.Delete();
            vehicle.Position = vehicle.GetData(EntityData.VEHICLE_POSITION);
            vehicle.Rotation = vehicle.GetData(EntityData.VEHICLE_ROTATION);
        }

        private void OnFastFoodTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);

                // Vehicle respawn
                RespawnFastfoodVehicle(vehicle);

                // Cancel the order
                player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                player.ResetData(EntityData.PLAYER_JOB_WON);

                // Delete map blip
                player.TriggerEvent("postalDeliverFinished");

                // Send the message to the player
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.job_vehicle_abandoned);
            });
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_FACTION) == Constants.JOB_POSTALGQ + Constants.MAX_FACTION_VEHICLES)
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
                        FastfoodOrderModel order = GetFastfoodOrderFromId(orderId);

                        player.SetData(EntityData.PLAYER_JOB_VEHICLE, vehicle);

                        player.TriggerEvent("postalDestinationCheckPoint", order.position);
                    }
                }
            }
        }

        [RemoteEvent("takePostalOrder")]
        public void TakeFastFoodOrderEvent(Client player, int orderId)
        {
            foreach (FastfoodOrderModel order in Globals.fastFoodOrderList)
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

        [RemoteEvent("postalCheckpointReached")]
        public void FastfoodCheckpointReachedEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_DELIVER_START) != null)
            {
                if (!player.IsInVehicle)
                {
                    Vehicle vehicle = player.GetData(EntityData.PLAYER_JOB_VEHICLE);
                    Vector3 vehiclePosition = vehicle.GetData(EntityData.VEHICLE_POSITION);

                    int elapsed = Globals.GetTotalSeconds() - player.GetData(EntityData.PLAYER_DELIVER_START);
                    int extra = (int)Math.Round((player.GetData(EntityData.PLAYER_DELIVER_TIME) - elapsed) / 2.0f);
                    int amount = GetFastFoodOrderAmount(player) + extra;

                    player.ResetData(EntityData.PLAYER_DELIVER_START);
                    player.SetData(EntityData.PLAYER_JOB_WON, amount > 0 ? amount : 25);

                    player.TriggerEvent("postalDeliverBack", vehiclePosition);

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
                    Globals.fastFoodOrderList.RemoveAll(order => order.id == orderId);

                 //   player.WarpOutOfVehicle();

                    player.SetSharedData(EntityData.PLAYER_MONEY, money + won);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                    player.SendNotification("You gained 5 xp in PostalGQ");
                    player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                    player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                    player.ResetData(EntityData.PLAYER_JOB_WON);

                    player.TriggerEvent("postalDeliverFinished");

                    // We get the motorcycle to its spawn point
                    // RespawnFastfoodVehicle(vehicle);

                    List<SkillsModel> PlayerSkillList = Database.LoadSkills();

                    foreach (SkillsModel skills in PlayerSkillList)
                    {
                        if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                        {
                            skills.postalexp = skills.postalexp + 5;

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
