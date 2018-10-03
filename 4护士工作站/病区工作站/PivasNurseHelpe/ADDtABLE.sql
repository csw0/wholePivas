USE [Pivas20140723]
GO

/****** Object:  Table [dbo].[IVRecordUpdateWait]    Script Date: 07/29/2014 15:30:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IVRecordUpdateWait](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[WardEmployeeID] [bigint] NOT NULL,
	[WardInsertDT] [datetime] NULL,
	[WardLabelNo] [varchar](50) NULL,
	[WardAct] [int] NULL,
	[WardRemark1] [varchar](256) NULL,
	[WardRemark2] [varchar](256) NULL,
	[WardRemark3] [varchar](256) NULL,
	[WardRemark4] [varchar](256) NULL,
	[CenterAct] [int] NULL,
	[CenterEmployeeID] [bigint] NULL,
	[CenterInsertDT] [datetime] NULL,
	[CenterRemark1] [varchar](256) NULL,
	[CenterRemark2] [varchar](256) NULL,
	[CenterRemark3] [varchar](256) NULL,
	[CenterRemark4] [varchar](256) NULL,
 CONSTRAINT [PK_IVRecordUpdateWait] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IVRecordUpdateWait]  WITH CHECK ADD  CONSTRAINT [FK_IVRecordUpdateWait_DEmployee] FOREIGN KEY([WardEmployeeID])
REFERENCES [dbo].[DEmployee] ([DEmployeeID])
GO

ALTER TABLE [dbo].[IVRecordUpdateWait] CHECK CONSTRAINT [FK_IVRecordUpdateWait_DEmployee]
GO

ALTER TABLE [dbo].[IVRecordUpdateWait]  WITH CHECK ADD  CONSTRAINT [FK_IVRecordUpdateWait_DEmployee1] FOREIGN KEY([CenterEmployeeID])
REFERENCES [dbo].[DEmployee] ([DEmployeeID])
GO

ALTER TABLE [dbo].[IVRecordUpdateWait] CHECK CONSTRAINT [FK_IVRecordUpdateWait_DEmployee1]
GO

