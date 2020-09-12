using RAGE;
using WiredPlayers_Client.model;
using System.Collections.Generic;

namespace WiredPlayers_Client.globals
{
    class Constants
    {
        public static readonly ushort INVALID_VALUE = 65535;

        public static readonly int SEX_MALE = 0;
        public static readonly int SEX_FEMALE = 1;

        public static readonly string HAND_MONEY = "PLAYER_MONEY";
        public static readonly string VEHICLE_DOORS_STATE = "VEHICLE_DOORS_STATE";
        public static readonly string VEHICLE_SIREN_SOUND = "VEHICLE_SIREN_SOUND";
        public static readonly string ITEM_ENTITY_RIGHT_HAND = "PLAYER_RIGHT_HAND";
        public static readonly string ITEM_ENTITY_WEAPON_CRATE = "PLAYER_WEAPON_CRATE";
        public static readonly string ITEM_ENTITY_HANDCUFFS = "PLAYER_HANDCUFFED";
        public static readonly string PLAYER_KILLED_STATE = "PLAYER_KILLED";

        public static readonly float CONSUME_PER_METER = 0.00065f;

        public static readonly int VEHICLE_SEAT_DRIVER = -1;

        // Vehicle types
        public const int VEHICLE_CLASS_COMPACTS = 0;
        public const int VEHICLE_CLASS_SEDANS = 1;
        public const int VEHICLE_CLASS_SUVS = 2;
        public const int VEHICLE_CLASS_COUPES = 3;
        public const int VEHICLE_CLASS_MUSCLE = 4;
        public const int VEHICLE_CLASS_SPORTS = 5;
        public const int VEHICLE_CLASS_CLASSICS = 6;
        public const int VEHICLE_CLASS_SUPER = 7;
        public const int VEHICLE_CLASS_MOTORCYCLES = 8;
        public const int VEHICLE_CLASS_OFFROAD = 9;
        public const int VEHICLE_CLASS_INDUSTRIAL = 10;
        public const int VEHICLE_CLASS_UTILITY = 11;
        public const int VEHICLE_CLASS_VANS = 12;
        public const int VEHICLE_CLASS_CYCLES = 13;
        public const int VEHICLE_CLASS_BOATS = 14;
        public const int VEHICLE_CLASS_HELICOPTERS = 15;
        public const int VEHICLE_CLASS_PLANES = 16;
        public const int VEHICLE_CLASS_SERVICE = 17;
        public const int VEHICLE_CLASS_EMERGENCY = 18;
        public const int VEHICLE_CLASS_MILITARY = 19;
        public const int VEHICLE_CLASS_COMMERCIAL = 20;
        public const int VEHICLE_CLASS_TRAINS = 21;

        public static readonly List<ClothesModel> CLOTHES_TYPES = new List<ClothesModel>()
        {
            new ClothesModel(0, 1, "clothes.masks"), new ClothesModel(0, 3, "clothes.torso"), new ClothesModel(0, 4, "clothes.legs"),
            new ClothesModel(0, 5, "clothes.bags"), new ClothesModel(0, 6, "clothes.feet"), new ClothesModel(0, 7, "clothes.complements"),
            new ClothesModel(0, 8, "clothes.undershirt"), new ClothesModel(0, 9, "clothes.body-armor"), new ClothesModel(0, 11, "clothes.tops"),
            new ClothesModel(1, 0, "clothes.hats"), new ClothesModel(1, 1, "clothes.glasses"), new ClothesModel(1, 2, "clothes.earrings"),
            new ClothesModel(1, 6, "clothes.watches"), new ClothesModel(1, 7, "clothes.bracelets")
        };

        public static readonly List<string> TATTOO_ZONES = new List<string>()
        {
            "tattoo.torso", "tattoo.head", "tattoo.left-arm", "tattoo.right-arm", "tattoo.left-leg", "tattoo.right-leg"
        };

        public static readonly List<FaceOption> MALE_FACE_OPTIONS = new List<FaceOption>()
        {
            new FaceOption("hairdresser.hair", 0, 36), new FaceOption("hairdresser.hair-primary", 0, 63), new FaceOption("hairdresser.hair-secondary", 0, 63),
            new FaceOption("hairdresser.eyebrows", 0, 33), new FaceOption("hairdresser.eyebrows-color", 0, 63), new FaceOption("hairdresser.beard", -1, 36),
            new FaceOption("hairdresser.beard-color", 0, 63)
        };

        public static readonly List<FaceOption> FEMALE_FACE_OPTIONS = new List<FaceOption>()
        {
            new FaceOption("hairdresser.hair", 0, 38), new FaceOption("hairdresser.hair-primary", 0, 63), new FaceOption("hairdresser.hair-secondary", 0, 63),
            new FaceOption("hairdresser.eyebrows", 0, 33), new FaceOption("hairdresser.eyebrows-color", 0, 63)
        };

        public static readonly List<Procedure> TOWNHALL_PROCEDURES = new List<Procedure>()
        {
            new Procedure("townhall.identification", 500), new Procedure("townhall.insurance", 2000),
            new Procedure("townhall.taxi", 5000), new Procedure("townhall.fines", 0)
        };

        public static readonly List<CarPiece> CAR_PIECE_LIST = new List<CarPiece>()
        {
            new CarPiece(0, "mechanic.spoiler", 250), new CarPiece(1, "mechanic.front-bumper", 250),new CarPiece(2, "mechanic.rear-bumper", 250),
            new CarPiece(3, "mechanic.side-skirt", 250), new CarPiece(4, "mechanic.exhaust", 100), new CarPiece(5, "mechanic.frame", 500),
            new CarPiece(6, "mechanic.grille", 200), new CarPiece(7, "mechanic.hood", 300), new CarPiece(8, "mechanic.fender", 100),
            new CarPiece(9, "mechanic.right-fender", 100), new CarPiece(10, "mechanic.roof", 400), new CarPiece(14, "mechanic.horn", 100),
            new CarPiece(15, "mechanic.suspension", 900), new CarPiece(22, "mechanic.xenon", 150), new CarPiece(23, "mechanic.front-wheels", 100),
            new CarPiece(24, "mechanic.back-wheels", 100), new CarPiece(25, "mechanic.plaque", 100), new CarPiece(27, "mechanic.trim-design", 800),
            new CarPiece(28, "mechanic.ornaments", 150), new CarPiece(33, "mechanic.steering-wheel", 100), new CarPiece(34, "mechanic.shift-lever", 100),
            new CarPiece(38, "mechanic.hydraulics", 1200), new CarPiece(46, "mechanic.window-tint", 200), new CarPiece(11, "mechanic.engine", 3500),
            new CarPiece(12, "mechanic.brakes", 1500), new CarPiece(13, "mechanic.transmission", 2000), new CarPiece(48, "mechanic.livery", 1000),
            new CarPiece(18, "mechanic.turbo", 5000), new CarPiece(66, "mechanic.colour1", 200), new CarPiece(67, "mechanic.colour2", 200),
        };

        public static List<Vector3> TRUCKER_CRATES = new List<Vector3>()
        {
            new Vector3(1275.89f, -3282.81f, 5.90159f),
            new Vector3(1275.54f, -3287.54f, 5.90159f),
            new Vector3(1275.4f, -3293.04f, 5.90159f),
        };

        public static List<string> VALID_WEAPONS = new List<string>()
        {
            "weapon_pistol", "weapon_combatpistol", "weapon_pistol50", "weapon_snspistol", "weapon_heavypistol", "weapon_vintagepistol", "weapon_marksmanpistol",
            "weapon_revolver", "weapon_appistol", "weapon_flaregun", "weapon_microsmg", "weapon_machinepistol", "weapon_smg", "weapon_combatpdw", "weapon_mg",
            "weapon_combatmg", "weapon_gusenberg", "weapon_minismg", "weapon_assaultrifle", "weapon_carbinerifle", "weapon_advancedrifle", "weapon_specialcarbine",
            "weapon_bullpuprifle", "weapon_compactrifle", "weapon_sniperrifle", "weapon_heavysniper", "weapon_marksmanrifle", "weapon_pumpshotgun", "weapon_sawnoffshotgun",
            "weapon_assaultshotgun", "weapon_bullpupshotgun", "weapon_musket", "weapon_heavyshotgun", "weapon_dbshotgun", "weapon_autoshotgun"
        };
    }
}
