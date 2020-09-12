using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class PlantModel
    {
        public int Id { get; set; }
        public Vector3 Position { get; set; }
        public uint Dimension { get; set; }
        public int GrowTime { get; set; }
        public Object Object { get; set; }
        public TextLabel Progress { get; set; }
        public ColShape PlantColshape { get; set; }
        public int itemIndex;
        public string itemName;
        public string itemDesc;
        public uint itemModelId;


        public ItemModel item;

        public Vector3 itemPosition;

        public TextLabel itemHud;
        public TextLabel itemHud2;
        public Object itemObject;
        public bool itemFound;



    }
}
