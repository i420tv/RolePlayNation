namespace WiredPlayers_Client.model
{
    class ClothesModel
    {
        public int clothesId { get; set; }
        public string description { get; set; }
        public int textures { get; set; }
        public int bodyPart { get; set; }
        public int type { get; set; }
        public int slot { get; set; }
        public int drawable { get; set; }
        public int texture { get; set; }
        public int products { get; set; }

        public ClothesModel() { }

        public ClothesModel(int type, int slot, string description)
        {
            this.type = type;
            this.slot = slot;
            this.description = description;
        }
    }
}
