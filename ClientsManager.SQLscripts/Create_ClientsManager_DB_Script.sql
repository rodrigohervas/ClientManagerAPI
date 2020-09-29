USE [master]
GO

/****** Object:  Database [ClientsManager]    Script Date: 9/28/2020 9:22:41 PM ******/
CREATE DATABASE [ClientsManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClientsManager', FILENAME = N'[YOUR-ROUTE-HERE]\ClientsManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClientsManager_log', FILENAME = N'[YOUR-ROUTE-HERE]\ClientsManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClientsManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ClientsManager] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ClientsManager] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ClientsManager] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ClientsManager] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ClientsManager] SET ARITHABORT OFF 
GO

ALTER DATABASE [ClientsManager] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [ClientsManager] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ClientsManager] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ClientsManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ClientsManager] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ClientsManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ClientsManager] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ClientsManager] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ClientsManager] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ClientsManager] SET  ENABLE_BROKER 
GO

ALTER DATABASE [ClientsManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ClientsManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ClientsManager] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ClientsManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ClientsManager] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ClientsManager] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [ClientsManager] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ClientsManager] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ClientsManager] SET  MULTI_USER 
GO

ALTER DATABASE [ClientsManager] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ClientsManager] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ClientsManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ClientsManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ClientsManager] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ClientsManager] SET QUERY_STORE = OFF
GO

USE [ClientsManager]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [ClientsManager] SET  READ_WRITE 
GO
