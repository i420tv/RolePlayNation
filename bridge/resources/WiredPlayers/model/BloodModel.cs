using System;

namespace WiredPlayers.model
{
    public class BloodModel
    {
        public int id { get; set; }
        public int doctor { get; set; }
        public int patient { get; set; }
        public string type { get; set; }
        public bool used { get; set; }
    }
}
