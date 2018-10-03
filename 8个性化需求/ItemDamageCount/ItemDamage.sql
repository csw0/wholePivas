USE [Pivas20150906苏]
GO

/****** Object:  Table [dbo].[ItemDamage]    Script Date: 07/26/2016 10:49:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ItemDamage](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Drugcode] [varchar](50) NOT NULL,
	[Drugname] [varchar](50) NOT NULL,
	[Spec] [varchar](50) NULL,
	[Count] [int] NOT NULL,
	[Money] [varchar](50) NULL,
	[Reason] [varchar](50) NULL,
	[Responsibilityid] [varchar](50) NULL,
	[Responsibilityer] [varchar](50) NULL,
	[Reportid] [varchar](50) NULL,
	[Reporter] [varchar](50) NULL,
	[Damagetime] [datetime] NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

