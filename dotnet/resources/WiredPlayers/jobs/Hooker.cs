using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.messages.success;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading;

namespace WiredPlayers.jobs
{
    public class Hooker : Script
    {
        public static Dictionary<int, Timer> sexTimerList;

        public Hooker()
        {
            // Initialize the variables
            sexTimerList = new Dictionary<int, Timer>();
        }

        public static void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (sexTimerList.TryGetValue(player.Value, out Timer sexTimer) == true)
            {
                sexTimer.Dispose();
                sexTimerList.Remove(player.Value);
            }
        }

        public static void OnSexServiceTimer(object playerObject)
        {
            NAPI.Task.Run(() =>
            {
                Client player = (Client)playerObject;
                Client target = player.GetData(EntityData.PLAYER_ALREADY_FUCKING);

                // We stop both animations
                player.StopAnimation();
                target.StopAnimation();

                // Health the player
                player.Health = 100;

                player.ResetData(EntityData.PLAYER_ANIMATION);
                player.ResetData(EntityData.HOOKER_TYPE_SERVICE);
                player.ResetData(EntityData.PLAYER_ALREADY_FUCKING);
                target.ResetData(EntityData.PLAYER_ALREADY_FUCKING);

                if (sexTimerList.TryGetValue(player.Value, out Timer sexTimer) == true)
                {
                    sexTimer.Dispose();
                    sexTimerList.Remove(player.Value);
                }

                // Send finish message to both players
                target.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.hooker_client_satisfied);
                player.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.hooker_service_finished);
            });
        }

        [Command(Commands.COM_SERVICE, Commands.HLP_HOOKER_SERVICE_COMMAND)]
        public void ServiceCommand(Client player, string service, int targetId, int price)
        {
            if (player.GetData(EntityData.PLAYER_JOB) != Constants.JOB_HOOKER)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_hooker);
            }
            else if (player.GetData(EntityData.PLAYER_ALREADY_FUCKING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_fucking);
            }
            else if (player.VehicleSeat != (int)VehicleSeat.RightFront)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_passenger);
            }
            else if(price <= 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
            }
            else
            {
                //NetHandle vehicle = player.Vehicle;
                Client target = Globals.GetPlayerById(targetId);

                NAPI.Util.ConsoleOutput(target.ToString());
                
                if (target.IsInVehicle && target.VehicleSeat == (int)VehicleSeat.Driver)

                if (target.VehicleSeat != (int)VehicleSeat.Driver)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.client_not_vehicle_driving);
                }
                else
                {
                    string playerMessage = string.Empty;
                    string targetMessage = string.Empty;

                    switch (service.ToLower())
                    {
                        case Commands.ARG_ORAL:
                            target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.HOOKER_TYPE_SERVICE, Constants.HOOKER_SERVICE_BASIC);

                            playerMessage = string.Format(InfoRes.oral_service_offer, target.Name, price);
                            targetMessage = string.Format(InfoRes.oral_service_receive, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        case Commands.ARG_SEX:
                            target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                            target.SetData(EntityData.JOB_OFFER_PRICE, price);
                            target.SetData(EntityData.HOOKER_TYPE_SERVICE, Constants.HOOKER_SERVICE_FULL);

                            playerMessage = string.Format(InfoRes.sex_service_offer, target.Name, price);
                            targetMessage = string.Format(InfoRes.sex_service_receive, player.Name, price);
                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                           target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_HOOKER_SERVICE_COMMAND);
                            break;
                    }
                }
            }
        }
    }
}


