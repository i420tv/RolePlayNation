using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.drivingschool;
using WiredPlayers.messages.error;
using System.Linq;
using System;

namespace WiredPlayers.jobs
{
    public class Taxi : Script
    {
        [ServerEvent(Event.PlayerEnterVehicle)]
        public void PlayerEnterVehicleEvent(Client player, Vehicle vehicle, sbyte seat)
        {
            if(vehicle.Model == (uint)VehicleHash.Taxi && seat == (sbyte)VehicleSeat.Driver)
            {
                // Check if the player has a taxi driver license
                if(DrivingSchool.GetPlayerLicenseStatus(player, Constants.LICENSE_TAXI) == -1)
                {
                    player.WarpOutOfVehicle();
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_taxi_license);
                }
            }
        }

        [RemoteEvent("requestTaxiDestination")]
        public void RequestTaxiDestinationEvent(Client player, Vector3 position)
        {
            // Check if there's someone driving the taxi
            Client driver = player.Vehicle.Occupants.Where(d => d.VehicleSeat == (int)VehicleSeat.Driver).FirstOrDefault();

            if(driver == null)
            {
                // Nobody's driving the vehicle
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_taxi_driver);
                return;
            }

            if(driver.GetData(EntityData.PLAYER_TAXI_PATH) != null)
            {
                // There's already a path set for the driver
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.taxi_has_path);
                return;
            }

            // Join driver and client
            player.SetData(EntityData.PLAYER_JOB_PARTNER, driver);
            driver.SetData(EntityData.PLAYER_JOB_PARTNER, player);

            // Create the path for the driver
            driver.TriggerEvent("createTaxiPath", position);
        }

        [RemoteEvent("taxiDestinationReached")]
        public void TaxiDestinationReachedEvent(Client player)
        {
            // Get the customer
            Client customer = player.GetData(EntityData.PLAYER_JOB_PARTNER);

            // Remove the link between players
            player.ResetData(EntityData.PLAYER_JOB_PARTNER);
            customer.ResetData(EntityData.PLAYER_JOB_PARTNER);

            // Make the payment
            int amount = 500;
            int customerMoney = customer.GetSharedData(EntityData.PLAYER_MONEY) - amount;
            
            if(customerMoney < 0)
            {
                amount = Math.Abs(customerMoney);
                customerMoney = 0;

                // Get the remaining money from the bank account
                customer.SetData(EntityData.PLAYER_BANK, customer.GetData(EntityData.PLAYER_BANK) - amount);
            }

            // Remove customer's money and give to the driver
            customer.SetSharedData(EntityData.PLAYER_MONEY, customerMoney);
            player.SetSharedData(EntityData.PLAYER_MONEY, player.GetSharedData(EntityData.PLAYER_MONEY) + 500);
        }
    }
}
