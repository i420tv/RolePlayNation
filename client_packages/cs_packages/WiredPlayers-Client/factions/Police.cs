using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace WiredPlayers_Client.factions
{
    class Police : Events.Script
    {
        public static bool handcuffed;
        private string crimesJson = null;
        private string  crimesList = null;
        private string selectedControl = null;
        private Dictionary<int, Blip> reinforces = null;

        public Police()
        {
            Events.Add("showCrimesMenu", ShowCrimesMenuEvent);
            Events.Add("applyCrimes", ApplyCrimesEvent);
            Events.Add("executePlayerCrimes", ExecutePlayerCrimesEvent);
            Events.Add("backCrimesMenu", BackCrimesMenuEvent);
            Events.Add("loadPoliceControlList", LoadPoliceControlListEvent);
            Events.Add("proccessPoliceControlAction", ProccessPoliceControlActionEvent);
            Events.Add("policeControlSelectedName", PoliceControlSelectedNameEvent);
            Events.Add("updatePoliceReinforces", UpdatePoliceReinforcesEvent);
            Events.Add("reinforcesRemove", ReinforcesRemoveEvent);

         //   Events.AddDataHandler("PLAYER_HANDCUFFED", PlayerHandcuffedStateChanged);

            Events.OnPlayerStartEnterVehicle += OnPlayerStartEnterVehicle;

            // Initialize the reinforces
            reinforces = new Dictionary<int, Blip>();
        }

        private void ShowCrimesMenuEvent(object[] args)
        {
            // Save crimes list
            crimesJson = args[0].ToString();

            // Show crimes menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateCrimesMenu", crimesJson, string.Empty });
        }

        private void ApplyCrimesEvent(object[] args)
        {
            // Store crimes to be applied
            crimesList = args[0].ToString();

            // Destroy crimes menu
            Browser.DestroyBrowserEvent(null);

            // Show the confirmation window
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/crimesConfirm.html", "populateCrimesConfirmMenu", crimesList });
        }

        private void ExecutePlayerCrimesEvent(object[] args)
        {
            // Destroy the confirmation menu
            Browser.DestroyBrowserEvent(null);

            // Apply crimes to the player
            Events.CallRemote("applyCrimesToPlayer", crimesList);
        }

        private void BackCrimesMenuEvent(object[] args)
        {
            // Destroy the confirmation menu
            Browser.DestroyBrowserEvent(null);

            // Show crimes menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateCrimesMenu", crimesJson, crimesList });
        }

        private void LoadPoliceControlListEvent(object[] args)
        {
            // Show the menu with the police control list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populatePoliceControlMenu", args[0].ToString() });
        }

        private void ProccessPoliceControlActionEvent(object[] args)
        {
            // Get the variables from the arguments
            string control = args[0] == null ? string.Empty : args[0].ToString();

            // Check the selected option
            int controlOption = (int)Player.LocalPlayer.GetSharedData("PLAYER_POLICE_CONTROL");

            switch (controlOption)
            {
                case 1:
                    if (control.Length == 0)
                    {
                        // Save the police control with a new name
                        Browser.CreateBrowserEvent(new object[] { "package://statics/html/policeControlName.html" });
                    }
                    else
                    {
                        // Override the existing police control
                        Events.CallRemote("policeControlSelected", control);
                    }
                    break;
                case 2:
                    // Show the window to change control's name
                    Browser.CreateBrowserEvent(new object[] { "package://statics/html/policeControlName.html" });
                    selectedControl = control;
                    break;
                default:
                    // Execute the option over the police control
                    Events.CallRemote("policeControlSelected", control);
                    break;
            }
        }

        private void PoliceControlSelectedNameEvent(object[] args)
        {
            // Save the police control with a new name
            Events.CallRemote("updatePoliceControlName", selectedControl, args[0].ToString());
        }

        private void UpdatePoliceReinforcesEvent(object[] args)
        {
            List<Reinforces> updatedReinforces = JsonConvert.DeserializeObject<List<Reinforces>>(args[0].ToString());

            // Search for policemen asking for reinforces
            foreach(Reinforces reinforcesModel in updatedReinforces)
            {
                // Get the identifier
                int police = reinforcesModel.playerId;
                Vector3 position = reinforcesModel.position;

                if(reinforces.ContainsKey(police))
                {
                    // Update the blip's position
                    reinforces[police].SetCoords(position.X, position.Y, position.Z);
                }
                else
                {
                    // Create a blip on the map
                    Blip reinforcesBlip = new Blip(487, position, string.Empty, 1, 38);

                    // Add the new member to the array
                    reinforces[police] = reinforcesBlip;
                }
            }
        }

        private void ReinforcesRemoveEvent(object[] args)
        {
            // Get the variables from the arguments
            int officer = Convert.ToInt32(args[0]);

            // Delete officer's reinforces
            reinforces[officer].Destroy();
            reinforces.Remove(officer);
        }

        private void PlayerHandcuffedStateChanged(Entity entity, object arg)
        {
            if(entity == Player.LocalPlayer)
            {
                // Toggle the handcuffed state
                handcuffed = arg != null;
            }
        }

        private void OnPlayerStartEnterVehicle(Vehicle vehicle, int seatId, Events.CancelEventArgs cancel)
        {
            if(handcuffed && seatId == Constants.VEHICLE_SEAT_DRIVER)
            {
                // Prevent the player from driving the vehicle
                cancel.Cancel = true;
            }
        }
    }
}
