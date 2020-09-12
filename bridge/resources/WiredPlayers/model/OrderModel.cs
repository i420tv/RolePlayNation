using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class OrderModel
    {
        public int id { get; set; }
        public Vector3 position { get; set; }
        public float distance { get; set; }
        public double limit { get; set; }
        public bool taken { get; set; }
    }
}
