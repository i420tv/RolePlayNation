using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class OreModel
    {
        public int Id { get; set; }

        public int itemIndex;
        public string itemName;
        public string itemDesc;
        public uint itemModelId;


        public ItemModel item;

        public Vector3 itemPosition;

        public TextLabel itemHud;
        public GTANetworkAPI.Object itemObject;
        public bool itemFound;
    }
}

