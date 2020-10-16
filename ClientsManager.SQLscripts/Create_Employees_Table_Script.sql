USE [ClientsManager]
GO

/****** Object:  Table [dbo].[Employees]    Script Date: 10/15/2020 3:30:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[EmployeeType_Id] [int] NOT NULL,
	[Position] [nvarchar](max) NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeType_Id] FOREIGN KEY([EmployeeType_Id])
REFERENCES [dbo].[EmployeeTypes] ([Id])
GO

ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeType_Id]
GO

