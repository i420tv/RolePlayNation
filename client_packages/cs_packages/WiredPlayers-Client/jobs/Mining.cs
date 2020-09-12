using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.jobs
{

    class Mining : Events.Script
    {
        
        private Blip Factory = null;
        private Blip joblocationMines = null;

        public Mining()
        {
            //Factory = new Blip(0, new Vector3(1112.388f, -2005.329f, 35.4394f), "Factory", 1, 0, 255, 0, true);
           // joblocationMines = new Blip(500, new Vector3(-593.95197, 2078.6904, 130.57394f), "Mine", 1, 0, 255, 0, true);

            Events.Add("showSmeltingUi", ShowSmeltingUiEvent);
            CreateJobLocationAdditions();
        }

        public void CreateJobLocationAdditions()
        {
            RAGE.Elements.Ped myPed = new RAGE.Elements.Ped(349680864, new Vector3(1112.388f, -2005.329f, 35.4394f), 180, 0);
            RAGE.Elements.Ped myPed2 = new RAGE.Elements.Ped(815693290, new Vector3(1089.563f, -2001.63f, 30.87784f), 90, 0);
        }
        private void ShowSmeltingUiEvent(object[] args)
        {

            // Create the fastfood menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/Smelting.html"});
        }
    }
}
