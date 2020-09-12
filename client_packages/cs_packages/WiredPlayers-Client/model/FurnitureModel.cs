using RAGE;
using RAGE.Elements;

namespace WiredPlayers_Client.model
{
    class FurnitureModel
    {
        public int id { get; set; }
        public uint hash { get; set; }
        public uint house { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public MapObject handle { get; set; }

        public FurnitureModel() { }
    }
}
