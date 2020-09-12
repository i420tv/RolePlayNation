using RAGE;
using RAGE.Elements;
using Newtonsoft.Json;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using System.Collections.Generic;
using System;

namespace WiredPlayers_Client.cardealer
{
    class CarDealer : Events.Script
    {
        private string carShopVehiclesJson = null;
        private Blip carShopTestBlip = null;
        private Checkpoint carShopTestCheckpoint = null;
        private Vehicle previewVehicle = null;
        private int previewCamera;
        private int dealership;

        public CarDealer()
        {
            Events.Add("showVehicleCatalog", ShowVehicleCatalogEvent);
            Events.Add("previewCarShopVehicle", PreviewCarShopVehicleEvent);
            Events.Add("rotatePreviewVehicle", RotatePreviewVehicleEvent);
            Events.Add("previewVehicleChangeColor", PreviewVehicleChangeColorEvent);
            Events.Add("showCatalog", ShowCatalogEvent);
            Events.Add("closeCatalog", CloseCatalogEvent);
            Events.Add("checkVehiclePayable", CheckVehiclePayableEvent);
            Events.Add("purchaseVehicle", PurchaseVehicleEvent);
            Events.Add("testVehicle", TestVehicleEvent);
            Events.Add("showCarshopCheckpoint", ShowCarshopCheckpointEvent);

            Events.OnPlayerEnterCheckpoint += OnPlayerEnterCheckpoint;
        }

        private void ShowVehicleCatalogEvent(object[] args)
        {
            // Get the variables from the arguments
            carShopVehiclesJson = args[0].ToString();
            dealership = Convert.ToInt32(args[1]);

            // Disable the chat
            Chat.Activate(false);
            Chat.Show(false);

            // Show the catalog
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/vehicleCatalog.html", "populateVehicleList", dealership, carShopVehiclesJson });
        }

        private void PreviewCarShopVehicleEvent(object[] args)
        {
            // Get the variables from the arguments
            string model = args[0].ToString();

            if(previewVehicle != null)
            {
                // Destroy the vehicle
                previewVehicle.Destroy();
            }

            // Destroy the catalog
            Browser.DestroyBrowserEvent(null);

            switch(dealership)
            {
                case 2:
                    previewVehicle = new Vehicle(RAGE.Game.Misc.GetHashKey(model), new Vector3(-878.5726f, -1353.408f, 0.1741f), 90.0f);
                    previewCamera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -882.3361f, -1342.628f, 5.0783f, -20.0f, 0.0f, 200.0f, 90.0f, true, 2);
                    break;
                default:
                    previewVehicle = new Vehicle(RAGE.Game.Misc.GetHashKey(model), new Vector3(-31.98111f, -1090.434f, 26.42225f), 180.0f);
                    previewCamera = RAGE.Game.Cam.CreateCameraWithParams(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), -37.83527f, -1088.096f, 27.92234f, -20.0f, 0.0f, 250, 90.0f, true, 2);
                    break;
            }

            // Make the camera point the vehicle
            RAGE.Game.Cam.SetCamActive(previewCamera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            // Disable the HUD
            RAGE.Game.Ui.DisplayHud(false);
            RAGE.Game.Ui.DisplayRadar(false);

            // Vehicle preview menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/vehiclePreview.html", "checkVehiclePayable" });
        }

        private void RotatePreviewVehicleEvent(object[] args)
        {
            // Get the variables from the arguments
            float rotation = (float)Convert.ToDouble(args[0]);

            // Set the vehicle's heading
            previewVehicle.SetHeading(rotation);
        }

        private void PreviewVehicleChangeColorEvent(object[] args)
        {
            // Get the variables from the arguments
            string colorHex = args[0].ToString().Substring(1);
            bool colorMain = (bool)args[1];

            // Get the RGB from HEX string
            int red = Convert.ToInt32(colorHex.Substring(0, 2), 16);
            int green = Convert.ToInt32(colorHex.Substring(2, 2), 16);
            int blue = Convert.ToInt32(colorHex.Substring(4, 2), 16);

            if (colorMain)
            {
                // Set the vehicle's primary color
                previewVehicle.SetCustomPrimaryColour(red, green, blue);
            }
            else
            {
                // Set the vehicle's secondary color
                previewVehicle.SetCustomSecondaryColour(red, green, blue);
            }
        }

        private void ShowCatalogEvent(object[] args)
        {
            // Destroy preview menu
            Browser.DestroyBrowserEvent(null);

            // Destroy the vehicle
            previewVehicle.Destroy();
            previewVehicle = null;

            // Enable the HUD
            RAGE.Game.Ui.DisplayHud(true);
            RAGE.Game.Ui.DisplayRadar(true);

            // Position the camera behind the character
            RAGE.Game.Cam.DestroyCam(previewCamera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            // Show the catalog
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/vehicleCatalog.html", "populateVehicleList", dealership, carShopVehiclesJson });
        }

        private void CloseCatalogEvent(object[] args)
        {
            // Destroy preview catalog
            Browser.DestroyBrowserEvent(null);

            // Enable the chat
            Chat.Activate(true);
            Chat.Show(true);
        }

        private void CheckVehiclePayableEvent(object[] args)
        {
            // Get the vehicles' list
            List<CarDealerVehicle> vehicleList = JsonConvert.DeserializeObject<List<CarDealerVehicle>>(carShopVehiclesJson);

            foreach (CarDealerVehicle veh in vehicleList)
            {
                if (RAGE.Game.Misc.GetHashKey(veh.model) == previewVehicle.Model)
                {
                    // Check if the player has enough money in the bank
                    int playerBankMoney = (int)Player.LocalPlayer.GetSharedData("PLAYER_BANK");

                    if (playerBankMoney >= veh.price)
                    {
                        // Enable purchase button
                        Browser.ExecuteFunctionEvent(new object[] { "showVehiclePurchaseButton" });
                    }
                    break;
                }
            }
        }

        private void PurchaseVehicleEvent(object[] args)
        {
            // Get the colors variables
            int primaryRed = 0, primaryGreen = 0, primaryBlue = 0;
            int secondaryRed = 0, secondaryGreen = 0, secondaryBlue = 0;

            // Get the vehicle's data
            uint model = previewVehicle.Model;
            previewVehicle.GetCustomPrimaryColour(ref primaryRed, ref primaryGreen, ref primaryBlue);
            previewVehicle.GetCustomSecondaryColour(ref secondaryRed, ref secondaryGreen, ref secondaryBlue);

            // Get color strings
            string firstColor = string.Format("{0},{1},{2}", primaryRed, primaryGreen, primaryBlue);
            string secondColor = string.Format("{0},{1},{2}", secondaryRed, secondaryGreen, secondaryBlue);

            // Destroy preview menu
            CloseCatalogEvent(null);

            // Destroy preview vehicle
            previewVehicle.Destroy();
            previewVehicle = null;

            // Enable the HUD
            RAGE.Game.Ui.DisplayHud(true);
            RAGE.Game.Ui.DisplayRadar(true);

            // Position the camera behind the character
            RAGE.Game.Cam.DestroyCam(previewCamera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            // Purchase the vehicle
            Events.CallRemote("purchaseVehicle", model.ToString(), firstColor, secondColor);
        }

        private void TestVehicleEvent(object[] args)
        { 
            // Get the vehicle's data
            string model = previewVehicle.Model.ToString();

            // Destroy preview menu
            CloseCatalogEvent(null);

            // Destroy preview vehicle
            previewVehicle.Destroy();
            previewVehicle = null;

            // Enable the HUD
            RAGE.Game.Ui.DisplayHud(true);
            RAGE.Game.Ui.DisplayRadar(true);

            // Position the camera behind the character
            RAGE.Game.Cam.DestroyCam(previewCamera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            // Create the vehicle for testing
            Events.CallRemote("testVehicle", model);
        }

        private void ShowCarshopCheckpointEvent(object[] args)
        {
            // Get the variables from the arguments
            Vector3 position = (Vector3)args[0];

            // Add a blip with the delivery place
            carShopTestBlip = new Blip(1, position, string.Empty, 1f, 1);
            carShopTestCheckpoint = new Checkpoint(4, position, 2.5f, new Vector3(), new RGBA(198, 40, 40, 200));
        }

        private void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Events.CancelEventArgs cancel)
        {
            if(checkpoint == carShopTestCheckpoint && Player.LocalPlayer.Vehicle != null)
            {
                // Destroy the checkpoint
                carShopTestCheckpoint.Destroy();
                carShopTestCheckpoint = null;

                // Delete the blip
                carShopTestBlip.Destroy();
                carShopTestBlip = null;

                // Deliver the test vehicle
                Events.CallRemote("deliverTestVehicle");
            }
        }
    }
}
