using System;

namespace WiredPlayers.model
{
    public class BankOperationModel
    {
        public string source { get; set; }
        public string receiver { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public string day { get; set; }
        public string time { get; set; }
    }
}
