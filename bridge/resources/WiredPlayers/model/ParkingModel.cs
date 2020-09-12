using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class ParkingModel
    {
        public int id { get; set; }
        public int type { get; set; }
        public int houseId { get; set; }
        public Vector3 position { get; set; }
        public int capacity { get; set; }
        public TextLabel parkingLabel { get; set; }
    }
}
