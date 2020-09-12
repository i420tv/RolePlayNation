using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.character;
using WiredPlayers.model;
using Progressbar;
using WiredPlayers.database;
using WiredPlayers.messages.information;
using WiredPlayers.messages.error;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
using GTANetworkMethods;

namespace WiredPlayers.jobs
{
    public class Farming : Script
    {
        public static List<PlayerModel> playerList;
        public static Dictionary<int, ItemModel> ItemCollection;
        public static Dictionary<int, Timer> farmingTimerList;
        public static Dictionary<int, Timer> SmeltingTimerList;
        public List<Vector3> WheatCoords = new List<Vector3>();
        public List<Vector3> IronCoords = new List<Vector3>();
        public static List<FarmingModel> FarmingList = new List<FarmingModel>();
        public static List<OreModel> IronList = new List<OreModel>();
        public OreModel WheatItem;
        public OreModel IronItem;
        public static List<OreModel> allminingItems = new List<OreModel>();
        public Farming()
        {
            // Initialize the variables
            farmingTimerList = new Dictionary<int, Timer>();

            GenerateWheatCoords();
          //  GenerateIronCoords();
        }
        public void GenerateWheatCoords()
        {
            WheatCoords.Add(new Vector3(2046.344, 4966.132, 41.07947));
            WheatCoords.Add(new Vector3(2051.014, 4961.396, 41.06622));
            WheatCoords.Add(new Vector3(2055.824, 4956.469, 41.03296));
            WheatCoords.Add(new Vector3(2061.681, 4950.714, 41.06129));
            WheatCoords.Add(new Vector3(2067.595, 4945.038, 41.05199));
            WheatCoords.Add(new Vector3(2065.048, 4942.143, 41.10296));
            WheatCoords.Add(new Vector3(2062.402, 4939.196, 41.10839));
            WheatCoords.Add(new Vector3(2059.709, 4936.383, 41.11107));
            WheatCoords.Add(new Vector3(2056.854, 4933.475, 41.07724));
            WheatCoords.Add(new Vector3(2053.888, 4930.686, 41.07146));
            WheatCoords.Add(new Vector3(2051.254, 4927.843, 41.13422));
            WheatCoords.Add(new Vector3(2048.445, 4924.925, 41.09797));
            WheatCoords.Add(new Vector3(2045.851, 4922.027, 41.12156));
            WheatCoords.Add(new Vector3(2042.598, 4918.922, 41.05925));

            GenerateWheat();
        }
        //public void GenerateIronCoords()
        //{
        //    IronCoords.Add(new Vector3(-536.5048, 1980.177, 127.1066));
        //    IronCoords.Add(new Vector3(-527.9396, 1981.272, 126.8919));
        //    IronCoords.Add(new Vector3(-519.8535, 1977.967, 126.5759));
        //    IronCoords.Add(new Vector3(-511.0958, 1977.526, 126.4923));
        //    IronCoords.Add(new Vector3(-502.8426, 1980.972, 125.9305));
        //    IronCoords.Add(new Vector3(-481.4002, 1985.859, 124.2487));
        //    IronCoords.Add(new Vector3(-492.7108, 1981.635, 125.0135));
        //    GenerateIron();
        //}
        public void GenerateWheat()
        {
            foreach (Vector3 o in WheatCoords)
            {
                Vector3 Object = new Vector3(0, 0, 0);
                Object = new Vector3(o.X, o.Y, o.Z - 1.0);
                GTANetworkAPI.Object Wheat = NAPI.Object.CreateObject(3660027849, Object, new Vector3(0, 0, 0), 255, 0);
                Vector3 WheatPos = new Vector3(0, 0, 0);
                WheatPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                FarmingModel WheatItem = new FarmingModel();
                WheatItem.itemPosition = WheatPos;
                WheatItem.itemName = "Wheat";
                WheatItem.itemDesc = "Copper Metal can be sold to a factory.";
                WheatItem.itemObject = Wheat;
                WheatItem.itemHud = NAPI.TextLabel.CreateTextLabel("Wheat", WheatPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                WheatItem.Prompt = NAPI.TextLabel.CreateTextLabel("Press E to Plant", Object, 10, 2, 4, new Color(255, 255, 255), true, 0);
                FarmingList.Add(WheatItem);
            }

        }
        //public void GenerateIron()
        //{
        //    foreach (Vector3 o in IronCoords)
        //    {
        //        GTANetworkAPI.Object Iron = NAPI.Object.CreateObject(1471437843, o, new Vector3(0, 0, 0), 255, 0);
        //        Vector3 newPos = new Vector3(0, 0, 0);
        //        newPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
        //        OreModel IronItem = new OreModel();
        //        IronItem.itemPosition = newPos;
        //        IronItem.itemName = "Iron";
        //        IronItem.itemDesc = "Iron Ingots can be sold to a factory.";
        //        IronItem.itemObject = Iron;
        //        IronItem.itemHud = NAPI.TextLabel.CreateTextLabel("Iron", newPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
        //        IronList.Add(IronItem);
        //    }

        //}
        //[Command("regenore")]
        //public void Regenore(Client player)
        //{
        //    if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_ADMIN)
        //    {
        //        GenerateOre();
        //        GenerateIron();
        //    }

        //}
        public static void OnPlayerDisconnected(Client player)
        {
            if (farmingTimerList.TryGetValue(player.Value, out Timer farmingTimer))
            {
                // Remove the timer
                farmingTimer.Dispose();
                farmingTimerList.Remove(player.Value);
            }
        }
        public static void AnimateWheatPlant(Client player, bool isFarming)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer farmingTimer = null;

            if (isFarming)
            {
                // Create the mining timer
                farmingTimer = new Timer(PlantAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            farmingTimerList.Add(player.Value, farmingTimer);
        }
        public static void AnimateWheatMaintain(Client player, bool isFarmingMaintain)
        {

            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer farmingTimer = null;

            if (isFarmingMaintain)
            {
                // Create the mining timer
                farmingTimer = new Timer(MaintainAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            farmingTimerList.Add(player.Value, farmingTimer);
        }
        public static PlayerModel GetCharacterById(int characterId)
        {
            // Get the business given an specific identifier
            return playerList.Where(character => character.id == characterId).FirstOrDefault();
        }
        [Command("Planting")]
        public static void PlantingCommand(Client player)
        {
            foreach (FarmingModel WheatItem in FarmingList)
            {
                if (WheatItem.itemHud.Position.DistanceTo(player.Position) < 5)
                {
                    if (farmingTimerList.ContainsKey(player.Value))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are already planting.");
                        return;
                    }

                    if (player == player)
                    {
                        //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                        //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                        //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                        //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                        //  {
                        //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                        //      return;
                        //  }

                        //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                        player.SetData(EntityData.PlayerMining, true);
                        player.PlayAnimation("amb@world_human_stand_fishing@base", "base", (int)Constants.AnimationFlags.Loop);
                        AnimateWheatPlant(player, true);
                        player.TriggerEvent("playermining"); // Not required atm
                        return;

                    }

                }
            }

        }
        [Command("Maintain")]
        public static void MaintainCommand(Client player)
        {
            foreach (FarmingModel WheatItem in FarmingList)
            {
                if (WheatItem.itemHud.Position.DistanceTo(player.Position) < 5)
                {
                    if (farmingTimerList.ContainsKey(player.Value))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                        return;
                    }


                    WheatItem.itemFound = true;
                    WheatItem.itemHud.Delete();
                    WheatItem.itemObject.Delete();
                    if (player == player)
                    {
                        //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                        //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                        //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                        //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                        //  {
                        //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                        //      return;
                        //  }

                        //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                        player.SetData(EntityData.PlayerMining, true);
                        AnimateWheatMaintain(player, true);
                        player.TriggerEvent("startMining"); // Not required atm
                        return;
                    }
                }
            }
        }


        private static void PlantAsync(object Wheat)
        {
            // Get the client mining
            Client player = (Client)Wheat;

            // Stop the mining animation
            player.StopAnimation();

            //// Give the ore to the player
            //int amount = 1;

            //// Check if the player has any Copper ore in the inventory
            //int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            //ItemModel WheatItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_WHEAT);

            //if (WheatItem == null)
            //{
            //    // Create the object
            //    WheatItem = new ItemModel()
            //    {
            //        amount = amount,
            //        dimension = 0,
            //        position = new Vector3(),
            //        hash = Constants.ITEM_HASH_WHEAT,
            //        ownerEntity = Constants.ITEM_ENTITY_PLAYER,
            //        ownerIdentifier = playerId,
            //        objectHandle = null,
            //        quality = "Poor"
            //    };
            //    Database.AddNewItem(WheatItem);
            //    Globals.itemList.Add(WheatItem);
            //    player.SendNotification("You gained 1 XP in Herbalism");
            //    player.SendChatMessage(Constants.COLOR_YELLOW + "You planted a seed of Wheat");
            //}
            //else
            //{
            //    // Add the amount
            //    WheatItem.amount += amount;

            //    // Update the amount into the database
            //    // Update the item's amount
            //    player.SetData(EntityData.PlayerMining, false);
            //    Database.UpdateItem(WheatItem);
            //    //  Globals.itemList.Add(CopperItem);
            //    // Send the confirmation message
            //    player.SendNotification("You gained 1 XP in Herbalism");
            //    player.SendChatMessage(Constants.COLOR_YELLOW + "You planted 1 Copper Ore");
            //}


            // Remove the timer from the list
            farmingTimerList.Remove(player.Value);

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
            Growthtimer(player, true);
        }
        private static void Growthtimer(Client player, bool isFarmingMaintain)
        {
            // Create the timer
            Timer farmingTimer = null;

            if (isFarmingMaintain)
            {
                // Create the mining timer
                farmingTimer = new Timer(GrowthAsync, player, 120000, Timeout.Infinite);
            }
            // Add the timer to the list
            farmingTimerList.Add(player.Value, farmingTimer);
        }
        public static void GrowthAsync(object Wheat)
        {
            Client player = (Client)Wheat;

            foreach (FarmingModel WheatItem in FarmingList)
            {

                    WheatItem.Prompt.Delete();
                    WheatItem.itemObject.Delete();
                
            }
            foreach (FarmingModel WheatItem in FarmingList)
            {

                    WheatItem.itemHud.Delete();
                    WheatItem.itemObject.Delete();
                
            }
        }
        private static void MaintainAsync(object Wheat)
        {
            // Get the client mining
            Client player = (Client)Wheat;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel IronItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_IRON);

            if (IronItem == null)
            {
                // Create the object
                IronItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_IRON,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(IronItem);
                Globals.itemList.Add(IronItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendNotification("You gained 1 XP in Strenght");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Iron Ore");
            }
            else
            {
                // Add the amount
                IronItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(IronItem);
                //Globals.itemList.Add(IronItem);  THis might dupliacte the item :S
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendNotification("You gained 1 XP in Strenght");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Iron Ore");
            }


            // Remove the timer from the list
            farmingTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    skills.strenghtexp = skills.strenghtexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 30)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 50)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 60)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 70)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 90)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 100)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }

                    Database.SaveSkills(PlayerSkillList);
                }
            }

        }
        [Command("Smelt")]
        public static void SmeltCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_SMELTING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_smelting);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Copper = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPER);
            if (Copper != null && Copper.amount > 0)
            {
                foreach (Vector3 Factory in Constants.FACTORY_POSITION_LIST)
                {
                    // Check if the player is close to the area
                    if (player.Position.DistanceTo(Factory) > 6f) continue;
                    {
                        if (Copper.amount == 1)
                        {
                         // Remove the baits from the inventory
                         Globals.itemList.Remove(Copper);
                         Database.RemoveItem(Copper.id);
                              
                            
                        }
                        else
                        {
                         Copper.amount--;  
                         // Update the amount
                         Database.UpdateItem(Copper);                                                        
                        }

                        // Start fishing minigame
                        player.SetData(EntityData.PLAYER_COCAINCOOK, true);
                    }
                    AnimateSmelting(player, true);
                    return;
                }

                // Player's not in the fishing zone
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_smelting_zone);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
            }
        }
        public static void AnimateSmelting(Client player, bool isSmelting)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isSmelting)
            {
                // Create the mining timer
                miningTimer = new Timer(SmeltingAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            farmingTimerList.Add(player.Value, miningTimer);
        }
        private static void SmeltingAsync(object Copper)
        {
            // Get the client mining
            Client player = (Client)Copper;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CopperbarItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPERBAR);

            if (CopperbarItem != null)
            {
                // Add the amount
                CopperbarItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_COCAINCOOK, false);
                Database.UpdateItem(CopperbarItem);
                Globals.itemList.Add(CopperbarItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Copper bar");
            }
            else
            {
                // Create the object
                CopperbarItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COPPERBAR,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(CopperbarItem);
                Globals.itemList.Add(CopperbarItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Copper bar");
            }


            // Remove the timer from the list
            farmingTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 30)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 50)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 60)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 70)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 90)
                    {
                        skills.mininglevel  = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 100)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
    }
}
    




