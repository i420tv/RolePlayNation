namespace WiredPlayers_Client.model
{
    class Tattoo
    {
        public int player { get; set; }
        public string name { get; set; }
        public int slot { get; set; }
        public string library { get; set; }
        public string hash { get; set; }
        public string maleHash { get; set; }
        public string femaleHash { get; set; }
        public int price { get; set; }

        public Tattoo() { }
    }
}
