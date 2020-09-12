using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.drivingschool;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WiredPlayers.townhall
{
    public class TownHall : Script
    {
        private TextLabel townHallTextLabel;

        public TownHall()
        {
            townHallTextLabel = NAPI.TextLabel.CreateTextLabel("/" + Commands.COM_TOWNHALL, new Vector3(-139.2177f, -631.8386f, 168.86f), 10.0f, 0.5f, 4, new Color(190, 235, 100), false, 0);
            NAPI.TextLabel.CreateTextLabel(GenRes.townhall_help, new Vector3(-139.2177f, -631.8386f, 168.76f), 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
        }

        [RemoteEvent("documentOptionSelected")]
        public void DocumentOptionSelectedEvent(Client player, int tramitation)
        {
            int money = player.GetSharedData(EntityData.PLAYER_MONEY);

            switch (tramitation)
            {
                case Constants.TRAMITATE_IDENTIFICATION:
                    if (player.GetData(EntityData.PLAYER_DOCUMENTATION) > 0)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_has_identification);
                    }
                    else if (money < Constants.PRICE_IDENTIFICATION)
                    {
                        string message = string.Format(ErrRes.player_not_identification_money, Constants.PRICE_IDENTIFICATION);
                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                    }
                    else
                    {
                        string message = string.Format(InfoRes.player_has_indentification, Constants.PRICE_IDENTIFICATION);
                        player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_IDENTIFICATION);
                        player.SetData(EntityData.PLAYER_DOCUMENTATION, Globals.GetTotalSeconds());
                        player.SendChatMessage(Constants.COLOR_INFO + message);


                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Log the payment made
                                Database.LogPayment(player.Name, GenRes.faction_townhall, GenRes.identification, Constants.PRICE_IDENTIFICATION);
                            });
                        });
                    }
                    break;
                case Constants.TRAMITATE_MEDICAL_INSURANCE:
                    if (player.GetData(EntityData.PLAYER_MEDICAL_INSURANCE) > Globals.GetTotalSeconds())
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_has_medical_insurance);
                    }
                    else if (money < Constants.PRICE_MEDICAL_INSURANCE)
                    {
                        string message = string.Format(ErrRes.player_not_medical_insurance_money, Constants.PRICE_MEDICAL_INSURANCE);
                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                    }
                    else
                    {
                        string message = string.Format(InfoRes.player_has_medical_insurance, Constants.PRICE_MEDICAL_INSURANCE);
                        player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_MEDICAL_INSURANCE);
                        player.SetData(EntityData.PLAYER_MEDICAL_INSURANCE, Globals.GetTotalSeconds() + 1209600);
                        player.SendChatMessage(Constants.COLOR_INFO + message);


                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Log the payment made
                                Database.LogPayment(player.Name, GenRes.faction_townhall, GenRes.medical_insurance, Constants.PRICE_MEDICAL_INSURANCE);
                            });
                        });
                    }
                    break;
                case Constants.TRAMITATE_TAXI_LICENSE:
                    if (DrivingSchool.GetPlayerLicenseStatus(player, Constants.LICENSE_TAXI) > 0)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_has_taxi_license);
                    }
                    else if (money < Constants.PRICE_TAXI_LICENSE)
                    {
                        string message = string.Format(ErrRes.player_not_taxi_license_money, Constants.PRICE_TAXI_LICENSE);
                        player.SendChatMessage(Constants.COLOR_ERROR + message);
                    }
                    else
                    {
                        string message = string.Format(InfoRes.player_has_taxi_license, Constants.PRICE_TAXI_LICENSE);
                        player.SetSharedData(EntityData.PLAYER_MONEY, money - Constants.PRICE_TAXI_LICENSE);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                        DrivingSchool.SetPlayerLicense(player, Constants.LICENSE_TAXI, 1);


                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Log the payment made
                                Database.LogPayment(player.Name, GenRes.faction_townhall, GenRes.taxi_license, Constants.PRICE_TAXI_LICENSE);
                            });
                        });
                    }
                    break;
                case Constants.TRAMITATE_FINE_LIST:
                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            List<FineModel> fineList = Database.LoadPlayerFines(player.Name);
                            if (fineList.Count > 0)
                            {
                                player.TriggerEvent("showPlayerFineList", NAPI.Util.ToJson(fineList));
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_no_fines);
                            }
                        });
                    });
                    break;
            }
        }

        [RemoteEvent("payPlayerFines")]
        public void PayPlayerFinesEvent(Client player, string finesJson)
        {
            List<FineModel> fineList = Database.LoadPlayerFines(player.Name);
            List<FineModel> removedFines = NAPI.Util.FromJson<List<FineModel>>(finesJson);
            int money = player.GetSharedData(EntityData.PLAYER_MONEY);
            int amount = 0;

            foreach (FineModel fine in removedFines)
            {
                // Get the money amount for all the fines
                amount += fine.amount;
            }

            if (amount == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_fines);
            }
            else if (amount > money)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + string.Format(ErrRes.player_not_fine_money, amount));
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // Remove money from player
                        player.SetSharedData(EntityData.PLAYER_MONEY, money - amount);

                        // Check if all fines were paid
                        if (removedFines.Count == fineList.Count)
                        {
                            // Volvemos a la página anterior
                            player.TriggerEvent("backTownHallIndex");
                        }

                        // Send the message to the player
                        player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.player_fines_paid, amount));

                        // Delete paid fines
                        Database.RemoveFines(removedFines);
                        Database.LogPayment(player.Name, GenRes.faction_townhall, GenRes.fines_payment, amount);
                    });
                });
            }
        }

        [Command(Commands.COM_TOWNHALL)]
        public void TownHallCommand(Client player)
        {
            if (player.Position.DistanceTo(townHallTextLabel.Position) < 2.0f)
            {
                player.TriggerEvent("showTownHallMenu");
            }
            else
            {
                // Player is not in the town hall
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_townhall);
            }
        }
    }
}