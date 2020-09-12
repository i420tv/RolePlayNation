using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class InteriorModel
    {
        public string captionMessage { get; set; }
        public Vector3 entrancePosition { get; set; }
        public Vector3 exitPosition { get; set; }
        public string iplName { get; set; }
        public int blipId { get; set; }
        public Blip blip { get; set; }
        public TextLabel textLabel { get; set; }
        public string blipName { get; set; }

        public InteriorModel(string captionMessage, Vector3 entrancePosition, Vector3 exitPosition, string iplName, int blipId, string blipName)
        {
            this.captionMessage = captionMessage;
            this.entrancePosition = entrancePosition;
            this.exitPosition = exitPosition;
            this.iplName = iplName;
            this.blipId = blipId;
            this.blipName = blipName;
        }
    }
}
