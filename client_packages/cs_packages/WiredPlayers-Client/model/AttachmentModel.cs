using RAGE;
using RAGE.Elements;

namespace WiredPlayers_Client.model
{
    public class AttachmentModel
    {
        public int itemId { get; set; }
        public string hash { get; set; }
        public string bodyPart { get; set; }
        public Vector3 offset { get; set; }
        public Vector3 rotation { get; set; }
        public MapObject attach { get; set; }

        public AttachmentModel() { }

        public AttachmentModel(int itemId, string hash, string bodyPart, Vector3 offset, Vector3 rotation)
        {
            this.itemId = itemId;
            this.hash = hash;
            this.bodyPart = bodyPart;
            this.offset = offset;
            this.rotation = rotation;
        }

        public AttachmentModel(int itemId, string hash, string bodyPart, Vector3 offset, Vector3 rotation, MapObject attach)
        {
            this.itemId = itemId;
            this.hash = hash;
            this.bodyPart = bodyPart;
            this.offset = offset;
            this.rotation = rotation;
            this.attach = attach;
        }
    }
}
