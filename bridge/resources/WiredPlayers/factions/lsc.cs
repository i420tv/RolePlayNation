using GTANetworkAPI;
using WiredPlayers.jobs;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.model;
using WiredPlayers.drivingschool;
using WiredPlayers.weapons;
using WiredPlayers.business;
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
    public class Lsc : Script
    {
            public static List<TunningModel> tunningList;

            public static void AddTunningToVehicle(Vehicle vehicle)
            {
                foreach (TunningModel tunning in tunningList)
                {
                    if (vehicle.GetData(EntityData.VEHICLE_ID) == tunning.vehicle)
                    {
                        vehicle.SetMod(tunning.slot, tunning.component);
                    }
                }
            }

            private int GetVehicleTunningComponent(int vehicleId, int slot)
            {
                // Get the component on the specified slot
                TunningModel tunning = tunningList.Where(tunningModel => tunningModel.vehicle == vehicleId && tunningModel.slot == slot).FirstOrDefault();

                return tunning == null ? 255 : tunning.component;
            }

            [RemoteEvent("repaintVehicle")]
            public void RepaintVehicleEvent(Client player, int colorType, string firstColor, string secondColor, int pearlescentColor, int vehiclePaid)
            {
                // Get player's vehicle
                Vehicle vehicle = player.GetData(EntityData.PLAYER_VEHICLE);

                switch (colorType)
                {
                    case 0:
                        // Predefined color
                        vehicle.PrimaryColor = int.Parse(firstColor);
                        vehicle.SecondaryColor = int.Parse(secondColor);

                        if (pearlescentColor >= 0)
                        {
                            vehicle.PearlescentColor = pearlescentColor;
                        }
                        break;
                    case 1:
                        // Custom color
                        string[] firstColorArray = firstColor.Split(',');
                        string[] secondColorArray = secondColor.Split(',');
                        vehicle.CustomPrimaryColor = new Color(int.Parse(firstColorArray[0]), int.Parse(firstColorArray[1]), int.Parse(firstColorArray[2]));
                        vehicle.CustomSecondaryColor = new Color(int.Parse(secondColorArray[0]), int.Parse(secondColorArray[1]), int.Parse(secondColorArray[2]));
                        break;
                }

                if (vehiclePaid > 0)
                {
                    // Check for the product amount
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel item = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                    if (item != null && item.amount >= 250)
                    {
                        // Get all the players who have keys for the vehicle
                        List<Client> vehicleOwners = NAPI.Pools.GetAllPlayers().Where(p => Vehicles.HasPlayerVehicleKeys(p, vehicle, false)).ToList();

                        // Search for a player with vehicle keys
                        foreach (Client target in vehicleOwners)
                        {
                            if (target.Position.DistanceTo(player.Position) < 4.0f)
                            {
                                // Vehicle repaint data
                                target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                target.SetData(EntityData.PLAYER_REPAINT_VEHICLE, vehicle);
                                target.SetData(EntityData.PLAYER_REPAINT_COLOR_TYPE, colorType);
                                target.SetData(EntityData.PLAYER_REPAINT_FIRST_COLOR, firstColor);
                                target.SetData(EntityData.PLAYER_REPAINT_SECOND_COLOR, secondColor);
                                target.SetData(EntityData.PLAYER_REPAINT_PEARLESCENT, pearlescentColor);
                                target.SetData(EntityData.JOB_OFFER_PRICE, vehiclePaid);
                                target.SetData(EntityData.JOB_OFFER_PRODUCTS, 250);

                                string playerMessage = string.Format(InfoRes.mechanic_repaint_offer, target.Name, vehiclePaid);
                                string targetMessage = string.Format(InfoRes.mechanic_repaint_accept, player.Name, vehiclePaid);
                                player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                return;
                            }
                        }

                        // There's no player with vehicle's keys near
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    }
                    else
                    {
                        string message = string.Format(ErrRes.not_required_products, 250);
                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                    }
                }
            }

            [RemoteEvent("cancelVehicleRepaint")]
            public void CancelVehicleRepaintEvent(Client player)
            {
                // Get player's vehicle
                Vehicle vehicle = player.GetData(EntityData.PLAYER_VEHICLE);

                // Get previous colors
                int vehicleColorType = vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                string primaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                string secondaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                int vehiclePearlescentColor = vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);

                if (vehicleColorType == Constants.VEHICLE_COLOR_TYPE_PREDEFINED)
                {
                    vehicle.PrimaryColor = int.Parse(primaryVehicleColor);
                    vehicle.SecondaryColor = int.Parse(secondaryVehicleColor);
                    vehicle.PearlescentColor = vehiclePearlescentColor;
                }
                else
                {
                    string[] primaryColor = primaryVehicleColor.Split(',');
                    string[] secondaryColor = secondaryVehicleColor.Split(',');
                    vehicle.CustomPrimaryColor = new Color(int.Parse(primaryColor[0]), int.Parse(primaryColor[1]), int.Parse(primaryColor[2]));
                    vehicle.CustomSecondaryColor = new Color(int.Parse(secondaryColor[0]), int.Parse(secondaryColor[1]), int.Parse(secondaryColor[2]));
                }
            }

            [RemoteEvent("modifyVehicle")]
            public void ModifyVehicleEvent(Client player, int slot, int component)
            {
                Vehicle vehicle = player.Vehicle;

                if (component > 0)
                {
                    player.Vehicle.SetMod(slot, component);
                }
                else
                {
                    player.Vehicle.RemoveMod(slot);
                }
            }

            [RemoteEvent("cancelVehicleModification")]
            public void CancelVehicleModificationEvent(Client player)
            {
                int vehicleId = player.Vehicle.GetData(EntityData.VEHICLE_ID);

                for (int i = 0; i < 49; i++)
                {
                    // Get the component in the slot
                    int component = GetVehicleTunningComponent(vehicleId, i);

                    // Remove or add the tunning part
                    player.Vehicle.SetMod(i, component);
                }
            }

            [RemoteEvent("confirmVehicleModification")]
            public void ConfirmVehicleModificationEvent(Client player, int slot, int mod)
            {
                // Get the vehicle's id
                int vehicleId = player.Vehicle.GetData(EntityData.VEHICLE_ID);

                // Get player's product amount
                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                ItemModel item = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                // Calculate the cost for the tunning
                int totalProducts = Constants.TUNNING_PRICE_LIST.Where(x => x.slot == slot).First().products;

                if (item != null && item.amount >= totalProducts)
                {
                    // Add component to database
                    TunningModel tunningModel = new TunningModel();
                    {
                        tunningModel.slot = slot;
                        tunningModel.component = mod;
                        tunningModel.vehicle = vehicleId;
                    }

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            tunningModel.id = Database.AddTunning(tunningModel);
                            tunningList.Add(tunningModel);

                            // Remove consumed products
                            item.amount -= totalProducts;

                            // Update the amount into the database
                            Database.UpdateItem(item);

                            // Confirmation message
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.vehicle_tunning);
                        });
                    });
                }
                else
                {
                    string message = string.Format(ErrRes.not_required_products, totalProducts);
                    player.SendChatMessage(Constants.COLOR_ERROR + message);
                }
            }
            public Lsc()
        {
            // Create all the equipment places
            foreach(Vector3 pos in Constants.LSC_POSITION)
            {
                // Create blips
                Blip Lsc = NAPI.Blip.CreateBlip(pos);
                Lsc.Name = "Los Santos Customs";
                Lsc.ShortRange = true;
                Lsc.Sprite = 446;
            }
            CreateJobLocation();
        }
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(-354.4169f, -128.2691f, 39.43061f);
            Vector3 jobPosDesc = jobPos;
            Vector3 Customercatalog = new Vector3(-351.5882f, -116.8137f, 38.8228f);
            Vector3 Mechanicshop = new Vector3(-322.7954f, -132.5701f, 38.95754f);
            Vector3 Paintshop = new Vector3(-327.0562f, -144.593f, 39.05999f);
            Vector3 Vehicles = new Vector3(-355.1477f, -76.85512f, 45.66396f);
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/duty~w~ to go on and off duty", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/Lveh~w~ to get your vehicle and ~y~/Lreturn~w~ to return your vehicle.", Vehicles, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/Mcatalog~w~ to open the customer catalog.", Customercatalog, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/Tuning~w~ to open the tuning catalog for mechanics.", Mechanicshop, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0);
            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/Paint~w~ to open the paint catalog for mechanics.", Paintshop, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0);
            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }
        #region Vehicle Creation and Returning
        [Command("lreturn", GreedyArg = true)]
        public void Command_Freturn(Client player)
        {
            Vector3 triggerPosition = new Vector3(-355.1477f, -76.85512f, 45.66396f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at the Los Santos Customs Vehicle Depo");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) != 8)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are not a Mechanic.");
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.TowTruck))
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Granger))
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
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Flatbed))
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

        [Command("lveh", GreedyArg = true)]
        public void Command_FactionV(Client player, string args)
        {
            int playerJob = player.GetData(EntityData.PLAYER_FACTION);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(-355.1477f, -76.85512f, 45.66396f);

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
                        if (v.model == (uint)VehicleHash.Flatbed)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.Granger))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You already have a vehicle out. We cant sign you more then one at a time.");
                            return;
                        }
                        if (v.model == (uint)VehicleHash.TowTruck)
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
                        case Commands.ARG_FLATBED:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (Faction.IsLSCMember(player))
                                {
                                    // Basic data for vehicle creation
                                    vehicle.model = (uint)VehicleHash.Flatbed;
                                    vehicle.faction = Constants.FACTION_LSC;
                                    vehicle.position = new Vector3(-358.2682f, -84.40323f, 45.60103f);
                                    vehicle.rotation = player.Rotation;
                                    vehicle.dimension = player.Dimension;
                                    vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                    vehicle.firstColor = "3,44,138";
                                    vehicle.secondColor = "3,44,138";
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
                                    player.SendChatMessage(Constants.COLOR_HELP + "You are not a Mechanic");
                                }
                            }
                            break;
                        case Commands.ARG_GRANGER:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (Faction.IsLSCMember(player))
                                {
                                    // Basic data for vehicle creation
                                    vehicle.model = (uint)VehicleHash.Granger;
                                    vehicle.faction = Constants.FACTION_LSC;
                                    vehicle.position = new Vector3(-358.2682f, -84.40323f, 45.60103f);
                                    vehicle.rotation = player.Rotation;
                                    vehicle.dimension = player.Dimension;
                                    vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                    vehicle.firstColor = "3,44,138";
                                    vehicle.secondColor = "3,44,138";
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
                                    player.SendChatMessage(Constants.COLOR_HELP + "You are not a Mechanic");
                                }
                            }
                            break;
                        case Commands.ARG_TOW:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
                            {
                                if (Faction.IsLSCMember(player))
                                {
                                    // Basic data for vehicle creation
                                    vehicle.model = (uint)VehicleHash.TowTruck;
                                    vehicle.faction = Constants.FACTION_LSC;
                                    vehicle.position = new Vector3(-358.2682f, -84.40323f, 45.60103f);
                                    vehicle.rotation = player.Rotation;
                                    vehicle.dimension = player.Dimension;
                                    vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                    vehicle.firstColor = "3,44,138";
                                    vehicle.secondColor = "3,44,138";
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
                                    player.SendChatMessage(Constants.COLOR_HELP + "You are not a Mechanic");
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

        }
        [Command(Commands.COM_REPAIR, Commands.HLP_MECHANIC_REPAIR_COMMAND)]
        public void RepairCommand(Client player, int vehicleId, string type, int price = 0)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_LSC)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_mechanic);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (PlayerInValidRepairPlace(player) == false)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_valid_repair_place);
            }
            else
            {
                Vehicle vehicle = Vehicles.GetVehicleById(vehicleId);
                if (vehicle == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                }
                else if (vehicle.Position.DistanceTo(player.Position) > 5.0f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.wanted_vehicle_far);
                }
                else
                {
                    int spentProducts = 0;

                    switch (type.ToLower())
                    {
                        case Commands.ARG_CHASSIS:
                            spentProducts = Constants.PRICE_VEHICLE_CHASSIS;
                            break;
                        case Commands.ARG_DOORS:
                            for (int i = 0; i < 6; i++)
                            {
                                if (vehicle.IsDoorBroken(i) == true)
                                {
                                    spentProducts += Constants.PRICE_VEHICLE_DOORS;
                                }
                            }
                            break;
                        case Commands.ARG_TYRES:
                            for (int i = 0; i < 4; i++)
                            {
                                if (vehicle.IsTyrePopped(i) == true)
                                {
                                    spentProducts += Constants.PRICE_VEHICLE_TYRES;
                                }
                            }
                            break;
                        case Commands.ARG_WINDOWS:
                            for (int i = 0; i < 4; i++)
                            {
                                if (vehicle.IsWindowBroken(i) == true)
                                {
                                    spentProducts += Constants.PRICE_VEHICLE_WINDOWS;
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_MECHANIC_REPAIR_COMMAND);
                            return;
                    }

                    if (price > 0)
                    {
                        // Get player's products
                        int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel item = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                        if (item != null && item.amount >= spentProducts)
                        {
                            // Get the players who have the keys for the vehicle
                            List<Client> vehicleOwners = NAPI.Pools.GetAllPlayers().Where(p => Vehicles.HasPlayerVehicleKeys(p, vehicle, false)).ToList();

                            foreach (Client target in vehicleOwners)
                            {
                                if (target.Position.DistanceTo(player.Position) < 4.0f)
                                {
                                    // Fill repair entity data
                                    target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                    target.SetData(EntityData.PLAYER_REPAIR_VEHICLE, vehicle);
                                    target.SetData(EntityData.PLAYER_REPAIR_TYPE, type);
                                    target.SetData(EntityData.JOB_OFFER_PRODUCTS, spentProducts);
                                    target.SetData(EntityData.JOB_OFFER_PRICE, price);

                                    string playerMessage = string.Format(InfoRes.mechanic_repair_offer, target.Name, price);
                                    string targetMessage = string.Format(InfoRes.mechanic_repair_accept, player.Name, price);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                                    return;
                                }
                            }

                            // There's no player with the vehicle's keys near
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                        }
                        else
                        {
                            string message = string.Format(ErrRes.not_required_products, spentProducts);
                            player.SendChatMessage(Constants.COLOR_ERROR + message);
                        }
                    }
                    else
                    {
                        string message = string.Format(InfoRes.repair_price, spentProducts);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                }
            }
        }
        public static bool PlayerInValidRepairPlace(Client player)
        {
            // Check if the player is in any workshop
            foreach (BusinessModel business in Business.businessList)
            {
                if (business.type == Constants.BUSINESS_TYPE_MECHANIC && player.Position.DistanceTo(business.position) < 25.0f)
                {
                    return true;
                }
            }

            // Check if the player has a towtruck near
            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                VehicleHash vehicleHash = (VehicleHash)vehicle.Model;
                if (vehicleHash == VehicleHash.TowTruck || vehicleHash == VehicleHash.Flatbed || vehicleHash == VehicleHash.Granger)
                {
                    return true;
                }
            }

            return false;
        }
        [Command(Commands.COM_REPAINT, Commands.HLP_MECHANIC_REPAINT_COMMAND)]
        public void RepaintCommand(Client player, int vehicleId)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_LSC)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_mechanic);
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
                foreach (BusinessModel business in Business.businessList)
                {
                    if (business.type == Constants.BUSINESS_TYPE_MECHANIC && player.Position.DistanceTo(business.position) < 25.0f)
                    {
                        Vehicle vehicle = Vehicles.GetVehicleById(vehicleId);
                        if (vehicle != null)
                        {
                            player.SetData(EntityData.PLAYER_VEHICLE, vehicle);
                            player.TriggerEvent("showRepaintMenu");
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                        }
                        return;
                    }
                }

                // Player is not in any workshop
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_mechanic_workshop);
            }
        }

        [Command(Commands.COM_TUNING)]
        public void TunningCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_LSC)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_mechanic);
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
                foreach (BusinessModel business in Business.businessList)
                {
                    if (business.type == Constants.BUSINESS_TYPE_MECHANIC && player.Position.DistanceTo(business.position) < 25.0f)
                    {
                        if (player.IsInVehicle)
                        {
                            player.SetData(EntityData.PLAYER_VEHICLE, player.Vehicle);
                            player.TriggerEvent("showTunningMenu");
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                        }
                        return;
                    }
                }

                // Player is not in any workshop
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_mechanic_workshop);
            }
        }
        [Command(Commands.COM_MCATALOG)]
        public void McatalogCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                foreach (BusinessModel business in Business.businessList)
                {
                    if (business.type == Constants.BUSINESS_TYPE_MECHANIC && player.Position.DistanceTo(business.position) < 25.0f)
                    {
                        if (player.IsInVehicle)
                        {
                            player.SetData(EntityData.PLAYER_VEHICLE, player.Vehicle);
                            player.TriggerEvent("showCatalogMenu");
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                        }
                        return;
                    }
                }

                // Player is not in any workshop
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_mechanic_workshop);
            }

        }

       [Command(Commands.COM_IMPOUND, Commands.HLP_LSC_IMPOUND_COMMAND)]
        public void InpoundCommand(Client player, string item)
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

            if(!Faction.IsLSCMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
                return;
            }
        }
        #region Vehicle Creation and return
        [Command("lreturn", GreedyArg = true)]
        public void CommandLSCReturn(Client player)
        {
            Vector3 triggerPosition = new Vector3(1840.681f, 2541.803f, 45.83076f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at the Los Santos Customs Vehicle Depo.");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_FACTION) != 8)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are not a Mechanic.");
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

        [Command("lveh", GreedyArg = true)]
        public void CommandLSCV(Client player, string args)
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
                            break;
                        case Commands.ARG_POLICE4:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
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
                            break;
                        case Commands.ARG_POLICET:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
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
                            break;
                        case Commands.ARG_INSURGENT2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        case Commands.ARG_KAMACHO:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        //
                        case Commands.ARG_PRISONB:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        case Commands.ARG_FBI:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        case Commands.ARG_FBI2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        case Commands.ARG_POLICE:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
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
                            break;
                        case Commands.ARG_RIOT2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 3)
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
                            break;
                        case Commands.ARG_POLICE2:
                            if (player.GetData(EntityData.PLAYER_RANK) > 0)
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
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
