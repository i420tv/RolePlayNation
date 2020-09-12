using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.vehicles;
using WiredPlayers.house;
using WiredPlayers.messages.general;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using WiredPlayers.factions;

namespace WiredPlayers.parking
{
    public class Parking : Script
    {
        public static List<ParkingModel> parkingList;
        public static List<ParkedCarModel> parkedCars;

        public static void LoadDatabaseParkings()
        {
            parkingList = Database.LoadAllParkings();
            foreach (ParkingModel parking in parkingList)
            {
                string parkingLabelText = GetParkingLabelText(parking.type);
                parking.parkingLabel = NAPI.TextLabel.CreateTextLabel(parkingLabelText, parking.position, 30.0f, 0.75f, 4, new Color(255, 255, 255));
            }
        }

        public static ParkingModel GetClosestParking(Client player, float distance = 1.5f)
        {
            ParkingModel parking = null;
            foreach (ParkingModel parkingModel in parkingList)
            {
                if (parkingModel.position.DistanceTo(player.Position) < distance)
                {
                    distance = parkingModel.position.DistanceTo(player.Position);
                    parking = parkingModel;
                }
            }
            return parking;
        }

        public static int GetParkedCarAmount(ParkingModel parking)
        {
            // Get all the vehicles in a parking
            return parkedCars.Count(parkedCar => parkedCar.parkingId == parking.id);
        }

        public static VehicleModel GetParkedVehicleById(int vehicleId)
        {
            // Get the vehicle parked with the given identifier
            ParkedCarModel parkedCar = parkedCars.Where(parkedVehicle => parkedVehicle.vehicle.id == vehicleId).FirstOrDefault();

            return parkedCar?.vehicle;
        }

        public static string GetParkingLabelText(int type)
        {
            string labelText = string.Empty;
            switch (type)
            {
                case Constants.PARKING_TYPE_PUBLIC:
                    labelText = GenRes.public_parking;
                    break;
                case Constants.PARKING_TYPE_GARAGE:
                    labelText = GenRes.garage;
                    break;
                case Constants.PARKING_TYPE_SCRAPYARD:
                    labelText = GenRes.scrapyard;
                    break;
                case Constants.PARKING_TYPE_DEPOSIT:
                    labelText = GenRes.police_depot;
                    break;
            }
            return labelText;
        }

        public static ParkingModel GetParkingById(int parkingId)
        {
            // Get the parking given an specific identifier
            return parkingList.Where(parkingModel => parkingModel.id == parkingId).FirstOrDefault();
        }

        private static ParkedCarModel GetParkedVehicle(int vehicleId)
        {
            // Get the parked vehicle given an specific identifier
            return parkedCars.Where(parkedCar => parkedCar.vehicle.id == vehicleId).FirstOrDefault();
        }

        private void PlayerParkVehicle(Client player, ParkingModel parking)
        {
            // Get vehicle data
            VehicleModel vehicleModel = new VehicleModel();
            {
                vehicleModel.rotation = player.Vehicle.Rotation;
                vehicleModel.id = player.Vehicle.GetData(EntityData.VEHICLE_ID);
                vehicleModel.model = player.Vehicle.GetData(EntityData.VEHICLE_MODEL);
                vehicleModel.colorType = player.Vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                vehicleModel.firstColor = player.Vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                vehicleModel.secondColor = player.Vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                vehicleModel.pearlescent = player.Vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);
                vehicleModel.faction = player.Vehicle.GetData(EntityData.VEHICLE_FACTION);
                vehicleModel.plate = player.Vehicle.GetData(EntityData.VEHICLE_PLATE);
                vehicleModel.owner = player.Vehicle.GetData(EntityData.VEHICLE_OWNER);
                vehicleModel.price = player.Vehicle.GetData(EntityData.VEHICLE_PRICE);
                vehicleModel.gas = player.Vehicle.GetData(EntityData.VEHICLE_GAS);
                vehicleModel.kms = player.Vehicle.GetData(EntityData.VEHICLE_KMS);

                // Update parking values
                vehicleModel.position = parking.position;
                vehicleModel.dimension = Convert.ToUInt32(parking.id);
                vehicleModel.parking = parking.id;
                vehicleModel.parked = 0;
            }

            // Link vehicle to the parking
            ParkedCarModel parkedCarModel = new ParkedCarModel();
            {
                parkedCarModel.vehicle = vehicleModel;
                parkedCarModel.parkingId = parking.id;
            }

            // Add the vehicle to the parking
            parkedCars.Add(parkedCarModel);

            // Save the vehicle and delete it from the game
            Vehicle vehicle = player.Vehicle;
            player.WarpOutOfVehicle();
            vehicle.Delete();

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Save the vehicle
                    Database.SaveVehicle(vehicleModel);
                });
            });
        }

        [Command(Commands.COM_PARK)]
        public void ParkCommand(Client player)
        {
            if (!player.IsInVehicle)
                return;

            if (player.VehicleSeat != (int)VehicleSeat.Driver)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_driving);
                return;
            }
            else if (player.Vehicle.GetData(EntityData.VEHICLE_TESTING) != null || player.Vehicle.GetData(EntityData.VEHICLE_FACTION) != Constants.FACTION_NONE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_faction_park);
                return;
            }
            else if (player.Vehicle.TraileredBy != null && player.Vehicle.TraileredBy.Exists)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_is_trailered);
                return;
            }
            else
            {
                if (!Vehicles.HasPlayerVehicleKeys(player, player.Vehicle, true) && !Faction.IsPoliceMember(player))
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_car_keys);
                }
                else
                {
                    // Get the closest parking
                    ParkingModel parking = parkingList.Where(p => player.Position.DistanceTo(p.position) < 3.5f).FirstOrDefault();

                    if (parking == null)
                    {
                        // Player's not in any parking
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_near);
                        return;
                    }

                    switch (parking.type)
                    {
                        case Constants.PARKING_TYPE_PUBLIC:
                            if(Vehicles.HasPlayerVehicleKeys(player, player.Vehicle, true))
                            {
                                string message = string.Format(InfoRes.parking_cost, Constants.PRICE_PARKING_PUBLIC);
                                player.SendChatMessage(Constants.COLOR_INFO + message);
                                PlayerParkVehicle(player, parking);
                            }
                            else
                            {
                                // The player doesn't have the keys
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_car_keys);
                            }
                            break;
                        case Constants.PARKING_TYPE_GARAGE:
                            HouseModel house = House.GetHouseById(parking.houseId);
                            if (house == null || House.HasPlayerHouseKeys(player, house) == false)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_garage_access);
                            }
                            else if (GetParkedCarAmount(parking) == parking.capacity)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.parking_full);
                            }
                            else if(!Vehicles.HasPlayerVehicleKeys(player, player.Vehicle, true))
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_car_keys);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.vehicle_garage_parked);
                                PlayerParkVehicle(player, parking);
                            }
                            break;
                        case Constants.PARKING_TYPE_DEPOSIT:
                            if (!Faction.IsPoliceMember(player))
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.vehicle_deposit_parked);
                                PlayerParkVehicle(player, parking);
                            }
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_allowed);
                            break;
                    }
                }
            }
        }

        [Command(Commands.COM_UNPARK, Commands.HLP_UNPARK_COMMAND)]
        public void UnparkCommand(Client player, int vehicleId)
        {
            VehicleModel vehicle = GetParkedVehicleById(vehicleId);

            if (vehicle == null)
            {
                // There's no vehicle with that identifier
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
            }
            else if (!Vehicles.HasPlayerVehicleKeys(player, vehicle, true))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_car_keys);
            }
            else
            {
                // Get the closest parking
                ParkingModel parking = parkingList.Where(p => player.Position.DistanceTo(p.position) < 2.5f).FirstOrDefault();

                if (parking == null)
                {
                    // Player's not in any parking
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_parking_near);
                    return;
                }

                // Check whether the vehicle is in this parking
                if (parking.id != vehicle.parking)
                {
                    // The vehicle is not in this parking
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_this_parking);
                    return;
                }

                int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                switch (parking.type)
                {
                    case Constants.PARKING_TYPE_DEPOSIT:
                        // Remove player's money
                        if (playerMoney >= Constants.PRICE_PARKING_DEPOSIT)
                        {
                            player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - Constants.PRICE_PARKING_DEPOSIT);

                            string message = string.Format(InfoRes.unpark_money, Constants.PRICE_PARKING_DEPOSIT);
                            player.SendChatMessage(Constants.COLOR_INFO + message);
                        }
                        else
                        {
                            string message = string.Format(ErrRes.parking_not_money, Constants.PRICE_PARKING_DEPOSIT);
                            player.SendChatMessage(Constants.COLOR_ERROR + message);
                            return;
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.vehicle_unparked);
                        break;
                }

                // Set the values to unpark the vehicle
                vehicle.dimension = player.Dimension;
                vehicle.position = parking.position;
                vehicle.parking = 0;
                vehicle.parked = 0;
                
                // Recreate the vehicle
                Vehicle newVehicle = Vehicles.CreateIngameVehicle(vehicle);

                // Update parking values
                newVehicle.SetData(EntityData.VEHICLE_DIMENSION, 0);
                newVehicle.SetData(EntityData.VEHICLE_PARKING, 0);
                newVehicle.SetData(EntityData.VEHICLE_PARKED, 0);

                // Unlink from the parking
                parkedCars.Remove(GetParkedVehicle(vehicleId));
            }
        }
    }
}
