using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.house;
using WiredPlayers.business;
using WiredPlayers.chat;
using WiredPlayers.weapons;
using WiredPlayers.parking;
using WiredPlayers.vehicles;
using WiredPlayers.drivingschool;
using WiredPlayers.factions;
using WiredPlayers.jobs;
using WiredPlayers.character;
using WiredPlayers.messages.general;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using WiredPlayers.messages.success;
using WiredPlayers.messages.administration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.globals
{
    public class Globals : Script
    {
        private int fastFoodId = 1;
        //private int burgerFlopperId = 1;
        public static int orderGenerationTime;
        public static List<FastfoodOrderModel> fastFoodOrderList;
        //  public static List<BurgerFlopperOrderModel> burgerFlopperOrderList;
        public static List<OrderModel> truckerOrderList;
        public static List<ItemModel> itemList;
        public static List<ScoreModel> scoreList;
        public static List<AdminTicketModel> adminTicketList;
        private static Timer playerListUpdater;
        private static Timer minuteTimer;

        public Globals()
        {
            NAPI.Server.SetGlobalServerChat(false);
            adminTicketList = new List<AdminTicketModel>();
            // burgerFlopperOrderList = new List<BurgerFlopperOrderModel>();
            fastFoodOrderList = new List<FastfoodOrderModel>();
            truckerOrderList = new List<OrderModel>();

            // Area in the lobby to change the character
            NAPI.TextLabel.CreateTextLabel(GenRes.character_help, new Vector3(401.8016f, -1001.897f, -99.00404f), 20.0f, 0.75f, 4, new Color(255, 255, 255), false);

            // Add car dealer's interior
            NAPI.World.RequestIpl("shr_int");
            NAPI.World.RequestIpl("shr_int_lod");
            NAPI.World.RemoveIpl("fakeint");
            NAPI.World.RemoveIpl("fakeint_lod");
            NAPI.World.RemoveIpl("fakeint_boards");
            NAPI.World.RemoveIpl("fakeint_boards_lod");
            NAPI.World.RemoveIpl("shutter_closed");

            // Add clubhouse's door
            NAPI.World.RequestIpl("hei_bi_hw1_13_door");

            // Close the doors from the lobby
            NAPI.Object.CreateObject(NAPI.Util.GetHashKey("bkr_prop_biker_door_entry"), new Vector3(404.5286f, -996.5656f, -98.80404f), new Vector3(0.0f, 0.0f, 90.0f));
            NAPI.Object.CreateObject(NAPI.Util.GetHashKey("bkr_prop_biker_door_entry"), new Vector3(401.2006f, -997.8686f, -98.80404f), new Vector3(0.0f, 0.0f, 270.0f));

            // Avoid player's respawn
            NAPI.Server.SetAutoRespawnAfterDeath(false);
            NAPI.Server.SetAutoSpawnOnConnect(false);

            // Disable global server chat
            NAPI.Server.SetGlobalServerChat(false);

            foreach (InteriorModel interior in Constants.INTERIOR_LIST)
            {
                if (interior.blipId > 0)
                {
                    interior.blip = NAPI.Blip.CreateBlip(interior.entrancePosition);
                    interior.blip.Sprite = (uint)interior.blipId;
                    interior.blip.Name = interior.blipName;
                    interior.blip.ShortRange = true;
                }

                if (interior.captionMessage != string.Empty)
                {
                    interior.textLabel = NAPI.TextLabel.CreateTextLabel(interior.captionMessage, interior.entrancePosition, 20.0f, 0.75f, 4, new Color(255, 255, 255), false, 0);
                }
            }

            // Fastfood orders
            Random rnd = new Random();
            orderGenerationTime = GetTotalSeconds() + rnd.Next(0, 1) * 60;

            // Burger orders
            Random rnds = new Random();
            orderGenerationTime = GetTotalSeconds() + rnds.Next(0, 1) * 60;

            // Permanent timers
            minuteTimer = new Timer(OnMinuteSpent, null, 1000, 1000);
            playerListUpdater = new Timer(UpdatePlayerList, null, 750, 750);

        }


        [Command("payday")]
        public void DebugPayDay(Client player)
        {
            int adminRank = player.GetData(EntityData.PLAYER_ADMIN_RANK);

            if (adminRank == 4)
                GeneratePlayerPayday(player);
        }

        public static Client GetPlayerById(int id)
        {
            // Get the player with the selected identifier
            return NAPI.Pools.GetAllPlayers().Where(pl => pl.GetData(EntityData.PLAYER_PLAYING) != null && pl.Value == id).FirstOrDefault();
        }

        public static int GetTotalSeconds()
        {
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private void UpdatePlayerList(object unused)
        {
            NAPI.Task.Run(() =>
            {
                // Initialize the score list
                List<ScoreModel> scoreList = new List<ScoreModel>();

                // Get all the players ingame
                List<Client> playingPlayers = NAPI.Pools.GetAllPlayers().Where(p => p.GetData(EntityData.PLAYER_PLAYING) != null).ToList();

                // Update the score list
                foreach (Client player in playingPlayers)
                {
                    ScoreModel score = new ScoreModel(player.Value, player.Name, player.Ping);
                    scoreList.Add(score);
                }

                // Update the list for all the players
                playingPlayers.ForEach(p => p.TriggerEvent("updatePlayerList", NAPI.Util.ToJson(scoreList)));
            });
        }

        private void OnMinuteSpent(object unused)
        {
            NAPI.Task.Run(() =>
            {
                // Adjust server's time
                TimeSpan currentTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
                NAPI.World.SetTime(currentTime.Hours, currentTime.Minutes, currentTime.Seconds);

                // Synchronize the weather
                NAPI.World.SetWeather(NAPI.World.GetWeather());

                int totalSeconds = GetTotalSeconds();
                List<Client> onlinePlayers = NAPI.Pools.GetAllPlayers().Where(pl => pl.GetData(EntityData.PLAYER_PLAYING) != null).ToList();

                foreach (Client player in onlinePlayers)
                {
                    if (currentTime.Minutes == 59 && currentTime.Seconds == 59)
                    {
                        // Reduce job cooldown
                        int employeeCooldown = player.GetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN);
                        if (employeeCooldown > 0)
                        {
                            player.SetData(EntityData.PLAYER_EMPLOYEE_COOLDOWN, employeeCooldown - 1);
                        }

                        // Generate the payday
                        GeneratePlayerPayday(player);
                    }

                    // Check if the player is injured waiting for the hospital respawn
                    if (player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) != null && player.GetData(EntityData.TIME_HOSPITAL_RESPAWN) <= totalSeconds)
                    {
                        // Send the death warning
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_can_die);
                    }

                    // Check if the player has job cooldown
                    int jobCooldown = player.GetData(EntityData.PLAYER_JOB_COOLDOWN);
                    if (jobCooldown > 0)
                    {
                        player.SetData(EntityData.PLAYER_JOB_COOLDOWN, jobCooldown - 1);
                    }

                    // Get the remaining jail time
                    int jailTime = player.GetData(EntityData.PLAYER_JAILED);

                    if (jailTime > 0)
                    {
                        jailTime--;
                        player.SetData(EntityData.PLAYER_JAILED, jailTime);
                    }
                    else if (jailTime == 0)
                    {
                        // Set the player position
                        player.Position = Constants.JAIL_SPAWNS[player.GetData(EntityData.PLAYER_JAIL_TYPE) == Constants.JAIL_TYPE_IC ? 3 : 4];

                        // Remove player from jail
                        player.SetData(EntityData.PLAYER_JAILED, -1);
                        player.SetData(EntityData.PLAYER_JAIL_TYPE, -1);

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_unjailed);
                    }

                    if (player.GetData(EntityData.PLAYER_DRUNK_LEVEL) != null)
                    {
                        // Lower alcohol level
                        float drunkLevel = player.GetData(EntityData.PLAYER_DRUNK_LEVEL) - 0.05f;

                        if (drunkLevel <= 0.0f)
                        {
                            player.ResetData(EntityData.PLAYER_DRUNK_LEVEL);
                        }
                        else
                        {
                            if (drunkLevel < Constants.WASTED_LEVEL)
                            {
                                player.ResetSharedData(EntityData.PLAYER_WALKING_STYLE);
                                NAPI.ClientEvent.TriggerClientEventForAll("resetPlayerWalkingStyle", player.Handle);
                            }

                            player.SetData(EntityData.PLAYER_DRUNK_LEVEL, drunkLevel);
                        }
                    }

                    // Save the character's data
                    Character.SaveCharacterData(player);
                }
                // Generate new BurgerFlopper orders
                /* if (orderGenerationTime <= totalSeconds && House.houseList.Count > 0)
                 {
                     Random rnds = new Random();
                     int generatedOrders = rnds.Next(7, 20);
                     for (int i = 0; i < generatedOrders; i++)
                     {
                         BurgerFlopperOrderModel order = new BurgerFlopperOrderModel();
                         {
                             order.id = burgerFlopperId;
                             order.pizzas = rnds.Next(0, 4);
                             order.hamburgers = rnds.Next(0, 4);
                             order.sandwitches = rnds.Next(0, 4);
                             order.position = GetPlayerBurgerFlopperDeliveryDestination();
                             order.limit = totalSeconds + 300;
                             order.taken = false;
                         }

                         burgerFlopperOrderList.Add(order);
                         burgerFlopperId++;
                     }

                     // Update the new timer time
                     orderGenerationTime = totalSeconds + rnds.Next(2, 5) * 60;
                 }
                 // Remove old orders
               //  burgerFlopperOrderList.RemoveAll(order => !order.taken && order.limit <= totalSeconds);
               */
                // Generate new fastfood orders
                if (orderGenerationTime <= totalSeconds && House.houseList.Count > 0)
                {
                    Random rnd = new Random();
                    int generatedOrders = rnd.Next(7, 20);
                    for (int i = 0; i < generatedOrders; i++)
                    {
                        FastfoodOrderModel order = new FastfoodOrderModel();
                        {
                            order.id = fastFoodId;
                            order.pizzas = rnd.Next(0, 4);
                            order.hamburgers = rnd.Next(0, 4);
                            order.sandwitches = rnd.Next(0, 4);
                            order.position = GetPlayerFastFoodDeliveryDestination();
                            order.limit = totalSeconds + 300;
                            order.taken = false;
                        }

                        fastFoodOrderList.Add(order);
                        fastFoodId++;
                    }

                    // Update the new timer time
                    orderGenerationTime = totalSeconds + rnd.Next(2, 5) * 60;
                }
                // Remove old orders
                fastFoodOrderList.RemoveAll(order => !order.taken && order.limit <= totalSeconds);

                // Generate new BurgerFlopper orders
                //if (orderGenerationTime <= totalSeconds && House.houseList.Count > 0)
                //{
                // Random rnd = new Random();
                //int generatedOrders = rnd.Next(7, 20);
                // (int i = 0; i < generatedOrders; i++)
                //
                // order = new BurgerFlopperOrderModel();
                //{
                //order.id = burgerFlopperId;
                //.pizzas = rnd.Next(0, 4);
                //.hamburgers = rnd.Next(0, 4);
                //.sandwitches = rnd.Next(0, 4);
                //.position = GetPlayerBurgerFlopperDeliveryDestination();
                //.limit = totalSeconds + 300;
                //order.taken = false;
                //

                //burgerFlopperOrderList.Add(order);
                //burgerFlopperId++;
                //

                // Update the new timer time
                //orderGenerationTime = totalSeconds + rnd.Next(2, 5) * 60;
                //}
                // Remove old orders
                //burgerFlopperOrderList.RemoveAll(order => !order.taken && order.limit <= totalSeconds);

                // Save all the vehicles
                Vehicles.SaveAllVehicles();
            });
        }

        private void GeneratePlayerPayday(Client player)
        {
            int total = 0;
            int bank = player.GetSharedData(EntityData.PLAYER_BANK);
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerRank = player.GetData(EntityData.PLAYER_RANK);
            int playerFaction = player.GetData(EntityData.PLAYER_FACTION);
            int played = player.GetData(EntityData.PLAYER_PLAYED); /// RESPECT POINTS

            player.SetData(EntityData.PLAYER_PLAYED, played + 1);

            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.payday_title);

            // Generate the salary
            if (playerFaction > 0 && playerFaction <= Constants.LAST_STATE_FACTION)
            {
                foreach (FactionModel faction in Constants.FACTION_RANK_LIST)
                {
                    if (faction.faction == playerFaction && faction.rank == playerRank)
                    {
                        total += faction.salary;
                        break;
                    }
                }
            }
            else
            {
                foreach (JobModel job in Constants.JOB_LIST)
                {
                    if (job.job == playerJob)
                    {
                        total += job.salary;
                        break;
                    }
                }
            }
            player.SendChatMessage(Constants.COLOR_HELP + GenRes.salary + total + "$");

            // Extra income from the level
            int levelEarnings = GetPlayerLevel(player) * Constants.PAID_PER_LEVEL;
            total += levelEarnings;
            if (levelEarnings > 0)
            {
                player.SendChatMessage(Constants.COLOR_HELP + GenRes.extra_income + levelEarnings + "$");
            }

            // Bank interest
            int bankInterest = (int)Math.Round(bank * 0.001);
            total += bankInterest;
            if (bankInterest > 0)
            {
                player.SendChatMessage(Constants.COLOR_HELP + GenRes.bank_interest + bankInterest + "$");
            }

            // Generación de impuestos por vehículos
            foreach (Vehicle vehicle in NAPI.Pools.GetAllVehicles())
            {
                VehicleHash vehicleHass = (VehicleHash)vehicle.Model;
                if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name && NAPI.Vehicle.GetVehicleClass(vehicleHass) != Constants.VEHICLE_CLASS_CYCLES)
                {
                    int vehicleTaxes = (int)Math.Round(vehicle.GetData(EntityData.VEHICLE_PRICE) * Constants.TAXES_VEHICLE);
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    string vehiclePlate = vehicle.GetData(EntityData.VEHICLE_PLATE) == string.Empty ? "LS " + (1000 + vehicleId) : vehicle.GetData(EntityData.VEHICLE_PLATE);
                    player.SendChatMessage(Constants.COLOR_HELP + GenRes.vehicle_taxes_from + vehicle.DisplayName + " (" + vehiclePlate + "): -" + vehicleTaxes + "$");
                    total -= vehicleTaxes;
                }
            }

            // Vehicle taxes
            foreach (ParkedCarModel parkedCar in Parking.parkedCars)
            {
                VehicleHash vehicleHass = (VehicleHash)parkedCar.vehicle.model;
                if (parkedCar.vehicle.owner == player.Name && NAPI.Vehicle.GetVehicleClass(vehicleHass) != Constants.VEHICLE_CLASS_CYCLES)
                {
                    int vehicleTaxes = (int)Math.Round(parkedCar.vehicle.price * Constants.TAXES_VEHICLE);
                    string vehiclePlate = parkedCar.vehicle.plate == string.Empty ? "LS " + (1000 + parkedCar.vehicle.id) : parkedCar.vehicle.plate;
                    player.SendChatMessage(Constants.COLOR_HELP + GenRes.vehicle_taxes_from + NAPI.Vehicle.GetVehicleDisplayName(vehicleHass) + " (" + vehiclePlate + "): -" + vehicleTaxes + "$");
                    total -= vehicleTaxes;
                }
            }

            // House taxes
            foreach (HouseModel house in House.houseList)
            {
                if (house.owner == player.Name)
                {
                    int houseTaxes = (int)Math.Round(house.price * Constants.TAXES_HOUSE);
                    player.SendChatMessage(Constants.COLOR_HELP + GenRes.house_taxes_from + house.name + ": -" + houseTaxes + "$");
                    total -= houseTaxes;
                }
            }

            // Calculate the total balance
            player.SendChatMessage(Constants.COLOR_HELP + "=====================");
            player.SendChatMessage(Constants.COLOR_HELP + GenRes.total + total + "$");
            player.SetSharedData(EntityData.PLAYER_BANK, bank + total);

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Add the payment log
                    Database.LogPayment("Payday", player.Name, "Payday", total);
                });
            });
        }

        private Vector3 GetPlayerFastFoodDeliveryDestination()
        {
            Random random = new Random();
            int element = random.Next(House.houseList.Count);
            return House.houseList[element].position;
        }
        /*  private Vector3 GetPlayerBurgerFlopperDeliveryDestination()
          {
              Random random = new Random();
              int element = random.Next(House.houseList.Count);
              return House.houseList[element].position;
          }
          */
        public static ItemModel GetItemModelFromId(int itemId)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in itemList)
            {
                if (itemModel.id == itemId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static ItemModel GetPlayerItemModelFromHash(int playerId, string hash)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && item.ownerIdentifier == playerId && item.hash == hash)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetClosestItem(Client player)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_GROUND && player.Position.DistanceTo(item.position) < 2.0f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetClosestItemWithHash(Client player, string hash)
        {
            ItemModel itemModel = null;
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_GROUND && item.hash == hash && player.Position.DistanceTo(item.position) < 2.0f)
                {
                    itemModel = item;
                    break;
                }
            }
            return itemModel;
        }

        public static ItemModel GetItemInEntity(int entityId, string entity)
        {
            ItemModel item = null;
            foreach (ItemModel itemModel in itemList)
            {
                if (itemModel.ownerEntity == entity && itemModel.ownerIdentifier == entityId)
                {
                    item = itemModel;
                    break;
                }
            }
            return item;
        }

        public static Vector3 GetForwardPosition(Entity entity, float distance)
        {
            Vector3 position = entity.Position;

            // Get the X and Y coordinates
            position.X += distance * (float)Math.Sin(180.0f - entity.Rotation.Z);
            position.Y += distance * (float)Math.Cos(180.0f - entity.Rotation.Z);

            return position;
        }

        private void SubstractPlayerItems(ItemModel item, int amount = 1)
        {
            item.amount -= amount;
            if (item.amount == 0)
            {
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // Remove the item from the database
                        Database.RemoveItem(item.id);
                        itemList.Remove(item);
                    });
                });
            }
        }

        private int GetPlayerInventoryTotal(Client player)
        {
            // Return the amount of items in the player's inventory
            return itemList.Count(item => item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && player.GetData(EntityData.PLAYER_SQL_ID) == item.ownerIdentifier);
        }

        private List<InventoryModel> GetPlayerInventory(Client player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_PLAYER && item.ownerIdentifier == playerId)
                {
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);
                    if (businessItem != null && businessItem.type != Constants.ITEM_TYPE_WEAPON)
                    {
                        // Create the item into the inventory
                        InventoryModel inventoryItem = new InventoryModel();
                        {
                            inventoryItem.id = item.id;
                            inventoryItem.hash = item.hash;
                            inventoryItem.description = businessItem.description;
                            inventoryItem.type = businessItem.type;
                            inventoryItem.amount = item.amount;
                        }

                        // Add the item to the inventory
                        inventory.Add(inventoryItem);
                    }
                }
            }
            return inventory;
        }

        private void UpdateInventory(Client player, ItemModel item, BusinessItemModel businessItem, int target)
        {
            // Check if the item is a weapon
            WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);

            // Get the description and type
            int type = weaponHash != 0 ? Constants.ITEM_TYPE_WEAPON : businessItem.type;
            string description = weaponHash != 0 ? weaponHash.ToString() : businessItem.description;

            // Create the inventory item to update
            InventoryModel inventoryItem = new InventoryModel();
            {
                inventoryItem.id = item.id;
                inventoryItem.hash = item.hash;
                inventoryItem.description = description;
                inventoryItem.type = type;
                inventoryItem.amount = item.amount;
            }

            // Update the inventory
            player.TriggerEvent("updateInventory", NAPI.Util.ToJson(inventoryItem), target);
        }

        private void ConsumeItem(Client player, ItemModel item, BusinessItemModel businessItem, bool consumedFromHand)
        {
            item.amount--;

            // Check if it changes the health
            if (businessItem.health != 0)
            {
                player.Health += businessItem.health;
                if (player.Health > 100) player.Health = 100;
            }

            if (businessItem.alcoholLevel > 0)
            {
                float currentAlcohol = 0;
                if (player.GetData(EntityData.PLAYER_DRUNK_LEVEL) != null)
                {
                    currentAlcohol = player.GetData(EntityData.PLAYER_DRUNK_LEVEL);
                }
                player.SetData(EntityData.PLAYER_DRUNK_LEVEL, currentAlcohol + businessItem.alcoholLevel);

                if (currentAlcohol + businessItem.alcoholLevel > Constants.WASTED_LEVEL)
                {
                    player.SetSharedData(EntityData.PLAYER_WALKING_STYLE, "move_m@drunk@verydrunk");
                    NAPI.ClientEvent.TriggerClientEventForAll("changePlayerWalkingStyle", player.Handle, "move_m@drunk@verydrunk");
                }
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    if (item.amount == 0)
                    {
                        // Remove the item from the hand
                        NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "dettachItemFromPlayer", player.Value);

                        player.ResetSharedData(EntityData.PLAYER_RIGHT_HAND);

                        // Remove the item from the database
                        Database.RemoveItem(item.id);
                        itemList.Remove(item);
                    }
                    else
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);
                    }

                    if (!consumedFromHand)
                    {
                        // Update the inventory
                        UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_SELF);
                    }

                    string message = string.Format(InfoRes.player_inventory_consume, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                });
            });
        }

        private void DropItem(Client player, ItemModel item, BusinessItemModel businessItem, bool droppedFromHand)
        {
            // Check if there are items of the same type near
            ItemModel closestItem = GetClosestItemWithHash(player, item.hash);

            // Check if it's a weapon or not
            WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);

            // Get the dropped amount
            int amount = weaponHash != 0 ? item.amount : 1;
            item.amount -= amount;

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    if (closestItem != null)
                    {
                        closestItem.amount += amount;

                        // Update the closest item's amount
                        Database.UpdateItem(closestItem);
                    }
                    else
                    {
                        if (weaponHash != 0 || (weaponHash == 0 && uint.TryParse(item.hash, out uint hash)))
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Get the hash from the item dropped
                                uint itemHash = weaponHash != 0 ? NAPI.Util.GetHashKey(Constants.WEAPON_ITEM_MODELS[weaponHash]) : uint.Parse(item.hash);

                                closestItem = item.Copy();
                                closestItem.amount = amount;
                                closestItem.ownerEntity = Constants.ITEM_ENTITY_GROUND;
                                closestItem.dimension = player.Dimension;
                                closestItem.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 0.8f);
                                closestItem.objectHandle = NAPI.Object.CreateObject(itemHash, closestItem.position, new Vector3(0.0f, 0.0f, 0.0f), (byte)closestItem.dimension);

                                // Create the new item
                                closestItem.id = Database.AddNewItem(closestItem);
                                itemList.Add(closestItem);
                            });
                        }
                    }

                    if (item.amount == 0)
                    {
                        if (droppedFromHand)
                        {
                            // Remove the attachment information
                            player.ResetSharedData(EntityData.PLAYER_RIGHT_HAND);

                            if (weaponHash != 0)
                            {
                                // Remove the weapon from the player
                                player.RemoveWeapon(weaponHash);
                            }
                            else
                            {
                                // Remove the item from the hand
                                NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "dettachItemFromPlayer", player.Value);
                            }
                        }

                        // There are no more items, we delete it
                        Database.RemoveItem(item.id);
                        itemList.Remove(item);
                    }
                    else
                    {
                        // Update the item's amount
                        Database.UpdateItem(item);
                    }

                    if (!droppedFromHand)
                    {
                        // Update the inventory
                        UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_SELF);
                    }

                    string message = string.Format(InfoRes.player_inventory_drop, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                });
            });
        }
        [RemoteEvent("switchCameras")]
        public void SwitchCameras(Client player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "LOBBYCAM2");
        }

        [RemoteEvent("endLobby")]
        public void EndingLobby(Client player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "ENDLOBBY");

            //InitCef.instance.CreateExampleProgressBar(player);


            if (player.GetData(EntityData.PLAYER_JOB) == 11 && player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
            {
                var clothDictionary = new Dictionary<int, ComponentVariation>();

                if (player.GetData(EntityData.PLAYER_SEX) == 0)
                {
                    //clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 }); //Mask
                    clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 }); // Legs
                    clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 }); // Shoes
                    clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                    clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });
                }
                if (player.GetData(EntityData.PLAYER_SEX) == 1)
                {
                    // clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                    clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                    clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                    clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                    clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });
                }

                NAPI.Player.SetPlayerClothes(player, clothDictionary);



                //hereee
            }
        }

        public static void StoreItemOnHand(Client player)
        {
            // Get the item
            ItemModel item = null;

            if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                item = GetItemModelFromId(NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId);

                // Remove the item from the hand
                NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "dettachItemFromPlayer", player.Value);

                // Reset the player data
                player.ResetSharedData(EntityData.PLAYER_RIGHT_HAND);
            }
            else
            {
                item = Weapons.GetWeaponItem(player, player.CurrentWeapon);
                player.GiveWeapon(WeaponHash.Unarmed, 0);
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Search for items of the same type
                    ItemModel inventoryItem = GetPlayerItemModelFromHash(player.GetData(EntityData.PLAYER_SQL_ID), item.hash);

                    if (inventoryItem == null)
                    {
                        // Store the item on the floor
                        item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;

                        // Update the amount into the database
                        Database.UpdateItem(item);
                    }
                    else
                    {
                        // Add the amount to the item in the inventory
                        inventoryItem.amount += item.amount;

                        // Delete the item on the hand
                        Database.RemoveItem(item.id);
                        itemList.Remove(item);
                    }
                });
            });
        }

        public static List<InventoryModel> GetPlayerInventoryAndWeapons(Client player)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            List<string> owners = new List<string> { Constants.ITEM_ENTITY_PLAYER, Constants.ITEM_ENTITY_WHEEL, Constants.ITEM_ENTITY_RIGHT_HAND };

            // Check the items on the owners list
            List<ItemModel> items = itemList.Where(i => owners.Contains(i.ownerEntity) && i.ownerIdentifier == playerId).ToList();

            foreach (ItemModel item in items)
            {
                BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                if (businessItem != null)
                {
                    // Create the item into the inventory
                    InventoryModel inventoryItem = new InventoryModel();
                    {
                        inventoryItem.id = item.id;
                        inventoryItem.hash = item.hash;
                        inventoryItem.description = businessItem.description;
                        inventoryItem.type = businessItem.type;
                        inventoryItem.amount = item.amount;
                    }

                    // Add the item to the inventory
                    inventory.Add(inventoryItem);
                }
            }

            return inventory;
        }

        public static List<InventoryModel> GetVehicleTrunkInventory(Vehicle vehicle)
        {
            List<InventoryModel> inventory = new List<InventoryModel>();
            int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
            foreach (ItemModel item in itemList)
            {
                if (item.ownerEntity == Constants.ITEM_ENTITY_VEHICLE && item.ownerIdentifier == vehicleId)
                {
                    // Check whether is a common item or a weapon
                    InventoryModel inventoryItem = new InventoryModel();
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                    if (businessItem != null)
                    {
                        inventoryItem.description = businessItem.description;
                        inventoryItem.type = businessItem.type;
                    }
                    else
                    {
                        inventoryItem.description = item.hash;
                        inventoryItem.type = Constants.ITEM_TYPE_WEAPON;
                    }

                    // Update the values
                    inventoryItem.id = item.id;
                    inventoryItem.hash = item.hash;
                    inventoryItem.amount = item.amount;

                    // Add the item to the inventory
                    inventory.Add(inventoryItem);
                }
            }
            return inventory;
        }

        public static void AttachItemToPlayer(Client player, int itemId, string hash, string bodyPart, Vector3 position, Vector3 rotation, string entityKey)
        {
            AttachmentModel attachment = new AttachmentModel(itemId, hash, bodyPart, position, rotation);
            string attachmentJson = NAPI.Util.ToJson(attachment);

            player.SetSharedData(entityKey, attachmentJson);

            NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "attachItemToPlayer", player.Value, attachmentJson);
        }

        public static void RemoveItemOnHands(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                // Get the item on the right hand
                string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                int itemId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                ItemModel currentItem = GetItemModelFromId(itemId);

                // Check if it's a weapon or not
                WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(currentItem.hash);

                if (weaponHash != 0)
                {
                    // Store the weapon from the player
                    player.GiveWeapon(WeaponHash.Unarmed, 0);
                }
                else
                {
                    // Remove the item from the hand
                    NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "dettachItemFromPlayer", player.Value);
                }
            }
            else if (player.GetSharedData(EntityData.PLAYER_WEAPON_CRATE) != null)
            {
                // Reset the data
                player.ResetSharedData(EntityData.PLAYER_WEAPON_CRATE);

                // Remove the item from the hand
                NAPI.ClientEvent.TriggerClientEventInDimension(player.Dimension, "dettachItemFromPlayer", player.Value);
            }
        }

        private int GetPlayerLevel(Client player)
        {
            int playedHours = player.GetData(EntityData.PLAYER_PLAYED);
            return playedHours;
        }

        [ServerEvent(GTANetworkAPI.Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            //NAPI.Native.SendNativeToPlayer(player, Hash.SET_PED_HELMET, player, false); 
        }

        [ServerEvent(GTANetworkAPI.Event.PlayerDisconnected)]
        public void OnPlayerDisconnected(Client player, DisconnectionType type, string reason)
        {
            if (player.GetData(EntityData.PLAYER_PLAYING) != null)
            {
                // Disconnect from the server
                player.ResetData(EntityData.PLAYER_PLAYING);

                // Remove opened ticket
                adminTicketList.RemoveAll(ticket => ticket.playerId == player.Value);

                // Other classes' disconnect function
                Telephone.OnPlayerDisconnected(player);
                Chat.OnPlayerDisconnected(player, type, reason);
                DrivingSchool.OnPlayerDisconnected(player, type, reason);
                FastFood.OnPlayerDisconnected(player, type, reason);
                Fishing.OnPlayerDisconnected(player, type, reason);
                Garbage.OnPlayerDisconnected(player, type, reason);
                Hooker.OnPlayerDisconnected(player, type, reason);
                Commands4everyone.OnPlayerDisconnected(player);
                Vehicles.OnPlayerDisconnected(player);
                Weapons.OnPlayerDisconnected(player);

                // Save the character's data
                Character.SaveCharacterData(player);

                // Warn the players near to the disconnected one
                string message = string.Format(InfoRes.player_disconnected, player.Name, reason);
                Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_DISCONNECT, 10.0f);
            }
        }

        [RemoteEvent("checkPlayerEventKeyStopAnim")]
        public void CheckPlayerEventKeyStopAnimEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ANIMATION) == null)
            {
                player.StopAnimation();
            }
        }

        [RemoteEvent("checkPlayerInventoryKey")]
        public void CheckPlayerInventoryKeyEvent(Client player)
        {
            if (GetPlayerInventoryTotal(player) > 0)
            {
                List<InventoryModel> inventory = GetPlayerInventory(player);
                player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_items_inventory);
            }
        }

        [RemoteEvent("checkPlayerEventKey")]
        public void CheckPlayerEventKeyEvent(Client player)
        {
            if (player.GetData(EntityData.PLAYER_PLAYING) != null)
            {
                // Check if the player's close to an ATM
                for (int i = 0; i < Constants.ATM_LIST.Count; i++)
                {
                    if (player.Position.DistanceTo(Constants.ATM_LIST[i]) <= 1.5f)
                    {
                        player.TriggerEvent("showATM");
                        return;
                    }
                }

                // Check if the player's in any business
                foreach (BusinessModel business in Business.businessList)
                {
                    if (player.Position.DistanceTo(business.position) <= 1.5f && business.ipl.Length > 0 && player.Dimension == business.dimension)
                    {
                        if (!Business.HasPlayerBusinessKeys(player, business) && business.locked)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.business_locked);
                        }
                        else
                        {
                            NAPI.World.RequestIpl(business.ipl);
                            player.Position = Business.GetBusinessExitPoint(business.ipl);
                            player.Dimension = Convert.ToUInt32(business.id);
                            player.SetData(EntityData.PLAYER_IPL, business.ipl);
                            player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, business.id);
                        }
                        return;
                    }
                    else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) == business.id && business.ipl.Length > 0)
                    {
                        Vector3 exitPosition = Business.GetBusinessExitPoint(business.ipl);
                        if (player.Position.DistanceTo(exitPosition) < 2.5f)
                        {
                            if (!Business.HasPlayerBusinessKeys(player, business) && business.locked)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.business_locked);
                            }
                            else if (player.GetData(EntityData.PLAYER_ROBBERY_START) != null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.stealing_progress);
                            }
                            else
                            {
                                player.Position = business.position;
                                player.Dimension = business.dimension;
                                player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                                player.ResetData(EntityData.PLAYER_IPL);

                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.GetData(EntityData.PLAYER_PLAYING) != null && target.GetData(EntityData.PLAYER_IPL) != null && target != player)
                                    {
                                        if (target.GetData(EntityData.PLAYER_IPL) == business.ipl)
                                        {
                                            return;
                                        }
                                    }
                                }
                                NAPI.World.RemoveIpl(business.ipl);
                            }
                        }
                        return;
                    }
                }

                // Check if the player's in any house
                foreach (HouseModel house in House.houseList)
                {
                    if (player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension)
                    {
                        if (!House.HasPlayerHouseKeys(player, house) && house.locked)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_locked);
                        }
                        else
                        {
                            NAPI.World.RequestIpl(house.ipl);
                            player.Position = House.GetHouseExitPoint(house.ipl);
                            player.Dimension = Convert.ToUInt32(house.id);
                            player.SetData(EntityData.PLAYER_IPL, house.ipl);
                            player.SetData(EntityData.PLAYER_HOUSE_ENTERED, house.id);
                        }
                        return;
                    }
                    else if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                    {
                        Vector3 exitPosition = House.GetHouseExitPoint(house.ipl);
                        if (player.Position.DistanceTo(exitPosition) < 2.5f)
                        {
                            if (!House.HasPlayerHouseKeys(player, house) && house.locked)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_locked);
                            }
                            else if (player.GetData(EntityData.PLAYER_ROBBERY_START) != null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.stealing_progress);
                            }
                            else
                            {
                                player.Position = house.position;
                                player.Dimension = house.dimension;
                                player.SetData(EntityData.PLAYER_HOUSE_ENTERED, 0);
                                player.ResetData(EntityData.PLAYER_IPL);

                                foreach (Client target in NAPI.Pools.GetAllPlayers())
                                {
                                    if (target.GetData(EntityData.PLAYER_PLAYING) != null && target.GetData(EntityData.PLAYER_IPL) != null && target != player)
                                    {
                                        if (target.GetData(EntityData.PLAYER_IPL) == house.ipl)
                                        {
                                            return;
                                        }
                                    }
                                }
                                NAPI.World.RemoveIpl(house.ipl);
                            }
                        }
                        return;
                    }
                }

                // Check if the player's in any interior
                foreach (InteriorModel interior in Constants.INTERIOR_LIST)
                {
                    if (player.Position.DistanceTo(interior.entrancePosition) < 1.5f)
                    {
                        NAPI.World.RequestIpl(interior.iplName);
                        player.Position = interior.exitPosition;
                        return;
                    }
                    else if (player.Position.DistanceTo(interior.exitPosition) < 1.5f)
                    {
                        player.Position = interior.entrancePosition;
                        return;
                    }
                }
            }
            else
            {
                Vector3 lobbyExit = new Vector3(404.6483f, -997.2951f, -99.00404f);

                if (lobbyExit.DistanceTo(player.Position) > 1.75f)
                {

                    player.Dimension = Convert.ToUInt32(player.Value);

                    // Player must have a character selected
                    if (player.GetData(EntityData.PLAYER_SQL_ID) == null)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_character_selected);
                    }
                    else
                    {
                        int playerSqlId = player.GetData(EntityData.PLAYER_SQL_ID);
                        ItemModel rightHand = GetItemInEntity(playerSqlId, Constants.ITEM_ENTITY_RIGHT_HAND);
                        ItemModel leftHand = GetItemInEntity(playerSqlId, Constants.ITEM_ENTITY_LEFT_HAND);

                        // Give the weapons to the player
                        Weapons.GivePlayerWeaponItems(player);

                        if (rightHand != null)
                        {
                            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(rightHand.hash);
                            WeaponHash weapon = NAPI.Util.WeaponNameToModel(rightHand.hash);

                            if (weapon != 0)
                            {
                                player.GiveWeapon(weapon, rightHand.amount);

                                // Create the attachment
                                AttachmentModel attachment = new AttachmentModel(rightHand.id, rightHand.hash, "IK_R_Hand", new Vector3(), new Vector3());
                                player.SetSharedData(EntityData.PLAYER_RIGHT_HAND, NAPI.Util.ToJson(attachment));
                            }
                            else
                            {
                                // Give the item to the player
                                player.GiveWeapon(WeaponHash.Unarmed, 1);
                                AttachItemToPlayer(player, rightHand.id, rightHand.hash, "IK_R_Hand", businessItem.position, businessItem.rotation, EntityData.PLAYER_RIGHT_HAND);
                            }
                        }

                        if (leftHand != null)
                        {
                            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(leftHand.hash);
                            AttachItemToPlayer(player, leftHand.id, leftHand.hash, "IK_L_Hand", businessItem.position, businessItem.rotation, EntityData.PLAYER_LEFT_HAND);
                            player.SetSharedData(EntityData.PLAYER_LEFT_HAND, leftHand.id);
                        }

                        // Calculate spawn dimension
                        if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
                        {
                            int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                            HouseModel house = House.GetHouseById(houseId);
                            player.Dimension = Convert.ToUInt32(house.id);
                            NAPI.World.RequestIpl(house.ipl);
                        }
                        else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                        {

                            int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);


                            if (lobbyExit.DistanceTo(player.Position) < 1.75f)
                            {
                                player.Dimension = Convert.ToUInt32(player.Value);
                                player.SetData(EntityData.PLAYER_BUSINESS_ENTERED, 0);
                            }
                            else
                            {
                                BusinessModel business = Business.GetBusinessById(businessId);
                                player.Dimension = Convert.ToUInt32(business.id);
                                NAPI.World.RequestIpl(business.ipl);
                            }
                        }
                        else
                        {
                            player.Dimension = 0;
                        }



                        // The player sent the player's identifier
                        string playerRageId = player.Value.ToString();

                        // Spawn the player into the world
                        //player.Name = "(( " + playerRageId + " ))";
                        player.Name = player.GetData(EntityData.PLAYER_NAME);

                        player.Position = player.GetData(EntityData.PLAYER_SPAWN_POS);
                        player.Rotation = player.GetData(EntityData.PLAYER_SPAWN_ROT);
                        player.Health = player.GetData(EntityData.PLAYER_HEALTH);
                        player.Armor = player.GetData(EntityData.PLAYER_ARMOR);

                        List<OceanVehicleModel> oceanVehicles = new List<OceanVehicleModel>();

                        oceanVehicles = Database.LoadOceanVehicles();

                        foreach (OceanVehicleModel ov in oceanVehicles)
                        {
                            if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                            {
                                player.SetData(EntityData.OCEANVEHICLE_ID, ov.id);
                                player.SetData(EntityData.OCEANVEHICLE_OWNER, ov.owner);
                                player.SetData(EntityData.OCEANVEHICLE_SCRAP, ov.scrap);
                                player.SetData(EntityData.OCEANVEHICLE_MODEL, ov.model);
                            }
                        }

                        List<AliasModel> aliases = new List<AliasModel>();

                        aliases = Database.LoadAliases();

                        foreach (AliasModel alias in aliases)
                        {
                            if (alias.playerId == player.GetData(EntityData.PLAYER_SQL_ID))
                            {
                                NAPI.Util.ConsoleOutput("1111111");
                                foreach (Client p in NAPI.Pools.GetAllPlayers())
                                {
                                    NAPI.Util.ConsoleOutput("FOUND PLAYER");

                                    if (alias.targetId == p.GetData(EntityData.PLAYER_SQL_ID))
                                    {
                                        int rageId = p.Value;
                                        NAPI.Util.ConsoleOutput("222222");

                                        string setNameData = "f" + alias.targetId.ToString();
                                        player.SetSharedData(setNameData, alias.name);
                                        player.SetSharedData(rageId.ToString(), alias.name);

                                        NAPI.Util.ConsoleOutput("33333333");

                                        //player.TriggerEvent("applyName", rageId, alias.name);
                                    }
                                }
                            }
                        }

                        if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
                        {
                            Vector3 deathPosition = null;
                            string deathPlace = string.Empty;
                            string deathHour = DateTime.Now.ToString("h:mm:ss tt");

                            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) > 0)
                            {
                                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                                HouseModel house = House.GetHouseById(houseId);
                                deathPosition = house.position;
                                deathPlace = house.name;
                            }
                            else if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                            {
                                int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                                BusinessModel business = Business.GetBusinessById(businessId);
                                deathPosition = business.position;
                                deathPlace = business.name;
                            }
                            else
                            {
                                deathPosition = player.Position;
                            }

                            // Creamos the report for the emergency department
                            FactionWarningModel factionWarning = new FactionWarningModel(Constants.FACTION_EMERGENCY, player.Value, deathPlace, deathPosition, -1, deathHour);
                            Faction.factionWarningList.Add(factionWarning);

                            string warnMessage = string.Format(InfoRes.emergency_warning, Faction.factionWarningList.Count - 1);

                            foreach (Client target in NAPI.Pools.GetAllPlayers())
                            {
                                if (target.GetData(EntityData.PLAYER_FACTION) == Constants.FACTION_EMERGENCY && target.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                                {
                                    target.SendChatMessage(Constants.COLOR_INFO + warnMessage);
                                }
                            }

                            player.SetData(EntityData.TIME_HOSPITAL_RESPAWN, GetTotalSeconds() + 240);
                            player.SendChatMessage(Constants.COLOR_INFO + InfoRes.emergency_warn);

                            // Change the death state
                            player.TriggerEvent("togglePlayerDead", true);
                        }

                        // Toggle connection flag
                        player.SetData(EntityData.PLAYER_PLAYING, true);

                        int aRank = player.GetData(EntityData.PLAYER_ADMIN_RANK);

                        player.TriggerEvent("playerLoggedIn");
                        player.TriggerEvent("permCheck", aRank);
                        player.TriggerEvent("destroyLastBrowser");

                        // Welcome message
                        player.SendChatMessage(Constants.COLOR_WHITE + "Welcome to " + Constants.COLOR_OCEANBLUE + "Oceanic Roleplay. " + Constants.COLOR_WHITE + "We hope you enjoy your stay!");
                        player.SendChatMessage(Constants.COLOR_WHITE + "To check your profile, use " + Constants.COLOR_YELLOW + "/profile");

                        player.SendChatMessage(Constants.COLOR_WHITE + "If you need help, you can use " + Constants.COLOR_YELLOW + "/helpme");
                        player.SendChatMessage(Constants.COLOR_WHITE + "Use" + Constants.COLOR_YELLOW + " F2 " + Constants.COLOR_WHITE + "to toggle the mouse cursor.");

                        //player.SendChatMessage(GenRes.welcome_hint);
                        //player.SendChatMessage(GenRes.help_hint);
                        //player.SendChatMessage(GenRes.ticket_hint);

                        List<VehicleModel> vehicles = Database.LoadAllVehicles();
                        List<HouseModel> houses = Database.LoadAllHouses();
                        List<BusinessModel> businesses = Database.LoadAllBusiness();

                        int ownedVehiclesAmount = 0;
                        int ownedHousesAmount = 0;
                        int ownedBusinessesAmount = 0;

                        foreach (VehicleModel veh in vehicles)
                        {
                            if (veh.owner == player.GetData(EntityData.PLAYER_NAME))
                            {
                                ownedVehiclesAmount = ownedVehiclesAmount + 1;

                                if (veh.model == (uint)VehicleHash.Submersible2 || veh.model == (uint)VehicleHash.Scrap)
                                {
                                    ownedVehiclesAmount = ownedVehiclesAmount - 1;
                                }

                                player.SetData(EntityData.PLAYER_VEHICLES, ownedVehiclesAmount);
                            }
                        }

                        foreach (HouseModel h in houses)
                        {
                            if (h.owner == player.GetData(EntityData.PLAYER_NAME))
                            {
                                ownedHousesAmount = ownedHousesAmount + 1;
                                player.SetData(EntityData.PLAYER_HOUSES, ownedHousesAmount);
                            }
                        }

                        foreach (BusinessModel bm in businesses)
                        {
                            if (bm.owner == player.GetData(EntityData.PLAYER_NAME))
                            {
                                ownedBusinessesAmount = ownedBusinessesAmount + 1;
                                player.SetData(EntityData.PLAYER_BUSINESSES, ownedBusinessesAmount);
                            }
                        }
                    }
                }
            }
        }

        [RemoteEvent("processMenuAction")]
        public void ProcessMenuActionEvent(Client player, int itemId, string action)
        {
            string message = string.Empty;
            ItemModel item = GetItemModelFromId(itemId);
            BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

            switch (action.ToLower())
            {
                case Commands.COM_CONSUME:
                    // Consume the selected item
                    ConsumeItem(player, item, businessItem, false);

                    break;
                case Commands.ARG_OPEN:
                    switch (item.hash)
                    {
                        case Constants.ITEM_HASH_PACK_BEER_AM:
                            ItemModel itemModel = GetPlayerItemModelFromHash(player.GetData(EntityData.PLAYER_SQL_ID), Constants.ITEM_HASH_BOTTLE_BEER_AM);
                            if (itemModel == null)
                            {
                                // Create the item
                                itemModel = new ItemModel();
                                {
                                    itemModel.hash = Constants.ITEM_HASH_BOTTLE_BEER_AM;
                                    itemModel.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                    itemModel.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);
                                    itemModel.amount = Constants.ITEM_OPEN_BEER_AMOUNT;
                                    itemModel.position = new Vector3(0.0f, 0.0f, 0.0f);
                                    itemModel.dimension = player.Dimension;
                                }

                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Create the new item
                                        itemModel.id = Database.AddNewItem(itemModel);
                                        itemList.Add(itemModel);
                                    });
                                });
                            }
                            else
                            {
                                // Add the amount to the current item
                                itemModel.amount += Constants.ITEM_OPEN_BEER_AMOUNT;


                                Task.Factory.StartNew(() =>
                                {
                                    NAPI.Task.Run(() =>
                                    {
                                        // Update the amount into the database
                                        Database.UpdateItem(item);
                                    });
                                });
                            }
                            break;
                    }

                    // Substract container amount
                    SubstractPlayerItems(item);

                    message = string.Format(InfoRes.player_inventory_open, businessItem.description.ToLower());
                    player.SendChatMessage(Constants.COLOR_INFO + message);

                    // Update the inventory
                    UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_SELF);

                    break;
                case Commands.ARG_EQUIP:
                    if (Inventory.HasPlayerItemOnHand(player))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_occupied);
                    }
                    else
                    {
                        // Set the item into the hand
                        item.ownerEntity = Constants.ITEM_ENTITY_RIGHT_HAND;
                        AttachItemToPlayer(player, item.id, item.hash, "IK_R_Hand", businessItem.position, businessItem.rotation, EntityData.PLAYER_RIGHT_HAND);

                        // Store the player's current weapon
                        player.GiveWeapon(WeaponHash.Unarmed, 0);

                        message = string.Format(InfoRes.player_inventory_equip, businessItem.description.ToLower());
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    break;
                case Commands.COM_DROP:
                    // Drop the item from the inventory
                    DropItem(player, item, businessItem, false);

                    break;
                case Commands.ARG_CONFISCATE:
                    Client target = player.GetData(EntityData.PLAYER_SEARCHED_TARGET);

                    // Transfer the item from the target to the player
                    item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                    item.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the amount into the database
                            Database.UpdateItem(item);
                        });
                    });

                    string playerMessage = string.Format(InfoRes.police_retired_items_to, target.Name);
                    string targetMessage = string.Format(InfoRes.police_retired_items_from, player.Name);
                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                    target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                    // Update the inventory
                    UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_PLAYER);

                    break;
                case Commands.ARG_STORE:
                    Vehicle targetVehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);

                    // Transfer the item from the player to the vehicle
                    item.ownerEntity = Constants.ITEM_ENTITY_VEHICLE;
                    item.ownerIdentifier = targetVehicle.GetData(EntityData.VEHICLE_ID);

                    // Remove the weapon if it's a weapon
                    player.RemoveWeapon(NAPI.Util.WeaponNameToModel(item.hash));

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the amount into the database
                            Database.UpdateItem(item);
                        });
                    });

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.trunk_stored_items);

                    // Update the inventory
                    UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_VEHICLE_PLAYER);

                    break;
                case Commands.ARG_WITHDRAW:
                    // Check if the player has any item on the hand
                    if (Inventory.HasPlayerItemOnHand(player))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_occupied);
                        return;
                    }

                    Vehicle sourceVehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);

                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);

                    if (weaponHash != 0)
                    {
                        // Give the weapon to the player
                        item.ownerEntity = Constants.ITEM_ENTITY_WHEEL;
                        player.GiveWeapon(weaponHash, 0);
                        player.SetWeaponAmmo(weaponHash, item.amount);
                    }
                    else
                    {
                        // Place the item into the inventory
                        item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                        player.GiveWeapon(WeaponHash.Unarmed, 0);
                    }

                    // Transfer the item from the vehicle to the player
                    item.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the amount into the database
                            Database.UpdateItem(item);
                        });
                    });

                    Chat.SendMessageToNearbyPlayers(player, InfoRes.trunk_item_withdraw, Constants.MESSAGE_ME, 20.0f);
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.trunk_withdraw_items);

                    // Update the inventory
                    UpdateInventory(player, item, businessItem, Constants.INVENTORY_TARGET_VEHICLE_TRUNK);

                    break;
            }
        }

        [RemoteEvent("closeVehicleTrunk")]
        public void CloseVehicleTrunkEvent(Client player)
        {
            Vehicle vehicle = player.GetData(EntityData.PLAYER_OPENED_TRUNK);

            if (vehicle == null)
                return;

            vehicle.CloseDoor(Constants.VEHICLE_TRUNK);
            player.ResetData(EntityData.PLAYER_OPENED_TRUNK);
        }

        [RemoteEvent("getPlayerTattoos")]
        public void GetPlayerTattoosEvent(Client player, Client targetPlayer)
        {
            int targetId = targetPlayer.GetData(EntityData.PLAYER_SQL_ID);
            List<TattooModel> playerTattooList = Customization.tattooList.Where(t => t.player == targetId).ToList();
            player.TriggerEvent("updatePlayerTattoos", NAPI.Util.ToJson(playerTattooList), targetPlayer);
        }

      

        [Command(Commands.COM_STORE)]
        public void StoreCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                // Store the item on right hand
                StoreItemOnHand(player);
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_empty);
            }
        }

        [Command(Commands.COM_CONSUME)]
        public void ConsumeCommand(Client player)
        {
            if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                int itemId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                ItemModel item = GetItemModelFromId(itemId);
                BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                // Check if it's consumable
                if (businessItem.type == Constants.ITEM_TYPE_CONSUMABLE)
                {
                    // Consume the item on the hand
                    ConsumeItem(player, item, businessItem, true);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.item_not_consumable);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_empty);
            }
        }

        [Command(Commands.COM_INVENTORY)]
        [RemoteEvent("OpenInventory")]
        public void InventoryCommand(Client player)
        {
            string active = null;
            if (player.GetData("Inventory") != null)
            {
                active = player.GetData("Inventory");
            }
            else
            {
                player.SetData("Inventory", "true");
            }

            if (active == "false")
            {
                List<InventoryModel> inventory = GetPlayerInventory(player);
                player.TriggerEvent("showPlayerInventory", NAPI.Util.ToJson(inventory), Constants.INVENTORY_TARGET_SELF);
                player.SetData("Inventory", "true");
            }
            else
            {
                CloseInventoryEvent(player);
                player.SetData("Inventory", "false");
            }
        }

        [RemoteEvent("closeInventory")]
        public void CloseInventoryEvent(Client player)
        {
            // Reset the variables related
            player.ResetData(EntityData.PLAYER_OPENED_TRUNK);
            player.ResetData(EntityData.PLAYER_SEARCHED_TARGET);

            player.SetData("Inventory", "false");

        }

        [Command(Commands.COM_PURCHASEBUSINESS)]
        public void PurchaseBusinessCommand(Client player, int amount = 0)
        {
            // Get all the businesses
            foreach (BusinessModel business in Business.businessList)
            {
                if (player.Position.DistanceTo(business.position) <= 1.5f && player.Dimension == business.dimension)
                {
                    Business.BuyBusiness(player, business);
                    return;
                }
            }
        }


        [Command(Commands.COM_PURCHASE)]
        public void PurchaseCommand(Client player, int amount = 0)
        {
            // Check if the player is inside a business
            if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
            {
                int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                BusinessModel business = Business.GetBusinessById(businessId);
                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                int playerSex = player.GetData(EntityData.PLAYER_SEX);

                switch (business.type)
                {
                    case Constants.BUSINESS_TYPE_CLOTHES:
                        // Temporary
                        if (!Customization.IsCustomCharacter(player))
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + "You need a custom character to get clothes");
                            return;
                        }

                        // Get the clothes on the character
                        List<ClothesModel> clothes = Customization.GetPlayerClothes(playerId);

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.about_complements);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.for_avoid_clipping1);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.for_avoid_clipping2);
                        player.TriggerEvent("showClothesBusinessPurchaseMenu", business.name, business.multiplier, Faction.IsPoliceMember(player));
                        break;
                    case Constants.BUSINESS_TYPE_BARBER_SHOP:
                        // Temporary
                        if (!Customization.IsCustomCharacter(player))
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + "You need a custom character to get a haircut.");
                            return;
                        }

                        // Load the players skin model
                        string skinModel = NAPI.Util.ToJson(player.GetData(EntityData.PLAYER_SKIN_MODEL));
                        player.TriggerEvent("showHairdresserMenu", playerSex, skinModel, business.name);
                        break;
                    case Constants.BUSINESS_TYPE_TATTOO_SHOP:
                        // Temporary
                        if (!Customization.IsCustomCharacter(player))
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + "You need a custom character to get a tattoo");
                            return;
                        }

                        // Remove player's clothes
                        player.SetClothes(11, 15, 0);
                        player.SetClothes(3, 15, 0);
                        player.SetClothes(8, 15, 0);

                        if (playerSex == 0)
                        {
                            player.SetClothes(4, 61, 0);
                            player.SetClothes(6, 34, 0);
                        }
                        else
                        {
                            player.SetClothes(4, 15, 0);
                            player.SetClothes(6, 35, 0);
                        }

                        // Load tattoo list
                        List<TattooModel> tattooList = Customization.tattooList.Where(t => t.player == playerId).ToList();
                        player.TriggerEvent("showTattooMenu", player.GetData(EntityData.PLAYER_SEX), NAPI.Util.ToJson(tattooList), NAPI.Util.ToJson(Constants.TATTOO_LIST), business.name, business.multiplier);

                        break;
                    default:
                        List<BusinessItemModel> businessItems = Business.GetBusinessSoldItems(business.type);

                        if (businessItems.Count > 0)
                        {
                            // Show the purchase menu
                            player.TriggerEvent("showBusinessPurchaseMenu", NAPI.Util.ToJson(businessItems), business.name, business.multiplier);
                        }

                        break;
                }
            }
            else
            {
                // Get all the houses
                foreach (HouseModel house in House.houseList)
                {
                    if (player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension)
                    {
                        House.BuyHouse(player, house);
                        return;
                    }
                }

                // Check if the player's in the scrapyard
                foreach (ParkingModel parking in Parking.parkingList)
                {
                    if (player.Position.DistanceTo(parking.position) < 2.5f && parking.type == Constants.PARKING_TYPE_SCRAPYARD)
                    {
                        if (amount > 0)
                        {
                            int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                            if (playerMoney >= amount)
                            {
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel item = GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                if (item == null)
                                {
                                    item = new ItemModel();
                                    {
                                        item.amount = amount;
                                        item.dimension = 0;
                                        item.position = new Vector3(0.0f, 0.0f, 0.0f);
                                        item.hash = Constants.ITEM_HASH_BUSINESS_PRODUCTS;
                                        item.ownerEntity = Constants.ITEM_ENTITY_PLAYER;
                                        item.ownerIdentifier = playerId;
                                        item.objectHandle = null;
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Add the item into the database
                                            item.id = Database.AddNewItem(item);
                                            itemList.Add(item);
                                        });
                                    });
                                }
                                else
                                {
                                    item.amount += amount;

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the amount into the database
                                            Database.UpdateItem(item);
                                        });
                                    });
                                }

                                player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - amount);

                                string message = string.Format(InfoRes.products_bought, amount, amount);
                                player.SendChatMessage(Constants.COLOR_INFO + message);
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_COMMAND_PURCHASE);
                        }
                        return;
                    }
                }
            }

        }

        [Command(Commands.COM_SELL, Commands.HLP_SELL_COMMAND, GreedyArg = true)]
        public void SellCommand(Client player, string args)
        {
            string[] arguments = args.Split(' ');
            int price = 0;
            int targetId = 0;
            int objectId = 0;
            Client target = null;
            string priceString = string.Empty;
            if (arguments.Length > 0)
            {
                switch (arguments[0].ToLower())
                {
                    case Commands.ARG_VEHICLE:
                        if (arguments.Length > 3)
                        {
                            if (int.TryParse(arguments[2], out targetId) == true)
                            {
                                target = GetPlayerById(targetId);
                                priceString = arguments[3];
                            }
                            else if (arguments.Length == 5)
                            {
                                target = NAPI.Player.GetPlayerFromName(arguments[2] + " " + arguments[3]);
                                priceString = arguments[4];
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_VEH_COMMAND);
                                return;
                            }

                            if (target == null || target == player)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.cant_sell_vehicle_self);
                                return;
                            }

                            if (int.TryParse(priceString, out price) == true)
                            {
                                if (price > 0)
                                {
                                    if (int.TryParse(arguments[1], out objectId) == true)
                                    {
                                        Vehicle vehicle = Vehicles.GetVehicleById(objectId);

                                        if (vehicle == null)
                                        {
                                            VehicleModel vehModel = Parking.GetParkedVehicleById(objectId);

                                            if (vehModel != null)
                                            {
                                                if (vehModel.owner == player.Name)
                                                {
                                                    string playerString = string.Format(InfoRes.vehicle_sell, vehModel.model, target.Name, price);
                                                    string targetString = string.Format(InfoRes.vehicle_sold, player.Name, vehModel.model, price);

                                                    target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                    target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                    target.SetData(EntityData.PLAYER_SELLING_HOUSE, objectId);

                                                    player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                    target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_veh_owner);
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.vehicle_not_exists);
                                            }
                                        }
                                        else
                                        {
                                            foreach (Vehicle veh in NAPI.Pools.GetAllVehicles())
                                            {
                                                if (veh.GetData(EntityData.VEHICLE_ID) == objectId)
                                                {
                                                    if (vehicle.GetData(EntityData.VEHICLE_OWNER) == player.Name)
                                                    {
                                                        string vehicleModel = vehicle.GetData(EntityData.VEHICLE_MODEL);
                                                        string playerString = string.Format(InfoRes.vehicle_sell, vehicleModel, target.Name, price);
                                                        string targetString = string.Format(InfoRes.vehicle_sold, player.Name, vehicleModel, price);

                                                        target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                        target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                        target.SetData(EntityData.PLAYER_SELLING_VEHICLE, objectId);

                                                        player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                        target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                    }
                                                    else
                                                    {
                                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_veh_owner);
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_VEH_COMMAND);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_VEH_COMMAND);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_VEH_COMMAND);
                        }
                        break;
                    case Commands.ARG_HOUSE:
                        if (arguments.Length < 2)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_SELL_HOUSE_COMMAND);
                        }
                        else
                        {
                            if (int.TryParse(arguments[1], out objectId) == true)
                            {
                                HouseModel house = House.GetHouseById(objectId);
                                if (house != null)
                                {
                                    if (house.owner == player.Name)
                                    {
                                        foreach (Client rndPlayer in NAPI.Pools.GetAllPlayers())
                                        {
                                            if (rndPlayer.GetData(EntityData.PLAYER_PLAYING) != null && rndPlayer.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                                            {
                                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_occupied);
                                                return;
                                            }
                                        }
                                        if (arguments.Length == 3)
                                        {
                                            int sellValue = (int)Math.Round(house.price * 0.7);
                                            string playerString = string.Format(InfoRes.house_sell_state, sellValue);
                                            player.SetData(EntityData.PLAYER_SELLING_HOUSE_STATE, objectId);

                                            int pHouses = player.GetData(EntityData.PLAYER_HOUSES) - 1;

                                            player.SetData(EntityData.PLAYER_HOUSES, pHouses);
                                            player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                        }
                                        else
                                        {
                                            if (int.TryParse(arguments[2], out targetId) == true)
                                            {
                                                target = GetPlayerById(targetId);
                                                priceString = arguments[3];
                                            }
                                            else if (arguments.Length == 5)
                                            {
                                                target = NAPI.Player.GetPlayerFromName(arguments[2] + " " + arguments[3]);
                                                priceString = arguments[4];
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_HOUSE_COMMAND);
                                                return;
                                            }

                                            if (int.TryParse(priceString, out price) == true)
                                            {
                                                if (price > 0)
                                                {
                                                    string playerString = string.Format(InfoRes.house_sell, target.Name, price);
                                                    string targetString = string.Format(InfoRes.house_sold, player.Name, price);

                                                    target.SetData(EntityData.PLAYER_JOB_PARTNER, player);
                                                    target.SetData(EntityData.PLAYER_SELLING_PRICE, price);
                                                    target.SetData(EntityData.PLAYER_SELLING_HOUSE, objectId);

                                                    player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                                    target.SendChatMessage(Constants.COLOR_INFO + targetString);
                                                }
                                                else
                                                {
                                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
                                                }
                                            }
                                            else
                                            {
                                                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_VEH_COMMAND);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_house_owner);
                                    }
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_exists);
                                }

                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_SELL_HOUSE_COMMAND);
                            }
                        }
                        break;
                    case Commands.ARG_WEAPON:
                        // Pending TODO
                        break;
                    case Commands.ARG_FISH:
                        if (player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) > 0)
                        {
                            int businessId = player.GetData(EntityData.PLAYER_BUSINESS_ENTERED);
                            BusinessModel business = Business.GetBusinessById(businessId);

                            if (business != null && business.type == Constants.BUSINESS_TYPE_FISHING)
                            {
                                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                                ItemModel fishModel = GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_FISH);

                                if (fishModel == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_fish_sellable);
                                }
                                else
                                {
                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                                    int amount = (int)Math.Round(fishModel.amount * Constants.PRICE_FISH / 1000.0d);

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Remove the item from the database
                                            Database.RemoveItem(fishModel.id);
                                            itemList.Remove(fishModel);
                                        });
                                    });

                                    player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney + amount);

                                    string message = string.Format(InfoRes.fishing_won, amount);
                                    player.SendChatMessage(Constants.COLOR_INFO + message);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_fishing_business);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_fishing_business);
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_SELL_COMMAND);
                        break;
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_SELL_COMMAND);
            }
        }

        [Command(Commands.COM_HELP)]
        public void HelpCommand(Client player)
        {
            player.SendChatMessage(Constants.COLOR_ERROR + "For help Go to #help in discord : https://discord.gg/CM43yxX");
            //player.TriggerEvent("helptext");
        }

        [Command(Commands.COM_WELCOME)]
        public void WelcomeCommand(Client player)
        {
            player.SendChatMessage(Constants.COLOR_ERROR + "~w~Welcome to ~b~Oceanic~w~ Roleplay! To get started or if you require help, join our Discord : https://discord.gg/CM43yxX");
            //player.TriggerEvent("welcomeHelp");
        }

        [Command(Commands.COM_SHOW, Commands.HLP_SHOW_DOC_COMMAND)]
        public void ShowCommand(Client player, string targetString, string documentation)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                string message;
                int currentLicense = 0;
                string nameChar = player.GetData(EntityData.PLAYER_NAME);
                int age = player.GetData(EntityData.PLAYER_AGE);
                string sexDescription = player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_MALE ? GenRes.sex_male : GenRes.sex_female;

                Client target = int.TryParse(targetString, out int targetId) ? GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

                if (target == null || player.Position.DistanceTo(target.Position) > 2.5f)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                    return;
                }

                switch (documentation.ToLower())
                {
                    case Commands.ARG_LICENSES:
                        string licenseMessage;
                        string playerLicenses = player.GetData(EntityData.PLAYER_LICENSES);
                        string[] playerLicensesArray = playerLicenses.Split(',');

                        message = string.Format(InfoRes.licenses_show, target.Name);
                        Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                        foreach (string license in playerLicensesArray)
                        {
                            int currentLicenseStatus = int.Parse(license);
                            switch (currentLicense)
                            {
                                case Constants.LICENSE_CAR:
                                    switch (currentLicenseStatus)
                                    {
                                        case -1:
                                            target.SendChatMessage(Constants.COLOR_HELP + InfoRes.car_license_not_available);
                                            break;
                                        case 0:
                                            target.SendChatMessage(Constants.COLOR_HELP + InfoRes.car_license_practical_pending);
                                            break;
                                        default:
                                            licenseMessage = string.Format(InfoRes.car_license_points, currentLicenseStatus);
                                            target.SendChatMessage(Constants.COLOR_HELP + licenseMessage);
                                            break;
                                    }
                                    break;
                                case Constants.LICENSE_MOTORCYCLE:
                                    switch (currentLicenseStatus)
                                    {
                                        case -1:
                                            target.SendChatMessage(Constants.COLOR_HELP + InfoRes.motorcycle_license_not_available);
                                            break;
                                        case 0:
                                            target.SendChatMessage(Constants.COLOR_HELP + InfoRes.motorcycle_license_practical_pending);
                                            break;
                                        default:
                                            licenseMessage = string.Format(InfoRes.motorcycle_license_points, currentLicenseStatus);
                                            target.SendChatMessage(Constants.COLOR_HELP + licenseMessage);
                                            break;
                                    }
                                    break;
                                case Constants.LICENSE_TAXI:
                                    if (currentLicenseStatus == -1)
                                    {
                                        target.SendChatMessage(Constants.COLOR_HELP + InfoRes.taxi_license_not_available);
                                    }
                                    else
                                    {
                                        target.SendChatMessage(Constants.COLOR_HELP + InfoRes.taxi_license_up_to_date);
                                    }
                                    break;
                            }
                            currentLicense++;
                        }
                        break;
                    case Commands.ARG_INSURANCE:
                        int playerMedicalInsurance = player.GetData(EntityData.PLAYER_MEDICAL_INSURANCE);
                        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                        dateTime = dateTime.AddSeconds(playerMedicalInsurance);

                        if (playerMedicalInsurance > 0)
                        {
                            message = string.Format(InfoRes.insurance_show, target.Name);
                            Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.name + nameChar);
                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.age + age);
                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.sex + sexDescription);
                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.expiry + dateTime.ToShortDateString());
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_medical_insurance);
                        }

                        break;
                    case Commands.ARG_IDENTIFICATION:
                        int playerDocumentation = player.GetData(EntityData.PLAYER_DOCUMENTATION);
                        if (playerDocumentation > 0)
                        {
                            message = string.Format(InfoRes.identification_show, target.Name);
                            Chat.SendMessageToNearbyPlayers(player, message, Constants.MESSAGE_ME, 20.0f);

                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.name + nameChar);
                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.age + age);
                            target.SendChatMessage(Constants.COLOR_INFO + GenRes.sex + sexDescription);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_undocumented);
                        }
                        break;
                }
            }
        }

        [Command(Commands.COM_PAY, Commands.HLP_PAY_COMMAND, GreedyArg = true)]
        public void PayCommand(Client player, string arguments)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
                return;
            }

            // Get the message splitted
            string[] args = arguments.Split(' ');

            if (args.Length < 2 || args.Length > 3)
            {
                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PAY_COMMAND);
                return;
            }

            // Initialize the target player
            Client target = null;

            if (int.TryParse(args[0], out int playerId))
            {
                // The player sent the player's identifier
                target = GetPlayerById(playerId);

                // Remove the id from the parameters
                args = args.Skip(1).ToArray();
            }
            else if (args.Length == 3)
            {
                // The player sent the name and surname
                target = NAPI.Player.GetPlayerFromName(args[0] + " " + args[1]);

                // Remove the name and surname from the parameters
                args = args.Skip(2).ToArray();
            }

            if (target == null || player.Position.DistanceTo(target.Position) > 2.5f)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                return;
            }

            if (target == player)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.hooker_offered_himself);
                return;
            }

            if (int.TryParse(args[0], out int amount))
            {
                if (amount < 0)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
                    return;
                }

                // Get the player's money
                int money = player.GetSharedData(EntityData.PLAYER_MONEY);

                if (money < amount)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                    return;
                }

                // Send the payment
                target.SetData(EntityData.PLAYER_PAYMENT, player);
                target.SetData(EntityData.JOB_OFFER_PRICE, amount);

                player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.payment_offer, amount, target.Name));
                target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.payment_received, player.Name, amount));
            }
            else
            {
                // The amount is not numeric
                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_PAY_COMMAND);
            }
        }

        [Command(Commands.COM_GIVE, Commands.HLP_GIVE_COMMAND)]
        public void GiveCommand(Client player, string targetString)
        {
            if (!Inventory.HasPlayerItemOnHand(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_empty);
                return;
            }

            Client target = int.TryParse(targetString, out int targetId) ? GetPlayerById(targetId) : NAPI.Player.GetPlayerFromName(targetString);

            if (target == null || player.Position.DistanceTo(target.Position) > 2.0f)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
            }
            else if (Inventory.HasPlayerItemOnHand(target))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.target_right_hand_not_empty);
            }
            else
            {
                ItemModel item = null;
                string playerMessage = string.Empty;
                string targetMessage = string.Empty;

                if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
                {
                    string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                    item = GetItemModelFromId(NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId);

                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);
                    AttachItemToPlayer(target, item.id, item.hash, "IK_R_Hand", businessItem.position, businessItem.rotation, EntityData.PLAYER_RIGHT_HAND);

                    player.ResetSharedData(EntityData.PLAYER_RIGHT_HAND);

                    playerMessage = string.Format(InfoRes.item_given, businessItem.description.ToLower(), target.Name);
                    targetMessage = string.Format(InfoRes.item_received, player.Name, businessItem.description.ToLower());
                }
                else
                {
                    // Get the player weapon on the hand
                    WeaponHash weaponHash = player.CurrentWeapon;
                    item = Weapons.GetWeaponItem(player, weaponHash);

                    target.GiveWeapon(weaponHash, 0);
                    target.SetWeaponAmmo(weaponHash, item.amount);

                    // Unequip the weapon from the player
                    player.RemoveWeapon(weaponHash);
                    player.GiveWeapon(WeaponHash.Unarmed, 0);

                    playerMessage = string.Format(InfoRes.item_given, item.hash.ToLower(), target.Name);
                    targetMessage = string.Format(InfoRes.item_received, player.Name, item.hash.ToLower());
                }

                // Change item's owner
                item.ownerIdentifier = target.GetData(EntityData.PLAYER_SQL_ID);

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // Update the amount into the database
                        Database.UpdateItem(item);

                        player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                        target.SendChatMessage(Constants.COLOR_INFO + targetMessage);
                    });
                });
            }
        }

        [Command(Commands.COM_CANCEL, Commands.HLP_GLOBALS_CANCEL_COMMAND)]
        public void CancelCommand(Client player, string cancel)
        {
            switch (cancel.ToLower())
            {
                case Commands.ARG_INTERVIEW:
                    if (player.GetData(EntityData.PLAYER_ON_AIR) != null)
                    {
                        player.ResetData(EntityData.PLAYER_ON_AIR);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.on_air_canceled);
                    }
                    break;
                case Commands.ARG_SERVICE:
                    if (player.GetData(EntityData.PLAYER_ALREADY_FUCKING) == null)
                    {
                        player.ResetData(EntityData.PLAYER_ALREADY_FUCKING);
                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.ResetData(EntityData.HOOKER_TYPE_SERVICE);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.hooker_service_canceled);
                    }
                    break;
                case Commands.ARG_MONEY:
                    if (player.GetData(EntityData.PLAYER_PAYMENT) != null)
                    {
                        player.ResetData(EntityData.PLAYER_PAYMENT);
                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.payment_canceled);
                    }
                    break;
                case Commands.ARG_ORDER:
                    if (player.GetData(EntityData.PLAYER_DELIVER_ORDER) != null)
                    {
                        player.ResetData(EntityData.PLAYER_DELIVER_ORDER);
                        player.ResetData(EntityData.PLAYER_JOB_CHECKPOINT);
                        player.ResetData(EntityData.PLAYER_JOB_VEHICLE);
                        player.ResetData(EntityData.PLAYER_JOB_WON);

                        // Remove the checkpoints
                        player.TriggerEvent("fastFoodDeliverFinished");

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.deliverer_order_canceled);
                    }
                    break;
                case Commands.ARG_REPAINT:
                    if (player.GetData(EntityData.PLAYER_REPAINT_VEHICLE) != null)
                    {
                        // Get the mechanic and the vehicle
                        Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                        Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAINT_VEHICLE);

                        // Get old colors
                        int vehicleColorType = vehicle.GetData(EntityData.VEHICLE_COLOR_TYPE);
                        string primaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_FIRST_COLOR);
                        string secondaryVehicleColor = vehicle.GetData(EntityData.VEHICLE_SECOND_COLOR);
                        int vehiclePearlescentColor = vehicle.GetData(EntityData.VEHICLE_PEARLESCENT_COLOR);

                        if (vehicleColorType == Constants.VEHICLE_COLOR_TYPE_PREDEFINED)
                        {
                            vehicle.PrimaryColor = int.Parse(primaryVehicleColor);
                            vehicle.SecondaryColor = int.Parse(secondaryVehicleColor);
                            vehicle.PearlescentColor = vehiclePearlescentColor;
                        }
                        else
                        {
                            string[] primaryColor = primaryVehicleColor.Split(',');
                            string[] secondaryColor = secondaryVehicleColor.Split(',');
                            vehicle.CustomPrimaryColor = new Color(int.Parse(primaryColor[0]), int.Parse(primaryColor[1]), int.Parse(primaryColor[2]));
                            vehicle.CustomSecondaryColor = new Color(int.Parse(secondaryColor[0]), int.Parse(secondaryColor[1]), int.Parse(secondaryColor[2]));
                        }

                        player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                        player.ResetData(EntityData.PLAYER_REPAINT_VEHICLE);
                        player.ResetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                        player.ResetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                        player.ResetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                        player.ResetData(EntityData.JOB_OFFER_PRICE);

                        // Remove repaint window
                        target.TriggerEvent("closeRepaintWindow");

                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.repaint_canceled);
                    }
                    break;
                default:
                    player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_GLOBALS_CANCEL_COMMAND);
                    break;
            }
        }

        [Command(Commands.COM_ACCEPT, Commands.HLP_GLOBALS_ACCEPT_COMMAND)]
        public void AcceptCommand(Client player, string accept)
        {
            if (player.GetSharedData(EntityData.PLAYER_KILLED) != 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_is_dead);
            }
            else
            {
                switch (accept.ToLower())
                {
                    case Commands.ARG_REPAIR:
                        if (player.GetData(EntityData.PLAYER_REPAIR_VEHICLE) != null)
                        {
                            Client mechanic = player.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (mechanic != null && mechanic.Position.DistanceTo(player.Position) < 5.0f)
                            {
                                int price = player.GetData(EntityData.JOB_OFFER_PRICE);
                                int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                                if (playerMoney >= price)
                                {
                                    // Get the vehicle to repair and the broken part
                                    string type = player.GetData(EntityData.PLAYER_REPAIR_TYPE);
                                    Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAIR_VEHICLE);

                                    int mechanicId = mechanic.GetData(EntityData.PLAYER_SQL_ID);
                                    int mechanicMoney = mechanic.GetSharedData(EntityData.PLAYER_MONEY);
                                    ItemModel item = GetPlayerItemModelFromHash(mechanicId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                    switch (type.ToLower())
                                    {
                                        case Commands.ARG_CHASSIS:
                                            vehicle.Repair();
                                            break;
                                        case Commands.ARG_DOORS:
                                            for (int i = 0; i < 6; i++)
                                            {
                                                if (vehicle.IsDoorBroken(i) == true)
                                                {
                                                    vehicle.FixDoor(i);
                                                }
                                            }
                                            break;
                                        case Commands.ARG_TYRES:
                                            for (int i = 0; i < 4; i++)
                                            {
                                                if (vehicle.IsTyrePopped(i) == true)
                                                {
                                                    vehicle.FixTyre(i);
                                                }
                                            }
                                            break;
                                        case Commands.ARG_WINDOWS:
                                            for (int i = 0; i < 4; i++)
                                            {
                                                if (vehicle.IsWindowBroken(i) == true)
                                                {
                                                    vehicle.FixWindow(i);
                                                }
                                            }
                                            break;
                                    }

                                    if (player != mechanic)
                                    {
                                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - price);
                                        mechanic.SetSharedData(EntityData.PLAYER_MONEY, mechanicMoney + price);
                                    }

                                    item.amount -= player.GetData(EntityData.JOB_OFFER_PRODUCTS);

                                    if (item.amount == 0)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Remove the item from the database
                                                Database.RemoveItem(item.id);
                                                itemList.Remove(item);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the amount into the database
                                                Database.UpdateItem(item);
                                            });
                                        });
                                    }

                                    player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                                    player.ResetData(EntityData.PLAYER_REPAIR_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_REPAIR_TYPE);
                                    player.ResetData(EntityData.JOB_OFFER_PRODUCTS);
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);

                                    string playerMessage = string.Format(InfoRes.vehicle_repaired_by, mechanic.Name, price);
                                    string mechanicMessage = string.Format(InfoRes.vehicle_repaired_by, player.Name, price);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    mechanic.SendChatMessage(Constants.COLOR_INFO + mechanicMessage);

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Save the log into the database
                                            Database.LogPayment(player.Name, mechanic.Name, Commands.COM_REPAIR, price);
                                        });
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_repair_offered);

                        }
                        break;
                    case Commands.ARG_REPAINT:
                        if (player.GetData(EntityData.PLAYER_REPAINT_VEHICLE) != null)
                        {
                            Client mechanic = player.GetData(EntityData.PLAYER_JOB_PARTNER);

                            if (mechanic != null && mechanic.Position.DistanceTo(player.Position) < 5.0f)
                            {
                                int price = player.GetData(EntityData.JOB_OFFER_PRICE);
                                int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

                                if (playerMoney >= price)
                                {
                                    Vehicle vehicle = player.GetData(EntityData.PLAYER_REPAINT_VEHICLE);
                                    int colorType = player.GetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                                    string firstColor = player.GetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                                    string secondColor = player.GetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                                    int pearlescentColor = player.GetData(EntityData.PLAYER_REPAINT_PEARLESCENT);

                                    int mechanicId = mechanic.GetData(EntityData.PLAYER_SQL_ID);
                                    int mechanicMoney = mechanic.GetSharedData(EntityData.PLAYER_MONEY);
                                    ItemModel item = GetPlayerItemModelFromHash(mechanicId, Constants.ITEM_HASH_BUSINESS_PRODUCTS);

                                    // Repaint the vehicle
                                    vehicle.SetData(EntityData.VEHICLE_COLOR_TYPE, colorType);
                                    vehicle.SetData(EntityData.VEHICLE_FIRST_COLOR, firstColor);
                                    vehicle.SetData(EntityData.VEHICLE_SECOND_COLOR, secondColor);
                                    vehicle.SetData(EntityData.VEHICLE_PEARLESCENT_COLOR, pearlescentColor);

                                    // Update the vehicle's color
                                    VehicleModel vehicleModel = new VehicleModel();
                                    {
                                        vehicleModel.id = vehicle.GetData(EntityData.VEHICLE_ID);
                                        vehicleModel.colorType = colorType;
                                        vehicleModel.firstColor = firstColor;
                                        vehicleModel.secondColor = secondColor;
                                        vehicleModel.pearlescent = pearlescentColor;
                                    }

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the vehicle's color into the database
                                            Database.UpdateVehicleColor(vehicleModel);
                                        });
                                    });

                                    if (player != mechanic)
                                    {
                                        player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - price);
                                        mechanic.SetSharedData(EntityData.PLAYER_MONEY, mechanicMoney + price);
                                    }

                                    item.amount -= player.GetData(EntityData.JOB_OFFER_PRODUCTS);

                                    if (item.amount == 0)
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Remove the item from the database
                                                Database.RemoveItem(item.id);
                                                itemList.Remove(item);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the amount into the database
                                                Database.UpdateItem(item);
                                            });
                                        });
                                    }

                                    player.ResetData(EntityData.PLAYER_JOB_PARTNER);
                                    player.ResetData(EntityData.PLAYER_REPAINT_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_REPAINT_COLOR_TYPE);
                                    player.ResetData(EntityData.PLAYER_REPAINT_FIRST_COLOR);
                                    player.ResetData(EntityData.PLAYER_REPAINT_SECOND_COLOR);
                                    player.ResetData(EntityData.JOB_OFFER_PRODUCTS);
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);

                                    string playerMessage = string.Format(InfoRes.vehicle_repainted_by, mechanic.Name, price);
                                    string mechanicMessage = string.Format(InfoRes.vehicle_repainted_to, player.Name, price);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    mechanic.SendChatMessage(Constants.COLOR_INFO + mechanicMessage);

                                    // Remove repaint menu
                                    mechanic.TriggerEvent("closeRepaintWindow");

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Save the log into the database
                                            Database.LogPayment(player.Name, mechanic.Name, Commands.COM_REPAINT, price);
                                        });
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_too_far);
                            }
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_repaint_offered);
                        }

                        break;
                    case Commands.ARG_SERVICE:

                        if (player.GetData(EntityData.HOOKER_TYPE_SERVICE) == null)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_service_offered);
                        }
                        else if (player.GetData(EntityData.PLAYER_ALREADY_FUCKING) != null)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_fucking);
                        }
                        else if (player.VehicleSeat != (int)VehicleSeat.Driver)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_vehicle_driving);
                        }
                        else
                        {
                            if (player.Vehicle.EngineStatus)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.engine_on);
                            }
                            else
                            {
                                Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                                if (player.GetData(EntityData.HOOKER_TYPE_SERVICE) != null)
                                {
                                    int amount = player.GetData(EntityData.JOB_OFFER_PRICE);
                                    int money = player.GetSharedData(EntityData.PLAYER_MONEY);

                                    if (target.GetData(EntityData.PLAYER_PLAYING) != null)
                                    {
                                        if (amount > money)
                                        {
                                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                                        }
                                        else
                                        {
                                            int targetMoney = target.GetSharedData(EntityData.PLAYER_MONEY);
                                            player.SetSharedData(EntityData.PLAYER_MONEY, money - amount);
                                            target.SetSharedData(EntityData.PLAYER_MONEY, targetMoney + amount);

                                            string playerMessage = string.Format(InfoRes.service_paid, amount);
                                            string targetMessage = string.Format(InfoRes.service_received, amount);
                                            player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                            target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                            player.SetData(EntityData.PLAYER_ANIMATION, target);
                                            player.SetData(EntityData.PLAYER_ALREADY_FUCKING, target);
                                            target.SetData(EntityData.PLAYER_ALREADY_FUCKING, player);

                                            // Reset the entity data
                                            player.ResetData(EntityData.JOB_OFFER_PRICE);
                                            player.ResetData(EntityData.PLAYER_JOB_PARTNER);

                                            // Check the type of the service
                                            if (player.GetData(EntityData.HOOKER_TYPE_SERVICE) == Constants.HOOKER_SERVICE_BASIC)
                                            {
                                                player.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_bj_loop_player", (int)Constants.AnimationFlags.Loop);
                                                target.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_bj_loop_female", (int)Constants.AnimationFlags.Loop);

                                                // Timer to finish the service
                                                Timer sexTimer = new Timer(Hooker.OnSexServiceTimer, player, 120000, Timeout.Infinite);
                                                Hooker.sexTimerList.Add(player.Value, sexTimer);
                                            }
                                            else
                                            {
                                                player.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_sex_loop_player", (int)Constants.AnimationFlags.Loop);
                                                target.PlayAnimation("mini@prostitutes@sexlow_veh", "low_car_sex_loop_female", (int)Constants.AnimationFlags.Loop);

                                                // Timer to finish the service
                                                Timer sexTimer = new Timer(Hooker.OnSexServiceTimer, player, 180000, Timeout.Infinite);
                                                Hooker.sexTimerList.Add(player.Value, sexTimer);
                                            }

                                            Task.Factory.StartNew(() =>
                                            {
                                                NAPI.Task.Run(() =>
                                                {
                                                    // Save the log into the database
                                                    Database.LogPayment(player.Name, target.Name, GenRes.hooker, amount);
                                                });
                                            });
                                        }
                                    }
                                    else
                                    {
                                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                                    }
                                }
                            }
                        }
                        break;
                    /* case Commands.ARG_LAWYER:

                         if (player.GetData(EntityData.LAWYER_TYPE_SERVICE) == null)
                         {
                             player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_lawyer_offered);
                         }
                         else if (player.GetData(EntityData.PLAYER_LAWYERING) != null)
                         {
                             player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.already_lawyering);
                         }
                             else
                             {
                                 Client target = player.GetData(EntityData.PLAYER_JOB_HELPER);
                                 if (player.GetData(EntityData.LAWYER_TYPE_SERVICE) != null)
                                 {
                                     int amount = player.GetData(EntityData.JOB_OFFER_PRICE);
                                     int money = player.GetSharedData(EntityData.PLAYER_MONEY);

                                     if (target.GetData(EntityData.PLAYER_PLAYING) != null)
                                     {
                                         if (amount > money)
                                         {
                                             player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                                         }
                                         else
                                         {
                                             int targetMoney = target.GetSharedData(EntityData.PLAYER_MONEY);
                                             player.SetSharedData(EntityData.PLAYER_MONEY, money - amount);
                                             target.SetSharedData(EntityData.PLAYER_MONEY, targetMoney + amount);

                                             string playerMessage = string.Format(InfoRes.law_service_paid, amount);
                                             string targetMessage = string.Format(InfoRes.law_service_received, amount);
                                             player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                             target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                             player.SetData(EntityData.PLAYER_ANIMATION, target);
                                             player.SetData(EntityData.PLAYER_LAWYERING, target);
                                             target.SetData(EntityData.PLAYER_LAWYERING, player);

                                             // Reset the entity data
                                             player.ResetData(EntityData.JOB_OFFER_PRICE);
                                             player.ResetData(EntityData.PLAYER_JOB_HELPER);

                                             // Check the type of the service
                                             if (player.GetData(EntityData.LAWYER_TYPE_SERVICE) == Constants.LAWYER_SERVICE_BASIC)
                                             {
                                                 player.PlayAnimation("anim@mp_player_intcelebrationpaired@f_f_manly_handshake", "anim@mp_player_intcelebrationpaired@f_f_manly_handshake", (int)Constants.AnimationFlags.Loop);
                                                 target.PlayAnimation("anim@mp_player_intcelebrationpaired@f_f_manly_handshake", "anim@mp_player_intcelebrationpaired@f_f_manly_handshake", (int)Constants.AnimationFlags.Loop);

                                                 // Timer to finish the service
                                                 Timer lawyerTimer = new Timer(Lawyer.OnLawServiceTimer, player, 120, Timeout.Infinite);
                                                 Lawyer.JailtalkTimerList.Add(player.Value, lawyerTimer);
                                             }
                                             else
                                             {
                                                 player.PlayAnimation("anim@mp_player_intcelebrationpaired@f_f_manly_handshake", "anim@mp_player_intcelebrationpaired@f_f_manly_handshake", (int)Constants.AnimationFlags.Loop);
                                                 target.PlayAnimation("anim@mp_player_intcelebrationpaired@f_f_manly_handshake", "anim@mp_player_intcelebrationpaired@f_f_manly_handshake", (int)Constants.AnimationFlags.Loop);

                                                 // Timer to finish the service
                                                 Timer lawyerTimer = new Timer(Lawyer.OnLawServiceTimer, player, 180, Timeout.Infinite);
                                                 Lawyer.JailtalkTimerList.Add(player.Value, lawyerTimer);
                                             }

                                             Task.Factory.StartNew(() =>
                                             {
                                                 NAPI.Task.Run(() =>
                                                 {
                                                     // Save the log into the database
                                                     Database.LogPayment(player.Name, target.Name, GenRes.lawyer, amount);
                                                 });
                                             });
                                         }
                                     }
                                     else
                                     {
                                         player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                                     }
                                 }
                             }

                         break;*/
                    case Commands.ARG_INTERVIEW:
                        if (!player.IsInVehicle)
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_vehicle);
                        }
                        else
                        {
                            NetHandle vehicle = player.Vehicle;
                            if (player.VehicleSeat != (int)VehicleSeat.RightRear)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_right_rear);
                            }
                            else
                            {
                                Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                                player.SetData(EntityData.PLAYER_ON_AIR, true);
                                player.SendChatMessage(Constants.COLOR_INFO + InfoRes.already_on_air);
                                target.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.interview_accepted);
                            }
                        }
                        break;
                    case Commands.ARG_MONEY:
                        if (player.GetData(EntityData.PLAYER_PAYMENT) != null)
                        {
                            Client target = player.GetData(EntityData.PLAYER_PAYMENT);
                            int amount = player.GetData(EntityData.JOB_OFFER_PRICE);

                            if (target.GetData(EntityData.PLAYER_PLAYING) != null)
                            {
                                int money = target.GetSharedData(EntityData.PLAYER_MONEY);

                                if (amount > 0 && money >= amount)
                                {
                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);
                                    player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney + amount);
                                    target.SetSharedData(EntityData.PLAYER_MONEY, money - amount);

                                    // Reset the entity data
                                    player.ResetData(EntityData.JOB_OFFER_PRICE);
                                    player.ResetData(EntityData.PLAYER_PAYMENT);

                                    // Send the messages to both players
                                    string playerMessage = string.Format(InfoRes.player_paid, target.Name, amount);
                                    string targetMessage = string.Format(InfoRes.target_paid, amount, player.Name);
                                    player.SendChatMessage(Constants.COLOR_INFO + playerMessage);
                                    target.SendChatMessage(Constants.COLOR_INFO + targetMessage);

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Save the logs into database
                                            Database.LogPayment(target.Name, player.Name, GenRes.payment_players, amount);
                                        });
                                    });
                                }
                                else
                                {
                                    target.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_enough_money);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                            }
                        }
                        break;
                    case Commands.ARG_VEHICLE:
                        if (player.GetData(EntityData.PLAYER_SELLING_VEHICLE) != null)
                        {
                            Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int amount = player.GetData(EntityData.PLAYER_SELLING_PRICE);
                            int vehicleId = player.GetData(EntityData.PLAYER_SELLING_VEHICLE);

                            if (target.GetData(EntityData.PLAYER_PLAYING) != null)
                            {
                                int money = player.GetSharedData(EntityData.PLAYER_BANK);

                                if (money >= amount)
                                {
                                    string vehicleModel = string.Empty;
                                    Vehicle vehicle = Vehicles.GetVehicleById(vehicleId);

                                    if (vehicle == null)
                                    {
                                        VehicleModel vehModel = Parking.GetParkedVehicleById(vehicleId);
                                        vehModel.owner = player.Name;
                                        vehicleModel = NAPI.Vehicle.GetVehicleDisplayName((VehicleHash)vehModel.model);
                                    }
                                    else
                                    {
                                        vehicle.SetData(EntityData.VEHICLE_OWNER, player.Name);
                                        vehicleModel = vehicle.DisplayName;
                                    }

                                    int targetMoney = target.GetSharedData(EntityData.PLAYER_BANK);
                                    player.SetSharedData(EntityData.PLAYER_BANK, money - amount);
                                    target.SetSharedData(EntityData.PLAYER_BANK, targetMoney + amount);

                                    player.ResetData(EntityData.PLAYER_SELLING_VEHICLE);
                                    player.ResetData(EntityData.PLAYER_SELLING_PRICE);

                                    player.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.vehicle_buy, target.Name, vehicleModel, amount));
                                    target.SendChatMessage(Constants.COLOR_INFO + string.Format(InfoRes.vehicle_bought, player.Name, vehicleModel, amount));

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Save the logs into database
                                            Database.LogPayment(target.Name, player.Name, GenRes.vehicle_sale, amount);
                                        });
                                    });
                                }
                                else
                                {
                                    // Send the error message to the player
                                    player.SendChatMessage(Constants.COLOR_ERROR + string.Format(ErrRes.carshop_no_money, amount));
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_found);
                            }
                        }
                        break;
                    case Commands.ARG_HOUSE:
                        if (player.GetData(EntityData.PLAYER_SELLING_HOUSE) != null)
                        {
                            Client target = player.GetData(EntityData.PLAYER_JOB_PARTNER);
                            int amount = player.GetData(EntityData.PLAYER_SELLING_PRICE);
                            int houseId = player.GetData(EntityData.PLAYER_SELLING_HOUSE);

                            if (target.GetData(EntityData.PLAYER_PLAYING) != null)
                            {
                                int money = player.GetSharedData(EntityData.PLAYER_BANK);

                                if (money >= amount)
                                {
                                    HouseModel house = House.GetHouseById(houseId);

                                    if (house.owner == target.Name)
                                    {
                                        house.owner = player.Name;
                                        house.tenants = 2;

                                        int targetMoney = target.GetSharedData(EntityData.PLAYER_BANK);
                                        player.SetSharedData(EntityData.PLAYER_BANK, money - amount);
                                        target.SetSharedData(EntityData.PLAYER_BANK, targetMoney + amount);

                                        player.ResetData(EntityData.PLAYER_SELLING_HOUSE);
                                        player.ResetData(EntityData.PLAYER_SELLING_PRICE);

                                        string playerString = string.Format(InfoRes.house_buyto, target.Name, amount);
                                        string targetString = string.Format(InfoRes.house_bought, player.Name, amount);
                                        player.SendChatMessage(Constants.COLOR_INFO + playerString);
                                        target.SendChatMessage(Constants.COLOR_INFO + targetString);

                                        Task.Factory.StartNew(() =>
                                        {
                                            NAPI.Task.Run(() =>
                                            {
                                                // Update the house
                                                Database.KickTenantsOut(house.id);
                                                Database.UpdateHouse(house);

                                                // Log the payment into database
                                                Database.LogPayment(target.Name, player.Name, GenRes.house_sale, amount);
                                            });
                                        });
                                    }
                                    else
                                    {
                                        player.SendChatMessage(ErrRes.house_sell_generic);
                                        target.SendChatMessage(ErrRes.house_sell_generic);
                                    }
                                }
                                else
                                {
                                    string message = string.Format(ErrRes.house_no_money, amount);
                                    player.SendChatMessage(Constants.COLOR_ERROR + message);
                                }
                            }
                        }
                        break;
                    case Commands.ARG_STATE_HOUSE:
                        if (player.GetData(EntityData.PLAYER_SELLING_HOUSE_STATE) != null)
                        {
                            HouseModel house = House.GetHouseById(player.GetData(EntityData.PLAYER_SELLING_HOUSE_STATE));
                            int amount = (int)Math.Round(house.price * Constants.HOUSE_SALE_STATE);

                            if (player.GetData(EntityData.PLAYER_PLAYING) != null)
                            {
                                if (house.owner == player.Name)
                                {
                                    house.locked = true;
                                    house.owner = string.Empty;
                                    house.status = Constants.HOUSE_STATE_BUYABLE;
                                    house.houseLabel.Text = House.GetHouseLabelText(house);
                                    NAPI.World.RemoveIpl(house.ipl);
                                    house.tenants = 2;

                                    int playerMoney = player.GetSharedData(EntityData.PLAYER_BANK);
                                    player.SetSharedData(EntityData.PLAYER_BANK, playerMoney + amount);

                                    player.SendChatMessage(Constants.COLOR_SUCCESS + string.Format(SuccRes.house_sold, amount));

                                    Task.Factory.StartNew(() =>
                                    {
                                        NAPI.Task.Run(() =>
                                        {
                                            // Update the house
                                            Database.KickTenantsOut(house.id);
                                            Database.UpdateHouse(house);

                                            // Log the payment into the database
                                            Database.LogPayment(player.Name, GenRes.state, GenRes.house_sale, amount);
                                        });
                                    });
                                }
                                else
                                {
                                    player.SendChatMessage(ErrRes.house_sell_generic);
                                }
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_ERROR + Commands.HLP_GLOBALS_ACCEPT_COMMAND);
                        break;
                }
            }
        }

        [Command(Commands.COM_PICK_UP)]
        public void PickUpCommand(Client player)
        {
            if (Inventory.HasPlayerItemOnHand(player))
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_occupied);
            }
            else if (player.GetSharedData(EntityData.PLAYER_WEAPON_CRATE) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.both_hand_occupied);
            }
            else
            {
                ItemModel item = GetClosestItem(player);
                if (item != null)
                {
                    // Get the item on the ground
                    ItemModel playerItem = GetPlayerItemModelFromHash(player.Value, item.hash);

                    if (playerItem != null)
                    {
                        // Add the amount to the player item
                        playerItem.amount += item.amount;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                Database.RemoveItem(item.id);
                                itemList.Remove(item);
                            });
                        });
                    }
                    else
                    {
                        playerItem = item;
                    }

                    // Delete the item on the ground
                    item.objectHandle.Delete();

                    // Play the animation
                    player.PlayAnimation("random@domestic", "pickup_low", 0);

                    // Add the item to the player
                    BusinessItemModel businessItem = Business.GetBusinessItemFromHash(playerItem.hash);
                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(playerItem.hash);

                    if (weaponHash != 0)
                    {
                        // Give the weapon to the player
                        player.GiveWeapon(weaponHash, 0);
                        player.SetWeaponAmmo(weaponHash, playerItem.amount);

                        // Change the owner entity
                        playerItem.ownerEntity = Constants.ITEM_ENTITY_WHEEL;
                    }
                    else
                    {
                        // Change the owner entity
                        playerItem.ownerEntity = Constants.ITEM_ENTITY_RIGHT_HAND;

                        // Create the object on the player's right hand
                        AttachItemToPlayer(player, playerItem.id, playerItem.hash, "IK_R_Hand", businessItem.position, businessItem.rotation, EntityData.PLAYER_RIGHT_HAND);
                    }

                    // Get the new owner of the item
                    playerItem.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the item's owner
                            Database.UpdateItem(item);
                        });
                    });

                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.player_picked_item);
                }
                else
                {
                    WeaponCrateModel weaponCrate = Weapons.GetClosestWeaponCrate(player);
                    if (weaponCrate != null)
                    {
                        int index = Weapons.weaponCrateList.IndexOf(weaponCrate);
                        weaponCrate.carriedEntity = Constants.ITEM_ENTITY_PLAYER;
                        weaponCrate.carriedIdentifier = player.Value;
                        player.PlayAnimation("anim@heists@box_carry@", "idle", (int)(Constants.AnimationFlags.Loop | Constants.AnimationFlags.OnlyAnimateUpperBody | Constants.AnimationFlags.AllowPlayerControl));

                        // Create the object on the player's arms
                        AttachItemToPlayer(player, index, weaponCrate.crateObject.Model.ToString(), "IK_R_Hand", new Vector3(0.0f, -0.5f, -0.25f), new Vector3(), EntityData.PLAYER_WEAPON_CRATE);

                        // Remove the item's object
                        weaponCrate.crateObject.Delete();
                        weaponCrate.crateObject = null;
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_items_near);
                    }
                }
            }
        }

        [Command(Commands.COM_DROP)]
        public void DropCommand(Client player)
        {
            if (Inventory.HasPlayerItemOnHand(player))
            {
                // Get the item on the right hand
                ItemModel item = null;

                if (player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
                {
                    string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                    item = GetItemModelFromId(NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId);
                }
                else
                {
                    item = Weapons.GetWeaponItem(player, player.CurrentWeapon);
                }

                // Get the business item from the hash
                BusinessItemModel businessItem = Business.GetBusinessItemFromHash(item.hash);

                // Drop the item on the hand
                DropItem(player, item, businessItem, true);
            }
            else if (player.GetSharedData(EntityData.PLAYER_WEAPON_CRATE) != null)
            {
                WeaponCrateModel weaponCrate = Weapons.GetPlayerCarriedWeaponCrate(player.Value);

                if (weaponCrate != null)
                {
                    // Update the new position and carrier
                    weaponCrate.position = new Vector3(player.Position.X, player.Position.Y, player.Position.Z - 1.0f);
                    weaponCrate.carriedEntity = string.Empty;
                    weaponCrate.carriedIdentifier = 0;

                    // Create the object on the floor
                    weaponCrate.crateObject = NAPI.Object.CreateObject(481432069, weaponCrate.position, new Vector3(), 0);

                    // Dettach the object from the player
                    RemoveItemOnHands(player);
                    player.StopAnimation();

                    string message = string.Format(InfoRes.player_inventory_drop, GenRes.weapon_crate);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.right_hand_empty);
            }
        }

        [Command(Commands.COM_TICKET, Commands.HLP_HELP_REQUEST, GreedyArg = true)]
        public void TicketCommand(Client player, string message)
        {
            foreach (AdminTicketModel ticket in adminTicketList)
            {
                if (player.Value == ticket.playerId)
                {
                    ticket.question = message;
                    return;
                }
            }

            // Create a new ticket
            AdminTicketModel adminTicket = new AdminTicketModel();
            {
                adminTicket.playerId = player.Value;
                adminTicket.question = message;
            }

            // Add the ticket to the list
            adminTicketList.Add(adminTicket);

            // Send the message to the staff online
            foreach (Client target in NAPI.Pools.GetAllPlayers())
            {
                if (target.GetData(EntityData.PLAYER_PLAYING) != null && target.GetData(EntityData.PLAYER_ADMIN_RANK) > 0)
                {
                    target.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.new_admin_ticket);
                }
                else if (target == player)
                {
                    player.SendChatMessage(Constants.COLOR_SUCCESS + SuccRes.help_request_sent);
                }
            }
        }

        [Command(Commands.COM_DOOR)]
        public void DoorCommand(Client player)
        {
            // Check if the player's in his house
            foreach (HouseModel house in House.houseList)
            {
                if ((player.Position.DistanceTo(house.position) <= 1.5f && player.Dimension == house.dimension) || player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == house.id)
                {
                    if (House.HasPlayerHouseKeys(player, house) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_house_owner);
                    }
                    else
                    {
                        house.locked = !house.locked;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Update the house
                                Database.UpdateHouse(house);
                            });
                        });

                        player.SendChatMessage(house.locked ? Constants.COLOR_INFO + InfoRes.house_locked : Constants.COLOR_INFO + InfoRes.house_opened);
                    }
                    return;
                }
            }

            // Check if the player's in his business
            foreach (BusinessModel business in Business.businessList)
            {
                if ((player.Position.DistanceTo(business.position) <= 1.5f && player.Dimension == business.dimension) || player.GetData(EntityData.PLAYER_BUSINESS_ENTERED) == business.id)
                {
                    if (Business.HasPlayerBusinessKeys(player, business) == false)
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_business_owner);
                    }
                    else
                    {
                        business.locked = !business.locked;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Update the business
                                Database.UpdateBusiness(business);
                            });
                        });

                        player.SendChatMessage(business.locked ? Constants.COLOR_INFO + InfoRes.business_locked : Constants.COLOR_INFO + InfoRes.business_opened);
                    }
                    return;
                }
            }

            // He's not in any house or business
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_house_business);
        }
    }
}
