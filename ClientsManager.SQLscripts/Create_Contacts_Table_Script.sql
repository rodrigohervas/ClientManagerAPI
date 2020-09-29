USE [ClientsManager]
GO

/****** Object:  Table [dbo].[Contacts]    Script Date: 9/28/2020 9:20:38 PM ******/
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

