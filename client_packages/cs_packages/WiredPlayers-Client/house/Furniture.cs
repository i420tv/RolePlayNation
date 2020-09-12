using RAGE;
using RAGE.Ui;
using Newtonsoft.Json;
using WiredPlayers_Client.model;
using System.Collections.Generic;

namespace WiredPlayers_Client.house
{
    class Furniture : Events.Script
    {
        private bool movingFurniture = false;
        private FurnitureModel selectedFurniture = null;
        private List<FurnitureModel> furnitureList = null;

        public Furniture()
        {
            Events.Add("moveFurniture", MoveFurnitureEvent);
            Events.OnClick += ClickEvent;
        }

        private void MoveFurnitureEvent(object[] args)
        {
            // Get the furniture available to move
            furnitureList = JsonConvert.DeserializeObject<List<FurnitureModel>>(args[0].ToString());

            // Enable the cursor and disable the chat
            Cursor.Visible = true;
            Chat.Activate(false);
            Chat.Show(false);


            // Set the flag for moving furniture
            movingFurniture = true;
        }

        private void ClickEvent(int x, int y, bool up, bool right)
        {
            // Check if the player can move the furniture
            if (!movingFurniture || right) return;

            // Check if the player clicked on any furniture
            if (selectedFurniture == null && !up)
            {
                selectedFurniture = GetClickedFurniture(x, y);
                Chat.Output("Furniture: " + selectedFurniture);
                return;
            }

            // Check if the player stopped holding the furniture
            if (selectedFurniture != null && up)
            {
                Events.CallRemote("updateFurniturePosition");
                selectedFurniture = null;
                return;
            }
        }

        private FurnitureModel GetClickedFurniture(int x, int y)
        {
            Vector3 screenPosition = new Vector3(x, y, 0.0f);
            Vector3 worldPosition = new Vector3(x, y, 0.0f);
            //Vector3 worldPosition = RAGE.Game.Graphics.GetScreenCoordFromWorldCoord();

            foreach (FurnitureModel furniture in furnitureList)
            {
                // Obtain the furniture's position
                if(worldPosition.DistanceTo(furniture.handle.Position) < 15.0)
                {
                    // Select the furniture as movable
                    return furniture;
                }
            }

            return null;
        }
    }
}
