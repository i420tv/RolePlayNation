using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.model;
using WiredPlayers.vehicles;
using WiredPlayers.database;
using WiredPlayers.business;
using WiredPlayers.parking;
using WiredPlayers.house;
using WiredPlayers.weapons;
using WiredPlayers.factions;
using WiredPlayers.character;
using WiredPlayers.messages.information;
using WiredPlayers.messages.error;
using WiredPlayers.messages.general;
using WiredPlayers.messages.administration;
using WiredPlayers.messages.success;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace WiredPlayers.model
{
    public class OceanItemModel
    {
        public int itemIndex;
        public string itemName;
        public string itemDesc;
        public uint itemModelId;
        public int itemScrapValue;
        public Blip blip;

        public Vector3 itemPosition;

        public TextLabel itemText;
        public GTANetworkAPI.Object itemObject;

        public bool itemFound;

        // public bool isFound;

    }
}
