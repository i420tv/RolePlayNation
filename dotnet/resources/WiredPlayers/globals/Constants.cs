using GTANetworkAPI;
using WiredPlayers.model;
using WiredPlayers.messages.general;
using WiredPlayers.messages.jobs;
using WiredPlayers.messages.items;
using WiredPlayers.messages.description;
using System.Collections.Generic;

namespace WiredPlayers.globals
{
    public class Constants
    {
        public const int UNDEFINED_VALUE = 65535;
        public const int ITEMS_PER_INVENTORY_PAGE = 16;
        public const decimal ITEMS_ROBBED_PER_TIME = 1.5m;
        public const int MAX_GARBAGE_ROUTES = 1;
        public const int TOTAL_COLOR_ELEMENTS = 3;
        public const int MAX_BANK_OPERATIONS = 25;
        public const int MAX_LICENSE_QUESTIONS = 3;
        public const int MAX_DRIVING_VEHICLE = 100;
        public const int REDUCTION_PER_KMS = 125;
        public const int MAX_THEFTS_IN_ROW = 10;
        public const int MAX_WEAPON_SPAWNS = 1;
        public const int MAX_CRATES_SPAWN = 12;
        public const int MAX_WEAPON_CHANCE = 1235;
        public const int MAX_AMMO_CHANCE = 500;
        public const float GAS_CAN_LITRES = 10.0f;
        public const float LEVEL_MULTIPLIER = 3.25f;
        public const int PAID_PER_LEVEL = 30;
        public const float HOUSE_SALE_STATE = 0.7f;
        public const float BUSINESS_SALE_STATE = 0.5f;
        public const int MAX_HEAD_OVERLAYS = 11;

        // Sex
        public const int SEX_NONE = -1;
        public const int SEX_MALE = 0;
        public const int SEX_FEMALE = 1;

        // Chat
        public const int CHAT_RANGES = 5;

        // Jail types
        public const int JAIL_TYPE_IC = 1;
        public const int JAIL_TYPE_OOC = 2;

        // Administrative ranks
        public const int STAFF_NONE = 0;
        public const int STAFF_SUPPORT = 1;
        public const int STAFF_GAME_MASTER = 2;
        public const int STAFF_S_GAME_MASTER = 3;
        public const int STAFF_ADMIN = 4;

        // Actions
        public const int ACTION_LOAD = 0;
        public const int ACTION_SAVE = 1;
        public const int ACTION_RENAME = 2;
        public const int ACTION_DELETE = 3;
        public const int ACTION_ADD = 4;
        public const int ACTION_SMS = 5;

        // Business types
        public const int BUSINESS_TYPE_NONE = -1;
        public const int BUSINESS_TYPE_24_7 = 1;
        public const int BUSINESS_TYPE_ELECTRONICS = 2;
        public const int BUSINESS_TYPE_HARDWARE = 3;
        public const int BUSINESS_TYPE_CLOTHES = 4;
        public const int BUSINESS_TYPE_BAR = 5;
        public const int BUSINESS_TYPE_DISCO = 6;
        public const int BUSINESS_TYPE_AMMUNATION = 7;
        public const int BUSINESS_TYPE_WAREHOUSE = 8;
        public const int BUSINESS_TYPE_JEWELRY = 9;
        public const int BUSINESS_TYPE_PRIVATE_OFFICE = 10;
        public const int BUSINESS_TYPE_CLUBHOUSE_THE_LOST = 11;
        public const int BUSINESS_TYPE_GAS_STATION = 12;
        public const int BUSINESS_TYPE_SLAUGHTERHOUSE = 13;
        public const int BUSINESS_TYPE_BARBER_SHOP = 14;
        public const int BUSINESS_TYPE_FACTORY = 15;
        public const int BUSINESS_TYPE_TORTURE_ROOM = 16;
        public const int BUSINESS_TYPE_GARAGE_LOW_END = 17;
        public const int BUSINESS_TYPE_WAREHOUSE_MEDIUM = 18;
        public const int BUSINESS_TYPE_SOCIAL_CLUB = 19;
        public const int BUSINESS_TYPE_MECHANIC = 20;
        public const int BUSINESS_TYPE_TATTOO_SHOP = 21;
        public const int BUSINESS_TYPE_BENNYS_WHORKSHOP = 22;
        public const int BUSINESS_TYPE_VANILLA = 23;
        public const int BUSINESS_TYPE_FISHING = 24;
        public const int BUSINESS_TYPE_CLUBHOUSE_1 = 25;
        public const int BUSINESS_TYPE_CLUBHOUSE_2 = 26;
        public const int BUSINESS_TYPE_META_LAB = 27;
        public const int BUSINESS_TYPE_WEED_WAREHOUSE = 28;
        public const int BUSINESS_TYPE_COCAINE_LAB = 29;
        public const int BUSINESS_TYPE_CASH_FACTORY = 30;
        public const int BUSINESS_TYPE_DOCUMENT_OFFICE = 31;
        public const int BUSINESS_TYPE_WAREHOUSE_SMALL = 32;
        public const int BUSINESS_TYPE_WAREHOUSE_LARGE = 34;
        public const int BUSINESS_TYPE_VEHICLE_WAREHOUSE = 35;

        // Phone numbers
        public const int NUMBER_POLICE = 911;
        public const int NUMBER_EMERGENCY = 112;
        public const int NUMBER_NEWS = 114;
        public const int NUMBER_FASTFOOD = 115;
        public const int NUMBER_MECHANIC = 116;
        public const int NUMBER_TAXI = 555;

        // Parking types
        public const int PARKING_TYPE_PUBLIC = 0;
        public const int PARKING_TYPE_GARAGE = 1;
        public const int PARKING_TYPE_SCRAPYARD = 2;
        public const int PARKING_TYPE_DEPOSIT = 3;

        // Clothes bodyparts
        public const int CLOTHES_MASK = 1;
        public const int CLOTHES_TORSO = 3;
        public const int CLOTHES_LEGS = 4;
        public const int CLOTHES_BAGS = 5;
        public const int CLOTHES_FEET = 6;
        public const int CLOTHES_ACCESSORIES = 7;
        public const int CLOTHES_UNDERSHIRT = 8;
        public const int CLOTHES_ARMOR = 9;
        public const int CLOTHES_DECALS = 10;
        public const int CLOTHES_TOPS = 11;

        // Tattoo zones
        public const int TATTOO_ZONE_TORSO = 0;
        public const int TATTOO_ZONE_HEAD = 1;
        public const int TATTOO_ZONE_LEFT_ARM = 2;
        public const int TATTOO_ZONE_RIGHT_ARM = 3;
        public const int TATTOO_ZONE_LEFT_LEG = 4;
        public const int TATTOO_ZONE_RIGHT_LEG = 5;

        // Accessory types
        public const int ACCESSORY_HATS = 0;
        public const int ACCESSORY_GLASSES = 1;
        public const int ACCESSORY_EARS = 2;

        // Vehicle components
        public const int VEHICLE_MOD_SPOILER = 0;
        public const int VEHICLE_MOD_FRONT_BUMPER = 1;
        public const int VEHICLE_MOD_REAR_BUMPER = 2;
        public const int VEHICLE_MOD_SIDE_SKIRT = 3;
        public const int VEHICLE_MOD_EXHAUST = 4;
        public const int VEHICLE_MOD_FRAME = 5;
        public const int VEHICLE_MOD_GRILLE = 6;
        public const int VEHICLE_MOD_HOOD = 7;
        public const int VEHICLE_MOD_FENDER = 8;
        public const int VEHICLE_MOD_RIGHT_FENDER = 9;
        public const int VEHICLE_MOD_ROOF = 10;
        public const int VEHICLE_MOD_ENGINE = 11;
        public const int VEHICLE_MOD_BRAKES = 12;
        public const int VEHICLE_MOD_TRANSMISSION = 13;
        public const int VEHICLE_MOD_HORN = 14;
        public const int VEHICLE_MOD_SUSPENSION = 15;
        public const int VEHICLE_MOD_ARMOR = 16;
        public const int VEHICLE_MOD_XENON = 22;
        public const int VEHICLE_MOD_FRONT_WHEELS = 23;
        public const int VEHICLE_MOD_BACK_WHEELS = 24;
        public const int VEHICLE_MOD_PLATE_HOLDERS = 25;
        public const int VEHICLE_MOD_TRIM_DESIGN = 27;
        public const int VEHICLE_MOD_ORNAMIENTS = 28;
        public const int VEHICLE_MOD_DIAL_DESIGN = 30;
        public const int VEHICLE_MOD_STEERING_WHEEL = 33;
        public const int VEHICLE_MOD_SHIFTER_LEAVERS = 34;
        public const int VEHICLE_MOD_PLAQUES = 35;
        public const int VEHICLE_MOD_HYDRAULICS = 38;
        public const int VEHICLE_MOD_LIVERY = 48;
        public const int VEHICLE_MOD_WINDOW_TINT = 46;
        public const int VEHICLE_MOD_TURBO = 18;
        public const int VEHICLE_MOD_COLOUR1 = 66;
        public const int VEHICLE_MOD_COLOUR2 = 67;


        // Inventory targets
        public const int INVENTORY_TARGET_SELF = 0;
        public const int INVENTORY_TARGET_PLAYER = 1;
        public const int INVENTORY_TARGET_HOUSE = 2;
        public const int INVENTORY_TARGET_VEHICLE_TRUNK = 3;
        public const int INVENTORY_TARGET_VEHICLE_PLAYER = 4;

        // Item types
        public const int ITEM_TYPE_CONSUMABLE = 0;
        public const int ITEM_TYPE_EQUIPABLE = 1;
        public const int ITEM_TYPE_OPENABLE = 2;
        public const int ITEM_TYPE_WEAPON = 3;
        public const int ITEM_TYPE_AMMUNITION = 4;
        public const int ITEM_TYPE_MISC = 5;

        // Amount of items when container opened
        public const int ITEM_OPEN_BEER_AMOUNT = 6;

        // 24-7 items
        public const string ITEM_HASH_FRIES = "1443311452";
        public const string ITEM_HASH_HOTDOG = "2565741261";
        public const string ITEM_HASH_CHOCOLATE_BAR = "921283475";
        public const string ITEM_HASH_BURGER = "2240524752";
        public const string ITEM_HASH_SANDWICH = "3602873787";
        public const string ITEM_HASH_CANDY = "3310697493";

        public const string ITEM_HASH_CUP_JUICE = "3638960837";
        public const string ITEM_HASH_ENERGY_DRINK = "582043502";
        public const string ITEM_HASH_BOTTLE_WATER = "746336278";
        public const string ITEM_HASH_CUP_COFFEE = "3696781377";
        public const string ITEM_HASH_CAN_COLA = "1020618269";

        public const string ITEM_HASH_CUP_WINE = "2998419875";
        public const string ITEM_HASH_CUP_CHAMPANGE = "600913159";
        public const string ITEM_HASH_BOTTLE_BEER_PISSWASSER = "4016900153";
        public const string ITEM_HASH_BOTTLE_BEER_AM = "1350970027";
        public const string ITEM_HASH_PACK_BEER_AM = "4241316616";
        public const string ITEM_HASH_BOTTLE_COGNAC = "1404018125";
        public const string ITEM_HASH_BOTTLE_CAVA = "3846720762";

        public const string ITEM_HASH_CIGARRETES_PACK_OPEN = "1079465856";

        // Electronic items
        public const string ITEM_HASH_TELEPHONE = "2277609629";
        public const string ITEM_HASH_WALKIE = "1806057883";
        public const string ITEM_HASH_RADIO_CASSETTE = "1060029110";
        public const string ITEM_HASH_CAMERA = "680380202";

        // Ammunition items
        public const string ITEM_HASH_PISTOL_AMMO_CLIP = "PistolAmmo";
        public const string ITEM_HASH_MACHINEGUN_AMMO_CLIP = "SmgAmmo";
        public const string ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP = "RifleAmmo";
        public const string ITEM_HASH_SNIPERRIFLE_AMMO_CLIP = "SniperAmmo";
        public const string ITEM_HASH_SHOTGUN_AMMO_CLIP = "ShotgunAmmo";

        // Stack of the guns
        public const int STACK_PISTOL_CAPACITY = 32;
        public const int STACK_MACHINEGUN_CAPACITY = 100;
        public const int STACK_SHOTGUN_CAPACITY = 24;
        public const int STACK_ASSAULTRIFLE_CAPACITY = 60;
        public const int STACK_SNIPERRIFLE_CAPACITY = 8;

        // Miscelaneous items
        public const string ITEM_HASH_ID_CARD = "511938898";
        public const string ITEM_HASH_CUFFS = "1070220657";
        public const string ITEM_HASH_JERRYCAN = "1069395324";
        public const string ITEM_HASH_FISHING_ROD = "2384362703";
        public const string ITEM_HASH_STOLEN_OBJECTS = "Stolen";
        public const string ITEM_HASH_BUSINESS_PRODUCTS = "Products";
        public const string ITEM_HASH_BAIT = "Bait";
        public const string ITEM_HASH_FISH = "Fish";

        // Vehicle color types
        public const int VEHICLE_COLOR_TYPE_PREDEFINED = 0;
        public const int VEHICLE_COLOR_TYPE_CUSTOM = 1;

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

        // Tax percentage
        public const float TAXES_VEHICLE = 0.005f;
        public const float TAXES_HOUSE = 0.0015f;

        // Gargabe route money
        public const int MONEY_GARBAGE_ROUTE = 350;

        // Price in products
        public const int PRICE_VEHICLE_CHASSIS = 300;
        public const int PRICE_VEHICLE_DOORS = 60;
        public const int PRICE_VEHICLE_WINDOWS = 15;
        public const int PRICE_VEHICLE_TYRES = 10;
        public const int PRICE_BARBER_SHOP = 100;
        public const int PRICE_ANNOUNCEMENT = 500;
        public const int PRICE_DRIVING_THEORICAL = 200;
        public const int PRICE_DRIVING_PRACTICAL = 300;
        public const int PRICE_IDENTIFICATION = 500;
        public const int PRICE_MEDICAL_INSURANCE = 2000;
        public const int PRICE_TAXI_LICENSE = 5000;
        public const int PRICE_STOLEN = 20;
        public const int PRICE_PARKING_PUBLIC = 50;
        public const int PRICE_PARKING_DEPOSIT = 500;
        public const int PRICE_PIZZA = 60;
        public const int PRICE_HAMBURGER = 40;
        public const int PRICE_SANDWICH = 20;
        public const int PRICE_GAS = 5;
        public const int PRICE_FISH = 350;

        // Factions
        public const int FACTION_NONE = 0;
        public const int FACTION_POLICE = 1;
        public const int FACTION_EMERGENCY = 2;
        public const int FACTION_NEWS = 3;
        public const int FACTION_TOWNHALL = 4;
        public const int FACTION_DRIVING_SCHOOL = 5;
        public const int FACTION_TAXI_DRIVER = 6;
        public const int FACTION_SHERIFF = 7;
        public const int FACTION_LSC = 8;
        public const int FACTION_ADMIN = 9;
        public const int LAST_STATE_FACTION = 10;
        public const int MAX_FACTION_VEHICLES = 100;

        // Jobs
        public const int JOB_NONE = 0;
        public const int JOB_FASTFOOD = 1;
        public const int JOB_THIEF = 2;
        public const int JOB_MECHANIC = 3;
        public const int JOB_GARBAGE = 4;
        public const int JOB_HOOKER = 5;
        public const int JOB_FISHERMAN = 6;
        public const int JOB_TAXI = 7;
        public const int JOB_TRUCKER = 8;
        public const int JOB_LAWYER = 9;
       // public const int JOB_BURGERFLOPPER = 10;
        public const int JOB_OCEANCLEANER = 11;
        public const int JOB_DELIVERYMAN = 12;


        // Database stored items' place
        public const string ITEM_ENTITY_GROUND = "Ground";
        public const string ITEM_ENTITY_PLAYER = "Player";
        public const string ITEM_ENTITY_VEHICLE = "Vehicle";
        public const string ITEM_ENTITY_HOUSE = "House";
        public const string ITEM_ENTITY_WHEEL = "Wheel";
        public const string ITEM_ENTITY_LEFT_HAND = "Left hand";
        public const string ITEM_ENTITY_RIGHT_HAND = "Right hand";

        // Application test
        public const int APPLICATION_TEST = 0;

        // Driving school's licenses
        public const int LICENSE_CAR = 0;
        public const int LICENSE_MOTORCYCLE = 1;
        public const int LICENSE_TAXI = 2;
        public const int LICENSE_ROADSIDEMECHANIC = 3;

        // Driving school exam type
        public const int CAR_DRIVING_THEORICAL = 1;
        public const int CAR_DRIVING_PRACTICE = 2;
        public const int MOTORCYCLE_DRIVING_THEORICAL = 3;
        public const int MOTORCYCLE_DRIVING_PRACTICE = 4;
        public const int ROADSIDE_DRIVING_PRACTICE = 5;

        // Town hall formalities
        public const int TRAMITATE_IDENTIFICATION = 0;
        public const int TRAMITATE_MEDICAL_INSURANCE = 1;
        public const int TRAMITATE_TAXI_LICENSE = 2;
        public const int TRAMITATE_FINE_LIST = 3;

        // Bank operations
        public const int OPERATION_WITHDRAW = 1;
        public const int OPERATION_DEPOSIT = 2;
        public const int OPERATION_TRANSFER = 3;
        public const int OPERATION_BALANCE = 4;

        //Business status
        public const int BUSINESS_STATE_NONE = 0;
        public const int BUSINESS_STATE_BUYABLE = 1;


        // House status
        public const int HOUSE_STATE_NONE = 0;
        public const int HOUSE_STATE_RENTABLE = 1;
        public const int HOUSE_STATE_BUYABLE = 2;

        // Police control's items
        public const int POLICE_DEPLOYABLE_CONE = 1245865676;
        public const int POLICE_DEPLOYABLE_BEACON = 93871477;
        public const int POLICE_DEPLOYABLE_BARRIER = -143315610;
        public const int POLICE_DEPLOYABLE_SPIKES = -874338148;

        // Chat message types
        public const int MESSAGE_TALK = 0;
        public const int MESSAGE_SHOUT = 1;
        public const int MESSAGE_WHISPER = 2;
        public const int MESSAGE_ME = 3;
        public const int MESSAGE_DO = 4;
        public const int MESSAGE_OOC = 5;
        public const int MESSAGE_SU_TRUE = 6;
        public const int MESSAGE_SU_FALSE = 7;
        public const int MESSAGE_NEWS = 8;
        public const int MESSAGE_PHONE = 9;
        public const int MESSAGE_DISCONNECT = 10;
        public const int MESSAGE_MEGAPHONE = 11;
        public const int MESSAGE_RADIO = 12;
        public const int MESSAGE_AME = 13;

        // Chat colors
        public const string COLOR_WHITE = "!{#FFFFFF}";
        public const string COLOR_CHAT_CLOSE = "!{#E6E6E6}";
        public const string COLOR_CHAT_NEAR = "!{#C8C8C8}";
        public const string COLOR_CHAT_MEDIUM = "!{#AAAAAA}";
        public const string COLOR_CHAT_FAR = "!{#8C8C8C}";
        public const string COLOR_CHAT_LIMIT = "!{#6E6E6E}";
        public const string COLOR_CHAT_ME = "!{#C2A2DA}";
        public const string COLOR_CHAT_DO = "!{#0F9622}";
        public const string COLOR_CHAT_FACTION = "!{#27F7C8}";
        public const string COLOR_CHAT_PHONE = "!{#27F7C8}";
        public const string COLOR_OOC_CLOSE = "!{#4C9E9E}";
        public const string COLOR_OOC_NEAR = "!{#438C8C}";
        public const string COLOR_OOC_MEDIUM = "!{#2E8787}";
        public const string COLOR_OOC_FAR = "!{#187373}";
        public const string COLOR_OOC_LIMIT = "!{#0A5555}";
        public const string COLOR_ADMIN_INFO = "!{#00FCFF}";
        public const string COLOR_ADMIN_NEWS = "!{#F93131}";
        public const string COLOR_ADMIN_MP = "!{#F93131}";
        public const string COLOR_SUCCESS = "!{#33B517}";
        public const string COLOR_ERROR = "!{#F62323}";
        public const string COLOR_INFO = "!{#FDFE8B}";
        public const string COLOR_HELP = "!{#FFFFFF}";
        public const string COLOR_SU_POSITIVE = "!{#E3E47D}";
        public const string COLOR_RADIO = "!{#1598C4}";
        public const string COLOR_RADIO_POLICE = "!{#4169E1}";
        public const string COLOR_RADIO_EMERGENCY = "!{#FF9F0F}";
        public const string COLOR_NEWS = "!{#805CC9}";
        public const string COLOR_YELLOW = "!{#FFFF00}";
        public const string COLOR_OCEANBLUE = "!{#3399ff}";
        public const string COLOR_SANDYORANGE = "!{#FF6600}";
        public const string COLOR_DOLLARGREEN = "!{#33CC33}";








        // Gargabe collector's routes
        public const int NORTH_ROUTE = 0;
        public const int EAST_ROUTE = 1;
        public const int SOUTH_ROUTE = 2;
        public const int WEST_ROUTE = 3;

        //Lawyer services
        public const int LAWYER_SERVICE_BASIC = 0;
        public const int LAWYER_SERVICE_FULL = 1;

        // Hooker's services
        public const int HOOKER_SERVICE_BASIC = 0;
        public const int HOOKER_SERVICE_FULL = 1;

        // Alcohol limit
        public const float WASTED_LEVEL = 0.4f;

        // Generic interiors
        public static List<InteriorModel> INTERIOR_LIST = new List<InteriorModel>
        {
            new InteriorModel(GenRes.townhall, new Vector3(-136.4768f, 6198.505f, 32.38424f), new Vector3(-141.1987f, -620.913f, 168.8205f), "ex_dt1_02_office_02b", 498, GenRes.townhall),
            new InteriorModel(GenRes.hospital, new Vector3(-248.6233f, 6331.584f, 32.42619f), new Vector3(275.446f, -1361.11f, 24.5378f), "Coroner_Int_On", 153, GenRes.hospital),
            new InteriorModel(GenRes.hospital, new Vector3(343.8997f,-1398.888f,32.50927f), new Vector3(275.446f, -1361.11f, 24.5378f), "Coroner_Int_On", 153, GenRes.hospital),
            new InteriorModel(GenRes.hospital, new Vector3(355.4596f,-596.2038f,28.77325f), new Vector3(275.446f, -1361.11f, 24.5378f), "rc12b_fixed", 153, GenRes.hospital),
            new InteriorModel(GenRes.driving_school, new Vector3(-227.6895f, 6333.742f, 32.41962f), new Vector3(-227.6895f, 6333.742f, 32.41962f), string.Empty, 269, GenRes.driving_school),
            new InteriorModel(GenRes.driving_school, new Vector3(-915.0544f, -2038.445f, 9.404984f), new Vector3(-227.6895f, 6333.742f, 32.41962f), string.Empty, 269, GenRes.driving_school),
            new InteriorModel(GenRes.weazel_news, new Vector3(-145.5413f, 6304.209f, 31.55754f), new Vector3(-1082.433f, -258.7667f, 37.76331f), "facelobby", 459, GenRes.weazel_news),
            new InteriorModel(GenRes.casino, new Vector3(935.7157f, 47.38577f, 81.09578f), new Vector3(965.2189f, 58.5619f, 112.553f), "vw_casino_main﻿", 628, GenRes.casino)
    };

        // Business IPLs from the game
        public static List<BusinessIplModel> BUSINESS_IPL_LIST = new List<BusinessIplModel>
        {
            new BusinessIplModel(BUSINESS_TYPE_24_7, "ipl_supermarket", new Vector3(-710.1048f, -914.5465f, 19.21559f)),
            new BusinessIplModel(BUSINESS_TYPE_ELECTRONICS, "ex_exec_warehouse_placement_interior_2_int_warehouse_l_dlc_milo", new Vector3(1026.751f, -3101.307f, -38.99986f)),
            new BusinessIplModel(BUSINESS_TYPE_HARDWARE, "v_chopshop", new Vector3(481.9714f, -1313.103f, 29.20123f)),
            new BusinessIplModel(BUSINESS_TYPE_CLOTHES, "ipl_clothes", new Vector3(126.5524f, -212.5681f, 54.55783f)),
            new BusinessIplModel(BUSINESS_TYPE_DISCO, "v_bahama", new Vector3(-1387.981f, -587.6373f, 30.31952f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE, "v_recycle", new Vector3(-593.5312f, -1630.137f, 27.01079f)),
            new BusinessIplModel(BUSINESS_TYPE_AMMUNATION, "ipl_ammu", new Vector3(1698.488f, 3752.896f, 34.70532f)),
            new BusinessIplModel(BUSINESS_TYPE_BAR, "v_rockclub", new Vector3(-564.4153f, 277.4367f, 83.13631f)),
            new BusinessIplModel(BUSINESS_TYPE_JEWELRY, "post_hiest_unload", new Vector3(-630.4483f, -236.8936f, 38.05701f)),
            new BusinessIplModel(BUSINESS_TYPE_PRIVATE_OFFICE, "v_psycheoffice", new Vector3(-1906.785f, -573.757f, 19.077f)),
            new BusinessIplModel(BUSINESS_TYPE_CLUBHOUSE_THE_LOST, "bkr_bi_hw1_13_int", new Vector3(982.4059f, -100.1532f, 74.84502f)),
            new BusinessIplModel(BUSINESS_TYPE_GAS_STATION, "ipl_supermarket", new Vector3(-710.1048f, -914.5465f, 19.21559f)),
            new BusinessIplModel(BUSINESS_TYPE_SLAUGHTERHOUSE, "ipl_slaughterhouse", new Vector3(964.3511f, -2185.115f, 30.30081f)),
            new BusinessIplModel(BUSINESS_TYPE_BARBER_SHOP, "barber_shop", new Vector3(133.9966f, -1710.311f, 29.29162f)),
            new BusinessIplModel(BUSINESS_TYPE_FACTORY, "id2_14_during1", new Vector3(717.0f, -975.0f, 25.0f)),
            new BusinessIplModel(BUSINESS_TYPE_TORTURE_ROOM, "v_torture", new Vector3(135.7002f, -2203.643f, 7.309135f)),
            new BusinessIplModel(BUSINESS_TYPE_GARAGE_LOW_END, "low_end_garage_no_ipl", new Vector3(178.8302f, -1000.515f, -98.99998f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE_MEDIUM, "ex_exec_warehouse_placement_interior_0_int_warehouse_m_dlc_milo", new Vector3(1048.286f, -3096.858f, -38.99991f)),
            new BusinessIplModel(BUSINESS_TYPE_SOCIAL_CLUB, "house_no_ipl_a", new Vector3(265.9776f, -1006.97f, -100.8839f)),
            new BusinessIplModel(BUSINESS_TYPE_TATTOO_SHOP, "business_no_ipl", new Vector3(-1154.249f, -1424.721f, 4.954462f)),
            new BusinessIplModel(BUSINESS_TYPE_BENNYS_WHORKSHOP, "business_no_ipl2", new Vector3(-205.4454f, -1312.916f, 31.13982f)),
            new BusinessIplModel(BUSINESS_TYPE_MECHANIC, "v_chopshop", new Vector3(481.9714f, -1313.103f, 29.20123f)),
            new BusinessIplModel(BUSINESS_TYPE_VANILLA, "vanilla_no_ipl", new Vector3(128.9892f, -1296.068f, 29.26953f)),
            new BusinessIplModel(BUSINESS_TYPE_FISHING, "ex_exec_warehouse_placement_interior_0_int_warehouse_m_dlc_milo", new Vector3(1048.286f, -3096.858f, -38.99991f)),
            new BusinessIplModel(BUSINESS_TYPE_CLUBHOUSE_1, "bkr_biker_interior_placement_interior_0_biker_dlc_int_01_milo", new Vector3(1107.04f, -3157.399f, -37.51859f)),
            new BusinessIplModel(BUSINESS_TYPE_CLUBHOUSE_2, "bkr_biker_interior_placement_interior_1_biker_dlc_int_02_milo", new Vector3(998.4809f, -3164.711f, -38.90733f)),
            new BusinessIplModel(BUSINESS_TYPE_META_LAB, "bkr_biker_interior_placement_interior_2_biker_dlc_int_ware01_milo", new Vector3(1009.5f, -3196.6f, -38.99682f)),
            new BusinessIplModel(BUSINESS_TYPE_WEED_WAREHOUSE, "bkr_biker_interior_placement_interior_3_biker_dlc_int_ware02_milo", new Vector3(1051.491f, -3196.536f, -39.14842f)),
            new BusinessIplModel(BUSINESS_TYPE_COCAINE_LAB, "bkr_biker_interior_placement_interior_4_biker_dlc_int_ware03_milo", new Vector3(1093.6f, -3196.6f, -38.99841f)),
            new BusinessIplModel(BUSINESS_TYPE_CASH_FACTORY, "bkr_biker_interior_placement_interior_5_biker_dlc_int_ware04_milo", new Vector3(1121.897f, -3195.338f, -40.4025f)),
            new BusinessIplModel(BUSINESS_TYPE_DOCUMENT_OFFICE, "bkr_biker_interior_placement_interior_6_biker_dlc_int_ware05_milo", new Vector3(1165f, -3196.6f, -39.01306f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE_SMALL, "ex_exec_warehouse_placement_interior_1_int_warehouse_s_dlc_milo", new Vector3(1094.988f, -3101.776f, -39.00363f)),
            new BusinessIplModel(BUSINESS_TYPE_WAREHOUSE_LARGE, "ex_exec_warehouse_placement_interior_2_int_warehouse_l_dlc_milo", new Vector3(1006.967f, -3102.079f, -39.0035f)),
            new BusinessIplModel(BUSINESS_TYPE_VEHICLE_WAREHOUSE, "imp_impexp_interior_placement_interior_1_impexp_intwaremed_milo_", new Vector3(994.5925f, -3002.594f, -39.64699f)),
        };

        // House interiors from the game
        public static List<HouseIplModel> HOUSE_IPL_LIST = new List<HouseIplModel>
        {
            // Apartments with IPL
            new HouseIplModel("apa_v_mp_h_01_a", new Vector3(-786.8663f, 315.7642f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_01_c", new Vector3(-786.9563f, 315.6229f, 187.9136f)),
            new HouseIplModel("apa_v_mp_h_01_b", new Vector3(-774.0126f, 342.0428f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_02_a", new Vector3(-787.0749f, 315.8198f, 217.6386f)),
            new HouseIplModel("apa_v_mp_h_02_c", new Vector3(-786.8195f, 315.5634f, 187.9137f)),
            new HouseIplModel("apa_v_mp_h_02_b", new Vector3(-774.1382f, 342.0316f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_03_a", new Vector3(-786.6245f, 315.6175f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_03_c", new Vector3(-786.9584f, 315.7974f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_03_b", new Vector3(-774.0223f, 342.1718f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_04_a", new Vector3(-787.0902f, 315.7039f, 217.6384f)),
            new HouseIplModel("apa_v_mp_h_04_c", new Vector3(-787.0155f, 315.7071f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_04_b", new Vector3(-773.8976f, 342.1525f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_05_a", new Vector3(-786.9887f, 315.7393f, 217.6386f)),
            new HouseIplModel("apa_v_mp_h_05_c", new Vector3(-786.8809f, 315.6634f, 187.9136f)),
            new HouseIplModel("apa_v_mp_h_05_b", new Vector3(-774.0675f, 342.0773f, 196.6864f)),
            new HouseIplModel("apa_v_mp_h_06_a", new Vector3(-787.1423f, 315.6943f, 217.6384f)),
            new HouseIplModel("apa_v_mp_h_06_c", new Vector3(-787.0961f, 315.815f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_06_b", new Vector3(-773.9552f, 341.9892f, 196.6862f)),
            new HouseIplModel("apa_v_mp_h_07_a", new Vector3(-787.029f, 315.7113f, 217.6385f)),
            new HouseIplModel("apa_v_mp_h_07_c", new Vector3(-787.0574f, 315.6567f, 187.9135f)),
            new HouseIplModel("apa_v_mp_h_07_b", new Vector3(-774.0109f, 342.0965f, 196.6863f)),
            new HouseIplModel("apa_v_mp_h_08_a", new Vector3(-786.9469f, 315.5655f, 217.6383f)),
            new HouseIplModel("apa_v_mp_h_08_c", new Vector3(-786.9756f, 315.723f, 187.9134f)),
            new HouseIplModel("apa_v_mp_h_08_b", new Vector3(-774.0349f, 342.0296f, 196.6862f)),

            // Apartments without IPL
            new HouseIplModel("house_no_ipl_a", new Vector3(265.9776f, -1006.97f, -100.8839f)),
            new HouseIplModel("house_no_ipl_b", new Vector3(-30.58078f, -595.3096f, 80.03086f)),
            new HouseIplModel("house_no_ipl_c", new Vector3(-30.58078f, -595.3096f, 80.03086f)),
            new HouseIplModel("house_no_ipl_d", new Vector3(-17.72512f, -588.8995f, 90.1148f)),
            new HouseIplModel("house_no_ipl_e", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_f", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_g", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_h", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_i", new Vector3(-174.2659f, 497.3836f, 137.667f)),
            new HouseIplModel("house_no_ipl_j", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_k", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_l", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_m", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_n", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_o", new Vector3(-1451.652f, -523.7687f, 56.92904f)),
            new HouseIplModel("house_no_ipl_p", new Vector3(-1451.652f, -523.7687f, 56.92904f)),

            // Trevor's trailer
            new HouseIplModel("TrevorsMP", new Vector3(1972.965f, 3816.529f, 33.42873f)),
            new HouseIplModel("TrevorsTrailerTidy", new Vector3(1972.965f, 3816.529f, 33.42873f)),

            // Floyd's house
            new HouseIplModel("vb_30_crimetape", new Vector3(-1149.709f, -1521.088f, 10.78267f)),

            // Lester's house
            new HouseIplModel("lester_house", new Vector3(1273.9f, -1719.305f, 54.77141f)),

            // Janitor's office
            new HouseIplModel("v_janitor", new Vector3(-107.6496f, -8.308348f, 70.51957f)),

            // Mansion
            new HouseIplModel("mansion_no_ipl", new Vector3(1396.415f, 1141.854f, 114.3336f)),

            // Flat with rooms
            new HouseIplModel("flat_no_ipl", new Vector3(346.3212f, -1012.968f, -99.19625f)),

            // Franklin's aunt's house
            new HouseIplModel("franklin_hoo_house", new Vector3(-14.31764f, -1439.986f, 31.10155f)),

            // Franklin's mansion
            new HouseIplModel("franklin_mansion_no_ipl", new Vector3(7.69007f, 538.0661f, 176.028f)),

            // O'Neill's farm
            new HouseIplModel("farmint", new Vector3(2436.875f, 4974.916f, 46.8106f)),

            // Motel room
            new HouseIplModel("motel_room_no_ipl", new Vector3(151.1905f, -1007.731f, -98.99999f))
        };

        // Faction ranks
        public static List<FactionModel> FACTION_RANK_LIST = new List<FactionModel>
        {
            new FactionModel(JobRes.none_m, JobRes.none_f, FACTION_NONE, 0, 0),

            // Police department
            new FactionModel(JobRes.lspd_0_m, JobRes.lspd_0_f, FACTION_POLICE, 0, 0),
            new FactionModel(JobRes.lspd_1_m, JobRes.lspd_1_f, FACTION_POLICE, 1, 1250),
            new FactionModel(JobRes.lspd_2_m, JobRes.lspd_2_f, FACTION_POLICE, 2, 1388),
            new FactionModel(JobRes.lspd_3_m, JobRes.lspd_3_f, FACTION_POLICE, 3, 1685),
            new FactionModel(JobRes.lspd_4_m, JobRes.lspd_4_f, FACTION_POLICE, 4, 2056),
            new FactionModel(JobRes.lspd_5_m, JobRes.lspd_5_f, FACTION_POLICE, 5, 2420),
            new FactionModel(JobRes.lspd_6_m, JobRes.lspd_6_f, FACTION_POLICE, 6, 2901),
            new FactionModel(JobRes.lspd_7_m, JobRes.lspd_7_f, FACTION_POLICE, 7, 2200),

            // Emergency department
            new FactionModel(JobRes.ems_1_m, JobRes.ems_1_f, FACTION_EMERGENCY, 1, 1075),
            new FactionModel(JobRes.ems_2_m, JobRes.ems_2_f, FACTION_EMERGENCY, 2, 1200),
            new FactionModel(JobRes.ems_3_m, JobRes.ems_3_f, FACTION_EMERGENCY, 3, 1500),
            new FactionModel(JobRes.ems_4_m, JobRes.ems_4_f, FACTION_EMERGENCY, 4, 1500),
            new FactionModel(JobRes.ems_5_m, JobRes.ems_5_f, FACTION_EMERGENCY, 5, 1800),
            new FactionModel(JobRes.ems_6_m, JobRes.ems_6_f, FACTION_EMERGENCY, 6, 1800),
            new FactionModel(JobRes.ems_7_m, JobRes.ems_7_f, FACTION_EMERGENCY, 7, 2200),
            new FactionModel(JobRes.ems_8_m, JobRes.ems_8_f, FACTION_EMERGENCY, 8, 2200),
            new FactionModel(JobRes.ems_9_m, JobRes.ems_9_f, FACTION_EMERGENCY, 9, 2800),
            new FactionModel(JobRes.ems_10_m, JobRes.ems_10_f, FACTION_EMERGENCY, 10, 3500),

            // News
            new FactionModel(JobRes.news_1_m, JobRes.news_1_f, FACTION_NEWS, 1, 1020),
            new FactionModel(JobRes.news_2_m, JobRes.news_2_f, FACTION_NEWS, 2, 1100),
            new FactionModel(JobRes.news_3_m, JobRes.news_3_f, FACTION_NEWS, 3, 1200),
            new FactionModel(JobRes.news_4_m, JobRes.news_4_f, FACTION_NEWS, 4, 1610),
            new FactionModel(JobRes.news_5_m, JobRes.news_5_f, FACTION_NEWS, 5, 2300),

            // Town hall
            new FactionModel(JobRes.town_1_m, JobRes.town_1_f, FACTION_TOWNHALL, 1, 1200),
            new FactionModel(JobRes.town_2_m, JobRes.town_2_f, FACTION_TOWNHALL, 2, 1800),
            new FactionModel(JobRes.town_3_m, JobRes.town_3_f, FACTION_TOWNHALL, 3, 2200),
            new FactionModel(JobRes.town_4_m, JobRes.town_4_f, FACTION_TOWNHALL, 4, 3000),

            // Transport services
            new FactionModel(JobRes.lstd_1_m, JobRes.lstd_1_f, FACTION_TAXI_DRIVER, 1, 1020),
            new FactionModel(JobRes.lstd_2_m, JobRes.lstd_2_f, FACTION_TAXI_DRIVER, 2, 1180),
            new FactionModel(JobRes.lstd_3_m, JobRes.lstd_3_f, FACTION_TAXI_DRIVER, 3, 1360),
            new FactionModel(JobRes.lstd_4_m, JobRes.lstd_4_f, FACTION_TAXI_DRIVER, 4, 1600),
            new FactionModel(JobRes.lstd_5_m, JobRes.lstd_5_f, FACTION_TAXI_DRIVER, 5, 1890),

            // Sherif
            new FactionModel(JobRes.lssd_1_m, JobRes.lssd_1_f, FACTION_SHERIFF, 1, 1250),
            new FactionModel(JobRes.lssd_2_m, JobRes.lssd_2_f, FACTION_SHERIFF, 2, 1388),
            new FactionModel(JobRes.lssd_3_m, JobRes.lssd_3_f, FACTION_SHERIFF, 3, 1685),
            new FactionModel(JobRes.lssd_4_m, JobRes.lssd_4_f, FACTION_SHERIFF, 4, 2056),
            new FactionModel(JobRes.lssd_5_m, JobRes.lssd_5_f, FACTION_SHERIFF, 5, 2420),
            new FactionModel(JobRes.lssd_6_m, JobRes.lssd_6_f, FACTION_SHERIFF, 6, 2901)
        };

        // Job description and salary
        public static List<JobModel> JOB_LIST = new List<JobModel>
        {
            new JobModel(JobRes.unemployed_m, JobRes.unemployed_f, JOB_NONE, 575),
            new JobModel(JobRes.fastfood_m, JobRes.fastfood_f, JOB_FASTFOOD, 775),
           // new JobModel(JobRes.fastfood_m, JobRes.fastfood_f, JOB_BURGERFLOPPER, 775),
            new JobModel(JobRes.thief_m, JobRes.thief_f, JOB_THIEF, 450),
            new JobModel(JobRes.mechanic_m, JobRes.mechanic_f, JOB_MECHANIC, 875),
            new JobModel(JobRes.gargage_m, JobRes.gargage_f, JOB_GARBAGE, 975),
            new JobModel(JobRes.hooker_m, JobRes.hooker_f, JOB_HOOKER, 575),
            new JobModel(JobRes.lawyer_m, JobRes.lawyer_f, JOB_LAWYER, 1000),
            new JobModel(JobRes.trucker_m, JobRes.trucker_f, JOB_TRUCKER, 1075)
        };

        // Job commands
        public static Dictionary<int, List<string>> JOB_COMMANDS = new Dictionary<int, List<string>>
        {
          //  { JOB_BURGERFLOPPER, new List<string> { Commands.COM_ORDERS } },
            { JOB_FASTFOOD, new List<string> { Commands.COM_ORDERS } },
            { JOB_THIEF, new List<string> { Commands.COM_FORCE, Commands.COM_STEAL, Commands.COM_HOTWIRE, Commands.COM_PAWN } },
            { JOB_MECHANIC, new List<string> { Commands.COM_REPAIR, Commands.COM_REPAINT, Commands.COM_TUNNING } },
            { JOB_GARBAGE, new List<string> { Commands.COM_GARBAGE } },
            { JOB_LAWYER, new List<string> { Commands.COM_LAWYER } },
            { JOB_HOOKER, new List<string> { Commands.COM_SERVICE } },
            { JOB_TRUCKER, new List<string> { Commands.COM_ORDERS } }
        };

        // Uniform list
        public static List<UniformModel> UNIFORM_LIST = new List<UniformModel>
        {  
            // Male police uniform
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 0, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 1, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 3, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 4, 35, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 5, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 6, 25, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 7, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 8, 58, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 9, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 10, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_MALE, 11, 55, 0),

            // Female police uniform
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 0, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 1, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 3, 14, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 4, 34, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 5, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 6, 25, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 7, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 8, 35, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 9, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 10, 0, 0),
            new UniformModel(0, FACTION_POLICE, SEX_FEMALE, 11, 48, 0),

            // Male paramedic uniform
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 0, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 1, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 3, 90, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 4, 96, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 5, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 6, 51, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 7, 126, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 8, 15, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 9, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 10, 57, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_MALE, 11,249, 0),

            // Female paramedic uniform
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 0, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 1, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 3, 14, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 4, 6, 2),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 5, -1, -1),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 6, 1, 3),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 7, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 8, 2, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 9, 0, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 10, 58, 0),
            new UniformModel(0, FACTION_EMERGENCY, SEX_FEMALE, 11, 9, 2),

            // Male Sheriff uniform
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 0, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 1, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 3, 11, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 4, 24, 6),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 5, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 6, 51, 3),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 7, 38, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 8, 58, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 9, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 10, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_MALE, 11, 26, 2),

            // Female Sheriff uniform
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 0, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 1, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 3, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 4, 6, 1),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 5, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 6, 55, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 7, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 8, 35, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 9, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 10, 0, 0),
            new UniformModel(0, FACTION_SHERIFF, SEX_FEMALE, 11, 27, 2),

            // Male fastfood uniform
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 0, 76, 17),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 1, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 3, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 4, 4, 1),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 5, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 6, 7, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 7, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 8, 15, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 9, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 10, 0, 0),
            new UniformModel(1, JOB_FASTFOOD, SEX_MALE, 11, 9, 14),
                        // Male fastfood uniform
          /*  new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 0, 76, 17),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 1, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 3, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 4, 4, 1),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 5, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 6, 7, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 7, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 8, 15, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 9, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 10, 0, 0),
            new UniformModel(1, JOB_BURGERFLOPPER, SEX_MALE, 11, 9, 14),
        */
        // Male Ocean Cleaner Uniform
          //  new UniformModel(1, JOB_OCEANCLEANER, SEX_MALE, 1, 38, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_MALE, 4, 94, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_MALE, 6, 67, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_MALE, 8, 57, 0), // Invisible Chest
            new UniformModel(1, JOB_OCEANCLEANER, SEX_MALE, 11, 53, 0), 


                        // Female Ocean Cleaner Uniform
           // new UniformModel(1, JOB_OCEANCLEANER, SEX_FEMALE, 1, 38, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_FEMALE, 4, 94, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_FEMALE, 6, 67, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_FEMALE, 8, 57, 0),
            new UniformModel(1, JOB_OCEANCLEANER, SEX_FEMALE, 11, 46, 0)

        };
        // Guns
        public static List<GunModel> GUN_LIST = new List<GunModel>()
        {
            // Pistols
            new GunModel(WeaponHash.Pistol, ITEM_HASH_PISTOL_AMMO_CLIP, 12),
            new GunModel(WeaponHash.CombatPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 12),
            new GunModel(WeaponHash.Pistol50, ITEM_HASH_PISTOL_AMMO_CLIP, 9),
            new GunModel(WeaponHash.SNSPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 6),
            new GunModel(WeaponHash.HeavyPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 18),
            new GunModel(WeaponHash.VintagePistol, ITEM_HASH_PISTOL_AMMO_CLIP, 7),
            new GunModel(WeaponHash.MarksmanPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 1),
            new GunModel(WeaponHash.Revolver, ITEM_HASH_PISTOL_AMMO_CLIP, 6),
            new GunModel(WeaponHash.APPistol, ITEM_HASH_PISTOL_AMMO_CLIP, 18),
            new GunModel(WeaponHash.FlareGun, ITEM_HASH_PISTOL_AMMO_CLIP, 1),

            // Machine guns
            new GunModel(WeaponHash.MicroSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 16),
            new GunModel(WeaponHash.MachinePistol, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 12),
            new GunModel(WeaponHash.SMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.AssaultSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CombatPDW, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.MG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 54),
            new GunModel(WeaponHash.CombatMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 100),
            new GunModel(WeaponHash.Gusenberg, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 30),
            new GunModel(WeaponHash.MiniSMG, ITEM_HASH_MACHINEGUN_AMMO_CLIP, 20),

            // Assault rifles
            new GunModel(WeaponHash.AssaultRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CarbineRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.AdvancedRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.SpecialCarbine, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.BullpupRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),
            new GunModel(WeaponHash.CompactRifle, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, 30),

            // Sniper rifles
            new GunModel(WeaponHash.SniperRifle, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 10),
            new GunModel(WeaponHash.HeavySniper, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 6),
            new GunModel(WeaponHash.MarksmanRifle, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, 8),

            // Shotguns
            new GunModel(WeaponHash.PumpShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.SawnOffShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.BullpupShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 14),
            new GunModel(WeaponHash.AssaultShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 8),
            new GunModel(WeaponHash.Musket, ITEM_HASH_SHOTGUN_AMMO_CLIP, 1),
            new GunModel(WeaponHash.HeavyShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 6),
            new GunModel(WeaponHash.DoubleBarrelShotgun, ITEM_HASH_SHOTGUN_AMMO_CLIP, 2)
        };

        // Jail positions
        public static List<Vector3> JAIL_SPAWNS = new List<Vector3>
        {
            // Cells
            new Vector3(460.0685f, -993.9847f, 24.91487f),
            new Vector3(459.6115f, -998.0204f, 24.91487f),
            new Vector3(459.8612f, -1001.641f, 24.91487f),

            // IC jail's exit
            new Vector3(463.6655f, -990.8979f, 24.91487f),

            // OOC jail's exit
            new Vector3(-1285.544f, -567.0439f, 31.71239f)
        };

        // Business sellable items
        public static List<BusinessItemModel> BUSINESS_ITEM_LIST = new List<BusinessItemModel>
        {
            // 24-7
            new BusinessItemModel(ItemRes.beer_bottle, ITEM_HASH_BOTTLE_BEER_AM, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.08f),
            new BusinessItemModel(ItemRes.beer_pack, ITEM_HASH_PACK_BEER_AM, ITEM_TYPE_OPENABLE, 60, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.sandwich, ITEM_HASH_SANDWICH, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 10, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(180.0f, 180.0f, 90.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.cigarettes, ITEM_HASH_CIGARRETES_PACK_OPEN, ITEM_TYPE_CONSUMABLE, 8, 0.1f, -2, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.cola, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.05f, -0.03f, 0.0f), new Vector3(270.0f, 20.0f, -20.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.candy, ITEM_HASH_CANDY, ITEM_TYPE_CONSUMABLE, 4, 0.1f, 3, 1, new Vector3(0.05f, -0.010f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.jerrycan, ITEM_HASH_JERRYCAN, ITEM_TYPE_EQUIPABLE, 25, 0.1f, 0, 1, new Vector3(0.09f, 0.09f, 0.0f), new Vector3(0.0f, 90.0f, 175.0f), BUSINESS_TYPE_24_7, 0.0f),
            new BusinessItemModel(ItemRes.coffee, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_24_7, 0.0f),
 
            // Oil station
            new BusinessItemModel(ItemRes.jerrycan, ITEM_HASH_JERRYCAN, ITEM_TYPE_EQUIPABLE, 25, 0.1f, 0, 1, new Vector3(0.09f, 0.09f, 0.0f), new Vector3(0.0f, 90.0f, 175.0f), BUSINESS_TYPE_GAS_STATION, 0.0f),
 
            // Electronic store
            new BusinessItemModel(ItemRes.smartphone, ITEM_HASH_TELEPHONE, ITEM_TYPE_EQUIPABLE, 200, 0.1f, 0, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),
            new BusinessItemModel(ItemRes.walkie, ITEM_HASH_WALKIE, ITEM_TYPE_EQUIPABLE, 150, 0.1f, 0, 1, new Vector3(0.06f, 0.0f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),
            new BusinessItemModel(ItemRes.camera, ITEM_HASH_CAMERA, ITEM_TYPE_EQUIPABLE, 50, 0.1f, 0, 1, new Vector3(0.05f, -0.02f, -0.02f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_ELECTRONICS, 0.0f),

            // Hardware store
            new BusinessItemModel(ItemRes.crowbar, WeaponHash.Crowbar.ToString(), ITEM_TYPE_WEAPON, 60, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.hammer, WeaponHash.Hammer.ToString(), ITEM_TYPE_WEAPON, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.flashlight, WeaponHash.Flashlight.ToString(), ITEM_TYPE_WEAPON, 30, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.hatchet, WeaponHash.Hatchet.ToString(), ITEM_TYPE_WEAPON, 200, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.wrench, WeaponHash.Wrench.ToString(), ITEM_TYPE_WEAPON, 100, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.knucle_duster, WeaponHash.KnuckleDuster.ToString(), ITEM_TYPE_WEAPON, 100, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.knife, WeaponHash.Knife.ToString(), ITEM_TYPE_WEAPON, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.switchblade, WeaponHash.SwitchBlade.ToString(), ITEM_TYPE_WEAPON, 150, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
            new BusinessItemModel(ItemRes.bat, WeaponHash.Bat.ToString(), ITEM_TYPE_WEAPON, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_HARDWARE, 0.0f),
 
            // Bar
            new BusinessItemModel(ItemRes.beer_bottle, ITEM_HASH_BOTTLE_BEER_PISSWASSER, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.08f),
            new BusinessItemModel(ItemRes.burger, ITEM_HASH_BURGER, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 20, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(ItemRes.coffee, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(ItemRes.cola, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
            new BusinessItemModel(ItemRes.hotdog, ITEM_HASH_HOTDOG, ITEM_TYPE_CONSUMABLE, 2, 0.1f, 15, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_BAR, 0.0f),
 
            // Clubhouse
            new BusinessItemModel(ItemRes.beer_bottle, ITEM_HASH_BOTTLE_BEER_PISSWASSER, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE_THE_LOST, 0.08f),
            new BusinessItemModel(ItemRes.burger, ITEM_HASH_BURGER, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 20, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE_THE_LOST, 0.0f),
            new BusinessItemModel(ItemRes.coffee, ITEM_HASH_CUP_COFFEE, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE_THE_LOST, 0.0f),
            new BusinessItemModel(ItemRes.cola, ITEM_HASH_CAN_COLA, ITEM_TYPE_CONSUMABLE, 5, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE_THE_LOST, 0.0f),
            new BusinessItemModel(ItemRes.hotdog, ITEM_HASH_HOTDOG, ITEM_TYPE_CONSUMABLE, 2, 0.1f, 15, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLUBHOUSE_THE_LOST, 0.0f),
 
            // Disco
            new BusinessItemModel(ItemRes.beer_bottle, ITEM_HASH_BOTTLE_BEER_AM, ITEM_TYPE_CONSUMABLE, 10, 0.1f, 1, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(270.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.08f),
            new BusinessItemModel(ItemRes.juice, ITEM_HASH_CUP_JUICE, ITEM_TYPE_CONSUMABLE, 6, 0.1f, 10, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.0f),
            new BusinessItemModel(ItemRes.energy_drink, ITEM_HASH_ENERGY_DRINK, ITEM_TYPE_CONSUMABLE, 6, 0.1f, 5, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.0f),
            new BusinessItemModel(ItemRes.cava_bottle, ITEM_HASH_BOTTLE_CAVA, ITEM_TYPE_CONSUMABLE, 70, 0.1f, 15, 5, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_DISCO, 0.05f),
 
            // Ammu-Nation
            new BusinessItemModel(ItemRes.pistol, WeaponHash.Pistol.ToString(), ITEM_TYPE_WEAPON, 2000, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(ItemRes.pistol_ammo, ITEM_HASH_PISTOL_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 300, 0.1f, 0, STACK_PISTOL_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(ItemRes.smg_ammo, ITEM_HASH_MACHINEGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 500, 0.1f, 0, STACK_MACHINEGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(ItemRes.shotgun_ammo, ITEM_HASH_SHOTGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 400, 0.1f, 0, STACK_SHOTGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(ItemRes.rifle_ammo, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 1000, 0.1f, 0, STACK_ASSAULTRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),
            new BusinessItemModel(ItemRes.sniper_ammo, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 2000, 0.1f, 0, STACK_SNIPERRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_AMMUNATION, 0.0f),

            // Clothes store
            new BusinessItemModel(ItemRes.bat, WeaponHash.Bat.ToString(), ITEM_TYPE_WEAPON, 300, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLOTHES, 0.0f),
            new BusinessItemModel(ItemRes.golf_club, WeaponHash.GolfClub.ToString(), ITEM_TYPE_WEAPON, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_CLOTHES, 0.0f),
 
            // Fishing store
            new BusinessItemModel(ItemRes.fishing_rod, ITEM_HASH_FISHING_ROD, ITEM_TYPE_EQUIPABLE, 250, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_FISHING, 0.0f),
            new BusinessItemModel(ItemRes.bait, ITEM_HASH_BAIT, ITEM_TYPE_MISC, 10, 0.1f, 0, 10, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_FISHING, 0.0f),
 
            // Miscellaneous
            new BusinessItemModel(ItemRes.fish, ITEM_HASH_FISH, ITEM_TYPE_MISC, 0, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.products, ITEM_HASH_BUSINESS_PRODUCTS, ITEM_TYPE_MISC, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.stolen_items, ITEM_HASH_STOLEN_OBJECTS, ITEM_TYPE_MISC, 50, 0.1f, 0, 1, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.bullpup_shotgun, WeaponHash.BullpupShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.compact_rifle, WeaponHash.CompactRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.carbine_rifle, WeaponHash.CarbineRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.heavy_shotgun, WeaponHash.HeavyShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.sawn_off_shotgun, WeaponHash.SawnOffShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.bullpup_rifle, WeaponHash.BullpupRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.assault_rifle, WeaponHash.AssaultRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.ap_pistol, WeaponHash.APPistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.double_barrel_shotgun, WeaponHash.DoubleBarrelShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.machine_pistol, WeaponHash.MachinePistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.sniper_rifle, WeaponHash.SniperRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.assault_smg, WeaponHash.AssaultSMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.combat_pdw, WeaponHash.CombatPDW.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.revolver, WeaponHash.Revolver.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.heavy_pistol, WeaponHash.HeavyPistol.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.pump_shotgun, WeaponHash.PumpShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.special_carbine, WeaponHash.SpecialCarbine.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.pistol_50, WeaponHash.Pistol50.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.advanced_rifle, WeaponHash.AdvancedRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.heavy_sniper, WeaponHash.HeavySniper.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.micro_smg, WeaponHash.MicroSMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.assault_shotgun, WeaponHash.AssaultShotgun.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.marksman_rifle, WeaponHash.MarksmanRifle.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.smg, WeaponHash.SMG.ToString(), ITEM_TYPE_WEAPON, 0, 0.1f, 0, 0, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.pistol_ammo, ITEM_HASH_PISTOL_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_PISTOL_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.smg_ammo, ITEM_HASH_MACHINEGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_MACHINEGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.shotgun_ammo, ITEM_HASH_SHOTGUN_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_SHOTGUN_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.rifle_ammo, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_ASSAULTRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f),
            new BusinessItemModel(ItemRes.sniper_ammo, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, ITEM_TYPE_AMMUNITION, 0, 0.1f, 0, STACK_SNIPERRIFLE_CAPACITY, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 0.0f, 0.0f), BUSINESS_TYPE_NONE, 0.0f)
        };

        // Clothes list
        public static List<BusinessClothesModel> BUSINESS_CLOTHES_LIST = new List<BusinessClothesModel>
        {
            // Masks
            new BusinessClothesModel(0, "Pig", CLOTHES_MASK, 1, SEX_NONE, 50),
            new BusinessClothesModel(0, "Skull", CLOTHES_MASK, 2, SEX_NONE, 50),
            new BusinessClothesModel(0, "Monkey smoker", CLOTHES_MASK, 3, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hockey", CLOTHES_MASK, 4, SEX_NONE, 50),
            new BusinessClothesModel(0, "Happy monkey", CLOTHES_MASK, 5, SEX_NONE, 50),
            new BusinessClothesModel(0, "Sinister thing", CLOTHES_MASK, 6, SEX_NONE, 50),
            new BusinessClothesModel(0, "Gargoyle", CLOTHES_MASK, 7, SEX_NONE, 50),
            new BusinessClothesModel(0, "Santa", CLOTHES_MASK, 8, SEX_NONE, 50),
            new BusinessClothesModel(0, "Reindeer", CLOTHES_MASK, 9, SEX_NONE, 50),
            new BusinessClothesModel(0, "Frosty", CLOTHES_MASK, 10, SEX_NONE, 50),
            new BusinessClothesModel(0, "Mask", CLOTHES_MASK, 11, SEX_NONE, 50),
            new BusinessClothesModel(0, "Venetian Pinocchio", CLOTHES_MASK, 12, SEX_NONE, 50),
            new BusinessClothesModel(0, "Cupid", CLOTHES_MASK, 13, SEX_NONE, 50),
            new BusinessClothesModel(0, "Ballistics", CLOTHES_MASK, 14, SEX_NONE, 50),
            new BusinessClothesModel(0, "Skull hockey", CLOTHES_MASK, 15, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hannibal Lecter", CLOTHES_MASK, 16, SEX_NONE, 50),
            new BusinessClothesModel(0, "Cat", CLOTHES_MASK, 17, SEX_NONE, 50),
            new BusinessClothesModel(0, "Zorro", CLOTHES_MASK, 18, SEX_NONE, 50),
            new BusinessClothesModel(0, "Owl", CLOTHES_MASK, 19, SEX_NONE, 50),
            new BusinessClothesModel(0, "Badger", CLOTHES_MASK, 20, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bear", CLOTHES_MASK, 21, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bison", CLOTHES_MASK, 22, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bull", CLOTHES_MASK, 23, SEX_NONE, 50),
            new BusinessClothesModel(0, "Eagle", CLOTHES_MASK, 24, SEX_NONE, 50),
            new BusinessClothesModel(0, "Cloudy crane", CLOTHES_MASK, 25, SEX_NONE, 50),
            new BusinessClothesModel(0, "Wolf", CLOTHES_MASK, 26, SEX_NONE, 50),
            new BusinessClothesModel(0, "Aviator hat", CLOTHES_MASK, 27, SEX_NONE, 50),
            new BusinessClothesModel(0, "Black skull", CLOTHES_MASK, 29, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hockey Jason", CLOTHES_MASK, 30, SEX_NONE, 50),
            new BusinessClothesModel(0, "Penguin", CLOTHES_MASK, 31, SEX_NONE, 50),
            new BusinessClothesModel(0, "Red stocking", CLOTHES_MASK, 32, SEX_NONE, 50),
            new BusinessClothesModel(0, "Happy ginger", CLOTHES_MASK, 33, SEX_NONE, 50),
            new BusinessClothesModel(0, "Elf", CLOTHES_MASK, 34, SEX_NONE, 50),
            new BusinessClothesModel(0, "Ski mask", CLOTHES_MASK, 35, SEX_NONE, 50),
            new BusinessClothesModel(0, "Black stocking", CLOTHES_MASK, 37, SEX_NONE, 50),
            new BusinessClothesModel(0, "Zombie", CLOTHES_MASK, 39, SEX_NONE, 50),
            new BusinessClothesModel(0, "Mummy", CLOTHES_MASK, 40, SEX_NONE, 50),
            new BusinessClothesModel(0, "Vampire", CLOTHES_MASK, 41, SEX_NONE, 50),
            new BusinessClothesModel(0, "Rebuilt", CLOTHES_MASK, 42, SEX_NONE, 50),
            new BusinessClothesModel(0, "Superhero", CLOTHES_MASK, 43, SEX_NONE, 50),
            new BusinessClothesModel(0, "Waifu", CLOTHES_MASK, 44, SEX_NONE, 50),
            new BusinessClothesModel(0, "Detective", CLOTHES_MASK, 45, SEX_NONE, 50),
            new BusinessClothesModel(0, "Police tape", CLOTHES_MASK, 47, SEX_NONE, 50),
            new BusinessClothesModel(0, "Tape", CLOTHES_MASK, 48, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bag", CLOTHES_MASK, 49, SEX_NONE, 50),
            new BusinessClothesModel(0, "Statue", CLOTHES_MASK, 50, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bandana", CLOTHES_MASK, 51, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hood", CLOTHES_MASK, 53, SEX_NONE, 50),
            new BusinessClothesModel(0, "T shirt", CLOTHES_MASK, 54, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hat", CLOTHES_MASK, 55, SEX_NONE, 50),
            new BusinessClothesModel(0, "Balaclava blue", CLOTHES_MASK, 56, SEX_NONE, 50),
            new BusinessClothesModel(0, "Balaclava wool", CLOTHES_MASK, 57, SEX_NONE, 50),
            new BusinessClothesModel(0, "Grated balaclava", CLOTHES_MASK, 58, SEX_NONE, 50),
            new BusinessClothesModel(0, "Werewolf", CLOTHES_MASK, 59, SEX_NONE, 50),
            new BusinessClothesModel(0, "Evil pumpkin", CLOTHES_MASK, 60, SEX_NONE, 50),
            new BusinessClothesModel(0, "Old zombie", CLOTHES_MASK, 61, SEX_NONE, 50),
            new BusinessClothesModel(0, "Freddy Krueger", CLOTHES_MASK, 62, SEX_NONE, 50),
            new BusinessClothesModel(0, "Shingeki no kyojin", CLOTHES_MASK, 63, SEX_NONE, 50),
            new BusinessClothesModel(0, "Vomited skull", CLOTHES_MASK, 64, SEX_NONE, 50),
            new BusinessClothesModel(0, "Pissed wolf dog", CLOTHES_MASK, 65, SEX_NONE, 50),
            new BusinessClothesModel(0, "Flycatcher with tongue", CLOTHES_MASK, 66, SEX_NONE, 50),
            new BusinessClothesModel(0, "Mordor Orc", CLOTHES_MASK, 67, SEX_NONE, 50),
            new BusinessClothesModel(0, "Horned demon", CLOTHES_MASK, 68, SEX_NONE, 50),
            new BusinessClothesModel(0, "Boogeyman", CLOTHES_MASK, 69, SEX_NONE, 50),
            new BusinessClothesModel(0, "Mexican zombie skull", CLOTHES_MASK, 70, SEX_NONE, 50),
            new BusinessClothesModel(0, "Witch", CLOTHES_MASK, 71, SEX_NONE, 50),
            new BusinessClothesModel(0, "Tanned horned demon", CLOTHES_MASK, 72, SEX_NONE, 50),
            new BusinessClothesModel(0, "Hairless", CLOTHES_MASK, 73, SEX_NONE, 50),
            new BusinessClothesModel(0, "Tan Angry Ginger", CLOTHES_MASK, 74, SEX_NONE, 50),
            new BusinessClothesModel(0, "Angry ginger", CLOTHES_MASK, 75, SEX_NONE, 50),
            new BusinessClothesModel(0, "Santa Claus cabin boy", CLOTHES_MASK, 76, SEX_NONE, 50),
            new BusinessClothesModel(0, "Shabby Christmas Tree", CLOTHES_MASK, 77, SEX_NONE, 50),
            new BusinessClothesModel(0, "Chocolate sponge cake with pastry cream", CLOTHES_MASK, 78, SEX_NONE, 50),
            new BusinessClothesModel(0, "very hairy werewolfery hairy werewolf", CLOTHES_MASK, 79, SEX_NONE, 50),
            new BusinessClothesModel(0, "Werewolf with LS cap", CLOTHES_MASK, 80, SEX_NONE, 50),
            new BusinessClothesModel(0, "Werewolf ready to play tennis", CLOTHES_MASK, 81, SEX_NONE, 50),
            new BusinessClothesModel(0, "Werewolf gym", CLOTHES_MASK, 82, SEX_NONE, 50),
            new BusinessClothesModel(0, "Werewolf wishes you Merry Christmas", CLOTHES_MASK, 83, SEX_NONE, 50),
            new BusinessClothesModel(0, "Bored Snow Yeti", CLOTHES_MASK, 84, SEX_NONE, 50),
            new BusinessClothesModel(0, "Chicken stuffed face", CLOTHES_MASK, 85, SEX_NONE, 50),
            new BusinessClothesModel(0, "Old very very past everything", CLOTHES_MASK, 86, SEX_NONE, 50),
            new BusinessClothesModel(0, "Grandpa with bad milk", CLOTHES_MASK, 87, SEX_NONE, 50),
            new BusinessClothesModel(0, "Old after project man", CLOTHES_MASK, 88, SEX_NONE, 50),
            new BusinessClothesModel(0, "Black biker type", CLOTHES_MASK, 89, SEX_NONE, 50),
            new BusinessClothesModel(0, "Half face nose red mouth", CLOTHES_MASK, 90, SEX_NONE, 50),
            new BusinessClothesModel(0, "Space helmet", CLOTHES_MASK, 91, SEX_NONE, 50),
            new BusinessClothesModel(0, "Teen Cthulhu", CLOTHES_MASK, 92, SEX_NONE, 50),
            new BusinessClothesModel(0, "T-REX", CLOTHES_MASK, 93, SEX_NONE, 50),
            new BusinessClothesModel(0, "Oni, Japanese demon", CLOTHES_MASK, 94, SEX_NONE, 50),
            new BusinessClothesModel(0, "Graceless clown", CLOTHES_MASK, 95, SEX_NONE, 50),
            new BusinessClothesModel(0, "King Kong", CLOTHES_MASK, 96, SEX_NONE, 50),
            new BusinessClothesModel(0, "Horse", CLOTHES_MASK, 97, SEX_NONE, 50),
            new BusinessClothesModel(0, "Unicorn", CLOTHES_MASK, 98, SEX_NONE, 50),
            new BusinessClothesModel(0, "Red skull with golden strokes", CLOTHES_MASK, 99, SEX_NONE, 50),
            new BusinessClothesModel(0, "PUG", CLOTHES_MASK, 100, SEX_NONE, 50),
            new BusinessClothesModel(0, "BIGNESS half face nose mouth", CLOTHES_MASK, 101, SEX_NONE, 50),
            new BusinessClothesModel(0, "Drawn by children", CLOTHES_MASK, 102, SEX_NONE, 50),

            // Female pants
            new BusinessClothesModel(0, "1 Skinny jeans", CLOTHES_LEGS, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "2 Wide jeans", CLOTHES_LEGS, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "3 Pirate tracksuit", CLOTHES_LEGS, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "4 Pants with belt", CLOTHES_LEGS, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "5 Pirate jeans", CLOTHES_LEGS, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "6 Short checkered", CLOTHES_LEGS, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "7 Formal trousers", CLOTHES_LEGS, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "8 Black skirt", CLOTHES_LEGS, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "9 Mini skirt", CLOTHES_LEGS, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "10 Mini skirt with sequins", CLOTHES_LEGS, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "11 Sport short", CLOTHES_LEGS, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "12 Rolled up jeans", CLOTHES_LEGS, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "13 Checked miniskirt", CLOTHES_LEGS, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "14 Gray shorts", CLOTHES_LEGS, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "15 Black bikini", CLOTHES_LEGS, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "16 Yellow shorts", CLOTHES_LEGS, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "17 White bikini", CLOTHES_LEGS, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "18 Red skirt", CLOTHES_LEGS, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "19 White underwear", CLOTHES_LEGS, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "20 Underwear with garter belt and stocking", CLOTHES_LEGS, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "21 Red bikini", CLOTHES_LEGS, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "22 White formal", CLOTHES_LEGS, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "23 Printed skirt", CLOTHES_LEGS, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "24 Denim shorts", CLOTHES_LEGS, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "25 Leopard miniskirt", CLOTHES_LEGS, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "26 Leggins", CLOTHES_LEGS, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "27 Striped miniskirt", CLOTHES_LEGS, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "28 Wide leg pants", CLOTHES_LEGS, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "29 Wide pants with pockets", CLOTHES_LEGS, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "30 Red leggings", CLOTHES_LEGS, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "31 Wide leg pants with knee pads", CLOTHES_LEGS, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "32 Formal trousers with belt", CLOTHES_LEGS, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "33 Green pants with reflective", CLOTHES_LEGS, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "34 Grey skirt", CLOTHES_LEGS, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "35 Black formal", CLOTHES_LEGS, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "36 Wide red", CLOTHES_LEGS, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "37 Wide trousers", CLOTHES_LEGS, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "38 Wide with grips", CLOTHES_LEGS, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "39 Ripped jeans", CLOTHES_LEGS, 43, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "40 Side ripped jeans", CLOTHES_LEGS, 44, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "41 Width with pockets", CLOTHES_LEGS, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "42 Formal", CLOTHES_LEGS, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "43 Widths with pockets", CLOTHES_LEGS, 48, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "44 Wide with belt", CLOTHES_LEGS, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "45 Formal", CLOTHES_LEGS, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "46 Tight", CLOTHES_LEGS, 51, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "47 Formal", CLOTHES_LEGS, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "48 Formal floral motifs", CLOTHES_LEGS, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "49 Tight", CLOTHES_LEGS, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "50 Tight with floral patterns", CLOTHES_LEGS, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "51 Pink bikini", CLOTHES_LEGS, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "52 Robe bottom", CLOTHES_LEGS, 57, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "53 Striped tracksuit", CLOTHES_LEGS, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "54 Check pajamas", CLOTHES_LEGS, 60, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "55 Lace culotte", CLOTHES_LEGS, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "56 Underwear with garter belt and stockings", CLOTHES_LEGS, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "57 Camel formal trousers", CLOTHES_LEGS, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "58 Striped tracksuit", CLOTHES_LEGS, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "59 Striped pyjamas", CLOTHES_LEGS, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "60 Black and white tracksuit", CLOTHES_LEGS, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "61 Width with skulls", CLOTHES_LEGS, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "62 Tight jeans", CLOTHES_LEGS, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "63 Ripped skinny jeans", CLOTHES_LEGS, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "64 Tight jeans", CLOTHES_LEGS, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "65 Denim shorts with stockings", CLOTHES_LEGS, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "66 Brown tracksuit", CLOTHES_LEGS, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "67 Black tracksuit", CLOTHES_LEGS, 81, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "68 Brown pirate tracksuit", CLOTHES_LEGS, 82, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "69 Black pirate tracksuit", CLOTHES_LEGS, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "70 Shitty jeans", CLOTHES_LEGS, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "71 Baggy pants", CLOTHES_LEGS, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "72 Camouflage leggings", CLOTHES_LEGS, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "73 Matching striped leggings", CLOTHES_LEGS, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "74 Printed pants", CLOTHES_LEGS, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "75 Bermuda shorts with prints", CLOTHES_LEGS, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "77 Wide pants with prints", CLOTHES_LEGS, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "76 High waisted jeans", CLOTHES_LEGS, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "77 Green widths with grips", CLOTHES_LEGS, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "78 Wide red with grips", CLOTHES_LEGS, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "79 Tight with stripes", CLOTHES_LEGS, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "80 Formal with belt", CLOTHES_LEGS, 99, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "81 Two-tone wide trousers", CLOTHES_LEGS, 101, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "82 Adjusted with belt", CLOTHES_LEGS, 102, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "83 Blue widths", CLOTHES_LEGS, 103, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "84 Wide with floral motifs", CLOTHES_LEGS, 104, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "85 Motif tracksuit", CLOTHES_LEGS, 105, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "86 Tight with zippers", CLOTHES_LEGS, 106, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "87 Yellow shorts", CLOTHES_LEGS, 107, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "88 Mini skirt with sequins", CLOTHES_LEGS, 108, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "89 Widths with pockets", CLOTHES_LEGS, 109, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "90 Wide with pirate pockets", CLOTHES_LEGS, 110, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "91 Light jeans", CLOTHES_LEGS, 111, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "92 Tight", CLOTHES_LEGS, 112, SEX_FEMALE, 50),

            // Male pants
            new BusinessClothesModel(0, "dark blue jeans black belt", CLOTHES_LEGS, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "jeans with visible slip", CLOTHES_LEGS, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "short black and white squares", CLOTHES_LEGS, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "white tracksuit", CLOTHES_LEGS, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "black skinny jeans", CLOTHES_LEGS, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide white tracksuit", CLOTHES_LEGS, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "short white", CLOTHES_LEGS, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal black black belt", CLOTHES_LEGS, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide dark gray", CLOTHES_LEGS, 8, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide dark green belt", CLOTHES_LEGS, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal black belt", CLOTHES_LEGS, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "short black", CLOTHES_LEGS, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal black belt", CLOTHES_LEGS, 13, SEX_MALE, 50),
            new BusinessClothesModel(0, "gray and white pants", CLOTHES_LEGS, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "short light brown", CLOTHES_LEGS, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "short pink and blue", CLOTHES_LEGS, 16, SEX_MALE, 50),
            new BusinessClothesModel(0, "short dark brown", CLOTHES_LEGS, 17, SEX_MALE, 50),
            new BusinessClothesModel(0, "yellow pants", CLOTHES_LEGS, 18, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal red with black belt", CLOTHES_LEGS, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal white black belt", CLOTHES_LEGS, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "black slip red hearts", CLOTHES_LEGS, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal gray", CLOTHES_LEGS, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "black skinny", CLOTHES_LEGS, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal black black buckle", CLOTHES_LEGS, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "black leather", CLOTHES_LEGS, 26, SEX_MALE, 50),
            new BusinessClothesModel(0, "yellow width", CLOTHES_LEGS, 27, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide red and white stripes", CLOTHES_LEGS, 29, SEX_MALE, 50),
            new BusinessClothesModel(0, "width dark green grips", CLOTHES_LEGS, 30, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide black skinny", CLOTHES_LEGS, 31, SEX_MALE, 50),
            new BusinessClothesModel(0, "tight red", CLOTHES_LEGS, 32, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide black knee pads", CLOTHES_LEGS, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide reflective dark green", CLOTHES_LEGS, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal brown dark belt", CLOTHES_LEGS, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide dark red", CLOTHES_LEGS, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide black grips", CLOTHES_LEGS, 41, SEX_MALE, 50),
            new BusinessClothesModel(0, "black tracksuit", CLOTHES_LEGS, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark normal cowboy", CLOTHES_LEGS, 43, SEX_MALE, 50),
            new BusinessClothesModel(0, "black fitted tracksuit", CLOTHES_LEGS, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide brown knee pads", CLOTHES_LEGS, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide brown black belt", CLOTHES_LEGS, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal light brown black belt", CLOTHES_LEGS, 48, SEX_MALE, 50),
            new BusinessClothesModel(0, "skinny brown light black belt", CLOTHES_LEGS, 49, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal dark red black belt", CLOTHES_LEGS, 50, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal dark brown flowers", CLOTHES_LEGS, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "skinny dark red black belt", CLOTHES_LEGS, 52, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark skinny brown flowers", CLOTHES_LEGS, 53, SEX_MALE, 50),
            new BusinessClothesModel(0, "short dark dark blue stripes", CLOTHES_LEGS, 54, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark white striped tracksuit", CLOTHES_LEGS, 55, SEX_MALE, 50),
            new BusinessClothesModel(0, "white bow skirt", CLOTHES_LEGS, 56, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal red checkered", CLOTHES_LEGS, 58, SEX_MALE, 50),
            new BusinessClothesModel(0, "normal dark blue and black stripes", CLOTHES_LEGS, 60, SEX_MALE, 50),
            new BusinessClothesModel(0, "white pants", CLOTHES_LEGS, 61, SEX_MALE, 50),
            new BusinessClothesModel(0, "black pirate yellow belt", CLOTHES_LEGS, 62, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark wide denim", CLOTHES_LEGS, 63, SEX_MALE, 50),
            new BusinessClothesModel(0, "blue tracksuit with white stripes", CLOTHES_LEGS, 64, SEX_MALE, 50),
            new BusinessClothesModel(0, "plain white striped", CLOTHES_LEGS, 65, SEX_MALE, 50),
            new BusinessClothesModel(0, "black and white tracksuit", CLOTHES_LEGS, 66, SEX_MALE, 50),
            new BusinessClothesModel(0, "wide black with white skulls", CLOTHES_LEGS, 69, SEX_MALE, 50),
            new BusinessClothesModel(0, "black leather", CLOTHES_LEGS, 71, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark wide denim black belt", CLOTHES_LEGS, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "ripped cowboy", CLOTHES_LEGS, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "phosphor cigarette motif", CLOTHES_LEGS, 77, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark red tracksuit", CLOTHES_LEGS, 78, SEX_MALE, 50),
            new BusinessClothesModel(0, "black skinny tracksuit", CLOTHES_LEGS, 79, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark brown pirate", CLOTHES_LEGS, 80, SEX_MALE, 50),
            new BusinessClothesModel(0, "black pirate", CLOTHES_LEGS, 81, SEX_MALE, 50),
            new BusinessClothesModel(0, "dark skinny jeans", CLOTHES_LEGS, 82, SEX_MALE, 50),
            new BusinessClothesModel(0, "black skinny", CLOTHES_LEGS, 83, SEX_MALE, 50),
            new BusinessClothesModel(0, "fitted with colorful motifs", CLOTHES_LEGS, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "23", CLOTHES_LEGS, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "28", CLOTHES_LEGS, 28, SEX_MALE, 50),
            new BusinessClothesModel(0, "33", CLOTHES_LEGS, 33, SEX_MALE, 50),
            new BusinessClothesModel(0, "35", CLOTHES_LEGS, 35, SEX_MALE, 50),
            new BusinessClothesModel(0, "39", CLOTHES_LEGS, 39, SEX_MALE, 50),
            new BusinessClothesModel(0, "40", CLOTHES_LEGS, 40, SEX_MALE, 50),
            new BusinessClothesModel(0, "57", CLOTHES_LEGS, 57, SEX_MALE, 50),
            new BusinessClothesModel(0, "59", CLOTHES_LEGS, 59, SEX_MALE, 50),
            new BusinessClothesModel(0, "67", CLOTHES_LEGS, 67, SEX_MALE, 50),
            new BusinessClothesModel(0, "68", CLOTHES_LEGS, 68, SEX_MALE, 50),
            new BusinessClothesModel(0, "70", CLOTHES_LEGS, 70, SEX_MALE, 50),
            new BusinessClothesModel(0, "72", CLOTHES_LEGS, 72, SEX_MALE, 50),
            new BusinessClothesModel(0, "73", CLOTHES_LEGS, 73, SEX_MALE, 50),
            new BusinessClothesModel(0, "74", CLOTHES_LEGS, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "84", CLOTHES_LEGS, 84, SEX_MALE, 50),
            new BusinessClothesModel(0, "85", CLOTHES_LEGS, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "86", CLOTHES_LEGS, 86, SEX_MALE, 50),
            new BusinessClothesModel(0, "87", CLOTHES_LEGS, 87, SEX_MALE, 50),
            new BusinessClothesModel(0, "88", CLOTHES_LEGS, 88, SEX_MALE, 50),
            new BusinessClothesModel(0, "89", CLOTHES_LEGS, 89, SEX_MALE, 50),
            new BusinessClothesModel(0, "90", CLOTHES_LEGS, 90, SEX_MALE, 50),
            new BusinessClothesModel(0, "91", CLOTHES_LEGS, 91, SEX_MALE, 50),
            new BusinessClothesModel(0, "92", CLOTHES_LEGS, 92, SEX_MALE, 50),
            new BusinessClothesModel(0, "93", CLOTHES_LEGS, 93, SEX_MALE, 50),
            new BusinessClothesModel(0, "94", CLOTHES_LEGS, 94, SEX_MALE, 50),
            new BusinessClothesModel(0, "95", CLOTHES_LEGS, 95, SEX_MALE, 50),
            new BusinessClothesModel(0, "96", CLOTHES_LEGS, 96, SEX_MALE, 50),
            new BusinessClothesModel(0, "97", CLOTHES_LEGS, 97, SEX_MALE, 50),
            new BusinessClothesModel(0, "98", CLOTHES_LEGS, 98, SEX_MALE, 50),
            new BusinessClothesModel(0, "99", CLOTHES_LEGS, 99, SEX_MALE, 50),
            new BusinessClothesModel(0, "100", CLOTHES_LEGS, 100, SEX_MALE, 50),
            new BusinessClothesModel(0, "101", CLOTHES_LEGS, 101, SEX_MALE, 50),
            new BusinessClothesModel(0, "102", CLOTHES_LEGS, 102, SEX_MALE, 50),
            new BusinessClothesModel(0, "103", CLOTHES_LEGS, 103, SEX_MALE, 50),
            new BusinessClothesModel(0, "104", CLOTHES_LEGS, 104, SEX_MALE, 50),
            new BusinessClothesModel(0, "105", CLOTHES_LEGS, 105, SEX_MALE, 50),
            new BusinessClothesModel(0, "106", CLOTHES_LEGS, 106, SEX_MALE, 50),
            new BusinessClothesModel(0, "107", CLOTHES_LEGS, 107, SEX_MALE, 50),
            new BusinessClothesModel(0, "108", CLOTHES_LEGS, 108, SEX_MALE, 50),
            new BusinessClothesModel(0, "109", CLOTHES_LEGS, 109, SEX_MALE, 50),
            new BusinessClothesModel(0, "110", CLOTHES_LEGS, 110, SEX_MALE, 50),
            new BusinessClothesModel(0, "111", CLOTHES_LEGS, 111, SEX_MALE, 50),
            new BusinessClothesModel(0, "112", CLOTHES_LEGS, 112, SEX_MALE, 50),
            new BusinessClothesModel(0, "113", CLOTHES_LEGS, 113, SEX_MALE, 50),
            new BusinessClothesModel(0, "114", CLOTHES_LEGS, 114, SEX_MALE, 50),

            // Bags
            new BusinessClothesModel(0, "Gray, white and black with blue stripes", CLOTHES_BAGS, 1, SEX_NONE, 50),
            new BusinessClothesModel(0, "USA flag", CLOTHES_BAGS, 10, SEX_NONE, 50),
            new BusinessClothesModel(0, "White blue cross", CLOTHES_BAGS, 21, SEX_NONE, 50),
            new BusinessClothesModel(0, "Black", CLOTHES_BAGS, 31, SEX_NONE, 50),
            new BusinessClothesModel(0, "Big brown", CLOTHES_BAGS, 40, SEX_NONE, 50),
            new BusinessClothesModel(0, "Big black", CLOTHES_BAGS, 44, SEX_NONE, 50),
            new BusinessClothesModel(0, "Green and black", CLOTHES_BAGS, 52, SEX_NONE, 50),
            new BusinessClothesModel(0, "41", CLOTHES_BAGS, 41, SEX_NONE, 50),
            new BusinessClothesModel(0, "45", CLOTHES_BAGS, 45, SEX_NONE, 50),
            new BusinessClothesModel(0, "66", CLOTHES_BAGS, 66, SEX_NONE, 50),

            // Female shoes
            new BusinessClothesModel(0, "Black round heel", CLOTHES_FEET, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and gray sneakers", CLOTHES_FEET, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown pelito boots", CLOTHES_FEET, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Converse black and white low ankle", CLOTHES_FEET, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Dark gray and white trainers", CLOTHES_FEET, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black flip flops", CLOTHES_FEET, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black pointed heel", CLOTHES_FEET, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black ankle boots", CLOTHES_FEET, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black open ankle boots", CLOTHES_FEET, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black long boots", CLOTHES_FEET, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and purple trainers", CLOTHES_FEET, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Purple and white sneakers", CLOTHES_FEET, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black plane", CLOTHES_FEET, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black strap heel", CLOTHES_FEET, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and silver sandal", CLOTHES_FEET, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gray flip flops", CLOTHES_FEET, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Green Goblin", CLOTHES_FEET, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gray round heel", CLOTHES_FEET, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown round heel", CLOTHES_FEET, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Leopard toe heel", CLOTHES_FEET, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Yellow boots", CLOTHES_FEET, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown ankle boots", CLOTHES_FEET, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Blue round heel", CLOTHES_FEET, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black boots", CLOTHES_FEET, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black boots with metal studs", CLOTHES_FEET, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black sneakers", CLOTHES_FEET, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black sneakers brand side", CLOTHES_FEET, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal tip", CLOTHES_FEET, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black ankle boots with buckle", CLOTHES_FEET, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold sneakers", CLOTHES_FEET, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Yellow, gray, black and white trainers", CLOTHES_FEET, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Converse yellow and white black heaters", CLOTHES_FEET, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Barefoot", CLOTHES_FEET, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown boots", CLOTHES_FEET, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal dark brown", CLOTHES_FEET, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown toe cowgirl boot", CLOTHES_FEET, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal brown cowgirl tip", CLOTHES_FEET, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Round heel light brown", CLOTHES_FEET, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Yellow ankle boots", CLOTHES_FEET, 43, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Yellow and black ankle boots", CLOTHES_FEET, 44, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Light blue cowgirl boot pointed", CLOTHES_FEET, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal light blue cowgirl tip", CLOTHES_FEET, 46, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and blue sneakers", CLOTHES_FEET, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Green and black boots", CLOTHES_FEET, 48, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Converse black and white high ankle", CLOTHES_FEET, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Converse yellow and white high ankle", CLOTHES_FEET, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Dark brown buckle boot", CLOTHES_FEET, 51, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal dark brown", CLOTHES_FEET, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "High heel brown boot", CLOTHES_FEET, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black boot with studs and zipper", CLOTHES_FEET, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal black studs", CLOTHES_FEET, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black buckle and zip boot", CLOTHES_FEET, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Normal black buckle", CLOTHES_FEET, 57, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black trainers with phosphory yellow stripes", CLOTHES_FEET, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown plane", CLOTHES_FEET, 59, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Camel sneakers", CLOTHES_FEET, 60, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black plane pink and light blue stripes", CLOTHES_FEET, 61, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "62 military pattern ankle boot", CLOTHES_FEET, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "63 black military high boot", CLOTHES_FEET, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "64 black military ankle boot", CLOTHES_FEET, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "65 camouflage military high boot", CLOTHES_FEET, 65, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "66 camouflage military ankle boot", CLOTHES_FEET, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "67 high velcro shoe", CLOTHES_FEET, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "68 timberland high boot", CLOTHES_FEET, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "69 timberland ankle boot", CLOTHES_FEET, 69, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "70 diver fins", CLOTHES_FEET, 70, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "71 virtual pattern socks", CLOTHES_FEET, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "72 river boots", CLOTHES_FEET, 72, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "73 high color snow boots", CLOTHES_FEET, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "74 color ankle snow boots", CLOTHES_FEET, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "75 black tall snow boots", CLOTHES_FEET, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "76 black ankle snow boots", CLOTHES_FEET, 76, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "77 high heeled platform boot", CLOTHES_FEET, 77, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "78 high velcro decorated sneaker", CLOTHES_FEET, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "79 sporty running modern high", CLOTHES_FEET, 79, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "80 sporty running modern ankle", CLOTHES_FEET, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "81 shoe sole with light", CLOTHES_FEET, 81, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "82 slipper be at home", CLOTHES_FEET, 82, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "83 high boot with flames", CLOTHES_FEET, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "84 flamed ankle boot", CLOTHES_FEET, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "85 black high reinforced boot", CLOTHES_FEET, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "86 black reinforced ankle boot", CLOTHES_FEET, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "87 multicolour socks", CLOTHES_FEET, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "88 mad max high reinforced boot", CLOTHES_FEET, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "89 mad max high boot", CLOTHES_FEET, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "90 mad max ankle boot", CLOTHES_FEET, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "91 wool boot", CLOTHES_FEET, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "92 costume boot", CLOTHES_FEET, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "93 dirty velcro high shoe", CLOTHES_FEET, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "94 costume boot colors", CLOTHES_FEET, 93, SEX_FEMALE, 50),

            // Male shoes
            new BusinessClothesModel(0, "1 Black and white plaid high ankle sneakers", CLOTHES_FEET, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "2 Black sneakers white sole", CLOTHES_FEET, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "3 Black and white plaid low ankle sneakers", CLOTHES_FEET, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "4 Normal dark brown and gray", CLOTHES_FEET, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "5 Converse dark blue high ankle", CLOTHES_FEET, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "6 Black and white flip flops", CLOTHES_FEET, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "7 Black flip flops white socks", CLOTHES_FEET, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "8 White sneakers high socks", CLOTHES_FEET, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "9 White sneakers brand high socks", CLOTHES_FEET, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "10 Black shoes pointed high socks", CLOTHES_FEET, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "11 Black and white checkered flat sneakers", CLOTHES_FEET, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "12 Brown boots", CLOTHES_FEET, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "13 Dark boots white sole", CLOTHES_FEET, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "14 Plain black pointed toe boot", CLOTHES_FEET, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "15 Black flip flops", CLOTHES_FEET, 16, SEX_MALE, 50),
            new BusinessClothesModel(0, "16 Green Goblin", CLOTHES_FEET, 17, SEX_MALE, 50),
            new BusinessClothesModel(0, "17 Normal black and white high socks", CLOTHES_FEET, 18, SEX_MALE, 50),
            new BusinessClothesModel(0, "18 Black and white shoe with black buttons", CLOTHES_FEET, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "19 Dark brown pointed toe high socks shoe", CLOTHES_FEET, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "20 Dark brown low toe sock shoe", CLOTHES_FEET, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "21 Converse blue high ankle", CLOTHES_FEET, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "22 Normal brown yellow sole", CLOTHES_FEET, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "23 Black boot", CLOTHES_FEET, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "24 Black studded boot", CLOTHES_FEET, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "25 Converse dark blue high ankle", CLOTHES_FEET, 26, SEX_MALE, 50),
            new BusinessClothesModel(0, "26 White sneakers black dots ankle high", CLOTHES_FEET, 28, SEX_MALE, 50),
            new BusinessClothesModel(0, "27 Gold high ankle sneakers", CLOTHES_FEET, 29, SEX_MALE, 50),
            new BusinessClothesModel(0, "28 Normal white and dark buckle", CLOTHES_FEET, 30, SEX_MALE, 50),
            new BusinessClothesModel(0, "29 Sneakers yellow gray white black", CLOTHES_FEET, 31, SEX_MALE, 50),
            new BusinessClothesModel(0, "30 Black and white trainers", CLOTHES_FEET, 32, SEX_MALE, 50),
            new BusinessClothesModel(0, "31 Barefoot", CLOTHES_FEET, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "32 High ankle brown boot", CLOTHES_FEET, 35, SEX_MALE, 50),
            new BusinessClothesModel(0, "33 Normal dark brown gold buckle", CLOTHES_FEET, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "34 High toe cowboy boot", CLOTHES_FEET, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "35 Low toe cowboy boot", CLOTHES_FEET, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "36 Blue and gray high sock shoe", CLOTHES_FEET, 40, SEX_MALE, 50),
            new BusinessClothesModel(0, "37 Dark red and black slipper", CLOTHES_FEET, 41, SEX_MALE, 50),
            new BusinessClothesModel(0, "38 Grayish slippers", CLOTHES_FEET, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "39 Yellow low boot", CLOTHES_FEET, 43, SEX_MALE, 50),
            new BusinessClothesModel(0, "40 Pointed blue high cowboy boot", CLOTHES_FEET, 44, SEX_MALE, 50),
            new BusinessClothesModel(0, "41 Pointed blue low cowboy boot", CLOTHES_FEET, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "42 White and blue trainers", CLOTHES_FEET, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "43 Green and white high boot", CLOTHES_FEET, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "44 Converse black and white high ankle", CLOTHES_FEET, 48, SEX_MALE, 50),
            new BusinessClothesModel(0, "45 Converse yellow and white high ankle", CLOTHES_FEET, 49, SEX_MALE, 50),
            new BusinessClothesModel(0, "46 Something black ankle boot", CLOTHES_FEET, 50, SEX_MALE, 50),
            new BusinessClothesModel(0, "47 Black low shoes", CLOTHES_FEET, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "48 Brown round boot", CLOTHES_FEET, 52, SEX_MALE, 50),
            new BusinessClothesModel(0, "49 Black studded high ankle boot", CLOTHES_FEET, 53, SEX_MALE, 50),
            new BusinessClothesModel(0, "50 Black studded low ankle shoe", CLOTHES_FEET, 54, SEX_MALE, 50),
            new BusinessClothesModel(0, "51 Black sneakers with phosphor yellow stripes", CLOTHES_FEET, 55, SEX_MALE, 50),
            new BusinessClothesModel(0, "52 Brown ankle shoe under black sole", CLOTHES_FEET, 56, SEX_MALE, 50),
            new BusinessClothesModel(0, "53 Camel sneakers", CLOTHES_FEET, 57, SEX_MALE, 50),
            new BusinessClothesModel(0, "54 Black plane red and light blue stripes", CLOTHES_FEET, 58, SEX_MALE, 50),
            new BusinessClothesModel(0, "55 Printed sneakers", CLOTHES_FEET, 59, SEX_MALE, 50),
            new BusinessClothesModel(0, "56 Tall black boots", CLOTHES_FEET, 60, SEX_MALE, 50),
            new BusinessClothesModel(0, "57 Low black boots", CLOTHES_FEET, 61, SEX_MALE, 50),
            new BusinessClothesModel(0, "58 Tall beige boots", CLOTHES_FEET, 62, SEX_MALE, 50),
            new BusinessClothesModel(0, "59 Low beige boots", CLOTHES_FEET, 63, SEX_MALE, 50),
            new BusinessClothesModel(0, "60 Green sneakers", CLOTHES_FEET, 64, SEX_MALE, 50),
            new BusinessClothesModel(0, "61 Timberland high boots", CLOTHES_FEET, 65, SEX_MALE, 50),
            new BusinessClothesModel(0, "62 Low timberland boots", CLOTHES_FEET, 66, SEX_MALE, 50),
            new BusinessClothesModel(0, "63 Complete diving fins", CLOTHES_FEET, 67, SEX_MALE, 50),
            new BusinessClothesModel(0, "64 Cut diving fins", CLOTHES_FEET, 69, SEX_MALE, 50),
            new BusinessClothesModel(0, "65 High yellow and khaki boots", CLOTHES_FEET, 70, SEX_MALE, 50),
            new BusinessClothesModel(0, "66 Low yellow and khaki boots", CLOTHES_FEET, 71, SEX_MALE, 50),
            new BusinessClothesModel(0, "67 High gray and black boots", CLOTHES_FEET, 72, SEX_MALE, 50),
            new BusinessClothesModel(0, "68 Low gray and black boots", CLOTHES_FEET, 73, SEX_MALE, 50),
            new BusinessClothesModel(0, "69 Turquoise slippers", CLOTHES_FEET, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "70 High beige sneakers", CLOTHES_FEET, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "71 Low beige sneakers", CLOTHES_FEET, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "72 Sneakers with lights", CLOTHES_FEET, 77, SEX_MALE, 50),
            new BusinessClothesModel(0, "73 Slippers", CLOTHES_FEET, 78, SEX_MALE, 50),
            new BusinessClothesModel(0, "74 Tall Fire Boots", CLOTHES_FEET, 79, SEX_MALE, 50),
            new BusinessClothesModel(0, "75 Low Fire Boots", CLOTHES_FEET, 80, SEX_MALE, 50),
            new BusinessClothesModel(0, "76 Tall black boots", CLOTHES_FEET, 81, SEX_MALE, 50),
            new BusinessClothesModel(0, "77 Low black boots", CLOTHES_FEET, 82, SEX_MALE, 50),
            new BusinessClothesModel(0, "78 White and yellow slippers", CLOTHES_FEET, 83, SEX_MALE, 50),
            new BusinessClothesModel(0, "79 Madmax high boots", CLOTHES_FEET, 84, SEX_MALE, 50),
            new BusinessClothesModel(0, "80 Madmax half boots", CLOTHES_FEET, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "81 Low Madmax boots", CLOTHES_FEET, 86, SEX_MALE, 50),
            new BusinessClothesModel(0, "82 Red boots", CLOTHES_FEET, 87, SEX_MALE, 50),
            new BusinessClothesModel(0, "83 Tall white boots", CLOTHES_FEET, 88, SEX_MALE, 50),
            new BusinessClothesModel(0, "84 Gray sneakers", CLOTHES_FEET, 89, SEX_MALE, 50),
            new BusinessClothesModel(0, "85 Green elf boots", CLOTHES_FEET, 90, SEX_MALE, 50),

            // Female accessories
            new BusinessClothesModel(0, "Wide gold hoop earrings", CLOTHES_ACCESSORIES, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Golden fine hoop earrings", CLOTHES_ACCESSORIES, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black right hand wristband", CLOTHES_ACCESSORIES, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Wide black and white square right hand bracelets", CLOTHES_ACCESSORIES, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and white square right hand wristband", CLOTHES_ACCESSORIES, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Golden Horseshoe Pendant Gem Dark Inside", CLOTHES_ACCESSORIES, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and gold rope pendants with circle and gold heart", CLOTHES_ACCESSORIES, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and White Palestine", CLOTHES_ACCESSORIES, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Wide right hand flower bracelets", CLOTHES_ACCESSORIES, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black and gold quartz pendants with gold circle and dark heart", CLOTHES_ACCESSORIES, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White pearl necklace", CLOTHES_ACCESSORIES, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black scarf white circles", CLOTHES_ACCESSORIES, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Right hand metal stud wristband", CLOTHES_ACCESSORIES, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Red and Black Palestine", CLOTHES_ACCESSORIES, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Scarf looking for Wally", CLOTHES_ACCESSORIES, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black tie", CLOTHES_ACCESSORIES, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White tie", CLOTHES_ACCESSORIES, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Tight black tie", CLOTHES_ACCESSORIES, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black bow tie", CLOTHES_ACCESSORIES, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "", CLOTHES_ACCESSORIES, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain with G pendant", CLOTHES_ACCESSORIES, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant gold skull red eyes", CLOTHES_ACCESSORIES, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Silver chain pendant silver skull red eyes", CLOTHES_ACCESSORIES, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain gold plate", CLOTHES_ACCESSORIES, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain € gold", CLOTHES_ACCESSORIES, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Silver chain silver pendant", CLOTHES_ACCESSORIES, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain wide gold pendant", CLOTHES_ACCESSORIES, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant $ gold", CLOTHES_ACCESSORIES, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant skull red eyes outside", CLOTHES_ACCESSORIES, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Silver chain pendant masked silver outside", CLOTHES_ACCESSORIES, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant silver plate exterior", CLOTHES_ACCESSORIES, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant C outside", CLOTHES_ACCESSORIES, 40, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "External DIX gold chain pendant", CLOTHES_ACCESSORIES, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant letters gold exterior", CLOTHES_ACCESSORIES, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Light gold chain without pendant", CLOTHES_ACCESSORIES, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain without pendant", CLOTHES_ACCESSORIES, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Wide gold chain", CLOTHES_ACCESSORIES, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Wide light gold chain", CLOTHES_ACCESSORIES, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Palestine dark brown and black", CLOTHES_ACCESSORIES, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Brown pearl necklace", CLOTHES_ACCESSORIES, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Red and white headphones", CLOTHES_ACCESSORIES, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Blue and pink tie", CLOTHES_ACCESSORIES, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Green tie", CLOTHES_ACCESSORIES, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Black suspenders", CLOTHES_ACCESSORIES, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Gold chain red and gold star pendant outside", CLOTHES_ACCESSORIES, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Exterior gold star gold chain pendant", CLOTHES_ACCESSORIES, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Light brown pearl necklace", CLOTHES_ACCESSORIES, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and blue headphones", CLOTHES_ACCESSORIES, 94, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "95 Neck and waist plate", CLOTHES_ACCESSORIES, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "96 Stethoscope", CLOTHES_ACCESSORIES, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "97 Paramedic ID", CLOTHES_ACCESSORIES, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "98 Neck plate", CLOTHES_ACCESSORIES, 98, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "99 Gold necklace F", CLOTHES_ACCESSORIES, 99, SEX_FEMALE, 5),
            new BusinessClothesModel(0, "100 Gold F variant necklace 1", CLOTHES_ACCESSORIES, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "101 Gold F variant necklace 2", CLOTHES_ACCESSORIES, 101, SEX_FEMALE, 50),


            // Male accessories
            new BusinessClothesModel(0, "Tight white tie", CLOTHES_ACCESSORIES, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "Black and white check bow tie", CLOTHES_ACCESSORIES, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "White tie", CLOTHES_ACCESSORIES, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "Silver chain", CLOTHES_ACCESSORIES, 16, SEX_MALE, 50),
            new BusinessClothesModel(0, "Exterior silver chain", CLOTHES_ACCESSORIES, 17, SEX_MALE, 50),
            new BusinessClothesModel(0, "Wide red tie", CLOTHES_ACCESSORIES, 18, SEX_MALE, 50),
            new BusinessClothesModel(0, "Fine red tie", CLOTHES_ACCESSORIES, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "Dark red tie", CLOTHES_ACCESSORIES, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "Wide blue tie", CLOTHES_ACCESSORIES, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "White bow tie", CLOTHES_ACCESSORIES, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "Fine blue tie", CLOTHES_ACCESSORIES, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "Wide white tie", CLOTHES_ACCESSORIES, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "Fine white tie", CLOTHES_ACCESSORIES, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "White scarf", CLOTHES_ACCESSORIES, 30, SEX_MALE, 50),
            new BusinessClothesModel(0, "Red white and blue bow tie", CLOTHES_ACCESSORIES, 32, SEX_MALE, 50),
            new BusinessClothesModel(0, "Scarf looking for Wally", CLOTHES_ACCESSORIES, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "Black bow tie", CLOTHES_ACCESSORIES, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "Wide black tie", CLOTHES_ACCESSORIES, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "Tight black tie", CLOTHES_ACCESSORIES, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "Loose black tie", CLOTHES_ACCESSORIES, 39, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant G", CLOTHES_ACCESSORIES, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant skull", CLOTHES_ACCESSORIES, 43, SEX_MALE, 50),
            new BusinessClothesModel(0, "Silver chain circle pendant", CLOTHES_ACCESSORIES, 44, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant plate", CLOTHES_ACCESSORIES, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "Light gold pendant gold chain", CLOTHES_ACCESSORIES, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain medallion", CLOTHES_ACCESSORIES, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "Silver chain masked pendant", CLOTHES_ACCESSORIES, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "Light gold chain", CLOTHES_ACCESSORIES, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "Dark gold chain", CLOTHES_ACCESSORIES, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "Dark wide gold chain", CLOTHES_ACCESSORIES, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "Clear big chain", CLOTHES_ACCESSORIES, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "Palestine brown and black", CLOTHES_ACCESSORIES, 112, SEX_MALE, 50),
            new BusinessClothesModel(0, "Brown pearl necklace", CLOTHES_ACCESSORIES, 113, SEX_MALE, 50),
            new BusinessClothesModel(0, "Red and white headphones", CLOTHES_ACCESSORIES, 114, SEX_MALE, 50),
            new BusinessClothesModel(0, "Blue and pink tie", CLOTHES_ACCESSORIES, 115, SEX_MALE, 50),
            new BusinessClothesModel(0, "Green tie", CLOTHES_ACCESSORIES, 117, SEX_MALE, 50),
            new BusinessClothesModel(0, "Black choker bow tie", CLOTHES_ACCESSORIES, 118, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain red star pendant", CLOTHES_ACCESSORIES, 119, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant gold star", CLOTHES_ACCESSORIES, 120, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant star red exterior", CLOTHES_ACCESSORIES, 121, SEX_MALE, 50),
            new BusinessClothesModel(0, "Gold chain pendant star gold exterior", CLOTHES_ACCESSORIES, 122, SEX_MALE, 50),
            new BusinessClothesModel(0, "Light brown pearl necklace", CLOTHES_ACCESSORIES, 123, SEX_MALE, 50),
            new BusinessClothesModel(0, "Blue and white headphones", CLOTHES_ACCESSORIES, 124, SEX_MALE, 50),
            new BusinessClothesModel(0, "125 Neck and waist plate", CLOTHES_ACCESSORIES, 125, SEX_MALE, 50),
            new BusinessClothesModel(0, "126 Stethoscope", CLOTHES_ACCESSORIES, 126, SEX_MALE, 50),
            new BusinessClothesModel(0, "127 Paramedic identification", CLOTHES_ACCESSORIES, 127, SEX_MALE, 50),
            new BusinessClothesModel(0, "128 Neck plate", CLOTHES_ACCESSORIES, 128, SEX_MALE, 50),
            new BusinessClothesModel(0, "129 Gold necklace F", CLOTHES_ACCESSORIES, 129, SEX_MALE, 50),
            new BusinessClothesModel(0, "130 Gold F variant necklace 1", CLOTHES_ACCESSORIES, 130, SEX_MALE, 50),
            new BusinessClothesModel(0, "131 Gold F variant necklace 2", CLOTHES_ACCESSORIES, 131, SEX_MALE, 50),

            // Female torsos
            new BusinessClothesModel(0, "Without gloves", CLOTHES_TORSO, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 2", CLOTHES_TORSO, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 3", CLOTHES_TORSO, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 4", CLOTHES_TORSO, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 5", CLOTHES_TORSO, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 6", CLOTHES_TORSO, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 7", CLOTHES_TORSO, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 8", CLOTHES_TORSO, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 9", CLOTHES_TORSO, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 10", CLOTHES_TORSO, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 11", CLOTHES_TORSO, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 12", CLOTHES_TORSO, 131, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 13", CLOTHES_TORSO, 161, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 14", CLOTHES_TORSO, 130, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 15", CLOTHES_TORSO, 129, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 16", CLOTHES_TORSO, 130, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 17", CLOTHES_TORSO, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Without gloves 18", CLOTHES_TORSO, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Long purple gloves", CLOTHES_TORSO, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Thick green gloves", CLOTHES_TORSO, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves", CLOTHES_TORSO, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 2", CLOTHES_TORSO, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 3", CLOTHES_TORSO, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 4", CLOTHES_TORSO, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 5", CLOTHES_TORSO, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 6", CLOTHES_TORSO, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 7", CLOTHES_TORSO, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 8", CLOTHES_TORSO, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 9", CLOTHES_TORSO, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 10", CLOTHES_TORSO, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 11", CLOTHES_TORSO, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 12", CLOTHES_TORSO, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 13", CLOTHES_TORSO, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves", CLOTHES_TORSO, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 2", CLOTHES_TORSO, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 3", CLOTHES_TORSO, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 4", CLOTHES_TORSO, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 5", CLOTHES_TORSO, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 6", CLOTHES_TORSO, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 7", CLOTHES_TORSO, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 8", CLOTHES_TORSO, 40, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 9", CLOTHES_TORSO, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 10", CLOTHES_TORSO, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 11", CLOTHES_TORSO, 43, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 12", CLOTHES_TORSO, 44, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 13", CLOTHES_TORSO, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 14", CLOTHES_TORSO, 46, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 15", CLOTHES_TORSO, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 16", CLOTHES_TORSO, 48, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 17", CLOTHES_TORSO, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 18", CLOTHES_TORSO, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 19", CLOTHES_TORSO, 51, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 20", CLOTHES_TORSO, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 21", CLOTHES_TORSO, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 22", CLOTHES_TORSO, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 23", CLOTHES_TORSO, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 24", CLOTHES_TORSO, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 25", CLOTHES_TORSO, 57, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Full driving gloves 26", CLOTHES_TORSO, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves", CLOTHES_TORSO, 59, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 2", CLOTHES_TORSO, 61, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 3", CLOTHES_TORSO, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 4", CLOTHES_TORSO, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 5", CLOTHES_TORSO, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 6", CLOTHES_TORSO, 65, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 7", CLOTHES_TORSO, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 8", CLOTHES_TORSO, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 9", CLOTHES_TORSO, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 10", CLOTHES_TORSO, 69, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 11", CLOTHES_TORSO, 70, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 12", CLOTHES_TORSO, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves", CLOTHES_TORSO, 72, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 2", CLOTHES_TORSO, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 3", CLOTHES_TORSO, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 4", CLOTHES_TORSO, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 5", CLOTHES_TORSO, 76, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 6", CLOTHES_TORSO, 77, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 7", CLOTHES_TORSO, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 8", CLOTHES_TORSO, 79, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 9", CLOTHES_TORSO, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 10", CLOTHES_TORSO, 81, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 11", CLOTHES_TORSO, 82, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 12", CLOTHES_TORSO, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White and yellow gloves 13", CLOTHES_TORSO, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves", CLOTHES_TORSO, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 2", CLOTHES_TORSO, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 3", CLOTHES_TORSO, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 4", CLOTHES_TORSO, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 5", CLOTHES_TORSO, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 6", CLOTHES_TORSO, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 7", CLOTHES_TORSO, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 8", CLOTHES_TORSO, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 9", CLOTHES_TORSO, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 10", CLOTHES_TORSO, 94, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 11", CLOTHES_TORSO, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 12", CLOTHES_TORSO, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "White gloves 13", CLOTHES_TORSO, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves", CLOTHES_TORSO, 98, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 2", CLOTHES_TORSO, 99, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 3", CLOTHES_TORSO, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 4", CLOTHES_TORSO, 101, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 5", CLOTHES_TORSO, 102, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 6", CLOTHES_TORSO, 103, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 7", CLOTHES_TORSO, 104, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 8", CLOTHES_TORSO, 105, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 9", CLOTHES_TORSO, 106, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 10", CLOTHES_TORSO, 107, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 11", CLOTHES_TORSO, 108, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 12", CLOTHES_TORSO, 109, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 13", CLOTHES_TORSO, 110, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Green and white motorcycle gloves", CLOTHES_TORSO, 127, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "Pink and white motorcycle gloves", CLOTHES_TORSO, 128, SEX_FEMALE, 50),

            // Male torsos
            new BusinessClothesModel(0, "Without gloves 2", CLOTHES_TORSO, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 3", CLOTHES_TORSO, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 4", CLOTHES_TORSO, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 5", CLOTHES_TORSO, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 6", CLOTHES_TORSO, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 7", CLOTHES_TORSO, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 8", CLOTHES_TORSO, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 9", CLOTHES_TORSO, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 10", CLOTHES_TORSO, 8, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 11", CLOTHES_TORSO, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 12", CLOTHES_TORSO, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 13", CLOTHES_TORSO, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 14", CLOTHES_TORSO, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 15", CLOTHES_TORSO, 13, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 16", CLOTHES_TORSO, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 17", CLOTHES_TORSO, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 18", CLOTHES_TORSO, 112, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 19", CLOTHES_TORSO, 113, SEX_MALE, 50),
            new BusinessClothesModel(0, "Without gloves 20", CLOTHES_TORSO, 114, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves", CLOTHES_TORSO, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 2", CLOTHES_TORSO, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 3", CLOTHES_TORSO, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 4", CLOTHES_TORSO, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 5", CLOTHES_TORSO, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 6", CLOTHES_TORSO, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 7", CLOTHES_TORSO, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 8", CLOTHES_TORSO, 26, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 9", CLOTHES_TORSO, 27, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 10", CLOTHES_TORSO, 28, SEX_MALE, 50),
            new BusinessClothesModel(0, "Open driving gloves 11", CLOTHES_TORSO, 29, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves", CLOTHES_TORSO, 30, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 2", CLOTHES_TORSO, 31, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 3", CLOTHES_TORSO, 32, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 4", CLOTHES_TORSO, 33, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 5", CLOTHES_TORSO, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 6", CLOTHES_TORSO, 35, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 7", CLOTHES_TORSO, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 8", CLOTHES_TORSO, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 9", CLOTHES_TORSO, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 10", CLOTHES_TORSO, 39, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 11", CLOTHES_TORSO, 40, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 12", CLOTHES_TORSO, 41, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 13", CLOTHES_TORSO, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 14", CLOTHES_TORSO, 43, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 15", CLOTHES_TORSO, 44, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 16", CLOTHES_TORSO, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 17", CLOTHES_TORSO, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 18", CLOTHES_TORSO, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 19", CLOTHES_TORSO, 48, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 20", CLOTHES_TORSO, 49, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 21", CLOTHES_TORSO, 50, SEX_MALE, 50),
            new BusinessClothesModel(0, "Closed driving gloves 22", CLOTHES_TORSO, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves", CLOTHES_TORSO, 52, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 2", CLOTHES_TORSO, 53, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 3", CLOTHES_TORSO, 54, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 4", CLOTHES_TORSO, 55, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 5", CLOTHES_TORSO, 56, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 6", CLOTHES_TORSO, 57, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 7", CLOTHES_TORSO, 58, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 8", CLOTHES_TORSO, 59, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 9", CLOTHES_TORSO, 60, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 10", CLOTHES_TORSO, 61, SEX_MALE, 50),
            new BusinessClothesModel(0, "Mittens gloves 11", CLOTHES_TORSO, 62, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves", CLOTHES_TORSO, 63, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 2", CLOTHES_TORSO, 64, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 3", CLOTHES_TORSO, 65, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 4", CLOTHES_TORSO, 66, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 5", CLOTHES_TORSO, 67, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 6", CLOTHES_TORSO, 68, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 7", CLOTHES_TORSO, 69, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 8", CLOTHES_TORSO, 70, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 9", CLOTHES_TORSO, 71, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 10", CLOTHES_TORSO, 72, SEX_MALE, 50),
            new BusinessClothesModel(0, "Yellow and white gloves 11", CLOTHES_TORSO, 73, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves", CLOTHES_TORSO, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 2", CLOTHES_TORSO, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 3", CLOTHES_TORSO, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 4", CLOTHES_TORSO, 77, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 5", CLOTHES_TORSO, 78, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 6", CLOTHES_TORSO, 79, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 7", CLOTHES_TORSO, 80, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 8", CLOTHES_TORSO, 81, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 9", CLOTHES_TORSO, 82, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 10", CLOTHES_TORSO, 83, SEX_MALE, 50),
            new BusinessClothesModel(0, "White gloves 11", CLOTHES_TORSO, 84, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves", CLOTHES_TORSO, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 2", CLOTHES_TORSO, 86, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 3", CLOTHES_TORSO, 87, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 4", CLOTHES_TORSO, 88, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 5", CLOTHES_TORSO, 89, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 6", CLOTHES_TORSO, 90, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 7", CLOTHES_TORSO, 91, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 8", CLOTHES_TORSO, 92, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 9", CLOTHES_TORSO, 93, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 10", CLOTHES_TORSO, 94, SEX_MALE, 50),
            new BusinessClothesModel(0, "Heavenly Gloves 11", CLOTHES_TORSO, 95, SEX_MALE, 50),
            new BusinessClothesModel(0, "High black gloves", CLOTHES_TORSO, 96, SEX_MALE, 50),
            new BusinessClothesModel(0, "Green and white motorcycle gloves", CLOTHES_TORSO, 110, SEX_MALE, 50),
            new BusinessClothesModel(0, "Pink and white motorcycle gloves", CLOTHES_TORSO, 111, SEX_MALE, 50),

            // Female tops
            new BusinessClothesModel(0, "1 Dark red and gray short sleeve t-shirt", CLOTHES_TOPS, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "2 Denim jacket with open sleeves", CLOTHES_TOPS, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "3 White off-the-shoulder t-shirt", CLOTHES_TOPS, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "4 White sweatshirt", CLOTHES_TOPS, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "5 Black and white check tank top", CLOTHES_TOPS, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "6 White top", CLOTHES_TOPS, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "7 Black and white rolled-up jacket", CLOTHES_TOPS, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "8 Black open jacket", CLOTHES_TOPS, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "9 Gray open-sleeved jacket", CLOTHES_TOPS, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "10 Black shirt", CLOTHES_TOPS, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "11 Black purple and white striped sweatshirt", CLOTHES_TOPS, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "12 Light blue black and white tank top", CLOTHES_TOPS, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "13 Black and white check tank top", CLOTHES_TOPS, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "14 Black strapless word", CLOTHES_TOPS, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "15 White lacoste polo shirt", CLOTHES_TOPS, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "16 Black bikini", CLOTHES_TOPS, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "17 Red tank top", CLOTHES_TOPS, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "18 Hawaiian shirt", CLOTHES_TOPS, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "19 White bikini", CLOTHES_TOPS, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "20 Mama noel t-shirt", CLOTHES_TOPS, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "21 Red and white rolled-up jacket", CLOTHES_TOPS, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "22 Purple to White Ruffle Dress", CLOTHES_TOPS, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "23 Lace white strapless", CLOTHES_TOPS, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "24 White T-shirt", CLOTHES_TOPS, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "25 White and black rolled-up jacket", CLOTHES_TOPS, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "26 Open red jacket", CLOTHES_TOPS, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "27 Greek camel tank top", CLOTHES_TOPS, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "28 White shirt inside pants", CLOTHES_TOPS, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "29 Gray vest with straps", CLOTHES_TOPS, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "30 Blue and black open sleeved jacket", CLOTHES_TOPS, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "31 Leopard tank top", CLOTHES_TOPS, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "32 White top with black stripes", CLOTHES_TOPS, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "33 Open camouflage jacket", CLOTHES_TOPS, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "34 Yellow and white short sleeved jacket", CLOTHES_TOPS, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "35 FIST black tank top", CLOTHES_TOPS, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "36 Pink flower dress", CLOTHES_TOPS, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "37 Green shoulder t-shirt", CLOTHES_TOPS, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "38 USA long sleeve t-shirt", CLOTHES_TOPS, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "39 Mama noel long sleeve t-shirt", CLOTHES_TOPS, 40, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "40 Blue off shoulder t-shirt", CLOTHES_TOPS, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "41 Green hooks military jacket", CLOTHES_TOPS, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "42 Red and white long sleeve t-shirt", CLOTHES_TOPS, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "43 Black hooks military jacket", CLOTHES_TOPS, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "44 Gray shirt", CLOTHES_TOPS, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "45 Black wide neck sweatshirt", CLOTHES_TOPS, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "46 Open gray jacket", CLOTHES_TOPS, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "47 Gray rolled-up jacket", CLOTHES_TOPS, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "48 Brown jacket", CLOTHES_TOPS, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "49 Black leather jacket", CLOTHES_TOPS, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "50 Black short sleeve shirt", CLOTHES_TOPS, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "51 Black open jacket", CLOTHES_TOPS, 57, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "52 Black jacket closed ", CLOTHES_TOPS, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "53 Torn t-shirt", CLOTHES_TOPS, 59, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "54 Ripped red and black long sleeve t-shirt", CLOTHES_TOPS, 60, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "55 Gray long sleeve t-shirt", CLOTHES_TOPS, 61, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "56 Black jacket with hood on", CLOTHES_TOPS, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "57 Black jacket with hood removed", CLOTHES_TOPS, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "58 Long open veig jacket", CLOTHES_TOPS, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "59 Red jacket with brown hairs on the neck", CLOTHES_TOPS, 65, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "60 Black low-cut shirt with shoulder pads", CLOTHES_TOPS, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "61 Yellow t-shirt off shoulder", CLOTHES_TOPS, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "62 T-shirt yellow orange and brown tones", CLOTHES_TOPS, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "63 Black jacket with buckles", CLOTHES_TOPS, 69, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "64 Veig jacket closed by belt", CLOTHES_TOPS, 70, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "65 Veig checkered shirt, yellow, brown and black rolled up", CLOTHES_TOPS, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "66 Red and white sweatshirt W", CLOTHES_TOPS, 72, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "67 White t-shirt inside the pants", CLOTHES_TOPS, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "68 Slim Fit White Top", CLOTHES_TOPS, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "69 Yellow and black t-shirt inside the pants", CLOTHES_TOPS, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "70 Long gray shirt with yellow stripes", CLOTHES_TOPS, 76, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "71 Black long shirt", CLOTHES_TOPS, 77, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "72 Black hoodie", CLOTHES_TOPS, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "73 Black sweater", CLOTHES_TOPS, 79, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "74 Dark red and white letterless jacket", CLOTHES_TOPS, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "75 Red and white jacket P", CLOTHES_TOPS, 81, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "76 Simple black shirt", CLOTHES_TOPS, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "77 ANDREAS dark green polo shirt", CLOTHES_TOPS, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "78 ANDREAS dark green polo inside the pants", CLOTHES_TOPS, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "79 Denim shirt inside the pants", CLOTHES_TOPS, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "80 Blue sweatshirt with white stripeF", CLOTHES_TOPS, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "81 Brown t-shirt", CLOTHES_TOPS, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "82 Brown jacket inside the pants", CLOTHES_TOPS, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "83 White open jacket", CLOTHES_TOPS, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "84 White closed jacket", CLOTHES_TOPS, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "85 Open red jacket", CLOTHES_TOPS, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "86 Closed red jacket", CLOTHES_TOPS, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "87 Dark jacket with open brown flowers", CLOTHES_TOPS, 94, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "88 Dark jacket with brown flowers closed", CLOTHES_TOPS, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "89 White shirt with flowers", CLOTHES_TOPS, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "90 Long dark blue jacket", CLOTHES_TOPS, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "91 White shirt closed with large pockets", CLOTHES_TOPS, 98, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "92 Long red and black jacket", CLOTHES_TOPS, 99, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "93 White tank top", CLOTHES_TOPS, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "94 Pink bikini", CLOTHES_TOPS, 101, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "95 Closed long dark brown jacket", CLOTHES_TOPS, 102, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "96 Gray long sleeve t-shirt inside the pants", CLOTHES_TOPS, 103, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "97 Closed light brown jacket", CLOTHES_TOPS, 104, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "98 White shirt navel outside", CLOTHES_TOPS, 105, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "99 Red white and black sweatshirt", CLOTHES_TOPS, 106, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "100 Open black trench coat", CLOTHES_TOPS, 107, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "101 Dirty Mama Noel", CLOTHES_TOPS, 108, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "102 Red and black closed shirt", CLOTHES_TOPS, 109, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "103 Black and green leather jacket", CLOTHES_TOPS, 110, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "104 Brown and white strapless", CLOTHES_TOPS, 111, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "105 Blue and black dress", CLOTHES_TOPS, 112, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "106 Brown and black dress", CLOTHES_TOPS, 113, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "107 Red and gold dress", CLOTHES_TOPS, 114, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "108 Dark green and yellow dress", CLOTHES_TOPS, 115, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "109 White and gray dress", CLOTHES_TOPS, 116, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "110 White T-shirt without sleeves inside the pants", CLOTHES_TOPS, 117, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "111 White tank top", CLOTHES_TOPS, 118, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "112 Large white polo shirt", CLOTHES_TOPS, 119, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "113 Black and blue open plaid shirt", CLOTHES_TOPS, 120, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "114 Black and blue checked plaid shirt", CLOTHES_TOPS, 121, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "115 Long closed camel jacket", CLOTHES_TOPS, 122, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "116 Black and white striped jacket", CLOTHES_TOPS, 123, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "117 Green long t-shirt", CLOTHES_TOPS, 125, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "118 Long black t-shirt", CLOTHES_TOPS, 126, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "119 Black jacket red and white triangle", CLOTHES_TOPS, 127, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "120 LIBERTY blue polo shirt", CLOTHES_TOPS, 128, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "121 Blue LIBERTY polo inside pants", CLOTHES_TOPS, 129, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "122 White and gray rolled-up polo", CLOTHES_TOPS, 130, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "123 Black LIBERTY hoodie", CLOTHES_TOPS, 131, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "124 Black sleeved shirt with yellow flowers", CLOTHES_TOPS, 132, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "125 Dark gray open long jacket", CLOTHES_TOPS, 133, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "126 Black and gray diamond argyle sleeveless sweater", CLOTHES_TOPS, 134, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "127 Dark brown jacket with belt", CLOTHES_TOPS, 135, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "128 Gray long sleeve t-shirt inside pants", CLOTHES_TOPS, 136, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "129 Long blue low-cut jacket", CLOTHES_TOPS, 137, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "130 Blue sweatshirt with white stripes", CLOTHES_TOPS, 138, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "131 Black open long jacket", CLOTHES_TOPS, 139, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "132 Green and white sweatshirt", CLOTHES_TOPS, 140, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "133 Light blue t-shirt", CLOTHES_TOPS, 141, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "134 Light blue long sleeve shirt", CLOTHES_TOPS, 142, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "135 Light blue jacket closed", CLOTHES_TOPS, 143, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "136 Blue and white sweatshirt closed", CLOTHES_TOPS, 144, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "137 Green and white t-shirt", CLOTHES_TOPS, 145, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "137 Veig and USA jacket", CLOTHES_TOPS, 146, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "138 Black and yellow jacket", CLOTHES_TOPS, 147, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "139 Dark open shirt with yellow letters", CLOTHES_TOPS, 148, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "140 Pink NAGASAK T-shirt", CLOTHES_TOPS, 149, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "141 Black and red sweatshirt", CLOTHES_TOPS, 150, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "142 Italy brown sweatshirt", CLOTHES_TOPS, 151, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "143 Yellow jacket USA", CLOTHES_TOPS, 152, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "144 Black open sleeveless jacket", CLOTHES_TOPS, 154, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "145 Black sleeveless jacket with zipper", CLOTHES_TOPS, 155, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "146 Black sleeveless jacket with buttons", CLOTHES_TOPS, 156, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "147 Black open sleeveless zipper jacket", CLOTHES_TOPS, 157, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "148 Closed black leather jacket", CLOTHES_TOPS, 158, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "149 Closed Sleeveless Black Leather Jacket", CLOTHES_TOPS, 159, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "150 Open black leather shirt", CLOTHES_TOPS, 160, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "151 Long black leather half sleeve shirt", CLOTHES_TOPS, 161, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "152 White snake sleeve dark blue jacket", CLOTHES_TOPS, 162, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "153 Open black leather jacket", CLOTHES_TOPS, 163, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "154 Michelin red coat", CLOTHES_TOPS, 164, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "155 Olive jacket", CLOTHES_TOPS, 165, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "156 Open denim shirt", CLOTHES_TOPS, 166, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "157 Sleeveless open denim shirt", CLOTHES_TOPS, 167, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "158 Black tank top", CLOTHES_TOPS, 168, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "159 Black short t-shirt", CLOTHES_TOPS, 169, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "160 Top negro suelto", CLOTHES_TOPS, 170, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "161 Loose Black Top", CLOTHES_TOPS, 171, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "162 Black hooded coat", CLOTHES_TOPS, 172, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "163 Penguin straps jacket", CLOTHES_TOPS, 173, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "164 Open stickers denim jacket", CLOTHES_TOPS, 174, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "165 Open Sticker Sleeveless Denim Jacket", CLOTHES_TOPS, 175, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "166 Black leather jacket with closed stickers", CLOTHES_TOPS, 176, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "167 Black leather jacket sleeveless stickers", CLOTHES_TOPS, 177, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "168 Black sleeveless shirt stickers", CLOTHES_TOPS, 178, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "169 Dark blue tank top", CLOTHES_TOPS, 179, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "170 Black long sleeve t-shirt with yellow stripes", CLOTHES_TOPS, 180, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "171 Black and white striped shirt", CLOTHES_TOPS, 185, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "172 Long green raincoat with hood", CLOTHES_TOPS, 186, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "173 Long green raincoat with open hood", CLOTHES_TOPS, 187, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "174 Long black trench coat", CLOTHES_TOPS, 189, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "175 Camel raincoat with hood", CLOTHES_TOPS, 190, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "176 Long camel hooded raincoat", CLOTHES_TOPS, 191, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "177 Gray sweater with gathered black diamonds", CLOTHES_TOPS, 192, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "178 Camouflage green and black open jacket", CLOTHES_TOPS, 193, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "179 Long open gray trench coat", CLOTHES_TOPS, 194, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "180 Black collection t-shirt with pink letters", CLOTHES_TOPS, 195, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "181 Christmas sweater", CLOTHES_TOPS, 196, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "182 Gray rolled-up sweater with yellow bits", CLOTHES_TOPS, 198, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "183 Blue shirt white trees", CLOTHES_TOPS, 200, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "184 Yellow sweatshirt with orange patches", CLOTHES_TOPS, 202, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "185 Black Fitted T-shirt with Light Blue Green and Pink Yellow Stripes", CLOTHES_TOPS, 203, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "186 Black tank top with hood on", CLOTHES_TOPS, 204, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "187 Yellow sweatshirt orange patches with hood on", CLOTHES_TOPS, 205, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "188 Black trench coat with hood on", CLOTHES_TOPS, 206, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "189 Black wide neck tank top", CLOTHES_TOPS, 207, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "190 Camouflage sleeveless t-shirt inside the pants", CLOTHES_TOPS, 208, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "191 Camouflage sleeveless top", CLOTHES_TOPS, 209, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "192 Bluish Camouflage Sleeveless Hoodie", CLOTHES_TOPS, 210, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "193 Bluish Camouflage Sleeveless Hoodie", CLOTHES_TOPS, 211, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "194 Bluish camouflage t-shirt tucked into pants", CLOTHES_TOPS, 212, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "195 Bluish Camouflage Raincoat with Hood", CLOTHES_TOPS, 214, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "196 Blue camouflage raincoat with hood on", CLOTHES_TOPS, 215, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "197 Open bluish camouflage raincoat", CLOTHES_TOPS, 216, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "198 Black tank top and bluish camouflage", CLOTHES_TOPS, 217, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "199 Black long sleeve t-shirt with bluish camouflage", CLOTHES_TOPS, 218, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "200 Blue and black open camouflage t-shirt", CLOTHES_TOPS, 219, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "201 Blue and Black Camouflage Sleeveless Open Top", CLOTHES_TOPS, 220, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "202 Blue sleeveless camouflage t-shirt ", CLOTHES_TOPS, 221, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "203 Collected bluish camouflage t-shirt", CLOTHES_TOPS, 222, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "204 Bluish Camouflage Loose Top", CLOTHES_TOPS, 223, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "205 Bluish camouflage t-shirt inside the pants", CLOTHES_TOPS, 224, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "206 Bluish Camouflage Tank Top", CLOTHES_TOPS, 225, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "207 Bluish camouflage tank top tucked into pants", CLOTHES_TOPS, 226, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "208 Closed long brown raincoat", CLOTHES_TOPS, 227, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "209 Long closed brown raincoat with hood on", CLOTHES_TOPS, 228, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "210 Blue Camouflage Sleeveless Jacket", CLOTHES_TOPS, 229, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "211 Blue sweatshirt and blue camouflage long sleeves", CLOTHES_TOPS, 230, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "212 Bluish camouflage long sleeve t-shirt", CLOTHES_TOPS, 231, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "213 Bluish camouflage shirt", CLOTHES_TOPS, 232, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "214 Sleeveless jacket closed by black zipper", CLOTHES_TOPS, 233, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "215 Black jacket with long sleeves closed with zipper", CLOTHES_TOPS, 234, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "216 White T-shirt with blue lettering sleeves", CLOTHES_TOPS, 235, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "217 Regular black t-shirt", CLOTHES_TOPS, 236, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "218 Jacket with grips", CLOTHES_TOPS, 238, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "219 Closed jacket", CLOTHES_TOPS, 239, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "220 Open jacket", CLOTHES_TOPS, 240, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "221 Closed jacket", CLOTHES_TOPS, 242, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "222 Open jacket", CLOTHES_TOPS, 243, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "223 Black shirt", CLOTHES_TOPS, 244, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "224 Polo", CLOTHES_TOPS, 246, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "225 Sleeveless", CLOTHES_TOPS, 247, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "226 Elegant coat", CLOTHES_TOPS, 248, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "227 Loose polo shirt", CLOTHES_TOPS, 249, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "228 Tucked in polo", CLOTHES_TOPS, 250, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "229 Closed jacket", CLOTHES_TOPS, 252, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "230 Christmas sweater", CLOTHES_TOPS, 253, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "231 Closed blue jacket", CLOTHES_TOPS, 257, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "232 Blue tucked shirt", CLOTHES_TOPS, 258, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "233 Yellow and khaki jacket", CLOTHES_TOPS, 259, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "234 Yellow and khaki jacket with hood", CLOTHES_TOPS, 261, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "235 Black jacket closed", CLOTHES_TOPS, 262, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "236 Long printed t-shirt", CLOTHES_TOPS, 264, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "237 Printed jacket closed", CLOTHES_TOPS, 265, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "238 Two-tone jacket", CLOTHES_TOPS, 266, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "239 Diamond sweater", CLOTHES_TOPS, 267, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "240 Printed sweater", CLOTHES_TOPS, 268, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "241 Printed shirt", CLOTHES_TOPS, 269, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "242 Open printed jacket", CLOTHES_TOPS, 270, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "243 Hooded Sweatshirts", CLOTHES_TOPS, 271, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "244 Hoodie", CLOTHES_TOPS, 272, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "245 Zipper jacket", CLOTHES_TOPS, 273, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "246 Closed motif jacket", CLOTHES_TOPS, 274, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "247 Open motif jacket", CLOTHES_TOPS, 275, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "248 Multicolor down", CLOTHES_TOPS, 278, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "249 Printed bikini", CLOTHES_TOPS, 279, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "250 Loose t-shirt", CLOTHES_TOPS, 280, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "251 Leopard tank top", CLOTHES_TOPS, 283, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "252 Printed top", CLOTHES_TOPS, 284, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "253 Light blue jacket closed", CLOTHES_TOPS, 285, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "254 Basic T-shirt", CLOTHES_TOPS, 286, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "255 Brands sweatshirt without hood", CLOTHES_TOPS, 292, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "256 Hooded sweatshirt", CLOTHES_TOPS, 293, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "257 Food jersey", CLOTHES_TOPS, 294, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "258 Food t-shirt", CLOTHES_TOPS, 295, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "259 Bloody jacket", CLOTHES_TOPS, 297, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "260 Food knit sweater", CLOTHES_TOPS, 301, SEX_FEMALE, 50),
            
            // Male tops
            new BusinessClothesModel(0, "1 Red t-shirt with gray sleeves", CLOTHES_TOPS, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "2 White T-shirt", CLOTHES_TOPS, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "3 Black and white check tank top", CLOTHES_TOPS, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "4 White open shirt", CLOTHES_TOPS, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "5 Black open shirt", CLOTHES_TOPS, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "6 White tank top", CLOTHES_TOPS, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "7 Black open sweatshirt with white stripes", CLOTHES_TOPS, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "8 White open sweatshirt", CLOTHES_TOPS, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "9 Blue and red rolled sleeve t-shirt", CLOTHES_TOPS, 8, SEX_MALE, 50),
            new BusinessClothesModel(0, "10 Black and white polo shirt with black stripes", CLOTHES_TOPS, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "11 Black low-cut jacket", CLOTHES_TOPS, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "12 Gray closed jacket", CLOTHES_TOPS, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "13 White shirt", CLOTHES_TOPS, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "14 White shirt inside the pants", CLOTHES_TOPS, 13, SEX_MALE, 50),
            new BusinessClothesModel(0, "15 Blue shirt blue and white squares", CLOTHES_TOPS, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "16 Without a shirt", CLOTHES_TOPS, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "17 Gray shirt", CLOTHES_TOPS, 16, SEX_MALE, 50),
            new BusinessClothesModel(0, "18 Blue tank top", CLOTHES_TOPS, 17, SEX_MALE, 50),
            new BusinessClothesModel(0, "19 Santa Claus t-shirt", CLOTHES_TOPS, 18, SEX_MALE, 50),
            new BusinessClothesModel(0, "20 Red and white neckline jacket", CLOTHES_TOPS, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "21 White jacket red scarf", CLOTHES_TOPS, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "22 Light gray sleeveless jacket", CLOTHES_TOPS, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "23 Basic white t-shirt", CLOTHES_TOPS, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "24 Open olive jacket", CLOTHES_TOPS, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "25 Light gray neckline jacket", CLOTHES_TOPS, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "26 Yellow sleeveless jacket", CLOTHES_TOPS, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "27 Black shirt with sleeves rolled up inside the pants", CLOTHES_TOPS, 26, SEX_MALE, 50),
            new BusinessClothesModel(0, "28 Black long jacket", CLOTHES_TOPS, 27, SEX_MALE, 50),
            new BusinessClothesModel(0, "29 Long black jacket with neckline", CLOTHES_TOPS, 28, SEX_MALE, 50),
            new BusinessClothesModel(0, "30 Long open black jacket", CLOTHES_TOPS, 29, SEX_MALE, 50),
            new BusinessClothesModel(0, "31 White and gray striped t-shirt", CLOTHES_TOPS, 33, SEX_MALE, 50),
            new BusinessClothesModel(0, "32 Basic black t-shirt", CLOTHES_TOPS, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "33 Black open jacket white stripes", CLOTHES_TOPS, 35, SEX_MALE, 50),
            new BusinessClothesModel(0, "34 Tank top white gray black and blue", CLOTHES_TOPS, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "35 Brown and camel jacket", CLOTHES_TOPS, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "36 Gray and black rolled-up t-shirt", CLOTHES_TOPS, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "37 Red blue and white polo shirt", CLOTHES_TOPS, 39, SEX_MALE, 50),
            new BusinessClothesModel(0, "38 Red sleeveless jacket", CLOTHES_TOPS, 40, SEX_MALE, 50),
            new BusinessClothesModel(0, "39 Orange brown camel check shirt", CLOTHES_TOPS, 41, SEX_MALE, 50),
            new BusinessClothesModel(0, "40 Blue sleeveless shirt with straps", CLOTHES_TOPS, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "41 Lime green t-shirt", CLOTHES_TOPS, 44, SEX_MALE, 50),
            new BusinessClothesModel(0, "42 USA tank top", CLOTHES_TOPS, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "43 Red blue and white open jacket", CLOTHES_TOPS, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "44 Basic blue t-shirt", CLOTHES_TOPS, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "45 Hooded green pockets sweatshirt", CLOTHES_TOPS, 48, SEX_MALE, 50),
            new BusinessClothesModel(0, "46 Black sweatshirt", CLOTHES_TOPS, 49, SEX_MALE, 50),
            new BusinessClothesModel(0, "47 Papa noel long sleeve t-shirt", CLOTHES_TOPS, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "48 Red and white long sleeve t-shirt", CLOTHES_TOPS, 52, SEX_MALE, 50),
            new BusinessClothesModel(0, "49 Black long sleeve t-shirt inside the pants", CLOTHES_TOPS, 53, SEX_MALE, 50),
            new BusinessClothesModel(0, "50 Hooded sweatshirt with black pockets", CLOTHES_TOPS, 54, SEX_MALE, 50),
            new BusinessClothesModel(0, "51 Dirty white t-shirt", CLOTHES_TOPS, 56, SEX_MALE, 50),
            new BusinessClothesModel(0, "52 Gray hoodie", CLOTHES_TOPS, 57, SEX_MALE, 50),
            new BusinessClothesModel(0, "53 Black open jacket", CLOTHES_TOPS, 58, SEX_MALE, 50),
            new BusinessClothesModel(0, "54 Long open gray jacket", CLOTHES_TOPS, 59, SEX_MALE, 50),
            new BusinessClothesModel(0, "55 Long neckline gray jacket", CLOTHES_TOPS, 60, SEX_MALE, 50),
            new BusinessClothesModel(0, "56 Zip-up brown sweatshirt", CLOTHES_TOPS, 61, SEX_MALE, 50),
            new BusinessClothesModel(0, "57 Long open black jacket", CLOTHES_TOPS, 62, SEX_MALE, 50),
            new BusinessClothesModel(0, "58 Basic black polo shirt", CLOTHES_TOPS, 63, SEX_MALE, 50),
            new BusinessClothesModel(0, "59 Black neckline jacket", CLOTHES_TOPS, 64, SEX_MALE, 50),
            new BusinessClothesModel(0, "60 Red shirt inside pants", CLOTHES_TOPS, 65, SEX_MALE, 50),
            new BusinessClothesModel(0, "61 Red cut-out t-shirt with black sleeves", CLOTHES_TOPS, 66, SEX_MALE, 50),
            new BusinessClothesModel(0, "62 Camiseta manga larga blanca", CLOTHES_TOPS, 67, SEX_MALE, 50),
            new BusinessClothesModel(0, "63 Sudadera negra capucha puesta", CLOTHES_TOPS, 68, SEX_MALE, 50),
            new BusinessClothesModel(0, "64 Sudadera negra capucha quitada", CLOTHES_TOPS, 69, SEX_MALE, 50),
            new BusinessClothesModel(0, "65 Chaqueta larga marron con pelito por cuello", CLOTHES_TOPS, 70, SEX_MALE, 50),
            new BusinessClothesModel(0, "66 Camiseta amarilla", CLOTHES_TOPS, 71, SEX_MALE, 50),
            new BusinessClothesModel(0, "67 Gabardina amarilla", CLOTHES_TOPS, 72, SEX_MALE, 50),
            new BusinessClothesModel(0, "68 Camiseta motivos amarillos y marron", CLOTHES_TOPS, 73, SEX_MALE, 50),
            new BusinessClothesModel(0, "69 Sudadera abierta marron motivos amarillos", CLOTHES_TOPS, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "70 Sudadera marron motivos amarillos", CLOTHES_TOPS, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "71 Gabardina camel cerrada cinturon", CLOTHES_TOPS, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "72 Gabardina gris abierta cuello levantado", CLOTHES_TOPS, 77, SEX_MALE, 50),
            new BusinessClothesModel(0, "73 Jersey cuadros amarillos y marron", CLOTHES_TOPS, 78, SEX_MALE, 50),
            new BusinessClothesModel(0, "74 Sudadera rojo y blanco W", CLOTHES_TOPS, 79, SEX_MALE, 50),
            new BusinessClothesModel(0, "75 Camiseta larga blanca", CLOTHES_TOPS, 80, SEX_MALE, 50),
            new BusinessClothesModel(0, "76 Camiseta larga negra", CLOTHES_TOPS, 81, SEX_MALE, 50),
            new BusinessClothesModel(0, "77 Polo largo gris", CLOTHES_TOPS, 82, SEX_MALE, 50),
            new BusinessClothesModel(0, "78 Camiseta larga gris rayas amarillas", CLOTHES_TOPS, 83, SEX_MALE, 50),
            new BusinessClothesModel(0, "79 Sudadera blanca letras frontal", CLOTHES_TOPS, 84, SEX_MALE, 50),
            new BusinessClothesModel(0, "80 Sudadera azul oscuro", CLOTHES_TOPS, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "81 Sudadera negra con capucha", CLOTHES_TOPS, 86, SEX_MALE, 50),
            new BusinessClothesModel(0, "82 Sudadera roja y blanca P", CLOTHES_TOPS, 87, SEX_MALE, 50),
            new BusinessClothesModel(0, "83 Sudadera roja y blanca P abierta", CLOTHES_TOPS, 88, SEX_MALE, 50),
            new BusinessClothesModel(0, "84 Jersey negro", CLOTHES_TOPS, 89, SEX_MALE, 50),
            new BusinessClothesModel(0, "85 Sudadera rojo oscuro y blanco", CLOTHES_TOPS, 90, SEX_MALE, 50),
            new BusinessClothesModel(0, "86 Camisa negra larga", CLOTHES_TOPS, 92, SEX_MALE, 50),
            new BusinessClothesModel(0, "87 Polo verde oscuro ANDREAS", CLOTHES_TOPS, 93, SEX_MALE, 50),
            new BusinessClothesModel(0, "88 Polo verde oscuro ANDREAS por dentro del pantalon", CLOTHES_TOPS, 94, SEX_MALE, 50),
            new BusinessClothesModel(0, "89 Camisa vaquera remangada por dentro del pantalon", CLOTHES_TOPS, 95, SEX_MALE, 50),
            new BusinessClothesModel(0, "90 Sudadera azul franja diagonal blanca", CLOTHES_TOPS, 96, SEX_MALE, 50),
            new BusinessClothesModel(0, "91 Camiseta marron", CLOTHES_TOPS, 97, SEX_MALE, 50),
            new BusinessClothesModel(0, "92 Camiseta manga larga marron por dentro del pantalon", CLOTHES_TOPS, 98, SEX_MALE, 50),
            new BusinessClothesModel(0, "93 Chaqueta blanca abierta", CLOTHES_TOPS, 99, SEX_MALE, 50),
            new BusinessClothesModel(0, "94 Chaqueta blanca escote", CLOTHES_TOPS, 100, SEX_MALE, 50),
            new BusinessClothesModel(0, "95 Chaqueta rojo oscuro abierta", CLOTHES_TOPS, 101, SEX_MALE, 50),
            new BusinessClothesModel(0, "96 Chaqueta rojo oscuro escote", CLOTHES_TOPS, 102, SEX_MALE, 50),
            new BusinessClothesModel(0, "97 Chaqueta marron motivos marron claro abierta", CLOTHES_TOPS, 103, SEX_MALE, 50),
            new BusinessClothesModel(0, "98 Chaqueta marron motivos marron claro escote", CLOTHES_TOPS, 104, SEX_MALE, 50),
            new BusinessClothesModel(0, "99 Camisa con motivos rojos y azules", CLOTHES_TOPS, 105, SEX_MALE, 50),
            new BusinessClothesModel(0, "100 Camisa abierta negra", CLOTHES_TOPS, 106, SEX_MALE, 50),
            new BusinessClothesModel(0, "101 Chaqueta blanca bolsillos cerrada", CLOTHES_TOPS, 107, SEX_MALE, 50),
            new BusinessClothesModel(0, "102 Chaqueta rojo oscuro y negro escote", CLOTHES_TOPS, 108, SEX_MALE, 50),
            new BusinessClothesModel(0, "103 Camiseta sin mangas blanca", CLOTHES_TOPS, 109, SEX_MALE, 50),
            new BusinessClothesModel(0, "104 Chaqueta cuero marron oscuro", CLOTHES_TOPS, 110, SEX_MALE, 50),
            new BusinessClothesModel(0, "105 Camiseta manga larga gris por dentro del pantalon", CLOTHES_TOPS, 111, SEX_MALE, 50),
            new BusinessClothesModel(0, "106 Camisa marron claro escote", CLOTHES_TOPS, 112, SEX_MALE, 50),
            new BusinessClothesModel(0, "107 Sudadera roja blanca y negra", CLOTHES_TOPS, 113, SEX_MALE, 50),
            new BusinessClothesModel(0, "108 Camisa escote blanca por dentro de pantalon", CLOTHES_TOPS, 114, SEX_MALE, 50),
            new BusinessClothesModel(0, "109 Gabardina marron oscuro abierta", CLOTHES_TOPS, 115, SEX_MALE, 50),
            new BusinessClothesModel(0, "110 Papa noel sucio", CLOTHES_TOPS, 116, SEX_MALE, 50),
            new BusinessClothesModel(0, "111 Camisa cuadros rojo y negro", CLOTHES_TOPS, 117, SEX_MALE, 50),
            new BusinessClothesModel(0, "112 Chaqueta verde y negro abierta", CLOTHES_TOPS, 118, SEX_MALE, 50),
            new BusinessClothesModel(0, "113 Camisa rayas azul y negro escote", CLOTHES_TOPS, 119, SEX_MALE, 50),
            new BusinessClothesModel(0, "114 Chaquetilla sin manga azul y negro a raya", CLOTHES_TOPS, 120, SEX_MALE, 50),
            new BusinessClothesModel(0, "115 Camiseta manga larga rayas negra y blanca", CLOTHES_TOPS, 121, SEX_MALE, 50),
            new BusinessClothesModel(0, "116 Chaqueta negra abierta", CLOTHES_TOPS, 122, SEX_MALE, 50),
            new BusinessClothesModel(0, "117 Polo blanco", CLOTHES_TOPS, 123, SEX_MALE, 50),
            new BusinessClothesModel(0, "118 Sudadera verde oscuro cerrada", CLOTHES_TOPS, 124, SEX_MALE, 50),
            new BusinessClothesModel(0, "119 Chaqueta camel", CLOTHES_TOPS, 125, SEX_MALE, 50),
            new BusinessClothesModel(0, "120 Camisa cuadros aguamarina y negro", CLOTHES_TOPS, 126, SEX_MALE, 50),
            new BusinessClothesModel(0, "121 Camisa cuadros aguamarina y negro abierta", CLOTHES_TOPS, 127, SEX_MALE, 50),
            new BusinessClothesModel(0, "122 Camiseta verde larga", CLOTHES_TOPS, 128, SEX_MALE, 50),
            new BusinessClothesModel(0, "123 Chaqueta negra triangulo rojo y blanco", CLOTHES_TOPS, 129, SEX_MALE, 50),
            new BusinessClothesModel(0, "124 Chaqueta negra triangulo rojo y blanco abierta", CLOTHES_TOPS, 130, SEX_MALE, 50),
            new BusinessClothesModel(0, "125 Polo LIBERTY negro", CLOTHES_TOPS, 131, SEX_MALE, 50),
            new BusinessClothesModel(0, "126 Polo LIBERTY negro por dentro del pantalon", CLOTHES_TOPS, 132, SEX_MALE, 50),
            new BusinessClothesModel(0, "127 Camisa remangada marron claro por dentro de pantalon", CLOTHES_TOPS, 133, SEX_MALE, 50),
            new BusinessClothesModel(0, "128 Sudadera LIBERTY negra", CLOTHES_TOPS, 134, SEX_MALE, 50),
            new BusinessClothesModel(0, "129 Camisa marron oscuro motivos amarillos", CLOTHES_TOPS, 135, SEX_MALE, 50),
            new BusinessClothesModel(0, "130 Chaqueta marron oscuro larga abierta", CLOTHES_TOPS, 136, SEX_MALE, 50),
            new BusinessClothesModel(0, "131 Gabardina marron oscuro", CLOTHES_TOPS, 138, SEX_MALE, 50),
            new BusinessClothesModel(0, "132 Camiseta manga larga gris por dentro de pantalon", CLOTHES_TOPS, 139, SEX_MALE, 50),
            new BusinessClothesModel(0, "133 Chaqueta azul oscuro larga escote", CLOTHES_TOPS, 140, SEX_MALE, 50),
            new BusinessClothesModel(0, "134 Sudadera azul rayas blancas", CLOTHES_TOPS, 141, SEX_MALE, 50),
            new BusinessClothesModel(0, "135 Gabardina negra larga abierta", CLOTHES_TOPS, 142, SEX_MALE, 50),
            new BusinessClothesModel(0, "136 Sudadera verde mangas blancas", CLOTHES_TOPS, 143, SEX_MALE, 50),
            new BusinessClothesModel(0, "137 Camisa blanca y rayas celestes", CLOTHES_TOPS, 144, SEX_MALE, 50),
            new BusinessClothesModel(0, "138 Chaqueta celeste a rayas", CLOTHES_TOPS, 145, SEX_MALE, 50),
            new BusinessClothesModel(0, "139 Camiseta blanca normal", CLOTHES_TOPS, 146, SEX_MALE, 50),
            new BusinessClothesModel(0, "140 Sudadera azul oscuro manga blanca", CLOTHES_TOPS, 147, SEX_MALE, 50),
            new BusinessClothesModel(0, "141 Sudadera verde y blanco", CLOTHES_TOPS, 148, SEX_MALE, 50),
            new BusinessClothesModel(0, "142 Camisa marron claro EEUU", CLOTHES_TOPS, 149, SEX_MALE, 50),
            new BusinessClothesModel(0, "143 Sudadera negra filos amarillos", CLOTHES_TOPS, 150, SEX_MALE, 50),
            new BusinessClothesModel(0, "144 Chaqueta negra abierta letras amarilla", CLOTHES_TOPS, 151, SEX_MALE, 50),
            new BusinessClothesModel(0, "145 Camiseta rosa y negro manga larga", CLOTHES_TOPS, 152, SEX_MALE, 50),
            new BusinessClothesModel(0, "146 Sudadera marron oscuro y rojo", CLOTHES_TOPS, 153, SEX_MALE, 50),
            new BusinessClothesModel(0, "147 Sudadera marron Italia", CLOTHES_TOPS, 154, SEX_MALE, 50),
            new BusinessClothesModel(0, "148 CAmisa amarilla EEUU", CLOTHES_TOPS, 155, SEX_MALE, 50),
            new BusinessClothesModel(0, "149 Chaquetilla negra abierta sin manga", CLOTHES_TOPS, 157, SEX_MALE, 50),
            new BusinessClothesModel(0, "150 Chaquetilla negra cremallera sin manga", CLOTHES_TOPS, 158, SEX_MALE, 50),
            new BusinessClothesModel(0, "151 Chaquetilla negra botones sin manga", CLOTHES_TOPS, 159, SEX_MALE, 50),
            new BusinessClothesModel(0, "152 Chaquetilla negra cremallera abierta", CLOTHES_TOPS, 160, SEX_MALE, 50),
            new BusinessClothesModel(0, "153 Chaqueta negra cuero", CLOTHES_TOPS, 161, SEX_MALE, 50),
            new BusinessClothesModel(0, "154 Chaqueta negra cuero sin manga", CLOTHES_TOPS, 162, SEX_MALE, 50),
            new BusinessClothesModel(0, "155 Camisa negra larga", CLOTHES_TOPS, 164, SEX_MALE, 50),
            new BusinessClothesModel(0, "156 Camisa azul oscuro mangas serpiente blanco", CLOTHES_TOPS, 165, SEX_MALE, 50),
            new BusinessClothesModel(0, "157 Chaqueta negra abierta", CLOTHES_TOPS, 166, SEX_MALE, 50),
            new BusinessClothesModel(0, "158 Abrigo rojo michelin", CLOTHES_TOPS, 167, SEX_MALE, 50),
            new BusinessClothesModel(0, "159 Sudadera marron oscuro capucha", CLOTHES_TOPS, 168, SEX_MALE, 50),
            new BusinessClothesModel(0, "160 Camisa vaquera larga abierta", CLOTHES_TOPS, 169, SEX_MALE, 50),
            new BusinessClothesModel(0, "161 Camisa vaquera abierta larga", CLOTHES_TOPS, 170, SEX_MALE, 50),
            new BusinessClothesModel(0, "162 Sudadera negra capucha", CLOTHES_TOPS, 171, SEX_MALE, 50),
            new BusinessClothesModel(0, "163 Chaqueta vaquera parches", CLOTHES_TOPS, 172, SEX_MALE, 50),
            new BusinessClothesModel(0, "164 Chaqueta vaquera sin mangas parches", CLOTHES_TOPS, 173, SEX_MALE, 50),
            new BusinessClothesModel(0, "165 Chaqueta cuero parches", CLOTHES_TOPS, 174, SEX_MALE, 50),
            new BusinessClothesModel(0, "166 Chaqueta cuero sin manga parches", CLOTHES_TOPS, 175, SEX_MALE, 50),
            new BusinessClothesModel(0, "167 Camisa negra letras rojas", CLOTHES_TOPS, 176, SEX_MALE, 50),
            new BusinessClothesModel(0, "168 Camisa azul oscuro sin manga", CLOTHES_TOPS, 177, SEX_MALE, 50),
            new BusinessClothesModel(0, "169 Camiseta manga larga negra y amarilla", CLOTHES_TOPS, 178, SEX_MALE, 50),
            new BusinessClothesModel(0, "170 Chaqueta cuero sin manga abierta", CLOTHES_TOPS, 179, SEX_MALE, 50),
            new BusinessClothesModel(0, "171 Chaqueta cuero sin manga", CLOTHES_TOPS, 180, SEX_MALE, 50),
            new BusinessClothesModel(0, "172 Chaqueta cuero abierta", CLOTHES_TOPS, 181, SEX_MALE, 50),
            new BusinessClothesModel(0, "173 Sudadera negra capucha", CLOTHES_TOPS, 182, SEX_MALE, 50),
            new BusinessClothesModel(0, "174 Chaqueta negra y blanca a rayas", CLOTHES_TOPS, 183, SEX_MALE, 50),
            new BusinessClothesModel(0, "175 Chaqueta verde oscuro larga capucha", CLOTHES_TOPS, 184, SEX_MALE, 50),
            new BusinessClothesModel(0, "176 Chaqueta verde oscuro larga abierta", CLOTHES_TOPS, 185, SEX_MALE, 50),
            new BusinessClothesModel(0, "177 Gabardina negra", CLOTHES_TOPS, 187, SEX_MALE, 50),
            new BusinessClothesModel(0, "178 Gabardina camel capucha", CLOTHES_TOPS, 188, SEX_MALE, 50),
            new BusinessClothesModel(0, "179 Gabardina camel capucha abierta", CLOTHES_TOPS, 189, SEX_MALE, 50),
            new BusinessClothesModel(0, "180 Jersey gris rombos negros", CLOTHES_TOPS, 190, SEX_MALE, 50),
            new BusinessClothesModel(0, "181 Chaqueta negra y verde camuflaje abierta", CLOTHES_TOPS, 191, SEX_MALE, 50),
            new BusinessClothesModel(0, "182 Gabardina gris abierta", CLOTHES_TOPS, 192, SEX_MALE, 50),
            new BusinessClothesModel(0, "183 Camiseta blanca letras naranja", CLOTHES_TOPS, 193, SEX_MALE, 50),
            new BusinessClothesModel(0, "184 Navideño", CLOTHES_TOPS, 194, SEX_MALE, 50),
            new BusinessClothesModel(0, "185 Jersey gris con bits amarillo", CLOTHES_TOPS, 196, SEX_MALE, 50),
            new BusinessClothesModel(0, "186 Camisa azul arboles blancos", CLOTHES_TOPS, 198, SEX_MALE, 50),
            new BusinessClothesModel(0, "187 Sudadera amarilla parches naranjas capucha", CLOTHES_TOPS, 200, SEX_MALE, 50),
            new BusinessClothesModel(0, "188 Camiseta negra manga larga dibujos amarillos rojos y celeste", CLOTHES_TOPS, 201, SEX_MALE, 50),
            new BusinessClothesModel(0, "189 Camiseta negra sin manga con capucha puesta", CLOTHES_TOPS, 202, SEX_MALE, 50),
            new BusinessClothesModel(0, "190 Sudadera amarilla parche naranja capucha puesta", CLOTHES_TOPS, 203, SEX_MALE, 50),
            new BusinessClothesModel(0, "191 Gabardina negra larga cacpucha puesta", CLOTHES_TOPS, 204, SEX_MALE, 50),
            new BusinessClothesModel(0, "192 Sudadera sin manga cuello ancho", CLOTHES_TOPS, 205, SEX_MALE, 50),
            new BusinessClothesModel(0, "193 Camiseta sin manga camuflaje azul capucha", CLOTHES_TOPS, 206, SEX_MALE, 50),
            new BusinessClothesModel(0, "194 Camiseta sin manga camuflaje azul capucha puesta", CLOTHES_TOPS, 207, SEX_MALE, 50),
            new BusinessClothesModel(0, "195 Camiseta camuflaje azul", CLOTHES_TOPS, 208, SEX_MALE, 50),
            new BusinessClothesModel(0, "196 Camiseta larga azul camuflaje", CLOTHES_TOPS, 209, SEX_MALE, 50),
            new BusinessClothesModel(0, "197 Camiseta larga azul camuflaje capucha", CLOTHES_TOPS, 210, SEX_MALE, 50),
            new BusinessClothesModel(0, "198 Camiseta larga azul camuflaje capucha puesta", CLOTHES_TOPS, 211, SEX_MALE, 50),
            new BusinessClothesModel(0, "199 Camiseta larga abierta azul camuflaje", CLOTHES_TOPS, 212, SEX_MALE, 50),
            new BusinessClothesModel(0, "200 Camiseta negra y azul camuflaje sin manga", CLOTHES_TOPS, 213, SEX_MALE, 50),
            new BusinessClothesModel(0, "201 Camiseta negra y azul camuflaje", CLOTHES_TOPS, 214, SEX_MALE, 50),
            new BusinessClothesModel(0, "202 Chaqueta azul camuflaje y negra", CLOTHES_TOPS, 215, SEX_MALE, 50),
            new BusinessClothesModel(0, "203 Chaqueta sin manga azul camuflaje y negra", CLOTHES_TOPS, 216, SEX_MALE, 50),
            new BusinessClothesModel(0, "204 Chaqueta marron larga", CLOTHES_TOPS, 217, SEX_MALE, 50),
            new BusinessClothesModel(0, "205 Chaqueta marron larga capucha puesta", CLOTHES_TOPS, 218, SEX_MALE, 50),
            new BusinessClothesModel(0, "206 Camiseta sin manga azul camuflaje", CLOTHES_TOPS, 219, SEX_MALE, 50),
            new BusinessClothesModel(0, "207 Sudadera azul mangas azul camuflaje", CLOTHES_TOPS, 220, SEX_MALE, 50),
            new BusinessClothesModel(0, "208 Camiseta manga larga azul camuflaje", CLOTHES_TOPS, 221, SEX_MALE, 50),
            new BusinessClothesModel(0, "209 Camiseta azul camuflaje", CLOTHES_TOPS, 222, SEX_MALE, 50),
            new BusinessClothesModel(0, "210 Camiseta sin manga negra cremallera", CLOTHES_TOPS, 223, SEX_MALE, 50),
            new BusinessClothesModel(0, "211 Chaqueta negra cremallera", CLOTHES_TOPS, 224, SEX_MALE, 50),
            new BusinessClothesModel(0, "212 Camiseta blanca y azul oscuro 98", CLOTHES_TOPS, 225, SEX_MALE, 50),
            new BusinessClothesModel(0, "213 Camiseta negra normal", CLOTHES_TOPS, 226, SEX_MALE, 50),
            new BusinessClothesModel(0, "214 Chaqueta ancha con agarres", CLOTHES_TOPS, 228, SEX_MALE, 50),
            new BusinessClothesModel(0, "215 Cazadora cerrada", CLOTHES_TOPS, 229, SEX_MALE, 50),
            new BusinessClothesModel(0, "216 Cazadora abierta", CLOTHES_TOPS, 230, SEX_MALE, 50),
            new BusinessClothesModel(0, "217 Chaqueta ancha con agarres", CLOTHES_TOPS, 231, SEX_MALE, 50),
            new BusinessClothesModel(0, "218 Chaqueton cerrado", CLOTHES_TOPS, 232, SEX_MALE, 50),
            new BusinessClothesModel(0, "219 Chaqueton abierto", CLOTHES_TOPS, 233, SEX_MALE, 50),
            new BusinessClothesModel(0, "220 Camisa negra", CLOTHES_TOPS, 234, SEX_MALE, 50),
            new BusinessClothesModel(0, "221 Polo rayas suelto", CLOTHES_TOPS, 235, SEX_MALE, 50),
            new BusinessClothesModel(0, "222 Polo rayas remetido", CLOTHES_TOPS, 236, SEX_MALE, 50),
            new BusinessClothesModel(0, "223 Camiseta sin mangas", CLOTHES_TOPS, 237, SEX_MALE, 50),
            new BusinessClothesModel(0, "224 Camiseta remangada corta", CLOTHES_TOPS, 238, SEX_MALE, 50),
            new BusinessClothesModel(0, "225 Camiseta estampada", CLOTHES_TOPS, 239, SEX_MALE, 50),
            new BusinessClothesModel(0, "226 Chaqueta elegante", CLOTHES_TOPS, 240, SEX_MALE, 50),
            new BusinessClothesModel(0, "227 Polo suelto", CLOTHES_TOPS, 241, SEX_MALE, 50),
            new BusinessClothesModel(0, "228 Polo remetido", CLOTHES_TOPS, 242, SEX_MALE, 50),
            new BusinessClothesModel(0, "229 Chaqueta cerrada", CLOTHES_TOPS, 244, SEX_MALE, 50),
            new BusinessClothesModel(0, "230 Jersey navidad", CLOTHES_TOPS, 245, SEX_MALE, 50),
            new BusinessClothesModel(0, "231 Cazadora azul cerrada", CLOTHES_TOPS, 249, SEX_MALE, 50),
            new BusinessClothesModel(0, "232 Camisa azul remetida", CLOTHES_TOPS, 250, SEX_MALE, 50),
            new BusinessClothesModel(0, "233 Chaqueta bicolor sin capucha", CLOTHES_TOPS, 251, SEX_MALE, 50),
            new BusinessClothesModel(0, "234 Chaqueta bicolor con capucha", CLOTHES_TOPS, 253, SEX_MALE, 50),
            new BusinessClothesModel(0, "235 Camiseta larga estampada", CLOTHES_TOPS, 255, SEX_MALE, 50),
            new BusinessClothesModel(0, "236 Chaqueta estampada cerrada", CLOTHES_TOPS, 256, SEX_MALE, 50),
            new BusinessClothesModel(0, "237 Chaqueta bicolor", CLOTHES_TOPS, 257, SEX_MALE, 50),
            new BusinessClothesModel(0, "238 Jersey rombos", CLOTHES_TOPS, 258, SEX_MALE, 50),
            new BusinessClothesModel(0, "239 Jersey estampado", CLOTHES_TOPS, 259, SEX_MALE, 50),
            new BusinessClothesModel(0, "240 Camisa estampados", CLOTHES_TOPS, 260, SEX_MALE, 50),
            new BusinessClothesModel(0, "241 Chaqueta estampada abierta", CLOTHES_TOPS, 261, SEX_MALE, 50),
            new BusinessClothesModel(0, "242 Sudadera sin capucha", CLOTHES_TOPS, 262, SEX_MALE, 50),
            new BusinessClothesModel(0, "243 Sudadera con capucha", CLOTHES_TOPS, 263, SEX_MALE, 50),
            new BusinessClothesModel(0, "244 Chaqueta cremalleras", CLOTHES_TOPS, 264, SEX_MALE, 50),
            new BusinessClothesModel(0, "245 Chaqueta motivos cerrada", CLOTHES_TOPS, 265, SEX_MALE, 50),
            new BusinessClothesModel(0, "246 Chaqueta motivos abierta", CLOTHES_TOPS, 266, SEX_MALE, 50),
            new BusinessClothesModel(0, "247 Plumon multicolor", CLOTHES_TOPS, 269, SEX_MALE, 50),
            new BusinessClothesModel(0, "248 Camiseta basica", CLOTHES_TOPS, 271, SEX_MALE, 50),
            new BusinessClothesModel(0, "249 Chaqueta celeste cerrada", CLOTHES_TOPS, 272, SEX_MALE, 50),
            new BusinessClothesModel(0, "250 Camiseta basica blanca", CLOTHES_TOPS, 273, SEX_MALE, 50),
            new BusinessClothesModel(0, "251 Sudadera marcas sin capucha", CLOTHES_TOPS, 279, SEX_MALE, 50),
            new BusinessClothesModel(0, "252 Sudadera marcas con capucha", CLOTHES_TOPS, 280, SEX_MALE, 50),
            new BusinessClothesModel(0, "253 Jersey comida", CLOTHES_TOPS, 281, SEX_MALE, 50),
            new BusinessClothesModel(0, "254 Camiseta comida", CLOTHES_TOPS, 282, SEX_MALE, 50),
            new BusinessClothesModel(0, "255 Chaqueta ensangrentada", CLOTHES_TOPS, 284, SEX_MALE, 50),
            new BusinessClothesModel(0, "256 Jersey de punto comida", CLOTHES_TOPS, 288, SEX_MALE, 50),

            //Body armor male

            new BusinessClothesModel(0, "0", CLOTHES_ARMOR, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "1", CLOTHES_ARMOR, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "2", CLOTHES_ARMOR, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "3", CLOTHES_ARMOR, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "4", CLOTHES_ARMOR, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "5", CLOTHES_ARMOR, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "6", CLOTHES_ARMOR, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "7", CLOTHES_ARMOR, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "8", CLOTHES_ARMOR, 8, SEX_MALE, 50),
            new BusinessClothesModel(0, "9", CLOTHES_ARMOR, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "10", CLOTHES_ARMOR, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "11", CLOTHES_ARMOR, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "12", CLOTHES_ARMOR, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "13", CLOTHES_ARMOR, 13, SEX_MALE, 50),
            new BusinessClothesModel(0, "14", CLOTHES_ARMOR, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "15", CLOTHES_ARMOR, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "16", CLOTHES_ARMOR, 16, SEX_MALE, 50),
            
            //body armor female
            
            new BusinessClothesModel(0, "0", CLOTHES_ARMOR, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "1", CLOTHES_ARMOR, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "2", CLOTHES_ARMOR, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "3", CLOTHES_ARMOR, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "4", CLOTHES_ARMOR, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "5", CLOTHES_ARMOR, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "6", CLOTHES_ARMOR, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "7", CLOTHES_ARMOR, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "8", CLOTHES_ARMOR, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "9", CLOTHES_ARMOR, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "10", CLOTHES_ARMOR, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "11", CLOTHES_ARMOR, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "12", CLOTHES_ARMOR, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "13", CLOTHES_ARMOR, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "14", CLOTHES_ARMOR, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "15", CLOTHES_ARMOR, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "16", CLOTHES_ARMOR, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "17", CLOTHES_ARMOR, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "18", CLOTHES_ARMOR, 18, SEX_FEMALE, 50),





            // Female undershirt
            new BusinessClothesModel(0, "0", CLOTHES_UNDERSHIRT, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "1", CLOTHES_UNDERSHIRT, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "2", CLOTHES_UNDERSHIRT, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "3", CLOTHES_UNDERSHIRT, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "4", CLOTHES_UNDERSHIRT, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "5", CLOTHES_UNDERSHIRT, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "6", CLOTHES_UNDERSHIRT, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "7", CLOTHES_UNDERSHIRT, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "8", CLOTHES_UNDERSHIRT, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "9", CLOTHES_UNDERSHIRT, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "10", CLOTHES_UNDERSHIRT, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "11", CLOTHES_UNDERSHIRT, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "12", CLOTHES_UNDERSHIRT, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "13", CLOTHES_UNDERSHIRT, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "14", CLOTHES_UNDERSHIRT, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "15", CLOTHES_UNDERSHIRT, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "16", CLOTHES_UNDERSHIRT, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "17", CLOTHES_UNDERSHIRT, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "18", CLOTHES_UNDERSHIRT, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "19", CLOTHES_UNDERSHIRT, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "20", CLOTHES_UNDERSHIRT, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "21", CLOTHES_UNDERSHIRT, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "22", CLOTHES_UNDERSHIRT, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "23", CLOTHES_UNDERSHIRT, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "24", CLOTHES_UNDERSHIRT, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "25", CLOTHES_UNDERSHIRT, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "26", CLOTHES_UNDERSHIRT, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "27", CLOTHES_UNDERSHIRT, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "28", CLOTHES_UNDERSHIRT, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "29", CLOTHES_UNDERSHIRT, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "30", CLOTHES_UNDERSHIRT, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "31", CLOTHES_UNDERSHIRT, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "32", CLOTHES_UNDERSHIRT, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "33", CLOTHES_UNDERSHIRT, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "34", CLOTHES_UNDERSHIRT, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "35", CLOTHES_UNDERSHIRT, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "36", CLOTHES_UNDERSHIRT, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "37", CLOTHES_UNDERSHIRT, 37, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "38", CLOTHES_UNDERSHIRT, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "39", CLOTHES_UNDERSHIRT, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "40", CLOTHES_UNDERSHIRT, 40, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "41", CLOTHES_UNDERSHIRT, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "42", CLOTHES_UNDERSHIRT, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "43", CLOTHES_UNDERSHIRT, 43, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "44", CLOTHES_UNDERSHIRT, 44, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "45", CLOTHES_UNDERSHIRT, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "46", CLOTHES_UNDERSHIRT, 46, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "47", CLOTHES_UNDERSHIRT, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "48", CLOTHES_UNDERSHIRT, 48, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "49", CLOTHES_UNDERSHIRT, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "50", CLOTHES_UNDERSHIRT, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "51", CLOTHES_UNDERSHIRT, 51, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "52", CLOTHES_UNDERSHIRT, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "53", CLOTHES_UNDERSHIRT, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "54", CLOTHES_UNDERSHIRT, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "55", CLOTHES_UNDERSHIRT, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "56", CLOTHES_UNDERSHIRT, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "57", CLOTHES_UNDERSHIRT, 57, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "58", CLOTHES_UNDERSHIRT, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "59", CLOTHES_UNDERSHIRT, 59, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "60", CLOTHES_UNDERSHIRT, 60, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "61", CLOTHES_UNDERSHIRT, 61, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "62", CLOTHES_UNDERSHIRT, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "63", CLOTHES_UNDERSHIRT, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "64", CLOTHES_UNDERSHIRT, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "65", CLOTHES_UNDERSHIRT, 65, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "66", CLOTHES_UNDERSHIRT, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "67", CLOTHES_UNDERSHIRT, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "68", CLOTHES_UNDERSHIRT, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "69", CLOTHES_UNDERSHIRT, 69, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "70", CLOTHES_UNDERSHIRT, 70, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "71", CLOTHES_UNDERSHIRT, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "72", CLOTHES_UNDERSHIRT, 72, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "73", CLOTHES_UNDERSHIRT, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "74", CLOTHES_UNDERSHIRT, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "75", CLOTHES_UNDERSHIRT, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "76", CLOTHES_UNDERSHIRT, 76, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "77", CLOTHES_UNDERSHIRT, 77, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "78", CLOTHES_UNDERSHIRT, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "79", CLOTHES_UNDERSHIRT, 79, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "80", CLOTHES_UNDERSHIRT, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "81", CLOTHES_UNDERSHIRT, 81, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "82", CLOTHES_UNDERSHIRT, 82, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "83", CLOTHES_UNDERSHIRT, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "84", CLOTHES_UNDERSHIRT, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "85", CLOTHES_UNDERSHIRT, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "86", CLOTHES_UNDERSHIRT, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "87", CLOTHES_UNDERSHIRT, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "88", CLOTHES_UNDERSHIRT, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "89", CLOTHES_UNDERSHIRT, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "90", CLOTHES_UNDERSHIRT, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "91", CLOTHES_UNDERSHIRT, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "92", CLOTHES_UNDERSHIRT, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "93", CLOTHES_UNDERSHIRT, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "94", CLOTHES_UNDERSHIRT, 94, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "95", CLOTHES_UNDERSHIRT, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "96", CLOTHES_UNDERSHIRT, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "97", CLOTHES_UNDERSHIRT, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "98", CLOTHES_UNDERSHIRT, 98, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "99", CLOTHES_UNDERSHIRT, 99, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "100", CLOTHES_UNDERSHIRT, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "101", CLOTHES_UNDERSHIRT, 101, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "102", CLOTHES_UNDERSHIRT, 102, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "103", CLOTHES_UNDERSHIRT, 103, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "104", CLOTHES_UNDERSHIRT, 104, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "105", CLOTHES_UNDERSHIRT, 105, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "106", CLOTHES_UNDERSHIRT, 106, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "107", CLOTHES_UNDERSHIRT, 107, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "108", CLOTHES_UNDERSHIRT, 108, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "109", CLOTHES_UNDERSHIRT, 109, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "110", CLOTHES_UNDERSHIRT, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "111", CLOTHES_UNDERSHIRT, 111, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "112", CLOTHES_UNDERSHIRT, 112, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "113", CLOTHES_UNDERSHIRT, 113, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "114", CLOTHES_UNDERSHIRT, 114, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "115", CLOTHES_UNDERSHIRT, 115, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "116", CLOTHES_UNDERSHIRT, 116, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "117", CLOTHES_UNDERSHIRT, 117, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "118", CLOTHES_UNDERSHIRT, 118, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "119", CLOTHES_UNDERSHIRT, 119, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "120", CLOTHES_UNDERSHIRT, 120, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "121", CLOTHES_UNDERSHIRT, 121, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "122", CLOTHES_UNDERSHIRT, 122, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "123", CLOTHES_UNDERSHIRT, 123, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "124", CLOTHES_UNDERSHIRT, 124, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "125", CLOTHES_UNDERSHIRT, 125, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "126", CLOTHES_UNDERSHIRT, 126, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "127", CLOTHES_UNDERSHIRT, 127, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "128", CLOTHES_UNDERSHIRT, 128, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "129", CLOTHES_UNDERSHIRT, 129, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "130", CLOTHES_UNDERSHIRT, 130, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "131", CLOTHES_UNDERSHIRT, 131, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "132", CLOTHES_UNDERSHIRT, 132, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "133", CLOTHES_UNDERSHIRT, 133, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "134", CLOTHES_UNDERSHIRT, 134, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "135", CLOTHES_UNDERSHIRT, 135, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "136", CLOTHES_UNDERSHIRT, 136, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "137", CLOTHES_UNDERSHIRT, 137, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "138", CLOTHES_UNDERSHIRT, 138, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "139", CLOTHES_UNDERSHIRT, 139, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "140", CLOTHES_UNDERSHIRT, 140, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "141", CLOTHES_UNDERSHIRT, 141, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "142", CLOTHES_UNDERSHIRT, 142, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "143", CLOTHES_UNDERSHIRT, 143, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "144", CLOTHES_UNDERSHIRT, 144, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "145", CLOTHES_UNDERSHIRT, 145, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "146", CLOTHES_UNDERSHIRT, 146, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "147", CLOTHES_UNDERSHIRT, 147, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "148", CLOTHES_UNDERSHIRT, 148, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "149", CLOTHES_UNDERSHIRT, 149, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "150", CLOTHES_UNDERSHIRT, 150, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "151", CLOTHES_UNDERSHIRT, 151, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "152", CLOTHES_UNDERSHIRT, 152, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "153", CLOTHES_UNDERSHIRT, 153, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "154", CLOTHES_UNDERSHIRT, 154, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "155", CLOTHES_UNDERSHIRT, 155, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "156", CLOTHES_UNDERSHIRT, 156, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "157", CLOTHES_UNDERSHIRT, 157, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "158", CLOTHES_UNDERSHIRT, 158, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "159", CLOTHES_UNDERSHIRT, 159, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "160", CLOTHES_UNDERSHIRT, 160, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "161", CLOTHES_UNDERSHIRT, 161, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "162", CLOTHES_UNDERSHIRT, 162, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "163", CLOTHES_UNDERSHIRT, 163, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "164", CLOTHES_UNDERSHIRT, 164, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "165", CLOTHES_UNDERSHIRT, 165, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "166", CLOTHES_UNDERSHIRT, 166, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "167", CLOTHES_UNDERSHIRT, 167, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "168", CLOTHES_UNDERSHIRT, 168, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "169", CLOTHES_UNDERSHIRT, 169, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "170", CLOTHES_UNDERSHIRT, 170, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "171", CLOTHES_UNDERSHIRT, 171, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "172", CLOTHES_UNDERSHIRT, 172, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "173", CLOTHES_UNDERSHIRT, 173, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "174", CLOTHES_UNDERSHIRT, 174, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "175", CLOTHES_UNDERSHIRT, 175, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "176", CLOTHES_UNDERSHIRT, 176, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "177", CLOTHES_UNDERSHIRT, 177, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "178", CLOTHES_UNDERSHIRT, 178, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "179", CLOTHES_UNDERSHIRT, 179, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "180", CLOTHES_UNDERSHIRT, 180, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "181", CLOTHES_UNDERSHIRT, 181, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "182", CLOTHES_UNDERSHIRT, 182, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "183", CLOTHES_UNDERSHIRT, 183, SEX_FEMALE, 50),
            new BusinessClothesModel(0, "184", CLOTHES_UNDERSHIRT, 184, SEX_FEMALE, 50),
                                    
            // Male undershirt
            new BusinessClothesModel(0, "0", CLOTHES_UNDERSHIRT, 0, SEX_MALE, 50),
            new BusinessClothesModel(0, "1", CLOTHES_UNDERSHIRT, 1, SEX_MALE, 50),
            new BusinessClothesModel(0, "2", CLOTHES_UNDERSHIRT, 2, SEX_MALE, 50),
            new BusinessClothesModel(0, "3", CLOTHES_UNDERSHIRT, 3, SEX_MALE, 50),
            new BusinessClothesModel(0, "4", CLOTHES_UNDERSHIRT, 4, SEX_MALE, 50),
            new BusinessClothesModel(0, "5", CLOTHES_UNDERSHIRT, 5, SEX_MALE, 50),
            new BusinessClothesModel(0, "6", CLOTHES_UNDERSHIRT, 6, SEX_MALE, 50),
            new BusinessClothesModel(0, "7", CLOTHES_UNDERSHIRT, 7, SEX_MALE, 50),
            new BusinessClothesModel(0, "8", CLOTHES_UNDERSHIRT, 8, SEX_MALE, 50),
            new BusinessClothesModel(0, "9", CLOTHES_UNDERSHIRT, 9, SEX_MALE, 50),
            new BusinessClothesModel(0, "10", CLOTHES_UNDERSHIRT, 10, SEX_MALE, 50),
            new BusinessClothesModel(0, "11", CLOTHES_UNDERSHIRT, 11, SEX_MALE, 50),
            new BusinessClothesModel(0, "12", CLOTHES_UNDERSHIRT, 12, SEX_MALE, 50),
            new BusinessClothesModel(0, "13", CLOTHES_UNDERSHIRT, 13, SEX_MALE, 50),
            new BusinessClothesModel(0, "14", CLOTHES_UNDERSHIRT, 14, SEX_MALE, 50),
            new BusinessClothesModel(0, "15", CLOTHES_UNDERSHIRT, 15, SEX_MALE, 50),
            new BusinessClothesModel(0, "16", CLOTHES_UNDERSHIRT, 16, SEX_MALE, 50),
            new BusinessClothesModel(0, "17", CLOTHES_UNDERSHIRT, 17, SEX_MALE, 50),
            new BusinessClothesModel(0, "18", CLOTHES_UNDERSHIRT, 18, SEX_MALE, 50),
            new BusinessClothesModel(0, "19", CLOTHES_UNDERSHIRT, 19, SEX_MALE, 50),
            new BusinessClothesModel(0, "20", CLOTHES_UNDERSHIRT, 20, SEX_MALE, 50),
            new BusinessClothesModel(0, "21", CLOTHES_UNDERSHIRT, 21, SEX_MALE, 50),
            new BusinessClothesModel(0, "22", CLOTHES_UNDERSHIRT, 22, SEX_MALE, 50),
            new BusinessClothesModel(0, "23", CLOTHES_UNDERSHIRT, 23, SEX_MALE, 50),
            new BusinessClothesModel(0, "24", CLOTHES_UNDERSHIRT, 24, SEX_MALE, 50),
            new BusinessClothesModel(0, "25", CLOTHES_UNDERSHIRT, 25, SEX_MALE, 50),
            new BusinessClothesModel(0, "26", CLOTHES_UNDERSHIRT, 26, SEX_MALE, 50),
            new BusinessClothesModel(0, "27", CLOTHES_UNDERSHIRT, 27, SEX_MALE, 50),
            new BusinessClothesModel(0, "28", CLOTHES_UNDERSHIRT, 28, SEX_MALE, 50),
            new BusinessClothesModel(0, "29", CLOTHES_UNDERSHIRT, 29, SEX_MALE, 50),
            new BusinessClothesModel(0, "30", CLOTHES_UNDERSHIRT, 30, SEX_MALE, 50),
            new BusinessClothesModel(0, "31", CLOTHES_UNDERSHIRT, 31, SEX_MALE, 50),
            new BusinessClothesModel(0, "32", CLOTHES_UNDERSHIRT, 32, SEX_MALE, 50),
            new BusinessClothesModel(0, "33", CLOTHES_UNDERSHIRT, 33, SEX_MALE, 50),
            new BusinessClothesModel(0, "34", CLOTHES_UNDERSHIRT, 34, SEX_MALE, 50),
            new BusinessClothesModel(0, "35", CLOTHES_UNDERSHIRT, 35, SEX_MALE, 50),
            new BusinessClothesModel(0, "36", CLOTHES_UNDERSHIRT, 36, SEX_MALE, 50),
            new BusinessClothesModel(0, "37", CLOTHES_UNDERSHIRT, 37, SEX_MALE, 50),
            new BusinessClothesModel(0, "38", CLOTHES_UNDERSHIRT, 38, SEX_MALE, 50),
            new BusinessClothesModel(0, "39", CLOTHES_UNDERSHIRT, 39, SEX_MALE, 50),
            new BusinessClothesModel(0, "40", CLOTHES_UNDERSHIRT, 40, SEX_MALE, 50),
            new BusinessClothesModel(0, "41", CLOTHES_UNDERSHIRT, 41, SEX_MALE, 50),
            new BusinessClothesModel(0, "42", CLOTHES_UNDERSHIRT, 42, SEX_MALE, 50),
            new BusinessClothesModel(0, "43", CLOTHES_UNDERSHIRT, 43, SEX_MALE, 50),
            new BusinessClothesModel(0, "44", CLOTHES_UNDERSHIRT, 44, SEX_MALE, 50),
            new BusinessClothesModel(0, "45", CLOTHES_UNDERSHIRT, 45, SEX_MALE, 50),
            new BusinessClothesModel(0, "46", CLOTHES_UNDERSHIRT, 46, SEX_MALE, 50),
            new BusinessClothesModel(0, "47", CLOTHES_UNDERSHIRT, 47, SEX_MALE, 50),
            new BusinessClothesModel(0, "48", CLOTHES_UNDERSHIRT, 48, SEX_MALE, 50),
            new BusinessClothesModel(0, "49", CLOTHES_UNDERSHIRT, 49, SEX_MALE, 50),
            new BusinessClothesModel(0, "50", CLOTHES_UNDERSHIRT, 50, SEX_MALE, 50),
            new BusinessClothesModel(0, "51", CLOTHES_UNDERSHIRT, 51, SEX_MALE, 50),
            new BusinessClothesModel(0, "52", CLOTHES_UNDERSHIRT, 52, SEX_MALE, 50),
            new BusinessClothesModel(0, "53", CLOTHES_UNDERSHIRT, 53, SEX_MALE, 50),
            new BusinessClothesModel(0, "54", CLOTHES_UNDERSHIRT, 54, SEX_MALE, 50),
            new BusinessClothesModel(0, "55", CLOTHES_UNDERSHIRT, 55, SEX_MALE, 50),
            new BusinessClothesModel(0, "56", CLOTHES_UNDERSHIRT, 56, SEX_MALE, 50),
            new BusinessClothesModel(0, "57", CLOTHES_UNDERSHIRT, 57, SEX_MALE, 50),
            new BusinessClothesModel(0, "58", CLOTHES_UNDERSHIRT, 58, SEX_MALE, 50),
            new BusinessClothesModel(0, "59", CLOTHES_UNDERSHIRT, 59, SEX_MALE, 50),
            new BusinessClothesModel(0, "60", CLOTHES_UNDERSHIRT, 60, SEX_MALE, 50),
            new BusinessClothesModel(0, "61", CLOTHES_UNDERSHIRT, 61, SEX_MALE, 50),
            new BusinessClothesModel(0, "62", CLOTHES_UNDERSHIRT, 62, SEX_MALE, 50),
            new BusinessClothesModel(0, "63", CLOTHES_UNDERSHIRT, 63, SEX_MALE, 50),
            new BusinessClothesModel(0, "64", CLOTHES_UNDERSHIRT, 64, SEX_MALE, 50),
            new BusinessClothesModel(0, "65", CLOTHES_UNDERSHIRT, 65, SEX_MALE, 50),
            new BusinessClothesModel(0, "66", CLOTHES_UNDERSHIRT, 66, SEX_MALE, 50),
            new BusinessClothesModel(0, "67", CLOTHES_UNDERSHIRT, 67, SEX_MALE, 50),
            new BusinessClothesModel(0, "68", CLOTHES_UNDERSHIRT, 68, SEX_MALE, 50),
            new BusinessClothesModel(0, "69", CLOTHES_UNDERSHIRT, 69, SEX_MALE, 50),
            new BusinessClothesModel(0, "70", CLOTHES_UNDERSHIRT, 70, SEX_MALE, 50),
            new BusinessClothesModel(0, "71", CLOTHES_UNDERSHIRT, 71, SEX_MALE, 50),
            new BusinessClothesModel(0, "72", CLOTHES_UNDERSHIRT, 72, SEX_MALE, 50),
            new BusinessClothesModel(0, "73", CLOTHES_UNDERSHIRT, 73, SEX_MALE, 50),
            new BusinessClothesModel(0, "74", CLOTHES_UNDERSHIRT, 74, SEX_MALE, 50),
            new BusinessClothesModel(0, "75", CLOTHES_UNDERSHIRT, 75, SEX_MALE, 50),
            new BusinessClothesModel(0, "76", CLOTHES_UNDERSHIRT, 76, SEX_MALE, 50),
            new BusinessClothesModel(0, "77", CLOTHES_UNDERSHIRT, 77, SEX_MALE, 50),
            new BusinessClothesModel(0, "78", CLOTHES_UNDERSHIRT, 78, SEX_MALE, 50),
            new BusinessClothesModel(0, "79", CLOTHES_UNDERSHIRT, 79, SEX_MALE, 50),
            new BusinessClothesModel(0, "80", CLOTHES_UNDERSHIRT, 80, SEX_MALE, 50),
            new BusinessClothesModel(0, "81", CLOTHES_UNDERSHIRT, 81, SEX_MALE, 50),
            new BusinessClothesModel(0, "82", CLOTHES_UNDERSHIRT, 82, SEX_MALE, 50),
            new BusinessClothesModel(0, "83", CLOTHES_UNDERSHIRT, 83, SEX_MALE, 50),
            new BusinessClothesModel(0, "84", CLOTHES_UNDERSHIRT, 84, SEX_MALE, 50),
            new BusinessClothesModel(0, "85", CLOTHES_UNDERSHIRT, 85, SEX_MALE, 50),
            new BusinessClothesModel(0, "86", CLOTHES_UNDERSHIRT, 86, SEX_MALE, 50),
            new BusinessClothesModel(0, "87", CLOTHES_UNDERSHIRT, 87, SEX_MALE, 50),
            new BusinessClothesModel(0, "88", CLOTHES_UNDERSHIRT, 88, SEX_MALE, 50),
            new BusinessClothesModel(0, "89", CLOTHES_UNDERSHIRT, 89, SEX_MALE, 50),
            new BusinessClothesModel(0, "90", CLOTHES_UNDERSHIRT, 90, SEX_MALE, 50),
            new BusinessClothesModel(0, "91", CLOTHES_UNDERSHIRT, 91, SEX_MALE, 50),
            new BusinessClothesModel(0, "92", CLOTHES_UNDERSHIRT, 92, SEX_MALE, 50),
            new BusinessClothesModel(0, "93", CLOTHES_UNDERSHIRT, 93, SEX_MALE, 50),
            new BusinessClothesModel(0, "94", CLOTHES_UNDERSHIRT, 94, SEX_MALE, 50),
            new BusinessClothesModel(0, "95", CLOTHES_UNDERSHIRT, 95, SEX_MALE, 50),
            new BusinessClothesModel(0, "96", CLOTHES_UNDERSHIRT, 96, SEX_MALE, 50),
            new BusinessClothesModel(0, "97", CLOTHES_UNDERSHIRT, 97, SEX_MALE, 50),
            new BusinessClothesModel(0, "98", CLOTHES_UNDERSHIRT, 98, SEX_MALE, 50),
            new BusinessClothesModel(0, "99", CLOTHES_UNDERSHIRT, 99, SEX_MALE, 50),
            new BusinessClothesModel(0, "100", CLOTHES_UNDERSHIRT, 100, SEX_MALE, 50),
            new BusinessClothesModel(0, "101", CLOTHES_UNDERSHIRT, 101, SEX_MALE, 50),
            new BusinessClothesModel(0, "102", CLOTHES_UNDERSHIRT, 102, SEX_MALE, 50),
            new BusinessClothesModel(0, "103", CLOTHES_UNDERSHIRT, 103, SEX_MALE, 50),
            new BusinessClothesModel(0, "104", CLOTHES_UNDERSHIRT, 104, SEX_MALE, 50),
            new BusinessClothesModel(0, "105", CLOTHES_UNDERSHIRT, 105, SEX_MALE, 50),
            new BusinessClothesModel(0, "106", CLOTHES_UNDERSHIRT, 106, SEX_MALE, 50),
            new BusinessClothesModel(0, "107", CLOTHES_UNDERSHIRT, 107, SEX_MALE, 50),
            new BusinessClothesModel(0, "108", CLOTHES_UNDERSHIRT, 108, SEX_MALE, 50),
            new BusinessClothesModel(0, "109", CLOTHES_UNDERSHIRT, 109, SEX_MALE, 50),
            new BusinessClothesModel(0, "110", CLOTHES_UNDERSHIRT, 110, SEX_MALE, 50),
            new BusinessClothesModel(0, "111", CLOTHES_UNDERSHIRT, 111, SEX_MALE, 50),
            new BusinessClothesModel(0, "112", CLOTHES_UNDERSHIRT, 112, SEX_MALE, 50),
            new BusinessClothesModel(0, "113", CLOTHES_UNDERSHIRT, 113, SEX_MALE, 50),
            new BusinessClothesModel(0, "114", CLOTHES_UNDERSHIRT, 114, SEX_MALE, 50),
            new BusinessClothesModel(0, "115", CLOTHES_UNDERSHIRT, 115, SEX_MALE, 50),
            new BusinessClothesModel(0, "116", CLOTHES_UNDERSHIRT, 116, SEX_MALE, 50),
            new BusinessClothesModel(0, "117", CLOTHES_UNDERSHIRT, 117, SEX_MALE, 50),
            new BusinessClothesModel(0, "118", CLOTHES_UNDERSHIRT, 118, SEX_MALE, 50),
            new BusinessClothesModel(0, "119", CLOTHES_UNDERSHIRT, 119, SEX_MALE, 50),
            new BusinessClothesModel(0, "120", CLOTHES_UNDERSHIRT, 120, SEX_MALE, 50),
            new BusinessClothesModel(0, "121", CLOTHES_UNDERSHIRT, 121, SEX_MALE, 50),
            new BusinessClothesModel(0, "122", CLOTHES_UNDERSHIRT, 122, SEX_MALE, 50),
            new BusinessClothesModel(0, "123", CLOTHES_UNDERSHIRT, 123, SEX_MALE, 50),
            new BusinessClothesModel(0, "124", CLOTHES_UNDERSHIRT, 124, SEX_MALE, 50),
            new BusinessClothesModel(0, "125", CLOTHES_UNDERSHIRT, 125, SEX_MALE, 50),
            new BusinessClothesModel(0, "126", CLOTHES_UNDERSHIRT, 126, SEX_MALE, 50),
            new BusinessClothesModel(0, "127", CLOTHES_UNDERSHIRT, 127, SEX_MALE, 50),
            new BusinessClothesModel(0, "128", CLOTHES_UNDERSHIRT, 128, SEX_MALE, 50),
            new BusinessClothesModel(0, "129", CLOTHES_UNDERSHIRT, 129, SEX_MALE, 50),
            new BusinessClothesModel(0, "130", CLOTHES_UNDERSHIRT, 130, SEX_MALE, 50),
            new BusinessClothesModel(0, "131", CLOTHES_UNDERSHIRT, 131, SEX_MALE, 50),
            new BusinessClothesModel(0, "132", CLOTHES_UNDERSHIRT, 132, SEX_MALE, 50),
            new BusinessClothesModel(0, "133", CLOTHES_UNDERSHIRT, 133, SEX_MALE, 50),
            new BusinessClothesModel(0, "134", CLOTHES_UNDERSHIRT, 134, SEX_MALE, 50),
            new BusinessClothesModel(0, "135", CLOTHES_UNDERSHIRT, 135, SEX_MALE, 50),
            new BusinessClothesModel(0, "136", CLOTHES_UNDERSHIRT, 136, SEX_MALE, 50),
            new BusinessClothesModel(0, "137", CLOTHES_UNDERSHIRT, 137, SEX_MALE, 50),
            new BusinessClothesModel(0, "138", CLOTHES_UNDERSHIRT, 138, SEX_MALE, 50),
            new BusinessClothesModel(0, "139", CLOTHES_UNDERSHIRT, 139, SEX_MALE, 50),
            new BusinessClothesModel(0, "140", CLOTHES_UNDERSHIRT, 140, SEX_MALE, 50),
            new BusinessClothesModel(0, "141", CLOTHES_UNDERSHIRT, 141, SEX_MALE, 50),
            new BusinessClothesModel(0, "142", CLOTHES_UNDERSHIRT, 142, SEX_MALE, 50),
            new BusinessClothesModel(0, "143", CLOTHES_UNDERSHIRT, 143, SEX_MALE, 50),

            // Female hats
            new BusinessClothesModel(1, "Auriculares rojos", ACCESSORY_HATS, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cono blanco", ACCESSORY_HATS, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cowgirl cuadros negro y blanco", ACCESSORY_HATS, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Achatado cuadros negro y blanco", ACCESSORY_HATS, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra Los Santos", ACCESSORY_HATS, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra", ACCESSORY_HATS, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Achatado azul oscuro", ACCESSORY_HATS, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cuadros negros y blanco", ACCESSORY_HATS, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra y blanca Fruit", ACCESSORY_HATS, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra cuadros negro y blanco", ACCESSORY_HATS, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pamela cuadros negro y blanco", ACCESSORY_HATS, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Marron claro y marron oscuro", ACCESSORY_HATS, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pintor negro", ACCESSORY_HATS, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Auriculares blanco", ACCESSORY_HATS, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco amarillo rojo y negro", ACCESSORY_HATS, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco abierto azul y negro", ACCESSORY_HATS, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro", ACCESSORY_HATS, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Camel", ACCESSORY_HATS, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Achatado rosa con cinta", ACCESSORY_HATS, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pamela marron claro", ACCESSORY_HATS, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Papa noel", ACCESSORY_HATS, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Duende", ACCESSORY_HATS, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Reno", ACCESSORY_HATS, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Negro chaplin", ACCESSORY_HATS, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Negro copa", ACCESSORY_HATS, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Marron", ACCESSORY_HATS, 28, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Lila", ACCESSORY_HATS, 29, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Choose you primero", ACCESSORY_HATS, 30, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Choose you segundo", ACCESSORY_HATS, 31, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Choose you tercero", ACCESSORY_HATS, 32, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Capitan america", ACCESSORY_HATS, 33, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Reina patriota", ACCESSORY_HATS, 34, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_HATS, 35, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Corra con cerveza", ACCESSORY_HATS, 36, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco caballo", ACCESSORY_HATS, 38, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Arbol navidad", ACCESSORY_HATS, 39, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Postre", ACCESSORY_HATS, 40, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Navideño", ACCESSORY_HATS, 41, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Mama noel", ACCESSORY_HATS, 42, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra Naughty", ACCESSORY_HATS, 43, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra hacia atras roja", ACCESSORY_HATS, 44, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco visera negro", ACCESSORY_HATS, 47, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro opaco", ACCESSORY_HATS, 49, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro espejo", ACCESSORY_HATS, 50, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra verde simbolo", ACCESSORY_HATS, 53, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pamela marron clarito", ACCESSORY_HATS, 54, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra roja y azul", ACCESSORY_HATS, 55, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra Magretics", ACCESSORY_HATS, 56, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra marron clarito", ACCESSORY_HATS, 58, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco caballo verde", ACCESSORY_HATS, 59, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Lana verde", ACCESSORY_HATS, 60, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Negro y verde", ACCESSORY_HATS, 61, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco verde", ACCESSORY_HATS, 62, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 63, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra triangulo rojo y blanco", ACCESSORY_HATS, 64, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra hacia atras", ACCESSORY_HATS, 65, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco oscuro abierta visera", ACCESSORY_HATS, 66, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro abierta visera", ACCESSORY_HATS, 67, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro visera espejo abierta", ACCESSORY_HATS, 68, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco verde visera abierta", ACCESSORY_HATS, 71, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco verde abierto", ACCESSORY_HATS, 74, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra amarilla y azul", ACCESSORY_HATS, 75, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra hacia atras azul", ACCESSORY_HATS, 76, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Achatado negro", ACCESSORY_HATS, 82, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco guerra con pinchos", ACCESSORY_HATS, 83, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco guerra negro", ACCESSORY_HATS, 84, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco cresta", ACCESSORY_HATS, 86, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco trinchera", ACCESSORY_HATS, 88, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco trinchera plata", ACCESSORY_HATS, 89, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro rayas amarillas", ACCESSORY_HATS, 90, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco negro rayas amarillas visera abierta", ACCESSORY_HATS, 91, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Casco abierto blanco y azul", ACCESSORY_HATS, 92, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Achatado marron claro", ACCESSORY_HATS, 93, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Normal negro y camel", ACCESSORY_HATS, 94, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra Bigness", ACCESSORY_HATS, 95, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cuernos reno punta roja", ACCESSORY_HATS, 100, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Gorra negra", ACCESSORY_HATS, 101, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "19", ACCESSORY_HATS, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "45", ACCESSORY_HATS, 45, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "46", ACCESSORY_HATS, 46, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "48", ACCESSORY_HATS, 48, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "51", ACCESSORY_HATS, 51, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "52", ACCESSORY_HATS, 52, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "69", ACCESSORY_HATS, 69, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "70", ACCESSORY_HATS, 70, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "72", ACCESSORY_HATS, 72, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "73", ACCESSORY_HATS, 73, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "77", ACCESSORY_HATS, 77, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "78", ACCESSORY_HATS, 78, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "79", ACCESSORY_HATS, 79, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "80", ACCESSORY_HATS, 80, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "85", ACCESSORY_HATS, 85, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "87", ACCESSORY_HATS, 87, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "96", ACCESSORY_HATS, 96, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "97", ACCESSORY_HATS, 97, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "98", ACCESSORY_HATS, 98, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "99", ACCESSORY_HATS, 99, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "102", ACCESSORY_HATS, 102, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "103", ACCESSORY_HATS, 103, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "104", ACCESSORY_HATS, 104, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "105", ACCESSORY_HATS, 105, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "106", ACCESSORY_HATS, 106, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "107", ACCESSORY_HATS, 107, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "108", ACCESSORY_HATS, 108, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "109", ACCESSORY_HATS, 109, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "110", ACCESSORY_HATS, 110, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "112", ACCESSORY_HATS, 112, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "116", ACCESSORY_HATS, 116, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "120", ACCESSORY_HATS, 120, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "121", ACCESSORY_HATS, 121, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "124", ACCESSORY_HATS, 124, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "126", ACCESSORY_HATS, 126, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "127", ACCESSORY_HATS, 127, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "129", ACCESSORY_HATS, 129, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "130", ACCESSORY_HATS, 130, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "132", ACCESSORY_HATS, 132, SEX_FEMALE, 50),

            // Male hats
            new BusinessClothesModel(1, "Auriculares rojos", ACCESSORY_HATS, 0, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cono blanco", ACCESSORY_HATS, 1, SEX_MALE, 50),
            new BusinessClothesModel(1, "Lana negro", ACCESSORY_HATS, 2, SEX_MALE, 50),
            new BusinessClothesModel(1, "Michael cuadros negros y blanco", ACCESSORY_HATS, 3, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra LS", ACCESSORY_HATS, 4, SEX_MALE, 50),
            new BusinessClothesModel(1, "Lana negro achatado", ACCESSORY_HATS, 5, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 6, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorrilla blanca", ACCESSORY_HATS, 7, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra hacia atras cuadrados negros y blancos", ACCESSORY_HATS, 9, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra cuadrados negros y blancos", ACCESSORY_HATS, 10, SEX_MALE, 50),
            new BusinessClothesModel(1, "Grande negro con cinta", ACCESSORY_HATS, 12, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cowboy negro", ACCESSORY_HATS, 13, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pañuelo blanco motivos negros", ACCESSORY_HATS, 14, SEX_MALE, 50),
            new BusinessClothesModel(1, "Auriculares blancos", ACCESSORY_HATS, 15, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco amarillo negro y rojo", ACCESSORY_HATS, 16, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco abierto azul y negro", ACCESSORY_HATS, 17, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro", ACCESSORY_HATS, 18, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pesquero verde", ACCESSORY_HATS, 20, SEX_MALE, 50),
            new BusinessClothesModel(1, "Sombrerillo canera", ACCESSORY_HATS, 21, SEX_MALE, 50),
            new BusinessClothesModel(1, "Papa noel", ACCESSORY_HATS, 22, SEX_MALE, 50),
            new BusinessClothesModel(1, "Duende navidad", ACCESSORY_HATS, 23, SEX_MALE, 50),
            new BusinessClothesModel(1, "Reno", ACCESSORY_HATS, 24, SEX_MALE, 50),
            new BusinessClothesModel(1, "Negro oscuro con cinta", ACCESSORY_HATS, 25, SEX_MALE, 50),
            new BusinessClothesModel(1, "Chaplin negro", ACCESSORY_HATS, 26, SEX_MALE, 50),
            new BusinessClothesModel(1, "Copa negro", ACCESSORY_HATS, 27, SEX_MALE, 50),
            new BusinessClothesModel(1, "Lana azul", ACCESSORY_HATS, 28, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pequeño gris", ACCESSORY_HATS, 29, SEX_MALE, 50),
            new BusinessClothesModel(1, "Grande rojo cinta negra", ACCESSORY_HATS, 30, SEX_MALE, 50),
            new BusinessClothesModel(1, "Choose you primero", ACCESSORY_HATS, 31, SEX_MALE, 50),
            new BusinessClothesModel(1, "Choose you segundo", ACCESSORY_HATS, 32, SEX_MALE, 50),
            new BusinessClothesModel(1, "Choose you tercero", ACCESSORY_HATS, 33, SEX_MALE, 50),
            new BusinessClothesModel(1, "Lana capitan america", ACCESSORY_HATS, 34, SEX_MALE, 50),
            new BusinessClothesModel(1, "Reina america", ACCESSORY_HATS, 35, SEX_MALE, 50),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_HATS, 36, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra con cerveza", ACCESSORY_HATS, 37, SEX_MALE, 50),
            new BusinessClothesModel(1, "Negro caballo", ACCESSORY_HATS, 39, SEX_MALE, 50),
            new BusinessClothesModel(1, "Arbol navidad", ACCESSORY_HATS, 40, SEX_MALE, 50),
            new BusinessClothesModel(1, "Postre", ACCESSORY_HATS, 41, SEX_MALE, 50),
            new BusinessClothesModel(1, "Navideño", ACCESSORY_HATS, 42, SEX_MALE, 50),
            new BusinessClothesModel(1, "Papa noel a cuadros", ACCESSORY_HATS, 43, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra roja y blanca con letras", ACCESSORY_HATS, 44, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra hacia atras roja", ACCESSORY_HATS, 45, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco visera hacia delante negro", ACCESSORY_HATS, 48, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro opaco", ACCESSORY_HATS, 50, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro visera espejo", ACCESSORY_HATS, 51, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra verde letra", ACCESSORY_HATS, 54, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra roja y azul con letras", ACCESSORY_HATS, 55, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra con letras doradas", ACCESSORY_HATS, 56, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra maron", ACCESSORY_HATS, 58, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco caballo verde", ACCESSORY_HATS, 59, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra verde", ACCESSORY_HATS, 60, SEX_MALE, 50),
            new BusinessClothesModel(1, "Grance negro cinta verde", ACCESSORY_HATS, 61, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco verde", ACCESSORY_HATS, 62, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra verde lisa", ACCESSORY_HATS, 63, SEX_MALE, 50),
            new BusinessClothesModel(1, "Grande azul oscuro con rayas", ACCESSORY_HATS, 64, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra triangulo rojo y blanco", ACCESSORY_HATS, 65, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra hacia atras", ACCESSORY_HATS, 66, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro visera abierta", ACCESSORY_HATS, 67, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro visera opaca abierta", ACCESSORY_HATS, 68, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro visera espejo abierta", ACCESSORY_HATS, 69, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco abierto verde", ACCESSORY_HATS, 75, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra azul y amarilla con letras", ACCESSORY_HATS, 76, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra azul hacia atras", ACCESSORY_HATS, 77, SEX_MALE, 50),
            new BusinessClothesModel(1, "Lana negra", ACCESSORY_HATS, 83, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra pinchos", ACCESSORY_HATS, 84, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra", ACCESSORY_HATS, 85, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra visera", ACCESSORY_HATS, 86, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra cresta", ACCESSORY_HATS, 87, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra negro", ACCESSORY_HATS, 89, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco guerra plata", ACCESSORY_HATS, 90, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro rayas amarillas", ACCESSORY_HATS, 91, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco negro rayas amarillas visera abierta", ACCESSORY_HATS, 92, SEX_MALE, 50),
            new BusinessClothesModel(1, "Casco abierto blanco con diana", ACCESSORY_HATS, 93, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pescador camel", ACCESSORY_HATS, 94, SEX_MALE, 50),
            new BusinessClothesModel(1, "Michael negro cinta camel", ACCESSORY_HATS, 95, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra BIGNESS", ACCESSORY_HATS, 96, SEX_MALE, 50),
            new BusinessClothesModel(1, "Reno puntas rojas", ACCESSORY_HATS, 101, SEX_MALE, 50),
            new BusinessClothesModel(1, "Gorra negra basica", ACCESSORY_HATS, 102, SEX_MALE, 50),
            new BusinessClothesModel(1, "19", ACCESSORY_HATS, 19, SEX_MALE, 50),
            new BusinessClothesModel(1, "38", ACCESSORY_HATS, 38, SEX_MALE, 50),
            new BusinessClothesModel(1, "46", ACCESSORY_HATS, 46, SEX_MALE, 50),
            new BusinessClothesModel(1, "47", ACCESSORY_HATS, 47, SEX_MALE, 50),
            new BusinessClothesModel(1, "49", ACCESSORY_HATS, 49, SEX_MALE, 50),
            new BusinessClothesModel(1, "52", ACCESSORY_HATS, 52, SEX_MALE, 50),
            new BusinessClothesModel(1, "53", ACCESSORY_HATS, 53, SEX_MALE, 50),
            new BusinessClothesModel(1, "70", ACCESSORY_HATS, 70, SEX_MALE, 50),
            new BusinessClothesModel(1, "71", ACCESSORY_HATS, 71, SEX_MALE, 50),
            new BusinessClothesModel(1, "72", ACCESSORY_HATS, 72, SEX_MALE, 50),
            new BusinessClothesModel(1, "73", ACCESSORY_HATS, 73, SEX_MALE, 50),
            new BusinessClothesModel(1, "74", ACCESSORY_HATS, 74, SEX_MALE, 50),
            new BusinessClothesModel(1, "78", ACCESSORY_HATS, 78, SEX_MALE, 50),
            new BusinessClothesModel(1, "79", ACCESSORY_HATS, 79, SEX_MALE, 50),
            new BusinessClothesModel(1, "80", ACCESSORY_HATS, 80, SEX_MALE, 50),
            new BusinessClothesModel(1, "81", ACCESSORY_HATS, 81, SEX_MALE, 50),
            new BusinessClothesModel(1, "82", ACCESSORY_HATS, 82, SEX_MALE, 50),
            new BusinessClothesModel(1, "88", ACCESSORY_HATS, 88, SEX_MALE, 50),
            new BusinessClothesModel(1, "97", ACCESSORY_HATS, 97, SEX_MALE, 50),
            new BusinessClothesModel(1, "98", ACCESSORY_HATS, 98, SEX_MALE, 50),
            new BusinessClothesModel(1, "99", ACCESSORY_HATS, 99, SEX_MALE, 50),
            new BusinessClothesModel(1, "100", ACCESSORY_HATS, 100, SEX_MALE, 50),
            new BusinessClothesModel(1, "103", ACCESSORY_HATS, 103, SEX_MALE, 50),
            new BusinessClothesModel(1, "104", ACCESSORY_HATS, 104, SEX_MALE, 50),
            new BusinessClothesModel(1, "105", ACCESSORY_HATS, 105, SEX_MALE, 50),
            new BusinessClothesModel(1, "106", ACCESSORY_HATS, 106, SEX_MALE, 50),
            new BusinessClothesModel(1, "107", ACCESSORY_HATS, 107, SEX_MALE, 50),
            new BusinessClothesModel(1, "108", ACCESSORY_HATS, 108, SEX_MALE, 50),
            new BusinessClothesModel(1, "109", ACCESSORY_HATS, 109, SEX_MALE, 50),
            new BusinessClothesModel(1, "110", ACCESSORY_HATS, 110, SEX_MALE, 50),
            new BusinessClothesModel(1, "111", ACCESSORY_HATS, 111, SEX_MALE, 50),
            new BusinessClothesModel(1, "112", ACCESSORY_HATS, 112, SEX_MALE, 50),
            new BusinessClothesModel(1, "113", ACCESSORY_HATS, 113, SEX_MALE, 50),
            new BusinessClothesModel(1, "114", ACCESSORY_HATS, 114, SEX_MALE, 50),
            new BusinessClothesModel(1, "115", ACCESSORY_HATS, 115, SEX_MALE, 50),
            new BusinessClothesModel(1, "116", ACCESSORY_HATS, 116, SEX_MALE, 50),
            new BusinessClothesModel(1, "117", ACCESSORY_HATS, 117, SEX_MALE, 50),
            new BusinessClothesModel(1, "118", ACCESSORY_HATS, 118, SEX_MALE, 50),
            new BusinessClothesModel(1, "119", ACCESSORY_HATS, 119, SEX_MALE, 50),
            new BusinessClothesModel(1, "120", ACCESSORY_HATS, 120, SEX_MALE, 50),
            new BusinessClothesModel(1, "122", ACCESSORY_HATS, 122, SEX_MALE, 50),
            new BusinessClothesModel(1, "123", ACCESSORY_HATS, 123, SEX_MALE, 50),
            new BusinessClothesModel(1, "124", ACCESSORY_HATS, 124, SEX_MALE, 50),
            new BusinessClothesModel(1, "125", ACCESSORY_HATS, 125, SEX_MALE, 50),
            new BusinessClothesModel(1, "126", ACCESSORY_HATS, 126, SEX_MALE, 50),
            new BusinessClothesModel(1, "127", ACCESSORY_HATS, 127, SEX_MALE, 50),
            new BusinessClothesModel(1, "128", ACCESSORY_HATS, 128, SEX_MALE, 50),
            new BusinessClothesModel(1, "130", ACCESSORY_HATS, 130, SEX_MALE, 50),
            new BusinessClothesModel(1, "131", ACCESSORY_HATS, 131, SEX_MALE, 50),
            new BusinessClothesModel(1, "132", ACCESSORY_HATS, 132, SEX_MALE, 50),
            new BusinessClothesModel(1, "133", ACCESSORY_HATS, 133, SEX_MALE, 50),
            new BusinessClothesModel(1, "134", ACCESSORY_HATS, 134, SEX_MALE, 50),

            // Female glasses
            new BusinessClothesModel(1, "Deportiva cristal amarillo naranja", ACCESSORY_GLASSES, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redondas marron oscuro", ACCESSORY_GLASSES, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redonda con picos superiores marron oscuro", ACCESSORY_GLASSES, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Rectas cristal marron", ACCESSORY_GLASSES, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redondas pico superior leopardo", ACCESSORY_GLASSES, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redonda negra patillas plata", ACCESSORY_GLASSES, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Ovaladas cristal opaco marron", ACCESSORY_GLASSES, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pasta cristal marron transparente", ACCESSORY_GLASSES, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Deportiva cristal amarillo", ACCESSORY_GLASSES, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Deportiva cristal de amarillo a azul", ACCESSORY_GLASSES, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aviador plata cristal oscuro", ACCESSORY_GLASSES, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Vista pasta negra cristal transparente", ACCESSORY_GLASSES, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redonda marron oscuro patillas oro", ACCESSORY_GLASSES, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pasta negra cristal transparente", ACCESSORY_GLASSES, 17, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Finas oro cristal negro", ACCESSORY_GLASSES, 18, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Finas negro cristal negro", ACCESSORY_GLASSES, 19, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redondas negras cristal transparente oscuro", ACCESSORY_GLASSES, 20, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Redonda negra cristal transparente", ACCESSORY_GLASSES, 21, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_GLASSES, 22, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cuadradas estrellas azul y blanco", ACCESSORY_GLASSES, 23, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cuadradas negra cristal oscuro", ACCESSORY_GLASSES, 24, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Deportiva verde cristal oscuro", ACCESSORY_GLASSES, 25, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Piloto", ACCESSORY_GLASSES, 26, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Snow", ACCESSORY_GLASSES, 27, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "28", ACCESSORY_GLASSES, 28, SEX_FEMALE, 50),

            // Male glasses
            new BusinessClothesModel(1, "Finas cuadrados blancos y negros", ACCESSORY_GLASSES, 1, SEX_MALE, 50),
            new BusinessClothesModel(1, "Finas negra cristal oscuro", ACCESSORY_GLASSES, 2, SEX_MALE, 50),
            new BusinessClothesModel(1, "Finas negras cristal transparente oscuro", ACCESSORY_GLASSES, 3, SEX_MALE, 50),
            new BusinessClothesModel(1, "Finas negrasl cristal transparente", ACCESSORY_GLASSES, 4, SEX_MALE, 50),
            new BusinessClothesModel(1, "Aviador dorado", ACCESSORY_GLASSES, 5, SEX_MALE, 50),
            new BusinessClothesModel(1, "Aviador plata", ACCESSORY_GLASSES, 8, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pasta negra cristal negro", ACCESSORY_GLASSES, 9, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrada negra superior oro", ACCESSORY_GLASSES, 10, SEX_MALE, 50),
            new BusinessClothesModel(1, "Aviador oro cristal marron", ACCESSORY_GLASSES, 12, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pasta negra cristal rojo", ACCESSORY_GLASSES, 13, SEX_MALE, 50),
            new BusinessClothesModel(1, "Deportiva cristal amarillo", ACCESSORY_GLASSES, 15, SEX_MALE, 50),
            new BusinessClothesModel(1, "Deportiva negra cristal rojo", ACCESSORY_GLASSES, 16, SEX_MALE, 50),
            new BusinessClothesModel(1, "Fina blanca cristal gris", ACCESSORY_GLASSES, 17, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrada oro cristal oscuro", ACCESSORY_GLASSES, 18, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrada pasta negra superior oro", ACCESSORY_GLASSES, 19, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pasta negra cristal transparente", ACCESSORY_GLASSES, 20, SEX_MALE, 50),
            new BusinessClothesModel(1, "Estrellas EEUU", ACCESSORY_GLASSES, 21, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pasta azul y estrellas blancas", ACCESSORY_GLASSES, 22, SEX_MALE, 50),
            new BusinessClothesModel(1, "Deportiva verde cristal negro", ACCESSORY_GLASSES, 23, SEX_MALE, 50),
            new BusinessClothesModel(1, "Piloto", ACCESSORY_GLASSES, 24, SEX_MALE, 50),
            new BusinessClothesModel(1, "Snow", ACCESSORY_GLASSES, 25, SEX_MALE, 50),
            new BusinessClothesModel(1, "26", ACCESSORY_GLASSES, 26, SEX_MALE, 50),

            // Female earrings
            new BusinessClothesModel(1, "Pinganillo negro y blanco", ACCESSORY_EARS, 0, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pinganillo rojo y negro", ACCESSORY_EARS, 1, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pinganillo rectangular negro", ACCESSORY_EARS, 2, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pendiente largo plata", ACCESSORY_EARS, 3, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pendiente marron caro", ACCESSORY_EARS, 4, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pendiente plata", ACCESSORY_EARS, 5, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Largo rombo oro", ACCESSORY_EARS, 6, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Largo oro", ACCESSORY_EARS, 7, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Largo bolitas oro", ACCESSORY_EARS, 8, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Cortina oro", ACCESSORY_EARS, 9, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Largo bolita verde", ACCESSORY_EARS, 10, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Largo oro", ACCESSORY_EARS, 11, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Pequeñito", ACCESSORY_EARS, 12, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aro arma oro", ACCESSORY_EARS, 13, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aro ancho oro", ACCESSORY_EARS, 14, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aro fino oro", ACCESSORY_EARS, 15, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aro ancho oro letras", ACCESSORY_EARS, 16, SEX_FEMALE, 50),
            new BusinessClothesModel(1, "Aro ancho oro letras", ACCESSORY_EARS, 17, SEX_FEMALE, 50),

            // Male earrings
            new BusinessClothesModel(1, "Pinganillo negro y blanco", ACCESSORY_EARS, 0, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pinganillo rojo y negro", ACCESSORY_EARS, 1, SEX_MALE, 50),
            new BusinessClothesModel(1, "Aro pequeño oro", ACCESSORY_EARS, 4, SEX_MALE, 50),
            new BusinessClothesModel(1, "Circulo oro pequeño", ACCESSORY_EARS, 7, SEX_MALE, 50),
            new BusinessClothesModel(1, "Piramide oro", ACCESSORY_EARS, 10, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrado oro", ACCESSORY_EARS, 13, SEX_MALE, 50),
            new BusinessClothesModel(1, "Diamante", ACCESSORY_EARS, 16, SEX_MALE, 50),
            new BusinessClothesModel(1, "Espino", ACCESSORY_EARS, 22, SEX_MALE, 50),
            new BusinessClothesModel(1, "Calavera plata", ACCESSORY_EARS, 25, SEX_MALE, 50),
            new BusinessClothesModel(1, "Pincho metal", ACCESSORY_EARS, 28, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrado pequeño negro", ACCESSORY_EARS, 31, SEX_MALE, 50),
            new BusinessClothesModel(1, "Cuadrado grande plata", ACCESSORY_EARS, 35, SEX_MALE, 50)





        };

        // Tattoo list
        public static List<BusinessTattooModel> TATTOO_LIST = new List<BusinessTattooModel>
        {
            // Torso
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Refined Hustler", "mpbusiness_overlays", "MP_Buis_M_Stomach_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Rich", "mpbusiness_overlays", "MP_Buis_M_Chest_000", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "$$$", "mpbusiness_overlays", "MP_Buis_M_Chest_001", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Makin' Paper", "mpbusiness_overlays", "MP_Buis_M_Back_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "High Roller", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Makin' Money", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_001", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love Money", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Chest_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Diamond Back", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Santo Capra Logo", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Money Bag", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Stom_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Respect", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Back_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gold Digger", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Back_001", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Carp Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_005", "MP_Xmas2_F_Tat_005", 230),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Carp Shaded", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_006", "MP_Xmas2_F_Tat_006", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Time To Die", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_009", "MP_Xmas2_F_Tat_009", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Roaring Tiger", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_011", "MP_Xmas2_F_Tat_011", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lizard", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_013", "MP_Xmas2_F_Tat_013", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Japanese Warrior", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_015", "MP_Xmas2_F_Tat_015", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Loose Lips Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_016", "MP_Xmas2_F_Tat_016", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Loose Lips Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_017", "MP_Xmas2_F_Tat_017", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Dagger Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_018", "MP_Xmas2_F_Tat_018", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Dagger Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_019", "MP_Xmas2_F_Tat_019", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Executioner", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_028", "MP_Xmas2_F_Tat_028", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bullet Proof", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_000_M", "MP_Gunrunning_Tattoo_000_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crossed Weapons", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_001_M", "MP_Gunrunning_Tattoo_001_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Butterfly Knife", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_009_M", "MP_Gunrunning_Tattoo_009_F", 320),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cash Money", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_010_M", "MP_Gunrunning_Tattoo_010_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dollar Daggers", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_012_M", "MP_Gunrunning_Tattoo_012_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Wolf Insignia", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_013_M", "MP_Gunrunning_Tattoo_013_F", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Backstabber", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_014_M", "MP_Gunrunning_Tattoo_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dog Tags", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_017_M", "MP_Gunrunning_Tattoo_017_F", 120),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dual Wield Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_018_M", "MP_Gunrunning_Tattoo_018_F", 270),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pistol Wings", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_019_M", "MP_Gunrunning_Tattoo_019_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crowned Weapons", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_020_M", "MP_Gunrunning_Tattoo_020_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Explosive Heart", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_022_M", "MP_Gunrunning_Tattoo_022_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Micro SMG Chain", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_028_M", "MP_Gunrunning_Tattoo_028_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Win Some Lose Some", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_029_M", "MP_Gunrunning_Tattoo_029_F", 280),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Crossed Arrows", "mphipster_overlays", "FM_Hip_M_Tat_000", "FM_Hip_F_Tat_000", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Chemistry", "mphipster_overlays", "FM_Hip_M_Tat_002", "FM_Hip_F_Tat_002", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Birds", "mphipster_overlays", "FM_Hip_M_Tat_006", "FM_Hip_F_Tat_006", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Infinity", "mphipster_overlays", "FM_Hip_M_Tat_011", "FM_Hip_F_Tat_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Antlers", "mphipster_overlays", "FM_Hip_M_Tat_012", "FM_Hip_F_Tat_012", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Boombox", "mphipster_overlays", "FM_Hip_M_Tat_013", "FM_Hip_F_Tat_013", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pyramid", "mphipster_overlays", "FM_Hip_M_Tat_024", "FM_Hip_F_Tat_024", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Watch Your Step", "mphipster_overlays", "FM_Hip_M_Tat_025", "FM_Hip_F_Tat_025", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sad", "mphipster_overlays", "FM_Hip_M_Tat_029", "FM_Hip_F_Tat_029", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Shark Fin", "mphipster_overlays", "FM_Hip_M_Tat_030", "FM_Hip_F_Tat_030", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skateboard", "mphipster_overlays", "FM_Hip_M_Tat_031", "FM_Hip_F_Tat_031", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Paper Plane", "mphipster_overlays", "FM_Hip_M_Tat_032", "FM_Hip_F_Tat_032", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Stag", "mphipster_overlays", "FM_Hip_M_Tat_033", "FM_Hip_F_Tat_033", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sewn Heart", "mphipster_overlays", "FM_Hip_M_Tat_035", "FM_Hip_F_Tat_035", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tooth", "mphipster_overlays", "FM_Hip_M_Tat_041", "FM_Hip_F_Tat_041", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Triangles", "mphipster_overlays", "FM_Hip_M_Tat_046", "FM_Hip_F_Tat_046", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cassette", "mphipster_overlays", "FM_Hip_M_Tat_047", "FM_Hip_F_Tat_047", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Block Back", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_000_M", "MP_MP_ImportExport_Tat_000_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Power Plant", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_001_M", "MP_MP_ImportExport_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tuned to Death", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_002_M", "MP_MP_ImportExport_Tat_002_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Serpents of Destruction", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_009_M", "MP_MP_ImportExport_Tat_009_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Take the Wheel", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_010_M", "MP_MP_ImportExport_Tat_010_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Talk Shit Get Hit", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_011_M", "MP_MP_ImportExport_Tat_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "King Fight", "mplowrider_overlays", "MP_LR_Tat_001_M", "MP_LR_Tat_001_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Holy Mary", "mplowrider_overlays", "MP_LR_Tat_002_M", "MP_LR_Tat_002_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gun Mic", "mplowrider_overlays", "MP_LR_Tat_004_M", "MP_LR_Tat_004_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Amazon", "mplowrider_overlays", "MP_LR_Tat_009_M", "MP_LR_Tat_009_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bad Angel", "mplowrider_overlays", "MP_LR_Tat_010_M", "MP_LR_Tat_010_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love Gamble", "mplowrider_overlays", "MP_LR_Tat_013_M", "MP_LR_Tat_013_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love is Blind", "mplowrider_overlays", "MP_LR_Tat_014_M", "MP_LR_Tat_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sad Angel", "mplowrider_overlays", "MP_LR_Tat_021_M", "MP_LR_Tat_021_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Takeover", "mplowrider_overlays", "MP_LR_Tat_026_M", "MP_LR_Tat_026_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Turbulence", "mpairraces_overlays", "MP_Airraces_Tattoo_000_M", "MP_Airraces_Tattoo_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pilot Skull", "mpairraces_overlays", "MP_Airraces_Tattoo_001_M", "MP_Airraces_Tattoo_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Winged Bombshell", "mpairraces_overlays", "MP_Airraces_Tattoo_002_M", "MP_Airraces_Tattoo_002_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Balloon Pioneer", "mpairraces_overlays", "MP_Airraces_Tattoo_004_M", "MP_Airraces_Tattoo_004_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Parachute Belle", "mpairraces_overlays", "MP_Airraces_Tattoo_005_M", "MP_Airraces_Tattoo_005_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bombs Away", "mpairraces_overlays", "MP_Airraces_Tattoo_006_M", "MP_Airraces_Tattoo_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eagle Eyes", "mpairraces_overlays", "MP_Airraces_Tattoo_007_M", "MP_Airraces_Tattoo_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Demon Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_000_M", "MP_MP_Biker_Tat_000_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Both Barrels", "mpbiker_overlays", "MP_MP_Biker_Tat_001_M", "MP_MP_Biker_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Web Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_003_M", "MP_MP_Biker_Tat_003_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Made In America", "mpbiker_overlays", "MP_MP_Biker_Tat_005_M", "MP_MP_Biker_Tat_005_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Chopper Freedom", "mpbiker_overlays", "MP_MP_Biker_Tat_006_M", "MP_MP_Biker_Tat_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Freedom Wheels", "mpbiker_overlays", "MP_MP_Biker_Tat_008_M", "MP_MP_Biker_Tat_008_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull Of Taurus", "mpbiker_overlays", "MP_MP_Biker_Tat_010_M", "MP_MP_Biker_Tat_010_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "R.I.P. My Brothers", "mpbiker_overlays", "MP_MP_Biker_Tat_011_M", "MP_MP_Biker_Tat_011_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Demon Crossbones", "mpbiker_overlays", "MP_MP_Biker_Tat_013_M", "MP_MP_Biker_Tat_013_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clawed Beast", "mpbiker_overlays", "MP_MP_Biker_Tat_017_M", "MP_MP_Biker_Tat_017_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skeletal Chopper", "mpbiker_overlays", "MP_MP_Biker_Tat_018_M", "MP_MP_Biker_Tat_018_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gruesome Talons", "mpbiker_overlays", "MP_MP_Biker_Tat_019_M", "MP_MP_Biker_Tat_019_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Reaper", "mpbiker_overlays", "MP_MP_Biker_Tat_021_M", "MP_MP_Biker_Tat_021_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Western MC", "mpbiker_overlays", "MP_MP_Biker_Tat_023_M", "MP_MP_Biker_Tat_023_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "American Dream", "mpbiker_overlays", "MP_MP_Biker_Tat_026_M", "MP_MP_Biker_Tat_026_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bone Wrench", "mpbiker_overlays", "MP_MP_Biker_Tat_029_M", "MP_MP_Biker_Tat_029_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brothers For Life", "mpbiker_overlays", "MP_MP_Biker_Tat_030_M", "MP_MP_Biker_Tat_030_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gear Head", "mpbiker_overlays", "MP_MP_Biker_Tat_031_M", "MP_MP_Biker_Tat_031_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Western Eagle", "mpbiker_overlays", "MP_MP_Biker_Tat_032_M", "MP_MP_Biker_Tat_032_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brotherhood of Bikes", "mpbiker_overlays", "MP_MP_Biker_Tat_034_M", "MP_MP_Biker_Tat_034_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Gas Guzzler", "mpbiker_overlays", "MP_MP_Biker_Tat_039_M", "MP_MP_Biker_Tat_039_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "No Regrets", "mpbiker_overlays", "MP_MP_Biker_Tat_041_M", "MP_MP_Biker_Tat_041_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ride Forever", "mpbiker_overlays", "MP_MP_Biker_Tat_043_M", "MP_MP_Biker_Tat_043_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Unforgiven", "mpbiker_overlays", "MP_MP_Biker_Tat_050_M", "MP_MP_Biker_Tat_050_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Biker Mount", "mpbiker_overlays", "MP_MP_Biker_Tat_052_M", "MP_MP_Biker_Tat_052_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reaper Vulture", "mpbiker_overlays", "MP_MP_Biker_Tat_058_M", "MP_MP_Biker_Tat_058_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Faggio", "mpbiker_overlays", "MP_MP_Biker_Tat_059_M", "MP_MP_Biker_Tat_059_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "We Are The Mods!", "mpbiker_overlays", "MP_MP_Biker_Tat_060_M", "MP_MP_Biker_Tat_060_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "SA Assault", "mplowrider2_overlays", "MP_LR_Tat_000_M", "MP_LR_Tat_000_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Love the Game", "mplowrider2_overlays", "MP_LR_Tat_008_M", "MP_LR_Tat_008_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lady Liberty", "mplowrider2_overlays", "MP_LR_Tat_011_M", "MP_LR_Tat_011_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Royal Kiss", "mplowrider2_overlays", "MP_LR_Tat_012_M", "MP_LR_Tat_012_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Two Face", "mplowrider2_overlays", "MP_LR_Tat_016_M", "MP_LR_Tat_016_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Death Behind", "mplowrider2_overlays", "MP_LR_Tat_019_M", "MP_LR_Tat_019_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Pretty", "mplowrider2_overlays", "MP_LR_Tat_031_M", "MP_LR_Tat_031_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reign Over", "mplowrider2_overlays", "MP_LR_Tat_032_M", "MP_LR_Tat_032_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Abstract Skull", "mpluxe_overlays", "MP_LUXE_TAT_003_M", "MP_LUXE_TAT_003_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eye of the Griffin", "mpluxe_overlays", "MP_LUXE_TAT_007_M", "MP_LUXE_TAT_007_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flying Eye", "mpluxe_overlays", "MP_LUXE_TAT_008_M", "MP_LUXE_TAT_008_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ancient Queen", "mpluxe_overlays", "MP_LUXE_TAT_014_M", "MP_LUXE_TAT_014_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Smoking Sisters", "mpluxe_overlays", "MP_LUXE_TAT_015_M", "MP_LUXE_TAT_015_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Mural", "mpluxe_overlays", "MP_LUXE_TAT_024_M", "MP_LUXE_TAT_024_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "The Howler", "mpluxe2_overlays", "MP_LUXE_TAT_002_M", "MP_LUXE_TAT_002_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Geometric Galaxy", "mpluxe2_overlays", "MP_LUXE_TAT_012_M", "MP_LUXE_TAT_012_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cloaked Angel", "mpluxe2_overlays", "MP_LUXE_TAT_022_M", "MP_LUXE_TAT_022_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Reaper Sway", "mpluxe2_overlays", "MP_LUXE_TAT_025_M", "MP_LUXE_TAT_025_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Cobra Dawn", "mpluxe2_overlays", "MP_LUXE_TAT_027_M", "MP_LUXE_TAT_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_029_M", "MP_LUXE_TAT_029_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bless The Dead", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_000_M", "MP_Smuggler_Tattoo_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Lies", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_002_M", "MP_Smuggler_Tattoo_002_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Give Nothing Back", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_003_M", "MP_Smuggler_Tattoo_003_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Never Surrender", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_006_M", "MP_Smuggler_Tattoo_006_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "No Honor", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_007_M", "MP_Smuggler_Tattoo_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Tall Ship Conflict", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_009_M", "MP_Smuggler_Tattoo_009_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "See You In Hell", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_010_M", "MP_Smuggler_Tattoo_010_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Torn Wings", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_013_M", "MP_Smuggler_Tattoo_013_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Jolly Roger", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_015_M", "MP_Smuggler_Tattoo_015_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull Compass", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_016_M", "MP_Smuggler_Tattoo_016_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Framed Tall Ship", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_017_M", "MP_Smuggler_Tattoo_017_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Finders Keepers", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_018_M", "MP_Smuggler_Tattoo_018_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lost At Sea", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_019_M", "MP_Smuggler_Tattoo_019_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dead Tales", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_021_M", "MP_Smuggler_Tattoo_021_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "X Marks The Spot", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_022_M", "MP_Smuggler_Tattoo_022_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Pirate Captain", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_024_M", "MP_Smuggler_Tattoo_024_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Claimed By The Beast", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_025_M", "MP_Smuggler_Tattoo_025_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Wheels of Death", "mpstunt_overlays", "MP_MP_Stunt_Tat_011_M", "MP_MP_Stunt_Tat_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Punk Biker", "mpstunt_overlays", "MP_MP_Stunt_Tat_012_M", "MP_MP_Stunt_Tat_012_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Bat Cat of Spades", "mpstunt_overlays", "MP_MP_Stunt_Tat_014_M", "MP_MP_Stunt_Tat_014_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Vintage Bully", "mpstunt_overlays", "MP_MP_Stunt_Tat_018_M", "MP_MP_Stunt_Tat_018_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Engine Heart", "mpstunt_overlays", "MP_MP_Stunt_Tat_019_M", "MP_MP_Stunt_Tat_019_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Road Kill", "mpstunt_overlays", "MP_MP_Stunt_Tat_024_M", "MP_MP_Stunt_Tat_024_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Winged Wheel", "mpstunt_overlays", "MP_MP_Stunt_Tat_026_M", "MP_MP_Stunt_Tat_026_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Punk Road Hog", "mpstunt_overlays", "MP_MP_Stunt_Tat_027_M", "MP_MP_Stunt_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Majestic Finish", "mpstunt_overlays", "MP_MP_Stunt_Tat_029_M", "MP_MP_Stunt_Tat_029_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Man's Ruin", "mpstunt_overlays", "MP_MP_Stunt_Tat_030_M", "MP_MP_Stunt_Tat_030_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Sugar Skull Trucker", "mpstunt_overlays", "MP_MP_Stunt_Tat_033_M", "MP_MP_Stunt_Tat_033_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Feather Road Kill", "mpstunt_overlays", "MP_MP_Stunt_Tat_034_M", "MP_MP_Stunt_Tat_034_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Big Grills", "mpstunt_overlays", "MP_MP_Stunt_Tat_037_M", "MP_MP_Stunt_Tat_037_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Monkey Chopper", "mpstunt_overlays", "MP_MP_Stunt_Tat_040_M", "MP_MP_Stunt_Tat_040_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Brapp", "mpstunt_overlays", "MP_MP_Stunt_Tat_041_M", "MP_MP_Stunt_Tat_041_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Ram Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_044_M", "MP_MP_Stunt_Tat_044_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Full Throttle", "mpstunt_overlays", "MP_MP_Stunt_Tat_046_M", "MP_MP_Stunt_Tat_046_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Racing Doll", "mpstunt_overlays", "MP_MP_Stunt_Tat_048_M", "MP_MP_Stunt_Tat_048_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Blackjack", "multiplayer_overlays", "FM_Tat_Award_M_003", "FM_Tat_Award_F_003", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Hustler", "multiplayer_overlays", "FM_Tat_Award_M_004", "FM_Tat_Award_F_004", 300),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Angel", "multiplayer_overlays", "FM_Tat_Award_M_005", "FM_Tat_Award_F_005", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Los Santos Customs", "multiplayer_overlays", "FM_Tat_Award_M_008", "FM_Tat_Award_F_008", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Blank Scroll", "multiplayer_overlays", "FM_Tat_Award_M_011", "FM_Tat_Award_F_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Embellished Scroll", "multiplayer_overlays", "FM_Tat_Award_M_012", "FM_Tat_Award_F_012", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Seven Deadly Sins", "multiplayer_overlays", "FM_Tat_Award_M_013", "FM_Tat_Award_F_013", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Trust No One", "multiplayer_overlays", "FM_Tat_Award_M_014", "FM_Tat_Award_F_014", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown", "multiplayer_overlays", "FM_Tat_Award_M_016", "FM_Tat_Award_F_016", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown and Gun", "multiplayer_overlays", "FM_Tat_Award_M_017", "FM_Tat_Award_F_017", 220),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown Dual Wield", "multiplayer_overlays", "FM_Tat_Award_M_018", "FM_Tat_Award_F_018", 240),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Clown Dual Wield Dollars", "multiplayer_overlays", "FM_Tat_Award_M_019", "FM_Tat_Award_F_019", 260),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Faith", "multiplayer_overlays", "FM_Tat_M_004", "FM_Tat_F_004", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skull on the Cross", "multiplayer_overlays", "FM_Tat_M_009", "FM_Tat_F_009", 400),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Flames", "multiplayer_overlays", "FM_Tat_M_010", "FM_Tat_F_010", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Script", "multiplayer_overlays", "FM_Tat_M_011", "FM_Tat_F_011", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Los Santos Bills", "multiplayer_overlays", "FM_Tat_M_012", "FM_Tat_F_012", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Eagle and Serpent", "multiplayer_overlays", "FM_Tat_M_013", "FM_Tat_F_013", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Evil Clown", "multiplayer_overlays", "FM_Tat_M_016", "FM_Tat_F_016", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "The Wages of Sin", "multiplayer_overlays", "FM_Tat_M_019", "FM_Tat_F_019", 450),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Dragon", "multiplayer_overlays", "FM_Tat_M_020", "FM_Tat_F_020", 420),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Cross", "multiplayer_overlays", "FM_Tat_M_024", "FM_Tat_F_024", 350),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "LS Bold", "multiplayer_overlays", "FM_Tat_M_025", "FM_Tat_F_025", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Trinity Knot", "multiplayer_overlays", "FM_Tat_M_029", "FM_Tat_F_029", 100),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Lucky Celtic Dogs", "multiplayer_overlays", "FM_Tat_M_030", "FM_Tat_F_030", 200),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Flaming Shamrock", "multiplayer_overlays", "FM_Tat_M_034", "FM_Tat_F_034", 150),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Way of the Gun", "multiplayer_overlays", "FM_Tat_M_036", "FM_Tat_F_036", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Stone Cross", "multiplayer_overlays", "FM_Tat_M_044", "FM_Tat_F_044", 250),
            new BusinessTattooModel(TATTOO_ZONE_TORSO, "Skulls and Rose", "multiplayer_overlays", "FM_Tat_M_045", "FM_Tat_F_045", 400),
            
            // Head
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Cash is King", "mpbusiness_overlays", "MP_Buis_M_Neck_000", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Bold Dollar Sign", "mpbusiness_overlays", "MP_Buis_M_Neck_001", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Script Dollar Sign", "mpbusiness_overlays", "MP_Buis_M_Neck_002", string.Empty, 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "$100", "mpbusiness_overlays", "MP_Buis_M_Neck_003", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Val-de-Grace Logo", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Neck_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Money Rose", "mpbusiness_overlays", string.Empty, "MP_Buis_F_Neck_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Los Muertos", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_007", "MP_Xmas2_F_Tat_007", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Snake Head Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_024", "MP_Xmas2_F_Tat_024", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Snake Head Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_025", "MP_Xmas2_F_Tat_025", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Beautiful Death", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_029", "MP_Xmas2_F_Tat_029", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Lock & Load", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_003_M", "MP_Gunrunning_Tattoo_003_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Beautiful Eye", "mphipster_overlays", "FM_Hip_M_Tat_005", "FM_Hip_F_Tat_005", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Geo Fox", "mphipster_overlays", "FM_Hip_M_Tat_021", "FM_Hip_F_Tat_021", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Morbid Arachnid", "mpbiker_overlays", "MP_MP_Biker_Tat_009_M", "MP_MP_Biker_Tat_009_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "FTW", "mpbiker_overlays", "MP_MP_Biker_Tat_038_M", "MP_MP_Biker_Tat_038_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Western Stylized", "mpbiker_overlays", "MP_MP_Biker_Tat_051_M", "MP_MP_Biker_Tat_051_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Sinner", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_011_M", "MP_Smuggler_Tattoo_011_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Thief", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_012_M", "MP_Smuggler_Tattoo_012_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Stunt Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_000_M", "MP_MP_Stunt_Tat_000_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Scorpion", "mpstunt_overlays", "MP_MP_Stunt_Tat_004_M", "MP_MP_Stunt_Tat_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Toxic Spider", "mpstunt_overlays", "MP_MP_Stunt_Tat_006_M", "MP_MP_Stunt_Tat_006_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Bat Wheel", "mpstunt_overlays", "MP_MP_Stunt_Tat_017_M", "MP_MP_Stunt_Tat_017_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Flaming Quad", "mpstunt_overlays", "MP_MP_Stunt_Tat_042_M", "MP_MP_Stunt_Tat_042_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_HEAD, "Skull", "multiplayer_overlays", "FM_Tat_Award_M_000", "FM_Tat_Award_F_000", 100),

            // Left arm
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "$100 Bill", "mpbusiness_overlays", "MP_Buis_M_LeftArm_000", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "All-Seeing Eye", "mpbusiness_overlays", "MP_Buis_M_LeftArm_001", string.Empty, 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Greed is Good", "mpbusiness_overlays", string.Empty, "MP_Buis_F_LArm_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Skull Rider", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_000", "MP_Xmas2_F_Tat_000", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Electric Snake", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_010", "MP_Xmas2_F_Tat_010", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "8 Ball Skull", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_012", "MP_Xmas2_F_Tat_012", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Time's Up Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_020", "MP_Xmas2_F_Tat_020", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Time's Up Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_021", "MP_Xmas2_F_Tat_021", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sidearm", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_004_M", "MP_Gunrunning_Tattoo_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Bandolier", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_008_M", "MP_Gunrunning_Tattoo_008_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Spiked Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_015_M", "MP_Gunrunning_Tattoo_015_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Blood Money", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_016_M", "MP_Gunrunning_Tattoo_016_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Praying Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_025_M", "MP_Gunrunning_Tattoo_025_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Serpent Revolver", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_027_M", "MP_Gunrunning_Tattoo_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Diamond Sparkle", "mphipster_overlays", "FM_Hip_M_Tat_003", "FM_Hip_F_Tat_003", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Bricks", "mphipster_overlays", "FM_Hip_M_Tat_007", "FM_Hip_F_Tat_007", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Mustache", "mphipster_overlays", "FM_Hip_M_Tat_015", "FM_Hip_F_Tat_015", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Lightning Bolt", "mphipster_overlays", "FM_Hip_M_Tat_016", "FM_Hip_F_Tat_016", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Pizza", "mphipster_overlays", "FM_Hip_M_Tat_026", "FM_Hip_F_Tat_026", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Padlock", "mphipster_overlays", "FM_Hip_M_Tat_027", "FM_Hip_F_Tat_027", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Thorny Rose", "mphipster_overlays", "FM_Hip_M_Tat_028", "FM_Hip_F_Tat_028", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Stop", "mphipster_overlays", "FM_Hip_M_Tat_034", "FM_Hip_F_Tat_034", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sunrise", "mphipster_overlays", "FM_Hip_M_Tat_037", "FM_Hip_F_Tat_037", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Sleeve", "mphipster_overlays", "FM_Hip_M_Tat_039", "FM_Hip_F_Tat_039", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Triangle White", "mphipster_overlays", "FM_Hip_M_Tat_043", "FM_Hip_F_Tat_043", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Peace", "mphipster_overlays", "FM_Hip_M_Tat_048", "FM_Hip_F_Tat_048", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Piston Sleeve", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_004_M", "MP_MP_ImportExport_Tat_004_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Scarlett", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_008_M", "MP_MP_ImportExport_Tat_008_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "No Evil", "mplowrider_overlays", "MP_LR_Tat_005_M", "MP_LR_Tat_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Los Santos Life", "mplowrider_overlays", "MP_LR_Tat_027_M", "MP_LR_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "City Sorrow", "mplowrider_overlays", "MP_LR_Tat_033_M", "MP_LR_Tat_033_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Toxic Trails", "mpairraces_overlays", "MP_Airraces_Tattoo_003_M", "MP_Airraces_Tattoo_003_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Urban Stunter", "mpbiker_overlays", "MP_MP_Biker_Tat_012_M", "MP_MP_Biker_Tat_012_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Macabre Tree", "mpbiker_overlays", "MP_MP_Biker_Tat_016_M", "MP_MP_Biker_Tat_016_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Cranial Rose", "mpbiker_overlays", "MP_MP_Biker_Tat_020_M", "MP_MP_Biker_Tat_020_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Live to Ride", "mpbiker_overlays", "MP_MP_Biker_Tat_024_M", "MP_MP_Biker_Tat_024_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Good Luck", "mpbiker_overlays", "MP_MP_Biker_Tat_025_M", "MP_MP_Biker_Tat_025_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Chain Fist", "mpbiker_overlays", "MP_MP_Biker_Tat_035_M", "MP_MP_Biker_Tat_035_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Ride Hard Die Fast", "mpbiker_overlays", "MP_MP_Biker_Tat_045_M", "MP_MP_Biker_Tat_045_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Muffler Helmet", "mpbiker_overlays", "MP_MP_Biker_Tat_053_M", "MP_MP_Biker_Tat_053_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Poison Scorpion", "mpbiker_overlays", "MP_MP_Biker_Tat_055_M", "MP_MP_Biker_Tat_055_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Love Hustle", "mplowrider2_overlays", "MP_LR_Tat_006_M", "MP_LR_Tat_006_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Skeleton Party", "mplowrider2_overlays", "MP_LR_Tat_018_M", "MP_LR_Tat_018_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "My Crazy Life", "mplowrider2_overlays", "MP_LR_Tat_022_M", "MP_LR_Tat_022_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Archangel & Mary", "mpluxe_overlays", "MP_LUXE_TAT_020_M", "MP_LUXE_TAT_020_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Gabriel", "mpluxe_overlays", "MP_LUXE_TAT_021_M", "MP_LUXE_TAT_021_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Fatal Dagger", "mpluxe2_overlays", "MP_LUXE_TAT_005_M", "MP_LUXE_TAT_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Egyptian Mural", "mpluxe2_overlays", "MP_LUXE_TAT_016_M", "MP_LUXE_TAT_016_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Divine Goddess", "mpluxe2_overlays", "MP_LUXE_TAT_018_M", "MP_LUXE_TAT_018_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Python Skull", "mpluxe2_overlays", "MP_LUXE_TAT_028_M", "MP_LUXE_TAT_028_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_031_M", "MP_LUXE_TAT_031_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Honor", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_004_M", "MP_Smuggler_Tattoo_004_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Horrors Of The Deep", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_008_M", "MP_Smuggler_Tattoo_008_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Mermaid's Curse", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_014_M", "MP_Smuggler_Tattoo_014_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "8 Eyed Skull", "mpstunt_overlays", "MP_MP_Stunt_Tat_001_M", "MP_MP_Stunt_Tat_001_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Big Cat", "mpstunt_overlays", "MP_MP_Stunt_Tat_002_M", "MP_MP_Stunt_Tat_002_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Moonlight Ride", "mpstunt_overlays", "MP_MP_Stunt_Tat_008_M", "MP_MP_Stunt_Tat_008_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Piston Head", "mpstunt_overlays", "MP_MP_Stunt_Tat_022_M", "MP_MP_Stunt_Tat_022_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Tanked", "mpstunt_overlays", "MP_MP_Stunt_Tat_023_M", "MP_MP_Stunt_Tat_023_F", 450),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Stuntman's End", "mpstunt_overlays", "MP_MP_Stunt_Tat_035_M", "MP_MP_Stunt_Tat_035_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Kaboom", "mpstunt_overlays", "MP_MP_Stunt_Tat_039_M", "MP_MP_Stunt_Tat_039_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Engine Arm", "mpstunt_overlays", "MP_MP_Stunt_Tat_043_M", "MP_MP_Stunt_Tat_043_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Burning Heart", "multiplayer_overlays", "FM_Tat_Award_M_001", "FM_Tat_Award_F_001", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Racing Blonde", "multiplayer_overlays", "FM_Tat_Award_M_007", "FM_Tat_Award_F_007", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Racing Brunette", "multiplayer_overlays", "FM_Tat_Award_M_015", "FM_Tat_Award_F_015", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Serpents", "multiplayer_overlays", "FM_Tat_M_005", "FM_Tat_F_005", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Oriental Mural", "multiplayer_overlays", "FM_Tat_M_006", "FM_Tat_F_006", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Zodiac Skull", "multiplayer_overlays", "FM_Tat_M_015", "FM_Tat_F_015", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Lady M", "multiplayer_overlays", "FM_Tat_M_031", "FM_Tat_F_031", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_ARM, "Dope Skull", "multiplayer_overlays", "FM_Tat_M_041", "FM_Tat_F_041", 100),

            // Right arm
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dollar Skull", "mpbusiness_overlays", "MP_Buis_M_RightArm_000", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Green", "mpbusiness_overlays", "MP_Buis_M_RightArm_001", string.Empty, 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dollar Sign", "mpbusiness_overlays", string.Empty, "MP_Buis_F_RArm_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_003", "MP_Xmas2_F_Tat_003", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Shaded", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_004", "MP_Xmas2_F_Tat_004", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Death Before Dishonor", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_008", "MP_Xmas2_F_Tat_008", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "You're Next Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_022", "MP_Xmas2_F_Tat_022", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "You're Next Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_023", "MP_Xmas2_F_Tat_023", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Fuck Luck Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_026", "MP_Xmas2_F_Tat_026", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Fuck Luck Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_027", "MP_Xmas2_F_Tat_027", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grenade", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_002_M", "MP_Gunrunning_Tattoo_002_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Have a Nice Day", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_021_M", "MP_Gunrunning_Tattoo_021_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Combat Reaper", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_024_M", "MP_Gunrunning_Tattoo_024_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Single Arrow", "mphipster_overlays", "FM_Hip_M_Tat_001", "FM_Hip_F_Tat_001", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Bone", "mphipster_overlays", "FM_Hip_M_Tat_004", "FM_Hip_F_Tat_004", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Cube", "mphipster_overlays", "FM_Hip_M_Tat_008", "FM_Hip_F_Tat_008", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Horseshoe", "mphipster_overlays", "FM_Hip_M_Tat_010", "FM_Hip_F_Tat_010", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Spray Can", "mphipster_overlays", "FM_Hip_M_Tat_014", "FM_Hip_F_Tat_014", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Eye Triangle", "mphipster_overlays", "FM_Hip_M_Tat_017", "FM_Hip_F_Tat_017", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Origami", "mphipster_overlays", "FM_Hip_M_Tat_018", "FM_Hip_F_Tat_018", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geo Pattern", "mphipster_overlays", "FM_Hip_M_Tat_020", "FM_Hip_F_Tat_020", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Pencil", "mphipster_overlays", "FM_Hip_M_Tat_022", "FM_Hip_F_Tat_022", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Smiley", "mphipster_overlays", "FM_Hip_M_Tat_023", "FM_Hip_F_Tat_023", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Shapes", "mphipster_overlays", "FM_Hip_M_Tat_036", "FM_Hip_F_Tat_036", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Triangle Black", "mphipster_overlays", "FM_Hip_M_Tat_044", "FM_Hip_F_Tat_044", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mesh Band", "mphipster_overlays", "FM_Hip_M_Tat_045", "FM_Hip_F_Tat_045", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mechanical Sleeve", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_003_M", "MP_MP_ImportExport_Tat_003_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dialed In", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_005_M", "MP_MP_ImportExport_Tat_005_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Engulfed Block", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_006_M", "MP_MP_ImportExport_Tat_006_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Drive Forever", "mpimportexport_overlays", "MP_MP_ImportExport_Tat_007_M", "MP_MP_ImportExport_Tat_007_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Seductress", "mplowrider_overlays", "MP_LR_Tat_015_M", "MP_LR_Tat_015_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Swooping Eagle", "mpbiker_overlays", "MP_MP_Biker_Tat_007_M", "MP_MP_Biker_Tat_007_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lady Mortality", "mpbiker_overlays", "MP_MP_Biker_Tat_014_M", "MP_MP_Biker_Tat_014_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Eagle Emblem", "mpbiker_overlays", "MP_MP_Biker_Tat_033_M", "MP_MP_Biker_Tat_033_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grim Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_042_M", "MP_MP_Biker_Tat_042_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Skull Chain", "mpbiker_overlays", "MP_MP_Biker_Tat_046_M", "MP_MP_Biker_Tat_046_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Snake Bike", "mpbiker_overlays", "MP_MP_Biker_Tat_047_M", "MP_MP_Biker_Tat_047_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "These Colors Don't Run", "mpbiker_overlays", "MP_MP_Biker_Tat_049_M", "MP_MP_Biker_Tat_049_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mum", "mpbiker_overlays", "MP_MP_Biker_Tat_054_M", "MP_MP_Biker_Tat_054_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lady Vamp", "mplowrider2_overlays", "MP_LR_Tat_003_M", "MP_LR_Tat_003_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Loving Los Muertos", "mplowrider2_overlays", "MP_LR_Tat_028_M", "MP_LR_Tat_028_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Black Tears", "mplowrider2_overlays", "MP_LR_Tat_035_M", "MP_LR_Tat_035_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Floral Raven", "mpluxe_overlays", "MP_LUXE_TAT_004_M", "MP_LUXE_TAT_004_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mermaid Harpist", "mpluxe_overlays", "MP_LUXE_TAT_013_M", "MP_LUXE_TAT_013_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geisha Bloom", "mpluxe_overlays", "MP_LUXE_TAT_019_M", "MP_LUXE_TAT_019_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Intrometric", "mpluxe2_overlays", "MP_LUXE_TAT_010_M", "MP_LUXE_TAT_010_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Heavenly Deity", "mpluxe2_overlays", "MP_LUXE_TAT_017_M", "MP_LUXE_TAT_017_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Floral Print", "mpluxe2_overlays", "MP_LUXE_TAT_026_M", "MP_LUXE_TAT_026_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Geometric Design", "mpluxe2_overlays", "MP_LUXE_TAT_030_M", "MP_LUXE_TAT_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Crackshot", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_001_M", "MP_Smuggler_Tattoo_001_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mutiny", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_005_M", "MP_Smuggler_Tattoo_005_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Stylized Kraken", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_023_M", "MP_Smuggler_Tattoo_023_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Poison Wrench", "mpstunt_overlays", "MP_MP_Stunt_Tat_003_M", "MP_MP_Stunt_Tat_003_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Arachnid of Death", "mpstunt_overlays", "MP_MP_Stunt_Tat_009_M", "MP_MP_Stunt_Tat_009_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grave Vulture", "mpstunt_overlays", "MP_MP_Stunt_Tat_010_M", "MP_MP_Stunt_Tat_010_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Coffin Racer", "mpstunt_overlays", "MP_MP_Stunt_Tat_016_M", "MP_MP_Stunt_Tat_016_F", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Biker Stallion", "mpstunt_overlays", "MP_MP_Stunt_Tat_036_M", "MP_MP_Stunt_Tat_036_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "One Down Five Up", "mpstunt_overlays", "MP_MP_Stunt_Tat_038_M", "MP_MP_Stunt_Tat_038_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Seductive Mechanic", "mpstunt_overlays", "MP_MP_Stunt_Tat_049_M", "MP_MP_Stunt_Tat_049_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Grim Reaper Smoking Gun", "multiplayer_overlays", "FM_Tat_Award_M_002", "FM_Tat_Award_F_002", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Ride or Die", "multiplayer_overlays", "FM_Tat_Award_M_010", "FM_Tat_Award_F_010", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Brotherhood", "multiplayer_overlays", "FM_Tat_M_000", "FM_Tat_F_000", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dragons", "multiplayer_overlays", "FM_Tat_M_001", "FM_Tat_F_001", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dragons and Skull", "multiplayer_overlays", "FM_Tat_M_003", "FM_Tat_F_003", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Flower Mural", "multiplayer_overlays", "FM_Tat_M_014", "FM_Tat_F_014", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Serpent Skull", "multiplayer_overlays", "FM_Tat_M_018", "FM_Tat_F_018", 350),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Virgin Mary", "multiplayer_overlays", "FM_Tat_M_027", "FM_Tat_F_027", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Mermaid", "multiplayer_overlays", "FM_Tat_M_028", "FM_Tat_F_028", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Dagger", "multiplayer_overlays", "FM_Tat_M_038", "FM_Tat_F_038", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_ARM, "Lion", "multiplayer_overlays", "FM_Tat_M_047", "FM_Tat_F_047", 100),

            // Left leg
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Single", "mpbusiness_overlays", string.Empty, "MP_Buis_F_LLeg_000", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Spider Outline", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_001", "MP_Xmas2_F_Tat_001", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Spider Color", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_002", "MP_Xmas2_F_Tat_002", 350),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Patriot Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_005_M", "MP_Gunrunning_Tattoo_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Stylized Tiger", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_007_M", "MP_Gunrunning_Tattoo_007_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Death Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_011_M", "MP_Gunrunning_Tattoo_011_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Rose Revolver", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_023_M", "MP_Gunrunning_Tattoo_023_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Squares", "mphipster_overlays", "FM_Hip_M_Tat_009", "FM_Hip_F_Tat_009", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Charm", "mphipster_overlays", "FM_Hip_M_Tat_019", "FM_Hip_F_Tat_019", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Black Anchor", "mphipster_overlays", "FM_Hip_M_Tat_040", "FM_Hip_F_Tat_040", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "LS Serpent", "mplowrider_overlays", "MP_LR_Tat_007_M", "MP_LR_Tat_007_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Presidents", "mplowrider_overlays", "MP_LR_Tat_020_M", "MP_LR_Tat_020_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Rose Tribute", "mpbiker_overlays", "MP_MP_Biker_Tat_002_M", "MP_MP_Biker_Tat_002_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Ride or Die", "mpbiker_overlays", "MP_MP_Biker_Tat_015_M", "MP_MP_Biker_Tat_015_F", 100),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Bad Luck", "mpbiker_overlays", "MP_MP_Biker_Tat_027_M", "MP_MP_Biker_Tat_027_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Engulfed Skull", "mpbiker_overlays", "MP_MP_Biker_Tat_036_M", "MP_MP_Biker_Tat_036_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Scorched Soul", "mpbiker_overlays", "MP_MP_Biker_Tat_037_M", "MP_MP_Biker_Tat_037_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Ride Free", "mpbiker_overlays", "MP_MP_Biker_Tat_044_M", "MP_MP_Biker_Tat_044_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Bone Cruiser", "mpbiker_overlays", "MP_MP_Biker_Tat_056_M", "MP_MP_Biker_Tat_056_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Laughing Skull", "mpbiker_overlays", "MP_MP_Biker_Tat_057_M", "MP_MP_Biker_Tat_057_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Death Us Do Part", "mplowrider2_overlays", "MP_LR_Tat_029_M", "MP_LR_Tat_029_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Serpent of Death", "mpluxe_overlays", "MP_LUXE_TAT_000_M", "MP_LUXE_TAT_000_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Cross of Roses", "mpluxe2_overlays", "MP_LUXE_TAT_011_M", "MP_LUXE_TAT_011_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dagger Devil", "mpstunt_overlays", "MP_MP_Stunt_Tat_007_M", "MP_MP_Stunt_Tat_007_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dirt Track Hero", "mpstunt_overlays", "MP_MP_Stunt_Tat_013_M", "MP_MP_Stunt_Tat_013_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Golden Cobra", "mpstunt_overlays", "MP_MP_Stunt_Tat_021_M", "MP_MP_Stunt_Tat_021_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Quad Goblin", "mpstunt_overlays", "MP_MP_Stunt_Tat_028_M", "MP_MP_Stunt_Tat_028_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Stunt Jesus", "mpstunt_overlays", "MP_MP_Stunt_Tat_031_M", "MP_MP_Stunt_Tat_031_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon and Dagger", "multiplayer_overlays", "FM_Tat_Award_M_009", "FM_Tat_Award_F_009", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Melting Skull", "multiplayer_overlays", "FM_Tat_M_002", "FM_Tat_F_002", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon Mural", "multiplayer_overlays", "FM_Tat_M_008", "FM_Tat_F_008", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Serpent Skull", "multiplayer_overlays", "FM_Tat_M_021", "FM_Tat_F_021", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Hottie", "multiplayer_overlays", "FM_Tat_M_023", "FM_Tat_F_023", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Smoking Dagger", "multiplayer_overlays", "FM_Tat_M_026", "FM_Tat_F_026", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Faith", "multiplayer_overlays", "FM_Tat_M_032", "FM_Tat_F_032", 200),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Chinese Dragon", "multiplayer_overlays", "FM_Tat_M_033", "FM_Tat_F_033", 300),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Dragon", "multiplayer_overlays", "FM_Tat_M_035", "FM_Tat_F_035", 250),
            new BusinessTattooModel(TATTOO_ZONE_LEFT_LEG, "Grim Reaper", "multiplayer_overlays", "FM_Tat_M_037", "FM_Tat_F_037", 200),
            
            // Right leg
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Diamond Crown", "mpbusiness_overlays", string.Empty, "MP_Buis_F_RLeg_000", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Floral Dagger", "mpchristmas2_overlays", "MP_Xmas2_M_Tat_014", "MP_Xmas2_F_Tat_014", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Combat Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_006_M", "MP_Gunrunning_Tattoo_006_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Restless Skull", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_026_M", "MP_Gunrunning_Tattoo_026_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Pistol Ace", "mpgunrunning_overlays", "MP_Gunrunning_Tattoo_030_M", "MP_Gunrunning_Tattoo_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Grub", "mphipster_overlays", "FM_Hip_M_Tat_038", "FM_Hip_F_Tat_038", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Sparkplug", "mphipster_overlays", "FM_Hip_M_Tat_042", "FM_Hip_F_Tat_042", 100),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Ink Me", "mplowrider_overlays", "MP_LR_Tat_017_M", "MP_LR_Tat_017_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dance of Hearts", "mplowrider_overlays", "MP_LR_Tat_023_M", "MP_LR_Tat_023_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dragon's Fury", "mpbiker_overlays", "MP_MP_Biker_Tat_004_M", "MP_MP_Biker_Tat_004_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Western Insignia", "mpbiker_overlays", "MP_MP_Biker_Tat_022_M", "MP_MP_Biker_Tat_022_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Dusk Rider", "mpbiker_overlays", "MP_MP_Biker_Tat_028_M", "MP_MP_Biker_Tat_028_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "American Made", "mpbiker_overlays", "MP_MP_Biker_Tat_040_M", "MP_MP_Biker_Tat_040_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "STFU", "mpbiker_overlays", "MP_MP_Biker_Tat_048_M", "MP_MP_Biker_Tat_048_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "San Andreas Prayer", "mplowrider2_overlays", "MP_LR_Tat_030_M", "MP_LR_Tat_030_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Elaborate Los Muertos", "mpluxe_overlays", "MP_LUXE_TAT_001_M", "MP_LUXE_TAT_001_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Starmetric", "mpluxe2_overlays", "MP_LUXE_TAT_023_M", "MP_LUXE_TAT_023_F", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Homeward Bound", "mpsmuggler_overlays", "MP_Smuggler_Tattoo_020_M", "MP_Smuggler_Tattoo_020_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Demon Spark Plug", "mpstunt_overlays", "MP_MP_Stunt_Tat_005_M", "MP_MP_Stunt_Tat_005_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Praying Gloves", "mpstunt_overlays", "MP_MP_Stunt_Tat_015_M", "MP_MP_Stunt_Tat_015_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Piston Angel", "mpstunt_overlays", "MP_MP_Stunt_Tat_020_M", "MP_MP_Stunt_Tat_020_F", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Speed Freak", "mpstunt_overlays", "MP_MP_Stunt_Tat_025_M", "MP_MP_Stunt_Tat_025_F", 150),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Wheelie Mouse", "mpstunt_overlays", "MP_MP_Stunt_Tat_032_M", "MP_MP_Stunt_Tat_032_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Severed Hand", "mpstunt_overlays", "MP_MP_Stunt_Tat_045_M", "MP_MP_Stunt_Tat_045_F", 400),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Brake Knife", "mpstunt_overlays", "MP_MP_Stunt_Tat_047_M", "MP_MP_Stunt_Tat_047_F", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Skull and Sword", "multiplayer_overlays", "FM_Tat_Award_M_006", "FM_Tat_Award_F_006", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "The Warrior", "multiplayer_overlays", "FM_Tat_M_007", "FM_Tat_F_007", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Tribal", "multiplayer_overlays", "FM_Tat_M_017", "FM_Tat_F_017", 250),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Fiery Dragon", "multiplayer_overlays", "FM_Tat_M_022", "FM_Tat_F_022", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Broken Skull", "multiplayer_overlays", "FM_Tat_M_039", "FM_Tat_F_039", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Flaming Skull", "multiplayer_overlays", "FM_Tat_M_040", "FM_Tat_F_040", 300),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Flaming Scorpion", "multiplayer_overlays", "FM_Tat_M_042", "FM_Tat_F_042", 200),
            new BusinessTattooModel(TATTOO_ZONE_RIGHT_LEG, "Indian Ram", "multiplayer_overlays", "FM_Tat_M_043", "FM_Tat_F_043", 200)
        };

        // Vehicle's doors
        public const int DRIVER_FRONT_DOOR = 0;
        public const int PASSENGER_FRONT_DOOR = 1;
        public const int DRIVER_REAR_DOOR = 2;
        public const int PASSENGER_REAR_DOOR = 3;
        public const int VEHICLE_HOOD = 4;
        public const int VEHICLE_TRUNK = 5;

        public static List<Vector3> CARSHOP1_SPAWNS = new List<Vector3>()
        {
            new Vector3(-13.99559f, -1080.065f, 26.67207f),
            new Vector3(-10.65174f, -1080.578f, 26.67614f),
            new Vector3(-7.598934f, -1082.825f, 26.67207f),
            new Vector3(-9.582541f, -1097.432f, 26.67207f)
        };
        public static List<Vector3> CARSHOP_SPAWNS = new List<Vector3>()
        {
            new Vector3(-207.5757f, 6219.714f, 31.49114f),
            new Vector3(-205.2744f, 6221.958f, 31.49089f),
            new Vector3(-203.0463f, 6224.42f, 31.4899f),
            new Vector3(-200.6914f, 6226.81f, 31.49411f),
        };

        public static List<Vector3> BIKESHOP_SPAWNS = new List<Vector3>()
{
            new Vector3(2141.214f, 4824.846f, 41.26408f),
            new Vector3(2135.951f, 4822.354f, 41.23187f),
            new Vector3(2129.432f, 4819.786f, 41.24192f),
            new Vector3(2123.531f, 4817.106f, 41.25154f)
        };
        public static List<Vector3> BIKESHOP1_SPAWNS = new List<Vector3>()
{
            new Vector3(-145.6139f, -1344.307f, 29.81028f),
            new Vector3(-142.4561f, -1344.573f, 29.81361f),
            new Vector3(-139.1066f, -1344.303f, 29.8299f),
        };
        public static List<Vector3> SHIP_SPAWNS = new List<Vector3>()
        {
            new Vector3(1477.21f, 3790.257f, 29.74804f),
            new Vector3(1456.734f, 3786.735f, 29.78764f),
        };
        public static List<Vector3> SHIP1_SPAWNS = new List<Vector3>()
        {
            new Vector3(-795.0441f, -1501.334f, -0.4743263f)
        };

        public static List<GarbageModel> GARBAGE_LIST = new List<GarbageModel>()
        {
            // North
            new GarbageModel(NORTH_ROUTE, 0, new Vector3(-81.96722f, 6478.826f, 31.09088f)),
            new GarbageModel(NORTH_ROUTE, 1, new Vector3(-93.95376f, 6495.34f, 31.09092f)),
            new GarbageModel(NORTH_ROUTE, 2, new Vector3(-74.22137f, 6503.464f, 31.09098f)),
            new GarbageModel(NORTH_ROUTE, 3, new Vector3(-29.01638f, 6529.239f, 31.09091f)),
            new GarbageModel(NORTH_ROUTE, 4, new Vector3(-5.041111f, 6511.063f, 31.14442f)),
            new GarbageModel(NORTH_ROUTE, 5, new Vector3(-136.5993f, 6474.146f, 31.06843f)),
            new GarbageModel(NORTH_ROUTE, 6, new Vector3(-189.9569f, 6430.725f, 31.11271f)),
            new GarbageModel(NORTH_ROUTE, 7, new Vector3(-360.819f, 6320.19f, 29.35117f)),
            new GarbageModel(NORTH_ROUTE, 8, new Vector3(-347.5222f, 6243.716f, 31.08796f)),
            new GarbageModel(NORTH_ROUTE, 9, new Vector3(-435.6904f, 6143.878f, 31.07823f)),
            new GarbageModel(NORTH_ROUTE, 10, new Vector3(-397.4097f, 6094.936f, 31.06692f)),
            new GarbageModel(NORTH_ROUTE, 11, new Vector3(-400.0103f, 6081.856f, 31.10014f)),
            new GarbageModel(NORTH_ROUTE, 12, new Vector3(-377.2241f, 6073.637f, 31.08218f)),
            new GarbageModel(NORTH_ROUTE, 13, new Vector3(-386.2651f, 6043.054f, 31.10082f)),
            new GarbageModel(NORTH_ROUTE, 14, new Vector3(-343.0647f, 6068.65f, 31.07741f)),
            new GarbageModel(NORTH_ROUTE, 15, new Vector3(-318.6424f, 6082.256f, 30.9172f)),
            new GarbageModel(NORTH_ROUTE, 16, new Vector3(-128.3863f, 6230.171f, 30.94285f)),
            new GarbageModel(NORTH_ROUTE, 17, new Vector3(-120.422f, 6207.901f, 30.80298f)),
            new GarbageModel(NORTH_ROUTE, 18, new Vector3(-203.6863f, 6246.423f, 31.09464f)),
            new GarbageModel(NORTH_ROUTE, 19, new Vector3(-255.7751f, 6250.462f, 31.08915f)),
            new GarbageModel(NORTH_ROUTE, 20, new Vector3(-154.0765f, 6349.84f, 31.19388f)),
            new GarbageModel(NORTH_ROUTE, 21, new Vector3(-138.2741f, 6342.727f, 31.09089f)),
            new GarbageModel(NORTH_ROUTE, 22, new Vector3(-126.1147f, 6345.387f, 31.09039f)),
            new GarbageModel(NORTH_ROUTE, 23, new Vector3(-52.59103f, 6355.281f, 30.98548f)),
            new GarbageModel(NORTH_ROUTE, 24, new Vector3(-20.58008f, 6386.159f, 30.92557f)),
            new GarbageModel(NORTH_ROUTE, 25, new Vector3(5.711287f, 6411.328f, 30.93486f)),

            // South
            new GarbageModel(SOUTH_ROUTE, 0, new Vector3(-223.4353f, -1559.368f, 33.44161f)),
            new GarbageModel(SOUTH_ROUTE, 1, new Vector3(-228.065f, -1633.905f, 33.11632f)),
            new GarbageModel(SOUTH_ROUTE, 2, new Vector3(-14.47537f, -1817.777f, 25.40428f)),
            new GarbageModel(SOUTH_ROUTE, 3, new Vector3(114.0621f, -1943.522f, 20.27394f)),
            new GarbageModel(SOUTH_ROUTE, 4, new Vector3(191.9191f, -1909.875f, 22.67064f)),
            new GarbageModel(SOUTH_ROUTE, 5, new Vector3(283.0735f, -2090.526f, 16.21736f)),
            new GarbageModel(SOUTH_ROUTE, 6, new Vector3(584.6151f, -2816.911f, 5.632026f)),
            new GarbageModel(SOUTH_ROUTE, 7, new Vector3(809.6602f, -2943.637f, 5.48317f)),
            new GarbageModel(SOUTH_ROUTE, 8, new Vector3(851.9996f, -2263.533f, 29.90977f)),
            new GarbageModel(SOUTH_ROUTE, 9, new Vector3(450.8191f, -1970.979f, 22.5295f)),
            new GarbageModel(SOUTH_ROUTE, 10, new Vector3(233.6994f, -1773.683f, 28.25623f)),
            new GarbageModel(SOUTH_ROUTE, 11, new Vector3(-49.93156f, -1486.384f, 31.23168f)),

            // East
            new GarbageModel(EAST_ROUTE, 0, new Vector3(-75.37492f, -1317.174f, 28.6338f)),
            new GarbageModel(EAST_ROUTE, 1, new Vector3(452.5973f, -1070.959f, 28.78741f)),
            new GarbageModel(EAST_ROUTE, 2, new Vector3(808.8442f, -1044.986f, 26.21269f)),
            new GarbageModel(EAST_ROUTE, 3, new Vector3(792.4164f, -913.0083f, 24.84123f)),
            new GarbageModel(EAST_ROUTE, 4, new Vector3(1374.186f, -582.5751f, 73.90349f)),
            new GarbageModel(EAST_ROUTE, 5, new Vector3(1169.929f, -317.8119f, 68.75391f)),
            new GarbageModel(EAST_ROUTE, 6, new Vector3(1079.681f, -792.5755f, 57.84469f)),
            new GarbageModel(EAST_ROUTE, 7, new Vector3(380.5891f, -903.834f, 29.00401f)),
            new GarbageModel(EAST_ROUTE, 8, new Vector3(-27.14754f, -1082.343f, 26.19075f)),
            new GarbageModel(EAST_ROUTE, 9, new Vector3(-167.9472f, -1298.646f, 30.72958f)),
            new GarbageModel(EAST_ROUTE, 10, new Vector3(-240.3486f, -1304.548f, 30.89062f)),

            // West
            new GarbageModel(WEST_ROUTE, 0, new Vector3(-995.6018f, -1116.74f, 1.685367f)),
            new GarbageModel(WEST_ROUTE, 1, new Vector3(-1054.836f, -1021.385f, 1.617427f)),
            new GarbageModel(WEST_ROUTE, 2, new Vector3(-1254.953f, -862.3749f, 11.91052f)),
            new GarbageModel(WEST_ROUTE, 3, new Vector3(-1318.305f, -769.3898f, 19.78054f)),
            new GarbageModel(WEST_ROUTE, 4, new Vector3(-1402.672f, -637.1053f, 28.26154f)),
            new GarbageModel(WEST_ROUTE, 5, new Vector3(-1542.366f, -563.0222f, 33.24453f)),
            new GarbageModel(WEST_ROUTE, 6, new Vector3(-1169.688f, -748.9186f, 18.93798f)),
            new GarbageModel(WEST_ROUTE, 7, new Vector3(-794.9119f, -959.1639f, 14.92298f)),
            new GarbageModel(WEST_ROUTE, 8, new Vector3(-339.9153f, -1316.762f, 30.80578f))
        };

        public static List<Vector3> STOLEN_CAR_CHECKS = new List<Vector3>()
        {
            new Vector3(210.473, -848.802, 29.75367)
        };

        public static List<TunningPriceModel> TUNNING_PRICE_LIST = new List<TunningPriceModel>()
        {
            new TunningPriceModel(VEHICLE_MOD_SPOILER, 250),
            new TunningPriceModel(VEHICLE_MOD_FRONT_BUMPER, 250),
            new TunningPriceModel(VEHICLE_MOD_REAR_BUMPER, 250),
            new TunningPriceModel(VEHICLE_MOD_SIDE_SKIRT, 250),
            new TunningPriceModel(VEHICLE_MOD_EXHAUST, 100),
            new TunningPriceModel(VEHICLE_MOD_FRAME, 500),
            new TunningPriceModel(VEHICLE_MOD_GRILLE, 200),
            new TunningPriceModel(VEHICLE_MOD_HOOD, 300),
            new TunningPriceModel(VEHICLE_MOD_FENDER, 100),
            new TunningPriceModel(VEHICLE_MOD_RIGHT_FENDER, 100),
            new TunningPriceModel(VEHICLE_MOD_ROOF, 400),
            new TunningPriceModel(VEHICLE_MOD_HORN, 100),
            new TunningPriceModel(VEHICLE_MOD_SUSPENSION, 900),
            new TunningPriceModel(VEHICLE_MOD_XENON, 150),
            new TunningPriceModel(VEHICLE_MOD_FRONT_WHEELS, 100),
            new TunningPriceModel(VEHICLE_MOD_BACK_WHEELS, 100),
            new TunningPriceModel(VEHICLE_MOD_PLATE_HOLDERS, 100),
            new TunningPriceModel(VEHICLE_MOD_TRIM_DESIGN, 800),
            new TunningPriceModel(VEHICLE_MOD_ORNAMIENTS, 150),
            new TunningPriceModel(VEHICLE_MOD_STEERING_WHEEL, 100),
            new TunningPriceModel(VEHICLE_MOD_SHIFTER_LEAVERS, 100),
            new TunningPriceModel(VEHICLE_MOD_HYDRAULICS, 1200),
            new TunningPriceModel(VEHICLE_MOD_ENGINE, 3500),
            new TunningPriceModel(VEHICLE_MOD_BRAKES, 3500),
            new TunningPriceModel(VEHICLE_MOD_TRANSMISSION, 3000),
            new TunningPriceModel(VEHICLE_MOD_TURBO, 5000),
            new TunningPriceModel(VEHICLE_MOD_LIVERY, 1000),
            new TunningPriceModel(VEHICLE_MOD_WINDOW_TINT, 500),
            new TunningPriceModel(VEHICLE_MOD_COLOUR2, 200),
            new TunningPriceModel(VEHICLE_MOD_COLOUR1, 200)
        };

        // Pawn shops
        public static List<Vector3> PAWN_SHOP = new List<Vector3>()
        {
            new Vector3(-44.59276f, 6447.872f, 31.47821f),
            new Vector3(1929.779f, 3721.547f, 32.8097f),
            new Vector3(-498.9339f, -1714.139f, 19.89918f)
        };

        // Weapons crates
        public static List<CrateSpawnModel> CRATE_SPAWN_LIST = new List<CrateSpawnModel>()
        {
            // Island crates
            new CrateSpawnModel(0, new Vector3(-2153.035f, 5202.386f, 13.69618f)),
            new CrateSpawnModel(0, new Vector3(-2165.921f, 5196.308f, 15.8804f)),
            new CrateSpawnModel(0, new Vector3(-2166.648f, 5198.421f, 15.8804f)),
            new CrateSpawnModel(0, new Vector3(-2160.49f, 5205.409f, 15.59176f)),
            new CrateSpawnModel(0, new Vector3(-2170.187f, 5198.082f, 15.91406f)),
            new CrateSpawnModel(0, new Vector3(-2170.373f, 5195.084f, 15.88041f)),
            new CrateSpawnModel(0, new Vector3(-2164.61f, 5206.934f, 15.8803f)),
            new CrateSpawnModel(0, new Vector3(-2163.792f, 5195.11f, 15.37502f)),
            new CrateSpawnModel(0, new Vector3(-2160.487f, 5192.151f, 14.30191f)),
            new CrateSpawnModel(0, new Vector3(-2166.827f, 5207.111f, 15.92977f)),
            new CrateSpawnModel(0, new Vector3(-2168.576f, 5186.567f, 15.04852f)),
            new CrateSpawnModel(0, new Vector3(-2168.642f, 5204.536f, 15.94069f)),
            new CrateSpawnModel(0, new Vector3(-2171.918f, 5205.345f, 16.65958f)),
            new CrateSpawnModel(0, new Vector3(-2172.359f, 5194.064f, 15.8004f)),
            new CrateSpawnModel(0, new Vector3(-2183.144f, 5201.518f, 18.20478f)),
            new CrateSpawnModel(0, new Vector3(-2172.86f, 5196.362f, 15.86153f)),
            new CrateSpawnModel(0, new Vector3(-2187.248f, 5198.493f, 17.98749f)),
            new CrateSpawnModel(0, new Vector3(-2189.329f, 5208.591f, 18.73738f)),
            new CrateSpawnModel(0, new Vector3(-2175.566f, 5197.272f, 15.98747f)),
            new CrateSpawnModel(0, new Vector3(-2189.522f, 5223.072f, 20.30787f)),
            new CrateSpawnModel(0, new Vector3(-2185.964f, 5189.925f, 16.80379f)),
            new CrateSpawnModel(0, new Vector3(-2191.006f, 5229.609f, 20.79736f)),
            new CrateSpawnModel(0, new Vector3(-2196.236f, 5187.129f, 15.81522f)),
            new CrateSpawnModel(0, new Vector3(-2190.801f, 5234.782f, 20.2835f)),
            new CrateSpawnModel(0, new Vector3(-2194.725f, 5178.433f, 14.53511f)),
            new CrateSpawnModel(0, new Vector3(-2185.524f, 5230.828f, 20.47778f)),
            new CrateSpawnModel(0, new Vector3(-2185.441f, 5175.364f, 14.06973f)),
            new CrateSpawnModel(0, new Vector3(-2177.196f, 5228.695f, 18.0752f)),
            new CrateSpawnModel(0, new Vector3(-2175.935f, 5232.989f, 16.57436f)),
            new CrateSpawnModel(0, new Vector3(-2173.745f, 5171.083f, 13.5497f)),
            new CrateSpawnModel(0, new Vector3(-2167.835f, 5238.139f, 15.93195f)),
            new CrateSpawnModel(0, new Vector3(-2165.319f, 5241.51f, 16.14181f)),
            new CrateSpawnModel(0, new Vector3(-2163.495f, 5247.925f, 17.10531f)),
            new CrateSpawnModel(0, new Vector3(-2162.924f, 5251.871f, 17.53788f)),
            new CrateSpawnModel(0, new Vector3(-2169.315f, 5276.271f, 17.32012f)),
            new CrateSpawnModel(0, new Vector3(-2156.115f, 5246.802f, 17.67452f)),
            new CrateSpawnModel(0, new Vector3(-2149.593f, 5240.237f, 15.51855f)),
            new CrateSpawnModel(0, new Vector3(-2146.744f, 5237.425f, 13.9655f)),
            new CrateSpawnModel(0, new Vector3(-2142.169f, 5222.767f, 6.812176f)),
            new CrateSpawnModel(0, new Vector3(-2142.939f, 5209.612f, 9.51135f)),
            new CrateSpawnModel(0, new Vector3(-2156.537f, 5211.339f, 14.68363f)),
            new CrateSpawnModel(0, new Vector3(-2151.335f, 5163.203f, 10.25639f)),
            new CrateSpawnModel(0, new Vector3(-2151.14f, 5152.164f, 9.27355f)),
            new CrateSpawnModel(0, new Vector3(-2159.767f, 5197.274f, 15.10713f)),
            new CrateSpawnModel(0, new Vector3(-2171.098f, 5156.605f, 10.21814f)),
            new CrateSpawnModel(0, new Vector3(-2180.309f, 5144.453f, 2.673023f)),
            new CrateSpawnModel(0, new Vector3(-2194.942f, 5148.009f, 10.29645f)),
            new CrateSpawnModel(0, new Vector3(-2194.885f, 5160.1f, 11.14006f)),
            new CrateSpawnModel(0, new Vector3(-2207.712f, 5161.979f, 13.56075f)),
            new CrateSpawnModel(0, new Vector3(-2209.597f, 5178.93f, 15.06639f)),
            new CrateSpawnModel(0, new Vector3(-2190.155f, 5205.833f, 18.26774f)),
            new CrateSpawnModel(0, new Vector3(-2184.456f, 5200.89f, 18.14797f)),
            new CrateSpawnModel(0, new Vector3(-2208.291f, 5144.133f, 11.18868f)),
            new CrateSpawnModel(0, new Vector3(-2213.94f, 5124.733f, 10.61521f)),
            new CrateSpawnModel(0, new Vector3(-2219.709f, 5108.725f, 10.24075f)),
            new CrateSpawnModel(0, new Vector3(-2208.989f, 5095.347f, 8.770149f)),
            new CrateSpawnModel(0, new Vector3(-2194.331f, 5086.611f, 6.89854f)),
            new CrateSpawnModel(0, new Vector3(-2185.621f, 5108.169f, 7.753129f)),
            new CrateSpawnModel(0, new Vector3(-2192.36f, 5131.622f, 11.40428f)),
            new CrateSpawnModel(0, new Vector3(-2229.719f, 5125.528f, 3.523588f)),
            new CrateSpawnModel(0, new Vector3(-2240.344f, 5116.163f, 2.330123f)),
            new CrateSpawnModel(0, new Vector3(-2236.817f, 5088.094f, 2.087778f)),
            new CrateSpawnModel(0, new Vector3(-2166.787f, 5200.901f, 20.06452)),
            new CrateSpawnModel(0, new Vector3(-2173.201f, 5199.95f, 20.03394f))
        };

        // Weapon and ammunition drop rate
        public static List<WeaponProbabilityModel> WEAPON_CHANCE_LIST = new List<WeaponProbabilityModel>() {
            // Weapons
            new WeaponProbabilityModel(0, "BullpupShotgun", 0, 0, 60),
            new WeaponProbabilityModel(0, "CompactRifle", 0, 61, 75),
            new WeaponProbabilityModel(0, "CarbineRifle", 0, 76, 95),
            new WeaponProbabilityModel(0, "HeavyShotgun", 0, 96, 125),
            new WeaponProbabilityModel(0, "SawnoffShotgun", 0, 126, 215),
            new WeaponProbabilityModel(0, "BullpupRifle", 0, 216, 235),
            new WeaponProbabilityModel(0, "AssaultRifle", 0, 236, 260),
            new WeaponProbabilityModel(0, "APPistol", 0, 261, 320),
            new WeaponProbabilityModel(0, "DoubleBarrelShotgun", 0, 321, 395),
            new WeaponProbabilityModel(0, "MachinePistol", 0, 396, 480),
            new WeaponProbabilityModel(0, "SniperRifle", 0, 481, 495),
            new WeaponProbabilityModel(0, "AssaultSMG", 0, 496, 545),
            new WeaponProbabilityModel(0, "CombatPDW", 0, 546, 580),
            new WeaponProbabilityModel(0, "Revolver", 0, 581, 620),
            new WeaponProbabilityModel(0, "HeavyPistol", 0, 621, 770),
            new WeaponProbabilityModel(0, "PumpShotgun", 0, 771, 850),
            new WeaponProbabilityModel(0, "SpecialCarbine", 0, 851, 865),
            new WeaponProbabilityModel(0, "Pistol50", 0, 866, 1065),
            new WeaponProbabilityModel(0, "AdvancedRifle", 0, 1066, 1085),
            new WeaponProbabilityModel(0, "HeavySniper", 0, 1086, 1100),
            new WeaponProbabilityModel(0, "MicroSMG", 0, 1101, 1150),
            new WeaponProbabilityModel(0, "AssaultShotgun", 0, 1151, 1180),
            new WeaponProbabilityModel(0, "MarksmanRifle", 0, 1181, 1195),
            new WeaponProbabilityModel(0, "SMG", 0, 1196, 1235),

            // Ammunition
            new WeaponProbabilityModel(1, ITEM_HASH_PISTOL_AMMO_CLIP, STACK_PISTOL_CAPACITY, 0, 355),
            new WeaponProbabilityModel(1, ITEM_HASH_MACHINEGUN_AMMO_CLIP, STACK_MACHINEGUN_CAPACITY, 356, 420),
            new WeaponProbabilityModel(1, ITEM_HASH_SHOTGUN_AMMO_CLIP, STACK_SHOTGUN_CAPACITY, 421, 485),
            new WeaponProbabilityModel(1, ITEM_HASH_ASSAULTRIFLE_AMMO_CLIP, STACK_ASSAULTRIFLE_CAPACITY, 486, 495),
            new WeaponProbabilityModel(1, ITEM_HASH_SNIPERRIFLE_AMMO_CLIP, STACK_SNIPERRIFLE_CAPACITY, 496, 500)
        };

        // Highlighted businesses
        public static List<BusinessBlipModel> BUSINESS_BLIP_LIST = new List<BusinessBlipModel>()
        {
            new BusinessBlipModel(Constants.BUSINESS_TYPE_AMMUNATION, 567),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_GAS_STATION, 361),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_HARDWARE, 80),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_BAR, 93),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_BARBER_SHOP, 71),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_FISHING, 68),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_DISCO, 136),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_MECHANIC, 643),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_TATTOO_SHOP, 75),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_CLOTHES, 73),
            new BusinessBlipModel(Constants.BUSINESS_TYPE_24_7, 52)
        };

        // Job points
        public static List<JobPickModel> JOB_PICK_LIST = new List<JobPickModel>()
        {
            new JobPickModel(JOB_FASTFOOD, 605, GenRes.fastfood_job, new Vector3(-133.24f, 6377.585f, 32.17684f), DescRes.job_fastfood),
          //  new JobPickModel(JOB_BURGERFLOPPER, 605, GenRes.fastfood_job, new Vector3(-1549.613f, -435.8356f, 35.8867f), DescRes.job_fastfood),
            new JobPickModel(JOB_HOOKER, 121, GenRes.hooker, new Vector3(-309.9488f, 6272.006f, 31.49236f), DescRes.job_hooker),
            new JobPickModel(JOB_HOOKER, 121, GenRes.hooker, new Vector3(2467.066f, 4100.877f, 37.58295f), DescRes.job_hooker),
           // new JobPickModel(JOB_LAWYER, 351, GenRes.lawyer, new Vector3(-67.26025f, -802.2687f, 44.22607f), DescRes.job_lawyer),
            new JobPickModel(JOB_GARBAGE, 0, GenRes.garbage_job, new Vector3(), DescRes.job_garbage),
            new JobPickModel(JOB_MECHANIC, 643, GenRes.mechanic_job, new Vector3(119.923f, 6627.261f, 31.94834f), DescRes.job_mechanic),
            new JobPickModel(JOB_MECHANIC, 643, GenRes.mechanic_job, new Vector3(1187.899f, 2646.012f, 38.36409f), DescRes.job_mechanic),
          //  new JobPickModel(JOB_THIEF, 605, GenRes.thief_job, new Vector3(1529.182f, 3794.486f, 34.46811f), DescRes.job_thief),
            new JobPickModel(JOB_TRUCKER, 0, GenRes.trucker_job, new Vector3(), DescRes.job_trucker),
            new JobPickModel(JOB_OCEANCLEANER, 0, GenRes.oceancleaner_job, new Vector3(288.0206f, -2981.255f, 5.862742f), DescRes.job_oceancleaner),
         //   new JobPickModel(JOB_DELIVERYMAN, 0, GenRes.deliveryman_job, new Vector3(95.79912, 6367.596, 31.37588f), DescRes.job_deliveryman)
        };

        // Author: A
        // Date: 05/16/2019 1:41 AM
        // Male skins on the creator
        public static readonly List<string> MALE_SKINS = new List<string>()
        {
            "u_m_y_abner", "a_m_m_acult_01", "a_m_o_acult_01", "a_m_y_acult_01", "a_m_o_acult_02", "a_m_y_acult_02", "a_m_m_afriamer_01", "ig_mp_agent14", "csb_agent",
            "s_m_y_airworker", "u_m_m_aldinapoli", "s_m_y_ammucity_01", "s_m_m_ammucountry", "ig_andreas", "u_m_y_antonb", "g_m_m_armboss_01", "g_m_m_armgoon_01",
            "g_m_y_armgoon_02", "g_m_m_armlieut_01", "s_m_y_armymech_01", "s_m_y_autopsy_01", "s_m_m_autoshop_01", "s_m_m_autoshop_02", "ig_money", "g_m_y_azteca_01",
            "u_m_y_babyd", "g_m_y_ballaeast_01", "g_m_y_ballaorig_01", "g_m_y_ballaorig_01", "ig_ballasog", "g_m_y_ballasout_01", "u_m_m_bankman", "ig_bankman",
            "s_m_y_barman_01", "ig_barry", "s_m_y_baywatch_01", "u_m_y_baygor", "a_m_m_beach_01", "a_m_o_beach_01", "a_m_y_beach_01", "a_m_m_beach_02", "a_m_y_beach_02",
            "a_m_y_beach_03", "a_m_y_beachvesp_01", "a_m_y_beachvesp_02", "ig_benny", "ig_bestmen", "ig_beverly", "a_m_m_bevhills_01", "a_m_y_bevhills_01", "a_m_m_bevhills_02",
            "a_m_y_bevhills_02", "u_m_m_bikehire_01", "mp_m_boatstaff_01", "s_m_m_bouncer_01", "ig_brad", "a_m_y_breakdance_01", "u_m_y_burgerdrug_01", "s_m_y_busboy_01",
            "a_m_y_busicas_01", "a_m_m_business_01", "a_m_y_business_01", "a_m_y_business_02", "a_m_y_business_03", "s_m_o_busker_01", "ig_car3guy1", "ig_car3guy2",
            "cs_carbuyer", "s_m_m_ccrew_01", "s_m_y_chef_01", "ig_chef2", "g_m_m_chemwork_01", "g_m_m_chiboss_01", "g_m_m_chigoon_01", "g_m_m_chigoon_02", "csb_chin_goon",
            "u_m_y_chip", "s_m_m_ciasec_01", "ig_clay", "ig_claypain", "ig_cletus", "s_m_m_cntrybar_01", "s_m_y_construct_01", "s_m_y_construct_02", "csb_customer",
            "u_m_y_cyclist_01", "a_m_y_cyclist_01", "ig_dale", "ig_davenorton", "s_m_y_dealer_01", "ig_devin", "s_m_y_devinsec_01", "a_m_y_dhill_01", "u_m_m_doa_01",
            "s_m_m_dockwork_01", "s_m_y_dockwork_01", "s_m_m_doctor_01", "ig_dom", "s_m_y_doorman_01", "a_m_y_downtown_01", "ig_dreyfuss", "ig_drfriedlander",
            "s_m_y_dwservice_01", "Name: s_m_y_dwservice_02", "a_m_m_eastsa_01", "a_m_y_eastsa_01", "a_m_m_eastsa_02", "a_m_y_eastsa_02", "u_m_m_edtoh", "a_m_y_epsilon_01",
            "a_m_y_epsilon_02", "mp_m_exarmy_01", "ig_fabien", "s_m_y_factory_01", "g_m_y_famca_01", "mp_m_famdd_01", "g_m_y_famdnf_01", "g_m_y_famfor_01", "a_m_m_farmer_01",
            "a_m_m_fatlatin_01", "ig_fbisuit_01", "u_m_m_fibarchitect", "u_m_y_fibmugger_01", "s_m_m_fiboffice_01", "s_m_m_fiboffice_02", "u_m_m_filmdirector", "u_m_o_finguru_01",
            "ig_floyd", "csb_fos_rep", "ig_g", "s_m_m_gaffer_01", "s_m_y_garbage", "s_m_m_gardener_01", "a_m_y_gay_01", "a_m_y_gay_02", "csb_g", "a_m_m_genfat_01",
            "a_m_m_genfat_02", "a_m_o_genstreet_01", "a_m_y_genstreet_01", "a_m_y_genstreet_02", "s_m_m_gentransport", "u_m_m_glenstank_01", "a_m_m_golfer_01", "a_m_y_golfer_01",
            "u_m_m_griff_01", "s_m_y_grip_01", "ig_groom", "csb_grove_str_dlr", "u_m_y_guido_01", "u_m_y_gunvend_01", "hc_hacker", "s_m_m_hairdress_01", "ig_hao", "a_m_m_hasjew_01",
            "a_m_y_hasjew_01", "s_m_m_highsec_01", "s_m_m_highsec_02", "a_m_y_hiker_01", "a_m_m_hillbilly_01", "a_m_m_hillbilly_02", "u_m_y_hippie_01", "a_m_y_hippy_01",
            "a_m_y_hipster_01", "a_m_y_hipster_03", "csb_hugh", "csb_imran", "a_m_m_indian_01", "a_m_y_indian_01", "csb_jackhowitzer", "s_m_m_janitor", "ig_jay_norris",
            "u_m_m_jesus_01", "a_m_y_jetski_01", "u_m_m_jewelsec_01", "u_m_m_jewelthief", "ig_jimmyboston", "ig_jimmydisanto", "ig_joeminuteman", "ig_josef", "ig_josh",
            "a_m_y_juggalo_01", "g_m_m_korboss_01", "g_m_y_korean_01", "g_m_y_korean_02", "g_m_y_korlieut_01", "a_m_m_ktown_01", "a_m_o_ktown_01", "a_m_y_ktown_01",
            "a_m_y_ktown_02", "ig_lamardavis", "s_m_m_lathandy_01", "a_m_y_latino_01", "ig_lazlow", "ig_lestercrest", "ig_lifeinvad_01", "s_m_m_lifeinvad_01", "ig_lifeinvad_02",
            "g_m_y_lost_01", "s_m_m_linecook", "g_m_y_lost_02", "g_m_y_lost_03", "s_m_m_lsmetro_01", "a_m_m_malibu_01", "u_m_y_mani", "ig_manuel", "s_m_m_mariachi_01",
            "s_m_y_marine_02", "u_m_m_markfost", "ig_marnie", "cs_martinmadrazo", "ig_maryann", "a_m_y_methhead_01", "g_m_m_mexboss_01", "g_m_m_mexboss_02", "a_m_m_mexcntry_01",
            "g_m_y_mexgang_01", "g_m_y_mexgoon_01", "g_m_y_mexgoon_02", "g_m_y_mexgoon_03", "a_m_m_mexlabor_01", "a_m_y_mexthug_01", "ig_michelle", "s_m_m_migrant_01",
            "u_m_y_militarybum", "ig_milton", "a_m_y_motox_02", "cs_movpremmale", "s_m_m_movprem_01", "mp_g_m_pros_01", "ig_mrk", "a_m_y_musclbeac_01", "a_m_y_musclbeac_02",
            "ig_nervousron", "ig_nigel", "a_m_m_og_boss_01", "ig_old_man1a", "ig_old_man2", "ig_omega", "ig_oneil", "ig_ortega", "csb_oscar", "a_m_m_paparazzi_01",
            "u_m_y_paparazzi", "ig_paper", "u_m_y_party_01", "u_m_m_partytarget", "s_m_y_pestcont_01", "hc_driver", "s_m_m_pilot_01", "s_m_y_pilot_01", "s_m_m_pilot_02",
            "g_m_y_pologoon_01", "g_m_y_pologoon_02", "a_m_m_polynesian_01", "a_m_y_polynesian_01", "ig_popov", "s_m_m_postal_01", "s_m_m_postal_02", "ig_priest",
            "s_m_y_prismuscl_01", "u_m_y_prisoner_01", "s_m_y_prisoner_01", "u_m_y_proldriver_01", "a_m_m_prolhost_01", "u_m_m_prolsec_01", "csb_prolsec", "ig_prolsec_02",
            "ig_ramp_gang", "ig_ramp_hic", "ig_ramp_hipster", "ig_ramp_mex", "ig_rashcosvki", "csb_reporter", "u_m_m_rivalpap", "a_m_y_roadcyc_01", "s_m_y_robber_01",
            "ig_roccopelosi", "a_m_y_runner_01", "a_m_y_runner_02", "a_m_m_rurmeth_01", "ig_russiandrunk", "a_m_m_salton_01", "a_m_o_salton_01", "a_m_y_salton_01",
            "a_m_m_salton_02", "a_m_m_salton_03", "a_m_m_salton_04", "g_m_y_salvaboss_01", "g_m_y_salvagoon_01", "g_m_y_salvagoon_02", "g_m_y_salvagoon_03", "g_m_y_salvagoon_03",
            "mp_m_shopkeep_01", "s_m_y_shop_mask", "ig_siemonyetarian", "a_m_m_skater_01", "a_m_y_skater_01", "a_m_y_skater_02", "a_m_m_skidrow_01", "a_m_m_socenlat_01",
            "ig_solomon", "a_m_m_soucent_01", "a_m_o_soucent_01", "a_m_m_soucent_02", "a_m_o_soucent_02", "a_m_y_soucent_02", "a_m_m_soucent_03", "a_m_o_soucent_03",
            "a_m_y_soucent_03", "a_m_m_soucent_04", "a_m_y_soucent_04", "u_m_m_spyactor", "a_m_y_stbla_01", "a_m_y_stbla_02", "ig_stevehains", "a_m_y_stlat_01", "a_m_m_stlat_02",
            "ig_stretch", "s_m_m_strpreach_01", "g_m_y_strpunk_01", "g_m_y_strpunk_02", "s_m_m_strvend_01", "s_m_y_strvend_01", "a_m_y_stwhi_01", "a_m_y_stwhi_02",
            "a_m_y_sunbathe_01", "a_m_y_surfer_01", "ig_taocheng", "ig_taostranslator", "u_m_o_taphillbilly", "u_m_y_tattoo_01", "a_m_m_tennis_01", "ig_tenniscoach", "ig_terry",
            "cs_tom", "ig_tomepsilon", "a_m_m_tourist_01", "ig_trafficwarden", "u_m_o_tramp_01", "a_m_m_tramp_01", "a_m_o_tramp_01", "a_m_m_trampbeac_01", "s_m_m_trucker_01",
            "ig_tylerdix", "csb_undercover", "s_m_m_ups_01", "s_m_y_uscg_01", "mp_m_g_vagfun_01", "ig_vagspeak", "s_m_y_valet_01", "a_m_y_vinewood_02", "a_m_y_vinewood_03",
            "a_m_y_vinewood_04", "ig_wade", "s_m_y_waiter_01", "ig_chengsr", "u_m_m_willyfist", "s_m_y_winclean_01", "s_m_y_xmech_02", "a_m_y_yoga_01", "ig_zimbor",
            "g_m_importexport_01", "ig_agent", "ig_malc", "mp_m_cocaine_01", "mp_m_counterfeit_01", "mp_m_execpa_01", "mp_m_forgery_01", "mp_m_meth_01", "mp_m_securoguard_01",
            "mp_m_waremech_01", "mp_m_weapexp_01", "mp_m_weapwork_01", "mp_m_weed_01", "s_m_y_xmech_02_mp", "ig_lestercrest_2", "ig_avon"
        };

        // Female skins on the creator
        public static readonly List<string> FEMALE_SKINS = new List<string>()
        {
            "ig_abigail", "csb_abigail", "s_f_y_airhostess_01", "ig_amandatownley", "csb_anita", "ig_ashley", "g_f_y_ballas_01", "s_f_y_bartender_01", "s_f_y_baywatch_01",
            "a_f_m_beach_01", "a_f_y_beach_01", "a_f_m_bevhills_01", "a_f_y_bevhills_01", "a_f_m_bevhills_02", "a_f_y_bevhills_02", "a_f_y_bevhills_03", "a_f_y_bevhills_04",
            "u_f_y_bikerchic", "mp_f_boatstaff_01", "ig_bride", "a_f_y_business_01", "a_f_m_business_02", "a_f_y_business_02", "a_f_y_business_03", "a_f_y_business_04",
            "u_f_y_comjane", "cs_debra", "ig_denise", "csb_denise_friend", "a_f_m_downtown_01", "mp_f_deadhooker", "a_f_m_eastsa_01", "a_f_y_eastsa_01", "a_f_m_eastsa_02",
            "a_f_y_eastsa_02", "a_f_y_eastsa_03", "a_f_y_epsilon_01", "s_f_y_factory_01", "g_f_y_families_01", "a_f_m_fatbla_01", "a_f_m_fatcult_01", "a_f_m_fatwhite_01",
            "s_f_m_fembarber", "a_f_y_fitness_01", "a_f_y_fitness_02", "a_f_y_genhot_01", "a_f_o_genstreet_01", "a_f_y_golfer_01", "cs_guadalope", "cs_gurk", "a_f_y_hiker_01",
            "a_f_y_hippie_01", "a_f_y_hipster_01", "a_m_y_hipster_02", "a_f_y_hipster_04", "s_f_y_hooker_01", "s_f_y_hooker_02", "s_f_y_hooker_03", "u_f_y_hotposh_01",
            "ig_hunter", "a_f_o_indian_01", "a_f_y_indian_01", "ig_janet", "u_f_y_jewelass_01", "ig_jewelass", "a_f_y_juggalo_01", "ig_karen_daniels", "ig_kerrymcintosh",
            "a_f_m_ktown_01", "a_f_o_ktown_01", "a_f_m_ktown_02", "g_f_y_lost_01", "ig_magenta", "s_f_m_maid_01", "ig_maude", "u_f_m_miranda", "u_f_y_mistress", "mp_f_misty_01",
            "ig_molly", "cs_movpremf_01", "u_f_o_moviestar", "ig_mrsphillips", "ig_mrs_thornhill", "ig_natalia", "ig_paige", "ig_patricia", "u_f_y_poppymich", "u_f_y_princess",
            "u_f_o_prolhost_01", "a_f_m_prolhost_01", "u_f_m_promourn_01", "a_f_y_runner_01", "a_f_y_rurmeth_01", "a_f_m_salton_01", "a_f_o_salton_01", "a_f_y_scdressy_01",
            "ig_screen_writer", "s_f_m_shop_high", "s_f_y_shop_low", "s_f_y_shop_mid", "a_f_y_skater_01", "a_f_m_skidrow_01", "a_f_m_soucent_01", "a_f_o_soucent_01",
            "a_f_y_soucent_01", "a_m_y_soucent_01", "a_f_m_soucent_02", "a_f_o_soucent_02", "a_f_y_soucent_02", "a_f_y_soucent_03", "a_f_m_soucentmc_01", "u_f_y_spyactress",
            "csb_stripper_01", "s_f_y_stripper_02", "s_f_m_sweatshop_01", "s_f_y_sweatshop_01", "ig_tanisha", "a_f_y_tennis_01", "ig_tonya", "a_f_y_topless_01", "a_f_m_tourist_01",
            "a_f_y_tourist_01", "a_f_y_tourist_02", "ig_tracydisanto", "a_f_m_tramp_01", "a_f_m_trampbeac_01", "g_f_y_vagos_01", "a_f_y_vinewood_03", "a_f_y_vinewood_04",
            "a_f_y_yoga_01", "a_f_y_femaleagent", "g_f_importexport_01", "mp_f_cardesign_01", "mp_f_chbar_01", "mp_f_cocaine_01", "mp_f_counterfeit_01", "mp_f_execpa_01",
            "mp_f_execpa_02", "mp_f_forgery_01", "mp_f_helistaff_01", "mp_f_meth_01", "mp_f_weed_01", "a_f_y_hipster_02", "a_f_y_hipster_03", "s_f_y_migrant_01"
        };

        // ATMs
        public static List<Vector3> ATM_LIST = new List<Vector3>()
        {
            new Vector3(-846.6537, -341.509, 37.6685),
            new Vector3(1153.747, -326.7634, 69.2050),
            new Vector3(285.6829, 143.4019, 104.169),
            new Vector3(-847.204, -340.4291, 37.6793),
            new Vector3(-1410.736, -98.9279, 51.397),
            new Vector3(-1410.183, -100.6454, 51.3965),
            new Vector3(-2295.853, 357.9348, 173.6014),
            new Vector3(-2295.069, 356.2556, 173.6014),
            new Vector3(-2294.3, 354.6056, 173.6014),
            new Vector3(-282.7141, 6226.43, 30.4965),
            new Vector3(-386.4596, 6046.411, 30.474),
            new Vector3(24.5933, -945.543, 28.333),
            new Vector3(5.686, -919.9551, 28.4809),
            new Vector3(296.1756, -896.2318, 28.2901),
            new Vector3(296.8775, -894.3196, 28.2615),
            new Vector3(-846.6537, -341.509, 37.6685),
            new Vector3(-847.204, -340.4291, 37.6793),
            new Vector3(-1410.736, -98.9279, 51.397),
            new Vector3(-1410.183, -100.6454, 51.3965),
            new Vector3(-2295.853, 357.9348, 173.6014),
            new Vector3(-2295.069, 356.2556, 173.6014),
            new Vector3(-2294.3, 354.6056, 173.6014),
            new Vector3(-282.7141, 6226.43, 30.4965),
            new Vector3(-386.4596, 6046.411, 30.474),
            new Vector3(24.5933, -945.543, 28.333),
            new Vector3(5.686, -919.9551, 28.4809),
            new Vector3(296.1756, -896.2318, 28.2901),
            new Vector3(296.8775, -894.3196, 28.2615),
            new Vector3(-712.9357, -818.4827, 22.7407),
            new Vector3(-710.0828, -818.4756, 22.7363),
            new Vector3(289.53, -1256.788, 28.4406),
            new Vector3(289.2679, -1282.32, 28.6552),
            new Vector3(-1569.84, -547.0309, 33.9322),
            new Vector3(-1570.765, -547.7035, 33.9322),
            new Vector3(-1305.708, -706.6881, 24.3145),
            new Vector3(-2071.928, -317.2862, 12.3181),
            new Vector3(-821.8936, -1081.555, 10.1366),
            new Vector3(-712.9357, -818.4827, 22.7407),
            new Vector3(-710.0828, -818.4756, 22.7363),
            new Vector3(289.53, -1256.788, 28.4406),
            new Vector3(289.2679, -1282.32, 28.6552),
            new Vector3(-1569.84, -547.0309, 33.9322),
            new Vector3(-1570.765, -547.7035, 33.9322),
            new Vector3(-1305.708, -706.6881, 24.3145),
            new Vector3(-2071.928, -317.2862, 12.3181),
            new Vector3(-821.8936, -1081.555, 10.1366),
            new Vector3(-867.013, -187.9928, 36.8822),
            new Vector3(-867.9745, -186.3419, 36.8822),
            new Vector3(-3043.835, 594.1639, 6.7328),
            new Vector3(-3241.455, 997.9085, 11.5484),
            new Vector3(-204.0193, -861.0091, 29.2713),
            new Vector3(118.6416, -883.5695, 30.1395),
            new Vector3(-256.6386, -715.8898, 32.7883),
            new Vector3(-259.2767, -723.2652, 32.7015),
            new Vector3(-254.5219, -692.8869, 32.5783),
            new Vector3(-867.013, -187.9928, 36.8822),
            new Vector3(-867.9745, -186.3419, 36.8822),
            new Vector3(-3043.835, 594.1639, 6.7328),
            new Vector3(-3241.455, 997.9085, 11.5484),
            new Vector3(-204.0193, -861.0091, 29.2713),
            new Vector3(118.6416, -883.5695, 30.1395),
            new Vector3(-256.6386, -715.8898, 32.7883),
            new Vector3(-259.2767, -723.2652, 32.7015),
            new Vector3(-254.5219, -692.8869, 32.5783),
            new Vector3(89.8134, 2.8803, 67.3521),
            new Vector3(-617.8035, -708.8591, 29.0432),
            new Vector3(-617.8035, -706.8521, 29.0432),
            new Vector3(-614.5187, -705.5981, 30.224),
            new Vector3(-611.8581, -705.5981, 30.224),
            new Vector3(-537.8052, -854.9357, 28.2754),
            new Vector3(-526.7791, -1223.374, 17.4527),
            new Vector3(-1315.416, -834.431, 15.9523),
            new Vector3(-1314.466, -835.6913, 15.9523),
            new Vector3(89.8134, 2.8803, 67.3521),
            new Vector3(-617.8035, -708.8591, 29.0432),
            new Vector3(-617.8035, -706.8521, 29.0432),
            new Vector3(-614.5187, -705.5981, 30.224),
            new Vector3(-611.8581, -705.5981, 30.224),
            new Vector3(-537.8052, -854.9357, 28.2754),
            new Vector3(-526.7791, -1223.374, 17.4527),
            new Vector3(-1315.416, -834.431, 15.9523),
            new Vector3(-1314.466, -835.6913, 15.9523),
            new Vector3(-1205.378, -326.5286, 36.851),
            new Vector3(-1206.142, -325.0316, 36.851),
            new Vector3(147.4731, -1036.218, 28.3678),
            new Vector3(145.8392, -1035.625, 28.3678),
            new Vector3(-1205.378, -326.5286, 36.851),
            new Vector3(-1206.142, -325.0316, 36.851),
            new Vector3(147.4731, -1036.218, 28.3678),
            new Vector3(145.8392, -1035.625, 28.3678),
            new Vector3(-1109.797, -1690.808, 4.375014),
            new Vector3(-721.1284, -415.5296, 34.98175),
            new Vector3(130.1186, -1292.669, 29.26953),
            new Vector3(129.7023, -1291.954, 29.26953),
            new Vector3(129.2096, -1291.14, 29.26953),
            new Vector3(288.8256, -1282.364, 29.64128),
            new Vector3(1077.768, -776.4548, 58.23997),
            new Vector3(527.2687, -160.7156, 57.08937),
            new Vector3(-57.64693, -92.66162, 57.77995),
            new Vector3(527.3583, -160.6381, 57.0933),
            new Vector3(-165.1658, 234.8314, 94.92194),
            new Vector3(-165.1503, 232.7887, 94.92194),
            new Vector3(-1091.462, 2708.637, 18.95291),
            new Vector3(1172.492, 2702.492, 38.17477),
            new Vector3(1171.537, 2702.492, 38.17542),
            new Vector3(1822.637, 3683.131, 34.27678),
            new Vector3(1686.753, 4815.806, 42.00874),
            new Vector3(1701.209, 6426.569, 32.76408),
            new Vector3(-1091.42, 2708.629, 18.95568),
            new Vector3(-660.703, -853.971, 24.484),
            new Vector3(-660.703, -853.971, 24.484),
            new Vector3(-1409.782, -100.41, 52.387),
            new Vector3(-1410.279, -98.649, 52.436),
            new Vector3(-2975.014,380.129,14.99909),
            new Vector3(155.9642,6642.763,31.60284),
            new Vector3(174.1721,6637.943,31.57305),
            new Vector3(-97.33363,6455.411,31.46716),
            new Vector3(-95.49733,6457.243,31.46098),
            new Vector3(-303.2701,-829.7642,32.41727),
            new Vector3(-301.6767,-830.1,32.41727),
            new Vector3(-717.6539,-915.6808,19.21559),
            new Vector3(-1391.023, -590.3637, 30.31957),
            new Vector3(1138.311, -468.941, 66.73091),
            new Vector3(1167.086, -456.1151, 66.79015),
            new Vector3(-132.8289f, 6366.315f, 31.4737f),
            new Vector3(1735.206f, 6410.597f, 35.03722f),
            new Vector3(540.5155f, 2671.07f, 42.15653f),
            new Vector3(1968.255f, 3743.691f, 32.34375f),
            new Vector3(1702.953f, 4933.514f, 42.06368f),
            new Vector3(-133.399f, 6366.865f, 31.479f)
        };

        public static List<Vector3> CAR_LICENSE_CHECKPOINTS = new List<Vector3>()
        {
            new Vector3(-210.185f, 6332.839f, 30.42618f),
            new Vector3(-292.3593f, 6245.958f, 30.53763f),
            new Vector3(-357.4913f, 6301.83f, 28.99157f),
            new Vector3(-180.3696f, 6465.34f, 29.74923f),
            new Vector3(-126.9398f, 6431.469f, 30.57843f),
            new Vector3(-40.52503f, 6491.655f, 30.51457f),
            new Vector3(68.51f, 6600.582f, 30.50891f),
            new Vector3(136.5284f, 6538.029f, 30.57347f),
            new Vector3(-95.59881f, 6292.953f, 30.46639f),
            new Vector3(-162.9532f, 6351.154f, 30.58549f)
        };

        public static List<Vector3> BIKE_LICENSE_CHECKPOINTS = new List<Vector3>()
        {
            new Vector3(-210.185f, 6332.839f, 30.42618f),
            new Vector3(-292.3593f, 6245.958f, 30.53763f),
            new Vector3(-357.4913f, 6301.83f, 28.99157f),
            new Vector3(-180.3696f, 6465.34f, 29.74923f),
            new Vector3(-126.9398f, 6431.469f, 30.57843f),
            new Vector3(-40.52503f, 6491.655f, 30.51457f),
            new Vector3(68.51f, 6600.582f, 30.50891f),
            new Vector3(136.5284f, 6538.029f, 30.57347f),
            new Vector3(-95.59881f, 6292.953f, 30.46639f),
            new Vector3(-162.9532f, 6351.154f, 30.58549f)
        };

        public static List<Vector3> FISHING_POSITION_LIST = new List<Vector3>()
        {
            new Vector3(-273.9995f, 6642.273f, 7.39921f),
            new Vector3(-275.8697f, 6640.357f, 7.548759f),
            new Vector3(-278.201f, 6638.141f, 7.552301f),
            new Vector3(-280.1694f, 6636.17f, 7.552289f),
            new Vector3(-282.4903f, 6633.953f, 7.481426f),
            new Vector3(-284.8904f, 6631.555f, 7.339838f),
            new Vector3(-287.5817f, 6629.255f, 7.186343f),
            new Vector3(-287.5817f, 6629.255f, 7.186343f),
            new Vector3(-1865.597f, -1237.609f, 8.615782f),
            new Vector3(-1852.26f, -1248.748f, 8.615789f),
            new Vector3(-1848.879f, -1251.584f, 8.615788f),
            new Vector3(-1844.536f, -1255.231f, 8.615788f),
            new Vector3(-1841.201f, -1258.026f, 8.615788f),
            new Vector3(-1835.786f, -1262.564f, 8.615788f),
            new Vector3(-1831.772f, -1265.936f, 8.618261f),
            new Vector3(-1827.006f, -1269.947f, 8.618273f),
            new Vector3(-1865.597f, -1237.609f, 8.615782f)
        };

        public static List<Vector3> EQUIPMENT_POSITIONS = new List<Vector3>()
        {
            new Vector3(-450.0351f, 6016.23f, 31.71639f),
            new Vector3(1856.94f, 3689.781f, 34.26704f),
            new Vector3(451.7805f, -991.1909f, 30.68961f),

        };

        public static Dictionary<WeaponHash, string> WEAPON_ITEM_MODELS = new Dictionary<WeaponHash, string>()
        {
            { WeaponHash.AdvancedRifle, "w_ar_advancedrifle" }, { WeaponHash.APPistol, "w_pi_appistol" }, { WeaponHash.AssaultRifle, "w_ar_assaultrifle" },
            { WeaponHash.AssaultShotgun, "w_sg_assaultshotgun" }, { WeaponHash.AssaultSMG, "w_sb_assaultsmg" }, { WeaponHash.Ball, "w_am_baseball" },
            { WeaponHash.Bat, "w_me_bat" }, { WeaponHash.BattleAxe, "w_me_battleaxe" }, { WeaponHash.Bottle, "w_me_bottle" }, { WeaponHash.BullpupRifle, "w_ar_bullpuprifle" },
            { WeaponHash.BullpupShotgun, "w_sg_bullpupshotgun" }, { WeaponHash.BZGas, "w_ex_bzgas" }, { WeaponHash.CarbineRifle, "w_ar_carbinerifle" },
            { WeaponHash.CombatMG, "w_mg_combatmg" }, { WeaponHash.CombatPDW, "w_mg_combatpdw" }, { WeaponHash.CombatPistol, "w_pi_combatpistol" },
            { WeaponHash.CompactGrenadeLauncher, "w_lr_compactgrenadelauncher" }, { WeaponHash.CompactRifle, "w_ar_compactrifle" }, { WeaponHash.Crowbar, "w_me_crowbar" },
            { WeaponHash.Dagger, "w_me_dagger" }, { WeaponHash.DoubleBarrelShotgun, "w_sg_doublebarrelshotgun" }, { WeaponHash.FireExtinguisher, "w_am_fireextinguisher" },
            { WeaponHash.Firework, "w_lr_firework" }, { WeaponHash.Flare, "w_am_flare" }, { WeaponHash.FlareGun, "w_pi_flaregun" }, { WeaponHash.Flashlight, "w_me_flashlight" },
            { WeaponHash.GolfClub, "w_me_golfclub" }, { WeaponHash.Grenade, "w_ex_grenade" }, { WeaponHash.GrenadeLauncher, "w_lr_grenadelauncher" },
            { WeaponHash.GrenadeLauncherSmoke, "w_lr_grenadelaunchersmoke" }, { WeaponHash.Gusenberg, "w_sb_gusenberg" }, { WeaponHash.Hammer, "w_me_hammer" },
            { WeaponHash.Hatchet, "w_me_hatchet" }, { WeaponHash.HeavyPistol, "w_pi_heavypistol" }, { WeaponHash.HeavyShotgun, "w_sg_heavyshotgun" },
            { WeaponHash.HeavySniper, "w_sr_heavysniper" }, { WeaponHash.HomingLauncher, "w_lr_hominglauncher" }, { WeaponHash.Knife, "w_me_knife" },
            { WeaponHash.KnuckleDuster, "w_me_knuckleduster" }, { WeaponHash.Machete, "w_me_machete" }, { WeaponHash.MachinePistol, "w_pi_machinepistol" },
            { WeaponHash.MarksmanPistol, "w_pi_marksmanpistol" }, { WeaponHash.MarksmanRifle, "w_sr_marksmanrifle" }, { WeaponHash.MG, "w_mg_mg" }, { WeaponHash.MicroSMG, "w_sb_microsmg" },
            { WeaponHash.Minigun, "w_mg_minigun" }, { WeaponHash.MiniSMG, "w_sb_minismg" }, { WeaponHash.Molotov, "w_ex_molotov" }, { WeaponHash.Musket, "w_ar_musket" },
            { WeaponHash.Nightstick, "w_me_nightstick" }, { WeaponHash.NightVision, "w_am_nightvision" }, { WeaponHash.Parachute, "w_am_parachute" }, { WeaponHash.PetrolCan, "w_am_petrolcan" },
            { WeaponHash.PipeBomb, "w_ex_pipebomb" }, { WeaponHash.Pistol, "w_pi_pistol" }, { WeaponHash.Pistol50, "w_pi_pistol50" }, { WeaponHash.PoolCue, "w_me_poolcue" },
            { WeaponHash.ProximityMine, "w_ex_proximitymine" }, { WeaponHash.PumpShotgun, "w_sg_pumpshotgun" }, { WeaponHash.Railgun, "w_ar_railgun" }, { WeaponHash.Revolver, "w_pi_revolver" },
            { WeaponHash.RPG, "w_lr_rpg" }, { WeaponHash.SawnOffShotgun, "w_sg_sawnoffshotgun" }, { WeaponHash.SMG, "w_sb_smg" }, { WeaponHash.SmokeGrenade, "w_ex_smokegrenade" },
            { WeaponHash.SniperRifle, "w_sr_sniperrifle" }, { WeaponHash.Snowball, "w_am_snowball" }, { WeaponHash.SNSPistol, "w_pi_snspistol" },
            { WeaponHash.SpecialCarbine, "w_ar_specialcarbine" }, { WeaponHash.StickyBomb, "w_ex_stickybomb" }, { WeaponHash.StunGun, "w_pi_stungun" },
            { WeaponHash.SweeperShotgun, "w_sg_sweepershotgun" }, { WeaponHash.SwitchBlade, "w_me_switchblade" }, { WeaponHash.VintagePistol, "w_pi_vintagepistol" }, { WeaponHash.Wrench, "w_me_wrench" }
        };

        public enum AnimationFlags
        {
            Loop = 1 << 0,
            StopOnLastFrame = 1 << 1,
            OnlyAnimateUpperBody = 1 << 4,
            AllowPlayerControl = 1 << 5,
            Cancellable = 1 << 7
        };
    }
}
