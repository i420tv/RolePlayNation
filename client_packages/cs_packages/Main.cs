using System;
using RAGE;

namespace ClientSide
{
    public class Main : Events.Script
    {
        public Main()
        {
            Events.Add("FreezePlayerClient", FreezePlayer);
        }

        public void FreezePlayer(object [] args)
        {
            RAGE.Elements.Player.LocalPlayer.FreezePosition((bool)args[0]);
        }
    }
}
