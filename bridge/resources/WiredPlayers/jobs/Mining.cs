using GTANetworkAPI;
using WiredPlayers.globals;
using WiredPlayers.character;
using WiredPlayers.model;
using Progressbar;
using WiredPlayers.database;
using WiredPlayers.messages.information;
using WiredPlayers.messages.error;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;


namespace WiredPlayers.jobs
{
    public class Mining : Script
    {
        public static List<PlayerModel> playerList;
        public static Dictionary<int, ItemModel> ItemCollection;
        public static Dictionary<int, Timer> miningTimerList;
        public static Dictionary<int, Timer> SmeltingTimerList;
        public List<Vector3> CopperCoords = new List<Vector3>();
        public List<Vector3> IronCoords = new List<Vector3>();
        public List<Vector3> LeadCoords = new List<Vector3>();
        public List<Vector3> TinCoords = new List<Vector3>();
        public List<Vector3> GoldCoords = new List<Vector3>();
        public List<Vector3> SodiumCoords = new List<Vector3>();
        public static List<OreModel> CopperList = new List<OreModel>();
        public static List<OreModel> IronList = new List<OreModel>();
        public static List<OreModel> LeadList = new List<OreModel>();
        public static List<OreModel> TinList = new List<OreModel>();
        public static List<OreModel> GoldList = new List<OreModel>();
        public static List<OreModel> SodiumList = new List<OreModel>();
        public static List<OreModel> allminingItems = new List<OreModel>();
        public Mining()
        {
            // Initialize the variables
            miningTimerList = new Dictionary<int, Timer>();
            NAPI.World.DeleteWorldProp(-18398025, new Vector3(-596.30225, 2088.9402, 131.41283), (15.0f));
            NAPI.World.DeleteWorldProp(-872784146, new Vector3(-596.30225, 2088.9402, 131.41283), (15.0f));
            NAPI.World.DeleteWorldProp(-1241212535, new Vector3(-596.30225, 2088.9402, 131.41283), (15.0f));
            GenerateCopperCoords();
            GenerateIronCoords();
            GenerateGoldCoords();
            GenerateLeadCoords();
            GenerateSodiumCoords();
            GenerateTinCoords();
        }
        #region Object Creation
        public void GenerateCopperCoords()
        {
            CopperCoords.Add(new Vector3(-593.95197, 2078.6904, 130.57394));
            CopperCoords.Add(new Vector3(-591.6027, 2075.5366, 130.48064));
            CopperCoords.Add(new Vector3(-590.6442, 2064.8406, 130.20831));
            CopperCoords.Add(new Vector3(-587.8685, 2060.606, 129.75726));
            CopperCoords.Add(new Vector3(-589.2712, 2052.7168, 129.17943));
            CopperCoords.Add(new Vector3(-585.1507, 2046.242, 130.0875));
            CopperCoords.Add(new Vector3(-584.5333, 2038.907, 129.145));
            CopperCoords.Add(new Vector3(-577.2247, 2032.859, 128.3855));
            CopperCoords.Add(new Vector3(-575.8845, 2026.687, 128.1181));
            CopperCoords.Add(new Vector3(-567.9684, 2021.757, 127.6449));
            CopperCoords.Add(new Vector3(-564.9797, 2015.08, 127.3812));
            CopperCoords.Add(new Vector3(-559.0182, 2007.495, 127.1938));
            CopperCoords.Add(new Vector3(-550.9442, 1998.582, 127.0662));
            CopperCoords.Add(new Vector3(-549.7714, 1992.081, 127.0293));
            CopperCoords.Add(new Vector3(-544.5245, 1988.185, 127.0208));
            CopperCoords.Add(new Vector3(-546.1243, 1983.032, 127.0993));
            CopperCoords.Add(new Vector3(-489.0492, 1986.026, 124.4413));
            CopperCoords.Add(new Vector3(-474.8775, 1990.75, 123.7687));
            CopperCoords.Add(new Vector3(-449.9116, 2031.628, 123.4848));
            CopperCoords.Add(new Vector3(-454.6979, 2038.662, 122.9403));
            CopperCoords.Add(new Vector3(-467.5643, 2073.158, 120.7393));
            CopperCoords.Add(new Vector3(2935.37, 2742.787, 43.99229));
            CopperCoords.Add(new Vector3(2936.408, 2755.657, 44.03392));
            CopperCoords.Add(new Vector3(2948.207, 2729.553, 46.32852));
            CopperCoords.Add(new Vector3(2974.086, 2739.717, 44.27745));
            CopperCoords.Add(new Vector3(2977.126, 2745.804, 43.72846));
            CopperCoords.Add(new Vector3(2991.997, 2751.028, 43.59391));
            CopperCoords.Add(new Vector3(3000.88, 2756.057, 43.71354));
            CopperCoords.Add(new Vector3(3005.6, 2768.712, 42.9519));
            CopperCoords.Add(new Vector3(3004.882, 2783.051, 44.61819));
            CopperCoords.Add(new Vector3(2989.804, 2777.931, 43.16215));
            CopperCoords.Add(new Vector3(2981.365, 2784.038, 39.94598));
            CopperCoords.Add(new Vector3(2980.472, 2789.373, 40.67949));


            GenerateOre();
        }
        public void GenerateIronCoords()
        {
            IronCoords.Add(new Vector3(-536.5048, 1980.177, 127.1066));
            IronCoords.Add(new Vector3(-527.9396, 1981.272, 126.8919));
            IronCoords.Add(new Vector3(-519.8535, 1977.967, 126.5759));
            IronCoords.Add(new Vector3(-511.0958, 1977.526, 126.4923));
            IronCoords.Add(new Vector3(-502.8426, 1980.972, 125.9305));
            IronCoords.Add(new Vector3(-481.4002, 1985.859, 124.2487));
            IronCoords.Add(new Vector3(-492.7108, 1981.635, 125.0135));
            IronCoords.Add(new Vector3(-464.6432, 1993.337, 123.7291));
            IronCoords.Add(new Vector3(-455.2367, 2002.466, 123.7007));
            IronCoords.Add(new Vector3(-451.1765, 2012.283, 123.5454));
            IronCoords.Add(new Vector3(-442.9424, 2014.099, 123.5454));
            IronCoords.Add(new Vector3(-450.85, 2022.599, 123.5066));
            IronCoords.Add(new Vector3(-454.8287, 2045.846, 122.8194));
            IronCoords.Add(new Vector3(-461.1902, 2057.895, 121.7148));
            IronCoords.Add(new Vector3(-467.8261, 2065.334, 120.7834));
            IronCoords.Add(new Vector3(-472.2589, 2081.1, 120.2112));
            IronCoords.Add(new Vector3(-470.6651, 2087.01, 120.2203));
            IronCoords.Add(new Vector3(-452.2715, 2053.786, 122.3838));
            IronCoords.Add(new Vector3(-435.8806, 2060.518, 121.3995));
            IronCoords.Add(new Vector3(2973.681, 2774.544, 38.16021));
            IronCoords.Add(new Vector3(2957.549, 2774.619, 39.97732));
            IronCoords.Add(new Vector3(2952.259, 2770.245, 39.04266));
            IronCoords.Add(new Vector3(2946.766, 2767.269, 39.11076));
            IronCoords.Add(new Vector3(2935.596, 2773.353, 38.93176));
            IronCoords.Add(new Vector3(2933.223, 2783.594, 39.2882));
            IronCoords.Add(new Vector3(2921.406, 2792.384, 40.52419));


            GenerateIron();
        }
        public void GenerateTinCoords()
        {
            TinCoords.Add(new Vector3(-428.528, 2062.309, 120.778));
            TinCoords.Add(new Vector3(-422.9998, 2066.117, 119.8834));
            TinCoords.Add(new Vector3(-544.1459, 1971.82, 127.192));
            TinCoords.Add(new Vector3(-543.3019, 1965.7, 127.0319));
            TinCoords.Add(new Vector3(-539.7396, 1958.325, 126.5564));
            TinCoords.Add(new Vector3(-541.116, 1951.344, 126.3845));
            TinCoords.Add(new Vector3(-538.5478, 1942.238, 125.8907));
            TinCoords.Add(new Vector3(-534.3332, 1935.171, 125.1719));
            TinCoords.Add(new Vector3(-536.4276, 1926.226, 124.5899));
            TinCoords.Add(new Vector3(-534.4328, 1918.582, 123.952));
            TinCoords.Add(new Vector3(-538.6108, 1912.652, 123.6778));
            TinCoords.Add(new Vector3(-538.0378, 1904.255, 123.2017));
            TinCoords.Add(new Vector3(-529.0594, 1898.549, 123.0209));
            TinCoords.Add(new Vector3(-521.0908, 1897.767, 122.3631));
            TinCoords.Add(new Vector3(-514.7388, 1893.031, 122.0856));
            TinCoords.Add(new Vector3(-506.48, 1894.836, 121.3614));
            TinCoords.Add(new Vector3(-498.8312, 1892.274, 120.9444));
            TinCoords.Add(new Vector3(-490.9808, 1895.626, 120.2071));
            TinCoords.Add(new Vector3(-484.1254, 1893.43, 120.0181));
            TinCoords.Add(new Vector3(-543.9633, 1903.804, 123.1603));
            TinCoords.Add(new Vector3(-547.3446, 1896.442, 123.0351));
            TinCoords.Add(new Vector3(-553.0597, 1894.665, 123.0349));
            TinCoords.Add(new Vector3(-562.2996, 1886.174, 123.0227));
            TinCoords.Add(new Vector3(-565.871, 1887.119, 123.035));
            TinCoords.Add(new Vector3(2955.923, 2901.211, 72.56123));
            TinCoords.Add(new Vector3(2948.973, 2907.943, 72.9335));
            TinCoords.Add(new Vector3(2958.136, 2924.917, 74.90025));
            TinCoords.Add(new Vector3(2965.793, 2931.526, 77.50094));

            GenerateTin();
        }
        public void GenerateLeadCoords()
        {
            LeadCoords.Add(new Vector3(2919.544, 2804.155, 42.55074));
            LeadCoords.Add(new Vector3(2926.184, 2814.325, 45.17887));
            LeadCoords.Add(new Vector3(2938.156, 2814.388, 43.21653));
            LeadCoords.Add(new Vector3(2945.422, 2819.231, 42.79794));
            LeadCoords.Add(new Vector3(2949.463, 2821.082, 43.09269));
            LeadCoords.Add(new Vector3(2956.936, 2819.435, 42.54478));
            LeadCoords.Add(new Vector3(2976.981, 2832.68, 46.39533));
            LeadCoords.Add(new Vector3(2971.127, 2844.599, 46.60767));
            LeadCoords.Add(new Vector3(2959.711, 2850.704, 47.39423));
            LeadCoords.Add(new Vector3(2948.873, 2852.513, 48.88198));
            LeadCoords.Add(new Vector3(2914.161, 2833.667, 54.00295));
            LeadCoords.Add(new Vector3(2916.939, 2852.879, 56.7502));
            LeadCoords.Add(new Vector3(2928.232, 2859.219, 56.41043));
            LeadCoords.Add(new Vector3(2935.436, 2864.833, 57.38452));
            LeadCoords.Add(new Vector3(2945.328, 2873.618, 58.45891));
            LeadCoords.Add(new Vector3(2965.561, 2896.787, 61.90408));
            LeadCoords.Add(new Vector3(2970.987, 2901.669, 62.46307));
            LeadCoords.Add(new Vector3(2982.7, 2908.352, 60.82835));
            LeadCoords.Add(new Vector3(2991.144, 2915.327, 60.4749));
            LeadCoords.Add(new Vector3(3008.564, 2904.496, 62.7826));
            LeadCoords.Add(new Vector3(3019.683, 2911.567, 63.99875));
            LeadCoords.Add(new Vector3(3025.348, 2921.355, 64.84753));
            LeadCoords.Add(new Vector3(3027.161, 2933.799, 64.83324));
            LeadCoords.Add(new Vector3(3028.35, 2946.06, 65.7478));
            LeadCoords.Add(new Vector3(3025.032, 2959.508, 67.47158));
            LeadCoords.Add(new Vector3(3012.115, 2955.291, 67.867));
            LeadCoords.Add(new Vector3(2993.713, 2944.243, 69.69943));
            LeadCoords.Add(new Vector3(2982.553, 2932.4, 70.61167));
            LeadCoords.Add(new Vector3(2974.425, 2922.299, 70.63287));

            GenerateLead();
        }
        public void GenerateGoldCoords()
        {
            GoldCoords.Add(new Vector3(2978.868, 2911.028, 70.90937));
            //GoldCoords.Add(new Vector3(-591.6027, 2075.5366, 130.48064));
            //GoldCoords.Add(new Vector3(-590.6442, 2064.8406, 130.20831));
            //GoldCoords.Add(new Vector3(-587.8685, 2060.606, 129.75726));
            //GoldCoords.Add(new Vector3(-589.2712, 2052.7168, 129.17943));
            //GoldCoords.Add(new Vector3(-585.1507, 2046.242, 129.5875));
            //GoldCoords.Add(new Vector3(-593.95197, 2078.6904, 130.57394));
            //GoldCoords.Add(new Vector3(-591.6027, 2075.5366, 130.48064));
            //GoldCoords.Add(new Vector3(-590.6442, 2064.8406, 130.20831));
            //GoldCoords.Add(new Vector3(-587.8685, 2060.606, 129.75726));
            //GoldCoords.Add(new Vector3(-589.2712, 2052.7168, 129.17943));
            //GoldCoords.Add(new Vector3(-585.1507, 2046.242, 129.5875));
            //GoldCoords.Add(new Vector3(-593.95197, 2078.6904, 130.57394));
            //GoldCoords.Add(new Vector3(-591.6027, 2075.5366, 130.48064));
            //GoldCoords.Add(new Vector3(-590.6442, 2064.8406, 130.20831));
            //GoldCoords.Add(new Vector3(-587.8685, 2060.606, 129.75726));
            //GoldCoords.Add(new Vector3(-589.2712, 2052.7168, 129.17943));
            //GoldCoords.Add(new Vector3(-585.1507, 2046.242, 129.5875));

            GenerateGold();
        }
        public void GenerateSodiumCoords()
        {
            SodiumCoords.Add(new Vector3(2966.786, 2901.921, 71.69897));
            SodiumCoords.Add(new Vector3(3056.91, 2993.729, 83.01649));
            SodiumCoords.Add(new Vector3(3068.763, 2959.842, 81.54498));
            SodiumCoords.Add(new Vector3(3065.08, 2939.419, 80.22344));
            SodiumCoords.Add(new Vector3(3065.063, 2927.407, 80.67377));
            SodiumCoords.Add(new Vector3(3060.519, 2898.169, 80.5465));
            SodiumCoords.Add(new Vector3(3047.48, 2878.612, 85.06948));
            SodiumCoords.Add(new Vector3(2378.315, 3592.341, 59.21129));
            SodiumCoords.Add(new Vector3(2373.818, 3587.522, 58.39561));
            SodiumCoords.Add(new Vector3(2367.792, 3589.618, 55.50785));
            SodiumCoords.Add(new Vector3(2360.668, 3592.567, 52.19774));
            SodiumCoords.Add(new Vector3(2362.075, 3588.355, 54.28266));
            SodiumCoords.Add(new Vector3(2363.604, 3584.434, 55.02208));
            SodiumCoords.Add(new Vector3(2360.065, 3576.797, 53.02427));
            SodiumCoords.Add(new Vector3(2356.16, 3579.7, 52.14982));
            SodiumCoords.Add(new Vector3(2354.047, 3573.838, 50.93596));
            SodiumCoords.Add(new Vector3(2357.584, 3572.005, 51.9369));
            SodiumCoords.Add(new Vector3(2362.748, 3565.911, 54.57034));
            SodiumCoords.Add(new Vector3(2366.797, 3566.801, 55.40144));
            SodiumCoords.Add(new Vector3(2367.202, 3560.874, 55.55518));
            SodiumCoords.Add(new Vector3(2389.835, 3561.881, 67.2943));
            SodiumCoords.Add(new Vector3(2391.828, 3565.32, 67.33797));
            SodiumCoords.Add(new Vector3(2389.186, 3568.208, 67.59615));
            SodiumCoords.Add(new Vector3(2387.433, 3572.128, 67.9002));
            SodiumCoords.Add(new Vector3(2435.54, 3669.526, 55.38376));
            SodiumCoords.Add(new Vector3(2447.425, 3668.583, 56.11948));
            SodiumCoords.Add(new Vector3(2452.648, 3657.623, 60.01836));
            SodiumCoords.Add(new Vector3(2450.554, 3653.231, 60.69425));
            SodiumCoords.Add(new Vector3(2444.049, 3649.936, 64.0922));
            SodiumCoords.Add(new Vector3(2437.644, 3648.756, 65.86763));

            GenerateSodium();
        }
        public void GenerateOre()
        {
            
            foreach (Vector3 o in CopperCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Copper = NAPI.Object.CreateObject(773471646, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 CopperPos = new Vector3(0, 0, 0);
                CopperPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel CopperItem = new OreModel();
                CopperItem.itemPosition = CopperPos;
                CopperItem.itemName = "Copper";
                CopperItem.itemDesc = "Copper Bars can be sold to a factory or combined with Tin to create Bronze bars.";
                CopperItem.itemObject = Copper;
                CopperItem.itemHud = NAPI.TextLabel.CreateTextLabel("Copper", CopperPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                CopperList.Add(CopperItem);
            }

        }
        public void GenerateIron()
        {
            foreach (Vector3 o in IronCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Iron = NAPI.Object.CreateObject(1471437843, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 newPos = new Vector3(0, 0, 0);
                newPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel IronItem = new OreModel();
                IronItem.itemPosition = newPos;
                IronItem.itemName = "Iron";
                IronItem.itemDesc = "Iron Bars can be sold to a factory.";
                IronItem.itemObject = Iron;
                IronItem.itemHud = NAPI.TextLabel.CreateTextLabel("Iron", newPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                IronList.Add(IronItem);
            }

        }
        public void GenerateLead()
        {
            foreach (Vector3 o in LeadCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Lead = NAPI.Object.CreateObject(1471437843, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 LeadPos = new Vector3(0, 0, 0);
                LeadPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel LeadItem = new OreModel();
                LeadItem.itemPosition = LeadPos;
                LeadItem.itemName = "Lead";
                LeadItem.itemDesc = "Lead Bars can be sold to a factory.";
                LeadItem.itemObject = Lead;
                LeadItem.itemHud = NAPI.TextLabel.CreateTextLabel("Lead", LeadPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                LeadList.Add(LeadItem);
            }

        }
        public void GenerateTin()
        {
            foreach (Vector3 o in TinCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Tin = NAPI.Object.CreateObject(773471646, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 TinPos = new Vector3(0, 0, 0);
                TinPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel TinItem = new OreModel();
                TinItem.itemPosition = TinPos;
                TinItem.itemName = "Tin";
                TinItem.itemDesc = "Tin Bars can be sold to a factory or combined with copper to create Bronze Bars.";
                TinItem.itemObject = Tin;
                TinItem.itemHud = NAPI.TextLabel.CreateTextLabel("Tin", TinPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                TinList.Add(TinItem);
            }

        }
        public void GenerateGold()
        {
            foreach (Vector3 o in GoldCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Gold = NAPI.Object.CreateObject(1471437843, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 GoldPos = new Vector3(0, 0, 0);
                GoldPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel GoldItem = new OreModel();
                GoldItem.itemPosition = GoldPos;
                GoldItem.itemName = "Gold";
                GoldItem.itemDesc = "Gold Bars can be sold to a factory.";
                GoldItem.itemObject = Gold;
                GoldItem.itemHud = NAPI.TextLabel.CreateTextLabel("Gold", GoldPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                GoldList.Add(GoldItem);
            }

        }
        public void GenerateSodium()
        {
            foreach (Vector3 o in SodiumCoords)
            {
                Vector3 VeinPosition = new Vector3(0, 0, 0);
                VeinPosition = new Vector3(o.X, o.Y, o.Z - 1.0f);
                GTANetworkAPI.Object Sodium = NAPI.Object.CreateObject(773471646, VeinPosition, new Vector3(0, 0, 0), 255, 0);
                Vector3 SodiumPos = new Vector3(0, 0, 0);
                SodiumPos = new Vector3(o.X, o.Y, o.Z + 0.5f);
                OreModel SodiumItem = new OreModel();
                SodiumItem.itemPosition = SodiumPos;
                SodiumItem.itemName = "Sodium";
                SodiumItem.itemDesc = "Sodium can be used to make drugs.";
                SodiumItem.itemObject = Sodium;
                SodiumItem.itemHud = NAPI.TextLabel.CreateTextLabel("Sodium", SodiumPos, 10, 2, 4, new Color(255, 255, 255), true, 0);
                SodiumList.Add(SodiumItem);
            }

        }
        [Command("regenore")]
        public void Regenore(Client player)
        {
            Destroyores();

            if (player.GetData(EntityData.PLAYER_ADMIN_RANK) > Constants.STAFF_ADMIN)
            {
                GenerateOre();
                GenerateIron();
                GenerateGold();
                GenerateTin();
                GenerateLead();
                GenerateSodium();
                

            }

        }
        public void Destroyores()
        {
            foreach(OreModel CopperItem in CopperList)
            {
                CopperItem.itemHud.Delete();
                CopperItem.itemObject.Delete();
            }
            foreach(OreModel IronItem in IronList)
            {
                IronItem.itemHud.Delete();
                IronItem.itemObject.Delete();
            }
            foreach(OreModel LeadItem in LeadList)
            {
                LeadItem.itemHud.Delete();
                LeadItem.itemObject.Delete();
            }
            foreach(OreModel GoldItem in GoldList)
            {
                GoldItem.itemHud.Delete();
                GoldItem.itemObject.Delete();
            }
            foreach(OreModel TinItem in TinList)
            {
                TinItem.itemHud.Delete();
                TinItem.itemObject.Delete();
            }
            foreach(OreModel SodiumItem in SodiumList)
            {
                SodiumItem.itemHud.Delete();
                SodiumItem.itemObject.Delete();
            }
        }
        #endregion
        public static void OnPlayerDisconnected(Client player)
        {
            if (miningTimerList.TryGetValue(player.Value, out Timer miningTimer))
            {
                // Remove the timer
                miningTimer.Dispose();
                miningTimerList.Remove(player.Value);
            }
        }
        #region Mining
        public static void AnimateMining(Client player, bool isMining)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMining)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectOreAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMiningIron(Client player, bool isMiningIron)
        {

            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMiningIron)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectIronAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMiningLead(Client player, bool isMining)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMining)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectLeadAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMiningGold(Client player, bool isMiningIron)
        {

            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMiningIron)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectGoldAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMiningSodium(Client player, bool isMining)
        {
            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMining)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectSodiumAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static void AnimateMiningTin(Client player, bool isMiningIron)
        {

            // Play the animation
            player.PlayAnimation("amb@world_human_gardener_plant@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isMiningIron)
            {
                // Create the mining timer
                miningTimer = new Timer(CollectTinAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        public static PlayerModel GetCharacterById(int characterId)
        {
            // Get the business given an specific identifier
            return playerList.Where(character => character.id == characterId).FirstOrDefault();
        }
        [Command("mining")]
        public static void MiningCommand(Client player)
        {
            foreach (OreModel CopperItem in CopperList)
            {
                if (CopperItem.itemHud.Position.DistanceTo(player.Position) < 5)
                {
                    if (miningTimerList.ContainsKey(player.Value))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                        return;
                    }


                    CopperItem.itemHud.Delete();
                    CopperItem.itemObject.Delete();
                    if (miningTimerList.ContainsKey(player.Value))
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                        return;
                    }

                    if (player == player)
                    {
                        //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                        //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                        //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                        //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                        //  {
                        //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                        //      return;
                        //  }

                        //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                        player.SetData(EntityData.PlayerMining, true);
                        player.PlayAnimation("amb@world_human_stand_fishing@base", "base", (int)Constants.AnimationFlags.Loop);
                        AnimateMining(player, true);
                        player.TriggerEvent("playermining"); // Not required atm
                        return;

                    }

                }
            }

        }
        public static void IronCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel >= 5)
                    {
                        foreach (OreModel IronItem in IronList)
                        {
                            if (IronItem.itemHud.Position.DistanceTo(player.Position) < 5)
                            {
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }


                                IronItem.itemFound = true;
                                IronItem.itemHud.Delete();
                                IronItem.itemObject.Delete();
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }

                                if (player == player)
                                {
                                    //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                                    //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                                    //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                                    //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                                    //  {
                                    //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                                    //      return;
                                    //  }

                                    //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                                    player.SetData(EntityData.PlayerMining, true);
                                    AnimateMiningIron(player, true);
                                    player.TriggerEvent("startMining"); // Not required atm
                                    return;

                                }

                            }
                        }
                       
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not high enough level to mine this");
                        return;
                    }
                    
                }
            
            }

        }
        public static void LeadCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel >= 10)
                    {
                        foreach (OreModel LeadItem in LeadList)
                        {
                            if (LeadItem.itemHud.Position.DistanceTo(player.Position) < 5)
                            {
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }


                                LeadItem.itemFound = true;
                                LeadItem.itemHud.Delete();
                                LeadItem.itemObject.Delete();
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }

                                if (player == player)
                                {
                                    //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                                    //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                                    //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                                    //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                                    //  {
                                    //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                                    //      return;
                                    //  }

                                    //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                                    player.SetData(EntityData.PlayerMining, true);
                                    AnimateMiningLead(player, true);
                                    player.TriggerEvent("startMining"); // Not required atm
                                    return;

                                }

                            }
                        }

                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not high enough level to mine this");
                        return;
                    }

                }

            }

        }
        public static void GoldCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel >= 20)
                    {
                        foreach (OreModel GoldItem in GoldList)
                        {
                            if (GoldItem.itemHud.Position.DistanceTo(player.Position) < 5)
                            {
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }


                                GoldItem.itemFound = true;
                                GoldItem.itemHud.Delete();
                                GoldItem.itemObject.Delete();
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }

                                if (player == player)
                                {
                                    //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                                    //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                                    //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                                    //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                                    //  {
                                    //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                                    //      return;
                                    //  }

                                    //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                                    player.SetData(EntityData.PlayerMining, true);
                                    AnimateMiningGold(player, true);
                                    player.TriggerEvent("startMining"); // Not required atm
                                    return;

                                }

                            }
                        }

                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not high enough level to mine this");
                        return;
                    }

                }

            }

        }
        public static void TinCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel >= 5)
                    {
                        foreach (OreModel TinItem in TinList)
                        {
                            if (TinItem.itemHud.Position.DistanceTo(player.Position) < 5)
                            {
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }


                                TinItem.itemFound = true;
                                TinItem.itemHud.Delete();
                                TinItem.itemObject.Delete();
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }

                                if (player == player)
                                {
                                    //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                                    //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                                    //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                                    //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                                    //  {
                                    //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                                    //      return;
                                    //  }

                                    //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                                    player.SetData(EntityData.PlayerMining, true);
                                    AnimateMiningTin(player, true);
                                    player.TriggerEvent("startMining"); // Not required atm
                                    return;

                                }

                            }
                        }

                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not high enough level to mine this");
                        return;
                    }

                }

            }

        }
        public static void SodiumCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel >= 20)
                    {
                        foreach (OreModel SodiumItem in SodiumList)
                        {
                            if (SodiumItem.itemHud.Position.DistanceTo(player.Position) < 5)
                            {
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }


                                SodiumItem.itemFound = true;
                                SodiumItem.itemHud.Delete();
                                SodiumItem.itemObject.Delete();
                                if (miningTimerList.ContainsKey(player.Value))
                                {
                                    player.SendChatMessage(Constants.COLOR_ERROR + "You are already mining.");
                                    return;
                                }

                                if (player == player)
                                {
                                    //  string rightHand = player.GetSharedData(EntityData.PLAYER_RIGHT_HAND);
                                    //  int pickeaxeId = NAPI.Util.FromJson<AttachmentModel>(rightHand).itemId;
                                    //  ItemModel pickaxe = Inventory.GetPlayerItemModelFromHash(Constants.ITEM_HASH_PICKAXE);

                                    //  if (pickaxe == null || pickaxe.hash != Constants.ITEM_HASH_PICKAXE)
                                    //  {
                                    //      player.SendChatMessage(Constants.COLOR_ERROR + "You do not have a pickaxe equipped.");
                                    //      return;
                                    //  }

                                    //int playerId = player.GetExternalData<CharacterModel>((int)ExternalDataSlot.Database).Id;

                                    player.SetData(EntityData.PlayerMining, true);
                                    AnimateMiningSodium(player, true);
                                    player.TriggerEvent("startMining"); // Not required atm
                                    return;

                                }

                            }
                        }

                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You are not high enough level to mine this");
                        return;
                    }

                }

            }

        }


        private static void CollectOreAsync(object oreItem)
        {
            // Get the client mining
            Client player = (Client)oreItem;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CopperItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPER);

            if (CopperItem == null)
            {
                // Create the object
                CopperItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COPPER,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(CopperItem);
                Globals.itemList.Add(CopperItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Copper Ore");
            }
            else
            {
                // Add the amount
                CopperItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(CopperItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Copper Ore");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectIronAsync(object Iron)
        {
            // Get the client mining
            Client player = (Client)Iron;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel IronItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_IRON);

            if (IronItem == null)
            {
                // Create the object
                IronItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_IRON,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(IronItem);
                Globals.itemList.Add(IronItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Iron Ore");
            }
            else
            {
                // Add the amount
                IronItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(IronItem);
                //Globals.itemList.Add(IronItem);  THis might dupliacte the item :S
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Iron Ore");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }

                    Database.SaveSkills(PlayerSkillList);
                }
            }

        }
        private static void CollectTinAsync(object Tin)
        {
            // Get the client mining
            Client player = (Client)Tin;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel TinItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_TIN);

            if (TinItem == null)
            {
                // Create the object
                TinItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_TIN,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(TinItem);
                Globals.itemList.Add(TinItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Tin Ore");
            }
            else
            {
                // Add the amount
                TinItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(TinItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Tin Ore");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectLeadAsync(object Lead)
        {
            // Get the client mining
            Client player = (Client)Lead;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel LeadItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEAD);

            if (LeadItem == null)
            {
                // Create the object
                LeadItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_LEAD,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(LeadItem);
                Globals.itemList.Add(LeadItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Lead Ore");
            }
            else
            {
                // Add the amount
                LeadItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(LeadItem);
                //Globals.itemList.Add(IronItem);  THis might dupliacte the item :S
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Lead Ore");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }

                    Database.SaveSkills(PlayerSkillList);
                }
            }

        }
        private static void CollectGoldAsync(object Gold)
        {
            // Get the client mining
            Client player = (Client)Gold;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel GoldItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_GOLD);

            if (GoldItem == null)
            {
                // Create the object
                GoldItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_GOLD,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(GoldItem);
                Globals.itemList.Add(GoldItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Gold Ore");
            }
            else
            {
                // Add the amount
                GoldItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(GoldItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 Gold Ore");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 3;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        private static void CollectSodiumAsync(object Sodium)
        {
            // Get the client mining
            Client player = (Client)Sodium;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 1;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel SodiumItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_SODIUM);

            if (SodiumItem == null)
            {
                // Create the object
                SodiumItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_SODIUM,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(SodiumItem);
                Globals.itemList.Add(SodiumItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 gram of Sodium");
            }
            else
            {
                // Add the amount
                SodiumItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PlayerMining, false);
                Database.UpdateItem(SodiumItem);
                //Globals.itemList.Add(IronItem);  THis might dupliacte the item :S
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You mined 1 gram of Sodium");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 5;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }

                    Database.SaveSkills(PlayerSkillList);
                }
            }

        }
        #endregion
        #region Smeling
        [Command("Smelt")]
        public static void SmeltCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            if (player.GetData(EntityData.PLAYER_SMELTING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_smelting);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Copper = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPER);
            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel > 0)
                    {
                        if (Copper != null && Copper.amount > 0)
                        {
                            foreach (Vector3 Factory in Constants.FACTORY_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(Factory) > 6f) continue;
                                {
                                    if (Copper.amount == 1)
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Copper);
                                        Database.RemoveItem(Copper.id);


                                    }
                                    else
                                    {
                                        Copper.amount--;
                                        // Update the amount
                                        Database.UpdateItem(Copper);
                                    }

                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_SMELTING, true);
                                }
                                AnimateSmelting(player, true);
                                return;
                            }

                            // Player's not in the fishing zone
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_smelting_zone);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You havent yet learned how to handle this metal");
                    }
                }
            }
        }
        public static void AnimateSmelting(Client player, bool isSmelting)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isSmelting)
            {
                // Create the mining timer
                miningTimer = new Timer(SmeltingAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        private static void SmeltingAsync(object Copper)
        {
            // Get the client mining
            Client player = (Client)Copper;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel CopperbarItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPERBAR);

            if (CopperbarItem != null)
            {
                // Add the amount
                CopperbarItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_SMELTING, false);
                Database.UpdateItem(CopperbarItem);
                Globals.itemList.Add(CopperbarItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Copper bar");
            }
            else
            {
                // Create the object
                CopperbarItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_COPPERBAR,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(CopperbarItem);
                Globals.itemList.Add(CopperbarItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Copper bar");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        public static void SmeltIron(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            if (player.GetData(EntityData.PLAYER_SMELTING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_smelting);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Iron = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_IRON);
            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel > 4)
                    {
                        if (Iron != null && Iron.amount > 0)
                        {
                            foreach (Vector3 Factory in Constants.FACTORY_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(Factory) > 6f) continue;
                                {
                                    if (Iron.amount == 1)
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Iron);
                                        Database.RemoveItem(Iron.id);


                                    }
                                    else
                                    {
                                        Iron.amount--;
                                        // Update the amount
                                        Database.UpdateItem(Iron);
                                    }

                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_SMELTING, true);
                                }
                                AnimateSmeltingIron(player, true);
                                return;
                            }

                            // Player's not in the fishing zone
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_smelting_zone);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You havent yet learned how to work with this metal.");
                    }
                }
            }
        }
        public static void AnimateSmeltingIron(Client player, bool isSmelting)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isSmelting)
            {
                // Create the mining timer
                miningTimer = new Timer(SmeltingIronAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        private static void SmeltingIronAsync(object Iron)
        {
            // Get the client mining
            Client player = (Client)Iron;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel IronbarItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_IRONBAR);

            if (IronbarItem != null)
            {
                // Add the amount
                IronbarItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_SMELTING, false);
                Database.UpdateItem(IronbarItem);
                Globals.itemList.Add(IronbarItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Iron bars");
            }
            else
            {
                // Create the object
                IronbarItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_IRONBAR,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(IronbarItem);
                Globals.itemList.Add(IronbarItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Iron bars");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        public static void SmeltLeadCommand(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            if (player.GetData(EntityData.PLAYER_SMELTING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_smelting);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Lead = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEAD);
            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel > 10)
                    {
                        if (Lead != null && Lead.amount > 0)
                        {
                            foreach (Vector3 Factory in Constants.FACTORY_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(Factory) > 6f) continue;
                                {
                                    if (Lead.amount == 1)
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Lead);
                                        Database.RemoveItem(Lead.id);


                                    }
                                    else
                                    {
                                        Lead.amount--;
                                        // Update the amount
                                        Database.UpdateItem(Lead);
                                    }

                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_SMELTING, true);
                                }
                                AnimateSmeltingLead(player, true);
                                return;
                            }

                            // Player's not in the fishing zone
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_smelting_zone);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You havent yet learned how to handle this metal");
                    }
                }
            }
        }
        public static void AnimateSmeltingLead(Client player, bool isSmelting)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isSmelting)
            {
                // Create the mining timer
                miningTimer = new Timer(SmeltingLeadAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        private static void SmeltingLeadAsync(object Lead)
        {
            // Get the client mining
            Client player = (Client)Lead;

            // Stop the mining animation
            player.StopAnimation();

            // Give the ore to the player
            int amount = 2;

            // Check if the player has any Copper ore in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel LeadbarItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEADBAR);

            if (LeadbarItem != null)
            {
                // Add the amount
                LeadbarItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_SMELTING, false);
                Database.UpdateItem(LeadbarItem);
                Globals.itemList.Add(LeadbarItem);
                //  Globals.itemList.Add(CopperItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Lead bars");
            }
            else
            {
                // Create the object
                LeadbarItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_LEADBAR,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(LeadbarItem);
                Globals.itemList.Add(LeadbarItem);
                // Inventory.ItemCollection.Add(CocainItem.id, CocainItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Lead bars");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        public static void SmeltGold(Client player)
        {
            List<SkillsModel> PlayerSkillList = Database.LoadSkills();
            if (player.GetData(EntityData.PLAYER_SMELTING) != null)
            {
                player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_already_smelting);
            }
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel Gold = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_GOLD);
            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    if (skills.mininglevel > 10)
                    {
                        if (Gold != null && Gold.amount > 0)
                        {
                            foreach (Vector3 Factory in Constants.FACTORY_POSITION_LIST)
                            {
                                // Check if the player is close to the area
                                if (player.Position.DistanceTo(Factory) > 6f) continue;
                                {
                                    if (Gold.amount == 1)
                                    {
                                        // Remove the baits from the inventory
                                        Globals.itemList.Remove(Gold);
                                        Database.RemoveItem(Gold.id);


                                    }
                                    else
                                    {
                                        Gold.amount--;
                                        // Update the amount
                                        Database.UpdateItem(Gold);
                                    }

                                    // Start fishing minigame
                                    player.SetData(EntityData.PLAYER_SMELTING, true);
                                }
                                AnimateSmeltingGold(player, true);
                                return;
                            }

                            // Player's not in the fishing zone
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_smelting_zone);
                        }
                        else
                        {
                            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_mats);
                        }
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + "You havent yet learned how to work with this metal.");
                    }
                }
            }
        }
        public static void AnimateSmeltingGold(Client player, bool isSmelting)
        {
            // Play the animation
            player.PlayAnimation("amb@prop_human_parking_meter@male@idle_a", "idle_a", (int)(Constants.AnimationFlags.AllowPlayerControl | Constants.AnimationFlags.Loop));

            // Create the timer
            Timer miningTimer = null;

            if (isSmelting)
            {
                // Create the mining timer
                miningTimer = new Timer(SmeltingGoldAsync, player, 20000, Timeout.Infinite);
            }
            // Add the timer to the list
            miningTimerList.Add(player.Value, miningTimer);
        }
        private static void SmeltingGoldAsync(object Gold)
        {
            // Get the client smelting
            Client player = (Client)Gold;

            // Stop the mining animation
            player.StopAnimation();

            // Give the amount to the player
            int amount = 2;

            // Check if the player has any GoldBar in the inventory
            int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
            ItemModel GoldbarItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_GOLDBAR);

            if (GoldbarItem != null)
            {
                // Add the amount
                GoldbarItem.amount += amount;

                // Update the amount into the database
                // Update the item's amount
                player.SetData(EntityData.PLAYER_SMELTING, false);
                Database.UpdateItem(GoldbarItem);
                Globals.itemList.Add(GoldbarItem);
                // Send the confirmation message
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Gold bars");
            }
            else
            {
                // Create the object
                GoldbarItem = new ItemModel()
                {
                    amount = amount,
                    dimension = 0,
                    position = new Vector3(),
                    hash = Constants.ITEM_HASH_GOLDBAR,
                    ownerEntity = Constants.ITEM_ENTITY_PLAYER,
                    ownerIdentifier = playerId,
                    objectHandle = null,
                    quality = "Poor"
                };
                Database.AddNewItem(GoldbarItem);
                Globals.itemList.Add(GoldbarItem);
                player.SendNotification("You gained 1 XP in Mining");
                player.SendChatMessage(Constants.COLOR_YELLOW + "You smelted 2 Gold bars");
            }


            // Remove the timer from the list
            miningTimerList.Remove(player.Value);

            List<SkillsModel> PlayerSkillList = Database.LoadSkills();

            foreach (SkillsModel skills in PlayerSkillList)
            {
                if (skills.id == player.GetData(EntityData.PLAYER_SQL_ID))
                {
                    skills.miningexp = skills.miningexp + 1;
                    if (skills.miningexp == 10)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 20)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 40)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 80)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 120)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 140)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 180)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 220)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 260)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 310)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 360)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 410)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 470)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 550)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 610)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 670)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 730)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 780)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 860)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    if (skills.miningexp == 910)
                    {
                        skills.mininglevel = skills.mininglevel + 1;
                        player.SendNotification("You leveled up in Mining!");
                    }
                    Database.SaveSkills(PlayerSkillList);
                }
            }
        }
        #endregion
        #region Selling
        [Command("selliron")]
        public static void SellIron(Client player)
        {
            foreach (Vector3 barBuyer in Constants.FACTORY_MERCHANT)
            {
                if (player.Position.DistanceTo(barBuyer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel IronItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_IRONBAR);
                    if (IronItem != null)
                    {
                        // Calculate the earnings
                        int wonAmount = IronItem.amount * Constants.PRICE_IRONBAR;
                        string message = string.Format(InfoRes.player_ironbar_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;


                            NAPI.Task.Run(() =>
                            {
                                // Delete stolen items
                                Database.RemoveItem(IronItem.id);
                                Globals.itemList.Remove(IronItem);
                            });


                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_ironbar);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_factorymerchant);
        }
        [Command("sellcopper")]
        public static void SellCopper(Client player)
        {
            foreach (Vector3 barBuyer in Constants.FACTORY_MERCHANT)
            {
                if (player.Position.DistanceTo(barBuyer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel CopperItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_COPPERBAR);
                    if (CopperItem != null)
                    {
                        // Calculate the earnings
                        int wonAmount = CopperItem.amount * Constants.PRICE_COPPERBAR;
                        string message = string.Format(InfoRes.player_copperbar_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;


                        NAPI.Task.Run(() =>
                        {
                            // Delete stolen items
                            Database.RemoveItem(CopperItem.id);
                            Globals.itemList.Remove(CopperItem);
                        });


                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_copperbar);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_factorymerchant);
        }
        [Command("selltin")]
        public static void SellTin(Client player)
        {
            foreach (Vector3 barBuyer in Constants.FACTORY_MERCHANT)
            {
                if (player.Position.DistanceTo(barBuyer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel TinItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_TINBAR);
                    if (TinItem != null)
                    {
                        // Calculate the earnings
                        int wonAmount = TinItem.amount * Constants.PRICE_TINBAR;
                        string message = string.Format(InfoRes.player_tinbar_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;


                        NAPI.Task.Run(() =>
                        {
                            // Delete stolen items
                            Database.RemoveItem(TinItem.id);
                            Globals.itemList.Remove(TinItem);
                        });


                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_tinbar);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_factorymerchant);
        }
        [Command("selllead")]
        public static void SellLead(Client player)
        {
            foreach (Vector3 barBuyer in Constants.FACTORY_MERCHANT)
            {
                if (player.Position.DistanceTo(barBuyer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel LeadItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_LEADBAR);
                    if (LeadItem != null)
                    {
                        // Calculate the earnings
                        int wonAmount = LeadItem.amount * Constants.PRICE_LEADBAR;
                        string message = string.Format(InfoRes.player_leadbar_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;


                        NAPI.Task.Run(() =>
                        {
                            // Delete stolen items
                            Database.RemoveItem(LeadItem.id);
                            Globals.itemList.Remove(LeadItem);
                        });


                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_leadbar);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_factorymerchant);
        }
        [Command("sellgold")]
        public static void SellGold(Client player)
        {
            foreach (Vector3 barBuyer in Constants.FACTORY_MERCHANT)
            {
                if (player.Position.DistanceTo(barBuyer) < 1.5f)
                {
                    int playerId = player.GetData(EntityData.PLAYER_SQL_ID);
                    ItemModel GoldItem = Globals.GetPlayerItemModelFromHash(playerId, Constants.ITEM_HASH_GOLDBAR);
                    if (GoldItem != null)
                    {
                        // Calculate the earnings
                        int wonAmount = GoldItem.amount * Constants.PRICE_GOLDBAR;
                        string message = string.Format(InfoRes.player_goldbar_sold, wonAmount);
                        int money = player.GetSharedData(EntityData.PLAYER_MONEY) + wonAmount;


                        NAPI.Task.Run(() =>
                        {
                            // Delete stolen items
                            Database.RemoveItem(GoldItem.id);
                            Globals.itemList.Remove(GoldItem);
                        });


                        player.SetSharedData(EntityData.PLAYER_MONEY, money);
                        player.SendChatMessage(Constants.COLOR_INFO + message);
                    }
                    else
                    {
                        player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.player_no_goldbar);
                    }
                    return;
                }
            }
            player.SendChatMessage(Constants.COLOR_ERROR + ErrRes.not_at_factorymerchant);
        }
        #endregion

    }
}
    




