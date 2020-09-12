using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WiredPlayers.character;
using WiredPlayers.model;
using WiredPlayers.factions;
using WiredPlayers.messages.help;
using WiredPlayers.messages.information;
using WiredPlayers.globals;
using WiredPlayers.database;
using WiredPlayers.messages.error;
using WiredPlayers.jobs;

namespace WiredPlayers.drugs
{

    class Drugs : Script
    {
        public static List<Vector3> Allleafs = new List<Vector3>();
        public static Dictionary<int, Timer> harvestTimerList;
        public static Dictionary<int, Timer> HarvestingWeed;
        public static Dictionary<int, Timer> PlantingTimer;
        public static List<PlantModel> allCocainItems = new List<PlantModel>();
        public static List<PlantModel> Plants = new List<PlantModel>();
        

        public Drugs()
        {
            harvestTimerList = new Dictionary<int, Timer>();
            PlantingTimer = new Dictionary<int, Timer>();
            HarvestingWeed = new Dictionary<int, Timer>();
            GenerateCocainCoords();
        }
        #region Cocain Leafs

        [Command(Commands.COM_REGENLEAFS)]
        public void RegenleafsCommand(Client player)
        {
            foreach (PlantModel LeafItem in allCocainItems)
            {
                LeafItem.itemHud.Delete();
                LeafItem.itemObject.Delete();
            }
            GenerateLeafs();
        }

        public void GenerateCocainCoords()
        {
            Allleafs.Add(new Vector3(2937.354, 5336.313, 102.0025));
            Allleafs.Add(new Vector3(2953.295, 5342.489, 102.6044));
            Allleafs.Add(new Vector3(2971.354, 5330.149, 100.6253));
            Allleafs.Add(new Vector3(2994.591, 5345.255, 98.37457));
            Allleafs.Add(new Vector3(1368.468, -1039.106, 43.80099));
            Allleafs.Add(new Vector3(1455.644, -566.085, 85.41368));
            Allleafs.Add(new Vector3(1470.736, -547.3842, 86.49038));
            Allleafs.Add(new Vector3(1576.542, -595.4244, 148.702));
            Allleafs.Add(new Vector3(2682.375, -861.5181, 26.05725));
            Allleafs.Add(new Vector3(2679.046, -855.9504, 26.87387));
            Allleafs.Add(new Vector3(2690.958, -847.2991, 26.8414));
            Allleafs.Add(new Vector3(2662.715, -859.8538, 27.61178));
            Allleafs.Add(new Vector3(2628.808, 4753.696, 33.94017));
            Allleafs.Add(new Vector3(-359.7701, 4313.289, 57.04502));
            Allleafs.Add(new Vector3(-2178.192, -37.99974, 70.90199));
            Allleafs.Add(new Vector3(-1986.918, 2510.394, 2.961013));
            Allleafs.Add(new Vector3(-1114.256, 4959.53, 218.9207));
            Allleafs.Add(new Vector3(1950.945, 5513.567, 132.7902));
            Allleafs.Add(new Vector3(1944.147, 5509.27, 137.1268));
            Allleafs.Add(new Vector3(1946.977, 5503.742, 139.3913));
            Allleafs.Add(new Vector3(1957.753, 5515.817, 130.7775));
            Allleafs.Add(new Vector3(1963.216, 5510.741, 132.3362));
            Allleafs.Add(new Vector3(1977.471, 5503.031, 134.7683));
            Allleafs.Add(new Vector3(1953.253, 5484.661, 147.4202));
            Allleafs.Add(new Vector3(1942.396, 5488.32, 148.7616));
            Allleafs.Add(new Vector3(1919.778, 5539.056, 147.1443));
            Allleafs.Add(new Vector3(1900.108, 5558.603, 158.7302));
            Allleafs.Add(new Vector3(1895.189, 5547.874, 156.2728));
            Allleafs.Add(new Vector3(1230.282, 4400.277, 35.29486));
            Allleafs.Add(new Vector3(1229.974, 4399.995, 35.14008));
            Allleafs.Add(new Vector3(1228.047, 4401.584, 34.97403));
            Allleafs.Add(new Vector3(1226.652, 4398.951, 35.03025));
            Allleafs.Add(new Vector3(1228.787, 4398.447, 35.0));
            Allleafs.Add(new Vector3(1230.46, 4397.94, 35.47279));
            Allleafs.Add(new Vector3(1230.773, 4396.269, 35.49924));
            Allleafs.Add(new Vector3(1227.36, 4395.799, 35.07538));
            Allleafs.Add(new Vector3(1224.921, 4395.562, 35.35334));
            Allleafs.Add(new Vector3(1227.61, 4393.45, 35.10379));
            Allleafs.Add(new Vector3(1229.294, 4393.513, 35.15907));
            Allleafs.Add(new Vector3(1231.708, 4393.596, 35.71077));
            Allleafs.Add(new Vector3(1231.658, 4390.811, 35.47964));
            Allleafs.Add(new Vector3(1230.171, 4390.682, 35.24492));
            Allleafs.Add(new Vector3(1228.715, 4390.092, 35.14572));
            Allleafs.Add(new Vector3(542.6321, 4502.904, 102.3483));
            Allleafs.Add(new Vector3(544.2847, 4502.634, 102.3836));
            Allleafs.Add(new Vector3(546.3793, 4501.592, 102.3856));
            Allleafs.Add(new Vector3(547.7927, 4499.272, 102.4287));
            Allleafs.Add(new Vector3(546.0796, 4498.474, 101.1811));
            Allleafs.Add(new Vector3(544.2186, 4497.696, 100.9638));
            Allleafs.Add(new Vector3(540.5925, 4495.994, 102.0371));
            Allleafs.Add(new Vector3(542.8718, 4493.071, 100.1525));
            Allleafs.Add(new Vector3(544.5725, 4491.272, 99.66529));
            Allleafs.Add(new Vector3(546.8743, 4487.974, 100.1572));
            Allleafs.Add(new Vector3(544.9679, 4482.169, 96.99758));
            Allleafs.Add(new Vector3(543.7928, 4484.181, 97.1552));
            Allleafs.Add(new Vector3(539.8862, 4484.22, 97.33788));
            Allleafs.Add(new Vector3(539.8862, 4484.22, 97.33788));
            Allleafs.Add(new Vector3(551.3146, 5565.464, 756.4984));
            Allleafs.Add(new Vector3(551.2023, 5567.855, 756.5333));
            Allleafs.Add(new Vector3(554.7584, 5568.165, 754.4507));
            Allleafs.Add(new Vector3(555.8044, 5566.681, 753.8199));
            Allleafs.Add(new Vector3(557.3096, 5565.639, 753.1125));
            Allleafs.Add(new Vector3(553.9716, 5568.072, 755.0272));
            Allleafs.Add(new Vector3(556.6293, 5565.961, 753.4282));
            Allleafs.Add(new Vector3(559.5775, 5573.389, 751.8383));
            Allleafs.Add(new Vector3(567.4787, 5575.203, 748.884));
            Allleafs.Add(new Vector3(560.0339, 5546.417, 751.0407));
            Allleafs.Add(new Vector3(557.5145, 5550.194, 752.0144));
            Allleafs.Add(new Vector3(1165.902, 4892.559, 216.1404));
            Allleafs.Add(new Vector3(1170.476, 4894.389, 215.8974));
            Allleafs.Add(new Vector3(1172.414, 4898.434, 216.2648));
            Allleafs.Add(new Vector3(1169.326, 4899.078, 217.127));
            Allleafs.Add(new Vector3(1162.512, 4903.031, 218.3379));
            Allleafs.Add(new Vector3(1166.963, 4902.492, 217.9289));
            Allleafs.Add(new Vector3(1171.577, 4903.932, 217.6106));
            Allleafs.Add(new Vector3(1171.528, 4907.616, 218.9084));
            Allleafs.Add(new Vector3(1166.608, 4903.639, 218.1832));
            Allleafs.Add(new Vector3(1166.036, 4896.213, 216.8682));
            Allleafs.Add(new Vector3(1160.63, 4884.565, 213.7541));
            Allleafs.Add(new Vector3(1156.47, 4900.001, 218.094));
            Allleafs.Add(new Vector3(1160.094, 4902.825, 218.3992));
            Allleafs.Add(new Vector3(1174.83, 4896.658, 215.3374));
            GenerateLeafs();
        }
        public void GenerateLeafs()
        {
            foreach (Vector3 o in Allleafs)
            {
                Vector3 PlantFix = new Vector3(0, 0, 0);
                PlantFix = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Leaf = NAPI.Object.CreateObject(2575791413, PlantFix, new Vector3(0, 0, -0.8), 255, 0);
                Vector3 LeafPos = new Vector3(0, 0, 0);
                LeafPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                PlantModel LeafItem = new PlantModel();
                LeafItem.Position = LeafPos;
                LeafItem.itemName = "Cocain Leaf";
                LeafItem.itemDesc = "Used to create Cocain";
                LeafItem.itemObject = Leaf;
                LeafItem.itemHud = NAPI.TextLabel.CreateTextLabel("Press E to Collect", LeafPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                allCocainItems.Add(LeafItem);
            }
        }
        public static void OnPlayerDisconnected(Client player)
        {
            if (harvestTimerList.TryGetValue(player.Value, out Timer miningTimer))
            {
                // Remove the timer
                miningTimer.Dispose();
                harvestTimerList.Remove(player.Value);
                if (PlantingTimer.TryGetValue(player.Value, out Timer plantTimer))
                {
                    plantTimer.Dispose();
                    PlantingTimer.Remove(player.Value);
                }
            }
        }
        public static void AnimateHarvest(Client player, bool isHarvesting)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isHarvesting)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectLeafAsync, player, 5000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }

        [Command("harvest")]
        public static void HarvestCommand(Client player)

        {
            foreach (PlantModel LeafItem in allCocainItems)
            {
                if (LeafItem.Position.DistanceTo(player.Position) < 5)
                {
                    if (harvestTimerList.ContainsKey(player.Value))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                        return;
                    }
                   // if (LeafItem.itemFound == false)
                     //   return;

                    LeafItem.itemFound = true;
                    LeafItem.itemHud.Delete();
                    LeafItem.itemObject.Delete();

                    if (player == player)
                    {
                        player.SetData(EntityData.PlayerMining, true);
                        AnimateHarvest(player, true);
                        player.TriggerEvent("playermining"); // Not required atm
                        return;

                    }

                }
            }
        }

        public static void CollectLeafAsync(object Leaf)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel LeafItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEAF);

            if (LeafItem == null)
            {

                foreach (SkillsModel skills in PlayerSkillList)
                {
                    if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                    {
                        if (skills.herbalismlevel < 10)
                        {
                            LeafItem = new ItemModel()
                            {
                                amount = amount,
                                dimension = 0,
                                position = new Vector3(),
                                hash = Constants.ITEM_HASH_LEAF,
                                ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                                ownerIdentifier = playerId,
                                objectHandle = null,
                                quality = "Poor"
                            };
                            Database.AddNewItem(LeafItem);
                            Globals.itemList.Add(LeafItem);

                            // Update the amount into the database
                            // Update the item's amount
                            player.SetData(EntityData.PlayerMining, false);
                            // Send the confirmation message
                            player.SendNotification("You gained 1 XP in Herbalism");
                            player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leaf of Poor Quality");
                        }
                        else if (skills.herbalismlevel < 19)
                        {
                            LeafItem = new ItemModel()
                            {
                                amount = amount,
                                dimension = 0,
                                position = new Vector3(),
                                hash = Constants.ITEM_HASH_LEAF,
                                ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                                ownerIdentifier = playerId,
                                objectHandle = null,
                                quality = "Decent"
                            };
                            Database.AddNewItem(LeafItem);
                            Globals.itemList.Add(LeafItem);


                            // Update the amount into the database
                            // Update the item's amount
                            player.SetData(EntityData.PlayerMining, false);
                            //  Globals.itemList.Add(CopperItem);
                            // Send the confirmation message
                            player.SendNotification("You gained 1 XP in Herbalism");
                            player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leafs of Decent Quality");
                        }
                        else if (skills.herbalismlevel > 19)
                        {
                            LeafItem = new ItemModel()
                            {
                                amount = amount,
                                dimension = 0,
                                position = new Vector3(),
                                hash = Constants.ITEM_HASH_LEAF,
                                ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                                ownerIdentifier = playerId,
                                objectHandle = null,
                                quality = "Good"
                            };
                            Database.AddNewItem(LeafItem);
                            Globals.itemList.Add(LeafItem);
                            //   Inventory.ItemCollection.Add(LeafItem.id, LeafItem);
                            // Update the item's amount
                            player.SetData(EntityData.PlayerMining, false);
                            // Send the confirmation message
                            player.SendNotification("You gained 1 XP in Herbalism");
                            player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leafs of Good Quality");
                        }
                        else if (skills.herbalismlevel > 29)
                        {
                            LeafItem = new ItemModel()
                            {
                                amount = amount,
                                dimension = 0,
                                position = new Vector3(),
                                hash = Constants.ITEM_HASH_LEAF,
                                ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                                ownerIdentifier = playerId,
                                objectHandle = null,
                                quality = "Great"
                            };
                            Database.AddNewItem(LeafItem);
                            Globals.itemList.Add(LeafItem);
                            //   Inventory.ItemCollection.Add(LeafItem.id, LeafItem);
                            player.SetData(EntityData.PlayerMining, false);
                            // Send the confirmation message
                            player.SendNotification("You gained 1 XP in Herbalism");
                            player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leafs of Great Quality");
                        }
                        else if (skills.herbalismlevel > 39)
                        {
                            LeafItem = new ItemModel()
                            {
                                amount = amount,
                                dimension = 0,
                                position = new Vector3(),
                                hash = Constants.ITEM_HASH_LEAF,
                                ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                                ownerIdentifier = playerId,
                                objectHandle = null,
                                quality = "Perfect"
                            };
                            Database.AddNewItem(LeafItem);
                            Globals.itemList.Add(LeafItem);
                            // Inventory.ItemCollection.Add(LeafItem.id, LeafItem);

                            // Update the amount into the database
                            // Update the item's amount
                            player.SetData(EntityData.PlayerMining, false);
                            // Send the confirmation message
                            player.SendNotification("You gained 1 XP in Herbalism");
                            player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leafs of Great Quality");
                        }
                    }
                }

            }
            //else
            //{
            //    // Create the object
            //    LeafItem = new ItemModel()
            //    {
            //        amount = amount,
            //        dimension = 0,
            //        position = new Vector3(),
            //        hash = Constants.ITEM_HASH_LEAF,
            //        ownerEntity = Constants.ITEM_ENTITY_PLAYER,
            //        ownerIdentifier = playerId,
            //        objectHandle = null,
            //        quality = "Poor"
            //    };
            //    Database.AddNewItem(LeafItem);
            //    Globals.itemList.Add(LeafItem);
            //    // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
            //    player.SendNotification("You gained 1 XP in Herbalism");
            //    player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leaf");
            //}
             else
            {

                // Add the amount
                LeafItem.amount += amount;

                // Update the amount into the database
                Database.UpdateItem(LeafItem);
                // Update the item's amount
               // Globals.itemList.Add(LeafItem);
                player.SetData(EntityData.PlayerMining, false);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Hearbalism");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You harvested 1 Leaf");

            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.herbalismexp = skills.herbalismexp + 1;
                    if (skills.herbalismexp == 25)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 80)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 160)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 290)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 420)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 550)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 680)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 900)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 1030)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 1160)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        #endregion
        #region Drug Production
        [Command("ecstacy")]
        public static void ExtacyCommand(Client player)
        {
            if(player.GetData(EntityData.PLAYER_COCAINCOOK) !=null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_cooking);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);

                if (Sodium != null && Sodium.amount > 0)
                {
                    //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel Mauretic = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_MAURETICACID);
                    if (Mauretic != null && Mauretic.amount > 0)
                    {
                        ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
                        if (Acetone != null && Acetone.amount > 0)
                        {
                            foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(LabLocations) > 6f) continue;

                                if (Sodium.amount == 1)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Sodium);
                                            Database.RemoveItem(Sodium.id);
                                        });
                                    });
                                }
                                if (Mauretic.amount == 1)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Mauretic);
                                            Database.RemoveItem(Mauretic.id);
                                        });
                                    });
                                }
                                if (Acetone.amount == 1)
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Acetone);
                                            Database.RemoveItem(Acetone.id);
                                        });
                                    });
                                }
                                else
                                {
                                    Sodium.amount--;
                                    Mauretic.amount--;
                                    Acetone.amount--;

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the amount
                                            Database.UpdateItem(Acetone);
                                            Database.UpdateItem(Sodium);
                                            Database.UpdateItem(Mauretic); // Muriatic
                                        });
                                    });
                                }

                                // Start fishing minigame
                                player.SetData(EntityData.PLAYER_COCAINCOOK, true);
                            }
                            // Player's not in the fishing zone
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone);
                            AnimateEx(player, true);
                            return;
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }

                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                }
            
        }
        [Command("cook")]
        public void CocainCommand(Client player)
        {
            
            if (player.GetData(EntityData.PLAYER_COCAINCOOK) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_cooking);
                return;
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CocainLeaf = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEAF);
                if (CocainLeaf != null && CocainLeaf.amount > 0)
                {
                    //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);
                    if (Sodium != null && Sodium.amount > 0)
                    {
                        ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
                        if (Acetone != null && Acetone.amount > 0)
                        {
                            foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(LabLocations) > 6f)
                                {

                                    if (Sodium.amount == 1)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Sodium);
                                                Database.RemoveItem(Sodium.id);
                                            });
                                        });
                                    }
                                    if (CocainLeaf.amount == 1)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(CocainLeaf);
                                                Database.RemoveItem(CocainLeaf.id);
                                            });
                                        });
                                    }
                                    if (Acetone.amount == 1)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Remove the baits from the inventory
                                            Globals.itemList.Remove(Acetone);
                                                Database.RemoveItem(Acetone.id);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        Sodium.amount--;
                                        CocainLeaf.amount--;
                                        Acetone.amount--;

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                            // Update the amount
                                            Database.UpdateItem(Acetone);
                                                Database.UpdateItem(Sodium);
                                                Database.UpdateItem(CocainLeaf);
                                            });
                                        });
                                    }
                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_COCAINCOOK, true);
                                }
                                // Player's not in the fishing zone
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone);
                            }
                            AnimateCooking(player, true);
                            return;
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                }
            //if (CocainLeaf.quality == "Decent")
            //{
            //    if (CocainLeaf != null && CocainLeaf.amount > 0)
            //    {
            //        //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //        ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);
            //        if (Sodium != null && Sodium.amount > 0)
            //        {
            //            ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
            //            if (Acetone != null && Acetone.amount > 0)
            //            {
            //                foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
            //                {
            //                    // Check if the player is close to the area
            //                    if (player.Position.DistanceTo(LabLocations) > 6f) continue;

            //                    if (Sodium.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Sodium);
            //                                Database.RemoveItem(Sodium.id);
            //                            });
            //                        });
            //                    }
            //                    if (CocainLeaf.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(CocainLeaf);
            //                                Database.RemoveItem(CocainLeaf.id);
            //                            });
            //                        });
            //                    }
            //                    if (Acetone.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Acetone);
            //                                Database.RemoveItem(Acetone.id);
            //                            });
            //                        });
            //                    }
            //                    else
            //                    {
            //                        Sodium.amount--;
            //                        CocainLeaf.amount--;
            //                        Acetone.amount--;

            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Update the amount
            //                                Database.UpdateItem(Acetone);
            //                                Database.UpdateItem(Sodium);
            //                                Database.UpdateItem(CocainLeaf);
            //                            });
            //                        });
            //                    }

            //                    // Start fishing minigame
            //                    player.SetData(EntityData.PLAYER_COCAINCOOK, true);
            //                }
            //                AnimateCookingDecent(player, true);
            //                return;
            //            }

            //            // Player's not in the fishing zone
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone);
            //        }
            //        else
            //        {
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //        }
            //    }
            //    else
            //    {
            //        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //    }
            //}
            //else
            //{
            //    return;
            //}
            //if (CocainLeaf.quality == "Good")
            //{
            //    if (CocainLeaf != null && CocainLeaf.amount > 0)
            //    {
            //        //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //        ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);
            //        if (Sodium != null && Sodium.amount > 0)
            //        {
            //            ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
            //            if (Acetone != null && Acetone.amount > 0)
            //            {
            //                foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
            //                {
            //                    // Check if the player is close to the area
            //                    if (player.Position.DistanceTo(LabLocations) > 6f) continue;

            //                    if (Sodium.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Sodium);
            //                                Database.RemoveItem(Sodium.id);
            //                            });
            //                        });
            //                    }
            //                    if (CocainLeaf.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(CocainLeaf);
            //                                Database.RemoveItem(CocainLeaf.id);
            //                            });
            //                        });
            //                    }
            //                    if (Acetone.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Acetone);
            //                                Database.RemoveItem(Acetone.id);
            //                            });
            //                        });
            //                    }
            //                    else
            //                    {
            //                        Sodium.amount--;
            //                        CocainLeaf.amount--;
            //                        Acetone.amount--;

            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Update the amount
            //                                Database.UpdateItem(Acetone);
            //                                Database.UpdateItem(Sodium);
            //                                Database.UpdateItem(CocainLeaf);
            //                            });
            //                        });
            //                    }

            //                    // Start fishing minigame
            //                    player.SetData(EntityData.PLAYER_COCAINCOOK, true);
            //                }
            //                AnimateCookingGood(player, true);
            //                return;
            //            }

            //            // Player's not in the fishing zone
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone);
            //        }
            //        else
            //        {
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //        }
            //    }
            //    else
            //    {
            //        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //    }
            //}
            //else
            //{
            //    return;
            //}
            //if (CocainLeaf.quality == "Great")
            //{
            //    if (CocainLeaf != null && CocainLeaf.amount > 0)
            //    {
            //        //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //        ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);
            //        if (Sodium != null && Sodium.amount > 0)
            //        {
            //            ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
            //            if (Acetone != null && Acetone.amount > 0)
            //            {
            //                foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
            //                {
            //                    // Check if the player is close to the area
            //                    if (player.Position.DistanceTo(LabLocations) > 6f) continue;

            //                    if (Sodium.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Sodium);
            //                                Database.RemoveItem(Sodium.id);
            //                            });
            //                        });
            //                    }
            //                    if (CocainLeaf.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(CocainLeaf);
            //                                Database.RemoveItem(CocainLeaf.id);
            //                            });
            //                        });
            //                    }
            //                    if (Acetone.amount == 1)
            //                    {
            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Remove the baits from the inventory
            //                                Globals.itemList.Remove(Acetone);
            //                                Database.RemoveItem(Acetone.id);
            //                            });
            //                        });
            //                    }
            //                    else
            //                    {
            //                        Sodium.amount--;
            //                        CocainLeaf.amount--;
            //                        Acetone.amount--;

            //                        Task.Factory.StartNew(() =>
            //                        {
            //                            NAPI.Task.Run(() =>
            //                            {
            //                                // Update the amount
            //                                Database.UpdateItem(Acetone);
            //                                Database.UpdateItem(Sodium);
            //                                Database.UpdateItem(CocainLeaf);
            //                            });
            //                        });
            //                    }

            //                    // Start fishing minigame
            //                    player.SetData(EntityData.PLAYER_COCAINCOOK, true);
            //                }
            //                AnimateCookingGreat(player, true);
            //                return;
            //            }

            //            // Player's not in the fishing zone
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone);
            //        }
            //        else
            //        {
            //            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //        }
            //    }
            //    else
            //    {
            //        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            //    }
            //}
            //else
            //{
            //    return;
            //}
        }
        [Command("meth")]
        public void MethCommand(Client player)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel mauretic_acid = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_MAURETICACID);
            if (mauretic_acid != null && mauretic_acid.amount > 0)
            {
                //  int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                ItemModel Sodium = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);
                if (Sodium != null && Sodium.amount > 0)
                {
                    ItemModel Acetone = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_ACETONE);
                    if (Acetone != null && Acetone.amount > 0)
                    {
                        foreach (Vector3 LabLocations in Constants.LAB_POSITION_LIST)
                        {
                            // Check if the player is close to the area
                            if (player.Position.DistanceTo(LabLocations) > 6f) continue;

                            if (Sodium.amount == 1)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Sodium);
                                        Database.RemoveItem(Sodium.id);
                                    });
                                });
                            }
                            if (Acetone.amount == 1)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Acetone);
                                        Database.RemoveItem(Acetone.id);
                                    });
                                });
                            }
                            if (mauretic_acid.amount == 1)
                            {
                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(mauretic_acid);
                                        Database.RemoveItem(mauretic_acid.id);
                                    });
                                });
                            }
                            else
                            {
                                Sodium.amount--;
                                mauretic_acid.amount--;
                                Acetone.amount--;

                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Update the amount
                                        Database.UpdateItem(Acetone);
                                        Database.UpdateItem(Sodium);
                                        Database.UpdateItem(mauretic_acid);
                                    });
                                });
                            }

                            // Start fishing minigame
                            player.SetData(EntityData.PLAYER_COCAINCOOK, true);
                            AnimateMeth(player, true);
                        }
                        // Player's not in the fishing zone
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_cooking_zone); 
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            }
        }
        public static void AnimateEx(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectexAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateExDecent(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectexdecentAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateExGood(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectexgoodAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateExGreat(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectexgreatAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateExPerfect(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectexperfectAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateCooking(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectcokeAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateCookingDecent(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectcokeDecent, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateCookingGood(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectcokeGood, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateCookingGreat(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectcokeGreat, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMeth(Client player, bool isCooking)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isCooking)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectMethAsync, player, 60000, Timeout.Infinite);
            }
            // Add the timer to the list
            harvestTimerList.Add(player.Value, miningTimer);
        }
        private static void CollectexAsync(object ex)
        {
            // Get the client mining
            Client player = (Client)ex;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 10;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel ExItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_EXTACY);

            if (ExItem != null)
            {
                // Add the amount
                ExItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(ExItem);
                Globals.itemList.Add(ExItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy");
            }
            else
            {
                // Create the object
                ExItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_EXTACY,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(ExItem);
                Globals.itemList.Add(ExItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectexdecentAsync(object ex)
        {
            // Get the client mining
            Client player = (Client)ex;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 10;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel ExItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_EXTACY);

            if (ExItem != null)
            {
                // Add the amount
                ExItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(ExItem);
                Globals.itemList.Add(ExItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Decent quality");
            }
            else
            {
                // Create the object
                ExItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_EXTACY,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Decent"
                };
                Database.AddNewItem(ExItem);
                Globals.itemList.Add(ExItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Decent quality");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectexgoodAsync(object ex)
        {
            // Get the client mining
            Client player = (Client)ex;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 10;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel ExItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_EXTACY);

            if (ExItem != null)
            {
                // Add the amount
                ExItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(ExItem);
                Globals.itemList.Add(ExItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Good quality");
            }
            else
            {
                // Create the object
                ExItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_EXTACY,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Good"
                };
                Database.AddNewItem(ExItem);
                Globals.itemList.Add(ExItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Good quality");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectexgreatAsync(object ex)
        {
            // Get the client mining
            Client player = (Client)ex;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 10;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel ExItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_EXTACY);

            if (ExItem != null)
            {
                // Add the amount
                ExItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(ExItem);
                Globals.itemList.Add(ExItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Great quality");
            }
            else
            {
                // Create the object
                ExItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_EXTACY,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Great"
                };
                Database.AddNewItem(ExItem);
                Globals.itemList.Add(ExItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Great quality");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectexperfectAsync(object ex)
        {
            // Get the client mining
            Client player = (Client)ex;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 10;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel ExItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_EXTACY);

            if (ExItem != null)
            {
                // Add the amount
                ExItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(ExItem);
                Globals.itemList.Add(ExItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Perfect quality");
            }
            else
            {
                // Create the object
                ExItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_EXTACY,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Perfect"
                };
                Database.AddNewItem(ExItem);
                Globals.itemList.Add(ExItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 10 pills of ecstasy of Perfect quality");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectcokeAsync(object Leaf)
        {
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CocainItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COCAIN);

            if (CocainItem != null)
            {
                // Add the amount
                CocainItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else
            {
                // Create the object
                CocainItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COCAIN,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectcokeDecent(object Leaf)
        {
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CocainItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COCAIN);

            if (CocainItem == null)
            {
                // Create the object
                CocainItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COCAIN,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Decent"
                };
                Database.AddNewItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else if (CocainItem.quality == "Decent")
            {
                // Add the amount
                CocainItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(CocainItem);
                // Globals.itemList.Add(CocainItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else
            {
                return;
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectcokeGood(object Leaf)
        {
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CocainItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COCAIN);

            if (CocainItem == null)
            {
                // Create the object
                CocainItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COCAIN,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Good"
                };
                Database.AddNewItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else if (CocainItem.quality == "Good")
            {
                // Add the amount
                CocainItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(CocainItem);
               // Globals.itemList.Add(CocainItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else
            {
                return;
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectcokeGreat(object Leaf)
        {
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CocainItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COCAIN);

            if (CocainItem != null)
            {
                // Add the amount
                CocainItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }
            else
            {
                // Create the object
                CocainItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COCAIN,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Great"
                };
                Database.AddNewItem(CocainItem);
                Globals.itemList.Add(CocainItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Cocain");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                        player.SendNotification("You leveled up in Drug Production!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectMethAsync(object Leaf)
        {
            // Get the client mining
            Client player = (Client)Leaf;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel MethItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_METH);

            if (MethItem == null)
            {
                // Create the object
                MethItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_METH,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Meth");
                Database.AddNewItem(MethItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
            }
            else
            {
                // Add the amount
                MethItem.amount += amount;

                // Update the amount into the database
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(MethItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Drug production");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You made 2 bags of Meth");
            }


            // Remove the timer from the list
            harvestTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.drugprodexp = skills.drugprodexp + 1;
                    if (skills.drugprodexp == 10)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 20)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 30)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 40)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 50)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 60)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 70)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 80)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 90)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    if (skills.drugprodexp == 100)
                    {
                        skills.drugprodlevel = skills.drugprodlevel + 1;
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        #endregion
        #region Sell
        [Command("sellcoke")]
        public void SellCokeCommand(Client player)
        {
                foreach (Vector3 drugDealer in Constants.DRUG_DEALERS)
                {
                    if (player.Position.DistanceTo(drugDealer) < 1.5f)
                    {
                        int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel CocainItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COCAIN);
                        if (CocainItem != null)
                        {
                            // Calculate the earnings
                            int wonAmount = CocainItem.amount * Constants.PRICE_COCAIN;
                            string message = string.Format(InfoRes.player_cocain_sold, wonAmount);
                            int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;

                            Task.Factory.StartNew(() =>
                            {
                                NAPI.Task.Run(() =>
                                {
                                    // Delete stolen items
                                    Database.RemoveItem(CocainItem.id);
                                    Globals.itemList.Remove(CocainItem);
                                });
                            });

                            player.SetSharedData(EntityData.PLAYER_MONEY, money);
                            player.SendChatMessage(Constants.COLOR_INFO + message);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_cocain);
                        }
                        return;
                    }
                }
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_drugdealer);
            }
        [Command("sellmeth")]
        public void SellMethCommand(Client player)
        {
            foreach (Vector3 drugDealer in Constants.DRUG_DEALERS)
            {
                if (player.Position.DistanceTo(drugDealer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel Meth = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_METH);
                    if (Meth != null)
                    {
                        // Calculate the earnings
                        int wonAmount = Meth.amount * Constants.PRICE_METH;
                        string message = string.Format(InfoRes.player_cocain_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Delete stolen items
                                Database.RemoveItem(Meth.id);
                                Globals.itemList.Remove(Meth);
                            });
                        });

                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_cocain);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_drugdealer);
        }
        [Command("sellweed")]
        public void SellWeedCommand(Client player)
        {
            foreach (Vector3 drugDealer in Constants.DRUG_DEALERS)
            {
                if (player.Position.DistanceTo(drugDealer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel Weed = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_WEED);
                    if (Weed != null)
                    {
                        // Calculate the earnings
                        int wonAmount = Weed.amount * Constants.PRICE_WEED;
                        string message = string.Format(InfoRes.player_cocain_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Delete stolen items
                                Database.RemoveItem(Weed.id);
                                Globals.itemList.Remove(Weed);
                            });
                        });

                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_cocain);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_drugdealer);
        }

        #endregion


        #region Weed Related
        private const int PlantGrowthTime = 6000;
        private const int MaxWeedPerPlant = 5;
        //  player.SetData(EntityData.TIME_HOSPITAL_RESPAWN, Globals.GetTotalSeconds() + 600);
        public static void LoadallWeed()
        {
            // Load the phone list
            Plants = Database.LoadAllPlants();
        }
        [Command(Commands.COM_WEED, GreedyArg = true)]
        public static void PlantCommand(Client player)
        {

            /*   if (player.Dimension != 0)
               {
                   player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.cant_plant_indoor);
                   return;
               }*/
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel WeedSeed = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_WEED_SEEDS);
            if (WeedSeed != null)
            {
                if (WeedSeed.amount == 1)
                {
                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Remove the baits from the inventory
                            Globals.itemList.Remove(WeedSeed);
                            Database.RemoveItem(WeedSeed.id);
                        });
                    });
                }
                else
                {
                    WeedSeed.amount--;

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the amount
                            Database.UpdateItem(WeedSeed);
                        });
                    });
                }
                if (PlantingTimer.ContainsKey(player.Value))
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_planting);
                    return;
                }
                // Check the closest plant
                PlantModel plant = GetClosestPlant(player);

                if (plant != null)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_has_plants_close);
                    return;
                }
                PlantingTimer.Remove(player.Value);
                // Plant the seeds
                Drugs.AnimatePlayerWeedManagement(player, true);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You dont have any weed seeds");
            }
        }
        public enum ColShapeTypes
        {
            BusinessEntrance = 0, BusinessPurchase = 1, HouseEntrance = 2,
            InteriorEntrance = 3, VehicleDealer = 5, Atm = 6, Plant = 7, Ore = 8
        }




        public static void UpdateGrowth()
        {
            foreach (PlantModel plant in Plants)
            {
                // Check if the plant has fully grown
                if (plant.GrowTime == PlantGrowthTime) continue;

                // Update the plant
                UpdatePlant(plant);
            }
        }

        public static void UpdatePlant(PlantModel plant)
        {
            // Get the growth percentage
            int growthTime = plant.Object == null ? plant.GrowTime : plant.GrowTime + 1;
            float growth = (float)Math.Round((decimal)(growthTime * 100 / PlantGrowthTime), 2);
            string growthText = string.Format(InfoRes.growth, Math.Floor(growth));

            // Create the corresponding object
            uint model = GetPlantModel(growth);

            if (plant.Object == null)
            {
                NAPI.Task.Run(() =>
                {
                    // Create the plant
                    plant.Object = NAPI.Object.CreateObject(model, plant.Position, new Vector3(), 255, plant.Dimension);

                    // Create the growth label
                    plant.Progress = NAPI.TextLabel.CreateTextLabel(growthText, new Vector3(0.0f, 0.0f, 4.0f).Add(plant.Position), 4.0f, 0.5f, 4, new Color(225, 200, 165), true, plant.Dimension);
                });
            }
            else
            {
                if (plant.Object.Model != model)
                {
                    NAPI.Task.Run(() =>
                    {
                        // Destroy the current object
                        plant.Object.Delete();

                        // Create the plant
                        plant.Object = NAPI.Object.CreateObject(model, plant.Position, new Vector3(), 255, plant.Dimension);
                    });
                }

                // Update the growth label
                plant.Progress.Text = growthText;
                plant.GrowTime++;

                // Update the plant's growth
              //  Database.ModifyPlant(plant);
            }

            if (plant.GrowTime == PlantGrowthTime)
            {
                NAPI.Task.Run(() =>
                {
                    // Create the colshape for the plant
                    plant.PlantColshape = NAPI.ColShape.CreateCylinderColShape(plant.Position, 3.5f, 1.0f, plant.Dimension);
                    plant.PlantColshape.SetData(EntityData.ColShapeId, plant.Id);

                    // Add the message to pop the instructional button up
                    // plant.PlantColshape.SetData(EntityData.ColShapeType, ColShapeTypes.Plant);
                    //  plant.PlantColshape.SetData(EntityData.InstructionalButton, HelpRes.action_plant);
                });
            }
        }

         public static PlantModel GetClosestPlant(Client player)
         {

             // Get the closest plant to the player
            return Plants.FirstOrDefault(plant => plant.Dimension == player.Dimension && player.Position.DistanceTo(plant.Position) < 2.0f);

         }
        [Command("harvestweed")]
        public static void HarvestweedCommand(Client player)
        {
            foreach (PlantModel plant in Plants)
            {
                if (plant.Progress.Position.DistanceTo(player.Position) < 5)
                {
                    if (plant.GrowTime == PlantGrowthTime)
                    {
                        plant.Progress.Delete();
                        plant.Object.Delete();
                        plant.PlantColshape.Delete();
                        plant.Progress.Delete();
                        Plants.Remove(plant);
                     

                        player.SetData(EntityData.PlayerMining, true);
                        AnimatePlayerWeedGathering(player, true);
                        player.TriggerEvent("startMining"); // Not required atm
                        Database.DeleteSingleRow(plant.Id);
                        return;


                    }
                    else
                    {

                    }

                }
            }

        }
        public static void AnimatePlayerWeedGathering(Client player, bool isHarvesting)
        {
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer plantHarvestTimer = null;
            if(isHarvesting)
            {
                // Create the collect timer
                plantHarvestTimer = new Timer(CollectWeedAsync, player, 5000, Timeout.Infinite);
            }
            HarvestingWeed.Add(player.Value, plantHarvestTimer);
        }
        public static void AnimatePlayerWeedManagement(Client player, bool isPlanting)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer plantManagementTimer = null;

            if (isPlanting)
            {
                // Create the plant timer
                plantManagementTimer = new Timer(PlantWeedSeedsAsync, player, 5000, Timeout.Infinite);
            }

            // Add the timer to the list
            PlantingTimer.Add(player.Value, plantManagementTimer);
        }

        public static async void PlantWeedSeedsAsync(object planter)
        {
            // Get the client planting
            Client player = (Client)planter;

            // Stop the plant animation
            player.StopAnimation();

            // Create the new plant object
            PlantModel plant = new PlantModel();
            {
                plant.Position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 1.0f);
                plant.Dimension = player.Dimension;
                plant.GrowTime = 0;
                Plants.Add(plant);
            }
            
            // Add the plant to the database
            // plant.Id = await DatabaseOperations.AddPlant(plant).ConfigureAwait(false);
            Database.AddPlant(plant);

            // Remove the timer from the list
            PlantingTimer.Remove(player.Value);

            // Create the ingame object
            int start = Globals.GetTotalSeconds();
            int time = (int)PlantGrowthTime;
            player.SetData(EntityData.PLAYER_DELIVER_START, start);
            player.SetData(EntityData.PLAYER_DELIVER_TIME, time);
            Task.Run(() => UpdatePlant(plant)).ConfigureAwait(false);
        }
        public static async void CollectWeedAsync(object planter)
        {
            // Get the client planting
            Client player = (Client)planter;


            // Stop the plant animation
            player.StopAnimation();

            // Give the weed to the player
            Random random = new Random();
            int amount = random.Next(1, MaxWeedPerPlant);

            // Check if the player has any weed plant in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel weedItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_WEED);

            if (weedItem == null)
            {
                //Create the object
                weedItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_WEED,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Herbalism");
                player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.plant_collected, amount));
                Database.AddNewItem(weedItem);
                Globals.itemList.Add(weedItem);
            }
            else
            {
                // Add the amount
                weedItem.amount += amount;

                // Update the amount into the database
                player.SendNotification("You gained 1 XP in Herbalism");
                player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.plant_collected, amount));
                Database.UpdateItem(weedItem);
            }
            HarvestingWeed.Remove(player.Value);
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.herbalismexp = skills.herbalismexp + 1;
                    if (skills.herbalismexp == 25)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 80)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 160)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 290)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 420)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 550)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 680)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 900)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 1030)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    if (skills.herbalismexp == 1160)
                    {
                        skills.herbalismlevel = skills.herbalismlevel + 1;
                        player.SendNotification("You leveled up in Herbalism!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
    

            
            private static uint GetPlantModel(float growth)
            {
                string modelName;

                if (growth < 25.0f) modelName = "bkr_prop_weed_plantpot_stack_01b";
                else if (growth < 50.0f) modelName = "bkr_prop_weed_01_small_01b";
                else if (growth < 75.0f) modelName = "bkr_prop_weed_med_01b";
                else modelName = "bkr_prop_weed_lrg_01b";
            //bkr_prop_weed_plantpot_stack_01b
            return NAPI.Util.GetHashKey(modelName);
            }
    }
    #endregion
}



