using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.factions;
using WiredPlayers.messages.administration;
using WiredPlayers.messages.error;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

namespace WiredPlayers.weapons
{
    public class Weapons : Script
    {
        private static Timer weaponTimer;
        private static List<Timer> vehicleWeaponTimer;
        public static List<WeaponCrateModel> weaponCrateList;

        public Weapons()
        {
            vehicleWeaponTimer = new List<Timer>();
            weaponCrateList = new List<WeaponCrateModel>();
        }

        public static void GivePlayerWeaponItems(Client player)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ItemModel item in Globals.itemList)
            {
                if (!int.TryParse(item.hash, out int itemId) && item.ownerIdentifier == playerId && item.ownerEntity == Constants.ITEM_ENTITY_WHEEL)
                {
                    WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);
                    player.GiveWeapon(weaponHash, 0);
                    player.SetWeaponAmmo(weaponHash, item.amount);
                }
            }
        }

        public static void GivePlayerNewWeapon(Client player, WeaponHash weapon, int bullets, bool licensed)
        {
            // Create weapon model
            ItemModel weaponModel = new ItemModel();
            {
                weaponModel.hash = weapon.ToString();
                weaponModel.amount = bullets;
                weaponModel.ownerEntity = Constants.ITEM_ENTITY_WHEEL;
                weaponModel.ownerIdentifier = player.GetData(EntityData.PLAYER_SQL_ID);
                weaponModel.position = new Vector3();
                weaponModel.dimension = 0;
            }

            weaponModel.id = Database.AddNewItem(weaponModel);
            Globals.itemList.Add(weaponModel);

            // Give the weapon to the player
            player.SetWeaponAmmo(weapon, bullets);

            if (licensed)
            {
                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // We add the weapon as a registered into database
                        Database.AddLicensedWeapon(weaponModel.id, player.Name);
                    });
                });
            }
        }

        public static ItemModel GetWeaponItem(Client player, WeaponHash weaponHash)
        {
            // Get the identifier from the player
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            // Get the item identifier from the weapon hash
            List<ItemModel> weapons = Globals.itemList.Where(i => i.ownerEntity == Constants.ITEM_ENTITY_WHEEL && i.ownerIdentifier == playerId).ToList();

            return weapons.Where(w => NAPI.Util.WeaponNameToModel(w.hash) == weaponHash).FirstOrDefault();
        }

        public static string GetGunAmmunitionType(WeaponHash weapon)
        {
            // Get the ammunition type given a weapon
            GunModel gunModel = Constants.GUN_LIST.Where(gun => weapon == gun.weapon).FirstOrDefault();

            return gunModel == null ? string.Empty : gunModel.ammunition;
        }

        public static int GetGunAmmunitionCapacity(WeaponHash weapon)
        {
            // Get the capacity from a weapons's clip magazine
            GunModel gunModel = Constants.GUN_LIST.Where(gun => weapon == gun.weapon).FirstOrDefault();

            return gunModel == null ? 0 : gunModel.capacity;
        }

        public static ItemModel GetEquippedWeaponItemModelByHash(int playerId, WeaponHash weapon)
        {
            // Get the equipped weapon's item model
            return Globals.itemList.Where(itemModel => itemModel.ownerIdentifier == playerId && (itemModel.ownerEntity == Constants.ITEM_ENTITY_WHEEL || itemModel.ownerEntity == Constants.ITEM_ENTITY_RIGHT_HAND) && weapon.ToString() == itemModel.hash).FirstOrDefault();
        }

        public static WeaponCrateModel GetClosestWeaponCrate(Client player, float distance = 1.5f)
        {
            // Get the closest weapon crate
            return weaponCrateList.Where(weaponCrateModel => player.Position.DistanceTo(weaponCrateModel.position) < distance && weaponCrateModel.carriedEntity == string.Empty).FirstOrDefault();
        }

        public static WeaponCrateModel GetPlayerCarriedWeaponCrate(int playerId)
        {
            // Get the weapon crate carried by the player
            return weaponCrateList.Where(weaponCrateModel => weaponCrateModel.carriedEntity == Constants.ITEM_ENTITY_PLAYER && weaponCrateModel.carriedIdentifier == playerId).FirstOrDefault();
        }

        public static void WeaponsPrewarn()
        {
            // Send the warning message to all factions
            foreach (Client player in NAPI.Pools.GetAllPlayers())
            {
                if (player.GetData(EntityData.PLAYER_PLAYING) != null && player.GetData(EntityData.PLAYER_FACTION) > Constants.LAST_STATE_FACTION)
                {
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.weapon_prewarn);
                }
            }

            // Timer for the next warning
            weaponTimer = new Timer(OnWeaponPrewarn, null, 600000, Timeout.Infinite);
        }

        public static void OnPlayerDisconnected(Client player)
        {
            WeaponCrateModel weaponCrate = GetPlayerCarriedWeaponCrate(player.Value);

            if (weaponCrate != null)
            {
                weaponCrate.position = new Vector3(player.Position.X, player.Position.Y, player.Position.X - 1.0f);
                weaponCrate.carriedEntity = string.Empty;
                weaponCrate.carriedIdentifier = 0;

                // Place the crate on the floor
                weaponCrate.crateObject.Detach();
                weaponCrate.crateObject.Position = weaponCrate.position;
            }
        }

        private static List<Vector3> GetRandomWeaponSpawns(int spawnPosition)
        {
            Random random = new Random();
            List<Vector3> weaponSpawns = new List<Vector3>();
            List<CrateSpawnModel> cratesInSpawn = GetSpawnsInPosition(spawnPosition);

            while (weaponSpawns.Count < Constants.MAX_CRATES_SPAWN)
            {
                Vector3 crateSpawn = cratesInSpawn[random.Next(cratesInSpawn.Count)].position;
                if (weaponSpawns.Contains(crateSpawn) == false)
                {
                    weaponSpawns.Add(crateSpawn);
                }
            }
            return weaponSpawns;
        }

        private static List<CrateSpawnModel> GetSpawnsInPosition(int spawnPosition)
        {
            List<CrateSpawnModel> crateSpawnList = new List<CrateSpawnModel>();
            foreach (CrateSpawnModel crateSpawn in Constants.CRATE_SPAWN_LIST)
            {
                if (crateSpawn.spawnPoint == spawnPosition)
                {
                    crateSpawnList.Add(crateSpawn);
                }
            }
            return crateSpawnList;
        }

        private static CrateContentModel GetRandomCrateContent(int type, int chance)
        {
            CrateContentModel crateContent = new CrateContentModel();
            
            foreach (WeaponProbabilityModel weaponAmmo in Constants.WEAPON_CHANCE_LIST)
            {
                if (weaponAmmo.type == type && weaponAmmo.minChance <= chance && weaponAmmo.maxChance >= chance)
                {
                    crateContent.item = weaponAmmo.hash;
                    crateContent.amount = weaponAmmo.amount;
                    break;
                }
            }

            return crateContent;
        }

        private static void OnWeaponPrewarn(object unused)
        {
            NAPI.Task.Run(() =>
            {
                weaponTimer.Dispose();

                int currentSpawn = 0;
                weaponCrateList = new List<WeaponCrateModel>();
            
                Random random = new Random();
                int spawnPosition = random.Next(Constants.MAX_WEAPON_SPAWNS);

                // Get crates' spawn points
                List<Vector3> weaponSpawns = GetRandomWeaponSpawns(spawnPosition);

                foreach (Vector3 spawn in weaponSpawns)
                {
                    // Calculate weapon or ammunition crate
                    int type = currentSpawn % 2;
                    int chance = random.Next(type == 0 ? Constants.MAX_WEAPON_CHANCE : Constants.MAX_AMMO_CHANCE);
                    CrateContentModel crateContent = GetRandomCrateContent(type, chance);

                    // We create the crate
                    WeaponCrateModel weaponCrate = new WeaponCrateModel();
                    {
                        weaponCrate.contentItem = crateContent.item;
                        weaponCrate.contentAmount = crateContent.amount;
                        weaponCrate.position = spawn;
                        weaponCrate.carriedEntity = string.Empty;
                        weaponCrate.crateObject = NAPI.Object.CreateObject(481432069, spawn, new Vector3(), 0);
                    }

                    weaponCrateList.Add(weaponCrate);
                    currentSpawn++;
                }

                // Warn all the factions about the place
                foreach (Client player in NAPI.Pools.GetAllPlayers())
                {
                    if (player.GetData(EntityData.PLAYER_PLAYING) != null && player.GetData(EntityData.PLAYER_FACTION) > Constants.LAST_STATE_FACTION)
                    {
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.weapon_spawn_island);
                    }
                }

                // Timer to warn the police
                weaponTimer = new Timer(OnPoliceCalled, null, 240000, Timeout.Infinite);
            });
        }

        private static void OnPoliceCalled(object unused)
        {
            NAPI.Task.Run(() =>
            {
                weaponTimer.Dispose();

                // Send the warning message to all the police members
                foreach (Client player in NAPI.Pools.GetAllPlayers())
                {
                    if (player.GetData(EntityData.PLAYER_PLAYING) != null && Faction.IsPoliceMember(player))
                    {
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.weapon_spawn_island);
                    }
                }

                // Finish the event
                weaponTimer = new Timer(OnWeaponEventFinished, null, 3600000, Timeout.Infinite);
            });
        }

        private static void OnVehicleUnpackWeapons(object vehicleObject)
        {
            NAPI.Task.Run(() =>
            {
                Vehicle vehicle = (Vehicle)vehicleObject;
                int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);

                foreach (WeaponCrateModel weaponCrate in weaponCrateList)
                {
                    if (weaponCrate.carriedEntity == Constants.ITEM_ENTITY_VEHICLE && weaponCrate.carriedIdentifier == vehicleId)
                    {
                        // Unpack the weapon in the crate
                        ItemModel item = new ItemModel();
                        {
                            item.hash = weaponCrate.contentItem;
                            item.amount = weaponCrate.contentAmount;
                            item.ownerEntity = Constants.ITEM_ENTITY_VEHICLE;
                            item.ownerIdentifier = vehicleId;
                        }

                        // Delete the crate
                        weaponCrate.carriedIdentifier = 0;
                        weaponCrate.carriedEntity = string.Empty;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                item.id = Database.AddNewItem(item);
                                Globals.itemList.Add(item);
                            });
                        });
                    }
                }

                // Warn driver about unpacked crates
                foreach (Client player in NAPI.Pools.GetAllPlayers())
                {
                    if (player.GetData(EntityData.PLAYER_VEHICLE) == vehicle)
                    {
                        player.ResetData(EntityData.PLAYER_VEHICLE);
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.weapons_unpacked);
                        break;
                    }
                }

                vehicle.ResetData(EntityData.VEHICLE_WEAPON_UNPACKING);
            });
        }

        private static void OnWeaponEventFinished(object unused)
        {
            NAPI.Task.Run(() =>
            {
                weaponTimer.Dispose();

                foreach (WeaponCrateModel crate in weaponCrateList)
                {
                    if (crate.crateObject != null)
                    {
                        crate.crateObject.Delete();
                    }
                }

                // Destroy weapon crates
                weaponCrateList = new List<WeaponCrateModel>();
                weaponTimer = null;
            });
        }

        private int GetVehicleWeaponCrates(int vehicleId)
        {
            // Get the crates on the vehicle
            return weaponCrateList.Where(w => w.carriedEntity == Constants.ITEM_ENTITY_VEHICLE && w.carriedIdentifier == vehicleId).Count();
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicle, sbyte seat)
        {
            if (vehicle.GetData(EntityData.VEHICLE_ID) != null && player.VehicleSeat == (int)VehicleSeat.Driver)
            {
                int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                if (vehicle.GetData(EntityData.VEHICLE_WEAPON_UNPACKING) == null && GetVehicleWeaponCrates(vehicleId) > 0)
                {
                    // Mark the delivery point
                    Vector3 weaponPosition = new Vector3(-2085.543f, 2600.857f, -0.4712417f);
                    Checkpoint weaponCheckpoint = NAPI.Checkpoint.CreateCheckpoint(4, weaponPosition, new Vector3(0.0f, 0.0f, 0.0f), 2.5f, new Color(198, 40, 40, 200));
                    player.SetData(EntityData.PLAYER_JOB_COLSHAPE, weaponCheckpoint);
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.weapon_position_mark);
                    player.TriggerEvent("showWeaponCheckpoint", weaponPosition);
                }
            }
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void OnPlayerExitVehicle(Client player, Vehicle vehicle)
        {
            if (vehicle.GetData(EntityData.VEHICLE_ID) != null)
            {
                int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                if (player.GetData(EntityData.PLAYER_JOB_COLSHAPE) != null && GetVehicleWeaponCrates(vehicleId) > 0)
                {
                    player.TriggerEvent("deleteWeaponCheckpoint");
                }
            }
        }

        [ServerEvent(Event.PlayerEnterCheckpoint)]
        public void OnPlayerEnterCheckpoint(Checkpoint checkpoint, Client player)
        {
            if (player.GetData(EntityData.PLAYER_JOB_COLSHAPE) != null)
            {
                if (checkpoint == player.GetData(EntityData.PLAYER_JOB_COLSHAPE) && player.VehicleSeat == (int)VehicleSeat.Driver)
                {
                    Vehicle vehicle = player.Vehicle;
                    int vehicleId = vehicle.GetData(EntityData.VEHICLE_ID);
                    if (GetVehicleWeaponCrates(vehicleId) > 0)
                    {
                        // Delete the checkpoint
                        Checkpoint weaponCheckpoint = player.GetData(EntityData.PLAYER_JOB_COLSHAPE);
                        player.ResetData(EntityData.PLAYER_JOB_COLSHAPE);
                        player.TriggerEvent("deleteWeaponCheckpoint");
                        weaponCheckpoint.Delete();

                        // Freeze the vehicle
                        vehicle.EngineStatus = false;
                        player.SetData(EntityData.PLAYER_VEHICLE, vehicle);
                        vehicle.SetData(EntityData.VEHICLE_WEAPON_UNPACKING, true);

                        vehicleWeaponTimer.Add(new Timer(OnVehicleUnpackWeapons, vehicle, 60000, Timeout.Infinite));
                        
                        player.SendChatMessage(Constants.COLOR_INFO + InfoRes.wait_for_weapons);
                    }
                }
            }
        }

        [RemoteEvent("reloadPlayerWeapon")]
        public void ReloadPlayerWeaponEvent(Client player, int currentBullets)
        {
            WeaponHash weapon = player.CurrentWeapon;
            int maxCapacity = GetGunAmmunitionCapacity(weapon);

            if (currentBullets < maxCapacity)
            {
                string bulletType = GetGunAmmunitionType(weapon);
                int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                ItemModel bulletItem = Globals.GetPlayerItemModelFromHash(playerId, bulletType);
                if (bulletItem != null)
                {
                    int bulletsLeft = maxCapacity - currentBullets;
                    if (bulletsLeft >= bulletItem.amount)
                    {
                        currentBullets += bulletItem.amount;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                Database.RemoveItem(bulletItem.id);
                                Globals.itemList.Remove(bulletItem);
                            });
                        });
                    }
                    else
                    {
                        currentBullets += bulletsLeft;
                        bulletItem.amount -= bulletsLeft;

                        Task.Factory.StartNew(() =>
                        {
                            NAPI.Task.Run(() =>
                            {
                                // Update the remaining bullets
                                Database.UpdateItem(bulletItem);
                            });
                        });
                    }

                    // Add ammunition to the weapon
                    ItemModel weaponItem = GetEquippedWeaponItemModelByHash(playerId, weapon);
                    weaponItem.amount = currentBullets;

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the bullets in the weapon
                            Database.UpdateItem(weaponItem);
                        });
                    });

                    // Reload the weapon
                    player.SetWeaponAmmo(weapon, currentBullets);
                    player.TriggerEvent("makePlayerReload");
                }
            }
        }

        [RemoteEvent("updateWeaponBullets")]
        public void UpdateWeaponBullets(Client player, int bullets)
        {
            if(player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null)
            {
                // Get the weapon from the hand
                string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND).ToString();
                int itemId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                ItemModel item = Globals.GetItemModelFromId(itemId);

                Task.Factory.StartNew(() =>
                {
                    NAPI.Task.Run(() =>
                    {
                        // Set the bullets on the weapon
                        item.amount = bullets;

                        // Update the remaining bullets
                        Database.UpdateItem(item);
                    });
                });
            }
        }

        [Command(Commands.COM_WEAPONS_EVENT)]
        public void WeaponsEventCommand(Client player)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_S_GAME_MASTER)
            {
                if (weaponTimer == null)
                {
                    WeaponsPrewarn();
                    player.SendChatMessage(Constants.COLOR_ADMIN_INFO + AdminRes.weapon_event_started);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.weapon_event_on_course);
                }
            }
        }
    }
}
