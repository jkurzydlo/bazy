-- MySQL dump 10.13  Distrib 8.3.0, for Win64 (x86_64)
--
-- Host: localhost    Database: przychodnia9
-- ------------------------------------------------------
-- Server version	8.3.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `address` (
  `id` int NOT NULL AUTO_INCREMENT,
  `street` varchar(45) DEFAULT NULL,
  `city` varchar(45) DEFAULT NULL,
  `buildingNumber` varchar(45) DEFAULT NULL,
  `postalCode` varchar(45) DEFAULT NULL,
  `type` enum('Zamieszkania','Zameldowania') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,NULL,'Nowy Sącz','123','12-222','Zamieszkania'),(2,NULL,'Kraków','45','22-123','Zameldowania'),(3,'Długa','Warszawa','123','22-222','Zamieszkania'),(4,NULL,'fdsfsd','12b','11-111','Zamieszkania'),(5,NULL,'Warszawa','123','22-334','Zamieszkania'),(6,NULL,'Poznań','123','11-234','Zamieszkania'),(7,NULL,'Szczecin','123','11-234','Zamieszkania'),(8,NULL,'Szczecin','123','11-111','Zamieszkania'),(9,NULL,'Kraków','123','11-324','Zamieszkania'),(10,NULL,'Warszawa','123','22-222','Zamieszkania'),(11,NULL,'Kraków','342','22-222','Zamieszkania'),(12,NULL,'Krakow','234','22-333','Zamieszkania'),(13,NULL,'Olsztyn','125','11-344','Zamieszkania'),(14,NULL,'Szczecin','123','22-434','Zamieszkania'),(15,NULL,'Poznań','235','11-343','Zamieszkania'),(16,NULL,'sg','234','33-333','Zamieszkania'),(17,NULL,'Warszawa','12','22-222','Zamieszkania'),(18,NULL,'Kraków','123','11-223','Zamieszkania'),(19,NULL,'Poznań','155','33-333','Zamieszkania');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `administrator`
--

DROP TABLE IF EXISTS `administrator`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `administrator` (
  `id` int NOT NULL AUTO_INCREMENT,
  `email` varchar(45) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Administrator_User1_idx` (`User_id`),
  CONSTRAINT `fk_Administrator_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `administrator`
--

LOCK TABLES `administrator` WRITE;
/*!40000 ALTER TABLE `administrator` DISABLE KEYS */;
/*!40000 ALTER TABLE `administrator` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointment`
--

DROP TABLE IF EXISTS `appointment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` varchar(45) DEFAULT NULL,
  `goal` varchar(45) DEFAULT NULL,
  `Notification_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`id`,`Notification_id`,`Patient_id`),
  KEY `fk_Appointment_Patient1_idx` (`Patient_id`),
  CONSTRAINT `fk_Appointment_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment`
--

LOCK TABLES `appointment` WRITE;
/*!40000 ALTER TABLE `appointment` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointment_has_receptionist`
--

DROP TABLE IF EXISTS `appointment_has_receptionist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointment_has_receptionist` (
  `Appointment_id` int NOT NULL,
  `Appointment_Notification_id` int NOT NULL,
  `Appointment_Patient_id` int NOT NULL,
  `Receptionist_id` int NOT NULL,
  `Receptionist_User_id` int NOT NULL,
  PRIMARY KEY (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`,`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_Appointment_has_Receptionist_Receptionist1_idx` (`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_Appointment_has_Receptionist_Appointment1_idx` (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  CONSTRAINT `fk_Appointment_has_Receptionist_Appointment1` FOREIGN KEY (`Appointment_id`, `Appointment_Notification_id`, `Appointment_Patient_id`) REFERENCES `appointment` (`id`, `Notification_id`, `Patient_id`),
  CONSTRAINT `fk_Appointment_has_Receptionist_Receptionist1` FOREIGN KEY (`Receptionist_id`, `Receptionist_User_id`) REFERENCES `receptionist` (`id`, `User_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment_has_receptionist`
--

LOCK TABLES `appointment_has_receptionist` WRITE;
/*!40000 ALTER TABLE `appointment_has_receptionist` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointment_has_receptionist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `category`
--

DROP TABLE IF EXISTS `category`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `category` (
  `category_id` int NOT NULL,
  `name` varchar(255) NOT NULL,
  PRIMARY KEY (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `category`
--

LOCK TABLES `category` WRITE;
/*!40000 ALTER TABLE `category` DISABLE KEYS */;
/*!40000 ALTER TABLE `category` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diesease_medicine`
--

DROP TABLE IF EXISTS `diesease_medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diesease_medicine` (
  `diesease_id` int NOT NULL,
  `medicine_id` int NOT NULL,
  PRIMARY KEY (`diesease_id`,`medicine_id`),
  KEY `diesease_medicine_medicine` (`medicine_id`),
  CONSTRAINT `diesease_medicine_diesease` FOREIGN KEY (`diesease_id`) REFERENCES `disease` (`id`),
  CONSTRAINT `diesease_medicine_medicine` FOREIGN KEY (`medicine_id`) REFERENCES `medicine` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diesease_medicine`
--

LOCK TABLES `diesease_medicine` WRITE;
/*!40000 ALTER TABLE `diesease_medicine` DISABLE KEYS */;
/*!40000 ALTER TABLE `diesease_medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `disease`
--

DROP TABLE IF EXISTS `disease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `disease` (
  `name` varchar(45) NOT NULL,
  `dateFrom` date DEFAULT NULL,
  `dateTo` date DEFAULT NULL,
  `isEnded` tinyint DEFAULT NULL,
  `comments` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=118 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disease`
--

LOCK TABLES `disease` WRITE;
/*!40000 ALTER TABLE `disease` DISABLE KEYS */;
INSERT INTO `disease` VALUES ('Nazwa',NULL,NULL,NULL,'dsads',96),('Katar','0001-01-01',NULL,NULL,NULL,97),('Zapalenie spojówek','0001-01-01',NULL,NULL,NULL,98),('Zawał','0001-01-01',NULL,NULL,NULL,99),('Zapalenie płuc','0001-01-01',NULL,NULL,NULL,100),('Złamany palec','2024-04-18',NULL,NULL,NULL,101),('Zapalenie oskrzeli','2024-04-18',NULL,NULL,NULL,102),('nowe','2024-04-18',NULL,NULL,NULL,103),('nowe3','2024-03-27',NULL,NULL,NULL,104),('Katar','2024-04-22',NULL,NULL,NULL,105),('nowe','2024-04-22',NULL,NULL,NULL,106),('Katar','2024-04-22',NULL,NULL,NULL,107),('qwe','2024-04-25',NULL,NULL,NULL,108),('fsd','2024-04-25',NULL,NULL,NULL,109),('dsf','2024-04-25',NULL,NULL,NULL,110),('Marskość wątroby','2024-04-28',NULL,NULL,NULL,111),('Schorzenie #1','2024-04-28',NULL,NULL,NULL,112),('Schorzenie #25','2024-04-29',NULL,NULL,NULL,113),('Schorzenie #1','2024-04-29',NULL,NULL,NULL,114),('Schorzenie #3','2024-04-29',NULL,NULL,NULL,115),('Schorzenie #15','2024-04-29',NULL,NULL,NULL,116),('Schorzeni #1','2024-05-02',NULL,NULL,NULL,117);
/*!40000 ALTER TABLE `disease` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor`
--

DROP TABLE IF EXISTS `doctor`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Doctor_User1_idx` (`User_id`),
  CONSTRAINT `fk_Doctor_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor`
--

LOCK TABLES `doctor` WRITE;
/*!40000 ALTER TABLE `doctor` DISABLE KEYS */;
INSERT INTO `doctor` VALUES (1,'fsd','dsfsd',NULL,8),(2,'Adam','Kowalski',NULL,10),(3,'anna','kowalska',NULL,11),(4,'Adam','Nowak',NULL,12),(5,'zx','zx',NULL,13),(6,'asd','sad',NULL,14),(12,'tre','hte',NULL,28),(13,'zxc','zxc',NULL,31),(14,'Jan','Kowalski',NULL,34),(18,'zxc','zxc',NULL,52),(20,'zxcc','jhjh',NULL,60),(21,'qw','qw',NULL,77),(22,'q','q',NULL,232);
/*!40000 ALTER TABLE `doctor` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_has_office`
--

DROP TABLE IF EXISTS `doctor_has_office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_has_office` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Office_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Office_id`),
  KEY `fk_Doctor_has_Office_Office1_idx` (`Office_id`),
  KEY `fk_Doctor_has_Office_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Office_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Office_Office1` FOREIGN KEY (`Office_id`) REFERENCES `office` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_has_office`
--

LOCK TABLES `doctor_has_office` WRITE;
/*!40000 ALTER TABLE `doctor_has_office` DISABLE KEYS */;
INSERT INTO `doctor_has_office` VALUES (12,28,1),(22,232,3);
/*!40000 ALTER TABLE `doctor_has_office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_has_patient`
--

DROP TABLE IF EXISTS `doctor_has_patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_has_patient` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Patient_id`),
  KEY `fk_Doctor_has_Patient_Patient1_idx` (`Patient_id`),
  KEY `fk_Doctor_has_Patient_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Patient_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Patient_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_has_patient`
--

LOCK TABLES `doctor_has_patient` WRITE;
/*!40000 ALTER TABLE `doctor_has_patient` DISABLE KEYS */;
INSERT INTO `doctor_has_patient` VALUES (22,232,117),(22,232,118),(22,232,119);
/*!40000 ALTER TABLE `doctor_has_patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `doctor_specialization`
--

DROP TABLE IF EXISTS `doctor_specialization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `doctor_specialization` (
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `Specialization_id` int NOT NULL,
  PRIMARY KEY (`Doctor_id`,`Doctor_User_id`,`Specialization_id`),
  KEY `fk_Doctor_has_Specialization_Specialization1_idx` (`Specialization_id`),
  KEY `fk_Doctor_has_Specialization_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_Doctor_has_Specialization_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Doctor_has_Specialization_Specialization1` FOREIGN KEY (`Specialization_id`) REFERENCES `specialization` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor_specialization`
--

LOCK TABLES `doctor_specialization` WRITE;
/*!40000 ALTER TABLE `doctor_specialization` DISABLE KEYS */;
INSERT INTO `doctor_specialization` VALUES (1,8,1),(2,10,1),(14,34,1),(18,52,1),(21,77,1),(22,232,1),(3,11,2),(4,12,2),(5,13,2),(6,14,2),(12,28,2),(20,60,2),(13,31,3);
/*!40000 ALTER TABLE `doctor_specialization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medicine`
--

DROP TABLE IF EXISTS `medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicine` (
  `name` varchar(45) DEFAULT NULL,
  `dose` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `amount` int DEFAULT NULL,
  `comments` varchar(45) DEFAULT NULL,
  `fraction` float DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=274 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicine`
--

LOCK TABLES `medicine` WRITE;
/*!40000 ALTER TABLE `medicine` DISABLE KEYS */;
INSERT INTO `medicine` VALUES ('dfdfd','ds',143,2,'sdfsd',NULL),('er','gfre',144,3,'grbgfr',NULL),('noway','fds',145,3,'gd',NULL),('dsf','gfsd',146,3,'gsgfd',NULL),('fsd','dsf',147,3,'dsf',NULL),('dsf','fds',148,3,'df',NULL),('dsf','dsf',149,3,NULL,NULL),('s','s',150,2,'d',NULL),('d','f',151,3,'d',NULL),('anbs','sadf',152,3,'d',NULL),('fdg','gdf',153,4,'dfgdf',NULL),('gfd','gd',154,4,NULL,NULL),('sdf','dsf',155,2,'sdffsd',NULL),('sdf','fsd',156,2,NULL,NULL),('Naproxem','25mg po posiłku',157,2,NULL,0.5),('Magnez','2 tabletki dziennie',158,5,NULL,1),('Lek#3','2 tabletki',159,3,'Kurację kontynuować 2 miesiące',0.5),('Lek #4','40 ml',160,1,NULL,0),('lek#2','fds',161,3,NULL,0.3),('dsf','fds',162,2,'fsd',0.43),('ds','fds',163,4,'df',0.56),('Lek#1','25mg',164,2,NULL,1),('50ml','1 tabletka po posiłku',165,1,'sdf',0.2),('sad','dsa',166,2,'das',0.33),('sdf','fsd',167,2,'csd',0.23),('sad','fds',168,2,'2',0.02),('23','2323',169,3,'23',0.23),('e','e',170,2,'3',0.03),('d','d',171,5,'5',0.05),('2','2',172,2,'2',0.02),('3','3',173,3,'3',0.03),('d','s',174,2,'2',0.02),('3','4',175,4,'d',0.04),('s','d',176,2,'2',0.02),('s','s',177,4,'4',0.04),('5','5',178,5,'5',0.05),('2','2',179,2,'3',0.02),('f','f',180,3,'d',0.34),('2','2',181,2,'2',0.02),('2','2',182,2,'2',0.02),('2','2',183,2,NULL,0.02),('ert','er',184,4,'5',0.05),('dgr','df',185,5,'f',0.04),('t','t',186,7,'fh',0.6),('2','2',187,2,'2',0.02),('3','33',188,3,'3',0.03),('2','2',189,2,'2',0.53),('3','3',190,3,NULL,0.12),('2','2',191,2,'2',0.02),('12','12',192,12,'12',0.12),('2','2',193,2,'2',0.02),('1','2',194,3,'3',0.03),('mndf','fds',195,2,NULL,1),('dsf','fsdfds',196,3,NULL,0.5),('z','2',197,2,'2',0.02),('lek#1','25mg',198,1,NULL,0.5),('lek #2','40mg',199,5,'dsfsd',1),('Lek#1','25mg',200,2,'sdf',0.5),('Lek#2','50mg',201,3,'sdf',0.5),('34','34',202,34,'34',0.34),('Lek#1','25mg',203,1,NULL,0.5),('Lek#3','23mg',204,3,NULL,0.26),('Lek#1','5ml',205,1,'sdf',0.5),('Lek #4','25ml',206,5,'dg',1),('Lek #47','1 tabletka rano',207,1,'df',1),('Lek#33','2 tabletki',208,2,NULL,1),('Lek #1','25mg',209,2,NULL,0.5),('Lek #2','50mg',210,5,NULL,1),('Lek #12','50mg',211,2,'sad',0.5),('Lek #3','40mg',212,5,NULL,1),('Lek#45','2 tabletki',213,5,'sdfsf',1),('Lek #1','25mg',214,2,NULL,1),('Lek #2','1 tabletka',215,4,NULL,0.5),('Lek #65','23mg',216,4,NULL,0.5),('Lek #33','25mg',217,2,NULL,0.2),('sdf','342',218,43,'df',0.34),('Lek#1','25mg',219,5,NULL,0.2),('2','2',220,2,'2',0.02),('3','3',221,3,'3',0.03),('4','4',222,4,'4',0.04),('5','5',223,5,'5',0.05),('6','66',224,6,'6',0.06),('Lek#1','25mg',200,5,NULL,0.5),('Lek#2','40mg',226,2,'dsf',1),('Lek #33','35mg',227,4,NULL,0.4),('Lek#1','25mg',228,2,'sdfsd',1),('Lek#2','50mg',229,5,'dfggfd',0.5),('Lek#4','50mg',230,5,NULL,0.5),('Lek #45','25ml',231,5,NULL,0.5),('fds','fds',232,23,'sdf',0.5),('df','dfg',233,45,'dfg',0.45),('ewrer','ger',234,3,'3',0.03),('4','4',235,4,NULL,0.04),('wer','ewr',236,25,'25',0.25),('3','3',237,3,'3',0.03),('Lek #5','50mg',238,2,'sdf',1),('5','5',239,5,'5',0.5),('Lek #6','50mg',240,5,NULL,1),('5','5',241,5,'5',0.05),('6','6',243,6,'6',0.06),('6','6',244,6,'6',0.06),('12','12',245,12,'12',0.12),('Nalgesin','12',246,12,'0',0.5),('Amlozek','23',247,10,NULL,1),('Amlozek','50',248,5,NULL,0.5),('Rulid','50mg',249,5,'12',0.5),('Nalgesin Forte','25mg',250,2,NULL,0.5),('Amlozek','1',251,1,NULL,0.01),('Sotahexal 80','25mg',252,5,'24',0.25),('Rulid','50ml',253,5,NULL,0.5),('Carboplatin Pfizer','25mg',254,2,NULL,0.5),('Loratadyna Galena','2ml',255,2,NULL,0.5),('Zoledronic acid Fresenius Kabi','50',256,5,NULL,0.2),('Nalgesin Forte','25mg',257,2,'30',1),('Amlozek','2',258,2,'2',0.02),('Ursopol','4',259,4,'4',0.04),('Coffepirine Tabletki od bólu głowy','25',260,25,'25',0.25),('Xyrem','4',261,4,'4',0.04),('Twinrix Adult','25',262,25,'25',0.25),('Aprovel','4',263,4,NULL,0.04),('Indapen SR','2',264,22,'2',0.02),('Xanax SR','23',265,1,'zd',0.1),('Ascorvita','2',266,2,'2',0.02),('Szczepionka tężcowa adsorbowana (T)','24mg',267,2,'2',0.25),('Aprovel','2',268,2,'2',0.02),('Zoledronic acid Fresenius Kabi','1',269,1,'1',0.01),('Amlozek','11',270,1,'1',0.01),('Rulid','2222',271,2,'2',0.02),('Twinrix Adult','1',272,1,'1',0.01),('Nalgesin Forte','3',273,3,'3',0.33);
/*!40000 ALTER TABLE `medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notification`
--

DROP TABLE IF EXISTS `notification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notification` (
  `id` int NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  `hour` time DEFAULT NULL,
  `isSent` tinyint DEFAULT NULL,
  `Appointment_id` int NOT NULL,
  `Appointment_Notification_id` int NOT NULL,
  `Appointment_Patient_id` int NOT NULL,
  PRIMARY KEY (`id`,`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  KEY `fk_Notification_Appointment1_idx` (`Appointment_id`,`Appointment_Notification_id`,`Appointment_Patient_id`),
  CONSTRAINT `fk_Notification_Appointment1` FOREIGN KEY (`Appointment_id`, `Appointment_Notification_id`, `Appointment_Patient_id`) REFERENCES `appointment` (`id`, `Notification_id`, `Patient_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
/*!40000 ALTER TABLE `notification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `office`
--

DROP TABLE IF EXISTS `office`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `office` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Number` int NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `office`
--

LOCK TABLES `office` WRITE;
/*!40000 ALTER TABLE `office` DISABLE KEYS */;
INSERT INTO `office` VALUES (1,12),(2,12),(3,12);
/*!40000 ALTER TABLE `office` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient`
--

DROP TABLE IF EXISTS `patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient` (
  `pesel` char(11) DEFAULT NULL,
  `phoneNumber` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `nextVisit` date DEFAULT NULL,
  `lastVisit` date DEFAULT NULL,
  `sex` enum('K','M') NOT NULL,
  `birthDate` date DEFAULT NULL,
  `secondName` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=120 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
INSERT INTO `patient` VALUES (NULL,NULL,NULL,105,'Adam','Nowak',NULL,NULL,'M','2024-04-22',NULL),(NULL,NULL,'a@a.pl',106,'Roman','Zieliński',NULL,NULL,'M','2024-04-22',NULL),('56387678903','534908765',NULL,107,'Jan','Kowalski',NULL,NULL,'M','1991-12-10',NULL),('87673567801','667887546','a@a.pl',108,'Adam','Nowak',NULL,NULL,'M','1987-01-11',NULL),('12345675694','223456864',NULL,109,'Roman','Zieliński',NULL,NULL,'M','2024-01-02',NULL),('23434567893',NULL,'a@a.pl',110,'Adam','Kowalski',NULL,NULL,'M','2010-01-11',NULL),('32676756724',NULL,NULL,111,'Adam','Zieliński',NULL,NULL,'M','2024-04-23',NULL),('76765645687',NULL,'g@j.pl',112,'Jan','Nowak',NULL,NULL,'M','2024-04-23',NULL),('01312906516','223786545','a@a.pl',113,'Adam','Nowacki',NULL,NULL,'M','2024-04-24',NULL),('11767865456',NULL,NULL,114,'Joanna','Kowalska',NULL,NULL,'K','1980-01-01','Maria'),('36745676381','332346785','a.kowalczyk@gmail.com',115,'Adam','Kowalczyk',NULL,NULL,'M','2024-04-28',NULL),('01312906516',NULL,NULL,116,'grzegorz','Rumak',NULL,NULL,'M','2024-04-29',NULL),('01312906516',NULL,NULL,117,'Roman','Zieliński',NULL,NULL,'M','2024-04-29',NULL),('70051251144',NULL,NULL,118,'Grzegorz','Nowak',NULL,NULL,'M','2024-04-29',NULL),('51012056651',NULL,NULL,119,'Adam','Kowalczyk',NULL,NULL,'M','2024-04-29',NULL);
/*!40000 ALTER TABLE `patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_address`
--

DROP TABLE IF EXISTS `patient_address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_address` (
  `Patient_id` int NOT NULL,
  `Address_id` int NOT NULL,
  PRIMARY KEY (`Patient_id`,`Address_id`),
  KEY `fk_Patient_has_Address_Address1_idx` (`Address_id`),
  KEY `fk_Patient_has_Address_Patient1_idx` (`Patient_id`),
  CONSTRAINT `fk_Patient_has_Address_Address1` FOREIGN KEY (`Address_id`) REFERENCES `address` (`id`),
  CONSTRAINT `fk_Patient_has_Address_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_address`
--

LOCK TABLES `patient_address` WRITE;
/*!40000 ALTER TABLE `patient_address` DISABLE KEYS */;
INSERT INTO `patient_address` VALUES (117,17),(118,18),(119,19);
/*!40000 ALTER TABLE `patient_address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patient_diesease`
--

DROP TABLE IF EXISTS `patient_diesease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patient_diesease` (
  `Disease_id` int NOT NULL,
  `Patient_id` int NOT NULL,
  PRIMARY KEY (`Disease_id`,`Patient_id`),
  KEY `fk_Disease_has_Patient_Patient1_idx` (`Patient_id`),
  KEY `fk_Disease_has_Patient_Disease1_idx` (`Disease_id`),
  CONSTRAINT `fk_Disease_has_Patient_Disease1` FOREIGN KEY (`Disease_id`) REFERENCES `disease` (`id`),
  CONSTRAINT `fk_Disease_has_Patient_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient_diesease`
--

LOCK TABLES `patient_diesease` WRITE;
/*!40000 ALTER TABLE `patient_diesease` DISABLE KEYS */;
INSERT INTO `patient_diesease` VALUES (99,108),(114,117),(115,118),(116,119),(117,119);
/*!40000 ALTER TABLE `patient_diesease` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription`
--

DROP TABLE IF EXISTS `prescription`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription` (
  `id` int NOT NULL AUTO_INCREMENT,
  `dateOfPrescription` date DEFAULT NULL,
  `realisationDate` date DEFAULT NULL,
  `code` varchar(45) DEFAULT NULL,
  `Patient_id` int NOT NULL,
  `pdf` varchar(100) DEFAULT NULL,
  `doctor_id` int DEFAULT NULL,
  `doctor_user_id` int DEFAULT NULL,
  PRIMARY KEY (`id`,`Patient_id`),
  UNIQUE KEY `idRecepty_UNIQUE` (`id`),
  KEY `fk_Prescription_Patient1_idx` (`Patient_id`),
  KEY `pr_dc_fk` (`doctor_id`,`doctor_user_id`),
  CONSTRAINT `fk_Prescription_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`),
  CONSTRAINT `pr_dc_fk` FOREIGN KEY (`doctor_id`, `doctor_user_id`) REFERENCES `doctor` (`id`, `User_id`)
) ENGINE=InnoDB AUTO_INCREMENT=119 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
INSERT INTO `prescription` VALUES (108,'2024-05-02','2024-05-02','09cb1041e744418684a01e',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\108252024.pdf',22,232),(109,'2024-05-02','2024-05-02','3f39237eb81b4893bf17b5',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\109252024.pdf',22,232),(110,'2024-05-02','2024-05-02','6445c2c5e512475995b04b',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\110252024.pdf',22,232),(111,'2024-05-02','2024-05-02','2cf2bffdde4a423aafa8a0',118,NULL,22,232),(112,'2024-05-02','2024-05-02','9ee5be17eb2047a6ab3628',118,'112002024.pdf',22,232),(113,'2024-05-02','2024-05-02','9188938fddb04958ae4cf5',117,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\113252024.pdf',22,232),(114,'2024-05-02','2024-05-02','afb2382163464c4991b113',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\114252024.pdf',22,232),(115,'2024-05-02','2024-05-02','5cd32dc4489c41719a14a1',119,'115252024.pdf',22,232),(116,'2024-05-02','2024-05-02','1089399d13fd4bf5845b3b',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\116252024.pdf',22,232),(117,'2024-05-03','2024-05-03','a07364fa2ba149dcad1e72',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\117352024.pdf',22,232),(118,'2024-05-03','2024-05-03','5e4a74b732a34c9e839523',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\118352024.pdf',22,232);
/*!40000 ALTER TABLE `prescription` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prescription_medicine`
--

DROP TABLE IF EXISTS `prescription_medicine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prescription_medicine` (
  `Prescription_id` int NOT NULL,
  `Prescription_Patient_id` int NOT NULL,
  `Medicine_id` int NOT NULL,
  PRIMARY KEY (`Prescription_id`,`Prescription_Patient_id`,`Medicine_id`),
  KEY `fk_Prescription_has_Medicine_Medicine1_idx` (`Medicine_id`),
  KEY `fk_Prescription_has_Medicine_Prescription1_idx` (`Prescription_id`,`Prescription_Patient_id`),
  CONSTRAINT `fk_Prescription_has_Medicine_Medicine1` FOREIGN KEY (`Medicine_id`) REFERENCES `medicine` (`id`),
  CONSTRAINT `fk_Prescription_has_Medicine_Prescription1` FOREIGN KEY (`Prescription_id`, `Prescription_Patient_id`) REFERENCES `prescription` (`id`, `Patient_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription_medicine`
--

LOCK TABLES `prescription_medicine` WRITE;
/*!40000 ALTER TABLE `prescription_medicine` DISABLE KEYS */;
INSERT INTO `prescription_medicine` VALUES (108,118,262),(108,118,263),(109,118,264),(110,119,265),(111,118,266),(112,118,267),(113,117,268),(114,118,269),(115,119,270),(116,118,271),(117,119,273),(118,119,273);
/*!40000 ALTER TABLE `prescription_medicine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `receptionist`
--

DROP TABLE IF EXISTS `receptionist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `receptionist` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `surname` varchar(45) DEFAULT NULL,
  `User_id` int NOT NULL,
  PRIMARY KEY (`id`,`User_id`),
  KEY `fk_Receptionist_User1_idx` (`User_id`),
  CONSTRAINT `fk_Receptionist_User1` FOREIGN KEY (`User_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receptionist`
--

LOCK TABLES `receptionist` WRITE;
/*!40000 ALTER TABLE `receptionist` DISABLE KEYS */;
/*!40000 ALTER TABLE `receptionist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `referral`
--

DROP TABLE IF EXISTS `referral`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `referral` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` varchar(100) DEFAULT NULL,
  `patient_id` int NOT NULL,
  `doctor_id` int DEFAULT NULL,
  `doctor_user_id` int DEFAULT NULL,
  `information` varchar(300) DEFAULT NULL,
  `medical_entity` varchar(300) DEFAULT NULL,
  `Pdf` varchar(100) DEFAULT NULL,
  `date` date DEFAULT NULL,
  `disease` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`,`patient_id`),
  KEY `patient_id` (`patient_id`),
  KEY `doctor_id` (`doctor_id`,`doctor_user_id`),
  CONSTRAINT `referral_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `patient` (`id`),
  CONSTRAINT `referral_ibfk_2` FOREIGN KEY (`doctor_id`, `doctor_user_id`) REFERENCES `doctor` (`id`, `User_id`)
) ENGINE=InnoDB AUTO_INCREMENT=65 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `referral`
--

LOCK TABLES `referral` WRITE;
/*!40000 ALTER TABLE `referral` DISABLE KEYS */;
INSERT INTO `referral` VALUES (60,'0593d668096411efb9af',119,22,232,'','Zakład Usług Lekarskich \"ZDROWIE\" Spółka z ograniczoną odpowiedzialnością','skierowanie60352024.pdf','2024-05-03','Złamanie nogi'),(61,'fc1d9db6096411efb9af',118,22,232,'fds','Makowska & Kucharski DENT Spółka Cywilna','skierowanie61352024.pdf','2024-05-03','sdf'),(62,'622cf82c096511efb9af',118,22,232,'dsa','Krzysztof Mikluszka','skierowanie62352024.pdf','2024-05-03','sad'),(63,'a0141e3b096511efb9af',118,22,232,'23','Zakład Usług Lekarskich \"ZDROWIE\" Spółka z ograniczoną odpowiedzialnością','C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\skierowanie63352024.pdf','2024-05-03','23'),(64,'56c24e3d096611efb9af',118,22,232,'greger','XMED PRYWATNA OPIEKA MEDYCZNA I SZKOLENIOWA DARIUSZ STRACHOTA','C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\skierowanie64352024.pdf','2024-05-03','wfds');
/*!40000 ALTER TABLE `referral` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialization`
--

DROP TABLE IF EXISTS `specialization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `specialization` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialization`
--

LOCK TABLES `specialization` WRITE;
/*!40000 ALTER TABLE `specialization` DISABLE KEYS */;
INSERT INTO `specialization` VALUES (1,'Chirurgia'),(2,'Onkologia'),(3,'Alergologia');
/*!40000 ALTER TABLE `specialization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `id` int NOT NULL AUTO_INCREMENT,
  `type` enum('recepcjonista','admin','lekarz') NOT NULL,
  `login` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `name` varchar(45) NOT NULL,
  `surname` varchar(45) NOT NULL,
  `hash` varchar(512) DEFAULT NULL,
  `firstLogin` tinyint(1) NOT NULL DEFAULT '0',
  `lastLogin` datetime DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=2536 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (5,'lekarz','fdfd','123','dsfdq','dsg','doctor',0,NULL),(6,'lekarz','adasd','0000\0\0','abc','cds',NULL,1,NULL),(7,'recepcjonista','fe','176112147214O','cx','gr',NULL,1,NULL),(8,'lekarz','fa','7847U91','ab','sa',NULL,1,NULL),(9,'lekarz','b','a','b','b',NULL,0,NULL),(10,'lekarz','AK02615','123','Adam','Kowalski','1',0,NULL),(11,'lekarz','ak04103','anna123','anna','kowalska','0',0,NULL),(12,'lekarz','an00227','6622LVe','Adam','Nowak','$2a$11$SKxMUhxTaZaP23QPkkSS8.wriA1TYM5nDjK8jWy6FdJ0ibsmuE13C',1,NULL),(13,'lekarz','zz05353','lmao','zx','zx','$2a$11$2ureyDLn/FEzCJ74NhAG0.BNXagUAeFxG7DcoZF3O.ohfAr08uqS2',0,NULL),(14,'lekarz','as04865','1382!.4','asd','sad','$2a$11$MmditWc.JWH.N6TYJVAr/.Q6K4l2UtYFSz/pLjFdoDH9ZSX6GZen.',0,NULL),(17,'admin','admin','admin','admin','admin','$2a$11$hk7hpRiflna5nQQx.ZqDhONisX2c4.nl5594UFVJNpnkBg2dhgJgG',1,'2024-04-11 15:31:44'),(28,'lekarz','fds','8634Vp}','fsdfsdfs','fsd','$2a$11$ZJky9t05HUAW5HpteIa/se85i6WwbEHwgtQnqGJcX0gtSeHY6DPQ2',1,NULL),(31,'lekarz','zz38482','382325Y','zxc','zxc','$2a$11$/ODjx1pj1p3fRl3QWo58zuKua/8BuHP14Si/ycjyRnzHY/O59zOpe',1,NULL),(34,'lekarz','am12345','12345','Adam','Nowak','$2a$11$V0sXND/GlTSmgvlBJw06H.lENoP13DlrdZhAtYC6ebEj5v.nEAi2e',0,'2024-04-08 19:34:53'),(52,'lekarz','zz64830','123','zxc','zxc','$2a$11$pG9CinSzogaaiwz/qwtH3uckIzpE5vF9ibEchf/ieaV39EjTFFOnG',0,'2024-04-09 09:27:18'),(60,'lekarz','gd','123','fdfgd','fdgd','$2a$11$A3wNExDiSCPts36DU4GlvOIb4cNULjQcq.JomKLo7TGdSLO49/9CK',0,'2024-04-11 15:46:47'),(77,'lekarz','qq27265','123','qw','qw','$2a$11$xK2Ujkg1WOyBOxoC3KUZF.80fbB9jTC1fevd9ia8VFpZANyLp2biK',0,'2024-04-11 15:27:16'),(232,'lekarz','q','q','q','q','$2a$11$tcPnkTwEyXL2LM0vuCLfEu1OPfUhTTQl2QPwL8cvzJCGqWpdYnO5i',0,'2024-05-03 18:41:40');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workhours`
--

DROP TABLE IF EXISTS `workhours`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workhours` (
  `id` int NOT NULL AUTO_INCREMENT,
  `start` datetime(5) DEFAULT NULL,
  `end` datetime(5) DEFAULT NULL,
  `Receptionist_id` int NOT NULL,
  `Receptionist_User_id` int NOT NULL,
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  PRIMARY KEY (`id`,`Receptionist_id`,`Receptionist_User_id`,`Doctor_id`,`Doctor_User_id`),
  KEY `fk_WorkHours_Receptionist1_idx` (`Receptionist_id`,`Receptionist_User_id`),
  KEY `fk_WorkHours_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_WorkHours_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_WorkHours_Receptionist1` FOREIGN KEY (`Receptionist_id`, `Receptionist_User_id`) REFERENCES `receptionist` (`id`, `User_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workhours`
--

LOCK TABLES `workhours` WRITE;
/*!40000 ALTER TABLE `workhours` DISABLE KEYS */;
/*!40000 ALTER TABLE `workhours` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-03 18:45:36
