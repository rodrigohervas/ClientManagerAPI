USE [ClientsManager]
GO

/****** Object:  Table [dbo].[BillableActivities]    Script Date: 9/28/2020 9:18:19 PM ******/
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


