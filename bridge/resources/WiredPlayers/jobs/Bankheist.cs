using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WiredPlayers.jobs
{
    public class Bankheist : Script
    {
        public static Dictionary<int, Timer> hackingTimerList;
        private static Dictionary<int, Timer> drillTimerList;
        public static Dictionary<int, Timer> harvestTimerList;
        public List<Vector3> BankBoxCoords = new List<Vector3>();
        public static List<OreModel> BoxList = new List<OreModel>();
        public Bankheist()
        {
            // Initialize the variables
            foreach (Vector3 terminal in Constants.HACKABLE_TERMINALS)
            { 
                // Create Terminals shops
                
                NAPI.TextLabel.CreateTextLabel("Terminal", terminal, 10.0f, 0.5f, 4, new Color(255, 255, 255), false, 0);
            }
            //OCEAN HIGHWAY FLEECA
            NAPI.ColShape.Create2DColShape(-2958.502f, 484.0262f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-2958.502, 484.0262, 14.67529), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-2957.343f, 485.9573f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-2957.343, 485.9573, 14.67533), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-2954.298f, 482.4786f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-2954.298, 482.4786, 14.6753), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-2952.594f, 484.2762f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-2952.594, 484.2762, 14.67538), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-2954.02f, 486.2813f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-2954.02, 486.2813, 14.67541), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            //PALETO
            NAPI.ColShape.Create2DColShape(-107.0286f, 6473.499f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-107.0286f, 6473.499f, 30.68672), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-107.6794f, 6475.671f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-107.6794f, 6475.671f, 30.68672f), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-102.8615f, 6475.573f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-102.8615f, 6475.573f, 30.68674), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-103.2876f, 6478.302f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-103.2876f, 6478.302f, 30.68673f), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));
            NAPI.ColShape.Create2DColShape(-105.868f, 6478.51f, 2f, 5f);
            NAPI.Marker.CreateMarker(MarkerType.HorizontalCircleSkinny, new Vector3(-105.868f, 6478.51f, 30.68714f), new Vector3(), new Vector3(), 1.5f, new Color(198, 40, 40, 200));

            NAPI.World.DeleteWorldProp(2121050683, new Vector3(-2957.867, 481.9294, 15.6970), (100.0f));
            // Initialize the variables
            hackingTimerList = new Dictionary<int, Timer>();
            drillTimerList = new Dictionary<int, Timer>();
            harvestTimerList = new Dictionary<int, Timer>();
            GeneratBankBoxCoords();
        }
        public void GeneratBankBoxCoords()
        {
            BankBoxCoords.Add(new Vector3(-2958.502, 484.0262, 15.67529));
            BankBoxCoords.Add(new Vector3(-2957.343, 485.9573, 15.67533));
            BankBoxCoords.Add(new Vector3(-2954.298, 482.4786, 15.6753));
            BankBoxCoords.Add(new Vector3(-2952.594, 484.2762, 15.67538));
            BankBoxCoords.Add(new Vector3(-2954.02, 486.2813, 15.67541));
            BankBoxCoords.Add(new Vector3(-107.0286, 6473.499, 31.62672));
            BankBoxCoords.Add(new Vector3(-107.6794, 6475.671, 31.62672));
            BankBoxCoords.Add(new Vector3(-102.8615, 6475.573, 31.62674));
            BankBoxCoords.Add(new Vector3(-103.2876, 6478.302, 31.62673));
            GenerateBoxes();
        }
        public void DestroyBoxes()
        {
            foreach (OreModel Box in BoxList)
            {
                Box.itemHud.Delete();
            }
        }
        [Command("regenboxes")]
        public void Regenore(Client player)
        {
            DestroyBoxes();

            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_ADMIN)
            {
                GenerateBoxes();
            }

        }
        public static void OnPlayerDisconnected(Client player)
        {
            if (hackingTimerList.TryGetValue(player.Value, out Timer hackingTimer))
            {
                // Remove the timer
                hackingTimer.Dispose();
                hackingTimerList.Remove(player.Value);
            }
            if (harvestTimerList.TryGetValue(player.Value, out Timer miningTimer))
            {
                // Remove the timer
                miningTimer.Dispose();
                harvestTimerList.Remove(player.Value);
            }
        }

        public void GenerateBoxes()
        {

            foreach (Vector3 o in BankBoxCoords)
            {
                Vector3 BoxPos = new Vector3(0, 0, 0);
                BoxPos = new Vector3(o.X, o.Y, o.Z);
                OreModel Box = new OreModel();
                Box.itemName = "Bank Box";
                Box.itemPosition = BoxPos;
                Box.itemHud = NAPI.TextLabel.CreateTextLabel("Bank Box", BoxPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                BoxList.Add(Box);
            }

        }
        [Command("hack")]
        public static void HackTerminal(Client player)
        {
            foreach (Vector3 terminal in Constants.HACKABLE_TERMINALS)
            {
                if (player.Position.DistanceTo(terminal) < 5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel HackingChip = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_HACKING_CHIP);
                    if (HackingChip != null)
                    {
                        if (HackingChip.amount == 1)
                        {
                            Task.Factory.StartNew(() =>
                            {
                                NAPI.Task.Run(() =>
                                {
                                    // Remove the baits from the inventory
                                    Globals.itemList.Remove(HackingChip);
                                    Database.RemoveItem(HackingChip.id);
                                });
                            });
                            player.SendChatMessage(Constants.COLOR_INFO + "Your hacking chip is now broken");
                        }
                        else
                        {
                            HackingChip.amount--;

                            Task.Factory.StartNew(() =>
                            {
                                NAPI.Task.Run(() =>
                                {
                                    // Update the amount
                                    Database.UpdateItem(HackingChip);
                                });
                            });
                        }
                        player.SetData(EntityData.DOOR_HACKED, true);
                        player.TriggerEvent("Hacking");
                        HackingTimer(player, true);
                    }
                }
            }
        }
        public static void HackingTimer(Client player, bool isHarvesting)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;
            harvestTimerList.Remove(player.Value);
            if (isHarvesting)
            {
                // Create the mining timer
                miningTimer = new Timer(HackSuccess, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void HackSuccess(object timer)
        {
            Client player = (Client)timer;

            // Stop the hacking animation
            player.StopAnimation();

            harvestTimerList.Remove(player.Value);
            hackingTimerList.Remove(player.Value);

            //NAPI.World.DeleteWorldProp(2121050683, new Vector3(-2957.867, 481.9294, 15.6970), (15.0f));
            player.TriggerEvent("destroyBrowser");
        }
        [Command("drill")]
        public static void DrillBoxes(Client player)
        {
            if (player.GetData(EntityData.PLAYER_DRILLING) != true)
            {

                    foreach (OreModel Box in BoxList)
                    {
                        if (Box.itemHud.Position.DistanceTo(player.Position) < 1)
                        {
                            if (player.GetData(EntityData.DOOR_HACKED) != null)
                            {
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel Drill = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_DRILL);
                                if (Drill != null)
                                {
                                    if (Drill.amount == 1)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Drill);
                                                Database.RemoveItem(Drill.id);
                                            });
                                        });
                                        player.SendChatMessage(Constants.COLOR_INFO + "Your drill broke");
                                    }
                                    else
                                    {
                                        Drill.amount--;

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Update the amount
                                            Database.UpdateItem(Drill);
                                            });
                                        });
                                    }
                                    Box.itemHud.Delete();
                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_DRILLING, true);
                                    DrillTimer(player, true);
                                }

                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_INFO + "You dont have a drill");
                                }
                            }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_INFO + "You havent hacked the door");
                        }
                    }
                    }
                
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You are already drilling");
            }

        }
        public static void DrillTimer(Client player, bool isHacking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));
            drillTimerList.Remove(player.Value);
            // Create the timer
            Timer drillTimer = null;

            // Create the mining timer
            drillTimer = new Timer(DrillSuccess, player, 60000, Timeout.Infinite);

            // Add the timer to the list
            drillTimerList.Add(player.Value, drillTimer);
        }
        public static void DrillSuccess(object timer)
        {
            Client player = (Client)timer;

            // Stop the hacking animation
            player.StopAnimation();

            drillTimerList.Remove(player.Value);
            player.ResetData(EntityData.PLAYER_DRILLING);
            int amount = 100;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel BagofMoney = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_STOLEN_OBJECTS);

            if (BagofMoney == null)
            {
                BagofMoney = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_STOLEN_OBJECTS,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(BagofMoney);
                Globals.itemList.Add(BagofMoney);

                // Send the confirmation message
                player.SendChatMessage(Constants.COLOR_YELLOW + "You stole some valuable items.");
            }
            else
            {

                // Add the amount
                BagofMoney.amount += amount;

                // Update the amount into the database
                Database.UpdateItem(BagofMoney);
                // Send the confirmation message
                player.SendChatMessage(Constants.COLOR_YELLOW + "You stole some valuable items");

            }
        }
    }
}