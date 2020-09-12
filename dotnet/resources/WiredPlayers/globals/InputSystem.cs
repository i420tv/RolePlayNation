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

namespace WiredPlayers.globals
{
    class InputSystem : GTANetworkAPI.Script
    {
        [RemoteEvent("DisableInputSS")]
        public void DisableInput(Client player, object[] args)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "LockInput");
        }
        [RemoteEvent("EnableInputSS")]
        public void EnableInput(Client player, object[] args)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "UnlockInput");
        }
    }
}
