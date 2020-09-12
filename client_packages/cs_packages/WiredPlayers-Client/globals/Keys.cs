using RAGE;
using RAGE.Ui;
using RAGE.Elements;
using System.Collections.Generic;
using WiredPlayers_Client.factions;
using WiredPlayers_Client.weapons;
using WiredPlayers_Client.vehicles;
using System;
using System.Drawing;

namespace WiredPlayers_Client.globals
{
    class Keys : Events.Script
    {
        public static Keys instance;

        private static readonly int KEY_PRESS_TIME = 350000;
        private static Dictionary<int, long> pressedKeys;
        private static List<int> consoleKeys;

        public int UI = 0;
        public int CC = 0;
        public bool IsInputLocked;


        public Keys()
        {
            instance = this;
            // Initialize the dictionary
            pressedKeys = new Dictionary<int, long>();

            // Bind the required Keys
            BindConsoleKeys();

            RAGE.Events.Add("LockInput", LockInput);
            RAGE.Events.Add("UnlockInput", UnlockInput);

        }

        public void LockInput(object[] args)
        {            
            IsInputLocked = true;

        }
        public void UnlockInput(object[] args)
        {
            IsInputLocked = false;

        }

        public static int DetectPressedKey(long currentTicks)
        {
            // Check the first released key
            int releasedKey = -1;

            // Check if the keys are loaded and player has not opened a CEF instance
            if (consoleKeys == null || Browser.customBrowser != null) return releasedKey;

            foreach (int key in consoleKeys)
            {
                if (pressedKeys.TryGetValue(key, out long downTicks))
                {
                    // If there's already a key released we do nothing
                    if (releasedKey >= 0) continue;

                    // Check if the key is already up
                    if (!Input.IsDown(key) && (currentTicks - downTicks) > KEY_PRESS_TIME)
                    {
                        releasedKey = key;
                        pressedKeys.Remove(releasedKey);
                    }
                }
                else if (Input.IsDown(key))
                {
                    // Store the key into the dictionary
                    pressedKeys.Add(key, currentTicks);
                }
            }
            return releasedKey;
        }


        /// <summary>
        /// TRY TO DISABLE THESE KEYS WHEN THE CHAT WINDOW IS ENABLED
        /// </summary>
        /// <param name="key"></param>
        public static void FireKeyPressed(int key)
        {
            if (instance.IsInputLocked)
                return;

            switch (key)
            {
                case (int)ConsoleKey.Add:
                    if (Player.LocalPlayer.Vehicle == null && !Police.handcuffed && !Emergency.dead)
                    {
                        // Reset the player's animation
                        Events.CallRemote("checkPlayerEventKeyStopAnim");
                    }
                    break;
                case (int)ConsoleKey.R:
                    if (Player.LocalPlayer.Vehicle == null && !Police.handcuffed)
                    {
                        int weapon = 0;
                        Player.LocalPlayer.GetCurrentWeapon(ref weapon, true);

                        if (weapon > 0 && !Player.LocalPlayer.IsReloading() && Weapons.IsValidWeapon(weapon))
                        {
                            int ammo = 0;
                            Player.LocalPlayer.GetAmmoInClip((uint)weapon, ref ammo);

                            // Reload the weapon
                            Events.CallRemote("reloadPlayerWeapon", ammo);
                        }
                    }
                    break;
                case (int)ConsoleKey.F3:
                    if (!Globals.viewingPlayers)
                    {
                        // Change the flag
                        Globals.viewingPlayers = true;

                        // Create the player list browser
                        Browser.CreateBrowserEvent(new object[] { "package://statics/html/playerList.html" });
                    }
                    break;

                case (int)ConsoleKey.E:
                    if (!Globals.viewingPlayers)
                    { 

                        // Create the player list browser
                        Events.CallRemote("actionkeyE");
                    }
                    break;
                case (int)ConsoleKey.L:
                     if (Player.LocalPlayer.Vehicle == null)
                         return;

                     if (!Player.LocalPlayer.Vehicle.IsSeatFree(-1, 0) && Player.LocalPlayer.Vehicle.GetPedInSeat(-1, 0) == Player.LocalPlayer.Handle)
                     {
                         if (Keys.instance.CC == 0)
                         {
                             Events.CallRemote("toggleCC", 1, Vehicles.instance.calculatedSpeed);
                             Keys.instance.CC = 1;
                             return;
                         }
                         if (Keys.instance.CC == 1)
                         {
                             Events.CallRemote("toggleCC", 0, Vehicles.instance.calculatedSpeed);
                             Keys.instance.CC = 0;
                             return;
                         }
                     }
                     break;
                case (int)ConsoleKey.F10:
                    if (!Globals.viewingPlayers)
                    {
                        if (Keys.instance.UI == 0)
                        {
                            RAGE.Game.Ui.DisplayHud(false);
                            RAGE.Game.Ui.DisplayRadar(false);
                            Chat.Show(false);
                            Chat.Activate(false);
                            Keys.instance.UI = 1;
                            return;
                        }
                        if (Keys.instance.UI == 1)
                        {
                            RAGE.Game.Ui.DisplayHud(true);
                            RAGE.Game.Ui.DisplayRadar(true);
                            Chat.Show(true);
                            Chat.Activate(true);
                            Keys.instance.UI = 0;
                            return;
                        }
                    }
                    break;

            }
        }
        private void BindConsoleKeys()
        {
            // Initialize the list
            consoleKeys = new List<int>()
            {
                (int)ConsoleKey.O,
                (int)ConsoleKey.Add,
                (int)ConsoleKey.R,
                (int)ConsoleKey.F3,
                (int)ConsoleKey.F10,
                (int)ConsoleKey.L,
            };

        }
    }
}
