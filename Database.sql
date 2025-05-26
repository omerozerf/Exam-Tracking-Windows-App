-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: lgstracking
-- ------------------------------------------------------
-- Server version	8.0.41

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
-- Table structure for table `admins`
--

DROP TABLE IF EXISTS `admins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `admins` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(100) NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `admins`
--

LOCK TABLES `admins` WRITE;
/*!40000 ALTER TABLE `admins` DISABLE KEYS */;
INSERT INTO `admins` VALUES (1,'admin','admin');
/*!40000 ALTER TABLE `admins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exams`
--

DROP TABLE IF EXISTS `exams`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exams` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `StudentID` int NOT NULL,
  `Date` date NOT NULL,
  `Math` float DEFAULT NULL,
  `Science` float DEFAULT NULL,
  `Turkish` float DEFAULT NULL,
  `History` float DEFAULT NULL,
  `Religion` float DEFAULT NULL,
  `English` float DEFAULT NULL,
  `Title` varchar(100) DEFAULT NULL,
  `MathCorrect` int DEFAULT NULL,
  `MathWrong` int DEFAULT NULL,
  `ScienceCorrect` int DEFAULT NULL,
  `ScienceWrong` int DEFAULT NULL,
  `TurkishCorrect` int DEFAULT NULL,
  `TurkishWrong` int DEFAULT NULL,
  `HistoryCorrect` int DEFAULT NULL,
  `HistoryWrong` int DEFAULT NULL,
  `ReligionCorrect` int DEFAULT NULL,
  `ReligionWrong` int DEFAULT NULL,
  `EnglishCorrect` int DEFAULT NULL,
  `EnglishWrong` int DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `StudentID` (`StudentID`),
  CONSTRAINT `exams_ibfk_1` FOREIGN KEY (`StudentID`) REFERENCES `students` (`ID`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=30 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exams`
--

LOCK TABLES `exams` WRITE;
/*!40000 ALTER TABLE `exams` DISABLE KEYS */;
INSERT INTO `exams` VALUES (23,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'Hız ve Renk',1,0,2,0,3,0,4,0,5,0,6,0),(24,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'X Sınavı',5,0,7,0,13,0,10,0,7,3,9,1),(25,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'Y Sınavı',20,0,20,0,20,0,10,0,10,0,5,0),(26,9,'2024-10-01',NULL,NULL,NULL,NULL,NULL,NULL,'KD STARTER LGS-1(KAFADENGİ STARTER LGS-1) Kitapçık B',17,3,16,4,17,3,9,1,8,2,10,0),(27,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'X Sinavi',5,0,7,0,13,0,10,0,7,3,9,1),(28,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'Hiz ve Renk',1,0,2,0,3,0,4,0,5,0,6,0),(29,9,'2025-05-24',NULL,NULL,NULL,NULL,NULL,NULL,'Hiz ve Renk',1,0,2,0,3,0,4,0,5,0,6,0);
/*!40000 ALTER TABLE `exams` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `students` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Surname` varchar(100) NOT NULL,
  `School` varchar(100) DEFAULT NULL,
  `Class` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Username` (`Username`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES (9,'student1','student1','Ömer Faruk','Özer','Maltepe','8-D');
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-05-24 21:02:23
