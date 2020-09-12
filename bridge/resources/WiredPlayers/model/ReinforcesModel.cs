using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class ReinforcesModel
    {
        public int playerId { get; set; }
        public Vector3 position { get; set; }

        public ReinforcesModel(int playerId, Vector3 position)
        {
            this.playerId = playerId;
            this.position = position;
        }
    }
}
