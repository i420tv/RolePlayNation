using System;

namespace WiredPlayers.model
{
    public class WeaponProbabilityModel
    {
        public int type { get; set; }
        public string hash { get; set; }
        public int amount { get; set; }
        public int minChance { get; set; }
        public int maxChance { get; set; }

        public WeaponProbabilityModel(int type, string hash, int amount, int minChance, int maxChance)
        {
            this.type = type;
            this.hash = hash;
            this.amount = amount;
            this.minChance = minChance;
            this.maxChance = maxChance;
        }
    }
}
