namespace WiredPlayers_Client.model
{
    class Procedure
    {
        public string desc { get; set; }
        public int price { get; set; }

        public Procedure(string desc, int price)
        {
            this.desc = desc;
            this.price = price;
        }
    }
}
