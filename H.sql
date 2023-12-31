USE [FinalProject]
GO
/****** Object:  Table [dbo].[TradeHistory]    Script Date: 2023/5/23 19:39:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TradeHistory](
	[TradeHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[Seller] [int] NOT NULL,
	[Buyer] [int] NOT NULL,
	[CommodityId] [int] NOT NULL,
	[CommodityQuantity] [int] NOT NULL,
	[CommodityUnitPrice] [int] NOT NULL,
	[TradeTime] [datetime] NOT NULL,
 CONSTRAINT [PK_TradeHistory] PRIMARY KEY CLUSTERED 
(
	[TradeHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TradeHistory]  WITH CHECK ADD  CONSTRAINT [FK_TradeHistory_Commodity] FOREIGN KEY([CommodityId])
REFERENCES [dbo].[Commodity] ([CommodityID])
GO
ALTER TABLE [dbo].[TradeHistory] CHECK CONSTRAINT [FK_TradeHistory_Commodity]
GO
