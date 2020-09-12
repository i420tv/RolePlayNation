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
  PRIMARY KEY (`id`,`lockpickingexp`,`lockpickinglevel`,`miningexp`,`strenghtexp`,`firstaidexp`,`truckerexp`,`staminaexp`,`fishingexp`,`engineerexp`,`mininglevel`,`firstaidlevel`,`truckerlevel`,`staminalevel`,`strenghtlevel`,`fishinglevel`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skills`
--

LOCK TABLES `skills` WRITE;
/*!40000 ALTER TABLE `skills` DISABLE KEYS */;
INSERT INTO `skills` VALUES (17,3,1,18,1,0,0,0,0,0,1,0,0,0,0,0);
/*!40000 ALTER TABLE `skills` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-19 22:31:23
