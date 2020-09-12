using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.factions;
using WiredPlayers.messages.information;
using WiredPlayers.messages.general;
using WiredPlayers.messages.error;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WiredPlayers.character
{
    public class Telephone : Script
    {
        public static List<PhoneModel> phoneList;

        public static void LoadPhones()
        {
            // Load the phone list
            phoneList = Database.LoadAllPhones();

            if (phoneList.Count > 0)
            {
                // Load the contact list
                List<ContactModel> contactList = Database.LoadAllContacts();

                foreach (PhoneModel phone in phoneList)
                {
                    // Get all the contacts from the phone
                    phone.contacts = contactList.Where(c => c.owner == phone.number).ToList();
                }
            }
        }

        public static PhoneModel GetPlayerHoldingPhone(Client player)
        {
            // Get the item on the player's right hand
            string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);

            if (rightHand != null)
            {
                // Check if the player has a phone on his hand
                int itemId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                return phoneList.Where(p => p.itemId == itemId).FirstOrDefault();
            }

            return null;
        }

        public static Client SearchPhoneOwnerById(int phoneId)
        {
            ItemModel item = Globals.itemList.Where(i => i.id == phoneId && (i.ownerEntity == Constants.ITEM_ENTITY_PLAYER || i.ownerEntity == Constants.ITEM_ENTITY_RIGHT_HAND)).FirstOrDefault();

            if (item != null)
            {
                // Get the player with the selected identifier
                return NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_SQL_ID) == item.ownerIdentifier).FirstOrDefault();
            }

            return null;
        }

        public static Client SearchPhoneOwnerByNumber(int number)
        {
            PhoneModel phone = phoneList.Where(p => p.number == number).FirstOrDefault();

            if (phone != null)
            {
                ItemModel item = Globals.itemList.Where(i => i.id == phone.itemId && (i.ownerEntity == Constants.ITEM_ENTITY_PLAYER || i.ownerEntity == Constants.ITEM_ENTITY_RIGHT_HAND)).FirstOrDefault();

                if (item != null)
                {
                    // Get the player with the selected identifier
                    return NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null && p.GetData(EntityData.PLAYER_SQL_ID) == item.ownerIdentifier).FirstOrDefault();
                }
            }

            return null;
        }

        public static void OnPlayerDisconnected(Client player)
        {
            if (player.GetData(EntityData.PLAYER_PHONE_TALKING) != null)
            {
                // Get the player he's talking with
                Client target = player.GetData(EntityData.PLAYER_PHONE_TALKING);

                // Hang up the call
                target.ResetData(EntityData.PLAYER_PHONE_TALKING);
                target.ResetData(EntityData.PLAYER_PHONE_CALL_STARTED);

                // Send the confirmation message
                target.SendChatMessage(Constants.COLOR_INFO + InfoRes.finished_call);
            }
        }

        private ContactModel GetContactFromId(int number, int contactId)
        {
            // Get the phone corresponding to the number
            PhoneModel phone = phoneList.Where(p => p.number == number).FirstOrDefault();

            if (phone != null)
            {
                // Get the contact matching the selected identifier
                return phone.contacts.Where(c => c.id == contactId).FirstOrDefault();
            }

            return null;
        }

        private int GetNumerFromContactName(string contactName, int playerPhone)
        {
            // Get the phone corresponding to the number
            PhoneModel phone = phoneList.Where(p => p.number == playerPhone).FirstOrDefault();

            if (phone != null)
            {
                // Get the contact matching the name
                ContactModel contactModel = phone.contacts.Where(c => c.contactName == contactName).FirstOrDefault();

                return contactModel == null ? 0 : contactModel.contactNumber;
            }

            return 0;
        }

        private List<ContactModel> GetTelephoneContactList(int number)
        {
            // Get the phone corresponding to the number
            PhoneModel phone = phoneList.Where(p => p.number == number).FirstOrDefault();

            return phone != null ? phone.contacts : new List<ContactModel>();
        }

        private string GetContactInTelephone(int phone, int number)
        {
            // Get the phone corresponding to the number
            PhoneModel phoneModel = phoneList.Where(p => p.number == phone).FirstOrDefault();

            if (phoneModel != null)
            {
                // Get the contact matching the name
                ContactModel contactModel = phoneModel.contacts.Where(c => c.contactNumber == number).FirstOrDefault();

                return contactModel == null ? string.Empty : contactModel.contactName;
            }

            return string.Empty;
        }

        [RemoteEvent("addNewContact")]
        public void AddNewContactEvent(Client player, int contactNumber, string contactName)
        {
            // Get the current player's phone
            PhoneModel phone = GetPlayerHoldingPhone(player);

            // Create the model for the new contact
            ContactModel contact = new ContactModel();
            {
                contact.owner = phone.number;
                contact.contactNumber = contactNumber;
                contact.contactName = contactName;
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Add contact to database
                    contact.id = Database.AddNewContact(contact);
                    phone.contacts.Add(contact);
                });
            });

            string actionMessage = string.Format(InfoRes.contact_created, contactName, contactNumber);
            player.SendChatMessage(Constants.COLOR_INFO + actionMessage);
        }

        [RemoteEvent("modifyContact")]
        public void ModifyContactEvent(Client player, int contactIndex, int contactNumber, string contactName)
        {
            // Get the player's phone
            PhoneModel phone = GetPlayerHoldingPhone(player);

            // Modify contact data
            ContactModel contact = GetContactFromId(phone.number, contactIndex);
            contact.contactNumber = contactNumber;
            contact.contactName = contactName;

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Modify the contact's data
                    Database.ModifyContact(contact);
                });
            });

            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.contact_modified);
        }

        [RemoteEvent("deleteContact")]
        public void DeleteContactEvent(Client player, int contactIndex)
        {
            // Get the player's phone
            PhoneModel phone = GetPlayerHoldingPhone(player);

            ContactModel contact = GetContactFromId(phone.number, contactIndex);
            string contactName = contact.contactName;
            int contactNumber = contact.contactNumber;

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Delete the contact
                    Database.DeleteContact(contactIndex);
                    phone.contacts.Remove(contact);
                });
            });

            string actionMessage = string.Format(InfoRes.contact_deleted, contactName, contactNumber);
            player.SendChatMessage(Constants.COLOR_INFO + actionMessage);
        }

        [RemoteEvent("sendPhoneMessage")]
        public void SendPhoneMessageEvent(Client player, int contactIndex, string textMessage)
        {
            // Get the player's phone
            PhoneModel phoneModel = GetPlayerHoldingPhone(player);

            ContactModel contact = GetContactFromId(phoneModel.number, contactIndex);

            // Get the phone owner
            Client target = SearchPhoneOwnerByNumber(contact.contactNumber);

            if (target == null || !target.Exists)
            {
                // There's no player matching the contact
                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.phone_disconnected);
                return;
            }

            string contactName = GetContactInTelephone(phoneModel.number, contact.contactNumber);

            if (contactName.Length == 0)
            {
                contactName = contact.contactNumber.ToString();
            }

            // Send the message to the target
            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.sms_sent);
            target.SendChatMessage(Constants.COLOR_INFO + "[" + GenRes.sms_from + contactName + "] " + textMessage);

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Add the SMS to the database
                    Database.AddSMSLog(phoneModel.number, contact.contactNumber, textMessage);
                });
            });
        }

        [Command(Commands.COM_CALL, Commands.HLP_PHONE_CALL_COMMAND, GreedyArg = true)]
        public void CallCommand(Client player, string called)
        {
            if (player.GetData(EntityData.PLAYER_PHONE_TALKING) != null || player.GetData(EntityData.PLAYER_CALLING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_phone_talking);
            }
            else
            {
                // Check if the player has a phone on his hand
                PhoneModel phone = GetPlayerHoldingPhone(player);

                if (phone == null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_telephone_hand);
                    return;
                }

                if (int.TryParse(called, out int number))
                {
                    string playerMessage = string.Empty;
                    List<Client> connectedPlayers = new List<Client>();

                    switch (number)
                    {
                        case Constants.NUMBER_POLICE:
                            // Get all the police members online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && Faction.IsPoliceMember(t)).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_POLICE);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_POLICE);
                            break;
                        case Constants.NUMBER_EMERGENCY:
                            // Get all the emergency members online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && t.GetData(EntityData.PLAYER_ON_DUTY) == 1 && t.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_EMERGENCY);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_EMERGENCY);
                            break;
                        case Constants.NUMBER_NEWS:
                            // Get all the news members online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && t.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_NEWS).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_NEWS);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_NEWS);
                            break;
                        case Constants.NUMBER_TAXI:
                            // Get all the taxi members online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && t.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_TAXI_DRIVER).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_TAXI);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.FACTION_TAXI_DRIVER);
                            break;
                        case Constants.NUMBER_FASTFOOD:
                            // Get all the fastfood deliverers online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && t.GetData(EntityData.PLAYER_ON_DUTY) == 1 && t.GetData(EntityData.PLAYER_JOB) == Constants.JOB_FASTFOOD).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_FASTFOOD);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.JOB_FASTFOOD + 100);
                            break;
                        case Constants.NUMBER_MECHANIC:
                            // Get all the mechanics online
                            connectedPlayers = NAPI.Pools.GetAllPlayers().Where(t => t.GetData(EntityData.PLAYER_PLAYING) != null && t.GetData(EntityData.PLAYER_ON_DUTY) == 1 && t.GetData(EntityData.PLAYER_JOB) == Constants.JOB_MECHANIC).ToList();
                            playerMessage = string.Format(InfoRes.calling, Constants.NUMBER_MECHANIC);
                            player.SetData(EntityData.PLAYER_CALLING, Constants.JOB_MECHANIC + 100);
                            break;
                        default:
                            // The player called a common number
                            if (number > 0)
                            {
                                // Get the target with the selected phone number
                                Client target = SearchPhoneOwnerByNumber(number);

                                if (target == null || !target.Exists)
                                {
                                    // The phone number doesn't exist
                                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.phone_disconnected);
                                    return;
                                }

                                // Check if the player has a contact name
                                string contact = GetContactInTelephone(number, phone.number);

                                if (contact.Length == 0)
                                {
                                    contact = phone.number.ToString();
                                }

                                // Make the player call
                                player.SetData(EntityData.PLAYER_CALLING, target);

                                // Check if the player calling is a contact into target's contact list
                                player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.calling, number));
                                target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.call_from, contact.Length > 0 ? contact : contact.ToString()));

                                return;
                            }

                            // The phone number doesn't exist
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.phone_disconnected);
                            return;
                    }

                    if (connectedPlayers.Count == 0)
                    {
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.line_occupied);
                        player.ResetData(EntityData.PLAYER_CALLING);
                    }
                    else
                    {
                        foreach (Client target in connectedPlayers)
                        {
                            // Send the message to each of the players
                            target.SendChatMessage(Constants.COLOR_INFO + InfoRes.central_call);
                        }


                        // Tell the player he's calling the number
                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                    }
                }
                else
                {
                    // Get the contacts phone number
                    int targetPhone = GetNumerFromContactName(called, phone.number);

                    // Get the player with the phone
                    Client target = SearchPhoneOwnerByNumber(targetPhone);

                    if (target == null || !target.Exists || target.GetData(EntityData.PLAYER_CALLING) != null || target.GetData(EntityData.PLAYER_PHONE_TALKING) != null)
                    {
                        // The contact player isn't online
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.phone_disconnected);
                        return;
                    }

                    // Store the call
                    player.SetData(EntityData.PLAYER_CALLING, target);

                    // Check if the player is in target's contact list
                    string contact = GetContactInTelephone(targetPhone, phone.number);

                    // Send the messages to both players
                    player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.calling, called));
                    target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.call_from, contact.Length > 0 ? contact : phone.number.ToString()));
                }
            }
        }

        [Command(Commands.COM_ANSWER)]
        public void AnswerCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_CALLING) != null || player.GetData(EntityData.PLAYER_PHONE_TALKING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_phone_talking);
                return;
            }

            // Get the players calling
            List<Client> callingPlayers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_CALLING) != null).ToList();

            foreach (Client target in callingPlayers)
            {
                if (target.GetData(EntityData.PLAYER_CALLING) is int)
                {
                    int factionJob = target.GetData(EntityData.PLAYER_CALLING);
                    int faction = player.GetData(EntityData.PLAYER_FACTION);
                    int job = player.GetData(EntityData.PLAYER_JOB);

                    if (factionJob == faction || (Faction.IsPoliceMember(player) && factionJob == Constants.FACTION_POLICE) || factionJob == job + 100)
                    {
                        // Link both players in the same call
                        target.ResetData(EntityData.PLAYER_CALLING);
                        player.SetData(EntityData.PLAYER_PHONE_TALKING, target);
                        target.SetData(EntityData.PLAYER_PHONE_TALKING, player);

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.call_received);
                        target.SendChatMessage(Constants.COLOR_INFO + InfoRes.call_taken);

                        return;
                    }
                }
                else if (target.GetData(EntityData.PLAYER_CALLING) == player)
                {
                    // Check if the player has a phone on his hand
                    PhoneModel phone = GetPlayerHoldingPhone(player);

                    if (phone == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_telephone_hand);
                        return;
                    }

                    // Link both players in the same call
                    target.ResetData(EntityData.PLAYER_CALLING);
                    player.SetData(EntityData.PLAYER_PHONE_TALKING, target);
                    target.SetData(EntityData.PLAYER_PHONE_TALKING, player);

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.call_received);
                    target.SendChatMessage(Constants.COLOR_INFO + InfoRes.call_taken);

                    // Store call starting time
                    target.SetData(EntityData.PLAYER_PHONE_CALL_STARTED, Globals.GetTotalSeconds());
                    return;
                }
            }

            // Nobody's calling the player
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_called);
        }

        [Command(Commands.COM_HANG)]
        public void HangCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_CALLING) != null)
            {
                // Hang up the call
                player.ResetData(EntityData.PLAYER_CALLING);
                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.finished_call);
            }
            else if (player.GetData(EntityData.PLAYER_PHONE_TALKING) != null)
            {
                // Get the player he's talking with
                Client target = player.GetData(EntityData.PLAYER_PHONE_TALKING);

                if(player.GetData(EntityData.PLAYER_PHONE_CALL_STARTED) != null || player.GetData(EntityData.PLAYER_PHONE_CALL_STARTED) != null)
                {
                    int elapsed = 0;
                    int started = 0;

                    // Get the phones from the players
                    int playerPhone = GetPlayerHoldingPhone(player).number;
                    int targetPhone = GetPlayerHoldingPhone(target).number;

                    if (player.GetData(EntityData.PLAYER_PHONE_CALL_STARTED) != null)
                    {
                        started = player.GetData(EntityData.PLAYER_PHONE_CALL_STARTED);
                    }
                    else
                    {
                        started = target.GetData(EntityData.PLAYER_PHONE_CALL_STARTED);
                    }

                    // Get phone call time
                    elapsed = Globals.GetTotalSeconds() - started;

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the elapsed time into the database
                            Database.AddCallLog(playerPhone, targetPhone, elapsed);
                        });
                    });
                }

                // Hang up the call for both players
                player.ResetData(EntityData.PLAYER_PHONE_TALKING);
                target.ResetData(EntityData.PLAYER_PHONE_TALKING);
                player.ResetData(EntityData.PLAYER_PHONE_CALL_STARTED);
                target.ResetData(EntityData.PLAYER_PHONE_CALL_STARTED);

                // Send the message to both players
                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.finished_call);
                target.SendChatMessage(Constants.COLOR_INFO + InfoRes.finished_call);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_phone_talking);
            }
        }

        [Command(Commands.COM_SMS, Commands.HLP_SMS_COMMAND, GreedyArg = true)]
        public void SmsCommand(Client player, int number, string message)
        {
            // Get the player's phone in hand
            PhoneModel phone = GetPlayerHoldingPhone(player);

            if (phone == null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_telephone_hand);
                return;
            }

            // Get the target player who has the phone
            Client target = SearchPhoneOwnerByNumber(number);

            if (target == null || !target.Exists)
            {
                // The phone doesn't exist
                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.phone_disconnected);
                return;
            }

            // Check if the player's in the contact list
            string contact = GetContactInTelephone(number, phone.number);

            if (contact.Length == 0)
            {
                contact = phone.number.ToString();
            }

            // Send the SMS warning to the player
            target.SendChatMessage(Constants.COLOR_INFO + "[" + GenRes.sms_from + contact + "] " + message);

            foreach (Client targetPlayer in NAPI.Pools.GetAllPlayers())
            {
                if (targetPlayer.Position.DistanceTo(player.Position) < 20.0f)
                {
                    // Send the message that the player is texting
                    targetPlayer.SendChatMessage(Constants.COLOR_CHAT_ME + string.Format(InfoRes.player_texting, player.Name));
                }
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Add the SMS into the database
                    Database.AddSMSLog(phone.number, number, message);
                });
            });
        }

        [Command(Commands.COM_CONTACTS, Commands.HLP_CONTACTS_COMMAND)]
        public void AgendaCommand(Client player, string action)
        {
            // Get the phone the player is holding
            PhoneModel phone = GetPlayerHoldingPhone(player);

            if (phone == null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_telephone_hand);
                return;
            }

            switch (action.ToLower())
            {
                case Commands.ARG_NUMBER:
                    //Sned the phone number to the player
                    player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.phone_number, phone.number));
                    break;
                case Commands.ARG_VIEW:
                    if (phone.contacts.Count > 0)
                    {
                        player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(phone.contacts), Constants.ACTION_LOAD);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.contact_list_empty);
                    }
                    break;
                case Commands.ARG_ADD:
                    player.TriggerEvent("addContactWindow", Constants.ACTION_ADD);
                    break;
                case Commands.ARG_MODIFY:
                    if (phone.contacts.Count > 0)
                    {
                        player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(phone.contacts), Constants.ACTION_RENAME);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.contact_list_empty);
                    }
                    break;
                case Commands.ARG_REMOVE:
                    if (phone.contacts.Count > 0)
                    {
                        player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(phone.contacts), Constants.ACTION_DELETE);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.contact_list_empty);
                    }
                    break;
                case Commands.ARG_SMS:
                    if (phone.contacts.Count > 0)
                    {
                        player.TriggerEvent("showPhoneContacts", NAPI.Util.ToJson(phone.contacts), Constants.ACTION_SMS);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.contact_list_empty);
                    }
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_CONTACTS_COMMAND);
                    break;
            }
        }
    }
}