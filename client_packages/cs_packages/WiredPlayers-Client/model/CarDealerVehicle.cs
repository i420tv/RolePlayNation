namespace WiredPlayers_Client.model
{
    class CarDealerVehicle
    {
        public string model { get; set; }
        public string hash { get; set; }
        public int carShop { get; set; }
        public int type { get; set; }
        public int speed { get; set; }
        public int price { get; set; }

        public CarDealerVehicle() { }

        public CarDealerVehicle(string model, string hash, int carShop, int type, int price)
        {
            this.model = model;
            this.hash = hash;
            this.carShop = carShop;
            this.type = type;
            this.price = price;
        }
    }
}
