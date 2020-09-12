using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.messages.success;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading;

namespace WiredPlayers.jobs
{/*
    public class Lawyer : Script
    {
        public static Dictionary<int, Timer> JailtalkTimerList;

        public Lawyer()
        {
            // Initialize the variables
            JailtalkTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (JailtalkTimerList.TryGetValue(player.Value, out Timer JailtalkTimer) == true)
            {
                JailtalkTimer.Dispose();
                JailtalkTimerList.Remove(player.Value);
            }
        }

        public static void OnLawServiceTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Client target = player.GetData(EntityData.PLAYER_LAWYERING);

                // We stop both animations
                player.StopAnimation();
                target.StopAnimation();

                // Health the player
                player.Health = 100;

                player.ResetData(EntityData.PLAYER_ANIMATION);
                player.ResetData(EntityData.LAWYER_TYPE_SERVICE);
                player.ResetData(EntityData.PLAYER_LAWYERING);
                target.ResetData(EntityData.PLAYER_LAWYERING);

                if (JailtalkTimerList.TryGetValue(player.Value, out Timer JailtalkTimer) == true)
                {
                    JailtalkTimer.Dispose();
                    JailtalkTimerList.Remove(player.Value);
                }

                // Send finish message to both players
                target.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.lawyer_client_satisfied);
                player.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.lawyer_service_finished);
            });
        }

        [Command(Commands.COM_LAWYER, Commands.HLP_LAWYER_SERVICE_COMMAND)]
        public void ServiceCommand(Client player, string service, Client target, string targetString, int price)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_LAWYER)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_lawyer);
            }
            else if (player.GetData(EntityData.PLAYER_LAWYERING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_lawyering);
            }
            else if(price <= 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
            }
            else
            
                {
                    string playerMessage = string.Empty;
                    string targetMessage = string.Empty;

                    switch (service.ToLower())
                    {
                        case Commands.ARG_OFFENDER:
                            target.SetData(EntityData.PLAYER_JOB_HELPER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.LAWYER_TYPE_SERVICE, Constants.LAWYER_SERVICE_BASIC);

                            playerMessage = string.Format(InfoRes.lawyer_service_offer, target.Name, price);
                            targetMessage = string.Format(InfoRes.lawyer_service_receive, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        case Commands.ARG_INNOCENT:
                            target.SetData(EntityData.PLAYER_JOB_HELPER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.LAWYER_TYPE_SERVICE, Constants.LAWYER_SERVICE_FULL);

                            playerMessage = string.Format(InfoRes.lawyer_service_offer, target.Name, price);
                            targetMessage = string.Format(InfoRes.lawyer_service_receive, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_LAWYER_SERVICE_COMMAND);
                            break;
                    }
                }
            }
        }*/
    }



