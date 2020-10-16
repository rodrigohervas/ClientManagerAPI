USE [ClientsManager]
GO

/****** Object:  Table [EventLogging].[Logs]    Script Date: 10/15/2020 3:31:15 PM ******/
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

