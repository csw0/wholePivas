USE [Pivas20140213]
GO

/****** Object:  Table [dbo].[IVRecordHistory]    Script Date: 02/13/2014 15:31:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IVRecordHistory](
	[IVRecordID] [bigint] NOT NULL,
	[PrescriptionID] [bigint] NULL,
	[DrugLGroupNo] [varchar](64) NULL,
	[GroupNo] [varchar](64) NULL,
	[Batch] [varchar](32) NULL,
	[BatchRule] [varchar](1024) NULL,
	[LabelNo] [varchar](32) NULL,
	[InfusionDT] [datetime] NULL,
	[BatchSaved] [bit] NULL,
	[IVStatus] [tinyint] NULL,
	[FreqCode] [varchar](32) NULL,
	[FreqName] [varchar](50) NULL,
	[BatchSavedDT] [datetime] NULL,
	[InsertDT] [datetime] NULL,
	[PrintDT] [datetime] NULL,
	[PrinterID] [varchar](16) NULL,
	[PrinterName] [varchar](16) NULL,
	[PatCode] [varchar](64) NULL,
	[PatName] [varchar](64) NULL,
	[BedNo] [varchar](16) NULL,
	[WardCode] [varchar](32) NULL,
	[WardName] [varchar](64) NULL,
	[Sex] [char](10) NULL,
	[Age] [char](10) NULL,
	[IsSame] [bit] NULL,
	[JustOne] [bit] NULL,
	[IsCommand] [bit] NULL,
	[TeamNumber] [int] NULL,
	[IsPack] [bit] NULL,
	[IsBatch] [bit] NULL,
	[PackName] [varchar](50) NULL,
	[Remark1] [varchar](256) NULL,
	[Remark2] [varchar](256) NULL,
	[Remark3] [varchar](256) NULL,
	[Remark4] [varchar](256) NULL,
	[Remark5] [varchar](256) NULL,
	[Remark6] [varchar](256) NULL,
	[FreqNum] [int] NULL,
	[LabelOver] [int] NULL,
	[WardRetreat] [int] NULL,
	[WardRID] [varchar](16) NULL,
	[WardRtime] [datetime] NULL,
	[PackID] [varchar](16) NULL,
	[PackTime] [datetime] NULL,
	[MarjorDrug] [varchar](32) NULL,
	[Menstruum] [varchar](32) NULL,
	[LabelOverID] [varchar](16) NULL,
	[LabelOverTime] [datetime] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'瓶签是否与昨天一致' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IVRecordHistory', @level2type=N'COLUMN',@level2name=N'IsSame'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否不冲配' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IVRecordHistory', @level2type=N'COLUMN',@level2name=N'IsPack'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统是否排过批次' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IVRecordHistory', @level2type=N'COLUMN',@level2name=N'IsBatch'
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_BatchSaved]  DEFAULT ((0)) FOR [BatchSaved]
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_IsSame]  DEFAULT ((1)) FOR [IsSame]
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_IsBatch]  DEFAULT ((0)) FOR [IsBatch]
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_FreqNum]  DEFAULT ((1)) FOR [FreqNum]
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_LabelOver]  DEFAULT ((0)) FOR [LabelOver]
GO

ALTER TABLE [dbo].[IVRecordHistory] ADD  CONSTRAINT [DF_IVRecordHistory_WardRetreat]  DEFAULT ((0)) FOR [WardRetreat]
GO


