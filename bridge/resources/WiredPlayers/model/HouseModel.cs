using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class HouseModel
    {
        public int id { get; set; }
        public string ipl { get; set; }
        public string name { get; set; }
        public Vector3 position { get; set; }
        public uint dimension { get; set; }
        public int price { get; set; }
        public string owner { get; set; }
        public int status { get; set; }
        public int tenants { get; set; }
        public int rental { get; set; }
        public bool locked { get; set; }
        public TextLabel houseLabel { get; set; }
    }
}
