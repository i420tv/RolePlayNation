using RAGE;
using RAGE.Elements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using System.Diagnostics;

namespace WiredPlayers_Client.jobs
{
    class Mechanic : Events.Script
    {
        public Mechanic()
        {
            Events.Add("showTunningMenu", ShowTunningMenuEvent);
            Events.Add("showCatalogMenu", ShowCatalogMenuEvent);
            Events.Add("showRepaintMenu", ShowRepaintMenuEvent);
            Events.Add("addVehicleComponent", AddVehicleComponentEvent);
            Events.Add("confirmVehicleModification", ConfirmVehicleModificationEvent);
            Events.Add("cancelVehicleModification", CancelVehicleModificationEvent); 
            Events.Add("repaintVehicle", RepaintVehicleEvent);
            Events.Add("closeRepaintWindow", CloseRepaintWindowEvent);
        }
        private void ShowCatalogMenuEvent(object[] args)
        {
            // Initialize the list
            List<CarPiece> vehicleComponents = new List<CarPiece>();

            foreach (CarPiece pieceGroup in Constants.CAR_PIECE_LIST)
            {
                // Get the number of mods for each component group
                int modNumber = Player.LocalPlayer.Vehicle.GetNumMods(pieceGroup.slot);

                if (modNumber > 0)
                {
                    // Initialize the components list
                    pieceGroup.components = new List<CarPiece>();

                    for (int i = 0; i < modNumber; i++)
                    {
                        // Create the component
                        CarPiece piece = new CarPiece(i, pieceGroup.desc + " " + (i + 1));
                        pieceGroup.components.Add(piece);
                    }

                    // Add all the pieces to the list
                    vehicleComponents.Add(pieceGroup);
                }
            }

            // Show the tunning menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/TuningCatalog.html", "populateTunningMenuShowcase", JsonConvert.SerializeObject(vehicleComponents) });
        }
        private void ShowTunningMenuEvent(object[] args)
        {
            // Initialize the list
            List<CarPiece> vehicleComponents = new List<CarPiece>();

            foreach(CarPiece pieceGroup in Constants.CAR_PIECE_LIST) 
            {
                // Get the number of mods for each component group
                int modNumber = Player.LocalPlayer.Vehicle.GetNumMods(pieceGroup.slot);

                if(modNumber > 0)
                {
                    // Initialize the components list
                    pieceGroup.components = new List<CarPiece>();

                    for(int i = 0; i < modNumber; i++)
                    {
                        // Create the component
                        CarPiece piece = new CarPiece(i, pieceGroup.desc + " " + (i + 1));
                        pieceGroup.components.Add(piece);
                    }

                    // Add all the pieces to the list
                    vehicleComponents.Add(pieceGroup);
                }
            }

            // Show the tunning menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/TuningMenu.html", "populateTunningMenu", JsonConvert.SerializeObject(vehicleComponents) });
        }

        private void ShowRepaintMenuEvent(object[] args)
        {
            // Disable the HUD
            RAGE.Game.Ui.DisplayHud(false);
            RAGE.Game.Ui.DisplayRadar(false);

            // Show the paint menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/repaintVehicle.html" });
        }

        private void AddVehicleComponentEvent(object[] args)
        {
            // Get the variables from the array
            int slot = Convert.ToInt32(args[0]);
            int component = Convert.ToInt32(args[1]);

            // Añadimos el componente al vehículo
            Player.LocalPlayer.Vehicle.SetMod(slot, component, false);
        }

        private void ConfirmVehicleModificationEvent(object[] args)
        {
            // Get the variables from the array
            int slot = Convert.ToInt32(args[0]);
            int mod = Convert.ToInt32(args[1]);

            // Add the tunning to the vehicle
            Events.CallRemote("confirmVehicleModification", slot, mod);
        }

        private void CancelVehicleModificationEvent(object[] args)
        {
            // Clear the tunning from the vehicle
            Events.CallRemote("cancelVehicleModification");
        }

        private void RepaintVehicleEvent(object[] args)
        {
            // Get the variables from the array
            int colorType = Convert.ToInt32(args[0]);
            string firstColor = args[1].ToString();
            string secondColor = args[2].ToString();
            int pearlescentColor = Convert.ToInt32(args[3]);
            int paid = Convert.ToInt32(args[4]);

            // Repaint the vehicle
            Events.CallRemote("repaintVehicle", colorType, firstColor, secondColor, pearlescentColor, paid);
        }

        private void CloseRepaintWindowEvent(object[] args)
        {
            // Enable the HUD
            RAGE.Game.Ui.DisplayHud(true);
            RAGE.Game.Ui.DisplayRadar(true);

            // Destroy the browser
            Browser.DestroyBrowserEvent(null);

            // Restore the vehicle's colors
            Events.CallRemote("cancelVehicleRepaint");
        }
    }
}
