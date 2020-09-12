using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class GunModel
    {
        public WeaponHash weapon { get; set; }
        public string ammunition { get; set; }
        public int capacity { get; set; }

        public GunModel(WeaponHash weapon, string ammunition, int capacity)
        {
            this.weapon = weapon;
            this.ammunition = ammunition;
            this.capacity = capacity;
        }
    }
}
