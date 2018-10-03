USE [Pivas20140213]
GO

/****** Object:  Table [dbo].[IVRecordDetailHistory]    Script Date: 02/13/2014 15:30:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[IVRecordDetailHistory](
	[IVRecodedDetaiID] [bigint] NOT NULL,
	[IVRecordID] [bigint] NOT NULL,
	[InceptDT] [datetime] NULL,
	[DrugCode] [varchar](64) NULL,
	[DrugName] [varchar](128) NULL,
	[Spec] [varchar](128) NULL,
	[Dosage] [decimal](12, 4) NULL,
	[DosageUnit] [varchar](8) NULL,
	[DgNo] [int] NULL,
	[ReturnFromHis] [int] NULL,
	[Remark7] [varchar](128) NULL,
	[Remark8] [varchar](128) NULL,
	[Remark9] [varchar](128) NULL,
	[Remark10] [varchar](128) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[IVRecordDetailHistory] ADD  CONSTRAINT [DF_IVRecordDetailHistay_DgNo]  DEFAULT ((0)) FOR [DgNo]
GO

ALTER TABLE [dbo].[IVRecordDetailHistory] ADD  CONSTRAINT [DF_IVRecordDetailHistay_ReturnFromHis]  DEFAULT ((-1)) FOR [ReturnFromHis]
GO


