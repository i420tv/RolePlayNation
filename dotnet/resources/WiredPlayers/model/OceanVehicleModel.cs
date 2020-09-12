using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class OceanVehicleModel
    {
        public int id { get; set; }
        public string owner { get; set; }
        public int scrap { get; set; }
        public uint model { get; set; }
        public int scrapValue { get; set; }
    }
}
