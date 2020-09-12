using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.jobs
{
    class FastFood : Events.Script
    {
        private Blip fastFoodBlip = null;
        private Checkpoint fastFoodCheckpoint = null;

        public FastFood()
        {
            Events.Add("showFastfoodOrders", ShowFastfoodOrdersEvent);
            Events.Add("deliverFastfoodOrder", DeliverFastfoodOrderEvent);
            Events.Add("fastFoodDestinationCheckPoint", FastFoodDestinationCheckPointEvent);
            Events.Add("fastFoodDeliverBack", FastFoodDeliverBackEvent);
            Events.Add("fastFoodDeliverFinished", FastFoodDeliverFinishedEvent);

            Events.OnPlayerEnterCheckpoint += OnPlayerEnterCheckpoint;
        }

        private void ShowFastfoodOrdersEvent(object[] args)
        {
            // Get the variables from the array
            string orders = args[0].ToString();
            string distances = args[1].ToString();

            // Create the fastfood menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/sideMenu.html", "populateFastfoodOrders", orders, distances });
        }

        private void DeliverFastfoodOrderEvent(object[] args)
        {
            // Get the variables from the array
            int order = Convert.ToInt32(args[0]);

            // Destroy the menu
            Browser.DestroyBrowserEvent(null);

            // Close the menu and attend the order
            Events.CallRemote("takeFastFoodOrder", order);
        }

        private void FastFoodDestinationCheckPointEvent(object[] args)
        {
            // Get the variables from the array
            Vector3 position = (Vector3)args[0];

            // Create a blip on the map
            fastFoodBlip = new Blip(1, position, string.Empty, 1, 1);
            fastFoodCheckpoint = new Checkpoint(4, position, 2.5f, new Vector3(), new RGBA(198, 40, 40, 200));
        }

        private void FastFoodDeliverBackEvent(object[] args)
        {
            // Get the variables from the array
            Vector3 position = (Vector3)args[0];

            // Set the blip at the starting position
            fastFoodBlip.SetCoords(position.X, position.Y, position.Z);
            fastFoodCheckpoint.Position = position;
        }

        private void FastFoodDeliverFinishedEvent(object[] args)
        {
            // Destroy the checkpoint on the map
            fastFoodCheckpoint.Destroy();
            fastFoodCheckpoint = null;

            // Destroy the blip on the map
            fastFoodBlip.Destroy();
            fastFoodBlip = null;
        }

        private void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Events.CancelEventArgs cancel)
        {
            if (checkpoint == fastFoodCheckpoint)
            {
                // Deliver the order
                Events.CallRemote("fastfoodCheckpointReached");
            }
        }
    }
}
