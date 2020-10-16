USE [ClientsManager]
GO

/****** Object:  Table [dbo].[Contacts]    Script Date: 10/15/2020 3:30:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Address_Id] [int] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Position] [nvarchar](150) NULL,
	[Telephone] [nvarchar](15) NOT NULL,
	[Cellphone] [nvarchar](15) NULL,
	[Email] [nvarchar](320) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Contacts] ADD  DEFAULT ((0)) FOR [Address_Id]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Addresses_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Addresses] ([Id])
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Addresses_Address_Id]
GO

ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
GO

ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Clients_Client_Id]
GO

