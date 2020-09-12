namespace WiredPlayers.model
{
    public class BusinessBlipModel
    {
        public int type { get; set; }
        public uint blip { get; set; }

        public BusinessBlipModel(int type, uint blip)
        {
            this.type = type;
            this.blip = blip;
        }
    }
}
