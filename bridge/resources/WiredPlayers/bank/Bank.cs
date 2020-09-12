using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using System.Collections.Generic;
using System.Threading.Tasks;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;

namespace WiredPlayers.bank
{
    public class Bank : Script
    {
        [RemoteEvent("executeBankOperation")]
        public void ExecuteBankOperationEvent(Client player, int operation, int amount, string targetName)
        {
            // Throw an error if the amount is less than zero.
            if (amount <= 0)
            {
                TriggerLocalResponse(player, ErrRes.bank_general_error);
                return;
            }

            // Figure out the action of the bank operation.
            switch (operation)
            {
                case Constants.OPERATION_WITHDRAW:
                    WithdrawFromBank(player, amount);
                    return;
                case Constants.OPERATION_DEPOSIT:
                    DepositToBank(player, amount);
                    return;
                case Constants.OPERATION_TRANSFER:
                    TransferFromBank(player, amount, targetName);
                    return;
                case Constants.OPERATION_BALANCE:
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// Update the player's shared data for money, bank, etc. Setting money to -99 will not update the money for the target.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bank"></param>
        /// <param name="money"></param>
        private void UpdatePlayerMoney(Client player, int bank, int money = -99)
        {
            player.SetSharedData(EntityData.PLAYER_BANK, bank);

            if (money == -99)
                return;

            player.SetSharedData(EntityData.PLAYER_MONEY, money);
        }

        /// <summary>
        /// Trigger a client event that tells the player what happened.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="response"></param>
        private void TriggerLocalResponse(Client player, string response)
        {
            player.TriggerEvent("bankOperationResponse", response);
        }

        /// <summary>
        /// Withdraws from the bank based on the player's input.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="bank"></param>
        /// <param name="amount"></param>
        private void WithdrawFromBank(Client player, int amount)
        {
            // Get Player Money, Bank Info, Name, etc.
            int bank = player.GetSharedData(EntityData.PLAYER_BANK);
            int money = player.GetSharedData(EntityData.PLAYER_MONEY);
            string name = player.GetData(EntityData.PLAYER_NAME);

            // If the bank has less than the amount requested. Throw an error.
            if (bank < amount)
            {
                TriggerLocalResponse(player, ErrRes.bank_not_enough_money);
                return;
            }

            // Update data and then push it to the player's shared data.
            bank -= amount;
            money += amount;
            UpdatePlayerMoney(player, bank, money);

            // Log the transaction to the database.
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    Database.LogPayment("ATM", name, GenRes.bank_op_withdraw, amount);
                });
            });
        }

        /// <summary>
        /// Deposit money into the bank based on exceeding amount or whatever amount the player provides.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="amount"></param>
        private void DepositToBank(Client player, int amount)
        {
            // Get Player Money, Bank Info, Name, etc.
            int bank = player.GetSharedData(EntityData.PLAYER_BANK);
            int money = player.GetSharedData(EntityData.PLAYER_MONEY);
            string name = player.GetData(EntityData.PLAYER_NAME);

            // If the player's money is less than the amount he wants to deposit. Just move all of the player's money into the bank.
            if (money < amount)
            {
                bank += money;
                UpdatePlayerMoney(player, bank, money);

                // We log the transaction into the database
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        Database.LogPayment(name, "ATM", GenRes.bank_op_deposit, money);
                    });
                });
                return;
            }

            // If the amount is less than the player's money deposit the amount instead.
            bank += amount;
            money -= amount;
            UpdatePlayerMoney(player, bank, money);

            // We log the transaction into the database
            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    Database.LogPayment("ATM", name, GenRes.bank_op_deposit, amount);
                });
            });
        }

        private void TransferFromBank(Client player, int amount, string targetPlayer)
        {
            // Get Player Money, Bank Info, Name, etc.
            int bank = player.GetSharedData(EntityData.PLAYER_BANK);
            int money = player.GetSharedData(EntityData.PLAYER_MONEY);
            string name = player.GetData(EntityData.PLAYER_NAME);

            // If the bank has less than the amount requested. End it here.
            if (bank < amount)
            {
                TriggerLocalResponse(player, ErrRes.bank_not_enough_money);
                return;
            }
            
            // Check if the account exists before we begin transferring.
            if (Database.FindCharacter(targetPlayer) != true)
            {
                TriggerLocalResponse(player, ErrRes.bank_account_not_found);
                return;
            }

            Client target = NAPI.Pools.GetAllPlayers().Find(x => x.Name == targetPlayer);

            // If the target player is the client transferring stop it.
            if (target == player)
            {
                TriggerLocalResponse(player, ErrRes.transfer_money_own);
                return;
            }

            // If the target is null they're probably not on the server currently.
            if (target == null)
            {
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        bank -= amount;
                        UpdatePlayerMoney(player, bank, money);
                        Database.TransferMoneyToPlayer(targetPlayer, amount);
                        Database.LogPayment(name, targetPlayer, GenRes.bank_op_transfer, amount);
                    });
                });
                return;
            }

            if (target.GetData(EntityData.PLAYER_PLAYING) != null)
            {
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        int targetBank = target.GetSharedData(EntityData.PLAYER_BANK);
                        targetBank += amount;
                        bank -= amount;
                        UpdatePlayerMoney(player, bank, money);
                        UpdatePlayerMoney(target, targetBank);
                    });
                });
            }
        }

        [RemoteEvent("loadPlayerBankBalance")]
        public void LoadPlayerBankBalanceEvent(Client player)
        {
            Task.Factory.StartNew(() => {
                NAPI.Task.Run(() =>
                {
                    // Show the bank operations for the player
                    List<BankOperationModel> operations = Database.GetBankOperations(player.Name, 1, Constants.MAX_BANK_OPERATIONS);
                    player.TriggerEvent("showPlayerBankBalance", NAPI.Util.ToJson(operations), player.Name);
                });
            });
        }
    }
}
