using RAGE;
using RAGE.Elements;
using WiredPlayers_Client.globals;
using System;

namespace WiredPlayers_Client.jobs
{
    class OceanCleaner : Events.Script
    {
        
        private Blip jobLocation = null;
        private Blip joblocationScrapyard = null;

        public OceanCleaner()
        {
            jobLocation = new Blip(371, new Vector3(285.238f, -2982.185f, 5.664789f), "Ocean Cleaning", 1, 0, 255, 0, true);
            joblocationScrapyard = new Blip(500, new Vector3(2355.144f, 3133.385f, 48.20871f), "Ocean Cleaning Scrapyard", 1, 0, 255, 0, true);

            Events.Add("showOceanJobUi", ShowOceanJobUiEvent);
            CreateJobLocationAdditions();
        }

        public void CreateJobLocationAdditions()
        {
            RAGE.Elements.Ped myPed = new RAGE.Elements.Ped(349680864, new Vector3(288.0206f, -2981.255f, 5.862742f), 180, 0);
            RAGE.Elements.Ped myPed2 = new RAGE.Elements.Ped(815693290, new Vector3(312.4634f, -2961.938f, 5.99905f), 90, 0);

            RAGE.Elements.Ped scrapYardPed = new RAGE.Elements.Ped(436345731, new Vector3(2340.844f, 3126.445f, 48.20873f), 0, 0);

        }
        private void ShowOceanJobUiEvent(object[] args)
        {

            // Create the fastfood menu
            Browser.CreateBrowserEvent(new object[] { "package://statics/html/OceanJob.html"});
        }
    }
}
