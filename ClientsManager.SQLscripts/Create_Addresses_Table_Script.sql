USE [ClientsManager]
GO

/****** Object:  Table [dbo].[Addresses]    Script Date: 10/15/2020 3:29:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[StreetNumber] [nvarchar](max) NOT NULL,
	[City] [nvarchar](350) NOT NULL,
	[StateProvince] [nvarchar](75) NULL,
	[Country] [nvarchar](75) NULL,
	[ZipCode] [nvarchar](15) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Clients_Client_Id]
GO

