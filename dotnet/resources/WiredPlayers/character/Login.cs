using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace WiredPlayers.character
{
    public class Login : Script
    {     
        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Client player)
        {            
            // Set the default skin and transparency
            //NAPI.Player.SetPlayerSkin(player, PedHash.Strperf01SMM);
            player.Transparency = 0;

            // Initialize the player data
            Character.InitializePlayerData(player);

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    AccountModel account = Database.GetAccount(player.SocialClubName);

                    switch (account.status)
                    {
                        case -1:
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.account_disabled);
                            player.Kick(InfoRes.account_disabled);
                            break;
                        case 0:
                            // Check if the account is registered or not
                            player.TriggerEvent(account.registered ? "accountLoginForm" : "showRegisterWindow");
                            break;
                        default:                        

                            if (account.lastCharacter > 0)
                            {
                                // Load selected character
                                PlayerModel character = Database.LoadCharacterInformationById(account.lastCharacter);
                                SkinModel skinModel = Database.GetCharacterSkin(account.lastCharacter);

                                player.Name = character.realName;
                                player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
                                player.SetSkin(NAPI.Util.GetHashKey(character.model));
                                
                                Character.LoadCharacterData(player, character);

                                if(Customization.IsCustomCharacter(player))
                                {
                                    // Load the clothes, tattoos and customization
                                    Customization.ApplyPlayerCustomization(player, skinModel, character.sex);
                                    Customization.ApplyPlayerClothes(player);
                                    Customization.ApplyPlayerTattoos(player);
                                    player.Transparency = 255;
                                }

                                if (player.GetData(EntityData.PLAYER_JOB) == 11 && player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                                {
                                    var clothDictionary = new Dictionary<int, ComponentVariation>();


                                    if (player.GetData(EntityData.PLAYER_SEX) == 0)
                                    {
                                        //clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 }); //Mask
                                        clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 }); // Legs
                                        clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 }); // Shoes
                                        clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                                        clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });
                                    }
                                    if (player.GetData(EntityData.PLAYER_SEX) == 1)
                                    {
                                        // clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                                        clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                                        clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                                        clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt
                                        clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });
                                    }

                                    NAPI.Player.SetPlayerClothes(player, clothDictionary);

                                    player.Transparency = 255;

                                }

                                /// Ocean Cleaning Section
                                ///                        


                            }

                            // Activate the login window
                            player.SetSharedData(EntityData.SERVER_TIME, DateTime.Now.ToString("HH:mm:ss"));
                            break;
                    }
                });
            });
        }

        [RemoteEvent("loginAccount")]
        public void LoginAccountEvent(Client player, string password)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Get the status of the account
                    int status = Database.LoginAccount(player.SocialClubName, password);

                    switch (status)
                    {
                        case 0:
                            LoadApplicationEvent(player);
                            break;
                        case 1:
                            player.TriggerEvent("clearLoginWindow");
                            List<string> playerList = Database.GetAccountCharacters(player.SocialClubName);
                            player.TriggerEvent("showPlayerCharacters", NAPI.Util.ToJson(playerList));
                            break;
                        default:
                            player.TriggerEvent("showLoginError");
                            break;
                    }
                });
            });
        }

        [RemoteEvent("registerAccount")]
        public void RegisterAccountEvent(Client player, string password)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Register the account
                    Database.RegisterAccount(player.SocialClubName, password);

                    // Show the application for the player
                    LoadApplicationEvent(player);
                });
            });
        }

        [RemoteEvent("goBackToCharacterList")]
        public void BackToNoCharacters(Client player)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    player.Transparency = 0;
                });
            });
        }

        [RemoteEvent("submitApplication")]
        public void SubmitApplicationEvent(Client player, string answers)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Get all the question and answers
                    Dictionary<int, int> application = NAPI.Util.FromJson<Dictionary<int, int>>(answers);

                    // Check if all the answers are correct
                    int mistakes = Database.CheckCorrectAnswers(application);

                    if (mistakes > 0)
                    {
                        // Tell the player his mistakes
                        player.TriggerEvent("failedApplication", mistakes);
                    }
                    else
                    {
                        // Tell the player he passed the test
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.application_passed);

                        // Destroy the test window
                        player.TriggerEvent("clearApplication");

                        List<string> playerList = Database.GetAccountCharacters(player.SocialClubName);
                        player.TriggerEvent("showPlayerCharacters", NAPI.Util.ToJson(playerList));

                        // Accept the account on the server
                        Database.ApproveAccount(player.SocialClubName);
                    }

                    // Register the attempt on the database
                    Database.RegisterApplication(player.SocialClubName, mistakes);
                });
            });
        }

        [RemoteEvent("changeCharacterSex")]
        public void ChangeCharacterSexEvent(Client player, int sex)
        {
            // Set the model of the player
            NAPI.Player.SetPlayerSkin(player, sex == 0 ? PedHash.FreemodeMale01 : PedHash.FreemodeFemale01);

            // Remove player's clothes
            player.SetClothes(11, 15, 0);
            player.SetClothes(3, 15, 0);
            player.SetClothes(8, 15, 0);

            // Save sex entity shared data
            player.SetData(EntityData.PLAYER_SEX, sex);

            // Force the player's animation
            player.PlayAnimation("amb@world_human_hang_out_street@female_arms_crossed@base", "base", (int)Constants.AnimationFlags.Loop);
        }

        [RemoteEvent("createCharacter")]
        public void CreateCharacterEvent(Client player, string characterName, string characterModel, int characterAge, int characterSex, string skinJson)
        {
            PlayerModel playerModel = new PlayerModel();
            SkinModel skinModel = NAPI.Util.FromJson<SkinModel>(skinJson);

            playerModel.realName = characterName;
            playerModel.model = characterModel;
            playerModel.age = characterAge;
            playerModel.sex = characterSex;

            // Set the player model
            player.SetSkin(NAPI.Util.GetHashKey(playerModel.model));

            if(Customization.IsCustomCharacter(player))
            {
                // Add the customization
                player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);
                Customization.ApplyPlayerCustomization(player, skinModel, playerModel.sex);
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    int playerId = Database.CreateCharacter(player, playerModel, skinModel);

                    if (playerId > 0)
                    {
                        Character.InitializePlayerData(player);
                        player.Transparency = 255;
                        player.SetData(EntityData.PLAYER_SQL_ID, playerId);
                        player.SetData(EntityData.PLAYER_NAME, playerModel.realName);
                        player.SetData(EntityData.PLAYER_AGE, playerModel.age);
                        player.SetData(EntityData.PLAYER_SEX, playerModel.sex);
                        player.SetData(EntityData.PLAYER_SPAWN_POS, new Vector3(-136.0034f, 6198.949f, 32.38448f));
                        player.SetData(EntityData.PLAYER_SPAWN_ROT, new Vector3(0.0f, 0.0f, 180.0f));

                        Database.UpdateLastCharacter(player.SocialClubName, playerId);

                        player.TriggerEvent("characterCreatedSuccessfully");
                    }
                });
            });
        }

        [RemoteEvent("setCharacterIntoCreator")]
        public void SetCharacterIntoCreatorEvent(Client player)
        {
            // Change player's skin
            NAPI.Player.SetPlayerSkin(player, PedHash.FreemodeMale01);

            // Remove clothes
            player.SetClothes(11, 15, 0);
            player.SetClothes(3, 15, 0);
            player.SetClothes(8, 15, 0);

            // Remove all the tattoos
            Customization.RemovePlayerTattoos(player);

            // Set player's position
            player.Transparency = 255;
            player.Rotation = new Vector3(0.0f, 0.0f, 270.0f);
            player.Position = new Vector3(397.3222f, -1004.373f, -99.00414f);

            // Play the idle animation
            PlayIdleCreatorAnimationEvent(player);
        }

        [RemoteEvent("playIdleCreatorAnimation")]
        public void PlayIdleCreatorAnimationEvent(Client player)
        {
            // Force the player's animation
            player.PlayAnimation("amb@world_human_hang_out_street@female_arms_crossed@base", "base", (int)Constants.AnimationFlags.Loop);
        }

        [RemoteEvent("getDefaultSkins")]
        public void GetDefaultSkinsEvent(Client player, int sex)
        {
            List<string> skinList = sex == Constants.SEX_MALE ? Constants.MALE_SKINS : Constants.FEMALE_SKINS;

            // Show the skins on the creator
            player.TriggerEvent("showDefaultSkins", NAPI.Util.ToJson(skinList));
        }

        [RemoteEvent("loadCharacter")]
        public void LoadCharacterEvent(Client player, string name)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    PlayerModel playerModel = Database.LoadCharacterInformationByName(name);

                    // Load player's model
                    player.Name = playerModel.realName;
                    player.SetSkin(NAPI.Util.GetHashKey(playerModel.model));

                    // Load player's basic data
                    Character.LoadCharacterData(player, playerModel);

                    if (Customization.IsCustomCharacter(player))
                    {
                        // Get the customization from the database
                        SkinModel skinModel = Database.GetCharacterSkin(playerModel.id);
                        player.SetData(EntityData.PLAYER_SKIN_MODEL, skinModel);

                        // Customize the character
                        Customization.ApplyPlayerCustomization(player, skinModel, playerModel.sex);
                        Customization.ApplyPlayerClothes(player);
                        Customization.ApplyPlayerTattoos(player);

                        player.Transparency = 255;

                        /// OCEAN CLEANER UNIFORM LOAD
                        
                        if (player.GetData(EntityData.PLAYER_JOB) == 11 && player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                        {
                            var clothDictionary = new Dictionary<int, ComponentVariation>();

                            if (player.GetData(EntityData.PLAYER_SEX) == 0)
                            {
                                //clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 }); //Mask
                                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 }); // Legs
                                clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 }); // Shoes
                                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                                clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });
                            }
                            if (player.GetData(EntityData.PLAYER_SEX) == 1)
                            {
                                // clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                                clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                                clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });
                            }

                            NAPI.Player.SetPlayerClothes(player, clothDictionary);
                        }

                    }

                    // Update last selected character
                    Database.UpdateLastCharacter(player.SocialClubName, playerModel.id);
                });
            });
        }

        [RemoteEvent("loadApplication")]
        public void LoadApplicationEvent(Client player)
        {
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Get random questions
                    List<TestModel> applicationQuestions = Database.GetRandomQuestions(Constants.APPLICATION_TEST, 10);

                    // Get the ids from each question
                    List<int> questionIds = applicationQuestions.Select(q => q.id).Distinct().ToList();

                    // Get the answers from the questions
                    List<TestModel> applicationAnswers = Database.GetQuestionAnswers(questionIds);

                    player.TriggerEvent("showApplicationTest", NAPI.Util.ToJson(applicationQuestions), NAPI.Util.ToJson(applicationAnswers));
                });
            });
        }
    }
}