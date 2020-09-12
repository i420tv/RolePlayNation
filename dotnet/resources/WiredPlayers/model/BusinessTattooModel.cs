namespace WiredPlayers.model
{
    public class BusinessTattooModel
    {
        public int slot { get; set; }
        public string name { get; set; }
        public string library { get; set; }
        public string maleHash { get; set; }
        public string femaleHash { get; set; }
        public int price { get; set; }

        public BusinessTattooModel(int slot, string name, string library, string maleHash, string femaleHash, int price)
        {
            this.slot = slot;
            this.name = name;
            this.library = library;
            this.maleHash = maleHash;
            this.femaleHash = femaleHash;
            this.price = price;
        }
    }
}
