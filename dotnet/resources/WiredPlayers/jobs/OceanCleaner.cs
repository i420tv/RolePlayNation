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

namespace WiredPlayers.jobs
{
    public class OceanCleaner : Script
    {
        #region Varibles
        public List<Vector3> allLocations = new List<Vector3>();
        public List<Vector3> planeWreck = new List<Vector3>();

        public List<string> scrapStories = new List<string>();
        public List<uint> scrapObjectIDs = new List<uint>();

        public List<uint> rareInformationsObjects = new List<uint>();

        public OceanItemModel oceanItem;
        public List<OceanItemModel> allOceanItems = new List<OceanItemModel>();

        private string NPCPeter = Constants.COLOR_YELLOW + "Peter: ";
        private string NPCMarcus = Constants.COLOR_YELLOW + "Marcus: ";
        private string NPCRay = Constants.COLOR_YELLOW + "Ray: ";

        ColShape ladderTrigger = NAPI.ColShape.CreateCylinderColShape(new Vector3(316.223, -2954.949, 5.999993), 1.5f, 1.0f);


        #endregion

        public OceanCleaner()
        {
            CreateJobLocation();
            CreateNPC();

            GenerateObjects();

            AddScrapStories();
            AddScrapObjectIDs();
        }

        #region Ocean Generation Systems

        public void GenerateObjects()
        {
            #region All Coordinates in the Ocean
            allLocations.Add(new Vector3(-197.406f, -2874.562f, -20.17717f));
            allLocations.Add(new Vector3(-96.12424f, -2954.485f, -31.45808f));
            allLocations.Add(new Vector3(47.5423f, -2830.49f, -18.88408f));
            allLocations.Add(new Vector3(-11.06648, -3344.253, -21.419));
            allLocations.Add(new Vector3(391.7362, -3350.827, -23.7604));
            allLocations.Add(new Vector3(380.4103, -3191.509, -20.63347));
            allLocations.Add(new Vector3(577.8287, -3398.531, -27.2825));
            allLocations.Add(new Vector3(773.7352, -3436.287, -23.63369));
            allLocations.Add(new Vector3(863.4482, -3483.535, -20.07652));
            allLocations.Add(new Vector3(895.7641, -3473.618, -25.42162));
            allLocations.Add(new Vector3(899.8591, -3534.702, -42.41016));

            allLocations.Add(new Vector3(1086.55, -3576.857, -48.82178));
            allLocations.Add(new Vector3(1097.326, -3574.143, -51.473));
            allLocations.Add(new Vector3(1287.072, -3480.589, -27.04528));
            allLocations.Add(new Vector3(1399.445, -3378.087, -30.56728));
            allLocations.Add(new Vector3(1354.463, -3288.844, -19.5622));
            allLocations.Add(new Vector3(1395.765, -3252.534, -25.32919));
            allLocations.Add(new Vector3(1462.296, -3112.371, -40.60774));
            allLocations.Add(new Vector3(1329.905, -3168.377, -18.44738));
            allLocations.Add(new Vector3(1268.678, -3023.76, -16.5846));
            allLocations.Add(new Vector3(1232.099, -3030.292, -11.44098));
            allLocations.Add(new Vector3(1222.728, -2842.213, -20.89691));
            allLocations.Add(new Vector3(1033.512, -2812.794, -23.78961));
            allLocations.Add(new Vector3(915.0052, -2831.454, -23.73385));
            allLocations.Add(new Vector3(788.9914, -2807.915, -13.7131));

            allLocations.Add(new Vector3(781.7189, -2672.67, -10.7654));
            allLocations.Add(new Vector3(725.7232, -2584.166, -10.01241));
            allLocations.Add(new Vector3(578.49, -2472.897, -7.957603));
            allLocations.Add(new Vector3(788.9914, -2807.915, -13.7131));
            allLocations.Add(new Vector3(1137.698, -2811.281, -19.6293));
            allLocations.Add(new Vector3(1325.386, -2859.082, -19.19444));
            allLocations.Add(new Vector3(1482.996, -2957.421, -27.30553));

            allLocations.Add(new Vector3(1987.171, -2903.058, -32.36184));
            allLocations.Add(new Vector3(2049.418, -2998.621, -56.56091));
            allLocations.Add(new Vector3(2164.802, -2856.118, -53.56902));
            allLocations.Add(new Vector3(2334.514, -2500.02, -27.67783));
            allLocations.Add(new Vector3(2375.584, -2315.381, -3.586826));
            allLocations.Add(new Vector3(2420.292, -2206.385, -14.67764));

            allLocations.Add(new Vector3(2500.281, -2257.73, -24.12484));
            allLocations.Add(new Vector3(2722.463, -2247.85, -39.39862));
            allLocations.Add(new Vector3(2820.808, -2340.239, -93.96251));
            allLocations.Add(new Vector3(3002.373, -2274.042, -105.0738));
            allLocations.Add(new Vector3(3050.733, -2209.683, -117.274));
            allLocations.Add(new Vector3(3084.555, -2118.004, -137.4067));

            allLocations.Add(new Vector3(3126.77, -2018.653, -120.9392));
            allLocations.Add(new Vector3(3164.129, -1891.034, -140.7883));
            allLocations.Add(new Vector3(3191.564, -1665.125, -143.2021));
            allLocations.Add(new Vector3(3161.498, -1529.14, -128.6227));
            allLocations.Add(new Vector3(3206.642, -1550.536, -79.38869));
            allLocations.Add(new Vector3(3124.832, -1429.691, -69.44806));
            allLocations.Add(new Vector3(3079.508, -1306.483, -36.71144));

            allLocations.Add(new Vector3(3021.929, -1304.223, -28.06494));
            allLocations.Add(new Vector3(2961.572, -1211.708, -22.54645));
            allLocations.Add(new Vector3(2788.006, -1160.415, -33.48037));
            allLocations.Add(new Vector3(2772.653, -1037.977, -15.90856));
            allLocations.Add(new Vector3(2902.795, -945.9814, -29.50091));
            allLocations.Add(new Vector3(3072.448, -786.8539, -8.341012));
            allLocations.Add(new Vector3(3073.528, -708.8233, -40.78448));
            allLocations.Add(new Vector3(3218.269, -527.4092, -107.1856));
            allLocations.Add(new Vector3(3191.303, -358.7619, -30.43814));
            allLocations.Add(new Vector3(3200.663, -384.4023, -26.82294));
            allLocations.Add(new Vector3(3155.023, -321.4277, -26.56757));
            allLocations.Add(new Vector3(3150.738, -292.7363, -24.41722));

            allLocations.Add(new Vector3(3156.543, -258.2168, -22.2284));
            allLocations.Add(new Vector3(3477.38, -216.8029, -34.01741));
            allLocations.Add(new Vector3(3684.125, -135.2915, -90.15111));
            allLocations.Add(new Vector3(3724.596, -70.51882, -95.59338));
            allLocations.Add(new Vector3(3570.207, 92.12326, -37.85585));
            allLocations.Add(new Vector3(3541.947, 212.675, -34.88544));

            allLocations.Add(new Vector3(3640.692, 309.8781, -61.32095));

            int coordsAmount = allLocations.Capacity;

            NAPI.Util.ConsoleOutput(coordsAmount.ToString());










            #endregion

            #region Plane Crash in Ocean

            planeWreck.Add(new Vector3(1754.578, -2990.057, -52.74268));
            planeWreck.Add(new Vector3(1814.195, -2956.95, -43.92405));
            planeWreck.Add(new Vector3(1830.879, -2971.892, -51.30523));
            planeWreck.Add(new Vector3(1862.498, -2938.683, -44.04823));
            planeWreck.Add(new Vector3(1826.038, -2918.893, -35.34026));

            #endregion
        }

        public void AddScrapStories()
        {
            string sf = "~m~Scrap:~n~ ";

            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~g~Car Door");
            scrapStories.Add(sf + "~y~Sunken Broken Canoe");
            scrapStories.Add(sf + "~o~Unused Cable Wheel");
            scrapStories.Add(sf + "~y~Rusty Steel Pipe");
            scrapStories.Add(sf + "~o~Rusty Steel Pipes~w~");

        }
        public void AddScrapObjectIDs()
        {
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);
            scrapObjectIDs.Add(674546851);
            scrapObjectIDs.Add(4090125259);
            scrapObjectIDs.Add(996225620);
            scrapObjectIDs.Add(3039590774);
            scrapObjectIDs.Add(2971578861);
            scrapObjectIDs.Add(568297919);

            ScrapObjectsInit();
        }

        public void ScrapObjectsInit()
        {
            AddScrapItemsAndScrapTitles();
        }

        public void AddScrapItemsAndScrapTitles()
        {
            NAPI.Util.ConsoleOutput("~b~Ocean Job:~w~ All Locations Added (Vector 3");

            for (int p = 0; p < scrapObjectIDs.Count; p++)
            {
                NAPI.Util.ConsoleOutput(p.ToString());

                List<Vector3> storiesPos = new List<Vector3>();
                storiesPos = allLocations;

                foreach (Vector3 storyPos in storiesPos)
                {

                }

                OceanItemModel oceanItem = new OceanItemModel();

                oceanItem.itemIndex = p;
                oceanItem.itemName = scrapStories[p];
                oceanItem.itemPosition = allLocations[p];
                oceanItem.itemModelId = scrapObjectIDs[p];
                oceanItem.itemObject = NAPI.Object.CreateObject(scrapObjectIDs[p], oceanItem.itemPosition, new Vector3(0, 0, 0));

                Vector3 orgPos = oceanItem.itemObject.Position;
                Vector3 localPos = orgPos;

                localPos.Z = localPos.Z + 2;

                oceanItem.itemText = NAPI.TextLabel.CreateTextLabel(scrapStories[p], localPos, 40, 2, 4, new Color(255, 255, 255), true, 0);

                allOceanItems.Add(oceanItem);

                NAPI.Util.ConsoleOutput("Added New Object to LIST " + allOceanItems.Count);
            }
        }

        #endregion

        #region Main Functions (Job Location & NPCs)
        public void CreateJobLocation()
        {
            Vector3 jobPos = new Vector3(312.4634f, -2961.938f, 6.052742f);
            Vector3 jobPosDesc = jobPos;

            NAPI.TextLabel.CreateTextLabel("~w~Type ~y~/getsub~w~ to get your sub and ~y~/returnsub~w~ to return your sub.", jobPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~r~Recover Price: ~w~$50", new Vector3(312.4634f, -2961.938f, 5.752742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        public void CreateNPC()
        {
            Vector3 npcPos = new Vector3(288.0206f, -2981.255f, 7.062742f);
            Vector3 npcDockPos = new Vector3(312.4634, -2961.938, 7.112742f);
            Vector3 npcScrapPos = new Vector3(2340.844f, 3126.445f, 49.3f);

            NAPI.TextLabel.CreateTextLabel("~r~Operations Manager", npcPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Marcus", new Vector3(288.0206f, -2981.255f, 6.862742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~b~/duty~w~ to go on duty.", new Vector3(288.0206f, -2981.255f, 6.242742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/info~w~ for more information on this job", new Vector3(288.0206f, -2981.255f, 6.042742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~g~/join~w~ to become a part of the ~b~Ocean Cleaning Department~w~ of Los Santos.", new Vector3(288.0206f, -2981.255f, 5.842742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~w~Use ~r~/quitjob~w~ to leave the company.", new Vector3(288.0206f, -2981.255f, 5.642742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name


            NAPI.TextLabel.CreateTextLabel("~r~Logistics Operator", npcDockPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Peter", new Vector3(312.4634f, -2961.938f, 6.962742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/recoversub  ~w~ to get your vehicle retrieved back by logistics.", new Vector3(312.4634f, -2961.938f, 5.902742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/returntruck ~w~to park the truck.", new Vector3(309.6263f, -2959.853f, 5.902742f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~r~Scrapyard Manager", npcScrapPos, 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name
            NAPI.TextLabel.CreateTextLabel("~y~ Ray", new Vector3(2340.844f, 3126.445f, 49.14073f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            NAPI.TextLabel.CreateTextLabel("~w~Use ~y~/sellscrap ~w~to sell the scrap in your truck.", new Vector3(2340.844f, 3126.445f, 48.40873f), 6.0f, 2.0f, 4, new Color(255, 255, 255), false, 0); // Freelancer Job Name

            //NAPI.Marker.CreateMarker(MarkerType.VerticalCylinder, new Vector3(285.238, -2982.185, 5.564789), new Vector3(), new Vector3(), 2.5f, new Color(198, 40, 40, 200));

            //NAPI.Ped.CreatePed(PedHash.Dockwork01SMY, new Vector3(285.238, -2982.185, 5.584789), 0);
        }

        public void JobStarted(Client player)
        {
            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "Your vehicle is ready and fueled.");

            CreateNewVehicle(player);

            //NAPI.Player.SetPlayerClothes(player, 3, 289, 0);          

            NAPI.Chat.SendChatMessageToPlayer(player, NPCPeter + Constants.COLOR_WHITE + "Your vest and oxygen tank is in the Submersibal, stay safe and see you soon!");

            //EquipJobUniformWithoutTank(player);
        }

        public void EquipJobUniformWithoutTank(Client player)
        {
            var clothDictionary = new Dictionary<int, ComponentVariation>();

            if (player.GetData(EntityData.PLAYER_SEX) == 0)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 }); //Mask
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 }); // Legs
                clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 }); // Shoes
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask

                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });

                NAPI.Player.SetPlayerAccessory(player, 0, 8, 0);

            }
            if (player.GetData(EntityData.PLAYER_SEX) == 1)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 });
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask
                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });

                NAPI.Player.SetPlayerAccessory(player, 0, 57, 0);

            }

            NAPI.Player.SetPlayerClothes(player, clothDictionary);
        }

        public void EquipJobUniformWithTank(Client player)
        {
            var clothDictionary = new Dictionary<int, ComponentVariation>();

            if (player.GetData(EntityData.PLAYER_SEX) == 0)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                clothDictionary.Add(6, new ComponentVariation { Drawable = 67, Texture = 0 });
                clothDictionary.Add(7, new ComponentVariation { Drawable = 33, Texture = 0 });
                clothDictionary.Add(8, new ComponentVariation { Drawable = 123, Texture = 0 });

                clothDictionary.Add(11, new ComponentVariation { Drawable = 53, Texture = 0 });
            }
            if (player.GetData(EntityData.PLAYER_SEX) == 1)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 38, Texture = 0 });
                clothDictionary.Add(4, new ComponentVariation { Drawable = 94, Texture = 0 });
                clothDictionary.Add(6, new ComponentVariation { Drawable = 70, Texture = 0 });
                //Add Oxygen Tank for Female
                clothDictionary.Add(8, new ComponentVariation { Drawable = 153, Texture = 0 });

                clothDictionary.Add(11, new ComponentVariation { Drawable = 46, Texture = 0 });
            }
            NAPI.Player.SetPlayerClothes(player, clothDictionary);
        }

        public void EquipTruckUniform(Client player)
        {
            var clothDictionary = new Dictionary<int, ComponentVariation>();

            if (player.GetData(EntityData.PLAYER_SEX) == 0)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 }); //Mask
                clothDictionary.Add(4, new ComponentVariation { Drawable = 46, Texture = 0 }); // Legs
                clothDictionary.Add(6, new ComponentVariation { Drawable = 62, Texture = 0 }); // Shoes
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask

                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 56, Texture = 0 });
                NAPI.Player.SetPlayerAccessory(player, 0, 145, 0);
            }
            if (player.GetData(EntityData.PLAYER_SEX) == 1)
            {
                clothDictionary.Add(1, new ComponentVariation { Drawable = 0, Texture = 0 });
                clothDictionary.Add(4, new ComponentVariation { Drawable = 48, Texture = 0 });
                clothDictionary.Add(6, new ComponentVariation { Drawable = 65, Texture = 0 });
                clothDictionary.Add(7, new ComponentVariation { Drawable = 0, Texture = 0 }); // No Pipe for Mask
                clothDictionary.Add(8, new ComponentVariation { Drawable = 57, Texture = 0 }); // Undershirt

                clothDictionary.Add(11, new ComponentVariation { Drawable = 88, Texture = 0 });
                NAPI.Player.SetPlayerAccessory(player, 0, 144, 0);


            }

            NAPI.Player.SetPlayerClothes(player, clothDictionary);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnPlayerEnterColshape(ColShape trigger, Client player)
        {
            trigger = ladderTrigger;

            if (player.GetData(EntityData.PLAYER_JOB) != 11)
                return;

            if (player.GetData(EntityData.PLAYER_ON_DUTY) != 1)
                return;

            EquipJobUniformWithoutTank(player);
        }

        #endregion

        #region Submarine Commands ONLY

        [Command("getsub", GreedyArg = true)]
        public void Command_Job_OceanCleaner_RentSub(Client player)
        {
            int playerJob = player.GetData(EntityData.PLAYER_JOB);
            int playerDuty = player.GetData(EntityData.PLAYER_ON_DUTY);

            Vector3 triggerPosition = new Vector3(312.4634f, -2961.938f, 6.962742f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {

            }
            else
            {

                if (playerDuty == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You need to let Marcus know you're working today first..");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName)
                    {
                        if (v.model == (uint)VehicleHash.Scrap)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "We already have a truck out for you, go deliver the scrap to the yard in Sandy Shores.");
                            return;
                        }

                        if (v.model == (uint)(VehicleHash.Submersible2))
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "We already have a vehicle in the system registered to you.");
                            return;
                        }

                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                        if (veh != null)
                        {
                            List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                            foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                                {
                                    if (ov.scrap > 0)
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Seems there are still things in your sub, go sell that first.");
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
                JobStarted(player);
            }
        }




        [Command("returnsub", GreedyArg = true)]
        public void Command_Job_OceanCleaner_EndWork(Client player)
        {
            Vector3 triggerPosition = new Vector3(312.4634f, -2961.938f, 6.962742f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not at the Ocean Cleaning Logistics dock");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_JOB) != 11)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You don't work for us, go ask if Marcus needs more guys..");
                    return;

                }
                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You should probably Let Marcus know first, that you are worknig today.");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Submersible2))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);
                        int vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                        veh.GetData(EntityData.VEHICLE_PRICE);

                        if (v.position.DistanceTo(player.Position) > 100)
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You need to bring your vehicle here to allow us to transfer the scrap to the truck.");
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Mhm.. I could probably recover the vehicle for you. " + Constants.COLOR_YELLOW + "/recoversub");
                            return;
                        }

                        if (veh != null)
                        {
                            foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                                {
                                    string owner = ov.owner;

                                    if (ov != null)
                                    {
                                        if (ov.scrap != 0)
                                        {
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Looks like you managed to get " + Constants.COLOR_YELLOW + ov.scrap + "x" + Constants.COLOR_WHITE + " scrap.");
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You'll find the scrap you found stored in the truck ready to take to the scrapyard in " + Constants.COLOR_SANDYORANGE + "Sandy Shores" + Constants.COLOR_WHITE + ".");

                                            CreateNewVehicleTruck(player, ov.scrap, ov.scrapValue);

                                            /// TRANSFER RESOURCES TO TRUCK - DATABASE STUFF
                                            /// 
                                            veh.Delete();
                                            Database.RemoveVehicle(vehicleId);
                                            Database.RemoveOceanVehicle(player);
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Thanks for returning the vehicle.");
                                            EquipTruckUniform(player);
                                            return;
                                        }

                                        if (ov.scrap == 0)
                                        {
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Didn't manage to find anything? Better luck next time.");
                                            veh.Delete();
                                            Database.RemoveVehicle(vehicleId);
                                            Database.RemoveOceanVehicle(player);
                                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Thanks for returning the vehicle.");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Mhm, it seems we were unable to find a vehicle by your name on the system.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Mhm, it seems we were unable to find a vehicle by your name on the system.");
                        }
                    }
                }
            }
        }

        [Command("recoversub", GreedyArg = true)]
        public void Command_Job_OceanCleaner_BringSub(Client player)
        {
            Vector3 triggerPosition = new Vector3(312.4634f, -2961.938f, 6.662742f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at the Ocean Cleaning Logistics dock.");
                return;
            }

            if (player.GetData(EntityData.PLAYER_JOB) != 11)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not part of this company.");
                return;
            }

            if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You're not on duty, go let ~y~Marcus~w~ know first.");
            }

            Vehicle veh = null;
            string playerName = player.GetData(EntityData.PLAYER_NAME);

            List<VehicleModel> vehicles = Database.LoadAllVehicles();

            foreach (VehicleModel v in vehicles)
            {
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Submersible2))
                {
                    int id = v.id;
                    veh = Vehicles.GetVehicleById(id);
                    int vehicleId = veh.GetData(EntityData.VEHICLE_ID);

                    if (veh != null)
                    {
                        veh.Position = new Vector3(317.3724, -2965.611, -0.1664049);
                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "We managed to find your vehicle, try not to lose it again.");
                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "Penalty: " + Constants.COLOR_WHITE + "You've been charged $50 which has been deducted from your account.");

                        int bankBalancePenalty = player.GetSharedData(EntityData.PLAYER_BANK) - 50;

                        player.SetSharedData(EntityData.PLAYER_BANK, bankBalancePenalty);
                    }
                }
                if (v.owner == playerName && v.model == (uint)(VehicleHash.Submersible2) == null)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You don't have a vehicle yet, there's nothing for me to find..");
                }
            }
        }

        #endregion

        #region Submarine Collecting & Inventory Commands ONLY
        [Command("collect", GreedyArg = true)]
        public void Command_Job_OceanCleaner_Collect(Client player)
        {
            foreach (OceanItemModel oceanItem in allOceanItems)
            {
                if (oceanItem.itemPosition.DistanceTo(player.Position) < 10)
                {
                    if (oceanItem.itemFound)
                        return;

                    oceanItem.itemFound = true;
                    NAPI.Chat.SendChatMessageToPlayer(player, oceanItem.itemName);

                    oceanItem.itemText.Delete();
                    oceanItem.itemObject.Delete();

                    #region Scrap Object Prices

                    if (oceanItem.itemIndex == 0) // Car Door
                    {
                        oceanItem.itemScrapValue = 20;
                    }
                    if (oceanItem.itemIndex == 1) // Car Door
                    {
                        oceanItem.itemScrapValue = 23;
                    }
                    if (oceanItem.itemIndex == 2) // Broken Canoe
                    {
                        oceanItem.itemScrapValue = 42;
                    }
                    if (oceanItem.itemIndex == 3) // Cable Wheel
                    {
                        oceanItem.itemScrapValue = 125;
                    }
                    if (oceanItem.itemIndex == 4) // Waste Water Tanl
                    {
                        oceanItem.itemScrapValue = 70;
                    }
                    if (oceanItem.itemIndex == 5) // Rusty Steel Pipe
                    {
                        oceanItem.itemScrapValue = 40;
                    }
                    if (oceanItem.itemIndex == 6) // Rusty Steel Pipes
                    {
                        oceanItem.itemScrapValue = 230;
                    }

                    #endregion

                    List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                    foreach (OceanVehicleModel ov in oceanVehicles)
                    {
                        if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                        {
                            ov.scrap = ov.scrap + 1;
                            ov.scrapValue = ov.scrapValue + oceanItem.itemScrapValue;

                            Database.SaveOceanVehicle(ov);

                            //allOceanItems.Remove(oceanItem);
                            //allOceanItems.RemoveAll(item => item == null);
                        }
                    }
                }
            }
        }

        [Command("checksub", GreedyArg = true)]
        public void Command_Job_OceanCleaner_SubInventory(Client player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not in a vehicle.");
                return;
            }

            if (player.IsInVehicle && player.Vehicle.Model != (uint)VehicleHash.Submersible2)
            {
                player.SendChatMessage(Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not in a Submersible.");
                return;
            }
            else if (player.IsInVehicle && player.Vehicle.Model == (uint)VehicleHash.Submersible2)
            {
                int vehicleId = 0;
                Vehicle veh = null;
                VehicleModel vehicle = new VehicleModel();

                veh = Vehicles.GetClosestVehicle(player);

                vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                string owner = veh.GetData(EntityData.VEHICLE_OWNER);

                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (OceanVehicleModel ov in oceanVehicles)
                {
                    if (ov.owner == owner)
                    {
                        player.SendChatMessage(Constants.COLOR_YELLOW + "VEHICLE INVENTORY: " + Constants.COLOR_WHITE + ov.scrap + "x Scrap");                      
                    }
                }
            }
        }

        #endregion

        #region Truck Selling & Inventory Commands ONLY

        [Command("returntruck", GreedyArg = true)]
        public void Command_Job_OceanCleaner_EndTruck(Client player)
        {
            Vector3 triggerPosition = new Vector3(309.6263f, -2959.853f, 5.999989f);

            if (triggerPosition.DistanceTo(player.Position) > 10.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at the Ocean Logistics dock.");
                return;
            }
            else
            {
                if (player.GetData(EntityData.PLAYER_JOB) != 11)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You don't work here, go to Marcus if you are interested in working here.");
                    return;
                }
                if (player.GetData(EntityData.PLAYER_ON_DUTY) == 0)
                {
                    NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You're not on duty.");
                    return;
                }

                Vehicle veh = null;
                string playerName = player.GetData(EntityData.PLAYER_NAME);

                List<VehicleModel> vehicles = Database.LoadAllVehicles();
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (VehicleModel v in vehicles)
                {
                    if (v.owner == playerName && v.model == (uint)(VehicleHash.Scrap))
                    {
                        int id = v.id;
                        veh = Vehicles.GetVehicleById(id);

                        NAPI.Util.ConsoleOutput("READ 1");

                        if (veh != null)
                        {
                            NAPI.Util.ConsoleOutput("READ 2");

                            foreach (OceanVehicleModel ov in oceanVehicles)
                            {
                                if (ov.owner == playerName)
                                {
                                    NAPI.Util.ConsoleOutput("READ 3");

                                    if (ov.scrap == 0)
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "Thanks for returning the vehicle.");
                                        EquipJobUniformWithoutTank(player);
                                        veh.Delete();
                                        Database.RemoveVehicle(id);
                                        Database.RemoveOceanVehicle(player);
                                        return;
                                    }
                                    else
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "There is still scrap in the truck, go and sell it first.");
                                    }

                                    if (ov.model == (uint)VehicleHash.Submersible2)
                                    {
                                        NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_YELLOW + "Peter: " + Constants.COLOR_WHITE + "You don't have a truck, but you do have a vehicle out in the ocean?.");
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        [Command("checktruck", GreedyArg = true)]
        public void Command_Job_OceanCleaner_TruckInventory(Client player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage(Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not in a vehicle.");
                return;
            }

            if (player.IsInVehicle && player.Vehicle.Model != (uint)VehicleHash.Scrap)
            {
                player.SendChatMessage(Constants.COLOR_YELLOW + "INFO: " + Constants.COLOR_WHITE + "You are not in a Scrap truck.");
                return;
            }
            else if (player.IsInVehicle && player.Vehicle.Model == (uint)VehicleHash.Scrap)
            {
                int vehicleId = 0;
                Vehicle veh = null;
                VehicleModel vehicle = new VehicleModel();

                veh = Vehicles.GetClosestVehicle(player);

                vehicleId = veh.GetData(EntityData.VEHICLE_ID);
                string owner = veh.GetData(EntityData.VEHICLE_OWNER);

                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (OceanVehicleModel ov in oceanVehicles)
                {
                    if (ov.owner == owner)
                    {
                        player.SendChatMessage(Constants.COLOR_YELLOW + "VEHICLE INVENTORY: " + Constants.COLOR_WHITE + "The vehicle contains " + ov.scrap + " amounts of scrap.");
                    }
                }
            }
        }

        [Command("sf")]
        public void Command_Job_OceanCleaner_DebugScrap(Client player, int amount, int sValue)
        {
            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) == 4)
            {
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (OceanVehicleModel ov in oceanVehicles)
                {
                    if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                    {

                        ov.scrap = amount;
                        ov.scrapValue = sValue;

                        Database.SaveOceanVehicle(ov);
                    }
                }
            }
        }
        [Command("sellscrap", GreedyArg = true)]
        public void Command_Job_OceanCleaner_SellScrap(Client player)
        {
            Vector3 triggerPosition = new Vector3(2340.844f, 3126.445f, 48.20873f);

            if (triggerPosition.DistanceTo(player.Position) > 1.75f)
            {
                NAPI.Chat.SendChatMessageToPlayer(player, Constants.COLOR_ERROR + "You are not at the Scrapyard.");
                return;
            }
            else
            {
                List<OceanVehicleModel> oceanVehicles = Database.LoadOceanVehicles();

                foreach (OceanVehicleModel ov in oceanVehicles)
                {
                    if (ov.owner == player.GetData(EntityData.PLAYER_NAME))
                    {
                        if (ov.scrap == 0)
                        {
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Uhm.. There's nothing in the truck bud, haha.");
                            return;
                        }
                        else if (ov.scrap > 0 && ov.scrap < 6)
                        {
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Seems like a good amount, nice one!");
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Here is " + Constants.COLOR_DOLLARGREEN + "$" + ov.scrapValue + Constants.COLOR_WHITE + ". See you next time!");

                        }
                        else if (ov.scrap > 5 && ov.scrap < 10)
                        {
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Nice, that's quite a lot you brought!");
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Here is " + Constants.COLOR_DOLLARGREEN + "$" + ov.scrapValue + Constants.COLOR_WHITE + ". Hope to see you again!");

                        }
                        else if (ov.scrap > 10)
                        {
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Wow, great job! You have a lot!");
                            player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Here is " + Constants.COLOR_DOLLARGREEN + "$" + ov.scrapValue + Constants.COLOR_WHITE + ". Be seeing you again buddy! Keep up the good work.");

                        }

                        player.SendChatMessage(Constants.COLOR_SANDYORANGE + "Ray: " + Constants.COLOR_WHITE + "Make sure to deposit the money, there's a lot of scumbags around in this city.");

                        int currentMoney = player.GetSharedData(EntityData.PLAYER_MONEY) + ov.scrapValue;

                        player.SetSharedData(EntityData.PLAYER_MONEY, currentMoney);

                        ov.scrap = 0;
                        ov.scrapValue = 0;

                        Database.SaveOceanVehicle(ov);
                    }
                }
            }
        }
        #endregion

        #region Vehicle Creation/Player Systems
        public void CreateNewVehicle(Client player)
        {
            VehicleModel vehicle = new VehicleModel();

            vehicle.id = 0;
            vehicle.model = (uint)VehicleHash.Submersible2;
            vehicle.faction = Constants.FACTION_NONE;
            vehicle.position = new Vector3(317.3724, -2965.611, -0.1664049);
            vehicle.rotation = new Quaternion(0, 0, 0, 0);
            vehicle.dimension = player.Dimension;
            vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
            vehicle.firstColor = "0,0,0";
            vehicle.secondColor = "0,0,0";
            vehicle.pearlescent = 0;
            vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
            vehicle.plate = string.Empty;
            vehicle.price = 0;
            vehicle.parking = 0;
            vehicle.parked = 0;
            vehicle.gas = 50.0f;
            vehicle.kms = 0.0f;

            int scrap = 0;
            int scrapValue = 0;

            Vehicles.CreateVehicle(player, vehicle, false);
            Database.AddNewOceanVehicle(vehicle, scrap, vehicle.model, scrapValue);
        }

        public void CreateNewVehicleTruck(Client player, int scrap, int scrapValue)
        {
            VehicleModel vehicle = new VehicleModel();

            vehicle.id = 0;
            vehicle.model = (uint)VehicleHash.Scrap;
            vehicle.faction = Constants.FACTION_NONE;
            vehicle.position = new Vector3(304.9536f, -2971.744f, 5.972999f);
            vehicle.rotation = new Quaternion(0, 0, 0, 0);
            vehicle.dimension = player.Dimension;
            vehicle.colorType = Constants.VEHICLE_COLOR_TYPE_CUSTOM;
            vehicle.firstColor = "0,0,0";
            vehicle.secondColor = "0,0,0";
            vehicle.pearlescent = 0;
            vehicle.owner = player.GetData(EntityData.PLAYER_NAME);
            vehicle.plate = string.Empty;
            vehicle.price = 0;
            vehicle.parking = 0;
            vehicle.parked = 0;
            vehicle.gas = 50.0f;
            vehicle.kms = 0.0f;

            Vehicles.CreateVehicle(player, vehicle, false);
            Database.AddNewOceanVehicle(vehicle, scrap, vehicle.model, scrapValue);
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void OnPlayerEnterVehicle(Client player, Vehicle vehicleLocal, sbyte seat)
        {
            if (vehicleLocal.DisplayName == "SUBMERS2")
            {
                if (player.GetData(EntityData.PLAYER_JOB) != 11)
                {
                    player.SendChatMessage(Constants.COLOR_ERROR + "You do not have the keys for this.");
                    player.Vehicle.EngineStatus = false;
                    return;
                }
            }

            if (vehicleLocal.DisplayName == "SUBMERS2")
            {
                if (player.GetData(EntityData.PLAYER_JOB) == 11 && player.GetData(EntityData.PLAYER_ON_DUTY) == 1)
                {
                    player.SendNotification("You put on your mask, vest and oxygen tank.");

                    EquipJobUniformWithTank(player);
                }
            }
        }
        #endregion
    }
}
