using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.database;
using WiredPlayers.business;
using WiredPlayers.parking;
using WiredPlayers.house;
using WiredPlayers.weapons;
using WiredPlayers.factions;
using WiredPlayers.character;
using WiredPlayers.messages.information;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.administration;
using WiredPlayers.messages.success;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace WiredPlayers.chat
{
    public class Chat : Script
    {
        public static Chat instance;

        public List<TextLabel> PlayerActions = new List<TextLabel>();

        public Chat()
        {
            instance = this;
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            // Deleting player's attached label
            if (player.GetData(EntityData.PLAYER_AME) != null)
            {
                TextLabel label = player.GetData(EntityData.PLAYER_AME);
                label.Detach();
                label.Delete();
            }
        }

        public static void SendMessageToNearbyPlayers(Client player, string message, int type, float range, bool excludePlayer = false)
        {
            string fakeName = null;
            //string playerSQL = null;

            // Calculate the different gaps for the chat
            float distanceGap = range / Constants.CHAT_RANGES;


            // Get the list of the connected players
            List<Client> targetList = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.Dimension == player.Dimension).ToList();






            foreach (Client target in targetList)
            {
                if (player != target || (player == target && !excludePlayer))
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    string playerDataName = "";

                    if (target.GetSharedData("f" + playerId) != null)
                    {
                        fakeName = target.GetSharedData("f" + player.GetData(EntityData.PLAYER_SQL_ID));
                    }
                    else
                    {
                        fakeName = "Player " + player.Value;
                    }
                    if (player == target)
                        fakeName = player.Name;               

                    float distance = player.Position.DistanceTo(target.Position);

                    if (distance <= range)
                    {
                        // Getting message color
                        string chatMessageColor = GetChatMessageColor(distance, distanceGap, false);
                        string oocMessageColor = GetChatMessageColor(distance, distanceGap, true);

                        switch (type)
                        {
                            // We send the message
                            case Constants.MESSAGE_TALK:
                                target.SendChatMessage(chatMessageColor + fakeName + GenRes.chat_say + message);
                                break;
                            case Constants.MESSAGE_SHOUT:
                                target.SendChatMessage(chatMessageColor + fakeName + GenRes.chat_yell + message + "!");
                                break;
                            case Constants.MESSAGE_WHISPER:
                                target.SendChatMessage(chatMessageColor + fakeName + GenRes.chat_whisper + message);
                                break;
                            case Constants.MESSAGE_PHONE:
                                target.SendChatMessage(chatMessageColor + fakeName + GenRes.chat_phone + message);
                                break;
                            case Constants.MESSAGE_RADIO:
                                target.SendChatMessage(chatMessageColor + fakeName + GenRes.chat_radio + message);
                                break;
                            case Constants.MESSAGE_ME:
                                target.SendChatMessage(Constants.COLOR_CHAT_ME + fakeName + " " + message);
                                break;
                            case Constants.MESSAGE_DO:
                                target.SendChatMessage(Constants.COLOR_CHAT_DO + fakeName + "[ID: " + player.Value + "] " + message);
                                break;
                            case Constants.MESSAGE_OOC:
                                target.SendChatMessage(oocMessageColor + "(([ID: " + player.Value + "] " + fakeName + ": " + message + "))");
                                break;
                            case Constants.MESSAGE_DISCONNECT:
                                target.SendChatMessage(Constants.COLOR_HELP + "[ID: " + player.Value + "] " + fakeName + ": " + message);
                                break;
                            case Constants.MESSAGE_MEGAPHONE:
                                target.SendChatMessage(Constants.COLOR_INFO + "[Megáfono de " + fakeName + "]: " + message);
                                break;
                            case Constants.MESSAGE_SU_TRUE:
                                message = string.Format(SuccRes.possitive_result, fakeName);
                                target.SendChatMessage(Constants.COLOR_SU_POSITIVE + message);
                                break;
                            case Constants.MESSAGE_SU_FALSE:
                                message = string.Format(ErrRes.negative_result, fakeName);
                                target.SendChatMessage(Constants.COLOR_ERROR + message);
                                break;
                        }
                    }
                }
            }
        }


        [RemoteEvent("triggerAme")]
        public void TriggerAme(Client player, object[] args)
        {
            string ameMessage = args[0].ToString();

            Vector3 playerPos = player.Position;
            Vector3 aboveHeadPos = new Vector3(playerPos.X, playerPos.Y, playerPos.Z = playerPos.Z + 1);



            TextLabel ame = NAPI.TextLabel.CreateTextLabel(player.GetData(EntityData.PLAYER_NAME) + ameMessage, new Vector3(aboveHeadPos.X, aboveHeadPos.Y, aboveHeadPos.Z), 20, 0.5f, 4, new Color(194, 162, 218));

            //ame.AttachTo(player, "SKEL_Head", new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f));



        }

        private static string GetChatMessageColor(float distance, float distanceGap, bool ooc)
        {
            string color = null;
            if (distance < distanceGap)
            {
                color = ooc ? Constants.COLOR_OOC_CLOSE : Constants.COLOR_CHAT_CLOSE;
            }
            else if (distance < distanceGap * 2)
            {
                color = ooc ? Constants.COLOR_OOC_NEAR : Constants.COLOR_CHAT_NEAR;
            }
            else if (distance < distanceGap * 3)
            {
                color = ooc ? Constants.COLOR_OOC_MEDIUM : Constants.COLOR_CHAT_MEDIUM;
            }
            else if (distance < distanceGap * 4)
            {
                color = ooc ? Constants.COLOR_OOC_FAR : Constants.COLOR_CHAT_FAR;
            }
            else
            {
                color = ooc ? Constants.COLOR_OOC_LIMIT : Constants.COLOR_CHAT_LIMIT;
            }
            return color;
        }

        [ServerEvent(Event.ChatMessage)]
        public void OnChatMessage(Client player, string message)
        {
            if (player.GetData(EntityData.PLAYER_PLAYING) == null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_chat);
                return;
            }

            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }

            if (player.GetData(EntityData.PLAYER_ON_AIR) != null)
            {
                WeazelNews.SendNewsMessage(player, message);
            }
            else if (player.GetData(EntityData.PLAYER_PHONE_TALKING) != null)
            {
                // Target player of the message
                Client target = player.GetData(EntityData.PLAYER_PHONE_TALKING);

                // We send the message to the player and target
                player.SendChatMessage(Constants.COLOR_CHAT_PHONE + GenRes.phone + player.Name + GenRes.chat_say + message);
                target.SendChatMessage(Constants.COLOR_CHAT_PHONE + GenRes.phone + player.Name + GenRes.chat_say + message);

                // We send the message to nearby players


                SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_PHONE, player.Dimension > 0 ? 7.5f : 10.0f, true);
            }
            else
            {
                // We send the message to nearby players

                //SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, player.Dimension > 0 ? 7.5f : 20.0f);

                //SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_SHOUT, 45.0f);


                SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_TALK, player.Dimension > 0 ? 7.5f : 10.0f, true);
            }

            // Log the message on the server           
            string timeString = "[" + DateTime.Now.ToString("HH:mm:ss") + "." + DateTime.Now.Millisecond + "] ";
            NAPI.Util.ConsoleOutput(timeString + "[ID:" + player.Value + "] " + player.Name + GenRes.chat_say + message);
        }

        [RemoteEvent("playerNotLoggedCommand")]
        public void PlayerNotLoggedCommandEvent(Client player)
        {
            // Send the message to the player
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_cant_command);
        }

        [RemoteEvent("logPlayerCommand")]
        public void LogPlayerCommandEvent(Client player, string command)
        {
            // Get the server time            
            string timeString = "[" + DateTime.Now.ToString("HH:mm:ss") + "." + DateTime.Now.Millisecond + "] ";

            // Log the command used
            NAPI.Util.ConsoleOutput(timeString + string.Format(GenRes.command_used, player.Value, player.Name, command));
        }

        [Command(Commands.COM_SAY, Commands.HLP_SAY_COMMAND, GreedyArg = true)]
        public void DecirCommand(Client player, string message)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                //player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                //SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_TALK, player.Dimension > 0 ? 7.5f : 10.0f);
            }
        }


        //public int CheckLevelChart()
        // {

        //}
        [Command("buylevel")]
        public void BuyPlayerLevel(Client player)
        {
            int pl = player.GetData(EntityData.PLAYER_LEVEL);
            int rp = player.GetData(EntityData.PLAYER_PLAYED); /// RESPECT POINTS
            int maxPoints = 10; // Default for Level 1
            int purchaseAmount = 100;

            if (pl == 1)
                maxPoints = 10;
            else if (pl == 2)
                maxPoints = maxPoints + 5;
            else if (pl == 3)
                maxPoints = maxPoints + 10;
            else if (pl == 4)
                maxPoints = maxPoints + 20;
            else if (pl == 5)
                maxPoints = maxPoints + 30;
            else if (pl == 6)
                maxPoints = maxPoints + 40;
            else if (pl == 7)
                maxPoints = maxPoints + 55;
            else if (pl == 8)
                maxPoints = maxPoints + 70;
            else if (pl == 9)
                maxPoints = maxPoints + 85;
            else if (pl == 10)
                maxPoints = maxPoints + 115;
            else if (pl == 11)
                maxPoints = maxPoints + 150;
            else if (pl == 12)
                maxPoints = maxPoints + 200;

            if (pl == 1)
                purchaseAmount = 100;
            else if (pl == 2)
                purchaseAmount = 250;
            else if (pl == 3)
                purchaseAmount = 500;
            else if (pl == 4)
                purchaseAmount = 750;
            else if (pl == 5)
                purchaseAmount = 1000;
            else if (pl == 6)
                purchaseAmount = 1500;
            else if (pl == 7)
                purchaseAmount = 2000;
            else if (pl == 8)
                purchaseAmount = 3000;
            else if (pl == 9)
                purchaseAmount = 5000;
            else if (pl == 10)
                purchaseAmount = 10000;
            else if (pl == 11)
                purchaseAmount = 11000;
            else if (pl == 12)
                purchaseAmount = 12500;


            if (rp == maxPoints || rp > maxPoints)
            {


                int currentMoney = player.GetSharedData(EntityData.PLAYER_BANK);

                if (currentMoney <= purchaseAmount)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You can't afford to buy the next level.");
                    return;
                }

                int changedMoney = currentMoney - purchaseAmount;
                int newLevel = pl = pl + 1;
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "You are now Level " + newLevel + "!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_DOLLARGREEN + "$" + purchaseAmount + Constants.COLOR_WHITE + " has been deducted from your Bank Account.");

                player.SetSharedData(EntityData.PLAYER_BANK, changedMoney);
                player.SetData(EntityData.PLAYER_LEVEL, newLevel);
                player.SetData(EntityData.PLAYER_PLAYED, 0);
            }
        }

        [Command("stats")]
        public void CheckPlayerStats(Client player)
        {
            int pl = player.GetData(EntityData.PLAYER_LEVEL);
            int rp = player.GetData(EntityData.PLAYER_PLAYED); /// RESPECT POINTS
            int maxPoints = 10; // Default for Level 1

            if (pl == 1)
                maxPoints = 10;
            else if (pl == 2)
                maxPoints = maxPoints + 5;
            else if (pl == 3)
                maxPoints = maxPoints + 10;
            else if (pl == 4)
                maxPoints = maxPoints + 20;
            else if (pl == 5)
                maxPoints = maxPoints + 30;
            else if (pl == 6)
                maxPoints = maxPoints + 40;
            else if (pl == 7)
                maxPoints = maxPoints + 55;
            else if (pl == 8)
                maxPoints = maxPoints + 70;
            else if (pl == 9)
                maxPoints = maxPoints + 85;
            else if (pl == 10)
                maxPoints = maxPoints + 115;
            else if (pl == 11)
                maxPoints = maxPoints + 150;
            else if (pl == 12)
                maxPoints = maxPoints + 200;

            List<VehicleModel> vehicles = Database.LoadAllVehicles();
            List<HouseModel> houses = Database.LoadAllHouses();
            List<BusinessModel> businesses = Database.LoadAllBusiness();

            int ownedVehiclesAmount = 0;
            int ownedHousesAmount = 0;
            int ownedBusinessesAmount = 0;

            foreach (VehicleModel veh in vehicles)
            {
                if (veh.owner == player.GetData(EntityData.PLAYER_NAME))
                {
                    ownedVehiclesAmount = ownedVehiclesAmount + 1;

                    if (veh.model == (uint)VehicleHash.Submersible2 || veh.model == (uint)VehicleHash.Scrap)
                    {
                        ownedVehiclesAmount = ownedVehiclesAmount - 1;
                    }

                    player.SetData(EntityData.PLAYER_VEHICLES, ownedVehiclesAmount);
                }
            }

            foreach (HouseModel h in houses)
            {
                if (h.owner == player.GetData(EntityData.PLAYER_NAME))
                {
                    ownedHousesAmount = ownedHousesAmount + 1;
                    player.SetData(EntityData.PLAYER_HOUSES, ownedHousesAmount);

                }
            }

            foreach (BusinessModel bm in businesses)
            {
                if (bm.owner == player.GetData(EntityData.PLAYER_NAME))
                {
                    ownedBusinessesAmount = ownedBusinessesAmount + 1;
                    player.SetData(EntityData.PLAYER_BUSINESSES, ownedBusinessesAmount);
                }
            }

            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_SANDYORANGE + "_________PLAYER STATS:_________");
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_OCEANBLUE + "[Name: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_NAME) + Constants.COLOR_OCEANBLUE + " ( " + Constants.COLOR_WHITE + "Player: " + player.GetData(EntityData.PLAYER_SQL_ID) + Constants.COLOR_OCEANBLUE + " )");
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_OCEANBLUE + "[Bank: " + Constants.COLOR_DOLLARGREEN + "$" + player.GetSharedData(EntityData.PLAYER_BANK) + Constants.COLOR_OCEANBLUE + "] [Cash: " + Constants.COLOR_DOLLARGREEN + "$" + player.GetSharedData(EntityData.PLAYER_MONEY) + Constants.COLOR_OCEANBLUE + "] [Paycheck: " + Constants.COLOR_DOLLARGREEN + "$" + " TBD" + Constants.COLOR_OCEANBLUE + "]");
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_OCEANBLUE + "[Level: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_LEVEL) + Constants.COLOR_OCEANBLUE + "] [Respect Points: " + Constants.COLOR_WHITE + rp + "/" + maxPoints + Constants.COLOR_OCEANBLUE + "] [" + "Job: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_JOB) + Constants.COLOR_OCEANBLUE + "] " + Constants.COLOR_OCEANBLUE + "[Contact Number: " + Constants.COLOR_WHITE + "TBD" + Constants.COLOR_OCEANBLUE + "]");
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_OCEANBLUE + "[Houses: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_HOUSES) + "/3" + Constants.COLOR_OCEANBLUE + "] [Vehicles: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_VEHICLES) + "/3" + Constants.COLOR_OCEANBLUE + "] [Businesses: " + Constants.COLOR_WHITE + player.GetData(EntityData.PLAYER_BUSINESSES) + "/3" + Constants.COLOR_OCEANBLUE + "] [Parking Spaces: " + Constants.COLOR_WHITE + "0/2" + Constants.COLOR_OCEANBLUE + "]");

            if (rp == maxPoints || rp > maxPoints)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_DOLLARGREEN + "You are ready to level up!");
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_SANDYORANGE + "Level Upgrade Cost: " + Constants.COLOR_DOLLARGREEN + "$500 " + Constants.COLOR_SANDYORANGE + "/buylevel");

            }

        }

        [RemoteEvent]
        public void TestVoice(Client player)
        {
            NAPI.Util.ConsoleOutput("LALA");
        }


        [Command(Commands.SHOUT, Alias = "shout", Description = Commands.HLP_YELL_COMMAND, GreedyArg = true)]
        public void GritarCommand(Client player, string message)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_SHOUT, 45.0f);
            }
        }

        [Command(Commands.COM_WHISPER, Alias = Commands.COM_WHISPER_ALIAS, Description = Commands.HLP_WHISPER_COMMAND, GreedyArg = true)]
        public void SusurrarCommand(Client player, string message)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_WHISPER, 3.0f);
            }
        }

        [Command("checkk", GreedyArg = true)]
        public void Testy(Client player)
        {
            // string fakeName = player.GetData("22");

            //NAPI.Util.ConsoleOutput(fakeName);

            //NAPI.ClientEvent.TriggerClientEvent(player, "aliasName", player);

            NAPI.Player.SetPlayerNametag(player, "Lool");
        }

        public static Client GetPlayerById(int id)
        {
            // Get the player with the selected identifier
            return NAPI.Pools.GetAllPlayers().Where(pl => pl.GetData(EntityData.PLAYER_PLAYING) != null && pl.Value == id).FirstOrDefault();
        }


        [Command("alias", GreedyArg = true)]
        public void CMD_ALIAS(Client player, int targetId, string name)
        {

            List<AliasModel> aliases = Database.LoadAliases();

            Client target = GetPlayerById(targetId);

            if (target == player)
            {
                player.SendNotification("You can not alias yourself.");
                return;
            }

            if (target == null)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, "No one with that ID was found.");
                return;
            }

            int playerSqlId = player.GetData(EntityData.PLAYER_SQL_ID);
            int targetSqlId = target.GetData(EntityData.PLAYER_SQL_ID);

            string setNameData = "f" + targetSqlId.ToString();

            List<int> yourIds = new List<int>();
            List<int> targetIds = new List<int>();



            player.SetData(setNameData, name);
            player.SetData(targetId.ToString(), name);
            NAPI.Util.ConsoleOutput(player.GetSharedData(target.Value.ToString()));



            NAPI.Util.ConsoleOutput(setNameData);


            if (aliases.Count == 0) /// When Alias list is empty (FIRST EVER ALIAS)
            {
                CreateNewAlias(player, playerSqlId, targetSqlId, name);
                NAPI.Util.ConsoleOutput("SECTION 1");
                return;
            }

            foreach (AliasModel a in aliases)
            {
                if (a.playerId == playerSqlId)
                {
                    yourIds.Add(a.playerId);
                    targetIds.Add(a.targetId);
                }
            }

            foreach (AliasModel alias in aliases)
            {
                if (alias.playerId == playerSqlId)
                {
                    yourIds.Add(alias.playerId);
                    targetIds.Add(alias.targetId);
                }

                NAPI.Chat.SendChatMessageToPlayer(player, "I ran a search");

                if (yourIds.Count == 0)
                {
                    CreateNewAlias(player, playerSqlId, targetSqlId, name);
                    yourIds.Add(playerSqlId);
                    NAPI.Util.ConsoleOutput("SECTION 1.5");
                    return;
                }

                if (playerSqlId == alias.playerId)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, "I got your ID");

                    if (alias.targetId == targetSqlId)
                    {
                        NAPI.Chat.SendChatMessageToPlayer(player, "I got the target ID");

                        NAPI.Chat.SendChatMessageToPlayer(player, "You are no longer calling this person " + alias.name + " and are now calling them " + name);
                        Database.SaveAlias(alias, name);
                        return;
                    }
                    if (!targetIds.Contains(targetSqlId))
                    {
                        CreateNewAlias(player, playerSqlId, targetSqlId, name);
                        yourIds.Add(playerSqlId);
                        NAPI.Util.ConsoleOutput("SECTION 2");
                        return;
                    }
                }
                else
                {
                    if (yourIds.Count == 0)
                    {
                        CreateNewAlias(player, playerSqlId, targetSqlId, name);
                        yourIds.Add(playerSqlId);
                        NAPI.Util.ConsoleOutput("SECTION 3");
                        return;
                    }
                }
            }
        }

        public void CreateNewAlias(Client player, int playerId, int targetId, string name)
        {
            AliasModel alias = new AliasModel();

            alias.id = 0;
            alias.playerId = playerId;
            alias.targetId = targetId;
            alias.name = name;

            Database.NewAlias(alias);
        }

        [RemoteEvent]
        public void ChangedName()
        {

        }


        [Command(Commands.COM_ME, Commands.HLP_ME_COMMAND, GreedyArg = true)]
        public void MeCommand(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, player.Dimension > 0 ? 7.5f : 20.0f);
        }

        [Command(Commands.COM_DO, Commands.HLP_DO_COMMAND, GreedyArg = true)]
        public void DoCommand(Client player, string message)
        {
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_DO, player.Dimension > 0 ? 7.5f : 20.0f);
        }

        [Command(Commands.COM_LUCK)]
        public void SuCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                Random random = new Random();
                int messageType = random.Next(0, 2) > 0 ? Constants.MESSAGE_SU_TRUE : Constants.MESSAGE_SU_FALSE;
                SendMessageToNearbyPlayers(player, string.Empty, messageType, 20.0f);
            }
        }

        [Command(Commands.COM_AME, Commands.HLP_AME_COMMAND, GreedyArg = true)]
        public void AmeCommand(Client player, string message = "")
        {
            if (player.GetData(EntityData.PLAYER_AME) != null)
            {
                // We get player's TextLabel
                TextLabel label = player.GetData(EntityData.PLAYER_AME);

                if (message.Length > 0)
                {
                    // We update label's text
                    label.Text = "*" + message + "*";
                }
                else
                {
                    // Deleting TextLabel
                    label.Detach();
                    label.Delete();
                    player.ResetData(EntityData.PLAYER_AME);
                }
            }
            else
            {
                TextLabel ameLabel = NAPI.TextLabel.CreateTextLabel("*" + message + "*", new Vector3(0.0f, 0.0f, 0.0f), 50.0f, 0.5f, 4, new Color(201, 90, 0, 255));

                //ameLabel.AttachTo(player, "SKEL_Head", new Vector3(0.0f, 0.0f, 1.0f), new Vector3(0.0f, 0.0f, 0.0f));
                player.SetData(EntityData.PLAYER_AME, ameLabel);
            }
        }

        [Command(Commands.COM_MEGAPHONE, Commands.HLP_MEGAPHONE_COMMAND, GreedyArg = true)]
        public void MegafonoCommand(Client player, string message)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                return;
            }

            if (player.Vehicle.Class != Constants.VEHICLE_CLASS_EMERGENCY)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_megaphone);
                return;
            }

            // Send the message with the megaphone
            SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_MEGAPHONE, 45.0f);
        }

        [Command(Commands.COM_PM, Commands.HLP_MP_COMMAND, GreedyArg = true)]
        public void MpCommand(Client player, string arguments)
        {
            Client target = null;
            string[] args = arguments.Trim().Split(' ');

            if (int.TryParse(args[0], out int targetId) == true)
            {
                // We get the player from the id
                target = Globals.GetPlayerById(targetId);
                args = args.Where(w => w != args[0]).ToArray();
                if (args.Length < 1)
                {
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_MP_COMMAND);
                    return;
                }
            }
            else if (args.Length > 2)
            {
                target = NAPI.Player.GetPlayerFromName(args[0] + " " + args[1]);
                args = args.Where(w => w != args[1]).ToArray();
                args = args.Where(w => w != args[0]).ToArray();
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_MP_COMMAND);
                return;
            }

            if (target != null && target.GetData(EntityData.PLAYER_PLAYING) != null)
            {
                if (player.GetData(EntityData.PLAYER_ADMIN_RANK) == Constants.STAFF_NONE && target.GetData(EntityData.PLAYER_ADMIN_RANK) == Constants.STAFF_NONE)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.mps_only_admin);
                }
                else
                {
                    string message = string.Join(" ", args);
                    string secondMessage = string.Empty;

                    // Sending messages to both players
                    player.SendChatMessage(Constants.COLOR_ADMIN_MP + "((" + GenRes.pm_to + "[ID: " + target.Value + "] " + target.Name + ": " + message + "))");
                    target.SendChatMessage(Constants.COLOR_ADMIN_MP + "((" + GenRes.pm_from + "[ID: " + player.Value + "] " + player.Name + ": " + message + "))");
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
            }
        }
    }
}
