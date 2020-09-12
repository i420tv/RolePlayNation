namespace WiredPlayers.model
{
    public class TunningPriceModel
    {
        public int slot { get; set; }
        public int products { get; set; }

        public TunningPriceModel(int slot, int products)
        {
            this.slot = slot;
            this.products = products;
        }
    }
}
