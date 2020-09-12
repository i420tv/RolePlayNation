using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.factions;
using WiredPlayers.messages.administration;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;

namespace WiredPlayers
{
    public class Main : Script
    {
        public Main()
        {
            //NAPI.Server.SetAutoSpawnOnConnect(false);
        }

        [RemoteEvent("fpsync.update")]
        public static void FingerPoint(Client sender, float camPitch, float camHeading)
        {
            NAPI.ClientEvent.TriggerClientEventInRange(sender.Position, 100, "fpsync.update", sender.Handle, camPitch, camHeading);
        }
        
    }
}
