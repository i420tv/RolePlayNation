using GTANetworkAPI;
using System;

namespace WiredPlayers.model
{
    public class PoliceControlModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int item { get; set; }
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public GTANetworkAPI.Object controlObject { get; set; }

        public PoliceControlModel() { }

        public PoliceControlModel(int id, string name, int item, Vector3 position, Vector3 rotation)
        {
            this.id = id;
            this.name = name;
            this.item = item;
            this.position = position;
            this.rotation = rotation;
        }

        public PoliceControlModel Copy()
        {
            PoliceControlModel policeControlModel = new PoliceControlModel();
            policeControlModel.id = id;
            policeControlModel.name = name;
            policeControlModel.item = item;
            policeControlModel.position = position;
            policeControlModel.rotation = rotation;
            return policeControlModel;
        }
    }
}
