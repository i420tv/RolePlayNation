using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.bank
{
    class Bank : Events.Script
    {
        public Bank()
        {
            Events.Add("showATM", ShowATMEvent);
            Events.Add("updateBankAccountMoney", UpdateBankAccountMoneyEvent);
            Events.Add("executeBankOperation", ExecuteBankOperationEvent);
            Events.Add("bankOperationResponse", BankOperationResponseEvent);
            Events.Add("loadPlayerBankBalance", LoadPlayerBankBalanceEvent);
            Events.Add("showPlayerBankBalance", ShowPlayerBankBalanceEvent);
            Events.Add("closeATM", CloseATMEvent);
        }

        private void ShowATMEvent(object[] args)
        {
            // Disable the chat
            Chat.Activate(false);
            Chat.Show(false);

            // Bank menu creation
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/bankMenu.html" });
        }

        private void UpdateBankAccountMoneyEvent(object[] args)
        {
            // Get player's bank balance
            int money = (int)Player.LocalPlayer.GetSharedData("PLAYER_BANK");

            // Update the balance on the screen
            Browser.ExecuteFunctionEvent(new object[] { "updateAccountMoney", money });
        }

        private void ExecuteBankOperationEvent(object[] args)
        {
            // Get the arguments received
            int operation = Convert.ToInt32(args[0]);
            int amount = Convert.ToInt32(args[1]);
            string target = args[2].ToString();

            // Execute a bank operation
            Events.CallRemote("executeBankOperation", operation, amount, target);
        }

        private void BankOperationResponseEvent(object[] args)
        {
            // Get the arguments received
            string response = args[0].ToString();

            // Check the action taken
            if(response == null || response.Length == 0)
            {
                // Go back on the screen
                Browser.ExecuteFunctionEvent(new object[] { "bankBack" });
            }
            else
            {
                // Show the error on the operation
                Browser.ExecuteFunctionEvent(new object[] { "showOperationError", response });
            }
        }

        private void LoadPlayerBankBalanceEvent(object[] args)
        {
            // Load player's bank balance
            Events.CallRemote("loadPlayerBankBalance");
        }

        private void ShowPlayerBankBalanceEvent(object[] args)
        {
            // Get the arguments received
            string operationJson = args[0].ToString();
            string playerName = args[1].ToString();

            // Show the player's bank operations
            Browser.ExecuteFunctionEvent(new object[] { "showBankOperations", operationJson, playerName });
        }

        private void CloseATMEvent(object[] args)
        {
            // Destroy the browser
            Browser.DestroyBrowserEvent(null);

            // Enable the chat
            Chat.Activate(true);
            Chat.Show(true);
        }
    }
}