-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: gtav
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `socialName` varchar(32) NOT NULL,
  `forumName` varchar(32) NOT NULL DEFAULT '',
  `password` varchar(64) NOT NULL,
  `status` int NOT NULL DEFAULT '0',
  `lastCharacter` int NOT NULL DEFAULT '-1',
  `lastIp` varchar(16) NOT NULL DEFAULT '',
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `retries` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`socialName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES ('Faktis','','532eaabd9574880dbf76b9b8cc00832c20a6ec113d682299550d7a6e0f345e25',0,17,'','2020-07-31 15:31:58',0);
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `admin`
--

DROP TABLE IF EXISTS `admin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admin` (
  `source` varchar(24) NOT NULL DEFAULT '',
  `target` varchar(24) NOT NULL DEFAULT '',
  `action` varchar(32) NOT NULL DEFAULT '',
  `time` int NOT NULL DEFAULT '0',
  `reason` varchar(150) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`source`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admin`
--

LOCK TABLES `admin` WRITE;
/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin` VALUES ('Faktis','Biggest Dickest','jail',1,'Lol','2020-07-15 00:53:37'),('Faktis','Biggest Dickest','jail',10,'cause','2020-07-15 00:54:36'),('Faktis','Biggest Dickest','jail',100,'haha','2020-07-15 00:55:37');
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `animations`
--

DROP TABLE IF EXISTS `animations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `animations` (
  `id` int NOT NULL AUTO_INCREMENT,
  `category` int DEFAULT NULL,
  `description` varchar(32) NOT NULL,
  `library` varchar(32) NOT NULL,
  `name` varchar(32) NOT NULL,
  `flag` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  KEY `Animation_Category` (`category`),
  CONSTRAINT `Animation_Category` FOREIGN KEY (`category`) REFERENCES `categories` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Server animations';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `animations`
--

LOCK TABLES `animations` WRITE;
/*!40000 ALTER TABLE `animations` DISABLE KEYS */;
/*!40000 ALTER TABLE `animations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `answers`
--

DROP TABLE IF EXISTS `answers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `answers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `question` int NOT NULL,
  `answer` text NOT NULL,
  `correct` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `answers`
--

LOCK TABLES `answers` WRITE;
/*!40000 ALTER TABLE `answers` DISABLE KEYS */;
INSERT INTO `answers` VALUES (1,1,'Yes',_binary ''),(2,1,'Nope. Just to ddos',_binary '\0'),(3,2,'Speed limit in populated areas is 80 Kmh',_binary ''),(4,2,'Speed limit is 150 in poplated areas',_binary '\0'),(5,2,'Speed limit is 100 in the whole of Los Santos',_binary '\0'),(6,3,'When operating a vehicle you should always drive on the right side of the road.',_binary ''),(7,3,'When operating a vehicle you can drive on both sides of the road',_binary '\0'),(8,3,'When operating a vehicle you should drive on the left side of the road.',_binary '\0'),(9,4,'If a emergency vehicle is behind you with sirens on you should pull over emediatly on the right side of the road.',_binary ''),(10,4,'If a emergency vehicle is behind you with sirens you should floor it to not slow them down.',_binary '\0'),(11,4,'If a emergency vehicle is behind you with sirens on you should stop in the middle of the road emediatly.',_binary '\0');
/*!40000 ALTER TABLE `answers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `applications`
--

DROP TABLE IF EXISTS `applications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applications` (
  `account` varchar(32) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `mistakes` int NOT NULL DEFAULT '0',
  `submission` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`account`,`submission`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applications`
--

LOCK TABLES `applications` WRITE;
/*!40000 ALTER TABLE `applications` DISABLE KEYS */;
INSERT INTO `applications` VALUES ('Aliliel',1,'2020-07-27 22:43:20'),('Aliliel',1,'2020-07-27 22:44:14'),('Aliliel',1,'2020-07-27 22:44:15'),('Aliliel',0,'2020-07-27 22:44:17'),('Aliliel',1,'2020-07-27 22:56:31'),('Aliliel',1,'2020-07-27 22:56:32'),('Aliliel',0,'2020-07-27 22:56:34'),('Arvent0',0,'2020-07-27 22:22:37'),('Arvent0',0,'2020-07-27 22:37:50'),('Arvent0',0,'2020-07-27 22:44:16'),('Arvent0',0,'2020-07-27 22:56:30'),('Deaderik',0,'2020-07-27 22:30:35'),('Deaderik',0,'2020-07-27 22:37:46'),('Deaderik',0,'2020-07-27 22:44:18'),('Deaderik',0,'2020-07-27 22:56:32'),('Faktis',0,'2020-07-26 21:46:36'),('Faktis',0,'2020-07-26 21:57:09'),('Faktis',0,'2020-07-26 22:20:49'),('Faktis',0,'2020-07-26 22:42:38'),('Faktis',0,'2020-07-26 23:03:58'),('Faktis',0,'2020-07-27 17:16:09'),('Faktis',0,'2020-07-27 17:24:49'),('Faktis',0,'2020-07-27 17:50:22'),('Faktis',0,'2020-07-27 17:54:41'),('Faktis',0,'2020-07-27 18:04:30'),('Faktis',0,'2020-07-27 18:07:16'),('Faktis',0,'2020-07-27 18:08:13'),('Faktis',0,'2020-07-27 18:13:53'),('Faktis',0,'2020-07-27 18:16:07'),('Faktis',0,'2020-07-27 18:21:08'),('Faktis',0,'2020-07-27 18:37:38'),('Faktis',0,'2020-07-27 22:36:53'),('Faktis',0,'2020-07-27 22:44:19'),('Faktis',0,'2020-07-27 22:56:34'),('Faktis',0,'2020-07-29 18:03:48'),('Faktis',0,'2020-07-29 18:06:57'),('Faktis',0,'2020-07-29 18:09:39'),('Faktis',0,'2020-07-29 18:59:09'),('Faktis',0,'2020-07-29 19:03:47'),('Faktis',0,'2020-07-29 19:08:47'),('Faktis',0,'2020-07-29 19:29:14'),('Faktis',0,'2020-07-29 19:33:18'),('Faktis',0,'2020-07-29 20:52:37'),('Faktis',0,'2020-07-29 20:56:37'),('Faktis',0,'2020-07-29 21:32:41'),('Faktis',0,'2020-07-29 22:57:58'),('Faktis',0,'2020-07-30 16:29:13'),('Faktis',0,'2020-07-30 17:01:38'),('Faktis',0,'2020-07-30 17:02:33'),('Faktis',0,'2020-07-30 17:08:06'),('Faktis',0,'2020-07-30 17:23:20'),('Faktis',0,'2020-07-30 18:00:40'),('Faktis',0,'2020-07-31 16:01:59'),('Faktis',0,'2020-07-31 16:20:36'),('Faktis',0,'2020-07-31 16:55:13'),('Faktis',0,'2020-07-31 17:03:14'),('OfficialGxbbs',0,'2020-07-29 21:02:49'),('OfficialGxbbs',0,'2020-07-29 21:32:40');
/*!40000 ALTER TABLE `applications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `blood`
--

DROP TABLE IF EXISTS `blood`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `blood` (
  `id` int NOT NULL AUTO_INCREMENT,
  `doctor` int NOT NULL,
  `patient` int NOT NULL,
  `bloodtype` varchar(8) NOT NULL,
  `used` bit(1) NOT NULL DEFAULT b'0',
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `blood`
--

LOCK TABLES `blood` WRITE;
/*!40000 ALTER TABLE `blood` DISABLE KEYS */;
INSERT INTO `blood` VALUES (1,17,18,'',_binary '\0','2020-07-15 01:34:28');
/*!40000 ALTER TABLE `blood` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `business`
--

DROP TABLE IF EXISTS `business`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `business` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` int NOT NULL DEFAULT '0',
  `ipl` varchar(64) DEFAULT NULL,
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int NOT NULL DEFAULT '0',
  `name` varchar(64) NOT NULL DEFAULT 'Negocio',
  `owner` varchar(32) NOT NULL DEFAULT '',
  `funds` int NOT NULL DEFAULT '0',
  `products` int NOT NULL DEFAULT '0',
  `multiplier` float NOT NULL DEFAULT '3',
  `locked` bit(1) NOT NULL DEFAULT b'0',
  `status` int NOT NULL DEFAULT '1',
  `price` int NOT NULL DEFAULT '1500000',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `business`
--

LOCK TABLES `business` WRITE;
/*!40000 ALTER TABLE `business` DISABLE KEYS */;
INSERT INTO `business` VALUES (57,20,'',-323.861,-133.026,37.6203,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(58,1,'',-366.993,-117.84,37.6962,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(59,2,'',-373.773,-116.759,37.6967,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(60,20,'',-370.679,-128.85,37.0626,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(61,1,'',-10.0358,-1087.85,25.6721,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(62,1,'',-18.2066,-1112.99,25.6721,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(63,24,'',-20.1468,-1118.5,25.8799,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(64,1,'',237.781,-381.258,43.7877,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(65,1,'',1352.51,4383.75,43.3438,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(66,29,'',-1399.94,-2303.23,12.9429,0,'Negocio','',0,0,3,_binary '\0',1,1500000);
/*!40000 ALTER TABLE `business` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `calls`
--

DROP TABLE IF EXISTS `calls`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `calls` (
  `phone` int NOT NULL,
  `target` int NOT NULL,
  `time` int NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`phone`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `calls`
--

LOCK TABLES `calls` WRITE;
/*!40000 ALTER TABLE `calls` DISABLE KEYS */;
/*!40000 ALTER TABLE `calls` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Animation categories';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `channels`
--

DROP TABLE IF EXISTS `channels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `channels` (
  `id` int NOT NULL AUTO_INCREMENT,
  `owner` int NOT NULL DEFAULT '0',
  `password` varchar(32) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `channels`
--

LOCK TABLES `channels` WRITE;
/*!40000 ALTER TABLE `channels` DISABLE KEYS */;
INSERT INTO `channels` VALUES (1,17,'c4ca4238a0b923820dcc509a6f75849b');
/*!40000 ALTER TABLE `channels` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `clothes`
--

DROP TABLE IF EXISTS `clothes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clothes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `player` int NOT NULL DEFAULT '0',
  `type` int NOT NULL DEFAULT '0',
  `slot` int NOT NULL DEFAULT '0',
  `drawable` int NOT NULL DEFAULT '0',
  `texture` int NOT NULL DEFAULT '0',
  `dressed` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clothes`
--

LOCK TABLES `clothes` WRITE;
/*!40000 ALTER TABLE `clothes` DISABLE KEYS */;
INSERT INTO `clothes` VALUES (1,18,0,1,95,0,_binary '\0'),(2,18,0,9,12,0,_binary ''),(3,17,0,1,3,0,_binary '\0'),(4,17,0,4,5,2,_binary ''),(5,18,0,11,262,0,_binary '\0'),(6,17,0,11,3,2,_binary ''),(7,18,0,3,96,0,_binary '\0'),(8,17,0,7,112,0,_binary ''),(9,17,0,5,41,0,_binary ''),(10,17,0,3,2,0,_binary '\0'),(11,17,0,3,8,0,_binary ''),(12,18,0,1,50,4,_binary ''),(13,18,0,11,193,0,_binary ''),(14,18,0,3,5,0,_binary ''),(15,18,1,0,131,0,_binary ''),(16,18,0,4,100,0,_binary ''),(17,18,0,6,6,0,_binary '');
/*!40000 ALTER TABLE `clothes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contacts`
--

DROP TABLE IF EXISTS `contacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `contacts` (
  `id` int NOT NULL AUTO_INCREMENT,
  `owner` int NOT NULL,
  `contactNumber` int NOT NULL,
  `contactName` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contacts`
--

LOCK TABLES `contacts` WRITE;
/*!40000 ALTER TABLE `contacts` DISABLE KEYS */;
INSERT INTO `contacts` VALUES (1,479733,479733,'Mine');
/*!40000 ALTER TABLE `contacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `controls`
--

DROP TABLE IF EXISTS `controls`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `controls` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(24) NOT NULL DEFAULT '',
  `item` int NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `rotation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `controls`
--

LOCK TABLES `controls` WRITE;
/*!40000 ALTER TABLE `controls` DISABLE KEYS */;
/*!40000 ALTER TABLE `controls` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `crimes`
--

DROP TABLE IF EXISTS `crimes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `crimes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `description` varchar(256) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `jail` int NOT NULL DEFAULT '0',
  `fine` int NOT NULL DEFAULT '0',
  `reminder` varchar(128) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `crimes`
--

LOCK TABLES `crimes` WRITE;
/*!40000 ALTER TABLE `crimes` DISABLE KEYS */;
/*!40000 ALTER TABLE `crimes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dealers`
--

DROP TABLE IF EXISTS `dealers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dealers` (
  `vehicleHash` varchar(24) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
  `dealerId` int NOT NULL,
  `vehicleType` int NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`vehicleHash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dealers`
--

LOCK TABLES `dealers` WRITE;
/*!40000 ALTER TABLE `dealers` DISABLE KEYS */;
INSERT INTO `dealers` VALUES ('adder',0,5,310000),('akuma',1,8,71000),('alpha',0,5,35600),('asea',0,1,7800),('asterope',0,1,15000),('avarus',1,8,55000),('bagger',1,8,25000),('baller',0,2,35000),('baller2',0,2,39000),('baller3',0,2,41000),('banshee',0,5,89000),('banshee2',0,5,122000),('bati',1,8,57000),('bati2',1,8,59000),('benson',0,2,76000),('bestiagts',0,5,96000),('bf400',1,8,47000),('bfinjection',0,9,36000),('bison',0,2,31000),('bjxl',0,2,18000),('blade',0,4,8000),('blazer',1,9,23000),('blazer2',1,9,16000),('blazer3',1,9,21000),('blazer4',1,8,27000),('blista',0,0,10000),('blista2',0,0,6000),('blista3',0,0,6300),('bmx',1,8,600),('bodhi2',0,9,23000),('brawler',0,9,52600),('brioso',0,0,15000),('buccaneer',0,4,14000),('buccaneer2',0,4,35000),('buffalo',0,5,45000),('buffalo2',0,5,49000),('bullet',0,5,140000),('burrito3',0,2,35000),('camper',0,2,32000),('carbonizzare',0,5,115000),('carbonrs',1,8,46000),('casco',0,5,152000),('cavalcade',0,2,16200),('cavalcade2',0,2,15900),('cheetah',0,5,260000),('cheetah2',0,5,197000),('chimera',1,8,31800),('chino',0,4,16000),('chino2',0,4,38000),('cliffhanger',1,8,38000),('cog55',0,1,55000),('cogcabrio',0,3,39000),('cognoscenti',0,1,65000),('comet2',0,5,96500),('comet3',0,5,120000),('contender',0,2,43200),('coquette',0,5,115000),('coquette2',0,5,139000),('coquette3',0,5,108500),('cruiser',1,8,350),('cyclone',0,5,375000),('daemon',1,8,10000),('daemon2',1,8,12500),('defiler',1,8,61900),('diablous',1,8,45000),('diablous2',1,8,40000),('dilettante',0,0,7000),('Dinghy',2,14,25000),('Dinghy2',2,14,32000),('Dinghy3',2,14,40000),('Dinghy4',2,14,55000),('dloader',0,9,40000),('dominator',0,4,30000),('dominator2',0,4,33000),('double',1,8,48000),('dubsta',0,2,37600),('dubsta2',0,2,46000),('dubsta3',0,2,59000),('dukes',0,4,15000),('elegy',0,5,79900),('elegy2',0,5,97000),('emperor',0,1,6400),('emperor2',0,1,3100),('enduro',1,8,7000),('entityxf',0,5,325000),('esskey',1,8,22400),('exemplar',0,3,45000),('f620',0,3,46300),('faction',0,4,13000),('faction2',0,4,35200),('faction3',0,4,42500),('faggio',1,8,2100),('faggio2',1,8,1500),('faggio3',1,8,2000),('fcr',1,8,27000),('fcr2',1,8,32000),('felon',0,3,42000),('felon2',0,3,42500),('feltzer2',0,5,104000),('feltzer3',0,5,147000),('fixter',1,8,620),('fmj',0,5,347000),('fq2',0,2,38800),('fugitive',0,1,14400),('furoregt',0,5,93000),('fusilade',0,5,41000),('futo',0,5,18700),('gargoyle',1,8,46700),('gauntlet',0,4,28000),('gauntlet2',0,4,36000),('gburrito',0,2,36000),('gburrito2',0,2,32000),('glendale',0,1,12000),('gp1',0,5,245000),('granger',0,2,25000),('gresley',0,2,26000),('guardian',0,2,84000),('habanero',0,2,13900),('hakuchou',1,8,72000),('hakuchou2',1,8,120000),('hexer',1,8,12500),('hotknife',0,4,32000),('huntley',0,2,43100),('infernus',0,5,135000),('infernus2',0,5,169000),('ingot',0,1,5600),('innovation',1,8,16000),('intruder',0,1,7600),('issi2',0,0,8000),('italigtb',0,5,330000),('italigtb2',0,5,355000),('jackal',0,3,43200),('jester',0,5,107000),('Jetmax',2,14,80000),('journey',0,2,21000),('kalahari',0,9,16200),('khamelion',0,5,86000),('kuruma',0,5,65000),('landstalker',0,2,22000),('lectro',1,8,31800),('lynx',0,5,109000),('mamba',0,5,146000),('manchez',1,8,19000),('Marquis',2,14,60000),('massacro',0,5,106500),('mesa',0,9,17200),('mesa3',0,9,29500),('monroe',0,5,145000),('moonbeam',0,4,9000),('moonbeam2',0,4,36000),('mule3',0,2,62000),('nemesis',1,8,29700),('nero',0,5,380000),('nero2',0,5,392000),('nightblade',1,8,35700),('nightshade',0,4,33000),('ninef',0,5,112500),('ninef2',0,5,110000),('omnis',0,5,89000),('oracle',0,3,28000),('oracle2',0,3,38000),('osiris',0,5,341000),('panto',0,0,4500),('patriot',0,2,26700),('pcj',1,8,33000),('penetrator',0,5,192000),('penumbra',0,5,19700),('peyote',0,1,30000),('pfister811',0,5,335000),('phoenix',0,4,20900),('picador',0,4,23000),('pony',0,2,21000),('prairie',0,0,11000),('premier',0,1,11100),('primo',0,1,13600),('primo2',0,1,46400),('prototipo',0,5,800000),('radi',0,2,22000),('rancherxl',0,9,15200),('rapidgt',0,5,34600),('rapidgt2',0,5,35100),('rapidgt3',0,5,57000),('ratbike',1,8,2500),('ratloader',0,4,3000),('ratloader2',0,4,5000),('reaper',0,5,337000),('rebel',0,9,2700),('rebel2',0,9,10000),('regina',0,1,4600),('retinue',0,5,38000),('rhapsody',0,0,1500),('rocoto',0,2,31000),('ruffian',1,8,32300),('ruiner',0,4,23000),('rumpo',0,2,35000),('rumpo3',0,2,90000),('ruston',0,5,69600),('sabregt',0,4,15500),('sabregt2',0,4,29500),('sadler',0,4,10000),('sanchez',1,8,13100),('sanchez2',1,8,23000),('sanctus',1,8,95000),('sandking',0,9,21000),('sandking2',0,9,20100),('schafter2',0,5,42200),('schafter3',0,5,53200),('schwarzer',0,5,48000),('scorcher',1,8,500),('Seashark',2,14,15000),('Seashark3',2,14,35000),('seminole',0,2,23700),('sentinel',0,3,25000),('sentinel2',0,3,30000),('serrano',0,2,22600),('seven70',0,5,122000),('sheava',0,5,368000),('slamvan',0,4,11000),('slamvan2',0,4,15000),('slamvan3',0,4,35300),('sovereign',1,8,42300),('specter',0,5,106000),('specter2',0,5,129000),('Speeder',2,14,72500),('Speeder2',2,14,90000),('speedo',0,2,23000),('Squalo',2,14,55000),('stalion',0,4,8700),('stalion2',0,4,38000),('stanier',0,1,9700),('stinger',0,5,149000),('stingergt',0,5,151000),('stratum',0,1,13000),('stretch',0,1,95000),('sultan',0,5,45000),('sultanrs',0,5,110000),('Suntrap',2,14,30000),('superd',0,1,55000),('surano',0,5,63000),('surfer',0,2,15000),('surfer2',0,2,10000),('surge',0,1,13700),('t20',0,5,385000),('tailgater',0,1,33000),('tampa',0,4,19000),('tempesta',0,5,340000),('thrust',1,8,24500),('Toro',2,14,90000),('Toro2',2,14,120000),('tribike',1,8,900),('Tropic',2,14,50000),('Tropic2',2,14,60000),('tropos',0,5,56000),('Tug',2,14,175000),('turismo2',0,5,260000),('turismor',0,5,270000),('vacca',0,5,198000),('vader',1,8,28100),('vagner',0,5,395000),('verlierer2',0,5,95000),('vigero',0,4,10600),('vindicator',1,8,39000),('virgo',0,4,14700),('virgo2',0,4,37900),('virgo3',0,4,45000),('visione',0,5,390000),('voltic',0,5,132000),('voodoo',0,4,32600),('voodoo2',0,4,25000),('vortex',1,8,55560),('warrener',0,1,7200),('washington',0,1,6800),('windsor',0,3,55000),('windsor2',0,3,63000),('wolfsbane',1,8,27600),('xa21',0,5,389000),('xls',0,2,42500),('zentorno',0,5,295000),('zion',0,3,25000),('zion2',0,3,32000),('zombiea',1,8,24900),('zombieb',1,8,26100),('ztype',0,5,55000);
/*!40000 ALTER TABLE `dealers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `dealers2`
--

DROP TABLE IF EXISTS `dealers2`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dealers2` (
  `vehicleHash` varchar(24) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
  `dealerId` int NOT NULL,
  `vehicleType` int NOT NULL,
  `price` int NOT NULL,
  PRIMARY KEY (`vehicleHash`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_spanish_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `dealers2`
--

LOCK TABLES `dealers2` WRITE;
/*!40000 ALTER TABLE `dealers2` DISABLE KEYS */;
INSERT INTO `dealers2` VALUES ('adder',4,5,310000),('akuma',5,8,71000),('alpha',4,5,35600),('asea',4,1,7800),('asterope',4,1,15000),('avarus',5,8,55000),('bagger',5,8,25000),('baller',4,2,35000),('baller2',4,2,39000),('baller3',4,2,41000),('banshee',4,5,89000),('banshee2',4,5,122000),('bati',5,8,57000),('bati2',5,8,59000),('benson',4,2,76000),('bestiagts',4,5,96000),('bf400',5,8,47000),('bfinjection',4,9,36000),('bison',4,2,31000),('bjxl',4,2,18000),('blade',4,4,8000),('blazer',5,9,23000),('blazer2',5,9,16000),('blazer3',5,9,21000),('blazer4',5,8,27000),('blista',4,0,10000),('blista2',4,0,6000),('blista3',4,0,6300),('bmx',5,8,600),('bodhi2',4,9,23000),('brawler',4,9,52600),('brioso',4,0,15000),('buccaneer',4,4,14000),('buccaneer2',4,4,35000),('buffalo',4,5,45000),('buffalo2',4,5,49000),('bullet',4,5,140000),('burrito3',4,2,35000),('camper',4,2,32000),('carbonizzare',4,5,115000),('carbonrs',5,8,46000),('casco',4,5,152000),('cavalcade',4,2,16200),('cavalcade2',4,2,15900),('cheetah',4,5,260000),('cheetah2',4,5,197000),('chimera',5,8,31800),('chino',4,4,16000),('chino2',4,4,38000),('cliffhanger',5,8,38000),('cog55',4,1,55000),('cogcabrio',4,3,39000),('cognoscenti',4,1,65000),('comet2',4,5,96500),('comet3',4,5,120000),('contender',4,2,43200),('coquette',4,5,115000),('coquette2',4,5,139000),('coquette3',4,5,108500),('cruiser',5,8,350),('cyclone',4,5,375000),('daemon',5,8,10000),('daemon2',5,8,12500),('defiler',5,8,61900),('diablous',5,8,45000),('diablous2',5,8,40000),('dilettante',4,0,7000),('Dinghy',3,14,25000),('Dinghy2',3,14,32000),('Dinghy3',3,14,40000),('Dinghy4',3,14,55000),('dloader',4,9,40000),('dominator',4,4,30000),('dominator2',4,4,33000),('double',5,8,48000),('dubsta',4,2,37600),('dubsta2',4,2,46000),('dubsta3',4,2,59000),('dukes',4,4,15000),('elegy',4,5,79900),('elegy2',4,5,97000),('emperor',4,1,6400),('emperor2',4,1,3100),('enduro',5,8,7000),('entityxf',4,5,325000),('esskey',5,8,22400),('exemplar',4,3,45000),('f620',4,3,46300),('faction',4,4,13000),('faction2',4,4,35200),('faction3',4,4,42500),('faggio',5,8,2100),('faggio2',5,8,1500),('faggio3',5,8,2000),('fcr',5,8,27000),('fcr2',5,8,32000),('felon',4,3,42000),('felon2',4,3,42500),('feltzer2',4,5,104000),('feltzer3',4,5,147000),('fixter',5,8,620),('fmj',4,5,347000),('fq2',4,2,38800),('fugitive',4,1,14400),('furoregt',4,5,93000),('fusilade',4,5,41000),('futo',4,5,18700),('gargoyle',5,8,46700),('gauntlet',4,4,28000),('gauntlet2',4,4,36000),('gburrito',4,2,36000),('gburrito2',4,2,32000),('glendale',4,1,12000),('gp1',4,5,245000),('granger',4,2,25000),('gresley',4,2,26000),('guardian',4,2,84000),('habanero',4,2,13900),('hakuchou',5,8,72000),('hakuchou2',5,8,120000),('hexer',5,8,12500),('hotknife',4,4,32000),('huntley',4,2,43100),('infernus',4,5,135000),('infernus2',4,5,169000),('ingot',4,1,5600),('innovation',5,8,16000),('intruder',4,1,7600),('issi2',4,0,8000),('italigtb',4,5,330000),('italigtb2',4,5,355000),('jackal',4,3,43200),('jester',4,5,107000),('Jetmax',3,14,80000),('journey',4,2,21000),('kalahari',4,9,16200),('khamelion',4,5,86000),('kuruma',4,5,65000),('landstalker',4,2,22000),('lectro',5,8,31800),('lynx',4,5,109000),('mamba',4,5,146000),('manchez',5,8,19000),('Marquis',3,14,60000),('massacro',4,5,106500),('mesa',4,9,17200),('mesa3',4,9,29500),('monroe',4,5,145000),('moonbeam',4,4,9000),('moonbeam2',4,4,36000),('mule3',4,2,62000),('nemesis',5,8,29700),('nero',4,5,380000),('nero2',4,5,392000),('nightblade',5,8,35700),('nightshade',4,4,33000),('ninef',4,5,112500),('ninef2',4,5,110000),('omnis',4,5,89000),('oracle',4,3,28000),('oracle2',4,3,38000),('osiris',4,5,341000),('panto',4,0,4500),('patriot',4,2,26700),('pcj',5,8,33000),('penetrator',4,5,192000),('penumbra',4,5,19700),('peyote',4,1,30000),('pfister811',4,5,335000),('phoenix',4,4,20900),('picador',4,4,23000),('pony',4,2,21000),('prairie',4,0,11000),('premier',4,1,11100),('primo',4,1,13600),('primo2',4,1,46400),('prototipo',4,5,800000),('radi',4,2,22000),('rancherxl',4,9,15200),('rapidgt',4,5,34600),('rapidgt2',4,5,35100),('rapidgt3',4,5,57000),('ratbike',5,8,2500),('ratloader',4,4,3000),('ratloader2',4,4,5000),('reaper',4,5,337000),('rebel',4,9,2700),('rebel2',4,9,10000),('regina',4,1,4600),('retinue',4,5,38000),('rhapsody',4,0,1500),('rocoto',4,2,31000),('ruffian',5,8,32300),('ruiner',4,4,23000),('rumpo',4,2,35000),('rumpo3',4,2,90000),('ruston',4,5,69600),('sabregt',4,4,15500),('sabregt2',4,4,29500),('sadler',4,4,10000),('sanchez',5,8,13100),('sanchez2',5,8,23000),('sanctus',5,8,95000),('sandking',4,9,21000),('sandking2',4,9,20100),('schafter2',4,5,42200),('schafter3',4,5,53200),('schwarzer',4,5,48000),('scorcher',5,8,500),('Seashark',3,14,15000),('Seashark3',3,14,35000),('seminole',4,2,23700),('sentinel',4,3,25000),('sentinel2',4,3,30000),('serrano',4,2,22600),('seven70',4,5,122000),('sheava',4,5,368000),('slamvan',4,4,11000),('slamvan2',4,4,15000),('slamvan3',4,4,35300),('sovereign',5,8,42300),('specter',4,5,106000),('specter2',4,5,129000),('Speeder',3,14,72500),('Speeder2',3,14,90000),('speedo',4,2,23000),('Squalo',3,14,55000),('stalion',4,4,8700),('stalion2',4,4,38000),('stanier',4,1,9700),('stinger',4,5,149000),('stingergt',4,5,151000),('stratum',4,1,13000),('stretch',4,1,95000),('sultan',4,5,45000),('sultanrs',4,5,110000),('Suntrap',3,14,30000),('superd',4,1,55000),('surano',4,5,63000),('surfer',4,2,15000),('surfer2',4,2,10000),('surge',4,1,13700),('t20',4,5,385000),('tailgater',4,1,33000),('tampa',4,4,19000),('tempesta',4,5,340000),('thrust',5,8,24500),('Toro',3,14,90000),('Toro2',3,14,120000),('tribike',5,8,900),('Tropic',3,14,50000),('Tropic2',3,14,60000),('tropos',4,5,56000),('Tug',3,14,175000),('turismo2',4,5,260000),('turismor',4,5,270000),('vacca',4,5,198000),('vader',5,8,28100),('vagner',4,5,395000),('verlierer2',4,5,95000),('vigero',4,4,10600),('vindicator',5,8,39000),('virgo',4,4,14700),('virgo2',4,4,37900),('virgo3',4,4,45000),('visione',4,5,390000),('voltic',4,5,132000),('voodoo',4,4,32600),('voodoo2',4,4,25000),('vortex',5,8,55560),('warrener',4,1,7200),('washington',4,1,6800),('windsor',4,3,55000),('windsor2',4,3,63000),('wolfsbane',5,8,27600),('xa21',4,5,389000),('xls',4,2,42500),('zentorno',4,5,295000),('zion',4,3,25000),('zion2',4,3,32000),('zombiea',5,8,24900),('zombieb',5,8,26100),('ztype',4,5,55000);
/*!40000 ALTER TABLE `dealers2` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fines`
--

DROP TABLE IF EXISTS `fines`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `fines` (
  `officer` varchar(32) NOT NULL DEFAULT '',
  `target` varchar(32) NOT NULL DEFAULT '',
  `amount` int NOT NULL DEFAULT '0',
  `reason` varchar(128) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`officer`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fines`
--

LOCK TABLES `fines` WRITE;
/*!40000 ALTER TABLE `fines` DISABLE KEYS */;
INSERT INTO `fines` VALUES ('Test Testy','Biggest Dickest',100,'test','2020-07-15 01:17:11');
/*!40000 ALTER TABLE `fines` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `furniture`
--

DROP TABLE IF EXISTS `furniture`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `furniture` (
  `id` int NOT NULL AUTO_INCREMENT,
  `hash` int NOT NULL DEFAULT '0',
  `house` int NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `rotation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `furniture`
--

LOCK TABLES `furniture` WRITE;
/*!40000 ALTER TABLE `furniture` DISABLE KEYS */;
/*!40000 ALTER TABLE `furniture` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hotwires`
--

DROP TABLE IF EXISTS `hotwires`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hotwires` (
  `vehicle` int NOT NULL,
  `player` varchar(24) NOT NULL DEFAULT '',
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`player`,`vehicle`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hotwires`
--

LOCK TABLES `hotwires` WRITE;
/*!40000 ALTER TABLE `hotwires` DISABLE KEYS */;
/*!40000 ALTER TABLE `hotwires` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `houses`
--

DROP TABLE IF EXISTS `houses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `houses` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ipl` varchar(32) NOT NULL DEFAULT '',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int NOT NULL DEFAULT '0',
  `name` varchar(32) NOT NULL DEFAULT 'Casa',
  `price` int NOT NULL DEFAULT '10000',
  `owner` varchar(32) NOT NULL DEFAULT '',
  `status` int NOT NULL DEFAULT '2',
  `tenants` int NOT NULL DEFAULT '0',
  `rental` int NOT NULL DEFAULT '0',
  `locked` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `houses`
--

LOCK TABLES `houses` WRITE;
/*!40000 ALTER TABLE `houses` DISABLE KEYS */;
/*!40000 ALTER TABLE `houses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `interiors`
--

DROP TABLE IF EXISTS `interiors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `interiors` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(64) NOT NULL DEFAULT '',
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `dimension` int NOT NULL DEFAULT '0',
  `type` int NOT NULL DEFAULT '0',
  `blip` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `interiors`
--

LOCK TABLES `interiors` WRITE;
/*!40000 ALTER TABLE `interiors` DISABLE KEYS */;
/*!40000 ALTER TABLE `interiors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `items`
--

DROP TABLE IF EXISTS `items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `items` (
  `id` int NOT NULL AUTO_INCREMENT,
  `hash` varchar(32) NOT NULL DEFAULT '',
  `ownerEntity` varchar(16) NOT NULL DEFAULT '',
  `ownerIdentifier` int NOT NULL DEFAULT '0',
  `amount` int NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int NOT NULL DEFAULT '0',
  `quality` varchar(45) DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=196 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `items`
--

LOCK TABLES `items` WRITE;
/*!40000 ALTER TABLE `items` DISABLE KEYS */;
INSERT INTO `items` VALUES (143,'3696781377','Player',17,1,0,0,0,0,''),(144,'Products','Player',17,72250,0,0,0,0,''),(145,'Products','Player',29,93200,0,0,0,0,''),(146,'Products','Player',28,36200,0,0,0,0,''),(147,'AssaultRifle','Wheel',17,10000,0,0,0,0,''),(148,'AssaultRifle','Wheel',28,10000,0,0,0,0,''),(149,'AssaultRifle','Wheel',29,10000,0,0,0,0,''),(152,'copper','Player',17,1,0,0,0,0,''),(153,'copper','Player',17,1,0,0,0,0,''),(154,'copper','Player',17,1,0,0,0,0,''),(155,'2384362703','Player',17,1,0,0,0,0,''),(156,'Bait','Player',17,24,0,0,0,0,''),(157,'Fish','Player',17,1663,0,0,0,0,''),(160,'mauretic_acid','Player',17,38,0,0,0,0,''),(162,'meth','Player',17,18,0,0,0,0,''),(165,'sodium','Player',17,7,0,0,0,0,''),(195,'cocain','Player',17,4,0,0,0,0,'Poor');
/*!40000 ALTER TABLE `items` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `licensed`
--

DROP TABLE IF EXISTS `licensed`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `licensed` (
  `item` int NOT NULL DEFAULT '0',
  `buyer` varchar(24) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`item`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `licensed`
--

LOCK TABLES `licensed` WRITE;
/*!40000 ALTER TABLE `licensed` DISABLE KEYS */;
INSERT INTO `licensed` VALUES (8,'Test Testy','2020-07-15 00:47:57'),(9,'Test Testy','2020-07-15 00:47:57'),(10,'Test Testy','2020-07-15 00:49:43'),(11,'Test Testy','2020-07-15 00:50:36'),(13,'Biggest Dickest','2020-07-15 01:02:05'),(14,'Biggest Dickest','2020-07-15 01:02:05'),(15,'Biggest Dickest','2020-07-15 01:04:09'),(25,'Biggest Dickest','2020-07-15 01:12:21'),(26,'Biggest Dickest','2020-07-15 01:12:21'),(27,'Biggest Dickest','2020-07-15 01:14:10'),(28,'Test Testy','2020-07-15 01:14:20'),(62,'Test Testy','2020-07-16 16:51:30'),(63,'Test Testy','2020-07-16 16:51:30'),(129,'Jon Dillion','2020-07-22 17:23:44'),(130,'Jon Dillion','2020-07-22 17:23:46'),(131,'Jon Dillion','2020-07-22 17:24:41'),(134,'Biggest Dickest','2020-07-25 14:13:55'),(135,'Biggest Dickest','2020-07-25 14:13:55');
/*!40000 ALTER TABLE `licensed` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `messages`
--

DROP TABLE IF EXISTS `messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `messages` (
  `id` int NOT NULL AUTO_INCREMENT,
  `senderNumber` int NOT NULL DEFAULT '0',
  `receiverNumber` int NOT NULL DEFAULT '0',
  `message` text NOT NULL,
  `deleted` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `messages`
--

LOCK TABLES `messages` WRITE;
/*!40000 ALTER TABLE `messages` DISABLE KEYS */;
/*!40000 ALTER TABLE `messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `money`
--

DROP TABLE IF EXISTS `money`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `money` (
  `source` varchar(32) NOT NULL,
  `receiver` varchar(32) NOT NULL,
  `type` varchar(32) NOT NULL,
  `amount` int NOT NULL DEFAULT '0',
  `date` date NOT NULL,
  `hour` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `money`
--

LOCK TABLES `money` WRITE;
/*!40000 ALTER TABLE `money` DISABLE KEYS */;
INSERT INTO `money` VALUES ('Payday','(( 1 ))','Payday',579,'2020-04-01','15:05:11'),('Payday','(( 1 ))','Payday',579,'2020-04-01','16:08:27'),('Payday','(( 1 ))','Payday',579,'2020-04-01','17:04:10'),('Payday','(( 0 ))','Payday',1265,'2020-04-01','18:23:52'),('Payday','Mateo Armostrong','Payday',3405,'2020-04-01','18:37:30'),('Mateo Armostrong','Tony Devachy','Hooker service',100,'2020-04-01','19:39:13'),('Mateo Armostrong','Tony Devachy','Payment between players',55,'2020-04-01','20:01:57'),('Payday','Tony Devachy','Payday',1266,'2020-04-01','20:15:54'),('Payday','Mateo Armostrong','Payday',3468,'2020-04-01','20:28:13'),('Payday','0','Payday',579,'2020-04-03','17:17:36'),('Payday','Matt Armstrong','Payday',579,'2020-04-04','23:48:58'),('Payday','T E','Payday',9843,'2020-04-05','01:06:16'),('Payday','Matt Armstrong','Payday',580,'2020-04-05','02:44:27'),('Payday','Matt Armstrong','Payday',640,'2020-04-05','15:02:56'),('Payday','Matt Armstrong','Payday',701,'2020-04-05','16:10:29'),('Payday','Matt Armstrong','Payday',702,'2020-04-05','17:53:11'),('Payday','Matt Armstrong','Payday',732,'2020-04-05','18:53:11'),('Payday','Matt Armstrong','Payday',733,'2020-04-05','19:54:36'),('Payday','Matt Armstrong','Payday',730,'2020-04-05','22:18:17'),('Payday','Matt Armstrong','Payday',761,'2020-04-06','00:44:49'),('Payday','Matt Armstrong','Payday',761,'2020-04-06','01:58:13'),('Payday','Matt Armstrong','Payday',762,'2020-04-06','04:08:32'),('Payday','Matt Armstrong','Payday',763,'2020-04-06','05:50:52'),('Payday','Matt Armstrong','Payday',794,'2020-04-06','06:56:27'),('Payday','Matt Armstrong','Payday',795,'2020-04-06','07:56:27'),('Payday','Matt Armstrong','Payday',795,'2020-04-06','08:56:27'),('Payday','Matt Armstrong','Payday',796,'2020-04-06','09:56:27'),('Payday','Matt Armstrong','Payday',797,'2020-04-06','10:56:27'),('Payday','Matt Armstrong','Payday',828,'2020-04-06','11:56:27'),('Payday','Matt Armstrong','Payday',829,'2020-04-06','12:56:27'),('Payday','Matt Armstrong','Payday',829,'2020-04-06','13:56:27'),('Payday','Matt Armstrong','Payday',830,'2020-04-06','15:00:45'),('Payday','Matt Armstrong','Payday',831,'2020-04-06','16:11:29'),('Payday','Matt Armstrong','Payday',862,'2020-04-06','17:17:57'),('Payday','Matt Armstrong','Payday',845,'2020-04-06','18:35:52'),('Payday','T E','Payday',9853,'2020-04-06','19:41:10'),('Payday','Matt Armstrong','Payday',846,'2020-04-06','19:42:10'),('Payday','T E','Payday',9863,'2020-04-06','20:47:34'),('Payday','Matt Armstrong','Payday',846,'2020-04-06','21:02:54'),('Payday','Matt Armstrong','Payday',846,'2020-04-06','22:11:13'),('Payday','Matt Armstrong','Payday',847,'2020-04-06','23:27:35'),('Payday','T E','Payday',9933,'2020-04-07','00:00:51'),('Payday','Matt Armstrong','Payday',848,'2020-04-07','01:43:59'),('Payday','Matt Armstrong','Payday',274,'2020-04-07','04:00:26'),('Payday','T E','Payday',10037,'2020-04-07','04:26:34'),('Payday','Matt Armstrong','Payday',304,'2020-04-07','05:15:53'),('Payday','T E','Payday',10622,'2020-04-07','05:41:23'),('Payday','Matt Armstrong','Payday',880,'2020-04-07','06:19:23'),('Payday','Matt Armstrong','Payday',880,'2020-04-07','07:19:23'),('Payday','Matt Armstrong','Payday',881,'2020-04-07','08:19:23'),('Payday','Matt Armstrong','Payday',882,'2020-04-07','09:19:23'),('Payday','Matt Armstrong','Payday',883,'2020-04-07','10:19:23'),('Payday','Matt Armstrong','Payday',884,'2020-04-07','11:19:23'),('Payday','Matt Armstrong','Payday',885,'2020-04-07','12:19:23'),('Payday','Matt Armstrong','Payday',886,'2020-04-07','13:19:23'),('Payday','Tom Winters','Payday',4306,'2020-04-07','15:51:37'),('Payday','Matt Armstrong','Payday',312,'2020-04-07','15:58:37'),('Payday','Matt Armstrong','Payday',312,'2020-04-07','17:09:21'),('Payday','Matt Armstrong','Payday',312,'2020-04-07','18:28:24'),('Payday','T E','Payday',10088,'2020-04-07','19:24:03'),('Payday','Matt Armstrong','Payday',343,'2020-04-07','19:32:03'),('Payday','T E','Payday',10098,'2020-04-07','20:28:47'),('Payday','Matt Armstrong','Payday',343,'2020-04-07','20:42:55'),('Payday','Matt Armstrong','Payday',343,'2020-04-07','22:02:57'),('Payday','Matt Armstrong','Payday',344,'2020-04-07','23:16:28'),('Payday','Matt Armstrong','Payday',344,'2020-04-08','01:33:28'),('Payday','Matt Armstrong','Payday',344,'2020-04-08','04:03:12'),('Payday','Matt Armstrong','Payday',345,'2020-04-08','05:21:43'),('Payday','Matt Armstrong','Payday',345,'2020-04-08','06:37:16'),('Payday','Matt Armstrong','Payday',345,'2020-04-08','07:37:16'),('Payday','Matt Armstrong','Payday',346,'2020-04-08','08:37:16'),('Payday','Matt Armstrong','Payday',346,'2020-04-08','09:37:16'),('Payday','Matt Armstrong','Payday',346,'2020-04-08','10:37:16'),('Payday','Matt Armstrong','Payday',347,'2020-04-08','11:37:16'),('Payday','Matt Armstrong','Payday',347,'2020-04-08','12:37:16'),('Payday','Matt Armstrong','Payday',347,'2020-04-08','13:37:16'),('Payday','Matt Armstrong','Payday',378,'2020-04-08','14:37:16'),('Payday','Test Testy','Payday',609,'2020-07-14','21:59:59'),('Payday','Test Testy','Payday',939,'2020-07-14','22:59:59'),('Payday','Test Testy','Payday',95,'2020-07-15','01:00:00'),('Payday','Biggest Dickest','Payday',34,'2020-07-15','01:00:00'),('Biggest Dickest','Test Testy','Payment between players',25000,'2020-07-15','01:18:47'),('Payday','Test Testy','Payday',11840,'2020-07-15','02:00:00'),('Payday','Biggest Dickest','Payday',1001005,'2020-07-15','02:00:00'),('Payday','Test Testy','Payday',11762,'2020-07-15','02:59:59'),('Payday','Test Testy','Payday',11804,'2020-07-15','03:59:59'),('Payday','Test Testy','Payday',10342,'2020-07-15','20:59:59'),('Payday','Test Testy','Payday',10383,'2020-07-15','21:59:59'),('Payday','Test Testy','Payday',9548,'2020-07-16','01:00:00'),('Payday','Test Testy','Payday',9588,'2020-07-16','01:59:59'),('Payday','Richard Tingler','Payday',34,'2020-07-16','01:59:59'),('Payday','Biggest Dickest','Payday',998206,'2020-07-16','01:59:59'),('Richard Tingler','Test Testy','Hooker service',10,'2020-07-16','02:31:58'),('Payday','Test Testy','Payday',10202,'2020-07-16','14:59:59'),('Payday','Test Testy','Payday',10242,'2020-07-16','15:59:59'),('Payday','Test Testy','Payday',9708,'2020-07-16','16:59:59'),('Payday','Test Testy','Payday',9747,'2020-07-16','17:59:59'),('Payday','Test Testy','Payday',9787,'2020-07-16','18:59:59'),('Payday','Biggest Dickest','Payday',999234,'2020-07-16','18:59:59'),('Payday','Test Testy','Payday',9497,'2020-07-16','20:59:59'),('Payday','Test Testy','Payday',9536,'2020-07-16','21:10:33'),('Payday','Test Testy','Payday',9576,'2020-07-16','21:10:35'),('Payday','Test Testy','Payday',9615,'2020-07-16','21:10:37'),('Payday','Test Testy','Payday',9655,'2020-07-16','21:10:39'),('Payday','Test Testy','Payday',9695,'2020-07-16','21:10:41'),('Payday','Test Testy','Payday',9734,'2020-07-16','21:10:47'),('Payday','Test Testy','Payday',9774,'2020-07-16','21:10:51'),('Payday','Test Testy','Payday',9814,'2020-07-16','21:10:54'),('Payday','Test Testy','Payday',9854,'2020-07-16','21:10:56'),('Payday','Test Testy','Payday',9894,'2020-07-16','21:11:09'),('Payday','Test Testy','Payday',9578,'2020-07-16','21:59:59'),('Test Testy','Biggest Dickest','Hooker service',100,'2020-07-16','22:30:12'),('Test Testy','Biggest Dickest','Hooker service',100,'2020-07-16','22:32:35'),('Payday','Test Testy','Payday',9618,'2020-07-16','22:59:59'),('Payday','Biggest Dickest','Payday',1000238,'2020-07-16','22:59:59'),('Payday','Test Testy','Payday',9657,'2020-07-16','23:59:59'),('Payday','Test Testy','Payday',9697,'2020-07-17','00:59:59'),('Payday','Test Testy','Payday',9737,'2020-07-17','01:59:59'),('Payday','Test Testy','Payday',9777,'2020-07-17','02:59:59'),('Payday','Test Testy','Payday',9816,'2020-07-17','03:59:59'),('Payday','Test Testy','Payday',9856,'2020-07-17','04:59:59'),('Payday','Test Testy','Payday',9896,'2020-07-17','05:59:59'),('Payday','Test Testy','Payday',9936,'2020-07-17','06:59:59'),('Payday','Test Testy','Payday',9976,'2020-07-17','07:59:59'),('Payday','Test Testy','Payday',10016,'2020-07-17','08:59:59'),('Payday','Test Testy','Payday',10056,'2020-07-17','09:59:59'),('Payday','Test Testy','Payday',10096,'2020-07-17','10:59:59'),('Payday','Test Testy','Payday',10136,'2020-07-17','11:59:59'),('Payday','Test Testy','Payday',9726,'2020-07-17','12:59:59'),('Payday','Test Testy','Payday',9766,'2020-07-17','13:59:59'),('Payday','Test Testy','Payday',9805,'2020-07-17','14:59:59'),('Payday','Test Testy','Payday',9845,'2020-07-18','02:59:59'),('Payday','Test Testy','Payday',9885,'2020-07-18','03:59:59'),('Payday','Test Testy','Payday',9925,'2020-07-18','15:59:59'),('Payday','Test Testy','Payday',10623,'2020-07-18','17:00:00'),('Payday','Test Testy','Payday',10663,'2020-07-18','20:59:59'),('Payday','Test Testy','Payday',10704,'2020-07-18','22:00:00'),('Payday','Test Testy','Payday',10745,'2020-07-18','23:59:59'),('Payday','Test Testy','Payday',10786,'2020-07-19','02:35:40'),('Payday','Test Testy','Payday',10826,'2020-07-19','14:59:59'),('Payday','Test Testy','Payday',10867,'2020-07-19','17:59:59'),('Payday','Test Testy','Payday',10908,'2020-07-19','19:59:59'),('Payday','Test Testy','Payday',10949,'2020-07-19','20:59:59'),('Payday','Test Testy','Payday',10990,'2020-07-19','21:59:59'),('Payday','Test Testy','Payday',11031,'2020-07-20','17:59:59'),('Payday','Test Testy','Payday',11072,'2020-07-20','20:59:59'),('Payday','Test Testy','Payday',11113,'2020-07-21','16:59:59'),('Payday','Test Testy','Payday',11154,'2020-07-21','17:59:59'),('Payday','Test Testy','Payday',12070,'2020-07-21','18:59:59'),('Payday','Test Testy','Payday',12112,'2020-07-21','19:59:59'),('Payday','Test Testy','Payday',12154,'2020-07-21','21:00:00'),('Payday','Test Testy','Payday',12197,'2020-07-21','21:59:59'),('Payday','Test Testy','Payday',12239,'2020-07-22','16:59:59'),('Payday','Biggest Dickest','Payday',1007020,'2020-07-22','18:00:00'),('Payday','Test Testy','Payday',13557,'2020-07-22','18:00:00'),('Payday','Jon Dillion','Payday',2935,'2020-07-22','18:00:00'),('Test Testy','Jon Dillion','Hooker service',100,'2020-07-22','18:11:15'),('Payday','Test Testy','Payday',12499,'2020-07-22','18:59:59'),('Payday','Jon Dillion','Payday',66,'2020-07-22','18:59:59'),('Payday','Test Testy','Payday',12532,'2020-07-22','19:59:59'),('Test Testy','Town hall','Identification card',500,'2020-07-22','21:05:31'),('Test Testy','Town hall','Medical insurance',2000,'2020-07-22','21:05:47'),('Test Testy','Town hall','Taxi license',5000,'2020-07-22','21:06:01'),('Payday','Test Testy','Payday',11649,'2020-07-22','21:59:59'),('Payday','Test Testy','Payday',11391,'2020-07-23','17:59:59'),('Payday','Jon Dillion','Payday',672,'2020-07-23','18:00:00'),('Payday','Test Testy','Payday',11432,'2020-07-23','18:59:59'),('Payday','Test Testy','Payday',11974,'2020-07-23','19:59:59'),('Payday','Test Testy','Payday',12016,'2020-07-23','20:59:59'),('Payday','Test Testy','Payday',12058,'2020-07-23','21:59:59'),('Payday','Test Testy','Payday',12100,'2020-07-23','22:59:59'),('Payday','Test Testy','Payday',12142,'2020-07-24','16:59:59'),('Payday','Test Testy','Payday',11378,'2020-07-24','18:00:00'),('Payday','Test Testy','Payday',11420,'2020-07-24','20:59:59'),('Payday','Test Testy','Payday',11461,'2020-07-24','21:59:59'),('Payday','Test Testy','Payday',11478,'2020-07-24','22:59:59'),('Payday','Ronnie Gee','Payday',34,'2020-07-24','22:59:59'),('ATM','Toasty G','Deposit',10000000,'2020-07-24','23:54:11'),('Payday','Test Testy','Payday',12744,'2020-07-25','00:00:00'),('Payday','Toasty G','Payday',8849,'2020-07-25','00:00:00'),('Payday','Ronnie Gee','Payday',64,'2020-07-25','00:00:00'),('ATM','Ronnie Gee','Deposit',3598,'2020-07-25','00:00:12'),('Ronnie Gee','ATM','Deposit',99996402,'2020-07-25','00:05:01'),('Payday','Test Testy','Payday',12787,'2020-07-25','01:59:59'),('Payday','Test Testy','Payday',12830,'2020-07-25','02:59:59'),('Payday','Test Testy','Payday',12147,'2020-07-25','03:59:59'),('Payday','Test Testy','Payday',12190,'2020-07-25','12:59:59'),('Payday','Test Testy','Payday',12232,'2020-07-25','13:59:59'),('Payday','Test Testy','Payday',12274,'2020-07-25','14:59:59'),('Payday','Biggest Dickest','Payday',1008057,'2020-07-25','15:00:00'),('Payday','Test Testy','Payday',12316,'2020-07-25','19:59:59'),('Payday','Test Testy','Payday',14185,'2020-07-25','20:59:59'),('Payday','Test Testy','Payday',14229,'2020-07-25','21:59:59'),('Payday','Test Testy','Payday',14273,'2020-07-25','22:59:59'),('Payday','Test Testy','Payday',14317,'2020-07-26','00:00:00'),('Payday','Test Testy','Payday',14332,'2020-07-26','11:59:59'),('Payday','Biggest Dickest','Payday',1009085,'2020-07-26','11:59:59'),('Payday','Test Testy','Payday',13830,'2020-07-26','14:59:59'),('Payday','Test Testy','Payday',13874,'2020-07-26','15:59:59'),('Payday','Test Testy','Payday',11592,'2020-07-26','16:59:59'),('Payday','Ronnie Gee','Payday',98794,'2020-07-26','17:00:00'),('Payday','Test Testy','Payday',11023,'2020-07-26','17:59:59'),('Payday','Test Testy','Payday',10489,'2020-07-26','18:59:59'),('Payday','Test Testy','Payday',10530,'2020-07-26','19:59:59'),('Payday','Toasty G','Payday',8112,'2020-07-26','19:59:59'),('Payday','Test Testy','Payday',11210,'2020-07-26','21:59:59'),('Payday','Test Testy','Payday',11242,'2020-07-27','19:00:00'),('Payday','Test Testy','Payday',11283,'2020-07-27','23:00:00'),('Payday','Corey Trevor','Payday',34,'2020-07-27','23:00:00'),('Payday','Froddey Girau','Payday',34,'2020-07-27','23:00:00'),('Payday','Casey Becker','Payday',34,'2020-07-27','23:00:00'),('Test Testy','ATM','Deposit',99892390,'2020-07-27','23:00:40'),('ATM','Froddey Girau','Deposit',100000,'2020-07-27','23:07:23'),('ATM','Casey Becker','Deposit',5000000,'2020-07-27','23:07:42'),('ATM','Froddey Girau','Deposit',990000000,'2020-07-27','23:07:56'),('Payday','Test Testy','Payday',109482,'2020-07-28','20:00:00'),('Payday','Test Testy','Payday',109621,'2020-07-29','18:59:59'),('Payday','Test Testy','Payday',111961,'2020-07-29','19:59:59'),('Payday','Test Testy','Payday',112103,'2020-07-29','21:00:00'),('Payday','Test Testy','Payday',112465,'2020-07-29','21:59:59'),('Payday','Test Testy','Payday',112607,'2020-07-29','22:59:59'),('Payday','Test Testy','Payday',112750,'2020-07-29','23:59:59'),('Payday','Test Testy','Payday',112893,'2020-07-30','00:59:59'),('Payday','Test Testy','Payday',113035,'2020-07-30','01:59:59'),('Payday','Test Testy','Payday',113179,'2020-07-30','02:59:59'),('Payday','Test Testy','Payday',113322,'2020-07-30','03:59:59'),('Payday','Test Testy','Payday',113465,'2020-07-30','04:59:59'),('Payday','Test Testy','Payday',113608,'2020-07-30','05:59:59'),('Payday','Test Testy','Payday',113752,'2020-07-30','06:59:59'),('Payday','Test Testy','Payday',113896,'2020-07-30','07:59:59'),('Payday','Test Testy','Payday',114040,'2020-07-30','08:59:59'),('Payday','Test Testy','Payday',114184,'2020-07-30','09:59:59'),('Payday','Test Testy','Payday',114328,'2020-07-30','10:59:59'),('Payday','Test Testy','Payday',114472,'2020-07-30','11:59:59'),('Payday','Test Testy','Payday',114617,'2020-07-30','12:59:59'),('Payday','Test Testy','Payday',114761,'2020-07-30','13:59:59'),('Payday','Test Testy','Payday',114906,'2020-07-30','14:59:59'),('Payday','Test Testy','Payday',115051,'2020-07-30','15:59:59'),('Payday','Test Testy','Payday',114576,'2020-07-30','17:00:00'),('Payday','Test Testy','Payday',114421,'2020-07-30','18:59:59'),('Payday','Test Testy','Payday',114565,'2020-07-30','19:59:59'),('Payday','Test Testy','Payday',115410,'2020-07-31','16:59:59'),('Payday','Test Testy','Payday',115555,'2020-07-31','17:59:59'),('Payday','Test Testy','Payday',115701,'2020-07-31','20:59:59'),('Payday','Test Testy','Payday',114705,'2020-07-31','21:59:59'),('Payday','Test Testy','Payday',114850,'2020-07-31','22:59:59'),('Payday','Test Testy','Payday',114995,'2020-07-31','23:59:59'),('Payday','Test Testy','Payday',115140,'2020-08-01','01:00:01'),('Payday','Test Testy','Payday',115285,'2020-08-01','02:00:00'),('Payday','Test Testy','Payday',113230,'2020-08-01','14:59:59'),('Payday','Test Testy','Payday',113374,'2020-08-01','15:59:59'),('Payday','Test Testy','Payday',113517,'2020-08-01','16:59:59');
/*!40000 ALTER TABLE `money` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `news`
--

DROP TABLE IF EXISTS `news`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `news` (
  `id` int NOT NULL AUTO_INCREMENT,
  `winner` int NOT NULL DEFAULT '0',
  `journalist` int NOT NULL DEFAULT '0',
  `amount` int NOT NULL DEFAULT '0',
  `annoucement` varchar(150) NOT NULL DEFAULT '0',
  `date` datetime NOT NULL,
  `given` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `news`
--

LOCK TABLES `news` WRITE;
/*!40000 ALTER TABLE `news` DISABLE KEYS */;
/*!40000 ALTER TABLE `news` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `oceanvehicles`
--

DROP TABLE IF EXISTS `oceanvehicles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `oceanvehicles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `owner` varchar(45) CHARACTER SET latin1 COLLATE latin1_general_ci DEFAULT NULL,
  `scrap` int NOT NULL DEFAULT '0',
  `info` int NOT NULL DEFAULT '0',
  `rareInfo` int NOT NULL DEFAULT '0',
  `scrapValue` int NOT NULL DEFAULT '0',
  `model` varchar(45) COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`id`,`scrap`,`info`,`rareInfo`,`scrapValue`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oceanvehicles`
--

LOCK TABLES `oceanvehicles` WRITE;
/*!40000 ALTER TABLE `oceanvehicles` DISABLE KEYS */;
INSERT INTO `oceanvehicles` VALUES (51,'Test Testy',0,0,0,0,'3228633070'),(52,'Casey Becker',1,0,0,40,'3527576645'),(53,'Froddey Girau',0,0,0,0,'3228633070'),(54,'Test Testy',0,0,0,0,'4061868990'),(55,'Froddey Girau',0,0,0,0,'4061868990'),(56,'',0,0,0,0,'2647026068');
/*!40000 ALTER TABLE `oceanvehicles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `parkings`
--

DROP TABLE IF EXISTS `parkings`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `parkings` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` int NOT NULL DEFAULT '0',
  `house` int NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `capacity` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `parkings`
--

LOCK TABLES `parkings` WRITE;
/*!40000 ALTER TABLE `parkings` DISABLE KEYS */;
INSERT INTO `parkings` VALUES (1,3,0,-470.038,6020.98,31.3406,0),(3,2,0,130.941,6584.35,31.3472,0),(5,2,0,-369.592,-110.806,38.68,0);
/*!40000 ALTER TABLE `parkings` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permissions`
--

DROP TABLE IF EXISTS `permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permissions` (
  `playerId` int NOT NULL DEFAULT '0',
  `command` varchar(16) NOT NULL DEFAULT '',
  `option` varchar(16) NOT NULL DEFAULT '',
  PRIMARY KEY (`playerId`,`command`,`option`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permissions`
--

LOCK TABLES `permissions` WRITE;
/*!40000 ALTER TABLE `permissions` DISABLE KEYS */;
/*!40000 ALTER TABLE `permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phones`
--

DROP TABLE IF EXISTS `phones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `phones` (
  `itemId` int NOT NULL,
  `owner` varchar(32) CHARACTER SET latin1 COLLATE latin1_spanish_ci NOT NULL,
  `number` int NOT NULL,
  `activation` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`itemId`),
  UNIQUE KEY `number` (`number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phones`
--

LOCK TABLES `phones` WRITE;
/*!40000 ALTER TABLE `phones` DISABLE KEYS */;
INSERT INTO `phones` VALUES (37,'Test Testy',479733,'2020-07-15 21:31:06');
/*!40000 ALTER TABLE `phones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `plants`
--

DROP TABLE IF EXISTS `plants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `plants` (
  `id` int NOT NULL AUTO_INCREMENT,
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `dimension` int NOT NULL DEFAULT '0',
  `growth` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `plants`
--

LOCK TABLES `plants` WRITE;
/*!40000 ALTER TABLE `plants` DISABLE KEYS */;
/*!40000 ALTER TABLE `plants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `questions`
--

DROP TABLE IF EXISTS `questions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `questions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `question` text NOT NULL,
  `license` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `questions`
--

LOCK TABLES `questions` WRITE;
/*!40000 ALTER TABLE `questions` DISABLE KEYS */;
INSERT INTO `questions` VALUES (1,'Are you here for alpha testing?',0),(2,'What is the speed limit in a populated area?',1),(3,'On what side of the road is it allowed to drive while operating a vehicle?',1),(4,'What do you do when a emergency vehicle is behind you with sirens?',1);
/*!40000 ALTER TABLE `questions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skills`
--

DROP TABLE IF EXISTS `skills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skills` (
  `id` int NOT NULL,
  `lockpickingexp` int NOT NULL DEFAULT '0',
  `lockpickinglevel` int NOT NULL DEFAULT '0',
  `miningexp` int NOT NULL DEFAULT '0',
  `strenghtexp` int NOT NULL DEFAULT '0',
  `firstaidexp` int NOT NULL DEFAULT '0',
  `truckerexp` int NOT NULL DEFAULT '0',
  `staminaexp` int NOT NULL DEFAULT '0',
  `fishingexp` int NOT NULL DEFAULT '0',
  `engineerexp` int NOT NULL DEFAULT '0',
  `mininglevel` int NOT NULL DEFAULT '0',
  `firstaidlevel` int NOT NULL DEFAULT '0',
  `truckerlevel` int NOT NULL DEFAULT '0',
  `staminalevel` int NOT NULL DEFAULT '0',
  `strenghtlevel` int NOT NULL DEFAULT '0',
  `fishinglevel` int NOT NULL DEFAULT '0',
  `engineerlevel` int NOT NULL DEFAULT '0',
  `herbalismexp` int NOT NULL DEFAULT '0',
  `herbalismlevel` int NOT NULL DEFAULT '0',
  `drugprodexp` int NOT NULL DEFAULT '0',
  `drugprodlevel` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`,`lockpickingexp`,`lockpickinglevel`,`miningexp`,`strenghtexp`,`firstaidexp`,`truckerexp`,`staminaexp`,`fishingexp`,`engineerexp`,`mininglevel`,`firstaidlevel`,`truckerlevel`,`staminalevel`,`strenghtlevel`,`fishinglevel`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skills`
--

LOCK TABLES `skills` WRITE;
/*!40000 ALTER TABLE `skills` DISABLE KEYS */;
INSERT INTO `skills` VALUES (17,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,55,13,43,4);
/*!40000 ALTER TABLE `skills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skins`
--

DROP TABLE IF EXISTS `skins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skins` (
  `characterId` int NOT NULL,
  `firstHeadShape` int NOT NULL,
  `secondHeadShape` int NOT NULL,
  `firstSkinTone` int NOT NULL,
  `secondSkinTone` int NOT NULL,
  `headMix` float NOT NULL,
  `skinMix` float NOT NULL,
  `hairModel` int NOT NULL,
  `firstHairColor` int NOT NULL,
  `secondHairColor` int NOT NULL,
  `beardModel` int NOT NULL,
  `beardColor` int NOT NULL,
  `chestModel` int NOT NULL,
  `chestColor` int NOT NULL,
  `blemishesModel` int NOT NULL,
  `ageingModel` int NOT NULL,
  `complexionModel` int NOT NULL,
  `sundamageModel` int NOT NULL,
  `frecklesModel` int NOT NULL,
  `noseWidth` float NOT NULL,
  `noseHeight` float NOT NULL,
  `noseLength` float NOT NULL,
  `noseBridge` float NOT NULL,
  `noseTip` float NOT NULL,
  `noseShift` float NOT NULL,
  `browHeight` float NOT NULL,
  `browWidth` float NOT NULL,
  `cheekboneHeight` float NOT NULL,
  `cheekboneWidth` float NOT NULL,
  `cheeksWidth` float NOT NULL,
  `eyes` float NOT NULL,
  `lips` float NOT NULL,
  `jawWidth` float NOT NULL,
  `jawHeight` float NOT NULL,
  `chinLength` float NOT NULL,
  `chinPosition` float NOT NULL,
  `chinWidth` float NOT NULL,
  `chinShape` float NOT NULL,
  `neckWidth` float NOT NULL,
  `eyesColor` int NOT NULL,
  `eyebrowsModel` int NOT NULL,
  `eyebrowsColor` int NOT NULL,
  `makeupModel` int NOT NULL,
  `blushModel` int NOT NULL,
  `blushColor` int NOT NULL,
  `lipstickModel` int NOT NULL,
  `lipstickColor` int NOT NULL,
  PRIMARY KEY (`characterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skins`
--

LOCK TABLES `skins` WRITE;
/*!40000 ALTER TABLE `skins` DISABLE KEYS */;
INSERT INTO `skins` VALUES (17,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(18,3,0,8,0,0.5,0.5,8,0,0,255,0,255,0,255,255,255,255,255,0.48,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(20,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(21,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(22,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(23,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(24,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(25,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(26,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(27,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(28,5,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(29,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(30,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0),(33,0,0,0,0,0.5,0.5,0,0,0,255,0,255,0,255,255,255,255,255,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,255,0,255,255,0,255,0);
/*!40000 ALTER TABLE `skins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sms`
--

DROP TABLE IF EXISTS `sms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `sms` (
  `phone` int NOT NULL,
  `target` int NOT NULL,
  `message` text NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`phone`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sms`
--

LOCK TABLES `sms` WRITE;
/*!40000 ALTER TABLE `sms` DISABLE KEYS */;
/*!40000 ALTER TABLE `sms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tattoos`
--

DROP TABLE IF EXISTS `tattoos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tattoos` (
  `player` int NOT NULL DEFAULT '0',
  `zone` int NOT NULL DEFAULT '0',
  `library` varchar(32) NOT NULL DEFAULT '',
  `hash` varchar(32) NOT NULL DEFAULT '',
  PRIMARY KEY (`player`,`hash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tattoos`
--

LOCK TABLES `tattoos` WRITE;
/*!40000 ALTER TABLE `tattoos` DISABLE KEYS */;
/*!40000 ALTER TABLE `tattoos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tunning`
--

DROP TABLE IF EXISTS `tunning`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tunning` (
  `id` int NOT NULL AUTO_INCREMENT,
  `vehicle` int NOT NULL DEFAULT '0',
  `slot` int NOT NULL DEFAULT '0',
  `component` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=94 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tunning`
--

LOCK TABLES `tunning` WRITE;
/*!40000 ALTER TABLE `tunning` DISABLE KEYS */;
INSERT INTO `tunning` VALUES (43,283,11,3),(44,283,13,2),(45,283,15,3),(46,283,23,12),(47,288,0,13),(48,288,1,14),(49,288,15,3),(50,288,3,3),(51,288,2,7),(52,292,0,13),(53,292,1,12),(54,292,2,6),(55,292,3,1),(56,292,3,1),(57,292,7,8),(58,286,0,9),(59,286,1,14),(60,286,2,7),(61,286,3,3),(62,286,11,4),(63,286,12,2),(64,292,15,3),(65,292,10,3),(66,292,23,11),(67,292,13,3),(68,286,12,2),(69,292,46,2),(70,286,13,3),(71,292,11,4),(72,292,12,2),(73,292,13,3),(74,286,48,7),(75,292,48,7),(76,287,0,8),(77,286,15,3),(78,287,2,7),(79,286,10,1),(80,287,1,2),(81,287,3,2),(82,287,7,8),(83,287,15,3),(84,287,48,3),(85,287,23,40),(86,287,5,4),(87,287,11,4),(88,287,12,2),(89,287,13,3),(90,342,11,3),(91,342,15,3),(92,342,13,2),(93,342,12,2);
/*!40000 ALTER TABLE `tunning` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL,
  `model` varchar(32) NOT NULL,
  `posX` float(10,0) NOT NULL DEFAULT '-136',
  `posY` float NOT NULL DEFAULT '6198.95',
  `posZ` float NOT NULL DEFAULT '32.3845',
  `rotation` float NOT NULL DEFAULT '0',
  `money` int NOT NULL DEFAULT '0',
  `bank` int NOT NULL DEFAULT '5000',
  `health` int NOT NULL DEFAULT '100',
  `armor` int NOT NULL DEFAULT '0',
  `age` int NOT NULL DEFAULT '14',
  `sex` int NOT NULL DEFAULT '0',
  `faction` int NOT NULL DEFAULT '0',
  `job` int NOT NULL DEFAULT '0',
  `rank` int NOT NULL DEFAULT '0',
  `radio` int NOT NULL DEFAULT '0',
  `jailed` varchar(8) NOT NULL DEFAULT '-1,-1',
  `carKeys` varchar(32) NOT NULL DEFAULT '0,0,0,0,0',
  `documentation` int NOT NULL DEFAULT '0',
  `licenses` varchar(32) NOT NULL DEFAULT '-1,-1,-1',
  `insurance` int NOT NULL DEFAULT '0',
  `weaponLicense` int NOT NULL DEFAULT '0',
  `houseRent` int NOT NULL DEFAULT '0',
  `houseEntered` int NOT NULL DEFAULT '0',
  `businessEntered` int NOT NULL DEFAULT '0',
  `jobDeliver` int NOT NULL DEFAULT '0',
  `jobCooldown` int NOT NULL DEFAULT '0',
  `played` int NOT NULL DEFAULT '0',
  `status` int NOT NULL DEFAULT '1',
  `socialName` varchar(32) NOT NULL,
  `adminRank` int NOT NULL DEFAULT '0',
  `adminname` varchar(24) NOT NULL DEFAULT '',
  `employeeCooldown` int NOT NULL DEFAULT '0',
  `duty` int NOT NULL DEFAULT '0',
  `killed` int NOT NULL DEFAULT '0',
  `jobPoints` varchar(64) NOT NULL DEFAULT '0,0,0,0,0,0,0',
  `rolePoints` int NOT NULL DEFAULT '0',
  `informationCollected` int NOT NULL DEFAULT '0',
  `rareInformationCollected` int NOT NULL DEFAULT '0',
  `level` int NOT NULL DEFAULT '0',
  `vehicles` int NOT NULL DEFAULT '0',
  `houses` int NOT NULL DEFAULT '0',
  `businesses` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (17,'Test Testy','mp_m_freemode_01',1352,4381.99,44.3437,228.417,99928720,115125428,100,0,18,0,0,3,7,0,'-1,-1','0,0,0,0,0',1595444731,'12,-1,1',1596654347,1598060932,0,0,65,7,0,8,1,'Faktis',4,'',2,1,0,'0,0,0,0,0,0,9',0,0,0,4,1,2,1),(18,'Biggest Dickest','mp_m_freemode_01',6,-9.57433,70.3102,144.997,0,0,92,42,18,0,1,0,6,0,'-1,-1','266,0,0,0,0',0,'12,-1,-1',0,0,0,0,0,1,0,6,1,'NNEZZiE',4,'',1,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(28,'Casey Becker','mp_m_freemode_01',497,-977.654,63.9005,25.6461,994950000,5003534,86,0,117,0,0,11,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,1,1,'Arvent0',0,'',4,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(29,'Froddey Girau','mp_m_freemode_01',2949,5329.28,101.557,267.451,9790000,990103534,100,0,21,0,0,12,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,1,1,'Deaderik',0,'',4,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(30,'Corey Trevor','mp_m_freemode_01',-56,-1098.69,26.4224,150.421,1000000000,3534,68,0,24,0,0,3,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,1,1,'Aliliel',0,'',4,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(33,'Ronnie Gee','mp_m_freemode_01',-34,-2514.7,5.2954,139.954,0,3500,98,0,21,0,0,8,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,0,1,'OfficialGxbbs',0,'',5,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `vehicles`
--

DROP TABLE IF EXISTS `vehicles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `vehicles` (
  `id` int NOT NULL AUTO_INCREMENT,
  `model` varchar(32) NOT NULL,
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `rotation` float NOT NULL,
  `firstColor` varchar(12) NOT NULL DEFAULT '0,0,0',
  `secondColor` varchar(12) NOT NULL DEFAULT '0,0,0',
  `dimension` int NOT NULL DEFAULT '0',
  `engine` int NOT NULL DEFAULT '0',
  `locked` int NOT NULL DEFAULT '0',
  `faction` int NOT NULL DEFAULT '0',
  `owner` varchar(32) NOT NULL,
  `plate` varchar(8) NOT NULL,
  `price` int NOT NULL DEFAULT '0',
  `parking` int NOT NULL DEFAULT '0',
  `parkedTime` int NOT NULL DEFAULT '0',
  `gas` float NOT NULL DEFAULT '0',
  `kms` float NOT NULL DEFAULT '0',
  `colorType` int NOT NULL DEFAULT '1',
  `pearlescent` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=348 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `vehicles`
--

LOCK TABLES `vehicles` WRITE;
/*!40000 ALTER TABLE `vehicles` DISABLE KEYS */;
INSERT INTO `vehicles` VALUES (273,'1813965170',310.427,-482.604,43.3788,354.811,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(274,'3990109732',357.69,-674.077,29.3376,83.5013,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(275,'3538169420',344.633,-864.899,29.3012,260.119,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(276,'1946655157',217.93,-1633.78,29.1498,25.1323,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(277,'39244776',-1173.52,-2870.09,13.9457,306.435,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(278,'3532022478',1912.8,3694.98,32.7729,119.376,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(279,'2239439183',1912.8,3694.98,32.7729,119.376,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(280,'3737920533',1908.85,3694.58,32.8843,121.435,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(281,'1813965170',40.037,-759.347,44.2268,121.954,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(283,'1813965170',-368.994,-115.891,38.6964,44.3126,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(284,'1813965170',1523.18,918.055,77.477,162.164,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(285,'1813965170',1524.45,905.517,77.3605,146.97,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(286,'SultanRS',-391.649,-105.804,38.069,232.569,'0,0,0','0,0,0',0,0,0,0,'Froddey Girau','LS 1286',0,0,0,48.0808,2.95438,1,0),(287,'SultanRS',-387.353,-128.16,38.0566,138.447,'0,0,0','0,0,0',0,0,0,0,'Casey Becker','LS 1287',0,0,0,48.3176,2.58872,1,0),(288,'SultanRS',311.805,-1206.31,38.8968,8.97498,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(289,'1813965170',315.461,-1215.14,38.2473,80.1873,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(290,'1813965170',317.532,-1219.41,38.2467,126.783,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(291,'1813965170',304.444,-1219.95,38.2949,86.1755,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(292,'SultanRS',-363.228,-128.311,38.6956,197.478,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(293,'SultanRS',-380.278,-115.912,38.0663,73.5256,'0,0,0','0,0,0',0,0,0,0,'Corey Trevor','LS 1293',0,0,0,47.4364,3.94393,1,0),(294,'1980674452',-389.8,-127.151,38.6816,338.939,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(295,'1451183779',-384.592,-126.173,38.6858,340.619,'0,0,0','0,0,0',0,0,0,0,'Corey Trevor','LS 1295',0,0,0,50,0,1,0),(296,'444994115',-362.558,-136.861,37.9672,350.634,'0,0,0','0,0,0',0,0,0,0,'Corey Trevor','LS 1296',0,0,0,50,0.0000153874,1,0),(298,'FMJ',-14.0048,-1080.05,26.0558,219.957,'62,11,102','8,8,8',0,0,0,0,'Test Testy','LS 1298',347000,0,0,50,0.000188575,1,0),(299,'FMJ',-10.7261,-1090.54,26.6721,165.485,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(300,'GP1',-11.116,-1080.22,26.0554,223.02,'193,101,16','8,8,8',0,0,0,0,'Casey Becker','LS 1300',245000,0,0,50,0.000147162,1,0),(301,'Faction2',-7.25357,-1082.96,26.0331,214.386,'8,8,8','8,8,8',0,0,0,0,'Froddey Girau','LS 1301',35200,0,0,50,0.00012356,1,0),(302,'Submersible2',360.82,-3168.11,0.888075,40.0664,'0,0,0','0,0,0',0,0,0,0,'Test Testy','LS 1302',0,0,0,49.5812,0.6477,1,0),(303,'Submersible2',375.545,-3211.66,-0.91199,40.5327,'0,0,0','0,0,0',0,0,0,0,'Casey Becker','LS 1303',0,0,0,49.6621,0.520294,1,0),(304,'Submersible2',390.99,-3207.93,-2.18205,36.1977,'0,0,0','0,0,0',0,0,0,0,'Froddey Girau','LS 1304',0,0,0,49.6682,0.512764,1,0),(305,'Boxville2',505.249,-636.153,24.7507,0,'0,0,0','0,0,0',0,0,0,112,'Test Testy','',0,0,0,50,0,1,0),(306,'Boxville2',505.249,-636.153,24.7507,0,'0,0,0','0,0,0',0,0,0,112,'Froddey Girau','',0,0,0,50,0,1,0),(309,'Elegy',-167.854,6475.7,30.1843,225.846,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(310,'Elegy',-163.701,6488.57,29.8748,196.895,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(311,'1196467202',-424.199,5948.74,31.906,146.441,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(312,'810895499',-425.904,5946.14,32.1057,143.423,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(313,'Bati',-425.904,5946.14,32.1057,143.423,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(314,'646777275',1439.3,1013.98,114.25,219.299,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(315,'Comet3',-34.2674,-2514.7,5.2954,338.683,'0,0,0','0,0,0',0,0,0,0,'Ronnie Gee','LS 1315',0,0,0,42.0037,12.303,1,0),(316,'Police',-118.024,-1721.7,29.8957,307.232,'255,255,255','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(334,'PBus',1854.49,2545.65,45.6721,267.167,'256,256,256','256,256,256',0,0,0,21,'Test Testy','',0,0,0,50,0,1,0),(335,'1718441594',960.458,-992.105,40.2748,97.5356,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(336,'1780626666',960.456,-992.106,40.2846,97.5356,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(337,'1780626666',1358.57,4374.43,44.3394,281.065,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(338,'329848940',1021.26,-3206.09,5.9018,84.7651,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(339,'329848940',1014.28,-3207.53,5.86913,73.7159,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(340,'3788608179',1008.43,-3212.78,5.89938,101.076,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(341,'3015758601',1009.06,-3201.32,5.90158,60.7551,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(342,'3514263643',1002.62,-3198.38,5.90135,268.346,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(343,'3632063247',1004.08,-3210.79,5.90137,321.308,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(344,'Nero2',1719.29,1505.04,84.7884,344.117,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(345,'Miljet',2459,5673.31,45.1019,207.345,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(346,'Miljet',2512.45,5558.88,44.7809,183.248,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0),(347,'Miljet',-1111.21,-3275.44,13.9443,55.8713,'0,0,0','0,0,0',0,0,0,9,'','',0,0,0,50,0,1,0);
/*!40000 ALTER TABLE `vehicles` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-01 17:35:32
