using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace WiredPlayers_Client.house
{
    class House : Events.Script
    {
        private int customCamera;
        private List<ClothesModel> clothes = null;

        public House()
        {
            Events.Add("showPlayerWardrobe", ShowPlayerWardrobeEvent);
            Events.Add("getPlayerPurchasedClothes", GetPlayerPurchasedClothesEvent);
            Events.Add("showPlayerClothes", ShowPlayerClothesEvent);
            Events.Add("previewPlayerClothes", PreviewPlayerClothesEvent);
            Events.Add("changePlayerClothes", ChangePlayerClothesEvent);
            Events.Add("clearWardrobeClothes", ClearWardrobeClothesEvent); 
            Events.Add("closeWardrobeMenu", CloseWardrobeMenuEvent); 
        }

        private void ShowPlayerWardrobeEvent(object[] args)
        {
            // Create a custom camera
            float forwardX = Player.LocalPlayer.Position.X + (Player.LocalPlayer.GetForwardX() * 1.5f);
            float forwardY = Player.LocalPlayer.Position.Y + (Player.LocalPlayer.GetForwardY() * 1.5f);
            customCamera = RAGE.Game.Cam.CreateCamera(RAGE.Game.Misc.GetHashKey("DEFAULT_SCRIPTED_CAMERA"), true);
            RAGE.Game.Cam.SetCamCoord(customCamera, forwardX, forwardY, Player.LocalPlayer.Position.Z + 0.5f);
            RAGE.Game.Cam.PointCamAtCoord(customCamera, Player.LocalPlayer.Position.X, Player.LocalPlayer.Position.Y, Player.LocalPlayer.Position.Z);

            // Enable the camera
            RAGE.Game.Cam.SetCamActive(customCamera, true);
            RAGE.Game.Cam.RenderScriptCams(true, false, 0, true, false, 0);

            // Show wardrobe's menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateWardrobeMenu", JsonConvert.SerializeObject(Constants.CLOTHES_TYPES), args[0].ToString() });
        }

        private void GetPlayerPurchasedClothesEvent(object[] args)
        {
            // Get the variables from the array
            int slot = Convert.ToInt32(args[0]);

            // Get the player's clothes
            Events.CallRemote("getPlayerPurchasedClothes", 0, slot);
        }

        private void ShowPlayerClothesEvent(object[] args)
        {
            // Get the variables from the array
            List<string> clothesNames = JsonConvert.DeserializeObject<List<string>>(args[1].ToString());
            clothes = JsonConvert.DeserializeObject<List<ClothesModel>>(args[0].ToString());

            for(int i = 0; i < clothesNames.Count; i++)
            {
                // Add the name for each clothes
                clothes[i].description = clothesNames[i];
            }

            // Show clothes of the selected type
            Browser.ExecuteFunctionEvent(new object[] { "populateWardrobeClothes", JsonConvert.SerializeObject(clothes) });
        }

        private void PreviewPlayerClothesEvent(object[] args)
        {
            // Get the variables from the array
            int index = Convert.ToInt32(args[0]);

            if (clothes[index].type == 0)
            {
                // Change player's clothes
                Player.LocalPlayer.SetComponentVariation(clothes[index].slot, clothes[index].drawable, clothes[index].texture, 0);
            }
            else
            {
                // Change player's accessory
                Player.LocalPlayer.SetPropIndex(clothes[index].slot, clothes[index].drawable, clothes[index].texture, true);
            }
        }

        private void ChangePlayerClothesEvent(object[] args)
        {
            // Get the variables from the array
            int slot = Convert.ToInt32(args[0]);
            int index = Convert.ToInt32(args[1]);

            // Equip the clothes
            Events.CallRemote("wardrobeClothesItemSelected", slot, index);
        }

        private void ClearWardrobeClothesEvent(object[] args)
        {
            // Get the variables from the arguments
            int slot = Convert.ToInt32(args[0]) + 1;

            // Clear the not purchased clothes
            Events.CallRemote("dressEquipedClothes", 0, slot);
        }

        private void CloseWardrobeMenuEvent(object[] args)
        {
            // Make the default camera active
            RAGE.Game.Cam.DestroyCam(customCamera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            // Destroy the browser
            Browser.DestroyBrowserEvent(null);
        }
    }
}
