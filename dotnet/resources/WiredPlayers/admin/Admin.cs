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

namespace WiredPlayers.admin
{
    public class Admin : Script
    {
        public static List<PermissionModel> permissionList;

        private bool HasUserCommandPermission(Client player, string command, string option = "")
        {
            bool hasPermission = false;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //hehsdaw
            foreach (PermissionModel permission in permissionList)
            {
                if (permission.playerId == playerId && command == permission.command)
                {
                    // We check whether it's a command option or just the command
                    if (option == string.Empty || option == permission.option)
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }

            return hasPermission;
        }

        private void SendHouseInfo(Client player, HouseModel house)
        {
            if (house == null)
                return;

            string title = Constants.COLOR_YELLOW + "Property" + Constants.COLOR_WHITE + " (ID: " + house.id + ")";
            player.SendChatMessage(title);
            player.SendChatMessage(Constants.COLOR_YELLOW + GenRes.name + Constants.COLOR_WHITE + house.name);
            //player.SendChatMessage(GenRes.ipl + house.ipl);
            player.SendChatMessage(Constants.COLOR_YELLOW + GenRes.owner + Constants.COLOR_WHITE + house.owner);
            player.SendChatMessage(Constants.COLOR_YELLOW + GenRes.price + Constants.COLOR_DOLLARGREEN + "$" + house.price);

            string houseStatus = "";

            if (house.status == 0)
                houseStatus = Constants.COLOR_ERROR + "Not for sell or rent.";
            else if (house.status == 1)
                houseStatus = Constants.COLOR_DOLLARGREEN + "Can be rented.";
            if (house.status == 2)
                houseStatus = Constants.COLOR_DOLLARGREEN + "Can be bought.";

            player.SendChatMessage(Constants.COLOR_YELLOW + "Residence Status: " + houseStatus);
        }

        [Command(Commands.COM_SKIN, Commands.HLP_SKIN_COMMAND)]
        public void SkinCommand(Client player, string pedModel)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                PedHash pedHash = NAPI.Util.PedNameToModel(pedModel);
                player.SetSkin(pedHash);
            }
        }

        [Command(Commands.COM_ADMIN, Commands.HLP_ADMIN_COMMAND, GreedyArg = true)]
        public void AdminCommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We send the message to all the players in the server
                NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + GenRes.admin_notice + message);
            }
        }

        [Command("ee", GreedyArg = true)]
        public void EmergencyEvent(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) == 4)
            {
                string errorDefault = Constants.COLOR_ERROR + "Official Government Emergency: " + Constants.COLOR_WHITE;
                NAPI.Chat.SendChatMessageToAll(errorDefault + message);
            }
        }

        [Command(Commands.COM_COORD, Commands.HLP_COORD_COMMAND)]
        public void CoordCommand(Client player, float posX, float posY, float posZ)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                player.Dimension = 0;
                player.Position = new Vector3(posX, posY, posZ);
                player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
            }
        }

        [Command(Commands.COM_TP, Commands.HLP_TP_COMMAND, GreedyArg = true)]
        public void TpCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    string message = string.Format(AdminRes.goto_player, target.Name);

                    // We get interior variables from the target player
                    int targetHouse = target.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    int targetBusiness = target.GetData(EntityData.PLAYER_BUSINESS_ENTERED);

                    // Change player's position and dimension
                    player.Position = target.Position;
                    player.Dimension = target.Dimension;
                    player.SetData(EntityData.PLAYER_HOUSE_ENTERED, targetHouse);
                    player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, targetBusiness);

                    // Confirmation message sent to the command executor
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_BRING, Commands.HLP_BRING_COMMAND, GreedyArg = true)]
        public void BringCommand(Client player, string targetString)
        {

            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    string message = string.Format(AdminRes.bring_player, player.SocialClubName);

                    // We get interior variables from the player
                    int playerHouse = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                    int playerBusiness = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);

                    // Change target's position and dimension
                    target.Position = player.Position;
                    target.Dimension = player.Dimension;
                    target.SetData(EntityData.PLAYER_HOUSE_ENTERED, playerHouse);
                    target.SetData(EntityData.PLAYER_BUSINESS_ENTERED, playerBusiness);

                    // Confirmation message sent to the command executor
                    target.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_GUN, Commands.HLP_GUN_COMMAND)]
        public void GunCommand(Client player, string targetString, string weaponName, int ammo)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the player from the input string
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null || target.GetData(EntityData.PLAYER_PLAYING) == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                    return;
                }

                if (Inventory.HasPlayerItemOnHand(target))
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.target_right_hand_not_empty);
                    return;
                }

                // Get the weapon from the name
                WeaponHash weapon = NAPI.Util.WeaponNameToModel(weaponName);

                if (weapon == 0)
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_GUN_COMMAND);
                }
                else
                {
                    // Give the weapon to the player
                    Weapons.GivePlayerNewWeapon(target, weapon, ammo, false);
                }
            }
        }

        [Command(Commands.COM_VEHICLE, Commands.HLP_VEHICLE_COMMAND, GreedyArg = true)]
        public void VehicleCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                int vehicleId = 0;
                Vehicle veh = null;
                VehicleModel vehicle = new VehicleModel();
                if (args.Trim().Length > 0)
                {
                    string[] arguments = args.Split(' ');
                    switch (arguments[0].ToLower())
                    {
                        case Commands.ARG_INFO:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                veh = Vehicles.GetClosestVehicle(player);
                                if (veh == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                                }
                                else
                                {
                                    vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                    string title = string.Format(GenRes.vehicle_check_title, vehicleId);
                                    VehicleHash model = (VehicleHash)veh.GetData(EntityData.VEHICLE_MODEL);
                                    string owner = veh.GetData(EntityData.VEHICLE_OWNER);

                                    player.SendChatMessage(title);
                                    player.SendChatMessage(GenRes.vehicle_model + model);
                                    player.SendChatMessage(GenRes.owner + owner);
                                }
                            }
                            break;
                        case Commands.ARG_CREATE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 4)
                                {
                                    string[] firstColorArray = arguments[2].Split(',');
                                    string[] secondColorArray = arguments[3].Split(',');

                                    if (firstColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS && secondColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS)
                                    {
                                        // Basic data for vehicle creation
                                        vehicle.model = NAPI.Util.GetHashKey(arguments[1]);
                                        vehicle.faction = Constants.FACTION_ADMIN;
                                        vehicle.position = Globals.GetForwardPosition(player, 2.5f);
                                        vehicle.rotation = player.Rotation;
                                        vehicle.dimension = player.Dimension;
                                        vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
                                        vehicle.firstColor = arguments[2];
                                        vehicle.secondColor = arguments[3];
                                        vehicle.pearlescent = 0;
                                        vehicle.owner = string.Empty;
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
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_CREATE_COMMAND);
                                }
                            }
                            break;
                        case Commands.ARG_MODIFY:
                            if (arguments.Length > 1)
                            {
                                switch (arguments[1].ToLower())
                                {
                                    case Commands.ARG_COLOR:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (arguments.Length == 4)
                                            {
                                                veh = Vehicles.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                                                }
                                                else
                                                {
                                                    string[] firstColorArray = arguments[2].Split(',');
                                                    string[] secondColorArray = arguments[3].Split(',');
                                                    if (firstColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS && secondColorArray.Length == Constants.TOTAL_COLOR_ELEMENTS)
                                                    {
                                                        try
                                                        {
                                                            vehicle.firstColor = arguments[2];
                                                            vehicle.secondColor = arguments[3];

                                                            veh.CustomPrimaryColor = new Color(int.Parse(firstColorArray[0]), int.Parse(firstColorArray[1]), int.Parse(firstColorArray[2]));
                                                            veh.CustomSecondaryColor = new Color(int.Parse(secondColorArray[0]), int.Parse(secondColorArray[1]), int.Parse(secondColorArray[2]));

                                                            veh.SetData(EntityData.VEHICLE_FIRST_COLOR, vehicle.firstColor.ToString());
                                                            veh.SetData(EntityData.VEHICLE_SECOND_COLOR, vehicle.secondColor.ToString());

                                                            Database.UpdateVehicleColor(vehicle);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            NAPI.Util.ConsoleOutput("[EXCEPTION Vehicle modify color] " + ex.Message);
                                                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_COLOR_COMMAND);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_COLOR_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_COLOR_COMMAND);
                                            }
                                        }
                                        break;
                                    case Commands.ARG_DIMENSION:
                                        if (arguments.Length == 4)
                                        {
                                            if (int.TryParse(arguments[2], out vehicleId) == true)
                                            {
                                                veh = Vehicles.GetVehicleById(vehicleId);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                                                }
                                                else
                                                {
                                                    // Obtenemos la dimension
                                                    if (uint.TryParse(arguments[3], out uint dimension) == true)
                                                    {
                                                        string message = string.Format(AdminRes.vehicle_dimension_modified, dimension);

                                                        veh.Dimension = dimension;
                                                        vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                        veh.SetData(EntityData.VEHICLE_DIMENSION, dimension);

                                                        Task.Factory.StartNew(() =>
                                                        {
                                                            NAPI.Task.Run(() =>
                                                            {
                                                                // Update the vehicle's dimension into the database
                                                                Database.UpdateVehicleSingleValue("dimension", Convert.ToInt32(dimension), vehicleId);
                                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                            });
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_DIMENSION_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_DIMENSION_COMMAND);
                                            }
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_DIMENSION_COMMAND);
                                        }
                                        break;
                                    case Commands.ARG_FACTION:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {

                                            if (arguments.Length == 3)
                                            {
                                                veh = Vehicles.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                                                }
                                                else
                                                {
                                                    // Obtenemos la facción
                                                    if (int.TryParse(arguments[2], out int faction) == true)
                                                    {
                                                        string message = string.Format(AdminRes.vehicle_faction_modified, faction);
                                                        vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                        veh.SetData(EntityData.VEHICLE_FACTION, faction);

                                                        Task.Factory.StartNew(() =>
                                                        {
                                                            NAPI.Task.Run(() =>
                                                            {
                                                                // Update the vehicle's faction into the database
                                                                Database.UpdateVehicleSingleValue("faction", faction, vehicleId);
                                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                            });
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_FACTION_COMMAND);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_FACTION_COMMAND);
                                            }
                                        }
                                        break;
                                    case Commands.ARG_POSITION:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (player.IsInVehicle)
                                            {
                                                vehicle.position = player.Vehicle.Position;
                                                vehicle.rotation = player.Vehicle.Rotation;
                                                vehicle.id = player.Vehicle.GetData(EntityData.VEHICLE_ID);
                                                player.Vehicle.SetData(EntityData.VEHICLE_POSITION, vehicle.position);
                                                player.Vehicle.SetData(EntityData.VEHICLE_ROTATION, vehicle.rotation);

                                                Task.Factory.StartNew(() =>
                                                {
                                                    NAPI.Task.Run(() =>
                                                    {
                                                        // Update the vehicle's position into the database
                                                        Database.UpdateVehiclePosition(vehicle);
                                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.vehicle_pos_updated);
                                                    });
                                                });
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                                            }
                                        }
                                        break;
                                    case Commands.ARG_OWNER:
                                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                        {
                                            if (arguments.Length == 4)
                                            {
                                                veh = Vehicles.GetClosestVehicle(player);
                                                if (veh == null)
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                                                }
                                                else
                                                {
                                                    string owner = arguments[2] + " " + arguments[3];
                                                    string message = string.Format(AdminRes.vehicle_owner_modified, owner);
                                                    vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                                                    veh.SetData(EntityData.VEHICLE_OWNER, owner);

                                                    Task.Factory.StartNew(() =>
                                                    {
                                                        NAPI.Task.Run(() =>
                                                        {
                                                            // Update the vehicle's owner into the database
                                                            Database.UpdateVehicleSingleString("owner", owner, vehicleId);
                                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                        });
                                                    });
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_VEHICLE_OWNER_COMMAND);
                                            }
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Commands.HLP_VEHICLE_MODIFY_COMMAND);
                                        break;
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Commands.HLP_VEHICLE_MODIFY_COMMAND);
                            }
                            break;
                        case Commands.ARG_REMOVE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh != null)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Remove the vehicle
                                                veh.Delete();
                                                Database.RemoveVehicle(vehicleId);
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Commands.HLP_VEHICLE_DELETE_COMMAND);
                                }
                            }
                            break;
                        case Commands.ARG_REPAIR:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (player.IsInVehicle)
                                {
                                    player.Vehicle.Repair();
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.vehicle_repaired);
                                }
                                else
                                {
                                    // Send the message warning that it should be into a vehicle
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                                }
                            }
                            break;
                        case Commands.ARG_LOCK:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                veh = Vehicles.GetClosestVehicle(player);
                                if (veh == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_vehicles_near);
                                }
                                else
                                {
                                    veh.Locked = !veh.Locked;
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + (veh.Locked ? SuccRes.veh_locked : SuccRes.veh_unlocked));
                                }
                            }
                            break;
                        case Commands.ARG_START:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (player.VehicleSeat == (int)VehicleSeat.Driver)
                                {
                                    player.Vehicle.EngineStatus = true;
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_driving);
                                }
                            }
                            break;
                        case Commands.ARG_BRING:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh != null)
                                    {
                                        // Get the vehicle to the player's position
                                        veh.Position = Globals.GetForwardPosition(player, 2.5f);
                                        veh.SetData(EntityData.VEHICLE_POSITION, veh.Position);

                                        // Send the message to the player
                                        string message = string.Format(AdminRes.vehicle_bring, vehicleId);
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Commands.HLP_VEHICLE_BRING_COMMAND);
                                }
                            }
                            break;
                        case Commands.ARG_TP:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                if (arguments.Length == 2 && int.TryParse(arguments[1], out vehicleId) == true)
                                {
                                    veh = Vehicles.GetVehicleById(vehicleId);
                                    if (veh == null)
                                    {
                                        VehicleModel vehModel = Parking.GetParkedVehicleById(vehicleId);

                                        if (vehModel == null)
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                                        }
                                        else
                                        {
                                            // Teleport player to the parking
                                            ParkingModel parking = Parking.GetParkingById(vehModel.parking);
                                            player.Position = parking.position;

                                            // Send the message to the player
                                            string message = string.Format(AdminRes.vehicle_goto, vehicleId);
                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                        }
                                    }
                                    else
                                    {
                                        // Get the player to the vehicle's position
                                        player.Position = Globals.GetForwardPosition(veh, 2.5f);

                                        // Send the message to the player
                                        string message = string.Format(AdminRes.vehicle_goto, vehicleId);
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Commands.HLP_VEHICLE_GOTO_COMMAND);
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Commands.HLP_VEHICLE_COMMAND);
                            break;
                    }
                }
            }
        }

        [Command(Commands.COM_GO, Commands.HLP_GO_COMMAND)]
        public void GoCommand(Client player, string location)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                switch (location.ToLower())
                {
                    case Commands.ARG_WORKSHOP:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1204.13f, -1489.49f, 4.34967f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_ELECTRONICS:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1148.98f, -1608.94f, 4.41592f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_POLICE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1111.952f, -824.9194f, 19.31578f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_TOWNHALL:
                        player.Dimension = 0;
                        player.Position = new Vector3(-136.4768f, 6198.505f, 32.38424f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_LICENSE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-227.5136f, 6321.819f, 31.46245f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_VANILLA:
                        player.Dimension = 0;
                        player.Position = new Vector3(120f, -1400f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_HOSPITAL:
                        player.Dimension = 0;
                        player.Position = new Vector3(-242.862f, 6325.652f, 32.42619f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_NEWS:
                        player.Dimension = 0;
                        player.Position = new Vector3(-600f, -950f, 25f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_BAHAMA:
                        player.Dimension = 0;
                        player.Position = new Vector3(-1400f, -590f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_MECHANIC:
                        player.Dimension = 0;
                        player.Position = new Vector3(492f, -1300f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    case Commands.ARG_GARBAGE:
                        player.Dimension = 0;
                        player.Position = new Vector3(-320f, -1550f, 30f);
                        player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                        player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_GO_COMMAND);
                        break;

                }
            }
        }

        //[Command("business", "create", GreedyArg = true)]
        //public void CMD_BusinessCreate(Client player, Client target, string name)
        //{

        //}

        [Command(Commands.COM_BUSINESS, Commands.HLP_BUSINESS_COMMAND, GreedyArg = true)]
        public void BusinessCommand(Client player, string args)
        {
            if (HasUserCommandPermission(player, Commands.COM_BUSINESS) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (args.Trim().Length > 0)
                {
                    BusinessModel business = new BusinessModel();
                    string[] arguments = args.Split(' ');
                    string message = string.Empty;
                    switch (arguments[0].ToLower())
                    {
                        case Commands.ARG_INFO:
                            break;
                        case Commands.ARG_CREATE:
                            if (HasUserCommandPermission(player, Commands.COM_BUSINESS, Commands.ARG_CREATE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                if (arguments.Length == 3)
                                {
                                    // We get the business type
                                    if (int.TryParse(arguments[1], out int type) && (arguments[2] == Commands.ARG_INNER || arguments[2] == Commands.ARG_OUTER))
                                    {
                                        business.type = type;
                                        business.ipl = arguments[2] == Commands.ARG_INNER ? Business.GetBusinessTypeIpl(type) : string.Empty;
                                        business.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 1.0f);
                                        business.dimension = arguments[2] == Commands.ARG_INNER ? player.Dimension : 0;
                                        business.multiplier = 3.0f;
                                        business.owner = string.Empty;
                                        business.locked = false;
                                        business.name = GenRes.business;

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Get the id from the business
                                                business.id = Database.AddNewBusiness(business);

                                                if (arguments[2] == Commands.ARG_INNER)
                                                {
                                                    // The business has a label to enter
                                                    business.businessLabel = NAPI.TextLabel.CreateTextLabel(business.name, business.position, 5.0f, 0.75f, 4, new Color(255, 255, 255), false, business.dimension);
                                                }
                                                else
                                                {
                                                    Vector3 businessPos = business.position;
                                                    Vector3 newPosition = businessPos;
                                                    newPosition.Z = newPosition.Z + 1.5f;

                                                    if (type == 12)
                                                    {
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("Gas Pump", newPosition, 5.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Business Name
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("/refuel", new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.2f), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0); // Business Command Info   
                                                    }
                                                    if (type == 1)
                                                    {
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("24/7", newPosition, 5.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Business Name
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("/purchase", new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.2f), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0); // Business Command Info    
                                                    }
                                                    if (type == 4)
                                                    {
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("Clothing Store", newPosition, 5.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Business Name
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("/purchase", new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.2f), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0); // Business Command Info    
                                                    }
                                                    if (type == 24)
                                                    {
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("Fishing Shop", newPosition, 5.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Business Name
                                                        business.businessLabel = NAPI.TextLabel.CreateTextLabel("/purchase", new Vector3(newPosition.X, newPosition.Y, newPosition.Z - 0.2f), 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0); // Business Command Info    
                                                    }
                                                    else
                                                    {
                                                        //business.businessLabel = NAPI.TextLabel.CreateTextLabel(business.name, newPosition, 5.0f, 0.75f, 4, new Color(255, 255, 255), false, 0);
                                                        business.businessColshape = NAPI.ColShape.CreateCylinderColShape(business.position, 2.5f, 5.0f);
                                                        business.businessMarker = NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, business.position, new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));
                                                    }
                                                }

                                                // Add the business to the list
                                                Business.businessList.Add(business);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_COMMAND);
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_COMMAND);
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                }
                            }
                            break;
                        case Commands.ARG_MODIFY:
                            if (HasUserCommandPermission(player, Commands.COM_BUSINESS, Commands.ARG_MODIFY) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                            {
                                business = Business.GetClosestBusiness(player);
                                if (business != null )
                                {
                                    if (arguments.Length > 1)
                                    {
                                        switch (arguments[1].ToLower())
                                        {
                                            case Commands.ARG_NAME:
                                                if (arguments.Length > 2)
                                                {
                                                    // We change business name
                                                    string businessName = string.Join(" ", arguments.Skip(2));
                                                    business.name = businessName;
                                                    business.businessLabel.Text = businessName;
                                                    message = string.Format(AdminRes.business_name_modified, businessName);

                                                    Task.Factory.StartNew(() =>
                                                    {
                                                        NAPI.Task.Run(() =>
                                                        {
                                                            // Update the business information
                                                            Database.UpdateBusiness(business);
                                                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                        });
                                                    });
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_MODIFY_NAME_COMMAND);
                                                }
                                                break;
                                            case Commands.ARG_TYPE:
                                                if (arguments.Length == 3)
                                                {
                                                    // We get business type
                                                    if (int.TryParse(arguments[2], out int businessType) == true)
                                                    {
                                                        // Changing business type
                                                        business.type = businessType;

                                                        if (business.ipl.Length > 0)
                                                        {
                                                            // Check if the business is inner or outer
                                                            business.ipl = Business.GetBusinessTypeIpl(businessType);
                                                        }

                                                        message = string.Format(AdminRes.business_type_modified, businessType);

                                                        Task.Factory.StartNew(() =>
                                                        {
                                                            NAPI.Task.Run(() =>
                                                            {
                                                                // Update the business information
                                                                Database.UpdateBusiness(business);
                                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                            });
                                                        });
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_MODIFY_TYPE_COMMAND);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                                    }
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_MODIFY_TYPE_COMMAND);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND2);
                                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_CREATE_TYPES_FIRST_COMMAND3);
                                                }
                                                break;
                                            default:
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_MODIFY_COMMAND);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_MODIFY_COMMAND);
                                    }
                                }
                            }
                            break;
                        case Commands.ARG_REMOVE:
                            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                            {
                                business = Business.GetClosestBusiness(player);
                                if (business != null)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            if (business.ipl.Length == 0)
                                            {
                                                // Delete the business checkpoint
                                                business.businessMarker.Delete();
                                                business.businessColshape.Delete();
                                            }
                                            else
                                            {
                                                // Delete the business label
                                                business.businessLabel.Delete();
                                            }

                                            // Delete the business
                                            Database.DeleteBusiness(business.id);
                                            Business.businessList.Remove(business);
                                        });
                                    });
                                }
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_BUSINESS_COMMAND);
                }
            }
        }

        [Command(Commands.COM_CHARACTER, Commands.HLP_CHARACTER_COMMAND)]
        public void CharacterCommand(Client player, string action, string name = "", string surname = "", string amount = "")
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                Client target = null;

                // We check whether we have an id or a full name
                if (int.TryParse(name, out int targetId) == true)
                {
                    target = Globals.GetPlayerById(targetId);
                    amount = surname;
                }
                else
                {
                    target = NAPI.Player.GetPlayerFromName(name + " " + surname);
                }

                // We check whether the player is connected
                if (target != null && target.GetData(EntityData.PLAYER_PLAYING) != null)
                {
                    // Getting the amount
                    if (int.TryParse(amount, out int value) == true)
                    {
                        string message = string.Empty;
                        switch (action.ToLower())
                        {
                            case Commands.ARG_BANK:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                                {
                                    target.SetSharedData(EntityData.PLAYER_BANK, value);
                                    message = string.Format(AdminRes.player_bank_modified, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Commands.ARG_MONEY:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                                {
                                    target.SetSharedData(EntityData.PLAYER_MONEY, value);
                                    message = string.Format(AdminRes.player_money_modified, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Commands.ARG_FACTION:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_FACTION, value);
                                    message = string.Format(AdminRes.player_faction_modified, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Commands.ARG_JOB:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_JOB, value);
                                    message = string.Format(AdminRes.player_job_modified, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Commands.ARG_RANK:
                                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                {
                                    target.SetData(EntityData.PLAYER_RANK, value);
                                    message = string.Format(AdminRes.player_rank_modified, value, target.Name);
                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                }
                                break;
                            case Commands.ARG_DIMENSION:
                                target.Dimension = Convert.ToUInt32(value);
                                message = string.Format(AdminRes.player_dimension_modified, value, target.Name);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_CHARACTER_COMMAND);
                                break;
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_CHARACTER_COMMAND);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_HOUSE, Commands.HLP_HOUSE_COMMAND, GreedyArg = true)]
        public void HouseCommand(Client player, string args)
        {
            HouseModel house = House.GetClosestHouse(player);

            string[] arguments = args.Split(' ');
            switch (arguments[0].ToLower())
            {
                case Commands.ARG_INFO:
                    // We get house identifier
                    if (arguments.Length == 2 && int.TryParse(arguments[1], out int houseId) == true)
                    {
                        house = House.GetHouseById(houseId);
                        if (house != null)
                        {
                            SendHouseInfo(player, house);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_exists);
                        }
                    }
                    else if (arguments.Length == 1)
                    {
                        SendHouseInfo(player, house);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_HOUSE_INFO_COMMAND);
                    }

                    break;
                case Commands.ARG_CREATE:
                    if (HasUserCommandPermission(player, Commands.COM_HOUSE, Commands.ARG_CREATE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                    {
                        string houseLabel = string.Empty;
                        house = new HouseModel();
                        {
                            house.ipl = Constants.HOUSE_IPL_LIST[0].ipl;
                            house.name = GenRes.house;
                            house.position = player.Position;
                            house.dimension = player.Dimension;
                            house.price = 10000;
                            house.owner = string.Empty;
                            house.status = Constants.HOUSE_STATE_BUYABLE;
                            house.tenants = 2;
                            house.rental = 0;
                            house.locked = true;
                        }

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Add a new house
                                house.id = Database.AddHouse(house);
                                house.houseLabel = NAPI.TextLabel.CreateTextLabel(House.GetHouseLabelText(house), house.position, 5.0f, 0.75f, 4, new Color(255, 255, 255));
                                House.houseList.Add(house);

                                // Send the confirmation message
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.house_created);
                            });
                        });
                    }
                    break;
                case Commands.ARG_MODIFY:
                    if (arguments.Length > 2)
                    {
                        string message = string.Empty;

                        if (int.TryParse(arguments[2], out int value) == true)
                        {
                            // Numeric modifications
                            switch (arguments[1].ToLower())
                            {
                                case Commands.ARG_INTERIOR:
                                    if (HasUserCommandPermission(player, Commands.COM_HOUSE, Commands.ARG_INTERIOR) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                    {
                                        if (value >= 0 && value < Constants.HOUSE_IPL_LIST.Count)
                                        {
                                            house.ipl = Constants.HOUSE_IPL_LIST[value].ipl;

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    // Update the house's information
                                                    Database.UpdateHouse(house);

                                                    // Confirmation message sent to the player
                                                    message = string.Format(AdminRes.house_interior_modified, value);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            });
                                        }
                                        else
                                        {
                                            message = string.Format(ErrRes.house_interior_modify, Constants.HOUSE_IPL_LIST.Count - 1);
                                            player.SendChatMessage(Constants.COLOR_ERROR + message);
                                        }
                                    }
                                    break;
                                case Commands.ARG_PRICE:
                                    if (HasUserCommandPermission(player, Commands.COM_HOUSE, Commands.ARG_PRICE) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                    {
                                        if (value > 0)
                                        {
                                            house.price = value;
                                            house.status = Constants.HOUSE_STATE_BUYABLE;
                                            house.houseLabel.Text = House.GetHouseLabelText(house);

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    // Update the house's information
                                                    Database.UpdateHouse(house);

                                                    // Confirmation message sent to the player
                                                    message = string.Format(AdminRes.house_price_modified, value);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_price_modify);
                                        }
                                    }
                                    break;
                                case Commands.ARG_STATE:
                                    if (value >= 0 && value < 3)
                                    {
                                        house.status = value;
                                        house.houseLabel.Text = House.GetHouseLabelText(house);

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(AdminRes.house_status_modified, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_status_modify);
                                    }
                                    break;
                                case Commands.ARG_RENT:
                                    if (value > 0)
                                    {
                                        house.rental = value;
                                        house.status = Constants.HOUSE_STATE_RENTABLE;
                                        house.houseLabel.Text = House.GetHouseLabelText(house);

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(AdminRes.house_rental_modified, value);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_rental_modify);
                                    }
                                    break;
                                default:
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_HOUSE_MODIFY_INT_COMMAND);
                                    break;
                            }
                        }
                        else
                        {
                            string name = string.Empty;
                            for (int i = 2; i < arguments.Length; i++)
                            {
                                name += arguments[i] + " ";
                            }

                            // Text based modifications
                            switch (arguments[1].ToLower())
                            {
                                case Commands.ARG_OWNER:
                                    if (HasUserCommandPermission(player, Commands.COM_HOUSE, Commands.ARG_OWNER) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                    {
                                        house.owner = name.Trim();

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(AdminRes.house_owner_modified, house.owner);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        });
                                    }
                                    break;
                                case Commands.ARG_NAME:
                                    if (HasUserCommandPermission(player, Commands.COM_HOUSE, Commands.ARG_NAME) || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                                    {
                                        house.name = name.Trim();
                                        house.houseLabel.Text = House.GetHouseLabelText(house);

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the house's information
                                                Database.UpdateHouse(house);

                                                // Confirmation message sent to the player
                                                message = string.Format(AdminRes.house_name_modified, house.name);
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                            });
                                        });
                                    }
                                    break;
                                default:
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_HOUSE_MODIFY_String_COMMAND);
                                    break;

                            }
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_HOUSE_MODIFY_COMMAND);
                    }
                    break;
                case Commands.ARG_REMOVE:
                    if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                    {
                        if (house != null)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                NAPI.Task.Run(() =>
                                {
                                    // Remove the house
                                    house.houseLabel.Delete();
                                    Database.DeleteHouse(house.id);
                                    House.houseList.Remove(house);

                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.house_deleted);
                                });
                            });
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_house_near);
                        }
                    }
                    break;
                case Commands.ARG_TP:
                    if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
                    {
                        // We get the house
                        if (arguments.Length == 2 && int.TryParse(arguments[1], out int propertyId) == true)
                        {
                            house = House.GetHouseById(propertyId);
                            if (house != null)
                            {
                                player.Position = house.position;
                                player.Dimension = house.dimension;
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_exists);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Commands.HLP_HOUSE_GOTO_COMMAND);
                        }
                    }
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_HOUSE_COMMAND);
                    break;
            }

        }

        [Command(Commands.COM_PARKING, Commands.HLP_PARKING_COMMAND, GreedyArg = true)]
        public void ParkingCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                string[] arguments = args.Split(' ');
                ParkingModel parking = Parking.GetClosestParking(player);
                switch (arguments[0].ToLower())
                {
                    case Commands.ARG_INFO:
                        if (parking != null)
                        {
                            int vehicles = 0;
                            string vehicleList = string.Empty;
                            string info = string.Format(AdminRes.parking_info, parking.id);
                            player.SendChatMessage(Constants.COLOR_ADMIN_INFO + info);
                            foreach (ParkedCarModel parkedCar in Parking.parkedCars)
                            {
                                if (parkedCar.parkingId == parking.id)
                                {
                                    vehicleList += parkedCar.vehicle.model + " LS-" + parkedCar.vehicle.id + " ";
                                    vehicles++;
                                }
                            }

                            // We send the message with the vehicles in the parking, if any
                            player.SendChatMessage(vehicles > 0 ? Constants.COLOR_HELP + vehicleList : Constants.COLOR_INFO + InfoRes.parking_empty);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_near);
                        }
                        break;
                    case Commands.ARG_CREATE:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            if (arguments.Length == 2)
                            {
                                // We get the parking type
                                if (int.TryParse(arguments[1], out int type) == true)
                                {
                                    if (type < Constants.PARKING_TYPE_PUBLIC || type > Constants.PARKING_TYPE_DEPOSIT)
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_CREATE_COMMAND);
                                    }
                                    else
                                    {
                                        parking = new ParkingModel();
                                        {
                                            parking.type = type;
                                            parking.position = player.Position;
                                        }

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Create the new parking
                                                parking.id = Database.AddParking(parking);
                                                parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(Parking.GetParkingLabelText(parking.type), parking.position, 20.0f, 0.75f, 4, new Color(255, 255, 255));
                                                Parking.parkingList.Add(parking);

                                                // Send the confirmation message to the player
                                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.parking_created);
                                            });
                                        });
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_CREATE_COMMAND);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_CREATE_COMMAND);
                            }
                        }
                        break;
                    case Commands.ARG_MODIFY:
                        if (arguments.Length == 3)
                        {
                            if (parking != null)
                            {
                                switch (arguments[1].ToLower())
                                {
                                    case Commands.ARG_HOUSE:
                                        if (parking.type == Constants.PARKING_TYPE_GARAGE)
                                        {
                                            // We link the house to this parking
                                            if (int.TryParse(arguments[2], out int houseId) == true)
                                            {
                                                parking.houseId = houseId;

                                                Task.Factory.StartNew(() =>
                                                {
                                                    NAPI.Task.Run(() =>
                                                    {
                                                        // Update the parking's information
                                                        Database.UpdateParking(parking);

                                                        // Confirmation message sent to the player
                                                        string message = string.Format(AdminRes.parking_house_modified, houseId);
                                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                    });
                                                });
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_MODIFY_COMMAND);
                                            }
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.parking_not_garage);
                                        }
                                        break;
                                    case Commands.ARG_PLACES:
                                        int slots = 0;
                                        if (int.TryParse(arguments[2], out slots) == true)
                                        {
                                            parking.capacity = slots;
                                            parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(Parking.GetParkingLabelText(parking.type), parking.position, 20.0f, 0.75f, 4, new Color(255, 255, 255));

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    // Update the parking's information
                                                    Database.UpdateParking(parking);

                                                    // Confirmation message sent to the player
                                                    string message = string.Format(AdminRes.parking_slots_modified, slots);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_MODIFY_COMMAND);
                                        }
                                        break;
                                    case Commands.ARG_TYPE:
                                        int type = 0;
                                        if (int.TryParse(arguments[2], out type) == true)
                                        {
                                            parking.type = type;

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    // Update the parking's information
                                                    Database.UpdateParking(parking);

                                                    // Confirmation message sent to the player
                                                    string message = string.Format(AdminRes.parking_type_modified, type);
                                                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                                                });
                                            });
                                        }
                                        else
                                        {
                                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_MODIFY_COMMAND);
                                        }
                                        break;
                                    default:
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_MODIFY_COMMAND);
                                        break;
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_near);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_MODIFY_COMMAND);
                        }
                        break;
                    case Commands.ARG_REMOVE:
                        if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
                        {
                            if (parking != null)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Update the parking's information
                                        parking.parkingLabel.Delete();
                                        Database.DeleteParking(parking.id);
                                        Parking.parkingList.Remove(parking);

                                        // Confirmation message sent to the player
                                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.parking_deleted);
                                    });
                                });
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_near);
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PARKING_COMMAND);
                        break;
                }
            }
        }

        [Command(Commands.COM_POS)]
        public void PosCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Vector3 position = player.Position;
                NAPI.Util.ConsoleOutput("{0},{1},{2}", player.Position.X, player.Position.Y, player.Position.Z);
            }
        }

        [Command(Commands.COM_REVIVE, Commands.HLP_REVIVE_COMMAND)]
        public void ReviveCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    if (target.GetSharedData(EntityData.PLAYER_KILLED) != 0)
                    {
                        Emergency.CancelPlayerDeath(target);
                        string playerMessage = string.Format(AdminRes.player_revived, target.Name);
                        string targetMessage = string.Format(SuccRes.admin_revived, player.SocialClubName);
                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_SUCCESS + targetMessage);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_dead);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_WEATHER, Commands.HLP_WEATHER_COMMAND)]
        public void WeatherCommand(Client player, int weather)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (weather < 0 || weather > 14)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.weather_value_invalid);
                }
                else
                {
                    if (weather > 14)
                        return;

                    // Change the weather
                    NAPI.World.SetWeather((Weather)weather);

                    string message = string.Format(AdminRes.weather_changed, player.Name, weather);
                    NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);
                }
            }
        }

        [Command(Commands.COM_JAIL, Commands.HLP_JAIL_COMMAND, GreedyArg = true)]
        public void JailCommand(Client player, string args)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                int jailTime = 0;
                string[] arguments = args.Trim().Split(' ');

                if (arguments.Length > 2)
                {
                    Client target = null;
                    string reason = string.Empty;

                    if (int.TryParse(arguments[0], out int targetId) == true)
                    {
                        target = Globals.GetPlayerById(targetId);
                        if (int.TryParse(arguments[1], out jailTime) == true)
                        {
                            reason = string.Join(" ", arguments.Skip(2));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_JAIL_COMMAND);
                        }
                    }
                    else if (arguments.Length > 3)
                    {
                        target = NAPI.Player.GetPlayerFromName(arguments[0] + " " + arguments[1]);
                        if (int.TryParse(arguments[2], out jailTime) == true)
                        {
                            reason = string.Join(" ", arguments.Skip(3));
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_JAIL_COMMAND);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_JAIL_COMMAND);
                        return;
                    }

                    // We move the player to the jail
                    target.Dimension = 0;
                    target.Position = new Vector3(1651.441f, 2569.83f, 45.56486f);

                    // We set jail type
                    target.SetData(EntityData.PLAYER_JAILED, jailTime);
                    target.SetData(EntityData.PLAYER_JAIL_TYPE, Constants.JAIL_TYPE_OOC);

                    // Message sent to the whole server
                    string message = string.Format(AdminRes.player_jailed, target.Name, jailTime, reason);
                    NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // We add the log in the database
                            Database.AddAdminLog(player.SocialClubName, target.Name, "jail", jailTime, reason);
                        });
                    });
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_JAIL_COMMAND);
                }
            }
        }

        [Command(Commands.COM_KICK, Commands.HLP_KICK_COMMAND, GreedyArg = true)]
        public void KickCommand(Client player, string targetString, string reason)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                target.Kick(reason);

                //  Message sent to the whole server
                string message = string.Format(AdminRes.player_kicked, player.Name, target.Name, reason);
                NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // We add the log in the database
                        Database.AddAdminLog(player.SocialClubName, target.Name, "kick", 0, reason);
                    });
                });
            }
        }

        [Command(Commands.COM_KICKALL)]
        public void KickAllCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                // Kick all the players only but the command sender
                NAPI.Pools.GetAllPlayers().Where(t => t != player).ToList().ForEach(t => t.Kick());

                // Confirmation message sent to the player
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.kicked_all);
            }
        }

        [Command(Commands.COM_BAN, Commands.HLP_BAN_COMMAND, GreedyArg = true)]
        public void BanCommand(Client player, string targetString, string reason)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                target.Ban(reason);

                string message = string.Format(AdminRes.player_banned, player.Name, target.Name, reason);
                NAPI.Chat.SendChatMessageToAll(Constants.COLOR_ADMIN_INFO + message);

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // We add the log in the database
                        Database.AddAdminLog(player.SocialClubName, target.Name, "ban", 0, reason);
                    });
                });
            }
        }

        [Command(Commands.COM_HEALTH, Commands.HLP_HEALTH_COMMAND)]
        public void HealthCommand(Client player, string targetString, int health)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                // We get the target player
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                    return;

                target.Health = health;

                // We send the confirmation message to both players
                string playerMessage = string.Format(AdminRes.player_health, target.Name, health);
                string targetMessage = string.Format(AdminRes.target_health, player.Name, health);
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
            }
        }

        [Command(Commands.COM_SAVE)]
        public void SaveCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                string message = string.Empty;

                // We print a message saying when the command starts
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.save_start);

                // Saving all business
                Database.UpdateAllBusiness(Business.businessList);

                message = string.Format(AdminRes.save_business, Business.businessList.Count);
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);

                // Saving all connected players
                List<Client> connectedPlayers = NAPI.Pools.GetAllPlayers().Where(pl => pl.GetData(EntityData.PLAYER_PLAYING) != null).ToList();
                foreach (Client target in connectedPlayers)
                {
                    // Save the player into the database
                    Character.SaveCharacterData(target);
                }

                // All the characters saved
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.characters_saved);

                // Vehicles saving
                List<VehicleModel> vehicleList = new List<VehicleModel>();
                List<OceanVehicleModel> oceanVehicleList = new List<OceanVehicleModel>();

                List<Vehicle> citizenVehicles = NAPI.Pools.GetAllVehicles().Where(v => v.GetData(EntityData.VEHICLE_FACTION) == 0 && v.GetData(EntityData.VEHICLE_PARKING) == 0).ToList();
                //List<Vehicle> oceanVehicles = NAPI.Pools.GetAllVehicles().Where(v => v.GetData(EntityData.VEHICLE_MODEL) == (uint)VehicleHash.Submersible2).ToList();

                /* foreach (Vehicle oceanVehicle in oceanVehicles)
                 {
                     OceanVehicleModel oceanVehicleModel = new OceanVehicleModel();

                     //oceanVehicleModel.id = player.GetData(EntityData.OCEANVEHICLE_ID);
                     oceanVehicleModel.owner = player.GetData(EntityData.OCEANVEHICLE_OWNER);
                     oceanVehicleModel.scrap = player.GetData(EntityData.OCEANVEHICLE_SCRAP);
                     oceanVehicleModel.info = player.GetData(EntityData.OCEANVEHICLE_INFO);
                     oceanVehicleModel.rareInfo = player.GetData(EntityData.OCEANVEHICLE_RAREINFO);

                     oceanVehicleList.Add(oceanVehicleModel);
                 }
                 */
                foreach (Vehicle vehicle in citizenVehicles)
                {
                    VehicleModel vehicleModel = new VehicleModel();
                    {
                        // Getting the needed values to be stored
                        vehicleModel.id = vehicle.GetData(EntityData.VEHICLE_ID);
                        vehicleModel.model = vehicle.GetData(EntityData.VEHICLE_MODEL);
                        vehicleModel.position = vehicle.Position;
                        vehicleModel.rotation = vehicle.Rotation;
                        vehicleModel.dimension = vehicle.Dimension;
                        vehicleModel.colorType = vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                        vehicleModel.firstColor = vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                        vehicleModel.secondColor = vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                        vehicleModel.pearlescent = vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);
                        vehicleModel.faction = vehicle.GetData(EntityData.VEHICLE_FACTION);
                        vehicleModel.plate = vehicle.GetData(EntityData.VEHICLE_PLATE);
                        vehicleModel.owner = vehicle.GetData(EntityData.VEHICLE_OWNER);
                        vehicleModel.price = vehicle.GetData(EntityData.VEHICLE_PRICE);
                        vehicleModel.parking = vehicle.GetData(EntityData.VEHICLE_PARKING);
                        vehicleModel.parked = vehicle.GetData(EntityData.VEHICLE_PARKED);
                        vehicleModel.gas = vehicle.GetData(EntityData.VEHICLE_GAS);
                        vehicleModel.kms = vehicle.GetData(EntityData.VEHICLE_KMS);

                        vehicleList.Add(vehicleModel);
                    }
                }

                // Saving the list into database
                Database.SaveAllVehicles(vehicleList);
                // Database.SaveAllOceanVehicles(oceanVehicleList);              

                // All vehicles saved
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.vehicles_saved);

                // End of the command
                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.save_finish);
            }
        }

        [Command(Commands.COM_ADUTY)]
        public void ADutyCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                if (player.GetData(EntityData.PLAYER_ADMIN_ON_DUTY) != null)
                {
                    player.Invincible = false;
                    player.ResetNametagColor();
                    player.ResetData(EntityData.PLAYER_ADMIN_ON_DUTY);
                    player.SendNotification(InfoRes.player_admin_free_time);
                }
                else
                {
                    player.Invincible = true;
                    player.NametagColor = new Color(231, 133, 46);
                    player.SetData(EntityData.PLAYER_ADMIN_ON_DUTY, true);
                    player.SendNotification(InfoRes.player_admin_on_duty);
                }
            }
        }

        [Command(Commands.COM_TICKETS)]
        public void TicketsCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.ticket_list);
                foreach (AdminTicketModel adminTicket in Globals.adminTicketList)
                {
                    Client target = Globals.GetPlayerById(adminTicket.playerId);

                    if (target != null && target.Exists)
                    {
                        string ticket = target.Name + " (" + adminTicket.playerId + "): " + adminTicket.question;
                        player.SendChatMessage(Constants.COLOR_HELP + ticket);
                    }
                }
            }
        }

        [Command(Commands.COM_ATICKET, Commands.HLP_ANSWER_HELP_REQUEST, GreedyArg = true)]
        public void ATicketCommand(Client player, int ticket, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                foreach (AdminTicketModel adminTicket in Globals.adminTicketList)
                {
                    if (adminTicket.playerId == ticket)
                    {
                        Client target = Globals.GetPlayerById(adminTicket.playerId);

                        // We send the answer to the player
                        string targetMessage = string.Format(InfoRes.ticket_answer, message);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                        // We send the confirmation to the staff
                        string playerMessage = string.Format(AdminRes.ticket_answered, ticket);
                        player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);

                        // Ticket removed
                        Globals.adminTicketList.Remove(adminTicket);
                        return;
                    }
                }

                // There's no ticket with that identifier
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.admin_ticket_not_found);
            }
        }

        [Command(Commands.COM_A, Commands.HLP_ADMIN_TEXT_COMMAND, GreedyArg = true)]
        public void ACommand(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE)
            {
                // Get all the staff playing
                List<Client> targetList = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE).ToList();

                foreach (Client target in targetList)
                {
                    // Send the message to each one of the staff members
                    target.SendChatMessage(Constants.COLOR_ADMIN_INFO + "((Staff [ID: " + player.Value + "] " + player.Name + ": " + message + "))");
                }
            }
        }

        [Command(Commands.COM_RECON, Commands.HLP_RECON_COMMAND, GreedyArg = true)]
        public void ReconCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target.GetData(EntityData.PLAYER_PLAYING) == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
                else if (target.Spectating)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_spectating);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.cant_spect_self);
                }
                else
                {
                    player.Spectate(target);
                    string message = string.Format(AdminRes.spectating_player, target.Name);
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + message);
                }
            }
        }

        [Command(Commands.COM_RECOFF)]
        public void RecoffCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                if (!player.Spectating)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_spectating);
                }
                else
                {
                    player.StopSpectating();
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.spect_stopped);
                }
            }
        }

        [Command(Commands.COM_INFO, Commands.HLP_INFO_COMMAND)]
        public void InfoCommand(Client player, string targetString)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_SUPPORT)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target != null)
                {
                    // Get player's basic data
                    PlayerData.RetrieveBasicDataEvent(player, target.Value);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
            }
        }

        [Command(Commands.COM_POINTS, Commands.HLP_POINTS_COMMAND, GreedyArg = true)]
        public void PuntosCommand(Client player, string arguments)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_GAME_MASTER)
            {
                string[] args = arguments.Trim().Split(' ');
                if (args.Length == 3 || args.Length == 4)
                {
                    int rolePoints = 0;
                    Client target = null;

                    if (int.TryParse(args[1], out int targetId) == true)
                    {
                        target = Globals.GetPlayerById(targetId);
                        rolePoints = int.Parse(args[2]);
                    }
                    else
                    {
                        target = NAPI.Player.GetPlayerFromName(args[1] + " " + args[2]);
                        rolePoints = int.Parse(args[3]);
                    }

                    if (target != null && target.GetData(EntityData.PLAYER_PLAYING) != null)
                    {
                        // We get player's role points
                        string playerMessage = string.Empty;
                        string targetMessage = string.Empty;
                        int targetRolePoints = target.GetData(EntityData.PLAYER_ROLE_POINTS);

                        switch (args[0].ToLower())
                        {
                            case Commands.ARG_GIVE:
                                // We give role points to the player
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, targetRolePoints + rolePoints);

                                playerMessage = string.Format(AdminRes.role_points_given, target.Name, rolePoints);
                                targetMessage = string.Format(AdminRes.role_points_received, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                                target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);

                                break;
                            case Commands.ARG_REMOVE:
                                // We remove role points to the player
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, targetRolePoints - rolePoints);

                                playerMessage = string.Format(AdminRes.role_points_removed, target.Name, rolePoints);
                                targetMessage = string.Format(AdminRes.role_points_lost, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                                target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
                                break;
                            case Commands.ARG_SET:
                                // We set player's role points
                                target.SetData(EntityData.PLAYER_ROLE_POINTS, rolePoints);

                                playerMessage = string.Format(AdminRes.role_points_set, target.Name, rolePoints);
                                targetMessage = string.Format(AdminRes.role_points_established, player.SocialClubName, rolePoints);
                                player.SendChatMessage(Constants.COLOR_ADMIN_INFO + playerMessage);
                                target.SendChatMessage(Constants.COLOR_ADMIN_INFO + targetMessage);
                                break;
                            default:
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POINTS_COMMAND);
                                break;
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POINTS_COMMAND);
                }
            }
        }
    }
}
