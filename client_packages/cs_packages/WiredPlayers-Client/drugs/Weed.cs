using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.drugs
{
    class Weed : Events.Script
    {
        
        private Blip jobLocation = null;
        private Blip joblocationScrapyard = null;

        public Weed()
        {
            Events.Add("showWeedUi", ShowWeedUiEvent);
            CreateWeedAdditions();
        }

        public void CreateWeedAdditions()
        {
            RAGE.Elements.Ped myPed = new RAGE.Elements.Ped(653210662, new Vector3(-581.2188f, -1612.64f, 27.01082f), 0, 0);
            RAGE.Elements.Ped myDealer = new RAGE.Elements.Ped(653210662, new Vector3(1364.366f, -2102.625f, 51.99857f), 0, 0);

        }
        private void ShowWeedUiEvent(object[] args)
        {

            // Create the fastfood menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/WeedSeed.html"});
        }
    }
}
