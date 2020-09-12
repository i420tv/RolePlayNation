using System;
using System.Collections.Generic;
using System.Text;

namespace DavWebCreator.Server.Models.Dummys
{
    public class VehicleDummy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxWeight { get; set; }
        public int CurrentWeight { get; set; }
        public int Fuel { get; set; }
        public int MaxFuel { get; set; }
        public bool IsAvailable { get; set; }

        public VehicleDummy(int id, string name, int maxWeight, int weight, int fuel, int maxFuel, bool isAvailable)
        {
            this.Id = id;
            this.Name = name;
            this.MaxWeight = maxWeight;
            this.CurrentWeight = weight;
            this.Fuel = fuel;
            this.MaxFuel = maxFuel;
            this.IsAvailable = isAvailable;
        }
    }
}
