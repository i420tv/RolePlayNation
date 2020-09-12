using RAGE;
using RAGE.Game;
using RAGE.Elements;
using static RAGE.Events;

namespace WiredPlayers_Client.globals
{
    class Doors : Events.Script
    {
        private static Colshape policeMainDoors = null;
        private static Colshape policeBackDoors = null;
        private static Colshape policeCellDoors = null;

        private static Colshape paletoSheriffDoors = null;
        private static Colshape shandySheriffDoors = null;

        private static Colshape motorsportMain = null;
        private static Colshape motorsportParking = null;

        private static Colshape supermarketDoors = null;

        private static Colshape clubhouseDoor = null;

        public Doors()
        {
            // Create the colshapes to trigger the door state change
            policeMainDoors = new CircleColshape(435.131f, -981.9197f, 5.0f, 0);
            policeBackDoors = new CircleColshape(468.535f, -1014.098f, 5.0f, 0);
            policeCellDoors = new CircleColshape(461.7501f, -998.361f, 5.0f, 0);

            paletoSheriffDoors = new CircleColshape(-443.5909f, 6016.152f, 5.0f, 0);
            shandySheriffDoors = new CircleColshape(1855.14f, 3683.586f, 5.0f, 0);            

            motorsportMain = new CircleColshape(-59.893f, -1092.952f, 5.0f, 0);
            motorsportParking = new CircleColshape(-39.134f, -1108.22f, 5.0f, 0);

            supermarketDoors = new CircleColshape(-711.545f, -915.54f, 5.0f, 0);

            clubhouseDoor = new CircleColshape(981.7533f, -102.7987f, 5.0f, 0);

            // Bind the event for the colshapes above
            OnPlayerEnterColshape += OnPlayerEnterColshapeEvent;
        }

        public static void OnPlayerEnterColshapeEvent(Colshape colshape, CancelEventArgs cancel)
        {
            if (colshape == policeMainDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_rc_door2"), 469.9679f, -1014.452f, 26.53623f, true, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_rc_door2"), 467.3716f, -1014.452f, 26.53623f, true, 0.0f, false);
                return;
            }

            if (colshape == policeBackDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_door002"), 434.7479f, -983.2151f, 30.83926f, true, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_door01"), 434.7479f, -980.6184f, 30.83926f, true, 0.0f, false);
                return;
            }

            if (colshape == policeCellDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -994.4086f, 25.06443f, true, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -997.6583f, 25.06443f, true, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_ph_cellgate"), 461.8065f, -1001.302f, 25.06443f, true, 0.0f, false);
                return;
            }

            if (colshape == paletoSheriffDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_shrf2door"), -442.66f, 6015.222f, 31.86633f, false, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_shrf2door"), -444.4985f, 6017.06f, 31.86633f, false, 0.0f, false);
                return;
            }

            if (colshape == shandySheriffDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_shrfdoor"), 1855.685f, 3683.93f, 34.59282f, false, 0.0f, false);
                return;
            }

            if (colshape == motorsportMain)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_csr_door_l"), -59.89302f, -1092.952f, 26.88362f, false, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_csr_door_r"), -60.54582f, -1094.749f, 26.88872f, false, 0.0f, false);
                return;
            }

            if (colshape == motorsportParking)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_csr_door_l"), -39.13366f, -1108.218f, 26.7198f, false, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_csr_door_r"), -37.33113f, -1108.873f, 26.7198f, false, 0.0f, false);
                return;
            }

            if (colshape == supermarketDoors)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_gasdoor"), -711.5449f, -915.5397f, 19.21559f, true, 0.0f, false);
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_gasdoor_r"), -711.5449f, -915.5397f, 19.2156f, true, 0.0f, false);
                return;
            }

            if (colshape == clubhouseDoor)
            {
                Object.SetStateOfClosestDoorOfType(Misc.GetHashKey("v_ilev_lostdoor"), 981.7533f, -102.7987f, 74.84873f, true, 0.0f, false);
                return;
            }
        }
    }
}