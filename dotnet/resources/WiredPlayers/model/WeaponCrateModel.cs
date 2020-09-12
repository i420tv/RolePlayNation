using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class WeaponCrateModel
    {
        public string contentItem { get; set; }
        public int contentAmount { get; set; }
        public Vector3 position { get; set; }
        public string carriedEntity { get; set; }
        public int carriedIdentifier { get; set; }
        public GTANetworkAPI.Object crateObject { get; set; }
    }
}
