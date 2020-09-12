using GTANetworkAPI;
using WiredPlayers.database;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.factions;
using WiredPlayers.character;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.information;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WiredPlayers.house
{
    public class House : Script
    {
        public static List<HouseModel> houseList;

        public static void LoadDatabaseHouses()
        {
            houseList = Database.LoadAllHouses();
            foreach (HouseModel houseModel in houseList)
            {
                string houseLabelText = GetHouseLabelText(houseModel);
                houseModel.houseLabel = NAPI.TextLabel.CreateTextLabel(houseLabelText, houseModel.position, 20.0f, 0.75f, 4, new Color(255, 255, 255), false, houseModel.dimension);
            }
        }

        public static HouseModel GetHouseById(int id)
        {
            // Get the house with the specified identifier
            return houseList.Where(houseModel => houseModel.id == id).FirstOrDefault();
        }

        public static HouseModel GetClosestHouse(Client player, float distance = 1.5f)
        {
            HouseModel house = null;
            foreach (HouseModel houseModel in houseList)
            {
                if (player.Position.DistanceTo(houseModel.position) < distance && player.Dimension == houseModel.dimension)
                {
                    house = houseModel;
                    distance = player.Position.DistanceTo(houseModel.position);
                }
            }
            return house;
        }

        public static Vector3 GetHouseExitPoint(string ipl)
        {
            // Get the house with the specified IPL
            HouseIplModel houseIpl = Constants.HOUSE_IPL_LIST.Where(houseIplModel => houseIplModel.ipl == ipl).FirstOrDefault();

            return houseIpl?.position;
        }

        public static bool HasPlayerHouseKeys(Client player, HouseModel house)
        {
            return player.Name == house.owner || player.GetData(EntityData.PLAYER_RENT_HOUSE) == house.id;
        }

        public static string GetHouseLabelText(HouseModel house)
        {
            string label = string.Empty;

            switch (house.status)
            {
                case Constants.HOUSE_STATE_NONE:
                    label = house.name + "\n" + GenRes.state_occupied;
                    break;
                case Constants.HOUSE_STATE_RENTABLE:
                    label = house.name + "\n" + GenRes.state_rent + "\n" + house.rental + "$";
                    break;
                case Constants.HOUSE_STATE_BUYABLE:
                    label = house.name + "\n" + GenRes.state_sale + "\n" + house.price + "$";
                    break;
            }
            return label;
        }

        public static void BuyHouse(Client player, HouseModel house)
        {
            if (player.GetData(EntityData.PLAYER_LEVEL) < 3)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You need to be Level 3 in order to purchase your first property.");
                return;
            }

            if (player.GetData(EntityData.PLAYER_HOUSES) > 1)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + "You already have two houses. You'll need to speak to the goverment to get more. " + Constants.COLOR_YELLOW + "/premium");
                return;
            }

            if (house.status != Constants.HOUSE_STATE_BUYABLE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_buyable);
                return;
            }

            if (player.GetSharedData(EntityData.PLAYER_BANK) < house.price)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_money);
                return;
            }
            
            int bank = player.GetSharedData(EntityData.PLAYER_BANK) - house.price;
            string message = string.Format(InfoRes.house_buy, house.name, house.price);
            string labelText = GetHouseLabelText(house);
            player.SendChatMessage(Constants.COLOR_INFO + message);
            player.SetSharedData(EntityData.PLAYER_BANK, bank);
            house.status = Constants.HOUSE_STATE_NONE;
            house.houseLabel.Text = GetHouseLabelText(house);
            house.owner = player.Name;
            house.locked = true;           

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Update the house
                    Database.UpdateHouse(house);
                });
            });
        }

        [RemoteEvent("getPlayerPurchasedClothes")]
        public void GetPlayerPurchasedClothesEvent(Client player, int type, int slot)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            int sex = player.GetData(EntityData.PLAYER_SEX);

            List<ClothesModel> clothesList = Customization.GetPlayerClothes(playerId).Where(c => c.type == type && c.slot == slot).ToList();

            if(clothesList.Count > 0)
            {
                List<string> clothesNames = Customization.GetClothesNames(clothesList);

                // Show player's clothes
                player.TriggerEvent("showPlayerClothes", NAPI.Util.ToJson(clothesList), NAPI.Util.ToJson(clothesNames));
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_clothes_in_wardrobe);
            }
        }

        [RemoteEvent("wardrobeClothesItemSelected")]
        public void WardrobeClothesItemSelectedEvent(Client player, int slot, int index)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            // Get the clothes from the player in the slot
            List<ClothesModel> playerSlotClothes = Customization.clothesList.Where(c => c.slot == slot && c.player == playerId).ToList();

            // Replace player clothes for the new ones
            foreach (ClothesModel clothes in playerSlotClothes)
            {
                if (playerSlotClothes[index] == clothes)
                {
                    if (clothes.type == 0)
                    {
                        player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                    }
                    else
                    {
                        player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                    }

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Dress the clothes
                            clothes.dressed = true;

                            // Update dressed clothes into database
                            Database.UpdateClothes(clothes);
                        });
                    });
                }
                else if (clothes.dressed && playerSlotClothes[index] != clothes)
                {
                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Undress the clothes
                            clothes.dressed = false;

                            // Update dressed clothes into database
                            Database.UpdateClothes(clothes);
                        });
                    });
                }
            }
        }

        [Command(Commands.COM_RENTABLE, Commands.HLP_RENTABLE_COMMAND)]
        public void RentableCommand(Client player, int amount = 0)
        {
            if (player.GetData(EntityData.PLAYER_HOUSE_ENTERED) == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_in_house);
            }
            else
            {
                int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
                HouseModel house = GetHouseById(houseId);
                if (house == null || house.owner != player.Name)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_house_owner);
                }
                else if (amount > 0)
                {
                    house.rental = amount;
                    house.status = Constants.HOUSE_STATE_RENTABLE;
                    house.tenants = 2;

                    // Update house's textlabel
                    house.houseLabel.Text = GetHouseLabelText(house);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the house
                            Database.UpdateHouse(house);
                        });
                    });

                    // Message sent to the player
                    string message = string.Format(InfoRes.house_state_rent, amount);
                    player.SendChatMessage(Constants.COLOR_INFO + message);
                }
                else if (house.status == Constants.HOUSE_STATE_RENTABLE)
                {
                    house.status = Constants.HOUSE_STATE_NONE;
                    house.tenants = 2;

                    // Update house's textlabel
                    house.houseLabel.Text = GetHouseLabelText(house);

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the house
                            Database.KickTenantsOut(house.id);
                            Database.UpdateHouse(house);
                        });
                    });

                    // Message sent to the player
                    player.SendChatMessage(Constants.COLOR_INFO + InfoRes.house_rent_cancel);
                }
                else
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.money_amount_positive);
                }
            }
        }

        [Command(Commands.COM_RENT)]
        public void RentCommand(Client player)
        {
            // Check if the player has a rented house
            if(player.GetData(EntityData.PLAYER_RENT_HOUSE) > 0) 
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_house_rented);
                return;
            }

            // Get the closes house
            HouseModel house = GetClosestHouse(player);

            if(house == null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_house_near);
                return;
            }

            if (house.status != Constants.HOUSE_STATE_RENTABLE)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.house_not_rentable);
                return;
            }

            // Get the player's money on hand
            int playerMoney = player.GetSharedData(EntityData.PLAYER_MONEY);

            if (playerMoney < house.rental)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_not_rent_money);
                return;
            }

            // Rent the house to the player
            player.SetSharedData(EntityData.PLAYER_MONEY, playerMoney - house.rental);
            player.SetData(EntityData.PLAYER_RENT_HOUSE, house.id);
            house.tenants--;

            if (house.tenants == 0)
            {
                house.status = Constants.HOUSE_STATE_NONE;
                house.houseLabel.Text = GetHouseLabelText(house);
            }

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Update house's tenants
                    Database.UpdateHouse(house);
                });
            });

            // Send the message to the player
            string message = string.Format(InfoRes.house_rent, house.name, house.rental);
            player.SendChatMessage(Constants.COLOR_INFO + message);
        }

        [Command(Commands.COM_UNRENT)]
        public void UnrentCommand(Client player)
        {
            int rentedHouse = player.GetData(EntityData.PLAYER_RENT_HOUSE);

            // Check if the house has any rented house
            if (rentedHouse == 0)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_rented_house);
                return;
            }

            // Get the house where the player is rented
            HouseModel house = GetHouseById(rentedHouse);

            if(player.Position.DistanceTo2D(house.position) > 2.5f)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_in_house_door);
                return;
            }

            // Remove player's rental
            player.SetData(EntityData.PLAYER_RENT_HOUSE, 0);
            house.tenants++;

            Task.Factory.StartNew(() =>
            {
                NAPI.Task.Run(() =>
                {
                    // Update house's tenants
                    Database.UpdateHouse(house);
                });
            });

            // Send the message to the player
            string message = string.Format(InfoRes.house_rent_stop, house.name);
            player.SendChatMessage(Constants.COLOR_INFO + message);
        }

        [Command(Commands.COM_WARDROBE)]
        public void WardrobeCommand(Client player)
        {
            int houseId = player.GetData(EntityData.PLAYER_HOUSE_ENTERED);
            if (houseId > 0)
            {
                HouseModel house = GetHouseById(houseId);
                if (HasPlayerHouseKeys(player, house) == true)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

                    if (Customization.GetPlayerClothes(playerId).Count > 0)
                    {
                        player.TriggerEvent("showPlayerWardrobe", Faction.IsPoliceMember(player));
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_clothes_in_wardrobe);
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
