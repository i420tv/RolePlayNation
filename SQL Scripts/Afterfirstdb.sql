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
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `business`
--

LOCK TABLES `business` WRITE;
/*!40000 ALTER TABLE `business` DISABLE KEYS */;
INSERT INTO `business` VALUES (13,12,'',174.067,6604.03,65.3484,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(14,12,'',178.22,6604.96,65.3681,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(15,1,'',-430.968,6040.96,64.7799,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(16,12,'',173.757,6611.54,60.8603,0,'Business','',0,0,3,_binary '\0',1,1500000),(19,20,'',110.933,6626.14,33.1603,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(20,7,'ipl_ammu',-441.789,6044.17,29.5379,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(22,7,'',-392.445,6068.24,33.5001,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(23,24,'',-281.798,6264.99,33.4503,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(24,4,'',-263.881,6622.12,9.42551,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(25,17,'low_end_garage_no_ipl',1724.55,3696.58,33.4161,0,'Shady Shack','',0,0,3,_binary '\0',0,1500000),(31,12,'',171.331,6599.47,33.3185,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(32,1,'',-163.549,6334.16,33.5808,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(33,1,'',-129.689,6335.75,34.9904,0,'Negocio','Test Testy',0,0,3,_binary '\0',0,1500000),(34,1,'',1950.47,3726.99,34.3653,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(35,1,'',1950.77,3732.78,34.3631,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(36,2,'',1956.2,3727.63,34.3555,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(37,1,'',432,-1019.81,30.8767,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(38,1,'',-615.796,2106.5,128.177,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(39,3,'',-611.182,2108.87,128.403,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(40,1,'',2537.46,2686.1,42.1024,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(42,12,'',-1290.95,-3020.06,13.281,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(43,1,'',-1296.22,-3023.96,13.2776,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(47,1,'',505.976,-672.72,24.2279,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(48,1,'',508.017,-681.331,24.2914,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(49,4,'',2.77234,-2511.87,5.00677,0,'Negocio','',0,0,3,_binary '\0',1,1500000),(50,4,'',19.0992,-2499.21,5.00668,0,'Negocio','',0,0,3,_binary '\0',1,1500000);
/*!40000 ALTER TABLE `business` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `oceanvehicles`
--

LOCK TABLES `oceanvehicles` WRITE;
/*!40000 ALTER TABLE `oceanvehicles` DISABLE KEYS */;
INSERT INTO `oceanvehicles` VALUES (35,'Test Testy',0,0,0,0,'2157618379'),(36,'Test Testy',0,0,0,0,'2157618379'),(37,'Test Testy',0,0,0,0,'2157618379'),(38,'Test Testy',0,0,0,0,'2157618379'),(39,'Test Testy',0,0,0,0,'2157618379'),(40,'Test Testy',0,0,0,0,'2715434129'),(41,'Test Testy',0,0,0,0,'2157618379'),(42,'Test Testy',0,0,0,0,'2715434129'),(43,'Test Testy',0,0,0,0,'2157618379'),(44,'Test Testy',0,0,0,0,'2715434129'),(45,'Test Testy',0,0,0,0,'4061868990'),(46,'Test Testy',0,0,0,0,'4061868990'),(47,'Ronnie Gee',2,0,0,110,'431982955'),(48,'Ronnie Gee',2,0,0,110,'329747436'),(50,'Toasty G',0,0,0,0,'3954262395');
/*!40000 ALTER TABLE `oceanvehicles` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (17,'Test Testy','mp_m_freemode_01',-657,-1712.45,37.0021,192.578,32990,10859545,100,0,18,0,0,8,6,0,'-1,-1','0,0,0,0,0',1595444731,'-1,-1,1',1596654347,1598060932,0,0,0,7,0,8,1,'Faktis',4,'',0,1,0,'0,0,0,0,0,0,0',0,0,0,2,1,1,1),(18,'Biggest Dickest','mp_m_freemode_01',1275,553.511,80.7301,323.865,100139166,1005005603,100,0,18,0,7,5,6,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,1,0,4,1,'NNEZZiE',4,'',3,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(19,'Richard Tingler','u_m_o_finguru_01',1669,3929.36,31.1989,129.937,790,3534,100,0,55,0,0,0,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,1,0,1,1,'iMightyChris',0,'',0,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(20,'Frank Litrico','mp_m_freemode_01',298,-3269.6,5.78707,197.825,1000000,0,100,0,32,0,0,11,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,0,1,'Aliliel',0,'',5,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(21,'James Mcpuffin','mp_m_freemode_01',-591,2072.33,131.293,186.3,0,3500,100,0,22,0,7,11,4,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,0,1,'seanjordon',4,'',0,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(22,'Jon Dillion','mp_m_freemode_01',112,-2655.3,6.00426,225.382,140,7173,100,0,18,0,0,0,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,3,1,'alderzmodz',4,'',3,0,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(23,'Deasd Easdaw','mp_m_freemode_01',-138,6205.61,31.2104,336.64,0,3500,100,0,18,0,0,0,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,0,1,'Faktis',0,'',0,0,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(24,'Tom Burgunday','mp_m_freemode_01',306,-1204.23,38.8926,89.5962,0,3500,100,0,23,0,0,0,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,0,1,'Faktis',0,'',0,0,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(25,'Ronnie Gee','mp_m_freemode_01',2155,2742.97,47.6758,184.475,99996402,100003598,87,0,21,0,0,11,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,2,1,'OfficialGxbbs',0,'',0,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0),(26,'Toasty G','mp_f_freemode_01',2160,2740.25,47.4151,126.426,990000288,10012349,100,0,18,1,0,0,0,0,'-1,-1','0,0,0,0,0',0,'-1,-1,-1',0,0,0,0,0,0,0,1,1,'TheToastOG',0,'',4,1,0,'0,0,0,0,0,0,0',0,0,0,1,0,0,0);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-25 12:19:06
