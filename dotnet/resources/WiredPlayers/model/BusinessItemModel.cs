using GTANetworkAPI;

namespace WiredPlayers.model
{
    public class BusinessItemModel
    {
        public string description { get; set; }
        public string hash { get; set; }
        public int type { get; set; }
        public int products { get; set; }
        public float weight { get; set; }
        public int health { get; set; }
        public int uses { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public int business { get; set; }
        public float alcoholLevel { get; set; }

        public BusinessItemModel(string description, string hash, int type, int products, float weight, int health, int uses, Vector3 position, Vector3 rotation, int business, float alcoholLevel)
        {
            this.description = description;
            this.hash = hash;
            this.type = type;
            this.products = products;
            this.weight = weight;
            this.health = health;
            this.uses = uses;
            this.position = position;
            this.rotation = rotation;
            this.business = business;
            this.alcoholLevel = alcoholLevel;
        }
    }
}
