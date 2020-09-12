using RAGE;
using Newtonsoft.Json;
using WiredPlayers_Client.model;
using WiredPlayers_Client.globals;
using System.Collections.Generic;
using System;

namespace WiredPlayers_Client.character
{
    class Telephone : Events.Script
    {
        private int action;
        private int contact;
        private List<Contact> contactsList;

        public Telephone()
        {
            Events.Add("showPhoneContacts", ShowPhoneContactsEvent);
            Events.Add("addContactWindow", AddContactWindowEvent);
            Events.Add("preloadContactData", PreloadContactDataEvent);
            Events.Add("setContactData", SetContactDataEvent);
            Events.Add("executePhoneAction", ExecutePhoneActionEvent);
            Events.Add("sendPhoneMessage", SendPhoneMessageEvent);
            Events.Add("cancelPhoneMessage", CancelPhoneMessageEvent);
        }

        private void ShowPhoneContactsEvent(object[] args)
        {
            // Get the variables from the arguments
            string contactsJson = args[0].ToString();

            // Store the values
            action = Convert.ToInt32(args[1]);
            contactsList = JsonConvert.DeserializeObject<List<Contact>>(contactsJson);

            // Show the list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateContactsMenu", contactsJson, action });
        }

        private void AddContactWindowEvent(object[] args)
        {
            // Store the action
            action = Convert.ToInt32(args[0]);

            // Show the menu to add a contact
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/addPhoneContact.html" });
        }

        private void PreloadContactDataEvent(object[] args)
        {
            if (contact > 0)
            {
                // Load contact's data
                int number = contactsList[contact].contactNumber;
                string name = contactsList[contact].contactName;

                // Show the data on the browser
                Browser.ExecuteFunctionEvent(new object[] { "populateContactData", number, name });
            }
        }

        private void SetContactDataEvent(object[] args)
        {
            // Get the variables from the arguments
            int number = Convert.ToInt32(args[0]);
            string name = args[1].ToString();

            // Destroy the web browser
            Browser.DestroyBrowserEvent(null);

            if (action == 4)
            {
                // Create new contact
                Events.CallRemote("addNewContact", number, name);
            }
            else
            {
                // Modify the contact data
                Events.CallRemote("modifyContact", contact, number, name);
            }
        }

        private void ExecutePhoneActionEvent(object[] args)
        {
            // Get the variables from the arguments
            int contactIndex = Convert.ToInt32(args[0]);

            // Get the selected contact
            contact = contactsList[contactIndex].id;

            // Destroy the web browser
            Browser.DestroyBrowserEvent(null);

            switch (action)
            {
                case 2:
                    // Load contact's data
                    int number = contactsList[contactIndex].contactNumber;
                    string name = contactsList[contactIndex].contactName;

                    // Modify a contact
                    Browser.CreateBrowserEvent(new object[] { "package://statics/html/addPhoneContact.html", "populateContactData", number, name });

                    break;
                case 3:
                    // Delete a contact
                    Events.CallRemote("deleteContact", contact);

                    break;
                case 5:
                    // Send SMS to a contact
                    Browser.CreateBrowserEvent(new object[] { "package://statics/html/sendContactMessage.html" });

                    break;
            }
        }

        private void SendPhoneMessageEvent(object[] args)
        {
            // Destroy the web browser
            Browser.DestroyBrowserEvent(null);

            // Send the SMS to the target
            Events.CallRemote("sendPhoneMessage", contact, args[0].ToString());
        }

        private void CancelPhoneMessageEvent(object[] args)
        {
            // Destroy the web browser
            Browser.DestroyBrowserEvent(null);

            // Show the list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateContactsMenu", JsonConvert.SerializeObject(contactsList), action });
        }
    }
}
