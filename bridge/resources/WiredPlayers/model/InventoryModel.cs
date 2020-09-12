using System;

namespace WiredPlayers.model
{
    public class InventoryModel
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public int amount { get; set; }
    }
}
