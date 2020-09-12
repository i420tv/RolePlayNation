using System;
using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class ScoreModel
    {
        public int playerId { get; set; }
        public string playerName { get; set; }
        public int playerPing { get; set; }       

        public ScoreModel(int playerId, string playerName, int playerPing)
        {            
            this.playerId = playerId;
            this.playerName = playerName;
            this.playerPing = playerPing;
        }
    }
}
