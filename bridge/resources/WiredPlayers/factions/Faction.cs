using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.chat;
using WiredPlayers.database;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System;

namespace WiredPlayers.factions
{
    public class Faction : Script
    {
        public static List<ChannelModel> channelList;
        public static List<FactionWarningModel> factionWarningList;
        public static List<UnitModel> Units = new List<UnitModel>();
        public static List<PoliceControlModel> policeControlList;
        public Faction()
        {
            // Initialize the required fields
            factionWarningList = new List<FactionWarningModel>();
            policeControlList = new List<PoliceControlModel>();
        }

        public static string GetPlayerFactionRank(Client player)
        {
            string rankString = string.Empty;
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            int rank = player.GetData(EntityData.PLAYER_RANK);

            // Get the player faction
            FactionModel factionModel = Constants.FACTION_RANK_LIST.Where(fact => fact.faction == faction && fact.rank == rank).FirstOrDefault();

            return factionModel == null ? string.Empty : (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? factionModel.descriptionMale : factionModel.descriptionFemale);
        }

        public static FactionWarningModel GetFactionWarnByTarget(int playerId, int faction)
        {
            // Get the faction warn for the given faction
            return factionWarningList.Where(factionWarn => factionWarn.playerId == playerId && factionWarn.faction == faction).FirstOrDefault();
        }

        public static bool IsPoliceMember(Client player)
        {
            // Check if the player is already connected
            if (player.GetData(EntityData.PLAYER_PLAYING) == null) return false;

            int faction = player.GetData(EntityData.PLAYER_FACTION);
            return (faction == Constants.FACTION_POLICE || faction == Constants.FACTION_SHERIFF) && player.GetData(EntityData.PLAYER_RANK) > 0;
        }
        public static bool IsDocMember(Client player)
        {
            // Check if the player is already connected
            if (player.GetData(EntityData.PLAYER_PLAYING) == null) return false;

            int faction = player.GetData(EntityData.PLAYER_FACTION);
            return (faction == Constants.FACTION_DOC) && player.GetData(EntityData.PLAYER_RANK) > 0;
        }
        public static bool IsLSCMember(Client player)
        {
            // Check if the player is already connected
            if (player.GetData(EntityData.PLAYER_PLAYING) == null) return false;

            int faction = player.GetData(EntityData.PLAYER_FACTION);
            return (faction == Constants.FACTION_LSC) && player.GetData(EntityData.PLAYER_RANK) > 0;
        }
        public static bool IsMedicMember(Client player)
        {
            // Check if the player is already connected
            if (player.GetData(EntityData.PLAYER_PLAYING) == null) return false;

            int faction = player.GetData(EntityData.PLAYER_FACTION);
            return (faction == Constants.FACTION_EMERGENCY) && player.GetData(EntityData.PLAYER_RANK) > 0;
        }

        private ChannelModel GetPlayerOwnedChannel(int playerId)
        {
            // Get the channel owned by a player
            return channelList.Where(channelModel => channelModel.owner == playerId).FirstOrDefault();
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            StringBuilder sBuilder = new StringBuilder();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private bool CheckInternalAffairs(int faction, Client target)
        {
            // Check if the player is from Internal Affairs
            return faction == Constants.FACTION_TOWNHALL && (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_POLICE && target.GetData(EntityData.PLAYER_RANK) == 7);
        }

        [RemoteEvent("removeWarning")]
        public void RemoveWarningEvent(Client player)
        {
            // Remove the report
            factionWarningList.RemoveAll(x => x.takenBy == player.Value);
        }

        [Command(Commands.COM_F, Commands.HLP_F_COMMAND, GreedyArg = true)]
        public void FCommand(Client player, string message)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (faction > 0 && faction < Constants.LAST_STATE_FACTION)
            {
                string rank = GetPlayerFactionRank(player);

                // Get the players on the faction
                List<Client> targetList = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_FACTION) == faction).ToList();

                foreach (Client target in targetList)
                {
                    // Send the message to the player
                    target.SendChatMessage(Constants.COLOR_CHAT_FACTION + "(([ID: " + player.Value + "] " + rank + " " + player.Name + ": " + message + "))");
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_state_faction);
            }
        }

        [Command(Commands.COM_R, Commands.HLP_R_COMMAND, GreedyArg = true)]
        public void RCommand(Client player, string message)
        {
            string Unit = player.GetData(EntityData.PLAYER_POLICE_UNIT);
            string timeString = "[" + DateTime.Now.ToString("HH:mm");
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0 && player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_NEWS)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
            }
            else
            {
                int faction = player.GetData(EntityData.PLAYER_FACTION);

                if (faction > 0 && faction < Constants.LAST_STATE_FACTION)
                {
                    // Get player's rank in faction
                    string rank = GetPlayerFactionRank(player);

                    // Get all the players in the faction
                    List<Client> targetList = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && (p.GetData(EntityData.PLAYER_FACTION) == faction || CheckInternalAffairs(faction, p)) && p.GetData(EntityData.PLAYER_ON_DUTY) == 1).ToList();

                    foreach (Client target in targetList)
                    {
                        // Send the message to each one of the players
                        target.SendChatMessage(Constants.COLOR_RADIO + GenRes.radio + Unit + " " + rank + " " + player.Name + GenRes.chat_say + message);
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(timeString, player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);

                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_state_faction);
                }
            }
        }

        [Command(Commands.COM_DP, Commands.HLP_DP_COMMAND, GreedyArg = true)]
        public void DpCommand(Client player, string message)
        {
            string Unit = player.GetData(EntityData.PLAYER_POLICE_UNIT);
            string timeString = "[" + DateTime.Now.ToString("HH:mm");
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }

            if (player.GetData(EntityData.PLAYER_FACTION) != Constants.FACTION_POLICE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a goverment faction");
                return;
            }

            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                return;
            }

            string rank = GetPlayerFactionRank(player);
            string radioMessage = GenRes.radio + Unit + " " + rank + " " + player.Name + GenRes.chat_say + message;

            // Get the players playing and in service
            List<Client> playerFactionMembers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_ON_DUTY) == 1).ToList();

            foreach (Client target in playerFactionMembers)
            {
                // Send the message to the player
                if (IsPoliceMember(target))
                {
                    target.SendChatMessage(Constants.COLOR_RADIO_POLICE + radioMessage);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                {
                    target.SendChatMessage(Constants.COLOR_RADIO_POLICE + radioMessage);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_DOC)
                {
                    target.SendChatMessage(Constants.COLOR_RADIO_POLICE + radioMessage);
                }
            }

            // Send the chat message to near players
            Chat.SendMessageToNearbyPlayers(timeString, player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
        }

        [Command(Commands.COM_DE, Commands.HLP_DE_COMMAND, GreedyArg = true)]
        public void DeCommand(Client player, string message)
        {
            string timeString = "[" + DateTime.Now.ToString("HH:mm") + "]";
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }

            if (!IsPoliceMember(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_faction);
                return;
            }

            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                return;
            }

            string rank = GetPlayerFactionRank(player);
            string radioMessage = GenRes.radio + rank + " " + player.Name + GenRes.chat_say + message;

            // Get the players playing and in service
            List<Client> playerFactionMembers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_ON_DUTY) == 1).ToList();

            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (IsPoliceMember(target))
                {
                    target.SendChatMessage(Constants.COLOR_RADIO_POLICE + radioMessage);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY)
                {
                    target.SendChatMessage(Constants.COLOR_RADIO + radioMessage);
                }
            }

            // Send the chat message to near players
            Chat.SendMessageToNearbyPlayers(timeString, player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
        }

        [Command(Commands.COM_FR, Commands.HLP_FR_COMMAND, GreedyArg = true)]
        public void FrCommand(Client player, string message)
        {
            string timeString = "[" + DateTime.Now.ToString("HH:mm");
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                int radio = player.GetData(EntityData.PLAYER_RADIO);
                if (radio > 0)
                {
                    string name = player.GetData(EntityData.PLAYER_NAME);

                    // Get the players with the same radio frequency
                    List<Client> targetList = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_RADIO) == radio).ToList();

                    foreach (Client target in targetList)
                    {
                        // Send the message to each player
                        target.SendChatMessage(Constants.COLOR_RADIO + GenRes.radio + name + GenRes.chat_say + message);
                    }

                    // Send the chat message to near players
                    Chat.SendMessageToNearbyPlayers(timeString, player, message, Constants.MESSAGE_RADIO, player.Dimension > 0 ? 7.5f : 10.0f);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.radio_frequency_none);
                }
            }
        }

        [Command(Commands.COM_FREQUENCY, Commands.HLP_FREQUENCY_COMMAND, GreedyArg = true)]
        public void FrequencyCommand(Client player, string args)
        {
            if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                // Get the item identifier
                string rightHand = Constants.INVENTORY_TARGET_PLAYER.ToString();
                int itemId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                ItemModel item = Globals.GetItemModelFromId(itemId);

                if (item != null && item.hash == Constants.ITEM_HASH_WALKIE)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ChannelModel ownedChannel = GetPlayerOwnedChannel(playerId);
                    string[] arguments = args.Trim().Split(' ');
                    switch (arguments[0].ToLower())
                    {
                        case Commands.ARG_CREATE:
                            if (arguments.Length == 2)
                            {
                                if (ownedChannel == null)
                                {
                                    // We create the new frequency
                                    MD5 md5Hash = MD5.Create();
                                    ChannelModel channel = new ChannelModel();
                                    {
                                        channel.owner = playerId;
                                        channel.password = GetMd5Hash(md5Hash, arguments[1]);
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Create the new channel
                                            channel.id = Database.AddChannel(channel);
                                            channelList.Add(channel);

                                            // Sending the message with created channel
                                            string message = string.Format(InfoRes.channel_created, channel.id);
                                            player.SendChatMessage(Constants.COLOR_INFO + message);
                                        });
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_owned_channel);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FREQUENCY_CREATE_COMMAND);
                            }
                            break;
                        case Commands.ARG_MODIFY:
                            if (arguments.Length == 2)
                            {
                                if (ownedChannel != null)
                                {
                                    MD5 md5Hash = MD5.Create();
                                    ownedChannel.password = GetMd5Hash(md5Hash, arguments[1]);

                                    // We kick all the players from the channel
                                    foreach (Client target in NAPI.Pools.GetAllPlayers())
                                    {
                                        int targetId = player.GetData(EntityData.PLAYER_SQL_ID);
                                        if (target.GetData(EntityData.PLAYER_RADIO) == ownedChannel.id && targetId != ownedChannel.owner)
                                        {
                                            target.SetData(EntityData.PLAYER_RADIO, 0);
                                            target.SendChatMessage(Constants.COLOR_INFO + InfoRes.channel_disconnected);
                                        }
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the channel and disconnect the leader
                                            Database.UpdateChannel(ownedChannel);
                                            Database.DisconnectFromChannel(ownedChannel.id);

                                            // Message sent with the confirmation
                                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.channel_updated);
                                        });
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_owned_channel);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FREQUENCY_MODIFY_COMMAND);
                            }
                            break;
                        case Commands.ARG_REMOVE:
                            if (ownedChannel != null)
                            {
                                // We kick all the players from the channel
                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    int targetId = player.GetData(EntityData.PLAYER_SQL_ID);
                                    if (target.GetData(EntityData.PLAYER_RADIO) == ownedChannel.id)
                                    {
                                        target.SetData(EntityData.PLAYER_RADIO, 0);
                                        if (ownedChannel.owner != targetId)
                                        {
                                            target.SendChatMessage(Constants.COLOR_INFO + InfoRes.channel_disconnected);
                                        }
                                    }
                                }

                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Disconnect the leader from the channel
                                        Database.DisconnectFromChannel(ownedChannel.id);

                                        // We destroy the channel
                                        Database.RemoveChannel(ownedChannel.id);
                                        channelList.Remove(ownedChannel);

                                        // Message sent with the confirmation
                                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.channel_deleted);
                                    });
                                });
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_owned_channel);
                            }
                            break;
                        case Commands.ARG_CONNECT:
                            if (arguments.Length == 3)
                            {
                                if (int.TryParse(arguments[1], out int frequency) == true)
                                {
                                    // We encrypt the password
                                    MD5 md5Hash = MD5.Create();
                                    string password = GetMd5Hash(md5Hash, arguments[2]);

                                    foreach (ChannelModel channel in channelList)
                                    {
                                        if (channel.id == frequency && channel.password == password)
                                        {
                                            string message = string.Format(InfoRes.channel_connected, channel.id);
                                            player.SetData(EntityData.PLAYER_RADIO, channel.id);
                                            player.SendChatMessage(Constants.COLOR_INFO + message);
                                            return;
                                        }
                                    }

                                    // Couldn't find any channel with that id
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.channel_not_found);
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FREQUENCY_CONNECT_COMMAND);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FREQUENCY_CONNECT_COMMAND);
                            }
                            break;
                        case Commands.ARG_DISCONNECT:
                            player.SetData(EntityData.PLAYER_RADIO, 0);
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.channel_disconnected);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FREQUENCY_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_walkie_in_hand);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_empty);
            }
        }
        [Command(Commands.COM_LEAVEUNIT)]
        public void LeaveunitCommand(Client player)
        {
            string unitname = player.GetData(EntityData.PLAYER_POLICE_UNIT);
            if (player.GetData(EntityData.PLAYER_POLICE_UNIT) == null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are not in a unit");
            }
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            if (IsPoliceMember(player))
            {
                player.ResetData(EntityData.PLAYER_POLICE_UNIT);
                player.SendNotification("You left unit " + unitname);
            }
            if (IsMedicMember(player))
            {
                player.ResetData(EntityData.PLAYER_POLICE_UNIT);
                player.SendNotification("You left unit " + unitname);
            }
            if (IsDocMember(player))
            {
                player.ResetData(EntityData.PLAYER_POLICE_UNIT);
                player.SendNotification("You left unit " + unitname);
            }
            else
            {
                return;
            }
        }
        [Command(Commands.COM_PUT, Commands.HLP_POLICE_PUT_COMMAND)]
        public void PutCommand(Client player, string item)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }
            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                return;
            }
            PoliceControlModel policeControl;
            switch (faction)
            {
                case Constants.FACTION_POLICE:
                    switch (item.ToLower())
                    {
                        case Commands.ARG_CONE:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_CONE, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_CONE, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_BEACON:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BEACON, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BEACON, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_BARRIER:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BARRIER, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BARRIER, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_SPIKES:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_SPIKES, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_SPIKES, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_PUT_COMMAND);
                            break;

                    }
                    break;
                case Constants.FACTION_EMERGENCY:

                    switch (item.ToLower())
                    {
                        case Commands.ARG_CONE:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_CONE, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_CONE, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_BEACON:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BEACON, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BEACON, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_BARRIER:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_BARRIER, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_BARRIER, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        case Commands.ARG_SPIKES:
                            policeControl = new PoliceControlModel(0, string.Empty, Constants.POLICE_DEPLOYABLE_SPIKES, player.Position, player.Rotation);
                            policeControl.position = new Vector3(policeControl.position.X, policeControl.position.Y, policeControl.position.Z - 1.0f);
                            policeControl.controlObject = NAPI.Object.CreateObject(Constants.POLICE_DEPLOYABLE_SPIKES, policeControl.position, policeControl.rotation);
                            policeControlList.Add(policeControl);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_PUT_COMMAND);
                            break;
                            
                    }
                    break;
            }
        }
        [Command(Commands.COM_REMOVE, Commands.HLP_POLICE_REMOVE_COMMAND)]
        public void RemoveCommand(Client player, string item)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }
            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_on_duty);
                return;
            }
            PoliceControlModel policeControl;
            switch (faction)
            {
                case Constants.FACTION_POLICE:
                    switch (item.ToLower())
                    {
                        case Commands.ARG_CONE:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_CONE);
                            break;
                        case Commands.ARG_BEACON:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BEACON);
                            break;
                        case Commands.ARG_BARRIER:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BARRIER);
                            break;
                        case Commands.ARG_SPIKES:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_SPIKES);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_REMOVE_COMMAND);
                            break;
                    }
                    break;
                case Constants.FACTION_EMERGENCY:

                    switch (item.ToLower())
                    {
                        case Commands.ARG_CONE:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_CONE);
                            break;
                        case Commands.ARG_BEACON:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BEACON);
                            break;
                        case Commands.ARG_BARRIER:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_BARRIER);
                            break;
                        case Commands.ARG_SPIKES:
                            Police.RemoveClosestPoliceControlItem(player, Constants.POLICE_DEPLOYABLE_SPIKES);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_POLICE_REMOVE_COMMAND);
                            break;
                    }
                    break;
            }
        }

        [Command(Commands.COM_CREATEUNIT, Commands.HLP_CREATEUNIT_COMMAND)]
        public void CreateunitCommand(Client player, string unitname)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            UnitModel Unit;
            if (player.GetData(EntityData.PLAYER_POLICE_UNIT) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are already in a unit");
            }
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            switch (faction)
            {
                case Constants.FACTION_POLICE:
                    if (player.GetData(EntityData.PLAYER_FACTION) != 1)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not part of a government department");
                    }
                    else
                    {

                        player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
                        player.SendNotification("You created unit " + unitname);
                        Unit = new UnitModel();
                        {
                            Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
                            Unit.member = player.GetData(EntityData.PLAYER_NAME);
                            Unit.name = unitname;
                            Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
                            Units.Add(Unit);
                        }
                        // Database.CreateUnit(Unit);

                    }
                    break;
                case Constants.FACTION_EMERGENCY:
                    if (player.GetData(EntityData.PLAYER_FACTION) != 2)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not part of a government department");
                    }
                    else
                    {

                        player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
                        player.SendNotification("You created unit " + unitname);
                        Unit = new UnitModel();
                        {
                            Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
                            Unit.member = player.GetData(EntityData.PLAYER_NAME);
                            Unit.name = unitname;
                            Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
                            Units.Add(Unit);
                        }
                        // Database.CreateUnit(Unit);

                    }
                    break;
                case Constants.FACTION_DOC:
                    if (player.GetData(EntityData.PLAYER_FACTION) != 21)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not part of a government department");
                    }
                    else
                    {

                        player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
                        player.SendNotification("You created unit " + unitname);
                        Unit = new UnitModel();
                        {
                            Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
                            Unit.member = player.GetData(EntityData.PLAYER_NAME);
                            Unit.name = unitname;
                            Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
                            Units.Add(Unit);
                        }
                        // Database.CreateUnit(Unit);

                    }
                    break;
            }
            //        if (IsPoliceMember(player))
            //{
            //    player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
            //    player.SendNotification("You created unit " + unitname);
            //    Unit = new UnitModel();
            //    {
            //        Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
            //        Unit.member = player.GetData(EntityData.PLAYER_NAME); 
            //        Unit.name = unitname;
            //        Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
            //        Units.Add(Unit);
            //    }
            //   // Database.CreateUnit(Unit);
            //}
            //if (IsMedicMember(player))
            //{
            //    player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
            //    player.SendNotification("You created unit " + unitname);
            //    Unit = new UnitModel();
            //    {
            //        Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
            //        Unit.member = player.GetData(EntityData.PLAYER_NAME);
            //        Unit.name = unitname;
            //        Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
            //        Units.Add(Unit);
            //    }
            //   // Database.CreateUnit(Unit);
            //}
            //if (IsDocMember(player))
            //{
            //    player.SetData(EntityData.PLAYER_POLICE_UNIT, unitname);
            //    player.SendNotification("You created unit " + unitname);
            //    Unit = new UnitModel();
            //    {
            //        Unit.id = player.GetData(EntityData.PLAYER_SQL_ID);
            //        Unit.member = player.GetData(EntityData.PLAYER_NAME);
            //        Unit.name = unitname;
            //        Unit.faction = player.GetData(EntityData.PLAYER_FACTION);
            //        Units.Add(Unit);
            //    }
            //   // Database.CreateUnit(Unit);
            //}
            //else
            //{
            //    player.SendChatMessage(Constants.COLOR_ERROR + "You are not apart of any government faction.");
            //}
        }
        [Command(Commands.COM_RECRUIT, Commands.HLP_RECRUIT_COMMAND)]
        public void RecruitCommand(Client player, string targetString)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction > Constants.FACTION_NONE)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) > 0)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_faction);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_lspd);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_POLICE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_ems);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_EMERGENCY);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_news);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_NEWS);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_townhall);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_TOWNHALL);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_transport);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_TAXI_DRIVER);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.FACTION_SHERIFF:
                            if (target.GetData(EntityData.PLAYER_JOB) > 0)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_job);
                            }
                            else if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.sheriff_faction);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.FACTION_SHERIFF);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, faction);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, faction);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_ONE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_ONE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TWO:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_TWO);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_THREE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_THREE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FOUR:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_FOUR);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FIVE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_FIVE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SIX:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_SIX);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SEVEN:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_SEVEN);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_EIGHT:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_EIGHT);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_NINE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_NINE);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TEN:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_recruit);
                            }
                            else
                            {
                                string targetMessage = string.Format(InfoRes.faction_recruited, GenRes.faction_i1);

                                // We get the player into the faction
                                target.SetData(EntityData.PLAYER_FACTION, Constants.ILLEGAL_FACTION_TEN);
                                target.SetData(EntityData.PLAYER_RANK, 1);

                                // Sending the message to the player
                                target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            }
                            break;
                    }

                    // We send the message to the recruiter
                    string playerMessage = string.Format(InfoRes.player_recruited, target.Name);
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_faction);
            }
        }

        [Command(Commands.COM_DISMISS, Commands.HLP_DISMISS_COMMAND)]
        public void DismissCommand(Client player, string targetString)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction != Constants.FACTION_NONE)
            {
                Client target = int.TryParse(targetString, out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) != faction)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_same_faction);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.FACTION_SHERIFF:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_ONE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TWO:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_THREE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FOUR:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FIVE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SIX:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SEVEN:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_EIGHT:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_NINE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TEN:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_dismiss);
                            }
                            else
                            {
                                // We kick the player from the faction
                                target.SetData(EntityData.PLAYER_FACTION, 0);
                                target.SetData(EntityData.PLAYER_RANK, 0);
                            }
                            break;
                    }

                    string playerMessage = string.Format(InfoRes.player_dismissed, target.Name);
                    string targetMessage = string.Format(InfoRes.faction_dismissed, player.Name);

                    // Send the messages to both players
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                    target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_faction);
            }
        }

        [Command(Commands.COM_RANK, Commands.HLP_RANK_COMMAND, GreedyArg = true)]
        public void RankCommand(Client player, string arguments)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (faction != Constants.FACTION_NONE)
            {
                string[] args = arguments.Split(' ');

                // Get the target player
                Client target = int.TryParse(args[0], out int targetId) ? Globals.GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(args[0] + " " + args[1]);

                if (target == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                }
                else if (target.GetData(EntityData.PLAYER_FACTION) != faction)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_same_faction);
                }
                else
                {
                    int rank = player.GetData(EntityData.PLAYER_RANK);
                    int givenRank = args.Length > 2 ? int.Parse(args[2]) : int.Parse(args[1]);

                    switch (faction)
                    {
                        case Constants.FACTION_POLICE:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_EMERGENCY:
                            if (rank < 10)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_NEWS:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_TOWNHALL:
                            if (rank < 3)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_TAXI_DRIVER:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.FACTION_SHERIFF:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_ONE:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TWO:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_THREE:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FOUR:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_FIVE:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SIX:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_SEVEN:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_EIGHT:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_NINE:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        case Constants.ILLEGAL_FACTION_TEN:
                            if (rank < 5)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                        default:
                            if (rank < 6)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.rank_too_low_rank);
                            }
                            else
                            {
                                // Change player's rank
                                target.SetData(EntityData.PLAYER_RANK, givenRank);
                            }
                            break;
                    }

                    // Send the message to both players
                    player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.player_rank_changed, target.Name, givenRank));
                    target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.faction_rank_changed, player.Name, givenRank));
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_faction);
            }
        }

        [Command(Commands.COM_REPORTS)]
        public void ReportsCommand(Client player)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (IsPoliceMember(player) || faction == Constants.FACTION_EMERGENCY)
            {
                int currentElement = 0;

                // Reports' header
                player.SendChatMessage(Constants.COLOR_INFO + GenRes.reports_header);

                // Get all the warnings for the player's faction
                List<FactionWarningModel> warnings = factionWarningList.Where(w => w.faction == faction).ToList();

                foreach (FactionWarningModel factionWarning in warnings)
                {
                    string message = currentElement + ". " + GenRes.time + factionWarning.hour;

                    if (factionWarning.place.Length > 0)
                    {
                        message += ", " + GenRes.place + factionWarning.place;
                    }

                    // Check if attended
                    if (factionWarning.takenBy > -1)
                    {
                        Client target = Globals.GetPlayerById(factionWarning.takenBy);
                        message += ", " + GenRes.attended_by + target.Name;
                    }
                    else
                    {
                        message += ", " + GenRes.unattended;
                    }

                    // We send the message to the player
                    player.SendChatMessage(Constants.COLOR_HELP + message);

                    currentElement++;
                }

                if (currentElement == 0)
                {
                    // There are no reports in the list
                    player.SendChatMessage(Constants.COLOR_HELP + GenRes.not_faction_warning);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_emergency_faction);
            }
        }

        [Command(Commands.COM_ATTEND, Commands.HLP_ATTEND_COMMAND)]
        [RemoteEvent]
        public void AttendCommand(Client player, int warning)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (IsPoliceMember(player) || faction == Constants.FACTION_EMERGENCY)
            {
                // Get all the warnings from the faction
                FactionWarningModel factionWarning = factionWarningList.Where(f => f.faction == faction).ToList()[warning];

                // Check the faction and whether the report is attended
                if (factionWarning == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.faction_warning_not_found);
                }
                else if (factionWarning.takenBy > -1)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.faction_warning_taken);
                }
                else if (factionWarning.playerId == player.Value)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_own_death);
                }
                else if (player.GetData(EntityData.PLAYER_FACTION_WARNING) != null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_have_faction_warning);
                }
                else
                {
                    factionWarning.takenBy = player.Value;
                    player.SetData(EntityData.PLAYER_FACTION_WARNING, true);

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.faction_warning_taken);

                    player.TriggerEvent("showFactionWarning", factionWarning.position);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_emergency_faction);
            }
        }

        [Command(Commands.COM_CLEAR_REPORTS, Commands.HLP_CLEAR_REPORTS_COMMAND)]
        public void ClearReportsCommand(Client player, int warning)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);

            if (IsPoliceMember(player) || faction == Constants.FACTION_EMERGENCY)
            {
                // Get all the warnings from the faction
                FactionWarningModel factionWarning = factionWarningList.Where(f => f.faction == faction).ToList()[warning];

                // Check the faction and whether the report is attended
                if (factionWarning == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.faction_warning_not_found);
                }
                else
                {
                    // We remove the report
                    factionWarningList.Remove(factionWarning);

                    // Send the message to the user
                    player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.faction_warning_deleted, warning));
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_police_emergency_faction);
            }
        }

        [Command(Commands.COM_MEMBERS)]
        [RemoteEvent]
        public void MembersCommand(Client player)
        {
            int faction = player.GetData(EntityData.PLAYER_FACTION);
            if (faction > 0)
            {
                player.SendChatMessage(Constants.COLOR_INFO + GenRes.members_online);
                foreach (Client target in NAPI.Pools.GetAllPlayers())
                {
                    if (target.GetData(EntityData.PLAYER_PLAYING) != null && target.GetData(EntityData.PLAYER_FACTION) == faction)
                    {
                        string rank = GetPlayerFactionRank(target);

                        if (rank == string.Empty)
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + "[Id: " + target.Value + "] " + target.Name);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + "[Id: " + target.Value + "] " + rank + " " + target.Name);
                        }
                    }
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_faction);
            }
        }

        [Command(Commands.COM_SIREN)]
        [RemoteEvent("siren")]
        public void SirenCommand(Client player)
        {
            if (!player.IsInVehicle || player.VehicleSeat != (int)VehicleSeat.Driver)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_driving);
                return;
            }

            // Get the class of the vehicle
            if (player.Vehicle.Class != Constants.VEHICLE_CLASS_EMERGENCY)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_emergency_vehicle);
            }

            // Toggle the siren status for all the players
            bool siren = player.Vehicle.GetSharedData(EntityData.VEHICLE_SIREN_SOUND);
            player.Vehicle.SetSharedData(EntityData.VEHICLE_SIREN_SOUND, !siren);
            List<Client> connectedPlayers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null).ToList();

            foreach (Client target in connectedPlayers)
            {
                // Synchronize the siren state
                target.TriggerEvent("toggleSirenState", player.Vehicle.Value, !siren);
            }
        }
    }
}
