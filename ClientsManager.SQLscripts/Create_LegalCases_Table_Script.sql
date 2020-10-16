USE [ClientsManager]
GO

/****** Object:  Table [dbo].[LegalCases]    Script Date: 10/15/2020 3:30:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LegalCases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Client_Id] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[TrustFund] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_LegalCases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[LegalCases] ADD  DEFAULT ((0.0)) FOR [TrustFund]
GO

ALTER TABLE [dbo].[LegalCases]  WITH CHECK ADD  CONSTRAINT [FK_LegalCases_Clients_Client_Id] FOREIGN KEY([Client_Id])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[LegalCases] CHECK CONSTRAINT [FK_LegalCases_Clients_Client_Id]
GO

