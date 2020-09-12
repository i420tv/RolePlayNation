using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class AliasModel
    {
        public int id { get; set; }
        public int playerId { get; set; }
        public int targetId { get; set; }
        public string name { get; set; }       
    }
}
