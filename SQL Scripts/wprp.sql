-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         5.5.5-10.1.29-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win32
-- HeidiSQL Versión:             8.0.0.4396
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Volcando estructura de base de datos para gtav
DROP DATABASE IF EXISTS `gtav`;
CREATE DATABASE IF NOT EXISTS `gtav` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_general_ci */;
USE `gtav`;


-- Volcando estructura para tabla gtav.accounts
DROP TABLE IF EXISTS `accounts`;
CREATE TABLE IF NOT EXISTS `accounts` (
  `socialName` varchar(32) NOT NULL,
  `forumName` varchar(32) NOT NULL DEFAULT '',
  `password` varchar(64) NOT NULL,
  `status` int(11) NOT NULL DEFAULT '0',
  `lastCharacter` int(11) NOT NULL DEFAULT '-1',
  `lastIp` varchar(16) NOT NULL DEFAULT '',
  `updated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `retries` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`socialName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.admin
DROP TABLE IF EXISTS `admin`;
CREATE TABLE IF NOT EXISTS `admin` (
  `source` varchar(24) NOT NULL DEFAULT '',
  `target` varchar(24) NOT NULL DEFAULT '',
  `action` varchar(32) NOT NULL DEFAULT '',
  `time` int(11) NOT NULL DEFAULT '0',
  `reason` varchar(150) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`source`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.answers
DROP TABLE IF EXISTS `answers`;
CREATE TABLE IF NOT EXISTS `answers` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `question` int(11) NOT NULL,
  `answer` text NOT NULL,
  `correct` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.applications
DROP TABLE IF EXISTS `applications`;
CREATE TABLE IF NOT EXISTS `applications` (
  `account` varchar(32) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL DEFAULT '',
  `mistakes` int(11) NOT NULL DEFAULT '0',
  `submission` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`account`,`submission`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_general_ci;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.blood
DROP TABLE IF EXISTS `blood`;
CREATE TABLE IF NOT EXISTS `blood` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `doctor` int(11) NOT NULL,
  `patient` int(11) NOT NULL,
  `bloodtype` varchar(8) NOT NULL,
  `used` bit(1) NOT NULL DEFAULT b'0',
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.business
DROP TABLE IF EXISTS `business`;
CREATE TABLE IF NOT EXISTS `business` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `type` int(10) NOT NULL DEFAULT '0',
  `ipl` varchar(64) DEFAULT NULL,
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int(11) NOT NULL DEFAULT '0',
  `name` varchar(64) NOT NULL DEFAULT 'Negocio',
  `owner` varchar(32) NOT NULL DEFAULT '',
  `funds` int(11) NOT NULL DEFAULT '0',
  `products` int(11) NOT NULL DEFAULT '0',
  `multiplier` float NOT NULL DEFAULT '3',
  `locked` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.calls
DROP TABLE IF EXISTS `calls`;
CREATE TABLE IF NOT EXISTS `calls` (
  `phone` int(10) NOT NULL,
  `target` int(10) NOT NULL,
  `time` int(10) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`phone`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.channels
DROP TABLE IF EXISTS `channels`;
CREATE TABLE IF NOT EXISTS `channels` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `owner` int(10) NOT NULL DEFAULT '0',
  `password` varchar(32) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.clothes
DROP TABLE IF EXISTS `clothes`;
CREATE TABLE IF NOT EXISTS `clothes` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `player` int(10) NOT NULL DEFAULT '0',
  `type` int(10) NOT NULL DEFAULT '0',
  `slot` int(10) NOT NULL DEFAULT '0',
  `drawable` int(10) NOT NULL DEFAULT '0',
  `texture` int(10) NOT NULL DEFAULT '0',
  `dressed` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.contacts
DROP TABLE IF EXISTS `contacts`;
CREATE TABLE IF NOT EXISTS `contacts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `owner` int(6) NOT NULL,
  `contactNumber` int(6) NOT NULL,
  `contactName` varchar(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.controls
DROP TABLE IF EXISTS `controls`;
CREATE TABLE IF NOT EXISTS `controls` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `name` varchar(24) NOT NULL DEFAULT '',
  `item` int(10) NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `rotation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.crimes
DROP TABLE IF EXISTS `crimes`;
CREATE TABLE IF NOT EXISTS `crimes` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(256) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `jail` int(11) NOT NULL DEFAULT '0',
  `fine` int(11) NOT NULL DEFAULT '0',
  `reminder` varchar(128) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.dealers
DROP TABLE IF EXISTS `dealers`;
CREATE TABLE IF NOT EXISTS `dealers` (
  `vehicleHash` varchar(24) COLLATE latin1_spanish_ci NOT NULL,
  `dealerId` int(11) NOT NULL,
  `vehicleType` int(11) NOT NULL,
  `price` int(11) NOT NULL,
  PRIMARY KEY (`vehicleHash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.fines
DROP TABLE IF EXISTS `fines`;
CREATE TABLE IF NOT EXISTS `fines` (
  `officer` varchar(32) NOT NULL DEFAULT '',
  `target` varchar(32) NOT NULL DEFAULT '',
  `amount` int(10) NOT NULL DEFAULT '0',
  `reason` varchar(128) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`officer`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.furniture
DROP TABLE IF EXISTS `furniture`;
CREATE TABLE IF NOT EXISTS `furniture` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `hash` int(10) NOT NULL DEFAULT '0',
  `house` int(10) NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `rotation` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.hotwires
DROP TABLE IF EXISTS `hotwires`;
CREATE TABLE IF NOT EXISTS `hotwires` (
  `vehicle` int(10) NOT NULL,
  `player` varchar(24) NOT NULL DEFAULT '',
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`player`,`vehicle`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.houses
DROP TABLE IF EXISTS `houses`;
CREATE TABLE IF NOT EXISTS `houses` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `ipl` varchar(32) NOT NULL DEFAULT '',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int(11) NOT NULL DEFAULT '0',
  `name` varchar(32) NOT NULL DEFAULT 'Casa',
  `price` int(11) NOT NULL DEFAULT '10000',
  `owner` varchar(32) NOT NULL DEFAULT '',
  `status` int(1) NOT NULL DEFAULT '2',
  `tenants` int(1) NOT NULL DEFAULT '0',
  `rental` int(10) NOT NULL DEFAULT '0',
  `locked` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.items
DROP TABLE IF EXISTS `items`;
CREATE TABLE IF NOT EXISTS `items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `hash` varchar(32) NOT NULL DEFAULT '',
  `ownerEntity` varchar(16) NOT NULL DEFAULT '',
  `ownerIdentifier` int(11) NOT NULL DEFAULT '0',
  `amount` int(11) NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `dimension` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.licensed
DROP TABLE IF EXISTS `licensed`;
CREATE TABLE IF NOT EXISTS `licensed` (
  `item` int(11) NOT NULL DEFAULT '0',
  `buyer` varchar(24) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`item`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.messages
DROP TABLE IF EXISTS `messages`;
CREATE TABLE IF NOT EXISTS `messages` (
	`id` INT(11) NOT NULL AUTO_INCREMENT,
	`senderNumber` INT(6) NOT NULL DEFAULT '0',
	`receiverNumber` INT(6) NOT NULL DEFAULT '0',
	`message` TEXT NOT NULL,
	`deleted` INT(11) NOT NULL DEFAULT '0',
	PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.money
DROP TABLE IF EXISTS `money`;
CREATE TABLE IF NOT EXISTS `money` (
  `source` varchar(32) NOT NULL,
  `receiver` varchar(32) NOT NULL,
  `type` varchar(32) NOT NULL,
  `amount` int(11) NOT NULL DEFAULT '0',
  `date` date NOT NULL,
  `hour` time NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.news
DROP TABLE IF EXISTS `news`;
CREATE TABLE IF NOT EXISTS `news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `winner` int(11) NOT NULL DEFAULT '0',
  `journalist` int(11) NOT NULL DEFAULT '0',
  `amount` int(11) NOT NULL DEFAULT '0',
  `annoucement` varchar(150) NOT NULL DEFAULT '0',
  `date` datetime NOT NULL,
  `given` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.parkings
DROP TABLE IF EXISTS `parkings`;
CREATE TABLE IF NOT EXISTS `parkings` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `type` int(1) NOT NULL DEFAULT '0',
  `house` int(11) NOT NULL DEFAULT '0',
  `posX` float NOT NULL DEFAULT '0',
  `posY` float NOT NULL DEFAULT '0',
  `posZ` float NOT NULL DEFAULT '0',
  `capacity` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.permissions
DROP TABLE IF EXISTS `permissions`;
CREATE TABLE IF NOT EXISTS `permissions` (
  `playerId` int(10) NOT NULL DEFAULT '0',
  `command` varchar(16) NOT NULL DEFAULT '',
  `option` varchar(16) NOT NULL DEFAULT '',
  PRIMARY KEY (`playerId`,`command`,`option`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.phones
DROP TABLE IF EXISTS `phones`;
CREATE TABLE IF NOT EXISTS `phones` (
  `itemId` int(11) NOT NULL,
  `owner` varchar(32) COLLATE latin1_spanish_ci NOT NULL,
  `number` int(6) NOT NULL,
  `activation` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`itemId`),
  UNIQUE KEY `number` (`number`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.questions
DROP TABLE IF EXISTS `questions`;
CREATE TABLE IF NOT EXISTS `questions` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `question` text NOT NULL,
  `license` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.skins
DROP TABLE IF EXISTS `skins`;
CREATE TABLE IF NOT EXISTS `skins` (
  `characterId` int(11) NOT NULL,
  `firstHeadShape` int(11) NOT NULL,
  `secondHeadShape` int(11) NOT NULL,
  `firstSkinTone` int(11) NOT NULL,
  `secondSkinTone` int(11) NOT NULL,
  `headMix` float NOT NULL,
  `skinMix` float NOT NULL,
  `hairModel` int(10) NOT NULL,
  `firstHairColor` int(10) NOT NULL,
  `secondHairColor` int(10) NOT NULL,
  `beardModel` int(10) NOT NULL,
  `beardColor` int(10) NOT NULL,
  `chestModel` int(10) NOT NULL,
  `chestColor` int(10) NOT NULL,
  `blemishesModel` int(10) NOT NULL,
  `ageingModel` int(10) NOT NULL,
  `complexionModel` int(10) NOT NULL,
  `sundamageModel` int(10) NOT NULL,
  `frecklesModel` int(10) NOT NULL,
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
  `eyesColor` int(11) NOT NULL,
  `eyebrowsModel` int(11) NOT NULL,
  `eyebrowsColor` int(11) NOT NULL,
  `makeupModel` int(11) NOT NULL,
  `blushModel` int(11) NOT NULL,
  `blushColor` int(11) NOT NULL,
  `lipstickModel` int(11) NOT NULL,
  `lipstickColor` int(11) NOT NULL,
  PRIMARY KEY (`characterId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.sms
DROP TABLE IF EXISTS `sms`;
CREATE TABLE IF NOT EXISTS `sms` (
  `phone` int(10) NOT NULL,
  `target` int(10) NOT NULL,
  `message` text NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`phone`,`target`,`date`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.tattoos
DROP TABLE IF EXISTS `tattoos`;
CREATE TABLE IF NOT EXISTS `tattoos` (
  `player` int(10) NOT NULL DEFAULT '0',
  `zone` int(10) NOT NULL DEFAULT '0',
  `library` varchar(32) NOT NULL DEFAULT '',
  `hash` varchar(32) NOT NULL DEFAULT '',
  PRIMARY KEY (`player`,`hash`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.tunning
DROP TABLE IF EXISTS `tunning`;
CREATE TABLE IF NOT EXISTS `tunning` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `vehicle` int(11) NOT NULL DEFAULT '0',
  `slot` int(11) NOT NULL DEFAULT '0',
  `component` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(32) NOT NULL,
  `model` varchar(32) NOT NULL,
  `posX` float(10,0) NOT NULL DEFAULT '-136.0034',
  `posY` float NOT NULL DEFAULT '6198.949',
  `posZ` float NOT NULL DEFAULT '32.38448',
  `rotation` float NOT NULL DEFAULT '0',
  `money` int(11) NOT NULL DEFAULT '0',
  `bank` int(11) NOT NULL DEFAULT '3500',
  `health` int(11) NOT NULL DEFAULT '100',
  `armor` int(11) NOT NULL DEFAULT '0',
  `age` int(11) NOT NULL DEFAULT '14',
  `sex` int(11) NOT NULL DEFAULT '0',
  `faction` int(11) NOT NULL DEFAULT '0',
  `job` int(11) NOT NULL DEFAULT '0',
  `rank` int(11) NOT NULL DEFAULT '0',
  `radio` int(11) NOT NULL DEFAULT '0',
  `jailed` varchar(8) NOT NULL DEFAULT '-1,-1',
  `carKeys` varchar(32) NOT NULL DEFAULT '0,0,0,0,0',
  `documentation` int(11) NOT NULL DEFAULT '0',
  `licenses` varchar(32) NOT NULL DEFAULT '-1,-1,-1',
  `insurance` int(11) NOT NULL DEFAULT '0',
  `weaponLicense` int(11) NOT NULL DEFAULT '0',
  `houseRent` int(11) NOT NULL DEFAULT '0',
  `houseEntered` int(11) NOT NULL DEFAULT '0',
  `businessEntered` int(11) NOT NULL DEFAULT '0',
  `jobDeliver` int(11) NOT NULL DEFAULT '0',
  `jobCooldown` int(11) NOT NULL DEFAULT '0',
  `played` int(11) NOT NULL DEFAULT '0',
  `status` int(11) NOT NULL DEFAULT '1',
  `socialName` varchar(32) NOT NULL,
  `adminRank` int(11) NOT NULL DEFAULT '0',
  `adminname` varchar(24) NOT NULL DEFAULT '',
  `employeeCooldown` int(11) NOT NULL DEFAULT '0',
  `duty` int(11) NOT NULL DEFAULT '0',
  `killed` int(11) NOT NULL DEFAULT '0',
  `jobPoints` varchar(64) NOT NULL DEFAULT '0,0,0,0,0,0,0',
  `rolePoints` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.


-- Volcando estructura para tabla gtav.vehicles
DROP TABLE IF EXISTS `vehicles`;
CREATE TABLE IF NOT EXISTS `vehicles` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `model` varchar(32) NOT NULL,
  `posX` float NOT NULL,
  `posY` float NOT NULL,
  `posZ` float NOT NULL,
  `rotation` float NOT NULL,
  `firstColor` varchar(12) NOT NULL DEFAULT '0,0,0',
  `secondColor` varchar(12) NOT NULL DEFAULT '0,0,0',
  `dimension` int(11) NOT NULL DEFAULT '0',
  `engine` int(11) NOT NULL DEFAULT '0',
  `locked` int(11) NOT NULL DEFAULT '0',
  `faction` int(11) NOT NULL DEFAULT '0',
  `owner` varchar(32) NOT NULL,
  `plate` varchar(8) NOT NULL,
  `price` int(11) NOT NULL DEFAULT '0',
  `parking` int(11) NOT NULL DEFAULT '0',
  `parkedTime` int(11) NOT NULL DEFAULT '0',
  `gas` float NOT NULL DEFAULT '0',
  `kms` float NOT NULL DEFAULT '0',
  `colorType` int(11) NOT NULL DEFAULT '1',
  `pearlescent` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
