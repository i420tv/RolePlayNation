using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using WiredPlayers_Client.model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace WiredPlayers_Client.character
{
    class Character : Events.Script
    {
        private int camera;
        private string characters = null;
        private PlayerModel playerData = null;

        public Character()
        {
            Events.Add("showPlayerCharacters", ShowPlayerCharactersEvent);
            Events.Add("loadCharacter", LoadCharacterEvent);
            Events.Add("showCharacterCreationMenu", ShowCharacterCreationMenuEvent); 
            Events.Add("changePlayerModel", ChangePlayerModelEvent);
            Events.Add("changePlayerSex", ChangePlayerSexEvent);
            Events.Add("getDefaultSkins", GetDefaultSkinsEvent);
            Events.Add("showDefaultSkins", ShowDefaultSkinsEvent);
            Events.Add("storePlayerData", StorePlayerDataEvent);
            Events.Add("cameraPointTo", CameraPointToEvent);
            Events.Add("rotateCharacter", RotateCharacterEvent);
            Events.Add("selectDefaultCharacter", SelectDefaultCharacterEvent);
            Events.Add("characterNameDuplicated", CharacterNameDuplicatedEvent);
            Events.Add("acceptCharacterCreation", AcceptCharacterCreationEvent);
            Events.Add("cancelCharacterCreation", CancelCharacterCreationEvent);
            Events.Add("characterCreatedSuccessfully", CharacterCreatedSuccessfullyEvent);
            Events.Add("loadIntoWorld", LoadIntoWorld);
            Events.Add("destroyLastBrowser", DestroyLastBrowser);
        }

        private void ShowPlayerCharactersEvent(object[] args)
        {
            // Store account characters
            characters = args[0].ToString();          

            // Show character list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateCharacterList", characters });
        }

        private void LoadIntoWorld(object[] args)
        {
            //characters = args[0].ToString();

            // if (characters[0] == null)
            //  return;
            RAGE.Game.Ui.DisplayRadar(true);
            RAGE.Game.Ui.DisplayHud(true);
            Chat.Activate(true);
            Chat.Show(true);
            Events.CallRemote("checkPlayerEventKey");
            /// LEAVE LOBBY AND ENTER THE WORLD \\
        }

        private void DestroyLastBrowser(object[] args)
        {
            Browser.DestroyBrowserEvent(null);
        }

        private void LoadCharacterEvent(object[] args)
        {
            // Get the variables from the array
            string characterName = args[0].ToString();

            if (characterName == null)
                return;
            // Destroy the menu
            //Browser.DestroyBrowserEvent(null);

            // Show character list
            Events.CallRemote("loadCharacter", characterName);
        }

        private void ShowCharacterCreationMenuEvent(object[] args)
        {
            // Destroy the menu
            Browser.DestroyBrowserEvent(null);

            // Initialize the character creation
            playerData = new PlayerModel();
            ApplyPlayerModelChanges();
            Events.CallRemote("switchCameras", Player.LocalPlayer);
            // Set the character into the creator menu
            Events.CallRemote("setCharacterIntoCreator");

               Player.LocalPlayer.Position = new Vector3(-2088.9587f, -1016.64075f, 8.971191f);
               Player.LocalPlayer.SetRotation(0, 0, 260, 0, true);
            

            // Disable the interface
            RAGE.Game.Ui.DisplayRadar(false);
            RAGE.Game.Ui.DisplayHud(false);
            Chat.Activate(false);
            Chat.Show(false);

            // Load the character creation menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/characterCreator.html" });
        }

        private void ChangePlayerModelEvent(object[] args)
        {
            // Get the model
            int model = Convert.ToInt32(args[0]);

            if(model == 0)
            {
                // Set the default player model
                playerData.model = "mp_m_freemode_01";
            }
            else
            {
                // Set the custom player model
                playerData.model = playerData.sex == Constants.SEX_MALE ? "mp_m_freemode_01" : "mp_f_freemode_01";
            }

            // Set the player model
            Player.LocalPlayer.Model = RAGE.Game.Misc.GetHashKey(playerData.model);

            // Update the model changes
            ApplyPlayerModelChanges();

            // Make the character idle
            Events.CallRemote("playIdleCreatorAnimation");
        }

        private void ChangePlayerSexEvent(object[] args)
        {
            // Store the value into the object
            playerData.sex = Convert.ToInt32(args[0]);

            if (playerData.model != "mp_m_freemode_01" && playerData.model != "mp_f_freemode_01")
            {
                // Set the default player model
                playerData.model = "mp_m_freemode_01";
            }
            else
            {
                // Set the custom player model
                playerData.model = playerData.sex == Constants.SEX_MALE ? "mp_m_freemode_01" : "mp_f_freemode_01";
            }

            // Set the player model
            Player.LocalPlayer.Model = RAGE.Game.Misc.GetHashKey(playerData.model);

            // Update the model changes
            ApplyPlayerModelChanges();

            // Make the character idle
            Events.CallRemote("playIdleCreatorAnimation");
        }

        private void GetDefaultSkinsEvent(object[] args)
        {
            // Get the skins corresponding to the sex
           // Events.CallRemote("getDefaultSkins", playerData.sex);
        }

        private void ShowDefaultSkinsEvent(object[] args)
        {
            // Show the skins on the browser
          //  Browser.ExecuteFunctionEvent(new object[] { "populateDefaultSkins", args[0].ToString() });
        }

        private void StorePlayerDataEvent(object[] args)
        {
            // Get the object from the JSON string
            Dictionary<string, object> dataObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(args[0].ToString());

            foreach (KeyValuePair<string, object> keyValue in dataObject)
            {
                // Set the value into the player data object
                PropertyInfo property = playerData.GetType().GetProperty(keyValue.Key);
                property.SetValue(playerData, Convert.ChangeType(keyValue.Value, property.PropertyType));
            }

            // Update the model changes
            ApplyPlayerModelChanges();
        }

        private void CameraPointToEvent(object[] args)
        {
            // Get the variables from the array
            int bodyPart = Convert.ToInt32(args[0]);

            if (bodyPart == 0)
            {
                // Make the camera point to the body
                RAGE.Game.Cam.SetCamCoord(camera, 402.8974f, -998.756f, -98.25f);
            }
            else
            {
                // Make the camera point to the face
                RAGE.Game.Cam.SetCamCoord(camera, -2087.432f, -1015.809f, 10.69179f);
            }
        }

        private void RotateCharacterEvent(object[] args)
        {
            // Get the variables from the array
            int rotation = Convert.ToInt32(args[0]);

            // Rotate the character
            Player.LocalPlayer.SetHeading(rotation);
        }

        private void SelectDefaultCharacterEvent(object[] args)
        {
            // Get the new model for the character
            playerData.model = args[0].ToString();

            // Set the player model
            Player.LocalPlayer.Model = RAGE.Game.Misc.GetHashKey(playerData.model);

            // Make the character idle
            Events.CallRemote("playIdleCreatorAnimation");
        }

        private void CharacterNameDuplicatedEvent(object[] args)
        {
            // Duplicated name
            Browser.ExecuteFunctionEvent(new object[] { "showPlayerDuplicatedWarn" });
        }

        private void AcceptCharacterCreationEvent(object[] args)
        {
            // Get the variables from the array
            string name = args[0].ToString();
            int age = Convert.ToInt32(args[1]);

            // Create the new character
            string skinJson = JsonConvert.SerializeObject(playerData);
            Events.CallRemote("createCharacter", name, playerData.model, age, playerData.sex, skinJson);
        }

        private void CancelCharacterCreationEvent(object[] args)
        {
            // Destroy the browser
            Browser.DestroyBrowserEvent(null);

            //CharacterCreatedSuccessfullyEvent(null);

            // Miramos el número de personajes
            List<string> characterNames = JsonConvert.DeserializeObject<List<string>>(characters);

            if (characterNames.Count > 0)
            {
                // Add clothes and tattoos if the player has any character
                Events.CallRemote("loadCharacter", Player.LocalPlayer.Name);
            }
            else
            {
                Events.CallRemote("goBackToCharacterList", Player.LocalPlayer);
            }

            // Show the character list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateCharacterList", characters });
            RAGE.Game.Ui.DisplayRadar(false);
            RAGE.Game.Ui.DisplayHud(false);
            Chat.Activate(false);
            Chat.Show(false);
        }

        private void CharacterCreatedSuccessfullyEvent(object[] args)
        {
            // Get the default camera
            RAGE.Game.Cam.DestroyCam(camera, true);
            RAGE.Game.Cam.RenderScriptCams(false, false, 0, true, false, 0);

            // Enable the interface
            RAGE.Game.Ui.DisplayRadar(true);
            RAGE.Game.Ui.DisplayHud(true);
            Chat.Activate(true);
            Chat.Show(true);

            // Destroy character creation menu
            Browser.DestroyBrowserEvent(null);

            Events.CallRemote("checkPlayerEventKey");
        }

        private void ApplyPlayerModelChanges()
        {
            // Apply the changes to the player
            Player.LocalPlayer.SetHeadBlendData(playerData.firstHeadShape, playerData.secondHeadShape, 0, playerData.firstSkinTone, playerData.secondSkinTone, 0, playerData.headMix, playerData.skinMix, 0, false);
            Player.LocalPlayer.SetComponentVariation(2, playerData.hairModel, 0, 0);
            Player.LocalPlayer.SetHairColor(playerData.firstHairColor, playerData.secondHairColor);
            Player.LocalPlayer.SetEyeColor(playerData.eyesColor);
            Player.LocalPlayer.SetHeadOverlay(1, playerData.beardModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlayColor(1, 1, playerData.beardColor, 0);
            Player.LocalPlayer.SetHeadOverlay(10, playerData.chestModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlayColor(10, 1, playerData.chestColor, 0);
            Player.LocalPlayer.SetHeadOverlay(2, playerData.eyebrowsModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlayColor(2, 1, playerData.eyebrowsColor, 0);
            Player.LocalPlayer.SetHeadOverlay(5, playerData.blushModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlayColor(5, 2, playerData.blushColor, 0);
            Player.LocalPlayer.SetHeadOverlay(8, playerData.lipstickModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlayColor(8, 2, playerData.lipstickColor, 0);
            Player.LocalPlayer.SetHeadOverlay(0, playerData.blemishesModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlay(3, playerData.ageingModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlay(6, playerData.complexionModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlay(7, playerData.sundamageModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlay(9, playerData.frecklesModel, 1.0f);
            Player.LocalPlayer.SetHeadOverlay(4, playerData.makeupModel, 1.0f);
            Player.LocalPlayer.SetFaceFeature(0, playerData.noseWidth);
            Player.LocalPlayer.SetFaceFeature(1, playerData.noseHeight);
            Player.LocalPlayer.SetFaceFeature(2, playerData.noseLength);
            Player.LocalPlayer.SetFaceFeature(3, playerData.noseBridge);
            Player.LocalPlayer.SetFaceFeature(4, playerData.noseTip);
            Player.LocalPlayer.SetFaceFeature(5, playerData.noseShift);
            Player.LocalPlayer.SetFaceFeature(6, playerData.browHeight);
            Player.LocalPlayer.SetFaceFeature(7, playerData.browWidth);
            Player.LocalPlayer.SetFaceFeature(8, playerData.cheekboneHeight);
            Player.LocalPlayer.SetFaceFeature(9, playerData.cheekboneWidth);
            Player.LocalPlayer.SetFaceFeature(10, playerData.cheeksWidth);
            Player.LocalPlayer.SetFaceFeature(11, playerData.eyes);
            Player.LocalPlayer.SetFaceFeature(12, playerData.lips);
            Player.LocalPlayer.SetFaceFeature(13, playerData.jawWidth);
            Player.LocalPlayer.SetFaceFeature(14, playerData.jawHeight);
            Player.LocalPlayer.SetFaceFeature(15, playerData.chinLength);
            Player.LocalPlayer.SetFaceFeature(16, playerData.chinPosition);
            Player.LocalPlayer.SetFaceFeature(17, playerData.chinWidth);
            Player.LocalPlayer.SetFaceFeature(18, playerData.chinShape);
            Player.LocalPlayer.SetFaceFeature(19, playerData.neckWidth);
        }
    }
}
