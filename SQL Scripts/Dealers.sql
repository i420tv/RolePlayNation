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
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-07-25  2:20:13
