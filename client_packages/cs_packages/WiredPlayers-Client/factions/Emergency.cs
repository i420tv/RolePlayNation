using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.factions
{
    class Emergency : Events.Script
    {
        public static bool dead;

        public Emergency()
        {
            Events.Add("togglePlayerDead", TogglePlayerDeadEvent);

            Events.OnEntityStreamIn += OnEntityStreamIn;
        }

        private void TogglePlayerDeadEvent(object[] args)
        {
            // Change dead state
            dead = Convert.ToBoolean(args[0]);

            // Check if the player should be in God mode
            Player.LocalPlayer.SetInvincible(dead);
        }

        private void OnEntityStreamIn(Entity entity)
        {
            if(entity.Type == RAGE.Elements.Type.Player && (int)entity.GetSharedData(Constants.PLAYER_KILLED_STATE) != 0)
            {
                // Make the player immortal
                ((Player)entity).SetInvincible(true);
            }
        }
    }
}
