using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.database;
using WiredPlayers.messages.error;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WiredPlayers.character
{
    public class Customization : Script
    {
        public static List<ClothesModel> clothesList;
        public static List<TattooModel> tattooList;

        public static void ApplyPlayerCustomization(Client player, SkinModel skinModel, int sex)
        {
            // Populate the head
            HeadBlend headBlend = new HeadBlend();
            {
                headBlend.ShapeFirst = Convert.ToByte(skinModel.firstHeadShape);
                headBlend.ShapeSecond = Convert.ToByte(skinModel.secondHeadShape);
                headBlend.SkinFirst = Convert.ToByte(skinModel.firstSkinTone);
                headBlend.SkinSecond = Convert.ToByte(skinModel.secondSkinTone);
                headBlend.ShapeMix = skinModel.headMix;
                headBlend.SkinMix = skinModel.skinMix;
            }

            // Get the hair and eyes colors
            byte eyeColor = Convert.ToByte(skinModel.eyesColor);
            byte hairColor = Convert.ToByte(skinModel.firstHairColor);
            byte hightlightColor = Convert.ToByte(skinModel.secondHairColor);

            // Add the face features
            float[] faceFeatures = new float[]
            {
                skinModel.noseWidth, skinModel.noseHeight, skinModel.noseLength, skinModel.noseBridge, skinModel.noseTip, skinModel.noseShift, skinModel.browHeight,
                skinModel.browWidth, skinModel.cheekboneHeight, skinModel.cheekboneWidth, skinModel.cheeksWidth, skinModel.eyes, skinModel.lips, skinModel.jawWidth,
                skinModel.jawHeight, skinModel.chinLength, skinModel.chinPosition, skinModel.chinWidth, skinModel.chinShape, skinModel.neckWidth
            };

            // Populate the head overlays
            Dictionary<int, HeadOverlay> headOverlays = new Dictionary<int, HeadOverlay>();

            for (int i = 0; i < Constants.MAX_HEAD_OVERLAYS; i++)
            {
                // Get the overlay model and color
                int[] overlayData = GetOverlayData(skinModel, i);

                // Create the overlay
                HeadOverlay headOverlay = new HeadOverlay();
                {
                    headOverlay.Index = Convert.ToByte(overlayData[0]);
                    headOverlay.Color = Convert.ToByte(overlayData[1]);
                    headOverlay.SecondaryColor = 0;
                    headOverlay.Opacity = 1.0f;
                }

                // Add the overlay
                headOverlays[i] = headOverlay;
            }

            // Update the character's skin
            player.SetCustomization(sex == Constants.SEX_MALE, headBlend, eyeColor, hairColor, hightlightColor, faceFeatures, headOverlays, new Decoration[] { });
            player.SetClothes(2, skinModel.hairModel, 0);
        }

        public static void ApplyPlayerClothes(Client player)
        {
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            foreach (ClothesModel clothes in clothesList)
            {
                if (clothes.player == playerId && clothes.dressed)
                {
                    if (clothes.type == 0)
                    {
                        player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                    }
                    else
                    {
                        player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                    }
                }
            }
        }

        public static void ApplyPlayerTattoos(Client player)
        {
            // Get the tattoos from the player
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            List<TattooModel> playerTattoos = tattooList.Where(t => t.player == playerId).ToList();

            foreach (TattooModel tattoo in playerTattoos)
            {
                // Add each tattoo to the player
                Decoration decoration = new Decoration();
                {
                    decoration.Collection = NAPI.Util.GetHashKey(tattoo.library);
                    decoration.Overlay = NAPI.Util.GetHashKey(tattoo.hash);
                }

                player.SetDecoration(decoration);
            }
        }

        public static void RemovePlayerTattoos(Client player)
        {
            // Check if the player has been registered
            if (player.GetData(EntityData.PLAYER_SQL_ID) == null) return;

            // Get the tattoos from the player
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            List<TattooModel> playerTattoos = tattooList.Where(t => t.player == playerId).ToList();

            foreach (TattooModel tattoo in playerTattoos)
            {
                // Add each tattoo to the player
                Decoration decoration = new Decoration();
                {
                    decoration.Collection = NAPI.Util.GetHashKey(tattoo.library);
                    decoration.Overlay = NAPI.Util.GetHashKey(tattoo.hash);
                }

                player.RemoveDecoration(decoration);
            }
        }

        private static int[] GetOverlayData(SkinModel skinModel, int index)
        {
            int[] overlayData = new int[2];

            switch (index)
            {
                case 0:
                    overlayData[0] = skinModel.blemishesModel;
                    overlayData[1] = 0;
                    break;
                case 1:
                    overlayData[0] = skinModel.beardModel;
                    overlayData[1] = skinModel.beardColor;
                    break;
                case 2:
                    overlayData[0] = skinModel.eyebrowsModel;
                    overlayData[1] = skinModel.eyebrowsColor;
                    break;
                case 3:
                    overlayData[0] = skinModel.ageingModel;
                    overlayData[1] = 0;
                    break;
                case 4:
                    overlayData[0] = skinModel.makeupModel;
                    overlayData[1] = 0;
                    break;
                case 5:
                    overlayData[0] = skinModel.blushModel;
                    overlayData[1] = skinModel.blushColor;
                    break;
                case 6:
                    overlayData[0] = skinModel.complexionModel;
                    overlayData[1] = 0;
                    break;
                case 7:
                    overlayData[0] = skinModel.sundamageModel;
                    overlayData[1] = 0;
                    break;
                case 8:
                    overlayData[0] = skinModel.lipstickModel;
                    overlayData[1] = skinModel.lipstickColor;
                    break;
                case 9:
                    overlayData[0] = skinModel.frecklesModel;
                    overlayData[1] = 0;
                    break;
                case 10:
                    overlayData[0] = skinModel.chestModel;
                    overlayData[1] = skinModel.chestColor;
                    break;
            }

            return overlayData;
        }

        public static List<ClothesModel> GetPlayerClothes(int playerId)
        {
            // Get a list with the player's clothes
            return clothesList.Where(c => c.player == playerId).ToList();
        }

        public static ClothesModel GetDressedClothesInSlot(int playerId, int type, int slot)
        {
            // Get the clothes in the selected slot
            return clothesList.FirstOrDefault(c => c.player == playerId && c.type == type && c.slot == slot && c.dressed);
        }

        public static List<string> GetClothesNames(List<ClothesModel> clothesList)
        {
            List<string> clothesNames = new List<string>();
            foreach (ClothesModel clothes in clothesList)
            {
                foreach (BusinessClothesModel businessClothes in Constants.BUSINESS_CLOTHES_LIST)
                {
                    if (businessClothes.clothesId == clothes.drawable && businessClothes.bodyPart == clothes.slot && businessClothes.type == clothes.type)
                    {
                        clothesNames.Add(businessClothes.description);
                        break;
                    }
                }
            }
            return clothesNames;
        }

        public static void SetDefaultClothes(Client player)
        {
            // Get the clothes list
            Dictionary<int, ComponentVariation> clothes = new Dictionary<int, ComponentVariation>();
            clothes.Add(Constants.CLOTHES_MASK, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_TORSO, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_LEGS, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_BAGS, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_FEET, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_ACCESSORIES, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_UNDERSHIRT, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_ARMOR, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_DECALS, new ComponentVariation(0, 0));
            clothes.Add(Constants.CLOTHES_TOPS, new ComponentVariation(0, 0));

            // Set the default clothes for the player
            player.SetClothes(clothes);
        }

        public static void UndressClothes(int playerId, int type, int slot)
        {
            foreach (ClothesModel clothes in clothesList)
            {
                if (clothes.player == playerId && clothes.type == type && clothes.slot == slot && clothes.dressed)
                {
                    clothes.dressed = false;

                    Task.Factory.StartNew(() =>
                    {
                        NAPI.Task.Run(() =>
                        {
                            // Update the clothes' state
                            Database.UpdateClothes(clothes);
                        });
                    });

                    break;
                }
            }
        }

        public static bool IsCustomCharacter(Client player)
        {
            return (PedHash)player.Model == PedHash.FreemodeMale01 || (PedHash)player.Model == PedHash.FreemodeFemale01;
        }

        [Command(Commands.COM_COMPLEMENT, Commands.HLP_COMPLEMENT_COMMAND)]
        public void ComplementCommand(Client player, string type, string action)
        {
            ClothesModel clothes = null;
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);

            if (action.ToLower() == Commands.ARG_WEAR || action.ToLower() == Commands.ARG_REMOVE)
            {
                switch (type.ToLower())
                {
                    case Commands.ARG_MASK:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_MASK);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_MASK && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_mask_bought);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.mask_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_mask_equiped);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_MASK, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_MASK);
                            }
                        }
                        break;
                    case Commands.ARG_BAG:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_BAGS);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_BAGS && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_bag_bought);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.bag_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_bag_equiped);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_BAGS, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_BAGS);
                            }
                        }
                        break;
                    case Commands.ARG_ACCESSORY:
                        clothes = GetDressedClothesInSlot(playerId, 0, Constants.CLOTHES_ACCESSORIES);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.CLOTHES_ACCESSORIES && c.type == 0).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_accessory_bought);
                                }
                                else
                                {
                                    player.SetClothes(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.accessory_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_accessory_equiped);
                            }
                            else
                            {
                                player.SetClothes(Constants.CLOTHES_ACCESSORIES, 0, 0);
                                UndressClothes(playerId, 0, Constants.CLOTHES_ACCESSORIES);
                            }
                        }
                        break;
                    case Commands.ARG_HAT:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_HATS);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_HATS && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_hat_bought);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.hat_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_hat_equiped);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_HATS, 57, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_HATS, 8, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_HATS);
                            }
                        }
                        break;
                    case Commands.ARG_GLASSES:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_GLASSES);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_GLASSES && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_glasses_bought);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.glasses_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_glasses_equiped);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_GLASSES, 5, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_GLASSES, 0, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_GLASSES);
                            }
                        }
                        break;
                    case Commands.ARG_EARRINGS:
                        clothes = GetDressedClothesInSlot(playerId, 1, Constants.ACCESSORY_EARS);
                        if (action.ToLower() == Commands.ARG_WEAR)
                        {
                            if (clothes == null)
                            {
                                clothes = GetPlayerClothes(playerId).Where(c => c.slot == Constants.ACCESSORY_EARS && c.type == 1).First();
                                if (clothes == null)
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_ear_bought);
                                }
                                else
                                {
                                    player.SetAccessories(clothes.slot, clothes.drawable, clothes.texture);
                                }
                            }
                            else
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.ear_equiped);
                            }
                        }
                        else
                        {
                            if (clothes == null)
                            {
                                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.no_ear_equiped);
                            }
                            else
                            {
                                if (player.GetData(EntityData.PLAYER_SEX) == Constants.SEX_FEMALE)
                                {
                                    player.SetAccessories(Constants.ACCESSORY_EARS, 12, 0);
                                }
                                else
                                {
                                    player.SetAccessories(Constants.ACCESSORY_EARS, 3, 0);
                                }
                                UndressClothes(playerId, 1, Constants.ACCESSORY_EARS);
                            }
                        }
                        break;
                    default:
                        player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_COMPLEMENT_COMMAND);
                        break;
                }
            }
            else
            {
                player.SendChatMessage(Constants.COLOR_HELP + Commands.HLP_COMPLEMENT_COMMAND);
            }
        }
    }
}
 