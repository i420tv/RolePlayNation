using System;

namespace WiredPlayers.model
{
    public class BusinessClothesModel
    {
        public int type { get; set; }
        public string description { get; set; }
        public int bodyPart { get; set; }
        public int clothesId { get; set; }
        public int texture { get; set; }
        public int sex { get; set; }
        public int products { get; set; }

        public BusinessClothesModel(int type, string description, int bodyPart, int clothesId, int sex, int products)
        {
            this.type = type;
            this.description = description;
            this.bodyPart = bodyPart;
            this.clothesId = clothesId;
            this.sex = sex;
            this.products = products;
        }
    }
}
