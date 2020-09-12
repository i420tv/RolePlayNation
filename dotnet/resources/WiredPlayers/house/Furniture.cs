using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.messages.error;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WiredPlayers.house
{
    public class Furniture : Script
    {
        private static List<FurnitureModel> furnitureList;

        public static void LoadDatabaseFurniture()
        {
            furnitureList = Database.LoadAllFurniture();
            foreach (FurnitureModel furnitureModel in furnitureList)
            {
                furnitureModel.handle = NAPI.Object.CreateObject(furnitureModel.hash, furnitureModel.position, furnitureModel.rotation, (byte)furnitureModel.house);
            }
        }

        public List<FurnitureModel> GetFurnitureInHouse(int houseId)
        {
            // Get all the furniture in the specified house
            return furnitureList.Where(furniture => furniture.house == houseId).ToList();
        }

        public FurnitureModel GetFurnitureById(int id)
        {
            // Get the furniture given the specific identifier
            return furnitureList.Where(furniture => furniture.id == id).FirstOrDefault();
        }
        
        [Command(Commands.COM_FURNITURE, Commands.HLP_FURNITURE_COMMAND)]
        public void FurnitureCommand(Client player, string action)
        {
            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) != null)
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = House.GetHouseById(houseId);

                if (house != null && house.owner == player.Name)
                {
                    switch (action.ToLower())
                    {
                        case Commands.ARG_PLACE:
                            FurnitureModel furniture = new FurnitureModel();
                            {
                                furniture.hash = NAPI.Util.GetHashKey("bkr_prop_weed_pallet");
                                furniture.house = Convert.ToUInt32(houseId);
                                furniture.position = player.Position;
                                furniture.rotation = player.Rotation;
                                furniture.handle = NAPI.Object.CreateObject(furniture.hash, furniture.position, furniture.rotation, (byte)furniture.house);
                            }

                            furnitureList.Add(furniture);
                            break;
                        case Commands.ARG_MOVE:
                            string furnitureJson = NAPI.Util.ToJson(GetFurnitureInHouse(houseId));
                            player.SetSharedData(EntityData.PLAYER_MOVING_FURNITURE, true);
                            player.TriggerEvent("moveFurniture", furnitureJson);
                            break;
                        case Commands.ARG_REMOVE:
                            break;
                        default:
                            player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_FURNITURE_COMMAND);
                            break;
                    }
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_house_owner);
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_house);
            }
        }
    }
}
