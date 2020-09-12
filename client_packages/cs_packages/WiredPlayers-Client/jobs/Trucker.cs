using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System.Collections.Generic;
using System.Linq;

namespace WiredPlayers_Client.jobs
{
    class Trucker : Events.Script
    {
        private Blip jobLocation = null;
        private Blip joblocationScrapyard = null;
        private Blip TruckerOrderBlip = null;
        private Checkpoint TruckerOrderCheckpoint = null;

        public Trucker()
        {
            Events.Add("showTruckerJobUi", ShowTruckerJobUiEvent);
            Events.Add("createTruckerCrates", CreateTruckerCratesEvent);
            Events.Add("TruckerJob1", TruckerJob1);
            jobLocation = new Blip(632, new Vector3(-44.04705f, -2519.901f, 7.394626f), "Truckers Union", 1, 44, 255, 0, true);

            CreateJobLocationAdditions();
        }
        private void ShowTruckerJobUiEvent(object[] args)
        {

            // Create the fastfood menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/Trucker.html" });
        }
        public void CreateJobLocationAdditions()
        {
            RAGE.Elements.Ped myPed = new RAGE.Elements.Ped(349680864, new Vector3(-44.04705f, -2519.901f, 7.394626f), 90, 0);
            RAGE.Elements.Ped myPed2 = new RAGE.Elements.Ped(815693290, new Vector3(-126.7936f, -2530.553f, 6.09571f), 180, 0);

        }
        private static List<MapObject> crateList;

        public static void CheckPlayerStoredCrate()
        {
            if(crateList != null && crateList.Count > 0 && RAGE.Game.Misc.GetHashKey("forklift") == Player.LocalPlayer.Vehicle.Model) {
                // Check if the player has any crate near
                if (GetCrateInRange(Player.LocalPlayer.Vehicle, 1.5f) == null) return;

                // Store the crate into the closest vehicle
                Vehicle truck = StoreCrateIntoVehicle("mule");

                if(truck != null)
                {
                    
                }
            }
            else
            {
                Player.LocalPlayer.Vehicle.SetDoorOpen(2, false, false);
                Player.LocalPlayer.Vehicle.SetDoorOpen(3, false, false);
            }
        }
        private void TruckerJob1(object[] args)
        {
            // Get the variables from the array


            // Create a blip on the map
            TruckerOrderBlip = new Blip(1, new Vector3(768.5526f, -1866.732f, 28.97363f), string.Empty, 1, 1);
            TruckerOrderCheckpoint = new Checkpoint(4, new Vector3(768.5526f, -1866.732f, 28.97363f), 2.5f, new Vector3(), new RGBA(198, 40, 40, 200));
            RAGE.Game.Ui.SetBlipRoute(1, true);
            RAGE.Game.Ui.SetBlipRouteColour(1, 28);
        }

        private void CreateTruckerCratesEvent(object[] args)
        {
            // Initialize the crates
            crateList = new List<MapObject>();

            foreach (Vector3 crate in Constants.TRUCKER_CRATES)
            {
                MapObject crateObject = new MapObject(RAGE.Game.Misc.GetHashKey("prop_boxpile_04a"), crate, new Vector3(0.0f, 0.0f, 0.0f));
                crateObject.SetPhysicsParams(17.5f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f);
                crateObject.SetActivatePhysicsAsSoonAsItIsUnfrozen(true);
                crateObject.FreezePosition(false);

                // Add the crate to the list
                crateList.Add(crateObject);
            }
        }

        private static Vehicle StoreCrateIntoVehicle(string model, float distance = 2.5f)
        {
            uint vehicleHash = RAGE.Game.Misc.GetHashKey(model);

            // Get the list of vehicles with selected model and trunk opened
            List<Vehicle> truckList = Entities.Vehicles.Streamed.Where(veh => veh.IsDoorFullyOpen(2) && veh.Model == vehicleHash).ToList();

            foreach(Vehicle truck in truckList)
            {
                // Get the closest crate
                MapObject crate = GetCrateInRange(truck, distance);

                if (crate != null)
                {
                    // Remove the crate from the game
                    crateList.Remove(crate);
                    crate.Destroy();

                    return truck;
                }
            }

            return null;
        }

        private static MapObject GetCrateInRange(Vehicle truck, float distance)
        {
            foreach(MapObject crate in crateList)
            {
                if(crate.Position.DistanceTo(truck.Position) <= distance)
                {
                    // We found the crate the player is storing
                    return crate;
                }
            }

            return null;
        }
    }
}
