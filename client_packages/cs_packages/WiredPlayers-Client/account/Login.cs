using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;

namespace WiredPlayers_Client.account
{
    class Login : Events.Script
    {
        public Login()
        {
            Events.Add("accountLoginForm", AccountLoginFormEvent);
            Events.Add("requestPlayerLogin", RequestPlayerLoginEvent);
            Events.Add("showLoginError", ShowLoginErrorEvent);
            Events.Add("clearLoginWindow", ClearLoginWindowEvent);

            RAGE.Game.Ui.DisplayHud(false);
            RAGE.Game.Ui.DisplayRadar(false);
            Chat.Activate(false);
            Chat.Show(false);
        }

        public static void AccountLoginFormEvent(object[] args)
        {
            // Create login window
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/login.html" });
        }

        private void RequestPlayerLoginEvent(object[] args)
        {
            // Get the password from the array
            string password = args[0].ToString();

            // Check for the credentials
            Events.CallRemote("loginAccount", password);
        }

        private void ShowLoginErrorEvent(object[] args)
        {
            // Show the message on the panel
            Browser.ExecuteFunctionEvent(new object[] { "showLoginError" });
        }

        private void ClearLoginWindowEvent(object[] args)
        {
            RAGE.Game.Pad.DisableControlAction(0, 12, true);
            RAGE.Game.Pad.DisableControlAction(0, 13, true);
            RAGE.Game.Pad.DisableControlAction(0, 14, true);
            RAGE.Game.Pad.DisableControlAction(0, 15, true);
            RAGE.Game.Pad.DisableControlAction(0, 16, true);
            RAGE.Game.Pad.DisableControlAction(0, 17, true);
          

            // Unfreeze the player
            Player.LocalPlayer.FreezePosition(true);
            Player.LocalPlayer.FreezeCameraRotation();

            Player.LocalPlayer.Position = new Vector3(-1356.546f, -465.3847f, 22.79196f);
            Player.LocalPlayer.SetRotation(0, 0, 90, 0, true);
            // Freeze the player until he logs in

            
           // Player.LocalPlayer.SetAlpha(0, false);

            // Show the message on the panel
            Browser.DestroyBrowserEvent(null);
        }
    }
}

