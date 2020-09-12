using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.house;
using WiredPlayers.vehicles;
using WiredPlayers.parking;
using WiredPlayers.messages.general;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WiredPlayers.character
{
    public class PlayerData : Script
    {
        [RemoteEvent("retrieveBasicData")]
        public static void RetrieveBasicDataEvent(Client player, int targetId)
        {
            // Get the target player
            Client target = NAPI.Pools.GetAllPlayers().Find(p => p.Value == targetId);

            // Get the basic data
            string age = target.GetData(EntityData.PLAYER_AGE) + GenRes.years;
            string sex = target.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? GenRes.sex_male : GenRes.sex_female;
            string money = target.GetSharedData(EntityData.PLAYER_MONEY) + "$";
            string bank = target.GetSharedData(EntityData.PLAYER_BANK) + "$";
            string job = GenRes.unemployed;
            string rank = string.Empty;

            // Get the job
            JobModel jobModel = Constants.JOB_LIST.Where(j => target.GetData(EntityData.PLAYER_JOB) == j.job).First();

            if (jobModel.job == 0)
            {
                // Get the player's faction
                FactionModel factionModel = Constants.FACTION_RANK_LIST.Where(f => target.GetData(EntityData.PLAYER_FACTION) == f.faction && target.GetData(EntityData.PLAYER_RANK) == f.rank).FirstOrDefault();

                if (factionModel != null && factionModel.faction > 0)
                {
                    switch (factionModel.faction)
                    {
                        case Constants.FACTION_POLICE:
                            job = GenRes.police_faction;
                            break;
                        case Constants.FACTION_EMERGENCY:
                            job = GenRes.emergency_faction;
                            break;
                        case Constants.FACTION_NEWS:
                            job = GenRes.news_faction;
                            break;
                        case Constants.FACTION_TOWNHALL:
                            job = GenRes.townhall_faction;
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            job = GenRes.transport_faction;
                            break;
                        case Constants.FACTION_SHERIFF:
                            job = GenRes.sheriff_faction;
                            break;
                    }

                    // Set player's rank
                    rank = target.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? factionModel.descriptionMale : factionModel.descriptionFemale;
                }
            }
            else
            {
                // Set the player's job
                job = target.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? jobModel.descriptionMale : jobModel.descriptionFemale;
            }

            // Show the data for the player
            player.TriggerEvent("showPlayerData", target.Value, target.Name, age, sex, money, bank, job, rank, player == target || player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_NONE);
        }

        [RemoteEvent("retrievePropertiesData")]
        public static void RetrievePropertiesDataEvent(Client player, int targetId)
        {
            // Initialize the variables
            List<string> houseAddresses = new List<string>();
            string rentedHouse = string.Empty;

            // Get the target player
            Client target = NAPI.Pools.GetAllPlayers().Find(p => p.Value == targetId);

            // Get the houses where the player is the owner
            List<HouseModel> houseList = House.houseList.Where(h => h.owner == target.Name).ToList();

            foreach (HouseModel house in houseList)
            {
                // Add the name of the house to the list
                houseAddresses.Add(house.name);
            }

            if (target.GetData(EntityData.PLAYER_RENT_HOUSE) > 0)
            {
                // Get the name of the rented house
                int houseId = target.GetData(EntityData.PLAYER_RENT_HOUSE);
                HouseModel rentedHouseModel = House.houseList.Where(h => h.id == houseId).FirstOrDefault();
                rentedHouse = rentedHouseModel == null ? string.Empty : rentedHouseModel.name; 
            }

            // Show the data for the player
            player.TriggerEvent("showPropertiesData", NAPI.Util.ToJson(houseAddresses), rentedHouse);
        }

        [RemoteEvent("retrieveVehiclesData")]
        public static void RetrieveVehiclesDataEvent(Client player, int targetId)
        {
            // Initialize the variables
            List<string> ownedVehicles = new List<string>();
            List<string> lentVehicles = new List<string>();

            // Get the target player
            Client target = NAPI.Pools.GetAllPlayers().Find(p => p.Value == targetId);

            // Get the vehicles in the game
            List<Vehicle> vehicles = NAPI.Pools.GetAllVehicles().Where(v => Vehicles.HasPlayerVehicleKeys(target, v, true)).ToList();
            List<ParkedCarModel> parkedVehicles = Parking.parkedCars.Where(v => Vehicles.HasPlayerVehicleKeys(target, v.vehicle, true)).ToList();
            
            foreach (Vehicle vehicle in vehicles)
            {
                // Get the vehicle name
                string vehicleName = ((VehicleHash)vehicle.Model).ToString() + " LS-" + (vehicle.GetData(EntityData.VEHICLE_ID) + 1000);

                if (vehicle.GetData(EntityData.VEHICLE_OWNER) == target.Name)
                {
                    // Add the the owned vehicles
                    ownedVehicles.Add(vehicleName);
                }
                else
                {
                    // Add the the lent vehicles
                    lentVehicles.Add(vehicleName);
                }
            }

            foreach (ParkedCarModel parkedVehicle in parkedVehicles)
            {
                // Get the vehicle name
                string vehicleName = ((VehicleHash)parkedVehicle.vehicle.model).ToString() + " LS-" + (parkedVehicle.vehicle.id + 1000);

                if (parkedVehicle.vehicle.owner == target.Name)
                {
                    // Add the the owned vehicles
                    ownedVehicles.Add(vehicleName);
                }
                else
                {
                    // Add the the lent vehicles
                    lentVehicles.Add(vehicleName);
                }
            }

            // Show the data for the player
            player.TriggerEvent("showVehiclesData", NAPI.Util.ToJson(ownedVehicles), NAPI.Util.ToJson(lentVehicles));
        }

        [RemoteEvent("retrieveExtendedData")]
        public static void RetrieveExtendedDataEvent(Client player, int targetId)
        {
            // Get the target player
            Client target = NAPI.Pools.GetAllPlayers().Find(p => p.Value == targetId);

            // Get the played time
            TimeSpan played = TimeSpan.FromMinutes(player.GetData(EntityData.PLAYER_PLAYED));
            string playedTime = Convert.ToInt32(played.TotalHours) + "h " + Convert.ToInt32(played.Minutes) + "m";

            // Show the data for the player
            player.TriggerEvent("showExtendedData", playedTime);
        }

        [Command(Commands.COM_PLAYER, Alias = Commands.COM_PLAYER_ALIAS)]
        public void PlayerCommand(Client player)
        {
            // Get players basic data
            RetrieveBasicDataEvent(player, player.Value);
        }
    }
}
