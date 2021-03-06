USE [master]
GO
/****** Object:  Database [ClientsManagerTestDB]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE DATABASE [ClientsManagerTestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClientsManagerTestDB', FILENAME = N'C:\Users\rodrigohervas\ClientsManagerTestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClientsManagerTestDB_log', FILENAME = N'C:\Users\rodrigohervas\ClientsManagerTestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ClientsManagerTestDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClientsManagerTestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClientsManagerTestDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ClientsManagerTestDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClientsManagerTestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClientsManagerTestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClientsManagerTestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClientsManagerTestDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ClientsManagerTestDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClientsManagerTestDB] SET  MULTI_USER 
GO
ALTER DATABASE [ClientsManagerTestDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClientsManagerTestDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClientsManagerTestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClientsManagerTestDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClientsManagerTestDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClientsManagerTestDB] SET QUERY_STORE = OFF
GO
USE [ClientsManagerTestDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [ClientsManagerTestDB]
GO
/****** Object:  Schema [EventLogging]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE SCHEMA [EventLogging]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[StreetNumber] [nvarchar](max) NOT NULL,
	[City] [nvarchar](350) NOT NULL,
	[StateProvince] [nvarchar](75) NOT NULL,
	[Country] [nvarchar](75) NOT NULL,
	[ZipCode] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillableActivities]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillableActivities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LegalCase_Id] [int] NOT NULL,
	[Employee_Id] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Start_DateTime] [datetime2](7) NOT NULL,
	[Finish_DateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BillableActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Website] [nvarchar](350) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Address_Id] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Position] [nvarchar](150) NOT NULL,
	[Telephone] [nvarchar](15) NOT NULL,
	[Cellphone] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[EmployeeType_Id] [int] NOT NULL,
	[Position] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTypes]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LegalCases]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LegalCases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[TrustFund] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LegalCases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [EventLogging].[Logs]    Script Date: 10/23/2020 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [EventLogging].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Severity] [nvarchar](max) NULL,
	[Timestamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[LogData] [nvarchar](250) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Address_Client_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_Address_Client_Id] ON [dbo].[Addresses]
(
	[Client_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BillableActivities_Employee_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_BillableActivities_Employee_Id] ON [dbo].[BillableActivities]
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BillableActivities_LegalCase_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_BillableActivities_LegalCase_Id] ON [dbo].[BillableActivities]
(
	[LegalCase_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contacts_Address_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contacts_Address_Id] ON [dbo].[Contacts]
(
	[Address_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contacts_Client_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contacts_Client_Id] ON [dbo].[Contacts]
(
	[Client_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_EmployeeType_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_Employees_EmployeeType_Id] ON [dbo].[Employees]
(
	[EmployeeType_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_LegalCases_Client_Id]    Script Date: 10/23/2020 1:30:17 PM ******/
CREATE NONCLUSTERED INDEX [IX_LegalCases_Client_Id] ON [dbo].[LegalCases]
(
	[Client_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses] ADD  DEFAULT (N'') FOR [ZipCode]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Clients_Client_Id]
GO
ALTER TABLE [dbo].[BillableActivities]  WITH CHECK ADD  CONSTRAINT [FK_BillableActivities_Employees_Employee_Id] FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillableActivities] CHECK CONSTRAINT [FK_BillableActivities_Employees_Employee_Id]
GO
ALTER TABLE [dbo].[BillableActivities]  WITH CHECK ADD  CONSTRAINT [FK_BillableActivities_LegalCases_LegalCase_Id] FOREIGN KEY([LegalCase_Id])
REFERENCES [dbo].[LegalCases] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BillableActivities] CHECK CONSTRAINT [FK_BillableActivities_LegalCases_LegalCase_Id]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Addresses_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Addresses_Address_Id]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Clients_Client_Id]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeType_Id] FOREIGN KEY([EmployeeType_Id])
REFERENCES [dbo].[EmployeeTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeType_Id]
GO
ALTER TABLE [dbo].[LegalCases]  WITH CHECK ADD  CONSTRAINT [FK_LegalCases_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LegalCases] CHECK CONSTRAINT [FK_LegalCases_Clients_Client_Id]
GO
USE [master]
GO
ALTER DATABASE [ClientsManagerTestDB] SET  READ_WRITE 
GO
