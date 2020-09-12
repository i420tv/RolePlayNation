using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;
using System.Drawing;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WiredPlayers_Client.vehicles
{
    class Vehicles : Events.Script
    {
        public static Vehicles instance;

        private Blip vehicleLocationBlip = null;
        private Checkpoint vehicleLocationCheckpoint = null;

        private static bool seatbelt;
        private static float kms = 0.0f;
        public static float gas = 0.0f;
        private static float distance = 0.0f;
        private static float consumed = 0.0f;
        public bool isAccelerating = false;

        public bool isCruiseControlOn = false;

        public float ccSpeed = 0;
        public int calculatedSpeed;
        public int health = 100;
        public static bool isAcc = false;

        private static bool isOutOfGas = false;
        private static bool isReminderOn = true;

        public static Vector3 lastPosition = null;
        public static Vehicle lastVehicle = null;

        public int lastHealth;

        public Vehicles()
        {
            instance = this;

            Events.Add("initializeSpeedometer", InitializeSpeedometerEvent);
            Events.Add("locateVehicle", LocateVehicleEvent);
            Events.Add("removeSpeedometer", RemoveSpeedometerEvent);
            Events.Add("toggleVehicleDoor", ToggleVehicleDoorEvent);
            Events.Add("toggleSeatbelt", ToggleSeatbeltEvent);
            Events.Add("ccActivate", CruiseControlActivate);
            Events.Add("ccDeactivate", CruiseControlDeactivate);


            Events.Add("aircraftSetSpeed", SetAircraftProperties);


        //    Events.OnPlayerLeaveVehicle += PlayerLeaveVehicleEvent;
            Events.OnPlayerEnterCheckpoint += OnPlayerEnterCheckpoint;
            Events.OnEntityStreamIn += EntityStreamInEvent;

            // Initialize the seatbelt state
            Player.LocalPlayer.SetConfigFlag(32, !seatbelt);


            CameraTest();
        }

        public void CameraTest()
        {
            int cameraTo = RAGE.Game.Cam.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", 300, 0, 0 + 2350f, 0, 0, 0, 2, false, 0);
            int cameraFrom = RAGE.Game.Cam.CreateCamWithParams("DEFAULT_SCRIPTED_CAMERA", 500, 500, 500 + 2350f, 0, 0, 0, 2, false, 0);

            RAGE.Game.Cam.SetCamActive(cameraTo, true);
            RAGE.Game.Cam.SetCamFov(cameraTo, 5.0f);
            RAGE.Game.Cam.PointCamAtEntity(cameraTo, Player.LocalPlayer.Handle, 0f, 0f, 0f, true);

            RAGE.Game.Cam.SetCamActiveWithInterp(cameraTo, cameraFrom, 1500, 4, 1);
        }

        public static void SetAircraftProperties(object[] args)
        {
            //mp.players.local.vehicle.setHandling("FINITIALDRIVEFORCE", 10.0)
            //mp.players.local.vehicle.setHandling("FTRACTIONCURVEMAX", 5.0)
            Player.LocalPlayer.Vehicle.SetMaxSpeed(800);
        }
        public void UpdateTextLabelPos(object[] args)
        {

            //TextLabel text = args[0];
        }

        public static void UpdateSpeedometer()
        {
            lastVehicle = Player.LocalPlayer.Vehicle;
            Vector3 currentPosition = lastVehicle.Position;

            if (instance.isCruiseControlOn)
                Player.LocalPlayer.Vehicle.SetMaxSpeed(instance.ccSpeed / 2.236436f);

            if (!instance.isCruiseControlOn)
                Player.LocalPlayer.Vehicle.SetMaxSpeed(500);


            // Get speedometer's data
            Vector3 velocity = lastVehicle.GetVelocity();

            float test = lastVehicle.GetAcceleration();

            instance.health = lastVehicle.GetHealth();
            int maxHealth = lastVehicle.GetMaxHealth();

            int healthPercent = (int)Math.Round((decimal)(Vehicles.instance.health * 100) / maxHealth);

            float speed = lastVehicle.GetSpeed();
            //int speed = (int)Math.Round(Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y + velocity.Z * velocity.Z) * 2.236936);
            speed = speed * 2.236936f;
            instance.calculatedSpeed = (int)speed;

            // Get the distance and consume
            distance = Vector3.Distance(currentPosition, lastPosition);
            consumed = distance * Constants.CONSUME_PER_METER;
            lastPosition = currentPosition;

            bool engineOn = Player.LocalPlayer.Vehicle.GetIsEngineRunning();

            if (speed > 30 && lastVehicle.HasCollidedWithAnything())
            {
                //lastVehicle.SetEngineOn(false, true, false);

                //if (engineOn == true)
                //Events.CallRemote("engineFailed");
            }

            if (instance.lastHealth != instance.health)
            {
                int calculation = instance.lastHealth - instance.health;

                if (calculation < 25)
                {
                    // You didn't crash hard enough.
                    instance.lastHealth = instance.health;
                    return;
                }
                else
                {
                    lastVehicle.SetEngineOn(false, true, false);

                    if (engineOn == true)
                    {
                        if (instance.health > 39)
                        {
                            Events.CallRemote("engineFailed");
                            instance.lastHealth = instance.health;
                        }
                        else
                        {
                            Events.CallRemote("engineFinished");
                            instance.lastHealth = instance.health;
                        }
                    }

                }
            }


            if (lastVehicle.Model == 0x9D80F93)
            {
                if (speed > 130)
                {
                    float newSpeed = speed + 1;
                    if (engineOn)
                        lastVehicle.SetForwardSpeed(newSpeed);
                }
            }

            if (!isOutOfGas)
                if (gas == 0 || gas < 0)
                {
                    // The fuel tank is empty
                    Events.CallRemote("outOfFuel");
                    gas = 0;
                    consumed = 0.0f;
                    isOutOfGas = true;
                }

            if (gas > 5 && gas < 5.1f)
                isReminderOn = true;

            if (isReminderOn)
            {
                if (gas < 5)
                {
                    Events.CallRemote("remindFuel");
                    isReminderOn = false;
                }
            }

            if (gas < 0)
                gas = 0;

            // Get the total gas and kms
            string totalKms = Math.Round((double)(kms + distance) / 10) / 100 + " mi";
            string totalGas = Math.Round((double)(gas - consumed) * 100) / 100 + " liter";

            // Draw the speedometer
            //RAGE.Game.UIText.Draw("Petrol: ", new Point(1075, 560), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
            //RAGE.Game.UIText.Draw(totalGas, new Point(1175, 560), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
          //  RAGE.Game.UIText.Draw("Mileage: ", new Point(1075, 590), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
          //  RAGE.Game.UIText.Draw(totalKms, new Point(1175, 590), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
            //RAGE.Game.UIText.Draw("MPH: ", new Point(1075, 650), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
            //RAGE.Game.UIText.Draw(Vehicles.instance.calculatedSpeed.ToString(), new Point(1175, 650), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);
          //  RAGE.Game.UIText.Draw("Health: ", new Point(1075, 620), 0.45f, Color.White, RAGE.Game.Font.ChaletComprimeCologne, false);

            // Update the vehicle's values
            kms += distance / 1000;

            if (!Vehicles.instance.isAccelerating)
                gas -= consumed;

            // Reinitialize the variables
            distance = 0.0f;
            consumed = 0.0f;

            Events.CallLocal("getFuel", Player.LocalPlayer, gas);
        }

        private void InitializeSpeedometerEvent(object[] args)
        {
            // Initialize the kilometers and gas
            kms = (float)Convert.ToDouble(args[0]);
            gas = (float)Convert.ToDouble(args[1]);

            // Initialize the counters
            distance = 0.0f;
            consumed = 0.0f;
            lastPosition = Player.LocalPlayer.Vehicle.Position;
            lastHealth = lastVehicle.GetHealth();
        }

        public void CruiseControlActivate(object[] args)
        {
            string speed = args[0].ToString();
            int chosenSpeed = int.Parse(speed);
            float convertedSpeed = (float)chosenSpeed;
            instance.ccSpeed = convertedSpeed;

            isCruiseControlOn = true;
        }
        public void CruiseControlDeactivate(object[] args)
        {
            isCruiseControlOn = false;
            Keys.instance.CC = 0;
            instance.ccSpeed = 500;
        }

        private void LocateVehicleEvent(object[] args)
        {
            // Get the variables from the array
            Vector3 position = (Vector3)args[0];

            // Create the blip on the map
            vehicleLocationBlip = new Blip(1, position, string.Empty, 1, 1);
            vehicleLocationCheckpoint = new Checkpoint(4, position, 2.5f, new Vector3(), new RGBA(198, 40, 40, 200));
        }

        public static void RemoveSpeedometerEvent(object[] args)
        {
            if (seatbelt)
            {
                seatbelt = false;
                Events.CallRemote("toggleSeatbelt", seatbelt);
            }

            // Reset the vehicle's position
            lastPosition = null;

            if (lastVehicle != null)
            {
                // Save the kilometers and gas
                Events.CallRemote("saveVehicleConsumes", lastVehicle.RemoteId, kms, gas);

                // Reset the player's vehicle
                lastVehicle = null;
            }
        }

        private void ToggleVehicleDoorEvent(object[] args)
        {
            // Get the values from the server
            int vehicleId = Convert.ToInt32(args[0]);
            int door = Convert.ToInt32(args[1]);
            bool opened = Convert.ToBoolean(args[2]);

            // Get the vehicle from the server
            Vehicle vehicle = Entities.Vehicles.GetAtRemote((ushort)vehicleId);

            if (opened)
            {
                // Open the selected door
                vehicle.SetDoorOpen(door, false, false);
            }
            else
            {
                // Close the selected door
                vehicle.SetDoorShut(door, true);
            }
        }

        private void ToggleSeatbeltEvent(object[] args)
        {
            // Change the seatbelt state
            seatbelt = !seatbelt;
            Player.LocalPlayer.SetConfigFlag(32, !seatbelt);

            // Send the message to the players nearby
            Events.CallRemote("toggleSeatbelt", seatbelt);
        }

        private void PlayerLeaveVehicleEvent()
        {
            if (lastPosition != null)
            {
                // Save and remove the speedometer
                RemoveSpeedometerEvent(null);
            }
        }

        private void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Events.CancelEventArgs cancel)
        {
            if (checkpoint == vehicleLocationCheckpoint)
            {
                // Destroy the checkpoint on the map
                vehicleLocationCheckpoint.Destroy();
                vehicleLocationCheckpoint = null;

                // Destroy the blip on the map
                vehicleLocationBlip.Destroy();
                vehicleLocationBlip = null;
            }
        }

        private void EntityStreamInEvent(Entity entity)
        {
            if (entity.Type == RAGE.Elements.Type.Vehicle)
            {
                // Get the vehicle from the entity
                Vehicle vehicle = (Vehicle)entity;

                // Get the state for each one of the doors
                string doorsJson = entity.GetSharedData(Constants.VEHICLE_DOORS_STATE).ToString();
                List<bool> doorStateList = JsonConvert.DeserializeObject<List<bool>>(doorsJson);

                for (int i = 0; i < doorStateList.Count; i++)
                {
                    if (doorStateList[i])
                    {
                        // Open the selected door
                        vehicle.SetDoorOpen(i, false, false);
                    }
                    else
                    {
                        // Close the selected door
                        vehicle.SetDoorShut(i, true);
                    }
                }

            }
        }
    }
}
