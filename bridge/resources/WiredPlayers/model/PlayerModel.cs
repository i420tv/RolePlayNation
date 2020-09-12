using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class PlayerModel
    {
        public int id { get; set; }
        public string realName { get; set; }
        public string model { get; set; }
        public int adminRank { get; set; }
        public string adminName { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public int money { get; set; }
        public int bank { get; set; }
        public int health { get; set; }
        public int armor { get; set; }
        public int age { get; set; }
        public int sex { get; set; }
        public int faction { get; set; }
        public int job { get; set; }
        public int rank { get; set; }
        public int duty { get; set; }
        public int radio { get; set; }
        public int killed { get; set; }
        public string jailed { get; set; }
        public string carKeys { get; set; }
        public int documentation { get; set; }
        public string licenses { get; set; }
        public int insurance { get; set; }
        public int weaponLicense { get; set; }
        public int status { get; set; }
        public int played { get; set; }
        public int houseRent { get; set; }
        public int houseEntered { get; set; }
        public int businessEntered { get; set; }
        public int employeeCooldown { get; set; }
        public int jobCooldown { get; set; }
        public int jobDeliver { get; set; }
        public string jobPoints { get; set; }
        public int rolePoints { get; set; }
        public int informationCollected { get; set; }
        public int rareInformationCollected { get; set; }
        public int level { get; set; }
        public int vehicles { get; set; }
        public int houses { get; set; }
        public int businesses { get; set; }
        public int miningexp { get; set; }
        public int mininglevel { get; set; }
        public int truckingexp { get; set; }
        public int truckinglevel { get; set; }
        public int lockpickingexp { get; set; }
        public int lockpickinglevel { get; set; }
        public int firstaidexp { get; set; }
        public int firstaidlevel { get; set; }
        public int engineerexp { get; set; }
        public int engineerlevel { get; set; }
        public int fishingexp { get; set; }
        public int fishinglevel { get; set; }
        public int strenghtexp { get; set; }
        public int strenghtlevel { get; set; }
        public int staminaexp { get; set; }
        public int staminalevel { get; set; }
        public int postalexp { get; set; }
        public int postallevel { get; set; }
        public string policeunit { get; set; }
    }
}
