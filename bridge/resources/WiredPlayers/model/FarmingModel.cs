using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class FarmingModel
    {
        public int Id { get; set; }

        public int itemIndex;
        public string itemName;
        public string itemDesc;
        public uint itemModelId;


        public ItemModel item;

        public Vector3 itemPosition;

        public TextLabel itemHud;
        public TextLabel Prompt;
        public GTANetworkAPI.Object itemObject;
        public bool itemFound;
    }
}

