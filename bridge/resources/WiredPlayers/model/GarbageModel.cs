using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class GarbageModel
    {
        public int route { get; set; }
        public int checkPoint { get; set; }
        public Vector3 position { get; set; }

        public GarbageModel(int route, int checkPoint, Vector3 position)
        {
            this.route = route;
            this.checkPoint = checkPoint;
            this.position = position;
        }
    }
}