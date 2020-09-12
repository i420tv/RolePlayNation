using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class FactionWarningModel
    {
        public int faction { get; set; }
        public int playerId { get; set; }
        public string place { get; set; }
        public Vector3 position { get; set; }
        public int takenBy { get; set; }
        public string hour { get; set; }

        public FactionWarningModel(int faction, int playerId, string place, Vector3 position, int takenBy, string hour)
        {
            this.faction = faction;
            this.playerId = playerId;
            this.place = place;
            this.position = position;
            this.takenBy = takenBy;
            this.hour = hour;
        }
    }
}
