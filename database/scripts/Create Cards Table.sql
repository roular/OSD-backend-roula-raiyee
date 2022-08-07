SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cards](
	[CardID] [int] IDENTITY(1,1) NOT NULL,
	[CardTitle] [varchar](1000) NULL,
	[CardCategory] [varchar](50) NULL,
	[CardDuedate] [varchar](50) NULL,
	[CardEstimate] [varchar](50) NULL,
	[CardImportance] [varchar](50) NULL,
	[CardType] [varchar](50) NULL,
	[Personid] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cards] ADD PRIMARY KEY CLUSTERED 
(
	[CardID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cards]  WITH CHECK ADD FOREIGN KEY([Personid])
REFERENCES [dbo].[Persons] ([Personid])
GO