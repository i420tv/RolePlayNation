using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.globals;
using WiredPlayers.database;
using System.Collections.Generic;

using System.Linq;

namespace WiredPlayers.character
{
    public class Inventory : Script
    {
        public static Dictionary<int, ItemModel> ItemCollection;
        public static void LoadDatabaseItems()
        {
            // Create the item list
            Globals.itemList = Database.LoadAllItems();

            // Get the objects on the ground
            List<ItemModel> groundItems = Globals.itemList.Where(it => it.ownerEntity == Constants.ITEM_ENTITY_GROUND).ToList();

            foreach (ItemModel item in groundItems)
            {
                // Get the hash from the object
                WeaponHash weaponHash = NAPI.Util.WeaponNameToModel(item.hash);
                uint hash = weaponHash == 0 ? uint.Parse(item.hash) : NAPI.Util.GetHashKey(Constants.WEAPON_ITEM_MODELS[weaponHash]);

                // Create each of the items on the ground
                item.objectHandle = NAPI.Object.CreateObject(hash, item.position, new Vector3(), 255, item.dimension);
            }
        }
        public static ItemModel GetPlayerItemModelFromHash(int playerId, string hash)
        {
            // Get the item given the hash
            return ItemCollection.Values.FirstOrDefault(i => i.ownerEntity == Constants.ITEM_ENTITY_PLAYER && i.ownerIdentifier == playerId && i.hash == hash);
        }

        public static bool HasPlayerItemOnHand(Client player)
        {
            // Check if the player has an item or weapon on the right hand
            return player.GetSharedData(EntityData.PLAYER_RIGHT_HAND) != null || player.CurrentWeapon != WeaponHash.Unarmed;
        }
        public static ItemModel GetItemModelFromId(int itemId)
        {
            return ItemCollection.ContainsKey(itemId) ? ItemCollection[itemId] : null;
        }
    }

}   
