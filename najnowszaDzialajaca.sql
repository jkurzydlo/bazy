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
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,NULL,'Nowy Sącz','123','12-222','Zamieszkania'),(2,NULL,'Kraków','45','22-123','Zameldowania'),(3,'Długa','Warszawa','123','22-222','Zamieszkania'),(4,NULL,'fdsfsd','12b','11-111','Zamieszkania'),(5,NULL,'Warszawa','123','22-334','Zamieszkania'),(6,NULL,'Poznań','123','11-234','Zamieszkania'),(7,NULL,'Szczecin','123','11-234','Zamieszkania'),(8,NULL,'Szczecin','123','11-111','Zamieszkania'),(9,NULL,'Kraków','123','11-324','Zamieszkania'),(10,NULL,'Warszawa','123','22-222','Zamieszkania'),(11,NULL,'Kraków','342','22-222','Zamieszkania'),(12,NULL,'Krakow','234','22-333','Zamieszkania'),(13,NULL,'Olsztyn','125','11-344','Zamieszkania'),(14,NULL,'Szczecin','123','22-434','Zamieszkania'),(15,NULL,'Poznań','235','11-343','Zamieszkania'),(16,NULL,'sg','234','33-333','Zamieszkania'),(17,NULL,'Warszawa','12','22-222','Zamieszkania'),(18,NULL,'Kraków','123','11-223','Zamieszkania'),(19,NULL,'Poznań','155','33-333','Zamieszkania'),(20,NULL,'Nowy Sącz','123','22-213','Zamieszkania');
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
  `date` datetime DEFAULT NULL,
  `goal` varchar(45) DEFAULT NULL,
  `Patient_id` int NOT NULL,
  `doctor_user_id` int NOT NULL,
  `doctor_id` int NOT NULL,
  `id` int NOT NULL AUTO_INCREMENT,
  `dateTo` datetime DEFAULT NULL,
  PRIMARY KEY (`id`,`Patient_id`,`doctor_id`,`doctor_user_id`),
  KEY `fk_Appointment_Patient1_idx` (`Patient_id`),
  KEY `doctor_id` (`doctor_id`,`doctor_user_id`),
  CONSTRAINT `appointment_ibfk_1` FOREIGN KEY (`doctor_id`, `doctor_user_id`) REFERENCES `doctor` (`id`, `User_id`),
  CONSTRAINT `fk_Appointment_Patient1` FOREIGN KEY (`Patient_id`) REFERENCES `patient` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=95 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointment`
--

LOCK TABLES `appointment` WRITE;
/*!40000 ALTER TABLE `appointment` DISABLE KEYS */;
INSERT INTO `appointment` VALUES ('2024-05-20 15:00:00','cośtam',107,232,22,21,NULL),('2024-05-13 16:00:00','cośtam',113,232,22,22,NULL),('2024-05-15 13:00:00','cośtam',105,232,22,23,NULL),('2024-05-21 20:00:00','cośtam',107,232,22,24,NULL),('2024-05-21 20:00:00','cośtam',114,232,22,25,NULL),('2024-05-21 12:00:00','cośtam',114,232,22,26,NULL),('2024-05-27 12:00:00','cośtam',105,232,22,27,NULL),('2024-05-27 13:00:00','cośtam',105,232,22,28,NULL),('2024-05-14 20:00:00','cośtam',107,232,22,29,NULL),('2024-05-14 21:00:00','cośtam',112,232,22,30,NULL),('2024-05-20 09:00:00','cośtam',105,232,22,31,NULL),('2024-05-14 12:00:00','cośtam',109,232,22,32,NULL),('2024-05-21 22:00:00','cośtam',105,232,22,33,NULL),('2024-05-20 12:00:00','cośtam',112,232,22,34,NULL),('2024-05-20 11:00:00','cośtam',112,232,22,35,NULL),('2024-05-27 16:00:00','cośtam',105,232,22,36,NULL),('2024-05-22 11:00:00','cośtam',105,232,22,37,NULL),('2024-05-15 08:00:00','cośtam',115,232,22,38,NULL),('2024-05-27 09:00:00','cośtam',105,232,22,39,NULL),('2024-06-03 12:00:00','cośtam',105,232,22,40,NULL),('2024-06-03 15:00:00','cośtam',113,232,22,41,NULL),('2024-05-21 11:00:00','cośtam',105,232,22,42,NULL),('2024-05-13 14:00:00','cośtam',105,232,22,43,NULL),('2024-05-27 10:00:00','cośtam',112,232,22,44,NULL),('2024-05-13 09:00:00','cośtam',111,232,22,45,NULL),('2024-06-03 09:00:00','cośtam',105,232,22,46,NULL),('2024-06-03 11:00:00','cośtam',112,232,22,47,NULL),('2024-05-13 10:00:00','cośtam',105,232,22,48,NULL),('2024-05-20 16:00:00','cośtam',105,232,22,49,NULL),('2024-05-13 11:00:00','cośtam',105,232,22,50,NULL),('2024-06-10 14:00:00','cośtam',105,232,22,51,NULL),('2024-07-08 09:00:00','cośtam',105,232,22,52,NULL),('2024-05-27 14:00:00','cośtam',105,232,22,53,NULL),('2024-05-29 11:00:00','cośtam',105,232,22,54,NULL),('2024-06-03 14:00:00','cośtam',105,232,22,55,NULL),('2025-05-07 08:00:00','cośtam',105,232,22,56,NULL),('2024-05-27 15:00:00','cośtam',112,232,22,57,NULL),('2024-05-13 13:00:00','cośtam',105,232,22,58,NULL),('2024-07-01 11:00:00','cośtam',105,232,22,59,NULL),('2024-07-01 15:00:00','cośtam',105,232,22,60,NULL),('2024-07-03 14:00:00','cośtam',105,232,22,61,NULL),('2024-08-13 20:00:00','cośtam',113,232,22,62,NULL),('2024-05-22 14:00:00','cośtam',105,232,22,63,NULL),('2024-06-04 14:00:00','cośtam',105,232,22,64,NULL),('2024-06-05 12:00:00','cośtam',105,232,22,65,NULL),('2024-06-05 11:00:00','cośtam',105,232,22,66,NULL),('2024-06-17 12:00:00','cośtam',113,232,22,67,NULL),('2024-07-01 13:00:00','cośtam',105,232,22,68,NULL),('2024-07-15 12:00:00','cośtam',105,232,22,69,NULL),('2024-07-15 14:00:00','cośtam',105,232,22,70,NULL),('2024-05-20 14:00:00','cośtam',105,232,22,71,NULL),('2024-08-19 14:00:00','cośtam',105,232,22,72,NULL),('2024-08-19 12:00:00','cośtam',105,232,22,73,NULL),('2024-07-09 14:00:00','cośtam',108,232,22,74,NULL),('2024-08-07 11:00:00','cośtam',105,8,1,75,NULL),('2024-08-12 12:00:00','cośtam',105,232,22,76,NULL),('2024-08-12 14:00:00','cośtam',105,232,22,77,NULL),('2024-08-12 10:00:00','cośtam',105,232,22,78,NULL),('2024-06-03 16:00:00','cośtam',112,232,22,79,NULL),('2024-05-21 14:00:00','cośtam',105,232,22,80,NULL),('2024-06-19 10:00:00','cośtam',105,232,22,81,NULL),('2024-08-05 15:00:00','cośtam',105,232,22,82,NULL),('2024-06-03 13:00:00','cośtam',113,232,22,83,NULL),('2024-08-12 16:00:00','cośtam',113,232,22,84,NULL),('2024-09-16 14:00:00','cośtam',112,232,22,85,NULL),('2024-05-20 10:00:00','cośtam',114,232,22,86,NULL),('2024-05-21 13:00:00','cośtam',114,232,22,87,NULL),('2024-09-09 13:00:00','cośtam',117,232,22,88,NULL),('2024-09-17 14:00:00','cośtam',117,232,22,89,NULL),('2024-05-27 11:00:00','cośtam',105,8,1,90,NULL),('2024-05-27 11:00:00','cośtam',105,232,22,91,NULL),('2024-06-03 10:00:00','cośtam',105,8,1,92,NULL),('2024-08-12 13:00:00','cośtam',112,232,22,93,NULL),('2024-09-09 10:00:00','cośtam',107,232,22,94,NULL);
/*!40000 ALTER TABLE `appointment` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=120 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disease`
--

LOCK TABLES `disease` WRITE;
/*!40000 ALTER TABLE `disease` DISABLE KEYS */;
INSERT INTO `disease` VALUES ('Nazwa',NULL,NULL,NULL,'dsads',96),('Katar','0001-01-01',NULL,NULL,NULL,97),('Zapalenie spojówek','0001-01-01',NULL,NULL,NULL,98),('Zawał','0001-01-01',NULL,NULL,NULL,99),('Zapalenie płuc','0001-01-01',NULL,NULL,NULL,100),('Złamany palec','2024-04-18',NULL,NULL,NULL,101),('Zapalenie oskrzeli','2024-04-18',NULL,NULL,NULL,102),('nowe','2024-04-18',NULL,NULL,NULL,103),('nowe3','2024-03-27',NULL,NULL,NULL,104),('Katar','2024-04-22',NULL,NULL,NULL,105),('nowe','2024-04-22',NULL,NULL,NULL,106),('Katar','2024-04-22',NULL,NULL,NULL,107),('qwe','2024-04-25',NULL,NULL,NULL,108),('fsd','2024-04-25',NULL,NULL,NULL,109),('dsf','2024-04-25',NULL,NULL,NULL,110),('Marskość wątroby','2024-04-28',NULL,NULL,NULL,111),('Schorzenie #1','2024-04-28',NULL,NULL,NULL,112),('Schorzenie #25','2024-04-29',NULL,NULL,NULL,113),('Schorzenie #1','2024-04-29',NULL,NULL,NULL,114),('Schorzenie #3','2024-04-29',NULL,NULL,NULL,115),('Schorzenie #15','2024-04-29',NULL,NULL,NULL,116),('Schorzeni #1','2024-05-02',NULL,NULL,NULL,117),('Zapalenie otrzewnej','2024-05-10',NULL,NULL,NULL,118),('chorboa#3','2024-05-11',NULL,NULL,NULL,119);
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
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `doctor`
--

LOCK TABLES `doctor` WRITE;
/*!40000 ALTER TABLE `doctor` DISABLE KEYS */;
INSERT INTO `doctor` VALUES (1,'fsd','dsfsd',NULL,8),(2,'Adam','Kowalski',NULL,10),(3,'anna','kowalska',NULL,11),(4,'Adam','Nowak',NULL,12),(5,'zx','zx',NULL,13),(6,'asd','sad',NULL,14),(12,'tre','hte',NULL,28),(13,'zxc','zxc',NULL,31),(14,'Jan','Kowalski',NULL,34),(18,'zxc','zxc',NULL,52),(20,'zxcc','jhjh',NULL,60),(21,'qw','qw',NULL,77),(22,'q','q',NULL,232),(23,'Joanna','Nowak',NULL,2546),(24,'Roman','Zieliński',NULL,3040),(25,'Marcelina','Kowalska',NULL,3110);
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
INSERT INTO `doctor_has_patient` VALUES (22,232,117),(22,232,118),(22,232,119),(25,3110,120);
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
INSERT INTO `doctor_specialization` VALUES (1,8,1),(2,10,1),(14,34,1),(18,52,1),(21,77,1),(22,232,1),(25,3110,1),(3,11,2),(4,12,2),(5,13,2),(6,14,2),(12,28,2),(20,60,2),(13,31,3),(23,2546,3),(24,3040,3);
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
) ENGINE=InnoDB AUTO_INCREMENT=278 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicine`
--

LOCK TABLES `medicine` WRITE;
/*!40000 ALTER TABLE `medicine` DISABLE KEYS */;
INSERT INTO `medicine` VALUES ('dfdfd','ds',143,2,'sdfsd',NULL),('er','gfre',144,3,'grbgfr',NULL),('noway','fds',145,3,'gd',NULL),('dsf','gfsd',146,3,'gsgfd',NULL),('fsd','dsf',147,3,'dsf',NULL),('dsf','fds',148,3,'df',NULL),('dsf','dsf',149,3,NULL,NULL),('s','s',150,2,'d',NULL),('d','f',151,3,'d',NULL),('anbs','sadf',152,3,'d',NULL),('fdg','gdf',153,4,'dfgdf',NULL),('gfd','gd',154,4,NULL,NULL),('sdf','dsf',155,2,'sdffsd',NULL),('sdf','fsd',156,2,NULL,NULL),('Naproxem','25mg po posiłku',157,2,NULL,0.5),('Magnez','2 tabletki dziennie',158,5,NULL,1),('Lek#3','2 tabletki',159,3,'Kurację kontynuować 2 miesiące',0.5),('Lek #4','40 ml',160,1,NULL,0),('lek#2','fds',161,3,NULL,0.3),('dsf','fds',162,2,'fsd',0.43),('ds','fds',163,4,'df',0.56),('Lek#1','25mg',164,2,NULL,1),('50ml','1 tabletka po posiłku',165,1,'sdf',0.2),('sad','dsa',166,2,'das',0.33),('sdf','fsd',167,2,'csd',0.23),('sad','fds',168,2,'2',0.02),('23','2323',169,3,'23',0.23),('e','e',170,2,'3',0.03),('d','d',171,5,'5',0.05),('2','2',172,2,'2',0.02),('3','3',173,3,'3',0.03),('d','s',174,2,'2',0.02),('3','4',175,4,'d',0.04),('s','d',176,2,'2',0.02),('s','s',177,4,'4',0.04),('5','5',178,5,'5',0.05),('2','2',179,2,'3',0.02),('f','f',180,3,'d',0.34),('2','2',181,2,'2',0.02),('2','2',182,2,'2',0.02),('2','2',183,2,NULL,0.02),('ert','er',184,4,'5',0.05),('dgr','df',185,5,'f',0.04),('t','t',186,7,'fh',0.6),('2','2',187,2,'2',0.02),('3','33',188,3,'3',0.03),('2','2',189,2,'2',0.53),('3','3',190,3,NULL,0.12),('2','2',191,2,'2',0.02),('12','12',192,12,'12',0.12),('2','2',193,2,'2',0.02),('1','2',194,3,'3',0.03),('mndf','fds',195,2,NULL,1),('dsf','fsdfds',196,3,NULL,0.5),('z','2',197,2,'2',0.02),('lek#1','25mg',198,1,NULL,0.5),('lek #2','40mg',199,5,'dsfsd',1),('Lek#1','25mg',200,2,'sdf',0.5),('Lek#2','50mg',201,3,'sdf',0.5),('34','34',202,34,'34',0.34),('Lek#1','25mg',203,1,NULL,0.5),('Lek#3','23mg',204,3,NULL,0.26),('Lek#1','5ml',205,1,'sdf',0.5),('Lek #4','25ml',206,5,'dg',1),('Lek #47','1 tabletka rano',207,1,'df',1),('Lek#33','2 tabletki',208,2,NULL,1),('Lek #1','25mg',209,2,NULL,0.5),('Lek #2','50mg',210,5,NULL,1),('Lek #12','50mg',211,2,'sad',0.5),('Lek #3','40mg',212,5,NULL,1),('Lek#45','2 tabletki',213,5,'sdfsf',1),('Lek #1','25mg',214,2,NULL,1),('Lek #2','1 tabletka',215,4,NULL,0.5),('Lek #65','23mg',216,4,NULL,0.5),('Lek #33','25mg',217,2,NULL,0.2),('sdf','342',218,43,'df',0.34),('Lek#1','25mg',219,5,NULL,0.2),('2','2',220,2,'2',0.02),('3','3',221,3,'3',0.03),('4','4',222,4,'4',0.04),('5','5',223,5,'5',0.05),('6','66',224,6,'6',0.06),('Lek#1','25mg',225,5,NULL,0.5),('Lek#2','40mg',226,2,'dsf',1),('Lek #33','35mg',227,4,NULL,0.4),('Lek#1','25mg',228,2,'sdfsd',1),('Lek#2','50mg',229,5,'dfggfd',0.5),('Lek#4','50mg',230,5,NULL,0.5),('Lek #45','25ml',231,5,NULL,0.5),('fds','fds',232,23,'sdf',0.5),('df','dfg',233,45,'dfg',0.45),('ewrer','ger',234,3,'3',0.03),('4','4',235,4,NULL,0.04),('wer','ewr',236,25,'25',0.25),('3','3',237,3,'3',0.03),('Lek #5','50mg',238,2,'sdf',1),('5','5',239,5,'5',0.5),('Lek #6','50mg',240,5,NULL,1),('5','5',241,5,'5',0.05),('6','6',243,6,'6',0.06),('6','6',244,6,'6',0.06),('12','12',245,12,'12',0.12),('Nalgesin','12',246,12,'0',0.5),('Amlozek','23',247,10,NULL,1),('Amlozek','50',248,5,NULL,0.5),('Rulid','50mg',249,5,'12',0.5),('Nalgesin Forte','25mg',250,2,NULL,0.5),('Amlozek','1',251,1,NULL,0.01),('Sotahexal 80','25mg',252,5,'24',0.25),('Rulid','50ml',253,5,NULL,0.5),('Carboplatin Pfizer','25mg',254,2,NULL,0.5),('Loratadyna Galena','2ml',255,2,NULL,0.5),('Zoledronic acid Fresenius Kabi','50',256,5,NULL,0.2),('Nalgesin Forte','25mg',257,2,'30',1),('Amlozek','2',258,2,'2',0.02),('Ursopol','4',259,4,'4',0.04),('Coffepirine Tabletki od bólu głowy','25',260,25,'25',0.25),('Xyrem','4',261,4,'4',0.04),('Twinrix Adult','25',262,25,'25',0.25),('Aprovel','4',263,4,NULL,0.04),('Indapen SR','2',264,22,'2',0.02),('Xanax SR','23',265,1,'zd',0.1),('Ascorvita','2',266,2,'2',0.02),('Szczepionka tężcowa adsorbowana (T)','24mg',267,2,'2',0.25),('Aprovel','2',268,2,'2',0.02),('Zoledronic acid Fresenius Kabi','1',269,1,'1',0.01),('Amlozek','11',270,1,'1',0.01),('Rulid','2222',271,2,'2',0.02),('Twinrix Adult','1',272,1,'1',0.01),('Nalgesin Forte','3',273,3,'3',0.33),('Ipertrofan 40','50mg',274,2,NULL,1),('DFV Doxivet','2 tabletki',275,5,'Przed posiłkiem',0.4),('Nalgesin','25mg',276,2,'dgdf',1),('Alka-Seltzer','5mg',277,2,'dsg',0.25);
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
  `appointment_id` int NOT NULL,
  `appointment_Patient_id` int NOT NULL,
  `appointment_doctor_id` int NOT NULL,
  `appointment_doctor_user_id` int NOT NULL,
  `content` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`,`appointment_id`,`appointment_Patient_id`,`appointment_doctor_id`,`appointment_doctor_user_id`),
  KEY `fk_Notification_appointment_idx` (`appointment_id`,`appointment_Patient_id`,`appointment_doctor_id`,`appointment_doctor_user_id`),
  CONSTRAINT `fk_Notification_appointment` FOREIGN KEY (`appointment_id`, `appointment_Patient_id`, `appointment_doctor_id`, `appointment_doctor_user_id`) REFERENCES `appointment` (`id`, `Patient_id`, `doctor_id`, `doctor_user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notification`
--

LOCK TABLES `notification` WRITE;
/*!40000 ALTER TABLE `notification` DISABLE KEYS */;
INSERT INTO `notification` VALUES (1,24,107,22,232,NULL),(2,25,114,22,232,NULL),(3,26,114,22,232,NULL),(4,27,105,22,232,NULL),(5,28,105,22,232,NULL),(6,29,107,22,232,NULL),(7,30,112,22,232,NULL),(8,31,105,22,232,NULL),(9,32,109,22,232,NULL),(10,33,105,22,232,NULL),(11,34,112,22,232,NULL),(12,35,112,22,232,NULL),(13,36,105,22,232,NULL),(14,37,105,22,232,NULL),(15,38,115,22,232,NULL),(16,39,105,22,232,NULL),(17,40,105,22,232,NULL),(18,41,113,22,232,NULL),(19,42,105,22,232,NULL),(20,43,105,22,232,NULL),(21,44,112,22,232,NULL),(22,45,111,22,232,NULL),(23,46,105,22,232,NULL),(24,47,112,22,232,NULL),(25,48,105,22,232,NULL),(26,49,105,22,232,NULL),(27,50,105,22,232,NULL),(28,51,105,22,232,NULL),(29,52,105,22,232,NULL),(30,53,105,22,232,NULL),(31,54,105,22,232,NULL),(32,55,105,22,232,NULL),(33,56,105,22,232,NULL),(34,57,112,22,232,NULL),(35,58,105,22,232,NULL),(36,59,105,22,232,NULL),(37,60,105,22,232,NULL),(38,61,105,22,232,NULL),(39,62,113,22,232,NULL),(40,63,105,22,232,NULL),(41,64,105,22,232,NULL),(42,65,105,22,232,NULL),(43,66,105,22,232,NULL),(44,67,113,22,232,NULL),(45,68,105,22,232,NULL),(46,69,105,22,232,NULL),(47,70,105,22,232,NULL),(48,71,105,22,232,NULL),(49,72,105,22,232,NULL),(50,73,105,22,232,NULL),(51,74,108,22,232,NULL),(52,76,105,22,232,NULL),(53,77,105,22,232,NULL),(54,78,105,22,232,NULL),(55,79,112,22,232,NULL),(56,80,105,22,232,NULL),(57,81,105,22,232,NULL),(58,82,105,22,232,NULL),(59,83,113,22,232,NULL),(60,84,113,22,232,NULL),(61,85,112,22,232,NULL),(62,86,114,22,232,NULL),(63,87,114,22,232,NULL),(64,88,117,22,232,NULL),(65,89,117,22,232,NULL);
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
) ENGINE=InnoDB AUTO_INCREMENT=121 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patient`
--

LOCK TABLES `patient` WRITE;
/*!40000 ALTER TABLE `patient` DISABLE KEYS */;
INSERT INTO `patient` VALUES (NULL,NULL,NULL,105,'Adam','Nowak',NULL,NULL,'M','2024-04-22',NULL),(NULL,NULL,'a@a.pl',106,'Roman','Zieliński',NULL,NULL,'M','2024-04-22',NULL),('56387678903','534908765',NULL,107,'Jan','Kowalski',NULL,NULL,'M','1991-12-10',NULL),('87673567801','667887546','a@a.pl',108,'Adam','Nowak',NULL,NULL,'M','1987-01-11',NULL),('12345675694','223456864',NULL,109,'Roman','Zieliński',NULL,NULL,'M','2024-01-02',NULL),('23434567893',NULL,'a@a.pl',110,'Adam','Kowalski',NULL,NULL,'M','2010-01-11',NULL),('32676756724',NULL,NULL,111,'Adam','Zieliński',NULL,NULL,'M','2024-04-23',NULL),('76765645687',NULL,'g@j.pl',112,'Jan','Nowak',NULL,NULL,'M','2024-04-23',NULL),('01312906516','223786545','a@a.pl',113,'Adam','Nowacki',NULL,NULL,'M','2024-04-24',NULL),('11767865456',NULL,NULL,114,'Joanna','Kowalska',NULL,NULL,'K','1980-01-01','Maria'),('36745676381','332346785','a.kowalczyk@gmail.com',115,'Adam','Kowalczyk',NULL,NULL,'M','2024-04-28',NULL),('01312906516',NULL,NULL,116,'grzegorz','Rumak',NULL,NULL,'M','2024-04-29',NULL),('01312906516',NULL,NULL,117,'Roman','Zieliński',NULL,NULL,'M','2024-04-29',NULL),('70051251144',NULL,NULL,118,'Grzegorz','Nowak',NULL,NULL,'M','2024-04-29',NULL),('51012056651',NULL,NULL,119,'Adam','Kowalczyk',NULL,NULL,'M','2024-04-29',NULL),('98073056354','776465345','rziel@gmail.com',120,'Roman','Zieliński',NULL,NULL,'M','1980-05-10',NULL);
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
INSERT INTO `patient_address` VALUES (117,17),(118,18),(119,19),(120,20);
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
INSERT INTO `patient_diesease` VALUES (99,108),(114,117),(119,117),(115,118),(116,119),(117,119),(118,120);
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
) ENGINE=InnoDB AUTO_INCREMENT=122 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prescription`
--

LOCK TABLES `prescription` WRITE;
/*!40000 ALTER TABLE `prescription` DISABLE KEYS */;
INSERT INTO `prescription` VALUES (108,'2024-05-02','2024-05-02','09cb1041e744418684a01e',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\108252024.pdf',22,232),(109,'2024-05-02','2024-05-02','3f39237eb81b4893bf17b5',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\109252024.pdf',22,232),(110,'2024-05-02','2024-05-02','6445c2c5e512475995b04b',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\110252024.pdf',22,232),(111,'2024-05-02','2024-05-02','2cf2bffdde4a423aafa8a0',118,NULL,22,232),(112,'2024-05-02','2024-05-02','9ee5be17eb2047a6ab3628',118,'112252024.pdf',22,232),(113,'2024-05-02','2024-05-02','9188938fddb04958ae4cf5',117,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\113252024.pdf',22,232),(114,'2024-05-02','2024-05-02','afb2382163464c4991b113',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\114252024.pdf',22,232),(115,'2024-05-02','2024-05-02','5cd32dc4489c41719a14a1',119,'115252024.pdf',22,232),(116,'2024-05-02','2024-05-02','1089399d13fd4bf5845b3b',118,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Debug\\net8.0-windows\\116252024.pdf',22,232),(117,'2024-05-03','2024-05-03','a07364fa2ba149dcad1e72',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\117352024.pdf',22,232),(118,'2024-05-03','2024-05-03','5e4a74b732a34c9e839523',119,'C:\\Users\\Jacek\\source\\repos\\bazy2\\bin\\Release\\net8.0-windows\\118352024.pdf',22,232),(119,'2024-05-10','2024-05-10','dec212b3f15e443fa53538',120,'I:\\bazy2 — kopia (2)\\bin\\Release\\net8.0-windows\\1191052024.pdf',25,3110),(120,'2024-05-11','2024-05-11','c7232ae47bf241399e67c5',118,NULL,22,232),(121,'2024-05-11','2024-05-11','c2e6e29cd20e4765bec712',118,'D:\\bazy2test123\\bazy\\bin\\Debug\\net8.0-windows\\1211152024.pdf',22,232);
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
INSERT INTO `prescription_medicine` VALUES (108,118,262),(108,118,263),(109,118,264),(110,119,265),(111,118,266),(112,118,267),(113,117,268),(114,118,269),(115,119,270),(116,118,271),(117,119,273),(118,119,273),(119,120,274),(119,120,275),(120,118,276),(121,118,277);
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `receptionist`
--

LOCK TABLES `receptionist` WRITE;
/*!40000 ALTER TABLE `receptionist` DISABLE KEYS */;
INSERT INTO `receptionist` VALUES (1,'Andrzej','Kowalski',2993),(2,'Roman','Zieliński',3061),(3,'nowy','test',3070);
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
  `email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `login` (`login`)
) ENGINE=InnoDB AUTO_INCREMENT=3412 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (5,'lekarz','fdfd','123','dsfdq','dsg','doctor',0,NULL,NULL),(6,'lekarz','adasd','0000\0\0','abc','cds',NULL,1,NULL,NULL),(7,'recepcjonista','fe','176112147214O','cx','gr',NULL,1,NULL,NULL),(8,'lekarz','fa','7847U91','ab','sa',NULL,1,NULL,NULL),(9,'lekarz','b','a','b','b',NULL,0,NULL,NULL),(10,'lekarz','AK02615','123','Adam','Kowalski','1',0,NULL,NULL),(11,'lekarz','ak04103','anna123','anna','kowalska','0',0,NULL,NULL),(12,'lekarz','an00227','6622LVe','Adam','Nowak','$2a$11$SKxMUhxTaZaP23QPkkSS8.wriA1TYM5nDjK8jWy6FdJ0ibsmuE13C',1,NULL,NULL),(13,'lekarz','zz05353','lmao','zx','zx','$2a$11$2ureyDLn/FEzCJ74NhAG0.BNXagUAeFxG7DcoZF3O.ohfAr08uqS2',0,NULL,NULL),(14,'lekarz','as04865','1382!.4','asd','sad','$2a$11$MmditWc.JWH.N6TYJVAr/.Q6K4l2UtYFSz/pLjFdoDH9ZSX6GZen.',0,NULL,NULL),(17,'admin','admin','admin','admin','admin','$2a$11$hk7hpRiflna5nQQx.ZqDhONisX2c4.nl5594UFVJNpnkBg2dhgJgG',1,'2024-05-11 12:08:13',NULL),(28,'lekarz','fds','8634Vp}','fsdfsdfs','fsd','$2a$11$ZJky9t05HUAW5HpteIa/se85i6WwbEHwgtQnqGJcX0gtSeHY6DPQ2',1,NULL,NULL),(31,'lekarz','zz38482','382325Y','zxc','zxc','$2a$11$/ODjx1pj1p3fRl3QWo58zuKua/8BuHP14Si/ycjyRnzHY/O59zOpe',1,NULL,NULL),(34,'lekarz','am12345','12345','Adam','Nowak','$2a$11$V0sXND/GlTSmgvlBJw06H.lENoP13DlrdZhAtYC6ebEj5v.nEAi2e',0,'2024-04-08 19:34:53',NULL),(52,'lekarz','zz64830','123','zxc','zxc','$2a$11$pG9CinSzogaaiwz/qwtH3uckIzpE5vF9ibEchf/ieaV39EjTFFOnG',0,'2024-04-09 09:27:18',NULL),(60,'lekarz','gd','123','fdfgd','fdgd','$2a$11$A3wNExDiSCPts36DU4GlvOIb4cNULjQcq.JomKLo7TGdSLO49/9CK',0,'2024-04-11 15:46:47',NULL),(77,'lekarz','qq27265','123','qw','qw','$2a$11$xK2Ujkg1WOyBOxoC3KUZF.80fbB9jTC1fevd9ia8VFpZANyLp2biK',0,'2024-04-11 15:27:16',NULL),(232,'lekarz','q','q','q','q','$2a$11$tcPnkTwEyXL2LM0vuCLfEu1OPfUhTTQl2QPwL8cvzJCGqWpdYnO5i',0,'2024-05-11 14:01:46',NULL),(2546,'lekarz','jn56445','8851-|A','Joanna','Nowak','$2a$11$nI25YZyOkBUb5XkWiH4nru7D6k8X6zaOTn/1jQYsChtPbbZRNl2d6',1,NULL,'xojese6864@amankro.com'),(2993,'recepcjonista','ak00081','12345','Andrzej','Kowalski','$2a$11$imrCe3l63.imoUMEHBrvU.KuXXmRADkqEERxHse4p1zD1KETjY5xG',0,'2024-05-09 15:03:58',NULL),(3040,'lekarz','rz67261','12345','Roman','Zieliński','$2a$11$km3gSs8m/OPb89arwQrj4OMcieVQcGPN5QIOhQyoC3tYjm2dAW3ZW',0,'2024-05-09 15:03:32',NULL),(3061,'recepcjonista','rz20100','12345','Roman','Zieliński','$2a$11$nZogadbuaad/DsadYM2HyuF53H/Ph9cUzs8Bs4PF15j.rmaWt/MlW',0,'2024-05-11 14:58:27',NULL),(3070,'recepcjonista','nt57446','12345','nowy','test','$2a$11$TwaxXnqdf36njv7sdSObZ.m0EHm5PV.kgnhpDkZF7/UiJ2jcBYnza',0,'2024-05-10 22:20:20',NULL),(3110,'lekarz','mk16366','12345','Marcelina','Kowalska','$2a$11$2WqY1qKl2wO/umC.GA9LAeg6KAtlrKjulBUqesHY540GPVL3zYbzC',0,'2024-05-10 22:19:30',NULL);
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
  `Doctor_id` int NOT NULL,
  `Doctor_User_id` int NOT NULL,
  `day` date DEFAULT NULL,
  `open` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`,`Doctor_id`,`Doctor_User_id`),
  KEY `fk_WorkHours_Doctor1_idx` (`Doctor_id`,`Doctor_User_id`),
  CONSTRAINT `fk_WorkHours_Doctor1` FOREIGN KEY (`Doctor_id`, `Doctor_User_id`) REFERENCES `doctor` (`id`, `User_id`)
) ENGINE=InnoDB AUTO_INCREMENT=180049 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workhours`
--

LOCK TABLES `workhours` WRITE;
/*!40000 ALTER TABLE `workhours` DISABLE KEYS */;
INSERT INTO `workhours` VALUES (178853,'2024-05-13 09:00:00.00000','2024-05-13 09:30:00.00000',22,232,NULL,0),(178854,'2024-05-13 10:00:00.00000','2024-05-13 10:30:00.00000',22,232,NULL,0),(178855,'2024-05-13 11:00:00.00000','2024-05-13 11:30:00.00000',22,232,NULL,0),(178856,'2024-05-13 12:00:00.00000','2024-05-13 12:30:00.00000',22,232,NULL,0),(178857,'2024-05-13 13:00:00.00000','2024-05-13 13:30:00.00000',22,232,NULL,0),(178858,'2024-05-13 14:00:00.00000','2024-05-13 14:30:00.00000',22,232,NULL,0),(178859,'2024-05-13 15:00:00.00000','2024-05-13 15:30:00.00000',22,232,NULL,0),(178860,'2024-05-13 16:00:00.00000','2024-05-13 16:30:00.00000',22,232,NULL,0),(178861,'2024-05-14 11:00:00.00000','2024-05-14 11:30:00.00000',22,232,NULL,1),(178862,'2024-05-14 12:00:00.00000','2024-05-14 12:30:00.00000',22,232,NULL,0),(178863,'2024-05-14 13:00:00.00000','2024-05-14 13:30:00.00000',22,232,NULL,1),(178864,'2024-05-14 14:00:00.00000','2024-05-14 14:30:00.00000',22,232,NULL,1),(178865,'2024-05-14 20:00:00.00000','2024-05-14 20:30:00.00000',22,232,NULL,0),(178866,'2024-05-14 21:00:00.00000','2024-05-14 21:30:00.00000',22,232,NULL,0),(178867,'2024-05-14 22:00:00.00000','2024-05-14 22:30:00.00000',22,232,NULL,1),(178868,'2024-05-15 08:00:00.00000','2024-05-15 08:30:00.00000',22,232,NULL,0),(178869,'2024-05-15 09:00:00.00000','2024-05-15 09:30:00.00000',22,232,NULL,1),(178870,'2024-05-15 10:00:00.00000','2024-05-15 10:30:00.00000',22,232,NULL,0),(178871,'2024-05-15 11:00:00.00000','2024-05-15 11:30:00.00000',22,232,NULL,1),(178872,'2024-05-15 12:00:00.00000','2024-05-15 12:30:00.00000',22,232,NULL,1),(178873,'2024-05-15 13:00:00.00000','2024-05-15 13:30:00.00000',22,232,NULL,0),(178874,'2024-05-15 14:00:00.00000','2024-05-15 14:30:00.00000',22,232,NULL,1),(178875,'2024-05-15 15:00:00.00000','2024-05-15 15:30:00.00000',22,232,NULL,0),(178876,'2024-05-20 09:00:00.00000','2024-05-20 09:30:00.00000',22,232,NULL,0),(178877,'2024-05-20 10:00:00.00000','2024-05-20 10:30:00.00000',22,232,NULL,0),(178878,'2024-05-20 11:00:00.00000','2024-05-20 11:30:00.00000',22,232,NULL,0),(178879,'2024-05-20 12:00:00.00000','2024-05-20 12:30:00.00000',22,232,NULL,0),(178880,'2024-05-20 13:00:00.00000','2024-05-20 13:30:00.00000',22,232,NULL,0),(178881,'2024-05-20 14:00:00.00000','2024-05-20 14:30:00.00000',22,232,NULL,0),(178882,'2024-05-20 15:00:00.00000','2024-05-20 15:30:00.00000',22,232,NULL,0),(178883,'2024-05-20 16:00:00.00000','2024-05-20 16:30:00.00000',22,232,NULL,0),(178884,'2024-05-21 11:00:00.00000','2024-05-21 11:30:00.00000',22,232,NULL,0),(178885,'2024-05-21 12:00:00.00000','2024-05-21 12:30:00.00000',22,232,NULL,0),(178886,'2024-05-21 13:00:00.00000','2024-05-21 13:30:00.00000',22,232,NULL,0),(178887,'2024-05-21 14:00:00.00000','2024-05-21 14:30:00.00000',22,232,NULL,0),(178888,'2024-05-21 20:00:00.00000','2024-05-21 20:30:00.00000',22,232,NULL,0),(178889,'2024-05-21 21:00:00.00000','2024-05-21 21:30:00.00000',22,232,NULL,1),(178890,'2024-05-21 22:00:00.00000','2024-05-21 22:30:00.00000',22,232,NULL,0),(178891,'2024-05-22 08:00:00.00000','2024-05-22 08:30:00.00000',22,232,NULL,1),(178892,'2024-05-22 09:00:00.00000','2024-05-22 09:30:00.00000',22,232,NULL,1),(178893,'2024-05-22 10:00:00.00000','2024-05-22 10:30:00.00000',22,232,NULL,1),(178894,'2024-05-22 11:00:00.00000','2024-05-22 11:30:00.00000',22,232,NULL,0),(178895,'2024-05-22 12:00:00.00000','2024-05-22 12:30:00.00000',22,232,NULL,1),(178896,'2024-05-22 13:00:00.00000','2024-05-22 13:30:00.00000',22,232,NULL,1),(178897,'2024-05-22 14:00:00.00000','2024-05-22 14:30:00.00000',22,232,NULL,0),(178898,'2024-05-22 15:00:00.00000','2024-05-22 15:30:00.00000',22,232,NULL,1),(178899,'2024-05-27 09:00:00.00000','2024-05-27 09:30:00.00000',22,232,NULL,0),(178900,'2024-05-27 10:00:00.00000','2024-05-27 10:30:00.00000',22,232,NULL,0),(178901,'2024-05-27 11:00:00.00000','2024-05-27 11:30:00.00000',22,232,NULL,0),(178902,'2024-05-27 12:00:00.00000','2024-05-27 12:30:00.00000',22,232,NULL,0),(178903,'2024-05-27 13:00:00.00000','2024-05-27 13:30:00.00000',22,232,NULL,0),(178904,'2024-05-27 14:00:00.00000','2024-05-27 14:30:00.00000',22,232,NULL,0),(178905,'2024-05-27 15:00:00.00000','2024-05-27 15:30:00.00000',22,232,NULL,0),(178906,'2024-05-27 16:00:00.00000','2024-05-27 16:30:00.00000',22,232,NULL,0),(178907,'2024-05-28 11:00:00.00000','2024-05-28 11:30:00.00000',22,232,NULL,1),(178908,'2024-05-28 12:00:00.00000','2024-05-28 12:30:00.00000',22,232,NULL,1),(178909,'2024-05-28 13:00:00.00000','2024-05-28 13:30:00.00000',22,232,NULL,0),(178910,'2024-05-28 14:00:00.00000','2024-05-28 14:30:00.00000',22,232,NULL,1),(178911,'2024-05-28 20:00:00.00000','2024-05-28 20:30:00.00000',22,232,NULL,1),(178912,'2024-05-28 21:00:00.00000','2024-05-28 21:30:00.00000',22,232,NULL,1),(178913,'2024-05-28 22:00:00.00000','2024-05-28 22:30:00.00000',22,232,NULL,1),(178914,'2024-05-29 08:00:00.00000','2024-05-29 08:30:00.00000',22,232,NULL,1),(178915,'2024-05-29 09:00:00.00000','2024-05-29 09:30:00.00000',22,232,NULL,1),(178916,'2024-05-29 10:00:00.00000','2024-05-29 10:30:00.00000',22,232,NULL,1),(178917,'2024-05-29 11:00:00.00000','2024-05-29 11:30:00.00000',22,232,NULL,0),(178918,'2024-05-29 12:00:00.00000','2024-05-29 12:30:00.00000',22,232,NULL,1),(178919,'2024-05-29 13:00:00.00000','2024-05-29 13:30:00.00000',22,232,NULL,1),(178920,'2024-05-29 14:00:00.00000','2024-05-29 14:30:00.00000',22,232,NULL,1),(178921,'2024-05-29 15:00:00.00000','2024-05-29 15:30:00.00000',22,232,NULL,1),(178922,'2024-06-03 09:00:00.00000','2024-06-03 09:30:00.00000',22,232,NULL,0),(178923,'2024-06-03 10:00:00.00000','2024-06-03 10:30:00.00000',22,232,NULL,1),(178924,'2024-06-03 11:00:00.00000','2024-06-03 11:30:00.00000',22,232,NULL,0),(178925,'2024-06-03 12:00:00.00000','2024-06-03 12:30:00.00000',22,232,NULL,0),(178926,'2024-06-03 13:00:00.00000','2024-06-03 13:30:00.00000',22,232,NULL,0),(178927,'2024-06-03 14:00:00.00000','2024-06-03 14:30:00.00000',22,232,NULL,0),(178928,'2024-06-03 15:00:00.00000','2024-06-03 15:30:00.00000',22,232,NULL,0),(178929,'2024-06-03 16:00:00.00000','2024-06-03 16:30:00.00000',22,232,NULL,0),(178930,'2024-06-04 11:00:00.00000','2024-06-04 11:30:00.00000',22,232,NULL,1),(178931,'2024-06-04 12:00:00.00000','2024-06-04 12:30:00.00000',22,232,NULL,1),(178932,'2024-06-04 13:00:00.00000','2024-06-04 13:30:00.00000',22,232,NULL,1),(178933,'2024-06-04 14:00:00.00000','2024-06-04 14:30:00.00000',22,232,NULL,0),(178934,'2024-06-04 20:00:00.00000','2024-06-04 20:30:00.00000',22,232,NULL,1),(178935,'2024-06-04 21:00:00.00000','2024-06-04 21:30:00.00000',22,232,NULL,1),(178936,'2024-06-04 22:00:00.00000','2024-06-04 22:30:00.00000',22,232,NULL,1),(178937,'2024-06-05 08:00:00.00000','2024-06-05 08:30:00.00000',22,232,NULL,1),(178938,'2024-06-05 09:00:00.00000','2024-06-05 09:30:00.00000',22,232,NULL,1),(178939,'2024-06-05 10:00:00.00000','2024-06-05 10:30:00.00000',22,232,NULL,1),(178940,'2024-06-05 11:00:00.00000','2024-06-05 11:30:00.00000',22,232,NULL,0),(178941,'2024-06-05 12:00:00.00000','2024-06-05 12:30:00.00000',22,232,NULL,0),(178942,'2024-06-05 13:00:00.00000','2024-06-05 13:30:00.00000',22,232,NULL,1),(178943,'2024-06-05 14:00:00.00000','2024-06-05 14:30:00.00000',22,232,NULL,1),(178944,'2024-06-05 15:00:00.00000','2024-06-05 15:30:00.00000',22,232,NULL,1),(178945,'2024-06-10 09:00:00.00000','2024-06-10 09:30:00.00000',22,232,NULL,1),(178946,'2024-06-10 10:00:00.00000','2024-06-10 10:30:00.00000',22,232,NULL,1),(178947,'2024-06-10 11:00:00.00000','2024-06-10 11:30:00.00000',22,232,NULL,1),(178948,'2024-06-10 12:00:00.00000','2024-06-10 12:30:00.00000',22,232,NULL,1),(178949,'2024-06-10 13:00:00.00000','2024-06-10 13:30:00.00000',22,232,NULL,1),(178950,'2024-06-10 14:00:00.00000','2024-06-10 14:30:00.00000',22,232,NULL,0),(178951,'2024-06-10 15:00:00.00000','2024-06-10 15:30:00.00000',22,232,NULL,1),(178952,'2024-06-10 16:00:00.00000','2024-06-10 16:30:00.00000',22,232,NULL,1),(178953,'2024-06-11 11:00:00.00000','2024-06-11 11:30:00.00000',22,232,NULL,1),(178954,'2024-06-11 12:00:00.00000','2024-06-11 12:30:00.00000',22,232,NULL,1),(178955,'2024-06-11 13:00:00.00000','2024-06-11 13:30:00.00000',22,232,NULL,1),(178956,'2024-06-11 14:00:00.00000','2024-06-11 14:30:00.00000',22,232,NULL,1),(178957,'2024-06-11 20:00:00.00000','2024-06-11 20:30:00.00000',22,232,NULL,1),(178958,'2024-06-11 21:00:00.00000','2024-06-11 21:30:00.00000',22,232,NULL,1),(178959,'2024-06-11 22:00:00.00000','2024-06-11 22:30:00.00000',22,232,NULL,1),(178960,'2024-06-12 08:00:00.00000','2024-06-12 08:30:00.00000',22,232,NULL,1),(178961,'2024-06-12 09:00:00.00000','2024-06-12 09:30:00.00000',22,232,NULL,1),(178962,'2024-06-12 10:00:00.00000','2024-06-12 10:30:00.00000',22,232,NULL,1),(178963,'2024-06-12 11:00:00.00000','2024-06-12 11:30:00.00000',22,232,NULL,1),(178964,'2024-06-12 12:00:00.00000','2024-06-12 12:30:00.00000',22,232,NULL,1),(178965,'2024-06-12 13:00:00.00000','2024-06-12 13:30:00.00000',22,232,NULL,1),(178966,'2024-06-12 14:00:00.00000','2024-06-12 14:30:00.00000',22,232,NULL,1),(178967,'2024-06-12 15:00:00.00000','2024-06-12 15:30:00.00000',22,232,NULL,1),(178968,'2024-06-17 09:00:00.00000','2024-06-17 09:30:00.00000',22,232,NULL,1),(178969,'2024-06-17 10:00:00.00000','2024-06-17 10:30:00.00000',22,232,NULL,1),(178970,'2024-06-17 11:00:00.00000','2024-06-17 11:30:00.00000',22,232,NULL,1),(178971,'2024-06-17 12:00:00.00000','2024-06-17 12:30:00.00000',22,232,NULL,0),(178972,'2024-06-17 13:00:00.00000','2024-06-17 13:30:00.00000',22,232,NULL,1),(178973,'2024-06-17 14:00:00.00000','2024-06-17 14:30:00.00000',22,232,NULL,1),(178974,'2024-06-17 15:00:00.00000','2024-06-17 15:30:00.00000',22,232,NULL,1),(178975,'2024-06-17 16:00:00.00000','2024-06-17 16:30:00.00000',22,232,NULL,1),(178976,'2024-06-18 11:00:00.00000','2024-06-18 11:30:00.00000',22,232,NULL,1),(178977,'2024-06-18 12:00:00.00000','2024-06-18 12:30:00.00000',22,232,NULL,1),(178978,'2024-06-18 13:00:00.00000','2024-06-18 13:30:00.00000',22,232,NULL,1),(178979,'2024-06-18 14:00:00.00000','2024-06-18 14:30:00.00000',22,232,NULL,1),(178980,'2024-06-18 20:00:00.00000','2024-06-18 20:30:00.00000',22,232,NULL,1),(178981,'2024-06-18 21:00:00.00000','2024-06-18 21:30:00.00000',22,232,NULL,1),(178982,'2024-06-18 22:00:00.00000','2024-06-18 22:30:00.00000',22,232,NULL,1),(178983,'2024-06-19 08:00:00.00000','2024-06-19 08:30:00.00000',22,232,NULL,1),(178984,'2024-06-19 09:00:00.00000','2024-06-19 09:30:00.00000',22,232,NULL,1),(178985,'2024-06-19 10:00:00.00000','2024-06-19 10:30:00.00000',22,232,NULL,0),(178986,'2024-06-19 11:00:00.00000','2024-06-19 11:30:00.00000',22,232,NULL,1),(178987,'2024-06-19 12:00:00.00000','2024-06-19 12:30:00.00000',22,232,NULL,1),(178988,'2024-06-19 13:00:00.00000','2024-06-19 13:30:00.00000',22,232,NULL,1),(178989,'2024-06-19 14:00:00.00000','2024-06-19 14:30:00.00000',22,232,NULL,1),(178990,'2024-06-19 15:00:00.00000','2024-06-19 15:30:00.00000',22,232,NULL,1),(178991,'2024-06-24 09:00:00.00000','2024-06-24 09:30:00.00000',22,232,NULL,1),(178992,'2024-06-24 10:00:00.00000','2024-06-24 10:30:00.00000',22,232,NULL,1),(178993,'2024-06-24 11:00:00.00000','2024-06-24 11:30:00.00000',22,232,NULL,1),(178994,'2024-06-24 12:00:00.00000','2024-06-24 12:30:00.00000',22,232,NULL,1),(178995,'2024-06-24 13:00:00.00000','2024-06-24 13:30:00.00000',22,232,NULL,1),(178996,'2024-06-24 14:00:00.00000','2024-06-24 14:30:00.00000',22,232,NULL,1),(178997,'2024-06-24 15:00:00.00000','2024-06-24 15:30:00.00000',22,232,NULL,1),(178998,'2024-06-24 16:00:00.00000','2024-06-24 16:30:00.00000',22,232,NULL,1),(178999,'2024-06-25 11:00:00.00000','2024-06-25 11:30:00.00000',22,232,NULL,1),(179000,'2024-06-25 12:00:00.00000','2024-06-25 12:30:00.00000',22,232,NULL,1),(179001,'2024-06-25 13:00:00.00000','2024-06-25 13:30:00.00000',22,232,NULL,1),(179002,'2024-06-25 14:00:00.00000','2024-06-25 14:30:00.00000',22,232,NULL,1),(179003,'2024-06-25 20:00:00.00000','2024-06-25 20:30:00.00000',22,232,NULL,1),(179004,'2024-06-25 21:00:00.00000','2024-06-25 21:30:00.00000',22,232,NULL,1),(179005,'2024-06-25 22:00:00.00000','2024-06-25 22:30:00.00000',22,232,NULL,1),(179006,'2024-06-26 08:00:00.00000','2024-06-26 08:30:00.00000',22,232,NULL,1),(179007,'2024-06-26 09:00:00.00000','2024-06-26 09:30:00.00000',22,232,NULL,1),(179008,'2024-06-26 10:00:00.00000','2024-06-26 10:30:00.00000',22,232,NULL,1),(179009,'2024-06-26 11:00:00.00000','2024-06-26 11:30:00.00000',22,232,NULL,1),(179010,'2024-06-26 12:00:00.00000','2024-06-26 12:30:00.00000',22,232,NULL,1),(179011,'2024-06-26 13:00:00.00000','2024-06-26 13:30:00.00000',22,232,NULL,1),(179012,'2024-06-26 14:00:00.00000','2024-06-26 14:30:00.00000',22,232,NULL,1),(179013,'2024-06-26 15:00:00.00000','2024-06-26 15:30:00.00000',22,232,NULL,1),(179014,'2024-07-01 09:00:00.00000','2024-07-01 09:30:00.00000',22,232,NULL,1),(179015,'2024-07-01 10:00:00.00000','2024-07-01 10:30:00.00000',22,232,NULL,1),(179016,'2024-07-01 11:00:00.00000','2024-07-01 11:30:00.00000',22,232,NULL,0),(179017,'2024-07-01 12:00:00.00000','2024-07-01 12:30:00.00000',22,232,NULL,1),(179018,'2024-07-01 13:00:00.00000','2024-07-01 13:30:00.00000',22,232,NULL,0),(179019,'2024-07-01 14:00:00.00000','2024-07-01 14:30:00.00000',22,232,NULL,1),(179020,'2024-07-01 15:00:00.00000','2024-07-01 15:30:00.00000',22,232,NULL,0),(179021,'2024-07-01 16:00:00.00000','2024-07-01 16:30:00.00000',22,232,NULL,1),(179022,'2024-07-02 11:00:00.00000','2024-07-02 11:30:00.00000',22,232,NULL,1),(179023,'2024-07-02 12:00:00.00000','2024-07-02 12:30:00.00000',22,232,NULL,1),(179024,'2024-07-02 13:00:00.00000','2024-07-02 13:30:00.00000',22,232,NULL,1),(179025,'2024-07-02 14:00:00.00000','2024-07-02 14:30:00.00000',22,232,NULL,1),(179026,'2024-07-02 20:00:00.00000','2024-07-02 20:30:00.00000',22,232,NULL,1),(179027,'2024-07-02 21:00:00.00000','2024-07-02 21:30:00.00000',22,232,NULL,1),(179028,'2024-07-02 22:00:00.00000','2024-07-02 22:30:00.00000',22,232,NULL,1),(179029,'2024-07-03 08:00:00.00000','2024-07-03 08:30:00.00000',22,232,NULL,1),(179030,'2024-07-03 09:00:00.00000','2024-07-03 09:30:00.00000',22,232,NULL,1),(179031,'2024-07-03 10:00:00.00000','2024-07-03 10:30:00.00000',22,232,NULL,1),(179032,'2024-07-03 11:00:00.00000','2024-07-03 11:30:00.00000',22,232,NULL,1),(179033,'2024-07-03 12:00:00.00000','2024-07-03 12:30:00.00000',22,232,NULL,1),(179034,'2024-07-03 13:00:00.00000','2024-07-03 13:30:00.00000',22,232,NULL,1),(179035,'2024-07-03 14:00:00.00000','2024-07-03 14:30:00.00000',22,232,NULL,0),(179036,'2024-07-03 15:00:00.00000','2024-07-03 15:30:00.00000',22,232,NULL,1),(179037,'2024-07-08 09:00:00.00000','2024-07-08 09:30:00.00000',22,232,NULL,0),(179038,'2024-07-08 10:00:00.00000','2024-07-08 10:30:00.00000',22,232,NULL,1),(179039,'2024-07-08 11:00:00.00000','2024-07-08 11:30:00.00000',22,232,NULL,1),(179040,'2024-07-08 12:00:00.00000','2024-07-08 12:30:00.00000',22,232,NULL,1),(179041,'2024-07-08 13:00:00.00000','2024-07-08 13:30:00.00000',22,232,NULL,1),(179042,'2024-07-08 14:00:00.00000','2024-07-08 14:30:00.00000',22,232,NULL,1),(179043,'2024-07-08 15:00:00.00000','2024-07-08 15:30:00.00000',22,232,NULL,1),(179044,'2024-07-08 16:00:00.00000','2024-07-08 16:30:00.00000',22,232,NULL,1),(179045,'2024-07-09 11:00:00.00000','2024-07-09 11:30:00.00000',22,232,NULL,1),(179046,'2024-07-09 12:00:00.00000','2024-07-09 12:30:00.00000',22,232,NULL,1),(179047,'2024-07-09 13:00:00.00000','2024-07-09 13:30:00.00000',22,232,NULL,1),(179048,'2024-07-09 14:00:00.00000','2024-07-09 14:30:00.00000',22,232,NULL,0),(179049,'2024-07-09 20:00:00.00000','2024-07-09 20:30:00.00000',22,232,NULL,1),(179050,'2024-07-09 21:00:00.00000','2024-07-09 21:30:00.00000',22,232,NULL,1),(179051,'2024-07-09 22:00:00.00000','2024-07-09 22:30:00.00000',22,232,NULL,1),(179052,'2024-07-10 08:00:00.00000','2024-07-10 08:30:00.00000',22,232,NULL,1),(179053,'2024-07-10 09:00:00.00000','2024-07-10 09:30:00.00000',22,232,NULL,1),(179054,'2024-07-10 10:00:00.00000','2024-07-10 10:30:00.00000',22,232,NULL,1),(179055,'2024-07-10 11:00:00.00000','2024-07-10 11:30:00.00000',22,232,NULL,1),(179056,'2024-07-10 12:00:00.00000','2024-07-10 12:30:00.00000',22,232,NULL,1),(179057,'2024-07-10 13:00:00.00000','2024-07-10 13:30:00.00000',22,232,NULL,1),(179058,'2024-07-10 14:00:00.00000','2024-07-10 14:30:00.00000',22,232,NULL,1),(179059,'2024-07-10 15:00:00.00000','2024-07-10 15:30:00.00000',22,232,NULL,1),(179060,'2024-07-15 09:00:00.00000','2024-07-15 09:30:00.00000',22,232,NULL,1),(179061,'2024-07-15 10:00:00.00000','2024-07-15 10:30:00.00000',22,232,NULL,1),(179062,'2024-07-15 11:00:00.00000','2024-07-15 11:30:00.00000',22,232,NULL,1),(179063,'2024-07-15 12:00:00.00000','2024-07-15 12:30:00.00000',22,232,NULL,0),(179064,'2024-07-15 13:00:00.00000','2024-07-15 13:30:00.00000',22,232,NULL,1),(179065,'2024-07-15 14:00:00.00000','2024-07-15 14:30:00.00000',22,232,NULL,0),(179066,'2024-07-15 15:00:00.00000','2024-07-15 15:30:00.00000',22,232,NULL,1),(179067,'2024-07-15 16:00:00.00000','2024-07-15 16:30:00.00000',22,232,NULL,1),(179068,'2024-07-16 11:00:00.00000','2024-07-16 11:30:00.00000',22,232,NULL,1),(179069,'2024-07-16 12:00:00.00000','2024-07-16 12:30:00.00000',22,232,NULL,1),(179070,'2024-07-16 13:00:00.00000','2024-07-16 13:30:00.00000',22,232,NULL,1),(179071,'2024-07-16 14:00:00.00000','2024-07-16 14:30:00.00000',22,232,NULL,1),(179072,'2024-07-16 20:00:00.00000','2024-07-16 20:30:00.00000',22,232,NULL,1),(179073,'2024-07-16 21:00:00.00000','2024-07-16 21:30:00.00000',22,232,NULL,1),(179074,'2024-07-16 22:00:00.00000','2024-07-16 22:30:00.00000',22,232,NULL,1),(179075,'2024-07-17 08:00:00.00000','2024-07-17 08:30:00.00000',22,232,NULL,1),(179076,'2024-07-17 09:00:00.00000','2024-07-17 09:30:00.00000',22,232,NULL,1),(179077,'2024-07-17 10:00:00.00000','2024-07-17 10:30:00.00000',22,232,NULL,1),(179078,'2024-07-17 11:00:00.00000','2024-07-17 11:30:00.00000',22,232,NULL,1),(179079,'2024-07-17 12:00:00.00000','2024-07-17 12:30:00.00000',22,232,NULL,1),(179080,'2024-07-17 13:00:00.00000','2024-07-17 13:30:00.00000',22,232,NULL,1),(179081,'2024-07-17 14:00:00.00000','2024-07-17 14:30:00.00000',22,232,NULL,1),(179082,'2024-07-17 15:00:00.00000','2024-07-17 15:30:00.00000',22,232,NULL,1),(179083,'2024-07-22 09:00:00.00000','2024-07-22 09:30:00.00000',22,232,NULL,1),(179084,'2024-07-22 10:00:00.00000','2024-07-22 10:30:00.00000',22,232,NULL,1),(179085,'2024-07-22 11:00:00.00000','2024-07-22 11:30:00.00000',22,232,NULL,1),(179086,'2024-07-22 12:00:00.00000','2024-07-22 12:30:00.00000',22,232,NULL,1),(179087,'2024-07-22 13:00:00.00000','2024-07-22 13:30:00.00000',22,232,NULL,1),(179088,'2024-07-22 14:00:00.00000','2024-07-22 14:30:00.00000',22,232,NULL,1),(179089,'2024-07-22 15:00:00.00000','2024-07-22 15:30:00.00000',22,232,NULL,1),(179090,'2024-07-22 16:00:00.00000','2024-07-22 16:30:00.00000',22,232,NULL,1),(179091,'2024-07-23 11:00:00.00000','2024-07-23 11:30:00.00000',22,232,NULL,1),(179092,'2024-07-23 12:00:00.00000','2024-07-23 12:30:00.00000',22,232,NULL,1),(179093,'2024-07-23 13:00:00.00000','2024-07-23 13:30:00.00000',22,232,NULL,1),(179094,'2024-07-23 14:00:00.00000','2024-07-23 14:30:00.00000',22,232,NULL,1),(179095,'2024-07-23 20:00:00.00000','2024-07-23 20:30:00.00000',22,232,NULL,1),(179096,'2024-07-23 21:00:00.00000','2024-07-23 21:30:00.00000',22,232,NULL,1),(179097,'2024-07-23 22:00:00.00000','2024-07-23 22:30:00.00000',22,232,NULL,1),(179098,'2024-07-24 08:00:00.00000','2024-07-24 08:30:00.00000',22,232,NULL,1),(179099,'2024-07-24 09:00:00.00000','2024-07-24 09:30:00.00000',22,232,NULL,1),(179100,'2024-07-24 10:00:00.00000','2024-07-24 10:30:00.00000',22,232,NULL,1),(179101,'2024-07-24 11:00:00.00000','2024-07-24 11:30:00.00000',22,232,NULL,1),(179102,'2024-07-24 12:00:00.00000','2024-07-24 12:30:00.00000',22,232,NULL,1),(179103,'2024-07-24 13:00:00.00000','2024-07-24 13:30:00.00000',22,232,NULL,1),(179104,'2024-07-24 14:00:00.00000','2024-07-24 14:30:00.00000',22,232,NULL,1),(179105,'2024-07-24 15:00:00.00000','2024-07-24 15:30:00.00000',22,232,NULL,1),(179106,'2024-07-29 09:00:00.00000','2024-07-29 09:30:00.00000',22,232,NULL,1),(179107,'2024-07-29 10:00:00.00000','2024-07-29 10:30:00.00000',22,232,NULL,1),(179108,'2024-07-29 11:00:00.00000','2024-07-29 11:30:00.00000',22,232,NULL,1),(179109,'2024-07-29 12:00:00.00000','2024-07-29 12:30:00.00000',22,232,NULL,1),(179110,'2024-07-29 13:00:00.00000','2024-07-29 13:30:00.00000',22,232,NULL,1),(179111,'2024-07-29 14:00:00.00000','2024-07-29 14:30:00.00000',22,232,NULL,1),(179112,'2024-07-29 15:00:00.00000','2024-07-29 15:30:00.00000',22,232,NULL,1),(179113,'2024-07-29 16:00:00.00000','2024-07-29 16:30:00.00000',22,232,NULL,1),(179114,'2024-07-30 11:00:00.00000','2024-07-30 11:30:00.00000',22,232,NULL,1),(179115,'2024-07-30 12:00:00.00000','2024-07-30 12:30:00.00000',22,232,NULL,1),(179116,'2024-07-30 13:00:00.00000','2024-07-30 13:30:00.00000',22,232,NULL,1),(179117,'2024-07-30 14:00:00.00000','2024-07-30 14:30:00.00000',22,232,NULL,1),(179118,'2024-07-30 20:00:00.00000','2024-07-30 20:30:00.00000',22,232,NULL,1),(179119,'2024-07-30 21:00:00.00000','2024-07-30 21:30:00.00000',22,232,NULL,1),(179120,'2024-07-30 22:00:00.00000','2024-07-30 22:30:00.00000',22,232,NULL,1),(179121,'2024-07-31 08:00:00.00000','2024-07-31 08:30:00.00000',22,232,NULL,1),(179122,'2024-07-31 09:00:00.00000','2024-07-31 09:30:00.00000',22,232,NULL,1),(179123,'2024-07-31 10:00:00.00000','2024-07-31 10:30:00.00000',22,232,NULL,1),(179124,'2024-07-31 11:00:00.00000','2024-07-31 11:30:00.00000',22,232,NULL,1),(179125,'2024-07-31 12:00:00.00000','2024-07-31 12:30:00.00000',22,232,NULL,1),(179126,'2024-07-31 13:00:00.00000','2024-07-31 13:30:00.00000',22,232,NULL,1),(179127,'2024-07-31 14:00:00.00000','2024-07-31 14:30:00.00000',22,232,NULL,1),(179128,'2024-07-31 15:00:00.00000','2024-07-31 15:30:00.00000',22,232,NULL,1),(179129,'2024-08-05 09:00:00.00000','2024-08-05 09:30:00.00000',22,232,NULL,1),(179130,'2024-08-05 10:00:00.00000','2024-08-05 10:30:00.00000',22,232,NULL,1),(179131,'2024-08-05 11:00:00.00000','2024-08-05 11:30:00.00000',22,232,NULL,1),(179132,'2024-08-05 12:00:00.00000','2024-08-05 12:30:00.00000',22,232,NULL,1),(179133,'2024-08-05 13:00:00.00000','2024-08-05 13:30:00.00000',22,232,NULL,1),(179134,'2024-08-05 14:00:00.00000','2024-08-05 14:30:00.00000',22,232,NULL,1),(179135,'2024-08-05 15:00:00.00000','2024-08-05 15:30:00.00000',22,232,NULL,0),(179136,'2024-08-05 16:00:00.00000','2024-08-05 16:30:00.00000',22,232,NULL,1),(179137,'2024-08-06 11:00:00.00000','2024-08-06 11:30:00.00000',22,232,NULL,1),(179138,'2024-08-06 12:00:00.00000','2024-08-06 12:30:00.00000',22,232,NULL,1),(179139,'2024-08-06 13:00:00.00000','2024-08-06 13:30:00.00000',22,232,NULL,1),(179140,'2024-08-06 14:00:00.00000','2024-08-06 14:30:00.00000',22,232,NULL,1),(179141,'2024-08-06 20:00:00.00000','2024-08-06 20:30:00.00000',22,232,NULL,1),(179142,'2024-08-06 21:00:00.00000','2024-08-06 21:30:00.00000',22,232,NULL,1),(179143,'2024-08-06 22:00:00.00000','2024-08-06 22:30:00.00000',22,232,NULL,1),(179144,'2024-08-07 08:00:00.00000','2024-08-07 08:30:00.00000',22,232,NULL,1),(179145,'2024-08-07 09:00:00.00000','2024-08-07 09:30:00.00000',22,232,NULL,1),(179146,'2024-08-07 10:00:00.00000','2024-08-07 10:30:00.00000',22,232,NULL,1),(179147,'2024-08-07 11:00:00.00000','2024-08-07 11:30:00.00000',22,232,NULL,1),(179148,'2024-08-07 12:00:00.00000','2024-08-07 12:30:00.00000',22,232,NULL,1),(179149,'2024-08-07 13:00:00.00000','2024-08-07 13:30:00.00000',22,232,NULL,1),(179150,'2024-08-07 14:00:00.00000','2024-08-07 14:30:00.00000',22,232,NULL,1),(179151,'2024-08-07 15:00:00.00000','2024-08-07 15:30:00.00000',22,232,NULL,1),(179152,'2024-08-12 09:00:00.00000','2024-08-12 09:30:00.00000',22,232,NULL,1),(179153,'2024-08-12 10:00:00.00000','2024-08-12 10:30:00.00000',22,232,NULL,0),(179154,'2024-08-12 11:00:00.00000','2024-08-12 11:30:00.00000',22,232,NULL,1),(179155,'2024-08-12 12:00:00.00000','2024-08-12 12:30:00.00000',22,232,NULL,0),(179156,'2024-08-12 13:00:00.00000','2024-08-12 13:30:00.00000',22,232,NULL,0),(179157,'2024-08-12 14:00:00.00000','2024-08-12 14:30:00.00000',22,232,NULL,0),(179158,'2024-08-12 15:00:00.00000','2024-08-12 15:30:00.00000',22,232,NULL,1),(179159,'2024-08-12 16:00:00.00000','2024-08-12 16:30:00.00000',22,232,NULL,0),(179160,'2024-08-13 11:00:00.00000','2024-08-13 11:30:00.00000',22,232,NULL,1),(179161,'2024-08-13 12:00:00.00000','2024-08-13 12:30:00.00000',22,232,NULL,1),(179162,'2024-08-13 13:00:00.00000','2024-08-13 13:30:00.00000',22,232,NULL,1),(179163,'2024-08-13 14:00:00.00000','2024-08-13 14:30:00.00000',22,232,NULL,1),(179164,'2024-08-13 20:00:00.00000','2024-08-13 20:30:00.00000',22,232,NULL,0),(179165,'2024-08-13 21:00:00.00000','2024-08-13 21:30:00.00000',22,232,NULL,1),(179166,'2024-08-13 22:00:00.00000','2024-08-13 22:30:00.00000',22,232,NULL,1),(179167,'2024-08-14 08:00:00.00000','2024-08-14 08:30:00.00000',22,232,NULL,1),(179168,'2024-08-14 09:00:00.00000','2024-08-14 09:30:00.00000',22,232,NULL,1),(179169,'2024-08-14 10:00:00.00000','2024-08-14 10:30:00.00000',22,232,NULL,1),(179170,'2024-08-14 11:00:00.00000','2024-08-14 11:30:00.00000',22,232,NULL,1),(179171,'2024-08-14 12:00:00.00000','2024-08-14 12:30:00.00000',22,232,NULL,1),(179172,'2024-08-14 13:00:00.00000','2024-08-14 13:30:00.00000',22,232,NULL,1),(179173,'2024-08-14 14:00:00.00000','2024-08-14 14:30:00.00000',22,232,NULL,1),(179174,'2024-08-14 15:00:00.00000','2024-08-14 15:30:00.00000',22,232,NULL,1),(179175,'2024-08-19 09:00:00.00000','2024-08-19 09:30:00.00000',22,232,NULL,1),(179176,'2024-08-19 10:00:00.00000','2024-08-19 10:30:00.00000',22,232,NULL,1),(179177,'2024-08-19 11:00:00.00000','2024-08-19 11:30:00.00000',22,232,NULL,1),(179178,'2024-08-19 12:00:00.00000','2024-08-19 12:30:00.00000',22,232,NULL,0),(179179,'2024-08-19 13:00:00.00000','2024-08-19 13:30:00.00000',22,232,NULL,1),(179180,'2024-08-19 14:00:00.00000','2024-08-19 14:30:00.00000',22,232,NULL,0),(179181,'2024-08-19 15:00:00.00000','2024-08-19 15:30:00.00000',22,232,NULL,1),(179182,'2024-08-19 16:00:00.00000','2024-08-19 16:30:00.00000',22,232,NULL,1),(179183,'2024-08-20 11:00:00.00000','2024-08-20 11:30:00.00000',22,232,NULL,1),(179184,'2024-08-20 12:00:00.00000','2024-08-20 12:30:00.00000',22,232,NULL,1),(179185,'2024-08-20 13:00:00.00000','2024-08-20 13:30:00.00000',22,232,NULL,1),(179186,'2024-08-20 14:00:00.00000','2024-08-20 14:30:00.00000',22,232,NULL,1),(179187,'2024-08-20 20:00:00.00000','2024-08-20 20:30:00.00000',22,232,NULL,1),(179188,'2024-08-20 21:00:00.00000','2024-08-20 21:30:00.00000',22,232,NULL,1),(179189,'2024-08-20 22:00:00.00000','2024-08-20 22:30:00.00000',22,232,NULL,1),(179190,'2024-08-21 08:00:00.00000','2024-08-21 08:30:00.00000',22,232,NULL,1),(179191,'2024-08-21 09:00:00.00000','2024-08-21 09:30:00.00000',22,232,NULL,1),(179192,'2024-08-21 10:00:00.00000','2024-08-21 10:30:00.00000',22,232,NULL,1),(179193,'2024-08-21 11:00:00.00000','2024-08-21 11:30:00.00000',22,232,NULL,1),(179194,'2024-08-21 12:00:00.00000','2024-08-21 12:30:00.00000',22,232,NULL,1),(179195,'2024-08-21 13:00:00.00000','2024-08-21 13:30:00.00000',22,232,NULL,1),(179196,'2024-08-21 14:00:00.00000','2024-08-21 14:30:00.00000',22,232,NULL,1),(179197,'2024-08-21 15:00:00.00000','2024-08-21 15:30:00.00000',22,232,NULL,1),(179198,'2024-08-26 09:00:00.00000','2024-08-26 09:30:00.00000',22,232,NULL,1),(179199,'2024-08-26 10:00:00.00000','2024-08-26 10:30:00.00000',22,232,NULL,1),(179200,'2024-08-26 11:00:00.00000','2024-08-26 11:30:00.00000',22,232,NULL,1),(179201,'2024-08-26 12:00:00.00000','2024-08-26 12:30:00.00000',22,232,NULL,1),(179202,'2024-08-26 13:00:00.00000','2024-08-26 13:30:00.00000',22,232,NULL,1),(179203,'2024-08-26 14:00:00.00000','2024-08-26 14:30:00.00000',22,232,NULL,1),(179204,'2024-08-26 15:00:00.00000','2024-08-26 15:30:00.00000',22,232,NULL,1),(179205,'2024-08-26 16:00:00.00000','2024-08-26 16:30:00.00000',22,232,NULL,1),(179206,'2024-08-27 11:00:00.00000','2024-08-27 11:30:00.00000',22,232,NULL,1),(179207,'2024-08-27 12:00:00.00000','2024-08-27 12:30:00.00000',22,232,NULL,1),(179208,'2024-08-27 13:00:00.00000','2024-08-27 13:30:00.00000',22,232,NULL,1),(179209,'2024-08-27 14:00:00.00000','2024-08-27 14:30:00.00000',22,232,NULL,1),(179210,'2024-08-27 20:00:00.00000','2024-08-27 20:30:00.00000',22,232,NULL,1),(179211,'2024-08-27 21:00:00.00000','2024-08-27 21:30:00.00000',22,232,NULL,1),(179212,'2024-08-27 22:00:00.00000','2024-08-27 22:30:00.00000',22,232,NULL,1),(179213,'2024-08-28 08:00:00.00000','2024-08-28 08:30:00.00000',22,232,NULL,1),(179214,'2024-08-28 09:00:00.00000','2024-08-28 09:30:00.00000',22,232,NULL,1),(179215,'2024-08-28 10:00:00.00000','2024-08-28 10:30:00.00000',22,232,NULL,1),(179216,'2024-08-28 11:00:00.00000','2024-08-28 11:30:00.00000',22,232,NULL,1),(179217,'2024-08-28 12:00:00.00000','2024-08-28 12:30:00.00000',22,232,NULL,1),(179218,'2024-08-28 13:00:00.00000','2024-08-28 13:30:00.00000',22,232,NULL,1),(179219,'2024-08-28 14:00:00.00000','2024-08-28 14:30:00.00000',22,232,NULL,1),(179220,'2024-08-28 15:00:00.00000','2024-08-28 15:30:00.00000',22,232,NULL,1),(179221,'2024-09-02 09:00:00.00000','2024-09-02 09:30:00.00000',22,232,NULL,1),(179222,'2024-09-02 10:00:00.00000','2024-09-02 10:30:00.00000',22,232,NULL,1),(179223,'2024-09-02 11:00:00.00000','2024-09-02 11:30:00.00000',22,232,NULL,1),(179224,'2024-09-02 12:00:00.00000','2024-09-02 12:30:00.00000',22,232,NULL,1),(179225,'2024-09-02 13:00:00.00000','2024-09-02 13:30:00.00000',22,232,NULL,1),(179226,'2024-09-02 14:00:00.00000','2024-09-02 14:30:00.00000',22,232,NULL,1),(179227,'2024-09-02 15:00:00.00000','2024-09-02 15:30:00.00000',22,232,NULL,1),(179228,'2024-09-02 16:00:00.00000','2024-09-02 16:30:00.00000',22,232,NULL,1),(179229,'2024-09-03 11:00:00.00000','2024-09-03 11:30:00.00000',22,232,NULL,1),(179230,'2024-09-03 12:00:00.00000','2024-09-03 12:30:00.00000',22,232,NULL,1),(179231,'2024-09-03 13:00:00.00000','2024-09-03 13:30:00.00000',22,232,NULL,1),(179232,'2024-09-03 14:00:00.00000','2024-09-03 14:30:00.00000',22,232,NULL,1),(179233,'2024-09-03 20:00:00.00000','2024-09-03 20:30:00.00000',22,232,NULL,1),(179234,'2024-09-03 21:00:00.00000','2024-09-03 21:30:00.00000',22,232,NULL,1),(179235,'2024-09-03 22:00:00.00000','2024-09-03 22:30:00.00000',22,232,NULL,1),(179236,'2024-09-04 08:00:00.00000','2024-09-04 08:30:00.00000',22,232,NULL,1),(179237,'2024-09-04 09:00:00.00000','2024-09-04 09:30:00.00000',22,232,NULL,1),(179238,'2024-09-04 10:00:00.00000','2024-09-04 10:30:00.00000',22,232,NULL,1),(179239,'2024-09-04 11:00:00.00000','2024-09-04 11:30:00.00000',22,232,NULL,1),(179240,'2024-09-04 12:00:00.00000','2024-09-04 12:30:00.00000',22,232,NULL,1),(179241,'2024-09-04 13:00:00.00000','2024-09-04 13:30:00.00000',22,232,NULL,1),(179242,'2024-09-04 14:00:00.00000','2024-09-04 14:30:00.00000',22,232,NULL,1),(179243,'2024-09-04 15:00:00.00000','2024-09-04 15:30:00.00000',22,232,NULL,1),(179244,'2024-09-09 09:00:00.00000','2024-09-09 09:30:00.00000',22,232,NULL,1),(179245,'2024-09-09 10:00:00.00000','2024-09-09 10:30:00.00000',22,232,NULL,0),(179246,'2024-09-09 11:00:00.00000','2024-09-09 11:30:00.00000',22,232,NULL,1),(179247,'2024-09-09 12:00:00.00000','2024-09-09 12:30:00.00000',22,232,NULL,1),(179248,'2024-09-09 13:00:00.00000','2024-09-09 13:30:00.00000',22,232,NULL,0),(179249,'2024-09-09 14:00:00.00000','2024-09-09 14:30:00.00000',22,232,NULL,1),(179250,'2024-09-09 15:00:00.00000','2024-09-09 15:30:00.00000',22,232,NULL,1),(179251,'2024-09-09 16:00:00.00000','2024-09-09 16:30:00.00000',22,232,NULL,1),(179252,'2024-09-10 11:00:00.00000','2024-09-10 11:30:00.00000',22,232,NULL,1),(179253,'2024-09-10 12:00:00.00000','2024-09-10 12:30:00.00000',22,232,NULL,1),(179254,'2024-09-10 13:00:00.00000','2024-09-10 13:30:00.00000',22,232,NULL,1),(179255,'2024-09-10 14:00:00.00000','2024-09-10 14:30:00.00000',22,232,NULL,1),(179256,'2024-09-10 20:00:00.00000','2024-09-10 20:30:00.00000',22,232,NULL,1),(179257,'2024-09-10 21:00:00.00000','2024-09-10 21:30:00.00000',22,232,NULL,1),(179258,'2024-09-10 22:00:00.00000','2024-09-10 22:30:00.00000',22,232,NULL,1),(179259,'2024-09-11 08:00:00.00000','2024-09-11 08:30:00.00000',22,232,NULL,1),(179260,'2024-09-11 09:00:00.00000','2024-09-11 09:30:00.00000',22,232,NULL,1),(179261,'2024-09-11 10:00:00.00000','2024-09-11 10:30:00.00000',22,232,NULL,1),(179262,'2024-09-11 11:00:00.00000','2024-09-11 11:30:00.00000',22,232,NULL,1),(179263,'2024-09-11 12:00:00.00000','2024-09-11 12:30:00.00000',22,232,NULL,1),(179264,'2024-09-11 13:00:00.00000','2024-09-11 13:30:00.00000',22,232,NULL,1),(179265,'2024-09-11 14:00:00.00000','2024-09-11 14:30:00.00000',22,232,NULL,1),(179266,'2024-09-11 15:00:00.00000','2024-09-11 15:30:00.00000',22,232,NULL,1),(179267,'2024-09-16 09:00:00.00000','2024-09-16 09:30:00.00000',22,232,NULL,1),(179268,'2024-09-16 10:00:00.00000','2024-09-16 10:30:00.00000',22,232,NULL,1),(179269,'2024-09-16 11:00:00.00000','2024-09-16 11:30:00.00000',22,232,NULL,1),(179270,'2024-09-16 12:00:00.00000','2024-09-16 12:30:00.00000',22,232,NULL,1),(179271,'2024-09-16 13:00:00.00000','2024-09-16 13:30:00.00000',22,232,NULL,1),(179272,'2024-09-16 14:00:00.00000','2024-09-16 14:30:00.00000',22,232,NULL,0),(179273,'2024-09-16 15:00:00.00000','2024-09-16 15:30:00.00000',22,232,NULL,1),(179274,'2024-09-16 16:00:00.00000','2024-09-16 16:30:00.00000',22,232,NULL,1),(179275,'2024-09-17 11:00:00.00000','2024-09-17 11:30:00.00000',22,232,NULL,1),(179276,'2024-09-17 12:00:00.00000','2024-09-17 12:30:00.00000',22,232,NULL,1),(179277,'2024-09-17 13:00:00.00000','2024-09-17 13:30:00.00000',22,232,NULL,1),(179278,'2024-09-17 14:00:00.00000','2024-09-17 14:30:00.00000',22,232,NULL,0),(179279,'2024-09-17 20:00:00.00000','2024-09-17 20:30:00.00000',22,232,NULL,1),(179280,'2024-09-17 21:00:00.00000','2024-09-17 21:30:00.00000',22,232,NULL,1),(179281,'2024-09-17 22:00:00.00000','2024-09-17 22:30:00.00000',22,232,NULL,1),(179282,'2024-09-18 08:00:00.00000','2024-09-18 08:30:00.00000',22,232,NULL,1),(179283,'2024-09-18 09:00:00.00000','2024-09-18 09:30:00.00000',22,232,NULL,1),(179284,'2024-09-18 10:00:00.00000','2024-09-18 10:30:00.00000',22,232,NULL,1),(179285,'2024-09-18 11:00:00.00000','2024-09-18 11:30:00.00000',22,232,NULL,1),(179286,'2024-09-18 12:00:00.00000','2024-09-18 12:30:00.00000',22,232,NULL,1),(179287,'2024-09-18 13:00:00.00000','2024-09-18 13:30:00.00000',22,232,NULL,1),(179288,'2024-09-18 14:00:00.00000','2024-09-18 14:30:00.00000',22,232,NULL,1),(179289,'2024-09-18 15:00:00.00000','2024-09-18 15:30:00.00000',22,232,NULL,1),(179290,'2024-09-23 09:00:00.00000','2024-09-23 09:30:00.00000',22,232,NULL,1),(179291,'2024-09-23 10:00:00.00000','2024-09-23 10:30:00.00000',22,232,NULL,1),(179292,'2024-09-23 11:00:00.00000','2024-09-23 11:30:00.00000',22,232,NULL,1),(179293,'2024-09-23 12:00:00.00000','2024-09-23 12:30:00.00000',22,232,NULL,1),(179294,'2024-09-23 13:00:00.00000','2024-09-23 13:30:00.00000',22,232,NULL,1),(179295,'2024-09-23 14:00:00.00000','2024-09-23 14:30:00.00000',22,232,NULL,1),(179296,'2024-09-23 15:00:00.00000','2024-09-23 15:30:00.00000',22,232,NULL,1),(179297,'2024-09-23 16:00:00.00000','2024-09-23 16:30:00.00000',22,232,NULL,1),(179298,'2024-09-24 11:00:00.00000','2024-09-24 11:30:00.00000',22,232,NULL,1),(179299,'2024-09-24 12:00:00.00000','2024-09-24 12:30:00.00000',22,232,NULL,1),(179300,'2024-09-24 13:00:00.00000','2024-09-24 13:30:00.00000',22,232,NULL,1),(179301,'2024-09-24 14:00:00.00000','2024-09-24 14:30:00.00000',22,232,NULL,1),(179302,'2024-09-24 20:00:00.00000','2024-09-24 20:30:00.00000',22,232,NULL,1),(179303,'2024-09-24 21:00:00.00000','2024-09-24 21:30:00.00000',22,232,NULL,1),(179304,'2024-09-24 22:00:00.00000','2024-09-24 22:30:00.00000',22,232,NULL,1),(179305,'2024-09-25 08:00:00.00000','2024-09-25 08:30:00.00000',22,232,NULL,1),(179306,'2024-09-25 09:00:00.00000','2024-09-25 09:30:00.00000',22,232,NULL,1),(179307,'2024-09-25 10:00:00.00000','2024-09-25 10:30:00.00000',22,232,NULL,1),(179308,'2024-09-25 11:00:00.00000','2024-09-25 11:30:00.00000',22,232,NULL,1),(179309,'2024-09-25 12:00:00.00000','2024-09-25 12:30:00.00000',22,232,NULL,1),(179310,'2024-09-25 13:00:00.00000','2024-09-25 13:30:00.00000',22,232,NULL,1),(179311,'2024-09-25 14:00:00.00000','2024-09-25 14:30:00.00000',22,232,NULL,1),(179312,'2024-09-25 15:00:00.00000','2024-09-25 15:30:00.00000',22,232,NULL,1),(179313,'2024-09-30 09:00:00.00000','2024-09-30 09:30:00.00000',22,232,NULL,1),(179314,'2024-09-30 10:00:00.00000','2024-09-30 10:30:00.00000',22,232,NULL,1),(179315,'2024-09-30 11:00:00.00000','2024-09-30 11:30:00.00000',22,232,NULL,1),(179316,'2024-09-30 12:00:00.00000','2024-09-30 12:30:00.00000',22,232,NULL,1),(179317,'2024-09-30 13:00:00.00000','2024-09-30 13:30:00.00000',22,232,NULL,1),(179318,'2024-09-30 14:00:00.00000','2024-09-30 14:30:00.00000',22,232,NULL,1),(179319,'2024-09-30 15:00:00.00000','2024-09-30 15:30:00.00000',22,232,NULL,1),(179320,'2024-09-30 16:00:00.00000','2024-09-30 16:30:00.00000',22,232,NULL,1),(179321,'2024-10-01 11:00:00.00000','2024-10-01 11:30:00.00000',22,232,NULL,1),(179322,'2024-10-01 12:00:00.00000','2024-10-01 12:30:00.00000',22,232,NULL,1),(179323,'2024-10-01 13:00:00.00000','2024-10-01 13:30:00.00000',22,232,NULL,1),(179324,'2024-10-01 14:00:00.00000','2024-10-01 14:30:00.00000',22,232,NULL,1),(179325,'2024-10-01 20:00:00.00000','2024-10-01 20:30:00.00000',22,232,NULL,1),(179326,'2024-10-01 21:00:00.00000','2024-10-01 21:30:00.00000',22,232,NULL,1),(179327,'2024-10-01 22:00:00.00000','2024-10-01 22:30:00.00000',22,232,NULL,1),(179328,'2024-10-02 08:00:00.00000','2024-10-02 08:30:00.00000',22,232,NULL,1),(179329,'2024-10-02 09:00:00.00000','2024-10-02 09:30:00.00000',22,232,NULL,1),(179330,'2024-10-02 10:00:00.00000','2024-10-02 10:30:00.00000',22,232,NULL,1),(179331,'2024-10-02 11:00:00.00000','2024-10-02 11:30:00.00000',22,232,NULL,1),(179332,'2024-10-02 12:00:00.00000','2024-10-02 12:30:00.00000',22,232,NULL,1),(179333,'2024-10-02 13:00:00.00000','2024-10-02 13:30:00.00000',22,232,NULL,1),(179334,'2024-10-02 14:00:00.00000','2024-10-02 14:30:00.00000',22,232,NULL,1),(179335,'2024-10-02 15:00:00.00000','2024-10-02 15:30:00.00000',22,232,NULL,1),(179336,'2024-10-07 09:00:00.00000','2024-10-07 09:30:00.00000',22,232,NULL,1),(179337,'2024-10-07 10:00:00.00000','2024-10-07 10:30:00.00000',22,232,NULL,1),(179338,'2024-10-07 11:00:00.00000','2024-10-07 11:30:00.00000',22,232,NULL,1),(179339,'2024-10-07 12:00:00.00000','2024-10-07 12:30:00.00000',22,232,NULL,1),(179340,'2024-10-07 13:00:00.00000','2024-10-07 13:30:00.00000',22,232,NULL,1),(179341,'2024-10-07 14:00:00.00000','2024-10-07 14:30:00.00000',22,232,NULL,1),(179342,'2024-10-07 15:00:00.00000','2024-10-07 15:30:00.00000',22,232,NULL,1),(179343,'2024-10-07 16:00:00.00000','2024-10-07 16:30:00.00000',22,232,NULL,1),(179344,'2024-10-08 11:00:00.00000','2024-10-08 11:30:00.00000',22,232,NULL,1),(179345,'2024-10-08 12:00:00.00000','2024-10-08 12:30:00.00000',22,232,NULL,1),(179346,'2024-10-08 13:00:00.00000','2024-10-08 13:30:00.00000',22,232,NULL,1),(179347,'2024-10-08 14:00:00.00000','2024-10-08 14:30:00.00000',22,232,NULL,1),(179348,'2024-10-08 20:00:00.00000','2024-10-08 20:30:00.00000',22,232,NULL,1),(179349,'2024-10-08 21:00:00.00000','2024-10-08 21:30:00.00000',22,232,NULL,1),(179350,'2024-10-08 22:00:00.00000','2024-10-08 22:30:00.00000',22,232,NULL,1),(179351,'2024-10-09 08:00:00.00000','2024-10-09 08:30:00.00000',22,232,NULL,1),(179352,'2024-10-09 09:00:00.00000','2024-10-09 09:30:00.00000',22,232,NULL,1),(179353,'2024-10-09 10:00:00.00000','2024-10-09 10:30:00.00000',22,232,NULL,1),(179354,'2024-10-09 11:00:00.00000','2024-10-09 11:30:00.00000',22,232,NULL,1),(179355,'2024-10-09 12:00:00.00000','2024-10-09 12:30:00.00000',22,232,NULL,1),(179356,'2024-10-09 13:00:00.00000','2024-10-09 13:30:00.00000',22,232,NULL,1),(179357,'2024-10-09 14:00:00.00000','2024-10-09 14:30:00.00000',22,232,NULL,1),(179358,'2024-10-09 15:00:00.00000','2024-10-09 15:30:00.00000',22,232,NULL,1),(179359,'2024-10-14 09:00:00.00000','2024-10-14 09:30:00.00000',22,232,NULL,1),(179360,'2024-10-14 10:00:00.00000','2024-10-14 10:30:00.00000',22,232,NULL,1),(179361,'2024-10-14 11:00:00.00000','2024-10-14 11:30:00.00000',22,232,NULL,1),(179362,'2024-10-14 12:00:00.00000','2024-10-14 12:30:00.00000',22,232,NULL,1),(179363,'2024-10-14 13:00:00.00000','2024-10-14 13:30:00.00000',22,232,NULL,1),(179364,'2024-10-14 14:00:00.00000','2024-10-14 14:30:00.00000',22,232,NULL,1),(179365,'2024-10-14 15:00:00.00000','2024-10-14 15:30:00.00000',22,232,NULL,1),(179366,'2024-10-14 16:00:00.00000','2024-10-14 16:30:00.00000',22,232,NULL,1),(179367,'2024-10-15 11:00:00.00000','2024-10-15 11:30:00.00000',22,232,NULL,1),(179368,'2024-10-15 12:00:00.00000','2024-10-15 12:30:00.00000',22,232,NULL,1),(179369,'2024-10-15 13:00:00.00000','2024-10-15 13:30:00.00000',22,232,NULL,1),(179370,'2024-10-15 14:00:00.00000','2024-10-15 14:30:00.00000',22,232,NULL,1),(179371,'2024-10-15 20:00:00.00000','2024-10-15 20:30:00.00000',22,232,NULL,1),(179372,'2024-10-15 21:00:00.00000','2024-10-15 21:30:00.00000',22,232,NULL,1),(179373,'2024-10-15 22:00:00.00000','2024-10-15 22:30:00.00000',22,232,NULL,1),(179374,'2024-10-16 08:00:00.00000','2024-10-16 08:30:00.00000',22,232,NULL,1),(179375,'2024-10-16 09:00:00.00000','2024-10-16 09:30:00.00000',22,232,NULL,1),(179376,'2024-10-16 10:00:00.00000','2024-10-16 10:30:00.00000',22,232,NULL,1),(179377,'2024-10-16 11:00:00.00000','2024-10-16 11:30:00.00000',22,232,NULL,1),(179378,'2024-10-16 12:00:00.00000','2024-10-16 12:30:00.00000',22,232,NULL,1),(179379,'2024-10-16 13:00:00.00000','2024-10-16 13:30:00.00000',22,232,NULL,1),(179380,'2024-10-16 14:00:00.00000','2024-10-16 14:30:00.00000',22,232,NULL,1),(179381,'2024-10-16 15:00:00.00000','2024-10-16 15:30:00.00000',22,232,NULL,1),(179382,'2024-10-21 09:00:00.00000','2024-10-21 09:30:00.00000',22,232,NULL,1),(179383,'2024-10-21 10:00:00.00000','2024-10-21 10:30:00.00000',22,232,NULL,1),(179384,'2024-10-21 11:00:00.00000','2024-10-21 11:30:00.00000',22,232,NULL,1),(179385,'2024-10-21 12:00:00.00000','2024-10-21 12:30:00.00000',22,232,NULL,1),(179386,'2024-10-21 13:00:00.00000','2024-10-21 13:30:00.00000',22,232,NULL,1),(179387,'2024-10-21 14:00:00.00000','2024-10-21 14:30:00.00000',22,232,NULL,1),(179388,'2024-10-21 15:00:00.00000','2024-10-21 15:30:00.00000',22,232,NULL,1),(179389,'2024-10-21 16:00:00.00000','2024-10-21 16:30:00.00000',22,232,NULL,1),(179390,'2024-10-22 11:00:00.00000','2024-10-22 11:30:00.00000',22,232,NULL,1),(179391,'2024-10-22 12:00:00.00000','2024-10-22 12:30:00.00000',22,232,NULL,1),(179392,'2024-10-22 13:00:00.00000','2024-10-22 13:30:00.00000',22,232,NULL,1),(179393,'2024-10-22 14:00:00.00000','2024-10-22 14:30:00.00000',22,232,NULL,1),(179394,'2024-10-22 20:00:00.00000','2024-10-22 20:30:00.00000',22,232,NULL,1),(179395,'2024-10-22 21:00:00.00000','2024-10-22 21:30:00.00000',22,232,NULL,1),(179396,'2024-10-22 22:00:00.00000','2024-10-22 22:30:00.00000',22,232,NULL,1),(179397,'2024-10-23 08:00:00.00000','2024-10-23 08:30:00.00000',22,232,NULL,1),(179398,'2024-10-23 09:00:00.00000','2024-10-23 09:30:00.00000',22,232,NULL,1),(179399,'2024-10-23 10:00:00.00000','2024-10-23 10:30:00.00000',22,232,NULL,1),(179400,'2024-10-23 11:00:00.00000','2024-10-23 11:30:00.00000',22,232,NULL,1),(179401,'2024-10-23 12:00:00.00000','2024-10-23 12:30:00.00000',22,232,NULL,1),(179402,'2024-10-23 13:00:00.00000','2024-10-23 13:30:00.00000',22,232,NULL,1),(179403,'2024-10-23 14:00:00.00000','2024-10-23 14:30:00.00000',22,232,NULL,1),(179404,'2024-10-23 15:00:00.00000','2024-10-23 15:30:00.00000',22,232,NULL,1),(179405,'2024-10-28 09:00:00.00000','2024-10-28 09:30:00.00000',22,232,NULL,1),(179406,'2024-10-28 10:00:00.00000','2024-10-28 10:30:00.00000',22,232,NULL,1),(179407,'2024-10-28 11:00:00.00000','2024-10-28 11:30:00.00000',22,232,NULL,1),(179408,'2024-10-28 12:00:00.00000','2024-10-28 12:30:00.00000',22,232,NULL,1),(179409,'2024-10-28 13:00:00.00000','2024-10-28 13:30:00.00000',22,232,NULL,1),(179410,'2024-10-28 14:00:00.00000','2024-10-28 14:30:00.00000',22,232,NULL,1),(179411,'2024-10-28 15:00:00.00000','2024-10-28 15:30:00.00000',22,232,NULL,1),(179412,'2024-10-28 16:00:00.00000','2024-10-28 16:30:00.00000',22,232,NULL,1),(179413,'2024-10-29 11:00:00.00000','2024-10-29 11:30:00.00000',22,232,NULL,1),(179414,'2024-10-29 12:00:00.00000','2024-10-29 12:30:00.00000',22,232,NULL,1),(179415,'2024-10-29 13:00:00.00000','2024-10-29 13:30:00.00000',22,232,NULL,1),(179416,'2024-10-29 14:00:00.00000','2024-10-29 14:30:00.00000',22,232,NULL,1),(179417,'2024-10-29 20:00:00.00000','2024-10-29 20:30:00.00000',22,232,NULL,1),(179418,'2024-10-29 21:00:00.00000','2024-10-29 21:30:00.00000',22,232,NULL,1),(179419,'2024-10-29 22:00:00.00000','2024-10-29 22:30:00.00000',22,232,NULL,1),(179420,'2024-10-30 08:00:00.00000','2024-10-30 08:30:00.00000',22,232,NULL,1),(179421,'2024-10-30 09:00:00.00000','2024-10-30 09:30:00.00000',22,232,NULL,1),(179422,'2024-10-30 10:00:00.00000','2024-10-30 10:30:00.00000',22,232,NULL,1),(179423,'2024-10-30 11:00:00.00000','2024-10-30 11:30:00.00000',22,232,NULL,1),(179424,'2024-10-30 12:00:00.00000','2024-10-30 12:30:00.00000',22,232,NULL,1),(179425,'2024-10-30 13:00:00.00000','2024-10-30 13:30:00.00000',22,232,NULL,1),(179426,'2024-10-30 14:00:00.00000','2024-10-30 14:30:00.00000',22,232,NULL,1),(179427,'2024-10-30 15:00:00.00000','2024-10-30 15:30:00.00000',22,232,NULL,1),(179428,'2024-11-04 09:00:00.00000','2024-11-04 09:30:00.00000',22,232,NULL,1),(179429,'2024-11-04 10:00:00.00000','2024-11-04 10:30:00.00000',22,232,NULL,1),(179430,'2024-11-04 11:00:00.00000','2024-11-04 11:30:00.00000',22,232,NULL,1),(179431,'2024-11-04 12:00:00.00000','2024-11-04 12:30:00.00000',22,232,NULL,1),(179432,'2024-11-04 13:00:00.00000','2024-11-04 13:30:00.00000',22,232,NULL,1),(179433,'2024-11-04 14:00:00.00000','2024-11-04 14:30:00.00000',22,232,NULL,1),(179434,'2024-11-04 15:00:00.00000','2024-11-04 15:30:00.00000',22,232,NULL,1),(179435,'2024-11-04 16:00:00.00000','2024-11-04 16:30:00.00000',22,232,NULL,1),(179436,'2024-11-05 11:00:00.00000','2024-11-05 11:30:00.00000',22,232,NULL,1),(179437,'2024-11-05 12:00:00.00000','2024-11-05 12:30:00.00000',22,232,NULL,1),(179438,'2024-11-05 13:00:00.00000','2024-11-05 13:30:00.00000',22,232,NULL,1),(179439,'2024-11-05 14:00:00.00000','2024-11-05 14:30:00.00000',22,232,NULL,1),(179440,'2024-11-05 20:00:00.00000','2024-11-05 20:30:00.00000',22,232,NULL,1),(179441,'2024-11-05 21:00:00.00000','2024-11-05 21:30:00.00000',22,232,NULL,1),(179442,'2024-11-05 22:00:00.00000','2024-11-05 22:30:00.00000',22,232,NULL,1),(179443,'2024-11-06 08:00:00.00000','2024-11-06 08:30:00.00000',22,232,NULL,1),(179444,'2024-11-06 09:00:00.00000','2024-11-06 09:30:00.00000',22,232,NULL,1),(179445,'2024-11-06 10:00:00.00000','2024-11-06 10:30:00.00000',22,232,NULL,1),(179446,'2024-11-06 11:00:00.00000','2024-11-06 11:30:00.00000',22,232,NULL,1),(179447,'2024-11-06 12:00:00.00000','2024-11-06 12:30:00.00000',22,232,NULL,1),(179448,'2024-11-06 13:00:00.00000','2024-11-06 13:30:00.00000',22,232,NULL,1),(179449,'2024-11-06 14:00:00.00000','2024-11-06 14:30:00.00000',22,232,NULL,1),(179450,'2024-11-06 15:00:00.00000','2024-11-06 15:30:00.00000',22,232,NULL,1),(179451,'2024-11-11 09:00:00.00000','2024-11-11 09:30:00.00000',22,232,NULL,1),(179452,'2024-11-11 10:00:00.00000','2024-11-11 10:30:00.00000',22,232,NULL,1),(179453,'2024-11-11 11:00:00.00000','2024-11-11 11:30:00.00000',22,232,NULL,1),(179454,'2024-11-11 12:00:00.00000','2024-11-11 12:30:00.00000',22,232,NULL,1),(179455,'2024-11-11 13:00:00.00000','2024-11-11 13:30:00.00000',22,232,NULL,1),(179456,'2024-11-11 14:00:00.00000','2024-11-11 14:30:00.00000',22,232,NULL,1),(179457,'2024-11-11 15:00:00.00000','2024-11-11 15:30:00.00000',22,232,NULL,1),(179458,'2024-11-11 16:00:00.00000','2024-11-11 16:30:00.00000',22,232,NULL,1),(179459,'2024-11-12 11:00:00.00000','2024-11-12 11:30:00.00000',22,232,NULL,1),(179460,'2024-11-12 12:00:00.00000','2024-11-12 12:30:00.00000',22,232,NULL,1),(179461,'2024-11-12 13:00:00.00000','2024-11-12 13:30:00.00000',22,232,NULL,1),(179462,'2024-11-12 14:00:00.00000','2024-11-12 14:30:00.00000',22,232,NULL,1),(179463,'2024-11-12 20:00:00.00000','2024-11-12 20:30:00.00000',22,232,NULL,1),(179464,'2024-11-12 21:00:00.00000','2024-11-12 21:30:00.00000',22,232,NULL,1),(179465,'2024-11-12 22:00:00.00000','2024-11-12 22:30:00.00000',22,232,NULL,1),(179466,'2024-11-13 08:00:00.00000','2024-11-13 08:30:00.00000',22,232,NULL,1),(179467,'2024-11-13 09:00:00.00000','2024-11-13 09:30:00.00000',22,232,NULL,1),(179468,'2024-11-13 10:00:00.00000','2024-11-13 10:30:00.00000',22,232,NULL,1),(179469,'2024-11-13 11:00:00.00000','2024-11-13 11:30:00.00000',22,232,NULL,1),(179470,'2024-11-13 12:00:00.00000','2024-11-13 12:30:00.00000',22,232,NULL,1),(179471,'2024-11-13 13:00:00.00000','2024-11-13 13:30:00.00000',22,232,NULL,1),(179472,'2024-11-13 14:00:00.00000','2024-11-13 14:30:00.00000',22,232,NULL,1),(179473,'2024-11-13 15:00:00.00000','2024-11-13 15:30:00.00000',22,232,NULL,1),(179474,'2024-11-18 09:00:00.00000','2024-11-18 09:30:00.00000',22,232,NULL,1),(179475,'2024-11-18 10:00:00.00000','2024-11-18 10:30:00.00000',22,232,NULL,1),(179476,'2024-11-18 11:00:00.00000','2024-11-18 11:30:00.00000',22,232,NULL,1),(179477,'2024-11-18 12:00:00.00000','2024-11-18 12:30:00.00000',22,232,NULL,1),(179478,'2024-11-18 13:00:00.00000','2024-11-18 13:30:00.00000',22,232,NULL,1),(179479,'2024-11-18 14:00:00.00000','2024-11-18 14:30:00.00000',22,232,NULL,1),(179480,'2024-11-18 15:00:00.00000','2024-11-18 15:30:00.00000',22,232,NULL,1),(179481,'2024-11-18 16:00:00.00000','2024-11-18 16:30:00.00000',22,232,NULL,1),(179482,'2024-11-19 11:00:00.00000','2024-11-19 11:30:00.00000',22,232,NULL,1),(179483,'2024-11-19 12:00:00.00000','2024-11-19 12:30:00.00000',22,232,NULL,1),(179484,'2024-11-19 13:00:00.00000','2024-11-19 13:30:00.00000',22,232,NULL,1),(179485,'2024-11-19 14:00:00.00000','2024-11-19 14:30:00.00000',22,232,NULL,1),(179486,'2024-11-19 20:00:00.00000','2024-11-19 20:30:00.00000',22,232,NULL,1),(179487,'2024-11-19 21:00:00.00000','2024-11-19 21:30:00.00000',22,232,NULL,1),(179488,'2024-11-19 22:00:00.00000','2024-11-19 22:30:00.00000',22,232,NULL,1),(179489,'2024-11-20 08:00:00.00000','2024-11-20 08:30:00.00000',22,232,NULL,1),(179490,'2024-11-20 09:00:00.00000','2024-11-20 09:30:00.00000',22,232,NULL,1),(179491,'2024-11-20 10:00:00.00000','2024-11-20 10:30:00.00000',22,232,NULL,1),(179492,'2024-11-20 11:00:00.00000','2024-11-20 11:30:00.00000',22,232,NULL,1),(179493,'2024-11-20 12:00:00.00000','2024-11-20 12:30:00.00000',22,232,NULL,1),(179494,'2024-11-20 13:00:00.00000','2024-11-20 13:30:00.00000',22,232,NULL,1),(179495,'2024-11-20 14:00:00.00000','2024-11-20 14:30:00.00000',22,232,NULL,1),(179496,'2024-11-20 15:00:00.00000','2024-11-20 15:30:00.00000',22,232,NULL,1),(179497,'2024-11-25 09:00:00.00000','2024-11-25 09:30:00.00000',22,232,NULL,1),(179498,'2024-11-25 10:00:00.00000','2024-11-25 10:30:00.00000',22,232,NULL,1),(179499,'2024-11-25 11:00:00.00000','2024-11-25 11:30:00.00000',22,232,NULL,1),(179500,'2024-11-25 12:00:00.00000','2024-11-25 12:30:00.00000',22,232,NULL,1),(179501,'2024-11-25 13:00:00.00000','2024-11-25 13:30:00.00000',22,232,NULL,1),(179502,'2024-11-25 14:00:00.00000','2024-11-25 14:30:00.00000',22,232,NULL,1),(179503,'2024-11-25 15:00:00.00000','2024-11-25 15:30:00.00000',22,232,NULL,1),(179504,'2024-11-25 16:00:00.00000','2024-11-25 16:30:00.00000',22,232,NULL,1),(179505,'2024-11-26 11:00:00.00000','2024-11-26 11:30:00.00000',22,232,NULL,1),(179506,'2024-11-26 12:00:00.00000','2024-11-26 12:30:00.00000',22,232,NULL,1),(179507,'2024-11-26 13:00:00.00000','2024-11-26 13:30:00.00000',22,232,NULL,1),(179508,'2024-11-26 14:00:00.00000','2024-11-26 14:30:00.00000',22,232,NULL,1),(179509,'2024-11-26 20:00:00.00000','2024-11-26 20:30:00.00000',22,232,NULL,1),(179510,'2024-11-26 21:00:00.00000','2024-11-26 21:30:00.00000',22,232,NULL,1),(179511,'2024-11-26 22:00:00.00000','2024-11-26 22:30:00.00000',22,232,NULL,1),(179512,'2024-11-27 08:00:00.00000','2024-11-27 08:30:00.00000',22,232,NULL,1),(179513,'2024-11-27 09:00:00.00000','2024-11-27 09:30:00.00000',22,232,NULL,1),(179514,'2024-11-27 10:00:00.00000','2024-11-27 10:30:00.00000',22,232,NULL,1),(179515,'2024-11-27 11:00:00.00000','2024-11-27 11:30:00.00000',22,232,NULL,1),(179516,'2024-11-27 12:00:00.00000','2024-11-27 12:30:00.00000',22,232,NULL,1),(179517,'2024-11-27 13:00:00.00000','2024-11-27 13:30:00.00000',22,232,NULL,1),(179518,'2024-11-27 14:00:00.00000','2024-11-27 14:30:00.00000',22,232,NULL,1),(179519,'2024-11-27 15:00:00.00000','2024-11-27 15:30:00.00000',22,232,NULL,1),(179520,'2024-12-02 09:00:00.00000','2024-12-02 09:30:00.00000',22,232,NULL,1),(179521,'2024-12-02 10:00:00.00000','2024-12-02 10:30:00.00000',22,232,NULL,1),(179522,'2024-12-02 11:00:00.00000','2024-12-02 11:30:00.00000',22,232,NULL,1),(179523,'2024-12-02 12:00:00.00000','2024-12-02 12:30:00.00000',22,232,NULL,1),(179524,'2024-12-02 13:00:00.00000','2024-12-02 13:30:00.00000',22,232,NULL,1),(179525,'2024-12-02 14:00:00.00000','2024-12-02 14:30:00.00000',22,232,NULL,1),(179526,'2024-12-02 15:00:00.00000','2024-12-02 15:30:00.00000',22,232,NULL,1),(179527,'2024-12-02 16:00:00.00000','2024-12-02 16:30:00.00000',22,232,NULL,1),(179528,'2024-12-03 11:00:00.00000','2024-12-03 11:30:00.00000',22,232,NULL,1),(179529,'2024-12-03 12:00:00.00000','2024-12-03 12:30:00.00000',22,232,NULL,1),(179530,'2024-12-03 13:00:00.00000','2024-12-03 13:30:00.00000',22,232,NULL,1),(179531,'2024-12-03 14:00:00.00000','2024-12-03 14:30:00.00000',22,232,NULL,1),(179532,'2024-12-03 20:00:00.00000','2024-12-03 20:30:00.00000',22,232,NULL,1),(179533,'2024-12-03 21:00:00.00000','2024-12-03 21:30:00.00000',22,232,NULL,1),(179534,'2024-12-03 22:00:00.00000','2024-12-03 22:30:00.00000',22,232,NULL,1),(179535,'2024-12-04 08:00:00.00000','2024-12-04 08:30:00.00000',22,232,NULL,1),(179536,'2024-12-04 09:00:00.00000','2024-12-04 09:30:00.00000',22,232,NULL,1),(179537,'2024-12-04 10:00:00.00000','2024-12-04 10:30:00.00000',22,232,NULL,1),(179538,'2024-12-04 11:00:00.00000','2024-12-04 11:30:00.00000',22,232,NULL,1),(179539,'2024-12-04 12:00:00.00000','2024-12-04 12:30:00.00000',22,232,NULL,1),(179540,'2024-12-04 13:00:00.00000','2024-12-04 13:30:00.00000',22,232,NULL,1),(179541,'2024-12-04 14:00:00.00000','2024-12-04 14:30:00.00000',22,232,NULL,1),(179542,'2024-12-04 15:00:00.00000','2024-12-04 15:30:00.00000',22,232,NULL,1),(179543,'2024-12-09 09:00:00.00000','2024-12-09 09:30:00.00000',22,232,NULL,1),(179544,'2024-12-09 10:00:00.00000','2024-12-09 10:30:00.00000',22,232,NULL,1),(179545,'2024-12-09 11:00:00.00000','2024-12-09 11:30:00.00000',22,232,NULL,1),(179546,'2024-12-09 12:00:00.00000','2024-12-09 12:30:00.00000',22,232,NULL,1),(179547,'2024-12-09 13:00:00.00000','2024-12-09 13:30:00.00000',22,232,NULL,1),(179548,'2024-12-09 14:00:00.00000','2024-12-09 14:30:00.00000',22,232,NULL,1),(179549,'2024-12-09 15:00:00.00000','2024-12-09 15:30:00.00000',22,232,NULL,1),(179550,'2024-12-09 16:00:00.00000','2024-12-09 16:30:00.00000',22,232,NULL,1),(179551,'2024-12-10 11:00:00.00000','2024-12-10 11:30:00.00000',22,232,NULL,1),(179552,'2024-12-10 12:00:00.00000','2024-12-10 12:30:00.00000',22,232,NULL,1),(179553,'2024-12-10 13:00:00.00000','2024-12-10 13:30:00.00000',22,232,NULL,1),(179554,'2024-12-10 14:00:00.00000','2024-12-10 14:30:00.00000',22,232,NULL,1),(179555,'2024-12-10 20:00:00.00000','2024-12-10 20:30:00.00000',22,232,NULL,1),(179556,'2024-12-10 21:00:00.00000','2024-12-10 21:30:00.00000',22,232,NULL,1),(179557,'2024-12-10 22:00:00.00000','2024-12-10 22:30:00.00000',22,232,NULL,1),(179558,'2024-12-11 08:00:00.00000','2024-12-11 08:30:00.00000',22,232,NULL,1),(179559,'2024-12-11 09:00:00.00000','2024-12-11 09:30:00.00000',22,232,NULL,1),(179560,'2024-12-11 10:00:00.00000','2024-12-11 10:30:00.00000',22,232,NULL,1),(179561,'2024-12-11 11:00:00.00000','2024-12-11 11:30:00.00000',22,232,NULL,1),(179562,'2024-12-11 12:00:00.00000','2024-12-11 12:30:00.00000',22,232,NULL,1),(179563,'2024-12-11 13:00:00.00000','2024-12-11 13:30:00.00000',22,232,NULL,1),(179564,'2024-12-11 14:00:00.00000','2024-12-11 14:30:00.00000',22,232,NULL,1),(179565,'2024-12-11 15:00:00.00000','2024-12-11 15:30:00.00000',22,232,NULL,1),(179566,'2024-12-16 09:00:00.00000','2024-12-16 09:30:00.00000',22,232,NULL,1),(179567,'2024-12-16 10:00:00.00000','2024-12-16 10:30:00.00000',22,232,NULL,1),(179568,'2024-12-16 11:00:00.00000','2024-12-16 11:30:00.00000',22,232,NULL,1),(179569,'2024-12-16 12:00:00.00000','2024-12-16 12:30:00.00000',22,232,NULL,1),(179570,'2024-12-16 13:00:00.00000','2024-12-16 13:30:00.00000',22,232,NULL,1),(179571,'2024-12-16 14:00:00.00000','2024-12-16 14:30:00.00000',22,232,NULL,1),(179572,'2024-12-16 15:00:00.00000','2024-12-16 15:30:00.00000',22,232,NULL,1),(179573,'2024-12-16 16:00:00.00000','2024-12-16 16:30:00.00000',22,232,NULL,1),(179574,'2024-12-17 11:00:00.00000','2024-12-17 11:30:00.00000',22,232,NULL,1),(179575,'2024-12-17 12:00:00.00000','2024-12-17 12:30:00.00000',22,232,NULL,1),(179576,'2024-12-17 13:00:00.00000','2024-12-17 13:30:00.00000',22,232,NULL,1),(179577,'2024-12-17 14:00:00.00000','2024-12-17 14:30:00.00000',22,232,NULL,1),(179578,'2024-12-17 20:00:00.00000','2024-12-17 20:30:00.00000',22,232,NULL,1),(179579,'2024-12-17 21:00:00.00000','2024-12-17 21:30:00.00000',22,232,NULL,1),(179580,'2024-12-17 22:00:00.00000','2024-12-17 22:30:00.00000',22,232,NULL,1),(179581,'2024-12-18 08:00:00.00000','2024-12-18 08:30:00.00000',22,232,NULL,1),(179582,'2024-12-18 09:00:00.00000','2024-12-18 09:30:00.00000',22,232,NULL,1),(179583,'2024-12-18 10:00:00.00000','2024-12-18 10:30:00.00000',22,232,NULL,1),(179584,'2024-12-18 11:00:00.00000','2024-12-18 11:30:00.00000',22,232,NULL,1),(179585,'2024-12-18 12:00:00.00000','2024-12-18 12:30:00.00000',22,232,NULL,1),(179586,'2024-12-18 13:00:00.00000','2024-12-18 13:30:00.00000',22,232,NULL,1),(179587,'2024-12-18 14:00:00.00000','2024-12-18 14:30:00.00000',22,232,NULL,1),(179588,'2024-12-18 15:00:00.00000','2024-12-18 15:30:00.00000',22,232,NULL,1),(179589,'2024-12-23 09:00:00.00000','2024-12-23 09:30:00.00000',22,232,NULL,1),(179590,'2024-12-23 10:00:00.00000','2024-12-23 10:30:00.00000',22,232,NULL,1),(179591,'2024-12-23 11:00:00.00000','2024-12-23 11:30:00.00000',22,232,NULL,1),(179592,'2024-12-23 12:00:00.00000','2024-12-23 12:30:00.00000',22,232,NULL,1),(179593,'2024-12-23 13:00:00.00000','2024-12-23 13:30:00.00000',22,232,NULL,1),(179594,'2024-12-23 14:00:00.00000','2024-12-23 14:30:00.00000',22,232,NULL,1),(179595,'2024-12-23 15:00:00.00000','2024-12-23 15:30:00.00000',22,232,NULL,1),(179596,'2024-12-23 16:00:00.00000','2024-12-23 16:30:00.00000',22,232,NULL,1),(179597,'2024-12-24 11:00:00.00000','2024-12-24 11:30:00.00000',22,232,NULL,1),(179598,'2024-12-24 12:00:00.00000','2024-12-24 12:30:00.00000',22,232,NULL,1),(179599,'2024-12-24 13:00:00.00000','2024-12-24 13:30:00.00000',22,232,NULL,1),(179600,'2024-12-24 14:00:00.00000','2024-12-24 14:30:00.00000',22,232,NULL,1),(179601,'2024-12-24 20:00:00.00000','2024-12-24 20:30:00.00000',22,232,NULL,1),(179602,'2024-12-24 21:00:00.00000','2024-12-24 21:30:00.00000',22,232,NULL,1),(179603,'2024-12-24 22:00:00.00000','2024-12-24 22:30:00.00000',22,232,NULL,1),(179604,'2024-12-25 08:00:00.00000','2024-12-25 08:30:00.00000',22,232,NULL,1),(179605,'2024-12-25 09:00:00.00000','2024-12-25 09:30:00.00000',22,232,NULL,1),(179606,'2024-12-25 10:00:00.00000','2024-12-25 10:30:00.00000',22,232,NULL,1),(179607,'2024-12-25 11:00:00.00000','2024-12-25 11:30:00.00000',22,232,NULL,1),(179608,'2024-12-25 12:00:00.00000','2024-12-25 12:30:00.00000',22,232,NULL,1),(179609,'2024-12-25 13:00:00.00000','2024-12-25 13:30:00.00000',22,232,NULL,1),(179610,'2024-12-25 14:00:00.00000','2024-12-25 14:30:00.00000',22,232,NULL,1),(179611,'2024-12-25 15:00:00.00000','2024-12-25 15:30:00.00000',22,232,NULL,1),(179612,'2024-12-30 09:00:00.00000','2024-12-30 09:30:00.00000',22,232,NULL,1),(179613,'2024-12-30 10:00:00.00000','2024-12-30 10:30:00.00000',22,232,NULL,1),(179614,'2024-12-30 11:00:00.00000','2024-12-30 11:30:00.00000',22,232,NULL,1),(179615,'2024-12-30 12:00:00.00000','2024-12-30 12:30:00.00000',22,232,NULL,1),(179616,'2024-12-30 13:00:00.00000','2024-12-30 13:30:00.00000',22,232,NULL,1),(179617,'2024-12-30 14:00:00.00000','2024-12-30 14:30:00.00000',22,232,NULL,1),(179618,'2024-12-30 15:00:00.00000','2024-12-30 15:30:00.00000',22,232,NULL,1),(179619,'2024-12-30 16:00:00.00000','2024-12-30 16:30:00.00000',22,232,NULL,1),(179620,'2024-12-31 11:00:00.00000','2024-12-31 11:30:00.00000',22,232,NULL,1),(179621,'2024-12-31 12:00:00.00000','2024-12-31 12:30:00.00000',22,232,NULL,1),(179622,'2024-12-31 13:00:00.00000','2024-12-31 13:30:00.00000',22,232,NULL,1),(179623,'2024-12-31 14:00:00.00000','2024-12-31 14:30:00.00000',22,232,NULL,1),(179624,'2024-12-31 20:00:00.00000','2024-12-31 20:30:00.00000',22,232,NULL,1),(179625,'2024-12-31 21:00:00.00000','2024-12-31 21:30:00.00000',22,232,NULL,1),(179626,'2024-12-31 22:00:00.00000','2024-12-31 22:30:00.00000',22,232,NULL,1),(179627,'2025-01-01 08:00:00.00000','2025-01-01 08:30:00.00000',22,232,NULL,1),(179628,'2025-01-01 09:00:00.00000','2025-01-01 09:30:00.00000',22,232,NULL,1),(179629,'2025-01-01 10:00:00.00000','2025-01-01 10:30:00.00000',22,232,NULL,1),(179630,'2025-01-01 11:00:00.00000','2025-01-01 11:30:00.00000',22,232,NULL,1),(179631,'2025-01-01 12:00:00.00000','2025-01-01 12:30:00.00000',22,232,NULL,1),(179632,'2025-01-01 13:00:00.00000','2025-01-01 13:30:00.00000',22,232,NULL,1),(179633,'2025-01-01 14:00:00.00000','2025-01-01 14:30:00.00000',22,232,NULL,1),(179634,'2025-01-01 15:00:00.00000','2025-01-01 15:30:00.00000',22,232,NULL,1),(179635,'2025-01-06 09:00:00.00000','2025-01-06 09:30:00.00000',22,232,NULL,1),(179636,'2025-01-06 10:00:00.00000','2025-01-06 10:30:00.00000',22,232,NULL,1),(179637,'2025-01-06 11:00:00.00000','2025-01-06 11:30:00.00000',22,232,NULL,1),(179638,'2025-01-06 12:00:00.00000','2025-01-06 12:30:00.00000',22,232,NULL,1),(179639,'2025-01-06 13:00:00.00000','2025-01-06 13:30:00.00000',22,232,NULL,1),(179640,'2025-01-06 14:00:00.00000','2025-01-06 14:30:00.00000',22,232,NULL,1),(179641,'2025-01-06 15:00:00.00000','2025-01-06 15:30:00.00000',22,232,NULL,1),(179642,'2025-01-06 16:00:00.00000','2025-01-06 16:30:00.00000',22,232,NULL,1),(179643,'2025-01-07 11:00:00.00000','2025-01-07 11:30:00.00000',22,232,NULL,1),(179644,'2025-01-07 12:00:00.00000','2025-01-07 12:30:00.00000',22,232,NULL,1),(179645,'2025-01-07 13:00:00.00000','2025-01-07 13:30:00.00000',22,232,NULL,1),(179646,'2025-01-07 14:00:00.00000','2025-01-07 14:30:00.00000',22,232,NULL,1),(179647,'2025-01-07 20:00:00.00000','2025-01-07 20:30:00.00000',22,232,NULL,1),(179648,'2025-01-07 21:00:00.00000','2025-01-07 21:30:00.00000',22,232,NULL,1),(179649,'2025-01-07 22:00:00.00000','2025-01-07 22:30:00.00000',22,232,NULL,1),(179650,'2025-01-08 08:00:00.00000','2025-01-08 08:30:00.00000',22,232,NULL,1),(179651,'2025-01-08 09:00:00.00000','2025-01-08 09:30:00.00000',22,232,NULL,1),(179652,'2025-01-08 10:00:00.00000','2025-01-08 10:30:00.00000',22,232,NULL,1),(179653,'2025-01-08 11:00:00.00000','2025-01-08 11:30:00.00000',22,232,NULL,1),(179654,'2025-01-08 12:00:00.00000','2025-01-08 12:30:00.00000',22,232,NULL,1),(179655,'2025-01-08 13:00:00.00000','2025-01-08 13:30:00.00000',22,232,NULL,1),(179656,'2025-01-08 14:00:00.00000','2025-01-08 14:30:00.00000',22,232,NULL,1),(179657,'2025-01-08 15:00:00.00000','2025-01-08 15:30:00.00000',22,232,NULL,1),(179658,'2025-01-13 09:00:00.00000','2025-01-13 09:30:00.00000',22,232,NULL,1),(179659,'2025-01-13 10:00:00.00000','2025-01-13 10:30:00.00000',22,232,NULL,1),(179660,'2025-01-13 11:00:00.00000','2025-01-13 11:30:00.00000',22,232,NULL,1),(179661,'2025-01-13 12:00:00.00000','2025-01-13 12:30:00.00000',22,232,NULL,1),(179662,'2025-01-13 13:00:00.00000','2025-01-13 13:30:00.00000',22,232,NULL,1),(179663,'2025-01-13 14:00:00.00000','2025-01-13 14:30:00.00000',22,232,NULL,1),(179664,'2025-01-13 15:00:00.00000','2025-01-13 15:30:00.00000',22,232,NULL,1),(179665,'2025-01-13 16:00:00.00000','2025-01-13 16:30:00.00000',22,232,NULL,1),(179666,'2025-01-14 11:00:00.00000','2025-01-14 11:30:00.00000',22,232,NULL,1),(179667,'2025-01-14 12:00:00.00000','2025-01-14 12:30:00.00000',22,232,NULL,1),(179668,'2025-01-14 13:00:00.00000','2025-01-14 13:30:00.00000',22,232,NULL,1),(179669,'2025-01-14 14:00:00.00000','2025-01-14 14:30:00.00000',22,232,NULL,1),(179670,'2025-01-14 20:00:00.00000','2025-01-14 20:30:00.00000',22,232,NULL,1),(179671,'2025-01-14 21:00:00.00000','2025-01-14 21:30:00.00000',22,232,NULL,1),(179672,'2025-01-14 22:00:00.00000','2025-01-14 22:30:00.00000',22,232,NULL,1),(179673,'2025-01-15 08:00:00.00000','2025-01-15 08:30:00.00000',22,232,NULL,1),(179674,'2025-01-15 09:00:00.00000','2025-01-15 09:30:00.00000',22,232,NULL,1),(179675,'2025-01-15 10:00:00.00000','2025-01-15 10:30:00.00000',22,232,NULL,1),(179676,'2025-01-15 11:00:00.00000','2025-01-15 11:30:00.00000',22,232,NULL,1),(179677,'2025-01-15 12:00:00.00000','2025-01-15 12:30:00.00000',22,232,NULL,1),(179678,'2025-01-15 13:00:00.00000','2025-01-15 13:30:00.00000',22,232,NULL,1),(179679,'2025-01-15 14:00:00.00000','2025-01-15 14:30:00.00000',22,232,NULL,1),(179680,'2025-01-15 15:00:00.00000','2025-01-15 15:30:00.00000',22,232,NULL,1),(179681,'2025-01-20 09:00:00.00000','2025-01-20 09:30:00.00000',22,232,NULL,1),(179682,'2025-01-20 10:00:00.00000','2025-01-20 10:30:00.00000',22,232,NULL,1),(179683,'2025-01-20 11:00:00.00000','2025-01-20 11:30:00.00000',22,232,NULL,1),(179684,'2025-01-20 12:00:00.00000','2025-01-20 12:30:00.00000',22,232,NULL,1),(179685,'2025-01-20 13:00:00.00000','2025-01-20 13:30:00.00000',22,232,NULL,1),(179686,'2025-01-20 14:00:00.00000','2025-01-20 14:30:00.00000',22,232,NULL,1),(179687,'2025-01-20 15:00:00.00000','2025-01-20 15:30:00.00000',22,232,NULL,1),(179688,'2025-01-20 16:00:00.00000','2025-01-20 16:30:00.00000',22,232,NULL,1),(179689,'2025-01-21 11:00:00.00000','2025-01-21 11:30:00.00000',22,232,NULL,1),(179690,'2025-01-21 12:00:00.00000','2025-01-21 12:30:00.00000',22,232,NULL,1),(179691,'2025-01-21 13:00:00.00000','2025-01-21 13:30:00.00000',22,232,NULL,1),(179692,'2025-01-21 14:00:00.00000','2025-01-21 14:30:00.00000',22,232,NULL,1),(179693,'2025-01-21 20:00:00.00000','2025-01-21 20:30:00.00000',22,232,NULL,1),(179694,'2025-01-21 21:00:00.00000','2025-01-21 21:30:00.00000',22,232,NULL,1),(179695,'2025-01-21 22:00:00.00000','2025-01-21 22:30:00.00000',22,232,NULL,1),(179696,'2025-01-22 08:00:00.00000','2025-01-22 08:30:00.00000',22,232,NULL,1),(179697,'2025-01-22 09:00:00.00000','2025-01-22 09:30:00.00000',22,232,NULL,1),(179698,'2025-01-22 10:00:00.00000','2025-01-22 10:30:00.00000',22,232,NULL,1),(179699,'2025-01-22 11:00:00.00000','2025-01-22 11:30:00.00000',22,232,NULL,1),(179700,'2025-01-22 12:00:00.00000','2025-01-22 12:30:00.00000',22,232,NULL,1),(179701,'2025-01-22 13:00:00.00000','2025-01-22 13:30:00.00000',22,232,NULL,1),(179702,'2025-01-22 14:00:00.00000','2025-01-22 14:30:00.00000',22,232,NULL,1),(179703,'2025-01-22 15:00:00.00000','2025-01-22 15:30:00.00000',22,232,NULL,1),(179704,'2025-01-27 09:00:00.00000','2025-01-27 09:30:00.00000',22,232,NULL,1),(179705,'2025-01-27 10:00:00.00000','2025-01-27 10:30:00.00000',22,232,NULL,1),(179706,'2025-01-27 11:00:00.00000','2025-01-27 11:30:00.00000',22,232,NULL,1),(179707,'2025-01-27 12:00:00.00000','2025-01-27 12:30:00.00000',22,232,NULL,1),(179708,'2025-01-27 13:00:00.00000','2025-01-27 13:30:00.00000',22,232,NULL,1),(179709,'2025-01-27 14:00:00.00000','2025-01-27 14:30:00.00000',22,232,NULL,1),(179710,'2025-01-27 15:00:00.00000','2025-01-27 15:30:00.00000',22,232,NULL,1),(179711,'2025-01-27 16:00:00.00000','2025-01-27 16:30:00.00000',22,232,NULL,1),(179712,'2025-01-28 11:00:00.00000','2025-01-28 11:30:00.00000',22,232,NULL,1),(179713,'2025-01-28 12:00:00.00000','2025-01-28 12:30:00.00000',22,232,NULL,1),(179714,'2025-01-28 13:00:00.00000','2025-01-28 13:30:00.00000',22,232,NULL,1),(179715,'2025-01-28 14:00:00.00000','2025-01-28 14:30:00.00000',22,232,NULL,1),(179716,'2025-01-28 20:00:00.00000','2025-01-28 20:30:00.00000',22,232,NULL,1),(179717,'2025-01-28 21:00:00.00000','2025-01-28 21:30:00.00000',22,232,NULL,1),(179718,'2025-01-28 22:00:00.00000','2025-01-28 22:30:00.00000',22,232,NULL,1),(179719,'2025-01-29 08:00:00.00000','2025-01-29 08:30:00.00000',22,232,NULL,1),(179720,'2025-01-29 09:00:00.00000','2025-01-29 09:30:00.00000',22,232,NULL,1),(179721,'2025-01-29 10:00:00.00000','2025-01-29 10:30:00.00000',22,232,NULL,1),(179722,'2025-01-29 11:00:00.00000','2025-01-29 11:30:00.00000',22,232,NULL,1),(179723,'2025-01-29 12:00:00.00000','2025-01-29 12:30:00.00000',22,232,NULL,1),(179724,'2025-01-29 13:00:00.00000','2025-01-29 13:30:00.00000',22,232,NULL,1),(179725,'2025-01-29 14:00:00.00000','2025-01-29 14:30:00.00000',22,232,NULL,1),(179726,'2025-01-29 15:00:00.00000','2025-01-29 15:30:00.00000',22,232,NULL,1),(179727,'2025-02-03 09:00:00.00000','2025-02-03 09:30:00.00000',22,232,NULL,1),(179728,'2025-02-03 10:00:00.00000','2025-02-03 10:30:00.00000',22,232,NULL,1),(179729,'2025-02-03 11:00:00.00000','2025-02-03 11:30:00.00000',22,232,NULL,1),(179730,'2025-02-03 12:00:00.00000','2025-02-03 12:30:00.00000',22,232,NULL,1),(179731,'2025-02-03 13:00:00.00000','2025-02-03 13:30:00.00000',22,232,NULL,1),(179732,'2025-02-03 14:00:00.00000','2025-02-03 14:30:00.00000',22,232,NULL,1),(179733,'2025-02-03 15:00:00.00000','2025-02-03 15:30:00.00000',22,232,NULL,1),(179734,'2025-02-03 16:00:00.00000','2025-02-03 16:30:00.00000',22,232,NULL,1),(179735,'2025-02-04 11:00:00.00000','2025-02-04 11:30:00.00000',22,232,NULL,1),(179736,'2025-02-04 12:00:00.00000','2025-02-04 12:30:00.00000',22,232,NULL,1),(179737,'2025-02-04 13:00:00.00000','2025-02-04 13:30:00.00000',22,232,NULL,1),(179738,'2025-02-04 14:00:00.00000','2025-02-04 14:30:00.00000',22,232,NULL,1),(179739,'2025-02-04 20:00:00.00000','2025-02-04 20:30:00.00000',22,232,NULL,1),(179740,'2025-02-04 21:00:00.00000','2025-02-04 21:30:00.00000',22,232,NULL,1),(179741,'2025-02-04 22:00:00.00000','2025-02-04 22:30:00.00000',22,232,NULL,1),(179742,'2025-02-05 08:00:00.00000','2025-02-05 08:30:00.00000',22,232,NULL,1),(179743,'2025-02-05 09:00:00.00000','2025-02-05 09:30:00.00000',22,232,NULL,1),(179744,'2025-02-05 10:00:00.00000','2025-02-05 10:30:00.00000',22,232,NULL,1),(179745,'2025-02-05 11:00:00.00000','2025-02-05 11:30:00.00000',22,232,NULL,1),(179746,'2025-02-05 12:00:00.00000','2025-02-05 12:30:00.00000',22,232,NULL,1),(179747,'2025-02-05 13:00:00.00000','2025-02-05 13:30:00.00000',22,232,NULL,1),(179748,'2025-02-05 14:00:00.00000','2025-02-05 14:30:00.00000',22,232,NULL,1),(179749,'2025-02-05 15:00:00.00000','2025-02-05 15:30:00.00000',22,232,NULL,1),(179750,'2025-02-10 09:00:00.00000','2025-02-10 09:30:00.00000',22,232,NULL,1),(179751,'2025-02-10 10:00:00.00000','2025-02-10 10:30:00.00000',22,232,NULL,1),(179752,'2025-02-10 11:00:00.00000','2025-02-10 11:30:00.00000',22,232,NULL,1),(179753,'2025-02-10 12:00:00.00000','2025-02-10 12:30:00.00000',22,232,NULL,1),(179754,'2025-02-10 13:00:00.00000','2025-02-10 13:30:00.00000',22,232,NULL,1),(179755,'2025-02-10 14:00:00.00000','2025-02-10 14:30:00.00000',22,232,NULL,1),(179756,'2025-02-10 15:00:00.00000','2025-02-10 15:30:00.00000',22,232,NULL,1),(179757,'2025-02-10 16:00:00.00000','2025-02-10 16:30:00.00000',22,232,NULL,1),(179758,'2025-02-11 11:00:00.00000','2025-02-11 11:30:00.00000',22,232,NULL,1),(179759,'2025-02-11 12:00:00.00000','2025-02-11 12:30:00.00000',22,232,NULL,1),(179760,'2025-02-11 13:00:00.00000','2025-02-11 13:30:00.00000',22,232,NULL,1),(179761,'2025-02-11 14:00:00.00000','2025-02-11 14:30:00.00000',22,232,NULL,1),(179762,'2025-02-11 20:00:00.00000','2025-02-11 20:30:00.00000',22,232,NULL,1),(179763,'2025-02-11 21:00:00.00000','2025-02-11 21:30:00.00000',22,232,NULL,1),(179764,'2025-02-11 22:00:00.00000','2025-02-11 22:30:00.00000',22,232,NULL,1),(179765,'2025-02-12 08:00:00.00000','2025-02-12 08:30:00.00000',22,232,NULL,1),(179766,'2025-02-12 09:00:00.00000','2025-02-12 09:30:00.00000',22,232,NULL,1),(179767,'2025-02-12 10:00:00.00000','2025-02-12 10:30:00.00000',22,232,NULL,1),(179768,'2025-02-12 11:00:00.00000','2025-02-12 11:30:00.00000',22,232,NULL,1),(179769,'2025-02-12 12:00:00.00000','2025-02-12 12:30:00.00000',22,232,NULL,1),(179770,'2025-02-12 13:00:00.00000','2025-02-12 13:30:00.00000',22,232,NULL,1),(179771,'2025-02-12 14:00:00.00000','2025-02-12 14:30:00.00000',22,232,NULL,1),(179772,'2025-02-12 15:00:00.00000','2025-02-12 15:30:00.00000',22,232,NULL,1),(179773,'2025-02-17 09:00:00.00000','2025-02-17 09:30:00.00000',22,232,NULL,1),(179774,'2025-02-17 10:00:00.00000','2025-02-17 10:30:00.00000',22,232,NULL,1),(179775,'2025-02-17 11:00:00.00000','2025-02-17 11:30:00.00000',22,232,NULL,1),(179776,'2025-02-17 12:00:00.00000','2025-02-17 12:30:00.00000',22,232,NULL,1),(179777,'2025-02-17 13:00:00.00000','2025-02-17 13:30:00.00000',22,232,NULL,1),(179778,'2025-02-17 14:00:00.00000','2025-02-17 14:30:00.00000',22,232,NULL,1),(179779,'2025-02-17 15:00:00.00000','2025-02-17 15:30:00.00000',22,232,NULL,1),(179780,'2025-02-17 16:00:00.00000','2025-02-17 16:30:00.00000',22,232,NULL,1),(179781,'2025-02-18 11:00:00.00000','2025-02-18 11:30:00.00000',22,232,NULL,1),(179782,'2025-02-18 12:00:00.00000','2025-02-18 12:30:00.00000',22,232,NULL,1),(179783,'2025-02-18 13:00:00.00000','2025-02-18 13:30:00.00000',22,232,NULL,1),(179784,'2025-02-18 14:00:00.00000','2025-02-18 14:30:00.00000',22,232,NULL,1),(179785,'2025-02-18 20:00:00.00000','2025-02-18 20:30:00.00000',22,232,NULL,1),(179786,'2025-02-18 21:00:00.00000','2025-02-18 21:30:00.00000',22,232,NULL,1),(179787,'2025-02-18 22:00:00.00000','2025-02-18 22:30:00.00000',22,232,NULL,1),(179788,'2025-02-19 08:00:00.00000','2025-02-19 08:30:00.00000',22,232,NULL,1),(179789,'2025-02-19 09:00:00.00000','2025-02-19 09:30:00.00000',22,232,NULL,1),(179790,'2025-02-19 10:00:00.00000','2025-02-19 10:30:00.00000',22,232,NULL,1),(179791,'2025-02-19 11:00:00.00000','2025-02-19 11:30:00.00000',22,232,NULL,1),(179792,'2025-02-19 12:00:00.00000','2025-02-19 12:30:00.00000',22,232,NULL,1),(179793,'2025-02-19 13:00:00.00000','2025-02-19 13:30:00.00000',22,232,NULL,1),(179794,'2025-02-19 14:00:00.00000','2025-02-19 14:30:00.00000',22,232,NULL,1),(179795,'2025-02-19 15:00:00.00000','2025-02-19 15:30:00.00000',22,232,NULL,1),(179796,'2025-02-24 09:00:00.00000','2025-02-24 09:30:00.00000',22,232,NULL,1),(179797,'2025-02-24 10:00:00.00000','2025-02-24 10:30:00.00000',22,232,NULL,1),(179798,'2025-02-24 11:00:00.00000','2025-02-24 11:30:00.00000',22,232,NULL,1),(179799,'2025-02-24 12:00:00.00000','2025-02-24 12:30:00.00000',22,232,NULL,1),(179800,'2025-02-24 13:00:00.00000','2025-02-24 13:30:00.00000',22,232,NULL,1),(179801,'2025-02-24 14:00:00.00000','2025-02-24 14:30:00.00000',22,232,NULL,1),(179802,'2025-02-24 15:00:00.00000','2025-02-24 15:30:00.00000',22,232,NULL,1),(179803,'2025-02-24 16:00:00.00000','2025-02-24 16:30:00.00000',22,232,NULL,1),(179804,'2025-02-25 11:00:00.00000','2025-02-25 11:30:00.00000',22,232,NULL,1),(179805,'2025-02-25 12:00:00.00000','2025-02-25 12:30:00.00000',22,232,NULL,1),(179806,'2025-02-25 13:00:00.00000','2025-02-25 13:30:00.00000',22,232,NULL,1),(179807,'2025-02-25 14:00:00.00000','2025-02-25 14:30:00.00000',22,232,NULL,1),(179808,'2025-02-25 20:00:00.00000','2025-02-25 20:30:00.00000',22,232,NULL,1),(179809,'2025-02-25 21:00:00.00000','2025-02-25 21:30:00.00000',22,232,NULL,1),(179810,'2025-02-25 22:00:00.00000','2025-02-25 22:30:00.00000',22,232,NULL,1),(179811,'2025-02-26 08:00:00.00000','2025-02-26 08:30:00.00000',22,232,NULL,1),(179812,'2025-02-26 09:00:00.00000','2025-02-26 09:30:00.00000',22,232,NULL,1),(179813,'2025-02-26 10:00:00.00000','2025-02-26 10:30:00.00000',22,232,NULL,1),(179814,'2025-02-26 11:00:00.00000','2025-02-26 11:30:00.00000',22,232,NULL,1),(179815,'2025-02-26 12:00:00.00000','2025-02-26 12:30:00.00000',22,232,NULL,1),(179816,'2025-02-26 13:00:00.00000','2025-02-26 13:30:00.00000',22,232,NULL,1),(179817,'2025-02-26 14:00:00.00000','2025-02-26 14:30:00.00000',22,232,NULL,1),(179818,'2025-02-26 15:00:00.00000','2025-02-26 15:30:00.00000',22,232,NULL,1),(179819,'2025-03-03 09:00:00.00000','2025-03-03 09:30:00.00000',22,232,NULL,1),(179820,'2025-03-03 10:00:00.00000','2025-03-03 10:30:00.00000',22,232,NULL,1),(179821,'2025-03-03 11:00:00.00000','2025-03-03 11:30:00.00000',22,232,NULL,1),(179822,'2025-03-03 12:00:00.00000','2025-03-03 12:30:00.00000',22,232,NULL,1),(179823,'2025-03-03 13:00:00.00000','2025-03-03 13:30:00.00000',22,232,NULL,1),(179824,'2025-03-03 14:00:00.00000','2025-03-03 14:30:00.00000',22,232,NULL,1),(179825,'2025-03-03 15:00:00.00000','2025-03-03 15:30:00.00000',22,232,NULL,1),(179826,'2025-03-03 16:00:00.00000','2025-03-03 16:30:00.00000',22,232,NULL,1),(179827,'2025-03-04 11:00:00.00000','2025-03-04 11:30:00.00000',22,232,NULL,1),(179828,'2025-03-04 12:00:00.00000','2025-03-04 12:30:00.00000',22,232,NULL,1),(179829,'2025-03-04 13:00:00.00000','2025-03-04 13:30:00.00000',22,232,NULL,1),(179830,'2025-03-04 14:00:00.00000','2025-03-04 14:30:00.00000',22,232,NULL,1),(179831,'2025-03-04 20:00:00.00000','2025-03-04 20:30:00.00000',22,232,NULL,1),(179832,'2025-03-04 21:00:00.00000','2025-03-04 21:30:00.00000',22,232,NULL,1),(179833,'2025-03-04 22:00:00.00000','2025-03-04 22:30:00.00000',22,232,NULL,1),(179834,'2025-03-05 08:00:00.00000','2025-03-05 08:30:00.00000',22,232,NULL,1),(179835,'2025-03-05 09:00:00.00000','2025-03-05 09:30:00.00000',22,232,NULL,1),(179836,'2025-03-05 10:00:00.00000','2025-03-05 10:30:00.00000',22,232,NULL,1),(179837,'2025-03-05 11:00:00.00000','2025-03-05 11:30:00.00000',22,232,NULL,1),(179838,'2025-03-05 12:00:00.00000','2025-03-05 12:30:00.00000',22,232,NULL,1),(179839,'2025-03-05 13:00:00.00000','2025-03-05 13:30:00.00000',22,232,NULL,1),(179840,'2025-03-05 14:00:00.00000','2025-03-05 14:30:00.00000',22,232,NULL,1),(179841,'2025-03-05 15:00:00.00000','2025-03-05 15:30:00.00000',22,232,NULL,1),(179842,'2025-03-10 09:00:00.00000','2025-03-10 09:30:00.00000',22,232,NULL,1),(179843,'2025-03-10 10:00:00.00000','2025-03-10 10:30:00.00000',22,232,NULL,1),(179844,'2025-03-10 11:00:00.00000','2025-03-10 11:30:00.00000',22,232,NULL,1),(179845,'2025-03-10 12:00:00.00000','2025-03-10 12:30:00.00000',22,232,NULL,1),(179846,'2025-03-10 13:00:00.00000','2025-03-10 13:30:00.00000',22,232,NULL,1),(179847,'2025-03-10 14:00:00.00000','2025-03-10 14:30:00.00000',22,232,NULL,1),(179848,'2025-03-10 15:00:00.00000','2025-03-10 15:30:00.00000',22,232,NULL,1),(179849,'2025-03-10 16:00:00.00000','2025-03-10 16:30:00.00000',22,232,NULL,1),(179850,'2025-03-11 11:00:00.00000','2025-03-11 11:30:00.00000',22,232,NULL,1),(179851,'2025-03-11 12:00:00.00000','2025-03-11 12:30:00.00000',22,232,NULL,1),(179852,'2025-03-11 13:00:00.00000','2025-03-11 13:30:00.00000',22,232,NULL,1),(179853,'2025-03-11 14:00:00.00000','2025-03-11 14:30:00.00000',22,232,NULL,1),(179854,'2025-03-11 20:00:00.00000','2025-03-11 20:30:00.00000',22,232,NULL,1),(179855,'2025-03-11 21:00:00.00000','2025-03-11 21:30:00.00000',22,232,NULL,1),(179856,'2025-03-11 22:00:00.00000','2025-03-11 22:30:00.00000',22,232,NULL,1),(179857,'2025-03-12 08:00:00.00000','2025-03-12 08:30:00.00000',22,232,NULL,1),(179858,'2025-03-12 09:00:00.00000','2025-03-12 09:30:00.00000',22,232,NULL,1),(179859,'2025-03-12 10:00:00.00000','2025-03-12 10:30:00.00000',22,232,NULL,1),(179860,'2025-03-12 11:00:00.00000','2025-03-12 11:30:00.00000',22,232,NULL,1),(179861,'2025-03-12 12:00:00.00000','2025-03-12 12:30:00.00000',22,232,NULL,1),(179862,'2025-03-12 13:00:00.00000','2025-03-12 13:30:00.00000',22,232,NULL,1),(179863,'2025-03-12 14:00:00.00000','2025-03-12 14:30:00.00000',22,232,NULL,1),(179864,'2025-03-12 15:00:00.00000','2025-03-12 15:30:00.00000',22,232,NULL,1),(179865,'2025-03-17 09:00:00.00000','2025-03-17 09:30:00.00000',22,232,NULL,1),(179866,'2025-03-17 10:00:00.00000','2025-03-17 10:30:00.00000',22,232,NULL,1),(179867,'2025-03-17 11:00:00.00000','2025-03-17 11:30:00.00000',22,232,NULL,1),(179868,'2025-03-17 12:00:00.00000','2025-03-17 12:30:00.00000',22,232,NULL,1),(179869,'2025-03-17 13:00:00.00000','2025-03-17 13:30:00.00000',22,232,NULL,1),(179870,'2025-03-17 14:00:00.00000','2025-03-17 14:30:00.00000',22,232,NULL,1),(179871,'2025-03-17 15:00:00.00000','2025-03-17 15:30:00.00000',22,232,NULL,1),(179872,'2025-03-17 16:00:00.00000','2025-03-17 16:30:00.00000',22,232,NULL,1),(179873,'2025-03-18 11:00:00.00000','2025-03-18 11:30:00.00000',22,232,NULL,1),(179874,'2025-03-18 12:00:00.00000','2025-03-18 12:30:00.00000',22,232,NULL,1),(179875,'2025-03-18 13:00:00.00000','2025-03-18 13:30:00.00000',22,232,NULL,1),(179876,'2025-03-18 14:00:00.00000','2025-03-18 14:30:00.00000',22,232,NULL,1),(179877,'2025-03-18 20:00:00.00000','2025-03-18 20:30:00.00000',22,232,NULL,1),(179878,'2025-03-18 21:00:00.00000','2025-03-18 21:30:00.00000',22,232,NULL,1),(179879,'2025-03-18 22:00:00.00000','2025-03-18 22:30:00.00000',22,232,NULL,1),(179880,'2025-03-19 08:00:00.00000','2025-03-19 08:30:00.00000',22,232,NULL,1),(179881,'2025-03-19 09:00:00.00000','2025-03-19 09:30:00.00000',22,232,NULL,1),(179882,'2025-03-19 10:00:00.00000','2025-03-19 10:30:00.00000',22,232,NULL,1),(179883,'2025-03-19 11:00:00.00000','2025-03-19 11:30:00.00000',22,232,NULL,1),(179884,'2025-03-19 12:00:00.00000','2025-03-19 12:30:00.00000',22,232,NULL,1),(179885,'2025-03-19 13:00:00.00000','2025-03-19 13:30:00.00000',22,232,NULL,1),(179886,'2025-03-19 14:00:00.00000','2025-03-19 14:30:00.00000',22,232,NULL,1),(179887,'2025-03-19 15:00:00.00000','2025-03-19 15:30:00.00000',22,232,NULL,1),(179888,'2025-03-24 09:00:00.00000','2025-03-24 09:30:00.00000',22,232,NULL,1),(179889,'2025-03-24 10:00:00.00000','2025-03-24 10:30:00.00000',22,232,NULL,1),(179890,'2025-03-24 11:00:00.00000','2025-03-24 11:30:00.00000',22,232,NULL,1),(179891,'2025-03-24 12:00:00.00000','2025-03-24 12:30:00.00000',22,232,NULL,1),(179892,'2025-03-24 13:00:00.00000','2025-03-24 13:30:00.00000',22,232,NULL,1),(179893,'2025-03-24 14:00:00.00000','2025-03-24 14:30:00.00000',22,232,NULL,1),(179894,'2025-03-24 15:00:00.00000','2025-03-24 15:30:00.00000',22,232,NULL,1),(179895,'2025-03-24 16:00:00.00000','2025-03-24 16:30:00.00000',22,232,NULL,1),(179896,'2025-03-25 11:00:00.00000','2025-03-25 11:30:00.00000',22,232,NULL,1),(179897,'2025-03-25 12:00:00.00000','2025-03-25 12:30:00.00000',22,232,NULL,1),(179898,'2025-03-25 13:00:00.00000','2025-03-25 13:30:00.00000',22,232,NULL,1),(179899,'2025-03-25 14:00:00.00000','2025-03-25 14:30:00.00000',22,232,NULL,1),(179900,'2025-03-25 20:00:00.00000','2025-03-25 20:30:00.00000',22,232,NULL,1),(179901,'2025-03-25 21:00:00.00000','2025-03-25 21:30:00.00000',22,232,NULL,1),(179902,'2025-03-25 22:00:00.00000','2025-03-25 22:30:00.00000',22,232,NULL,1),(179903,'2025-03-26 08:00:00.00000','2025-03-26 08:30:00.00000',22,232,NULL,1),(179904,'2025-03-26 09:00:00.00000','2025-03-26 09:30:00.00000',22,232,NULL,1),(179905,'2025-03-26 10:00:00.00000','2025-03-26 10:30:00.00000',22,232,NULL,1),(179906,'2025-03-26 11:00:00.00000','2025-03-26 11:30:00.00000',22,232,NULL,1),(179907,'2025-03-26 12:00:00.00000','2025-03-26 12:30:00.00000',22,232,NULL,1),(179908,'2025-03-26 13:00:00.00000','2025-03-26 13:30:00.00000',22,232,NULL,1),(179909,'2025-03-26 14:00:00.00000','2025-03-26 14:30:00.00000',22,232,NULL,1),(179910,'2025-03-26 15:00:00.00000','2025-03-26 15:30:00.00000',22,232,NULL,1),(179911,'2025-03-31 09:00:00.00000','2025-03-31 09:30:00.00000',22,232,NULL,1),(179912,'2025-03-31 10:00:00.00000','2025-03-31 10:30:00.00000',22,232,NULL,1),(179913,'2025-03-31 11:00:00.00000','2025-03-31 11:30:00.00000',22,232,NULL,1),(179914,'2025-03-31 12:00:00.00000','2025-03-31 12:30:00.00000',22,232,NULL,1),(179915,'2025-03-31 13:00:00.00000','2025-03-31 13:30:00.00000',22,232,NULL,1),(179916,'2025-03-31 14:00:00.00000','2025-03-31 14:30:00.00000',22,232,NULL,1),(179917,'2025-03-31 15:00:00.00000','2025-03-31 15:30:00.00000',22,232,NULL,1),(179918,'2025-03-31 16:00:00.00000','2025-03-31 16:30:00.00000',22,232,NULL,1),(179919,'2025-04-01 11:00:00.00000','2025-04-01 11:30:00.00000',22,232,NULL,1),(179920,'2025-04-01 12:00:00.00000','2025-04-01 12:30:00.00000',22,232,NULL,1),(179921,'2025-04-01 13:00:00.00000','2025-04-01 13:30:00.00000',22,232,NULL,1),(179922,'2025-04-01 14:00:00.00000','2025-04-01 14:30:00.00000',22,232,NULL,1),(179923,'2025-04-01 20:00:00.00000','2025-04-01 20:30:00.00000',22,232,NULL,1),(179924,'2025-04-01 21:00:00.00000','2025-04-01 21:30:00.00000',22,232,NULL,1),(179925,'2025-04-01 22:00:00.00000','2025-04-01 22:30:00.00000',22,232,NULL,1),(179926,'2025-04-02 08:00:00.00000','2025-04-02 08:30:00.00000',22,232,NULL,1),(179927,'2025-04-02 09:00:00.00000','2025-04-02 09:30:00.00000',22,232,NULL,1),(179928,'2025-04-02 10:00:00.00000','2025-04-02 10:30:00.00000',22,232,NULL,1),(179929,'2025-04-02 11:00:00.00000','2025-04-02 11:30:00.00000',22,232,NULL,1),(179930,'2025-04-02 12:00:00.00000','2025-04-02 12:30:00.00000',22,232,NULL,1),(179931,'2025-04-02 13:00:00.00000','2025-04-02 13:30:00.00000',22,232,NULL,1),(179932,'2025-04-02 14:00:00.00000','2025-04-02 14:30:00.00000',22,232,NULL,1),(179933,'2025-04-02 15:00:00.00000','2025-04-02 15:30:00.00000',22,232,NULL,1),(179934,'2025-04-07 09:00:00.00000','2025-04-07 09:30:00.00000',22,232,NULL,1),(179935,'2025-04-07 10:00:00.00000','2025-04-07 10:30:00.00000',22,232,NULL,1),(179936,'2025-04-07 11:00:00.00000','2025-04-07 11:30:00.00000',22,232,NULL,1),(179937,'2025-04-07 12:00:00.00000','2025-04-07 12:30:00.00000',22,232,NULL,1),(179938,'2025-04-07 13:00:00.00000','2025-04-07 13:30:00.00000',22,232,NULL,1),(179939,'2025-04-07 14:00:00.00000','2025-04-07 14:30:00.00000',22,232,NULL,1),(179940,'2025-04-07 15:00:00.00000','2025-04-07 15:30:00.00000',22,232,NULL,1),(179941,'2025-04-07 16:00:00.00000','2025-04-07 16:30:00.00000',22,232,NULL,1),(179942,'2025-04-08 11:00:00.00000','2025-04-08 11:30:00.00000',22,232,NULL,1),(179943,'2025-04-08 12:00:00.00000','2025-04-08 12:30:00.00000',22,232,NULL,1),(179944,'2025-04-08 13:00:00.00000','2025-04-08 13:30:00.00000',22,232,NULL,1),(179945,'2025-04-08 14:00:00.00000','2025-04-08 14:30:00.00000',22,232,NULL,1),(179946,'2025-04-08 20:00:00.00000','2025-04-08 20:30:00.00000',22,232,NULL,1),(179947,'2025-04-08 21:00:00.00000','2025-04-08 21:30:00.00000',22,232,NULL,1),(179948,'2025-04-08 22:00:00.00000','2025-04-08 22:30:00.00000',22,232,NULL,1),(179949,'2025-04-09 08:00:00.00000','2025-04-09 08:30:00.00000',22,232,NULL,1),(179950,'2025-04-09 09:00:00.00000','2025-04-09 09:30:00.00000',22,232,NULL,1),(179951,'2025-04-09 10:00:00.00000','2025-04-09 10:30:00.00000',22,232,NULL,1),(179952,'2025-04-09 11:00:00.00000','2025-04-09 11:30:00.00000',22,232,NULL,1),(179953,'2025-04-09 12:00:00.00000','2025-04-09 12:30:00.00000',22,232,NULL,1),(179954,'2025-04-09 13:00:00.00000','2025-04-09 13:30:00.00000',22,232,NULL,1),(179955,'2025-04-09 14:00:00.00000','2025-04-09 14:30:00.00000',22,232,NULL,1),(179956,'2025-04-09 15:00:00.00000','2025-04-09 15:30:00.00000',22,232,NULL,1),(179957,'2025-04-14 09:00:00.00000','2025-04-14 09:30:00.00000',22,232,NULL,1),(179958,'2025-04-14 10:00:00.00000','2025-04-14 10:30:00.00000',22,232,NULL,1),(179959,'2025-04-14 11:00:00.00000','2025-04-14 11:30:00.00000',22,232,NULL,1),(179960,'2025-04-14 12:00:00.00000','2025-04-14 12:30:00.00000',22,232,NULL,1),(179961,'2025-04-14 13:00:00.00000','2025-04-14 13:30:00.00000',22,232,NULL,1),(179962,'2025-04-14 14:00:00.00000','2025-04-14 14:30:00.00000',22,232,NULL,1),(179963,'2025-04-14 15:00:00.00000','2025-04-14 15:30:00.00000',22,232,NULL,1),(179964,'2025-04-14 16:00:00.00000','2025-04-14 16:30:00.00000',22,232,NULL,1),(179965,'2025-04-15 11:00:00.00000','2025-04-15 11:30:00.00000',22,232,NULL,1),(179966,'2025-04-15 12:00:00.00000','2025-04-15 12:30:00.00000',22,232,NULL,1),(179967,'2025-04-15 13:00:00.00000','2025-04-15 13:30:00.00000',22,232,NULL,1),(179968,'2025-04-15 14:00:00.00000','2025-04-15 14:30:00.00000',22,232,NULL,1),(179969,'2025-04-15 20:00:00.00000','2025-04-15 20:30:00.00000',22,232,NULL,1),(179970,'2025-04-15 21:00:00.00000','2025-04-15 21:30:00.00000',22,232,NULL,1),(179971,'2025-04-15 22:00:00.00000','2025-04-15 22:30:00.00000',22,232,NULL,1),(179972,'2025-04-16 08:00:00.00000','2025-04-16 08:30:00.00000',22,232,NULL,1),(179973,'2025-04-16 09:00:00.00000','2025-04-16 09:30:00.00000',22,232,NULL,1),(179974,'2025-04-16 10:00:00.00000','2025-04-16 10:30:00.00000',22,232,NULL,1),(179975,'2025-04-16 11:00:00.00000','2025-04-16 11:30:00.00000',22,232,NULL,1),(179976,'2025-04-16 12:00:00.00000','2025-04-16 12:30:00.00000',22,232,NULL,1),(179977,'2025-04-16 13:00:00.00000','2025-04-16 13:30:00.00000',22,232,NULL,1),(179978,'2025-04-16 14:00:00.00000','2025-04-16 14:30:00.00000',22,232,NULL,1),(179979,'2025-04-16 15:00:00.00000','2025-04-16 15:30:00.00000',22,232,NULL,1),(179980,'2025-04-21 09:00:00.00000','2025-04-21 09:30:00.00000',22,232,NULL,1),(179981,'2025-04-21 10:00:00.00000','2025-04-21 10:30:00.00000',22,232,NULL,1),(179982,'2025-04-21 11:00:00.00000','2025-04-21 11:30:00.00000',22,232,NULL,1),(179983,'2025-04-21 12:00:00.00000','2025-04-21 12:30:00.00000',22,232,NULL,1),(179984,'2025-04-21 13:00:00.00000','2025-04-21 13:30:00.00000',22,232,NULL,1),(179985,'2025-04-21 14:00:00.00000','2025-04-21 14:30:00.00000',22,232,NULL,1),(179986,'2025-04-21 15:00:00.00000','2025-04-21 15:30:00.00000',22,232,NULL,1),(179987,'2025-04-21 16:00:00.00000','2025-04-21 16:30:00.00000',22,232,NULL,1),(179988,'2025-04-22 11:00:00.00000','2025-04-22 11:30:00.00000',22,232,NULL,1),(179989,'2025-04-22 12:00:00.00000','2025-04-22 12:30:00.00000',22,232,NULL,1),(179990,'2025-04-22 13:00:00.00000','2025-04-22 13:30:00.00000',22,232,NULL,1),(179991,'2025-04-22 14:00:00.00000','2025-04-22 14:30:00.00000',22,232,NULL,1),(179992,'2025-04-22 20:00:00.00000','2025-04-22 20:30:00.00000',22,232,NULL,1),(179993,'2025-04-22 21:00:00.00000','2025-04-22 21:30:00.00000',22,232,NULL,1),(179994,'2025-04-22 22:00:00.00000','2025-04-22 22:30:00.00000',22,232,NULL,1),(179995,'2025-04-23 08:00:00.00000','2025-04-23 08:30:00.00000',22,232,NULL,1),(179996,'2025-04-23 09:00:00.00000','2025-04-23 09:30:00.00000',22,232,NULL,1),(179997,'2025-04-23 10:00:00.00000','2025-04-23 10:30:00.00000',22,232,NULL,1),(179998,'2025-04-23 11:00:00.00000','2025-04-23 11:30:00.00000',22,232,NULL,1),(179999,'2025-04-23 12:00:00.00000','2025-04-23 12:30:00.00000',22,232,NULL,1),(180000,'2025-04-23 13:00:00.00000','2025-04-23 13:30:00.00000',22,232,NULL,1),(180001,'2025-04-23 14:00:00.00000','2025-04-23 14:30:00.00000',22,232,NULL,1),(180002,'2025-04-23 15:00:00.00000','2025-04-23 15:30:00.00000',22,232,NULL,1),(180003,'2025-04-28 09:00:00.00000','2025-04-28 09:30:00.00000',22,232,NULL,1),(180004,'2025-04-28 10:00:00.00000','2025-04-28 10:30:00.00000',22,232,NULL,1),(180005,'2025-04-28 11:00:00.00000','2025-04-28 11:30:00.00000',22,232,NULL,1),(180006,'2025-04-28 12:00:00.00000','2025-04-28 12:30:00.00000',22,232,NULL,1),(180007,'2025-04-28 13:00:00.00000','2025-04-28 13:30:00.00000',22,232,NULL,1),(180008,'2025-04-28 14:00:00.00000','2025-04-28 14:30:00.00000',22,232,NULL,1),(180009,'2025-04-28 15:00:00.00000','2025-04-28 15:30:00.00000',22,232,NULL,1),(180010,'2025-04-28 16:00:00.00000','2025-04-28 16:30:00.00000',22,232,NULL,1),(180011,'2025-04-29 11:00:00.00000','2025-04-29 11:30:00.00000',22,232,NULL,1),(180012,'2025-04-29 12:00:00.00000','2025-04-29 12:30:00.00000',22,232,NULL,1),(180013,'2025-04-29 13:00:00.00000','2025-04-29 13:30:00.00000',22,232,NULL,1),(180014,'2025-04-29 14:00:00.00000','2025-04-29 14:30:00.00000',22,232,NULL,1),(180015,'2025-04-29 20:00:00.00000','2025-04-29 20:30:00.00000',22,232,NULL,1),(180016,'2025-04-29 21:00:00.00000','2025-04-29 21:30:00.00000',22,232,NULL,1),(180017,'2025-04-29 22:00:00.00000','2025-04-29 22:30:00.00000',22,232,NULL,1),(180018,'2025-04-30 08:00:00.00000','2025-04-30 08:30:00.00000',22,232,NULL,1),(180019,'2025-04-30 09:00:00.00000','2025-04-30 09:30:00.00000',22,232,NULL,1),(180020,'2025-04-30 10:00:00.00000','2025-04-30 10:30:00.00000',22,232,NULL,1),(180021,'2025-04-30 11:00:00.00000','2025-04-30 11:30:00.00000',22,232,NULL,1),(180022,'2025-04-30 12:00:00.00000','2025-04-30 12:30:00.00000',22,232,NULL,1),(180023,'2025-04-30 13:00:00.00000','2025-04-30 13:30:00.00000',22,232,NULL,1),(180024,'2025-04-30 14:00:00.00000','2025-04-30 14:30:00.00000',22,232,NULL,1),(180025,'2025-04-30 15:00:00.00000','2025-04-30 15:30:00.00000',22,232,NULL,1),(180026,'2025-05-05 09:00:00.00000','2025-05-05 09:30:00.00000',22,232,NULL,1),(180027,'2025-05-05 10:00:00.00000','2025-05-05 10:30:00.00000',22,232,NULL,1),(180028,'2025-05-05 11:00:00.00000','2025-05-05 11:30:00.00000',22,232,NULL,1),(180029,'2025-05-05 12:00:00.00000','2025-05-05 12:30:00.00000',22,232,NULL,1),(180030,'2025-05-05 13:00:00.00000','2025-05-05 13:30:00.00000',22,232,NULL,1),(180031,'2025-05-05 14:00:00.00000','2025-05-05 14:30:00.00000',22,232,NULL,1),(180032,'2025-05-05 15:00:00.00000','2025-05-05 15:30:00.00000',22,232,NULL,1),(180033,'2025-05-05 16:00:00.00000','2025-05-05 16:30:00.00000',22,232,NULL,1),(180034,'2025-05-06 11:00:00.00000','2025-05-06 11:30:00.00000',22,232,NULL,1),(180035,'2025-05-06 12:00:00.00000','2025-05-06 12:30:00.00000',22,232,NULL,1),(180036,'2025-05-06 13:00:00.00000','2025-05-06 13:30:00.00000',22,232,NULL,1),(180037,'2025-05-06 14:00:00.00000','2025-05-06 14:30:00.00000',22,232,NULL,1),(180038,'2025-05-06 20:00:00.00000','2025-05-06 20:30:00.00000',22,232,NULL,1),(180039,'2025-05-06 21:00:00.00000','2025-05-06 21:30:00.00000',22,232,NULL,1),(180040,'2025-05-06 22:00:00.00000','2025-05-06 22:30:00.00000',22,232,NULL,1),(180041,'2025-05-07 08:00:00.00000','2025-05-07 08:30:00.00000',22,232,NULL,0),(180042,'2025-05-07 09:00:00.00000','2025-05-07 09:30:00.00000',22,232,NULL,1),(180043,'2025-05-07 10:00:00.00000','2025-05-07 10:30:00.00000',22,232,NULL,1),(180044,'2025-05-07 11:00:00.00000','2025-05-07 11:30:00.00000',22,232,NULL,1),(180045,'2025-05-07 12:00:00.00000','2025-05-07 12:30:00.00000',22,232,NULL,1),(180046,'2025-05-07 13:00:00.00000','2025-05-07 13:30:00.00000',22,232,NULL,1),(180047,'2025-05-07 14:00:00.00000','2025-05-07 14:30:00.00000',22,232,NULL,1),(180048,'2025-05-07 15:00:00.00000','2025-05-07 15:30:00.00000',22,232,NULL,1);
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

-- Dump completed on 2024-05-11 15:55:33
