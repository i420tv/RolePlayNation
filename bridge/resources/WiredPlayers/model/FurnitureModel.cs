using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class FurnitureModel
    {
        public int id { get; set; }
        public uint hash { get; set; }
        public uint house { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public Object handle { get; set; }
    }
}
