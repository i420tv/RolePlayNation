using System;
using RAGE;
using RAGE.Ui;
using WiredPlayers_Client.globals;
using RAGE.Elements;
using RAGE.Game;

namespace WiredPlayers_Client.chat
{
    class Chat : Events.Script
    {
        public static bool Visible { get; private set; }
        public static bool Opened { get; private set; }
        public static bool Locked { get; private set; }
        public static HtmlWindow ChatBrowser { get; private set; }

        private string characters = null;




        public Chat()
        {
            Events.OnPlayerCommand += OnPlayerCommandEvent;
            Events.Add("changePlayerName", ChangePlayerName);

            // TODO More information about how chat works needed
            /*
            // Register the events
            Events.Add("toggleChatLock", ToggleChatLockEvent);
            Events.Add("toggleChatOpen", ToggleChatOpenEvent);

            // Create the custom chat
            ChatBrowser = new HtmlWindow("package://statics/html/chat.html");
            ChatBrowser.MarkAsChat();
            RAGE.Chat.Colors = true;

            // Lock the chat
            Locked = true;*/

            //Events.Add("aliasName", ChangeName);
        }

        public void ChangePlayerName(object[] args)
        {
        }


        private void ShowPlayerCharactersEvent(object[] args)
        {
            // Store account characters
            characters = args[0].ToString();

            // Show character list
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateCharacterList", characters });
        }



        private void OnPlayerCommandEvent(string cmd, Events.CancelEventArgs cancel)
        {
            // Temporary fix for Windows 7 users
            if (!Globals.playerLogged && cmd != "keyf")
            {
                // Send the message to the player
                Events.CallRemote("playerNotLoggedCommand");

                // Cancel the command
                cancel.Cancel = true;
            }
            else
            {
                int commandKey = -1;

                switch(cmd)
                {
                    case "key+":
                        commandKey = (int)ConsoleKey.Add;
                        break;
                    case "keyf":
                        commandKey = (int)ConsoleKey.F;
                        break;
                    case "keyk":
                        commandKey = (int)ConsoleKey.K;
                        break;
                    case "keyr":
                        commandKey = (int)ConsoleKey.R;
                        break;
                    case "keyf2":
                        commandKey = (int)ConsoleKey.F2;
                        break;
                    case "keyf3":
                        commandKey = (int)ConsoleKey.F3;
                        break;
                }

                if(commandKey >= 0)
                {
                    // Send the key command
                    Keys.FireKeyPressed(commandKey);
                }

                // Log the command
                Events.CallRemote("logPlayerCommand", cmd);
            }
        }

        public static void SetVisible(bool visible)
        {
            if (visible)
            {
                // Show the chat
            }
            else
            {
                // Hide the chat
                ChatBrowser.Active = false;
            }

            // Toggle the visible state
            Visible = visible;
        }

        public static void SetOpened(bool opened)
        {
            if (opened)
            {
                // Open the chat
                Cursor.Visible = true;
                RAGE.Chat.Show(true);
                RAGE.Chat.Activate(true);
                ChatBrowser.ExecuteJs("focusChat();");
            }
            else
            {
                // Close the chat
                Cursor.Visible = Browser.customBrowser != null;
                ChatBrowser.ExecuteJs("disableChatInput();");
            }

            // Toggle the open state
            Opened = opened;
        }

        public static void SetLocked(bool locked)
        {
            // Toggle the locked state
            Locked = locked;
        }

        private void ToggleChatLockEvent(object[] args)
        {
            // Get the locked state
            bool locked = Convert.ToBoolean(args[0]);

            // Change the state
            SetLocked(locked);
        }

        private void ToggleChatOpenEvent(object[] args)
        {
            // Get the locked state
            bool opened = Convert.ToBoolean(args[0]);

            // Change the state
            SetOpened(opened);
        }
    }
}
