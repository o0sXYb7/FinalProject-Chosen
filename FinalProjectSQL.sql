USE [FinalProject]
GO
/****** Object:  Table [dbo].[BreakingNews]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BreakingNews](
	[BreakingNewsID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[ActivityUrl] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_BreakingNews] PRIMARY KEY CLUSTERED 
(
	[BreakingNewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carousel]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carousel](
	[CarouselID] [int] IDENTITY(1,1) NOT NULL,
	[PictureName] [nvarchar](50) NULL,
	[Url] [nvarchar](50) NULL,
 CONSTRAINT [PK_輪播] PRIMARY KEY CLUSTERED 
(
	[CarouselID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commodity]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commodity](
	[CommodityID] [int] IDENTITY(1,1) NOT NULL,
	[TempStorageID] [int] NOT NULL,
	[MemberID] [int] NULL,
	[CommodityName] [nvarchar](50) NOT NULL,
	[CommodityQuantity] [int] NOT NULL,
	[CommodityUnitPrice] [int] NOT NULL,
	[Deadline] [datetime] NULL,
	[OnShelves] [bit] NULL,
 CONSTRAINT [PK_Shelves] PRIMARY KEY CLUSTERED 
(
	[CommodityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customerservice]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customerservice](
	[CustomerserviceID] [int] IDENTITY(1,1) NOT NULL,
	[Class] [nvarchar](50) NULL,
	[QuestionTitle] [nvarchar](50) NULL,
	[AnswerTitle] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customerservice] PRIMARY KEY CLUSTERED 
(
	[CustomerserviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raward_items]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raward_items](
	[Raward_id] [int] NOT NULL,
	[Raward_item_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Raward_level] [nvarchar](50) NOT NULL,
	[IsJackpot] [bit] NOT NULL,
	[Num] [int] NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Raward_items] PRIMARY KEY CLUSTERED 
(
	[Raward_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raward_lib]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Raward_lib](
	[Raward_id] [int] IDENTITY(1,1) NOT NULL,
	[Series] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Raward_lib] PRIMARY KEY CLUSTERED 
(
	[Raward_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShowRaward]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShowRaward](
	[ShowRaward_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Series] [nvarchar](50) NOT NULL,
	[Firm] [nvarchar](50) NOT NULL,
	[AddProbability] [int] NOT NULL,
	[NowProbability] [int] NOT NULL,
	[AllowStoreDay] [int] NOT NULL,
	[Fare] [int] NOT NULL,
	[OneDrawMoney] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Introduction] [nvarchar](100) NULL,
 CONSTRAINT [PK_ShowRaward] PRIMARY KEY CLUSTERED 
(
	[ShowRaward_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShowRaward_items]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShowRaward_items](
	[ShowRaward_item_id] [int] IDENTITY(1,1) NOT NULL,
	[ShowRaward_id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Raward_level] [nvarchar](50) NOT NULL,
	[IsJackpot] [bit] NOT NULL,
	[Num] [int] NOT NULL,
	[LaveNum] [int] NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ShowRaward_items] PRIMARY KEY CLUSTERED 
(
	[ShowRaward_item_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempStorage]    Script Date: 2023/5/12 下午 10:47:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempStorage](
	[TempStorageID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[PrizePool_ID] [int] NOT NULL,
	[Prize_ID] [nchar](10) NOT NULL,
	[PrizeName] [nvarchar](50) NOT NULL,
	[PrizeQuantity] [int] NOT NULL,
	[PrizePicture] [nvarchar](50) NOT NULL,
	[AwardDate] [datetime] NOT NULL,
	[Deadline] [datetime] NOT NULL,
	[Receive] [bit] NOT NULL,
	[Created] [bit] NOT NULL,
 CONSTRAINT [PK_TempStorage] PRIMARY KEY CLUSTERED 
(
	[TempStorageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BreakingNews] ON 

INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (1, N'本週新品推薦《海賊王》+《JOJO》', N'https://static2.oneone.com.tw/upload/img_up/31640/518/e87f8b86973907f14b3725785a3bc6ba_L.png', CAST(N'2022-11-09T18:34:41.000' AS DateTime), N'本週新品推薦《海賊王》難以攻陷的心腹+《JOJO的奇妙冒險》石之海STAND’S ASSEMBLE 《海賊王》難以攻陷的心腹 4月14日(六)凌晨00:00與日本同步開...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (2, N'【新玩法】超級福袋! 公仔加量! 加倍爽!', N'https://static2.oneone.com.tw/upload/img_up/31640/389/9ae163a90601de56c1c55af5b724b3d2_L.png', CAST(N'2022-11-02T16:38:10.000' AS DateTime), N'斜角巷《超級福袋》：第一彈 https://www.oneone.com.tw/product/7323 地方爸爸《超級福袋》爽撈熊 https://www.oneone.com.t...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (3, N'《七龍珠》基紐特戰隊!!來襲即刻開抽!', N'https://static2.oneone.com.tw/upload/img_up/31640/32a/8b502607dba461a960fbc951a32da252_L.png', CAST(N'2022-11-18T14:17:46.000' AS DateTime), N'【日本金證】《七龍珠》基紐特戰隊!!00:00來襲 https://www.oneone.com.tw/category/37 集結弗利沙麾下、由宇宙精銳戰是中挑選出來的超級特種部隊「...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (4, N'春日限定！消費滿額再送公仔', N'https://static2.oneone.com.tw/upload/img_up/3163f/90b/ef5a3c3c7614d2cd461530af1f761de6_L.png', CAST(N'2022-10-13T19:38:51.000' AS DateTime), N'❤4月春節回饋~限時開跑~消費滿額再送公仔 ❤好康一、連續假期-低價不斷 ★活動時間：即日起~4月底 ★活動內容： 活動期間內，春日限定 低價優惠!! 立即前往[...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (5, N'秒數大進化，抽賞有保障', N'https://static2.oneone.com.tw/upload/img_up/31640/581/063caee76648d32a370310d8e2500ec9_L.png', CAST(N'2022-09-28T13:07:12.000' AS DateTime), N'秒數大進化，抽賞有保障 3/29(三) 12:00~15:00 oneone商城將進行線上優化作業，為讓用戶有更舒服的抽賞體驗，以下調整介紹: 進化一、商品剩餘30抽(含)以上，保...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (6, N'本月可愛推薦《布丁狗》(全開箱)', N'https://static2.oneone.com.tw/upload/img_up/3163f/a39/517c475e0c9a309282913a7b75604f36_L.png', CAST(N'2023-05-08T17:57:59.000' AS DateTime), N'本月可愛推薦三麗鷗賞《布丁狗》(全開箱)點擊連結購買：
https://www.oneone.com.tw/category/120
&nbsp;
..')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (7, N'BLUE LOCK 藍色監獄，唯一套搶抽', N'https://static2.oneone.com.tw/upload/img_up/3163f/8a4/313224d465aa8ac413d73e03ea1c1d62_L.png', CAST(N'2023-05-07T09:47:31.000' AS DateTime), N'本週新品：BLUE LOCK 藍色監獄
202/03/25 00:00 凌晨與日本同步開抽!!

A賞 潔世一 角色立牌 約19cm


B賞 蜂樂迴 角色立牌 約16cm

...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (8, N'
塔比樂 輕拼樂 5/2 & 5/3 中獎號碼 大公開', N'https://static2.oneone.com.tw/009d228f-a6b7-4dca-93c6-233ca77c08df.png', CAST(N'2023-05-04T16:49:44.000' AS DateTime), N'「輕拼樂」首次開獎，[最後]才來[對] ★新系列商品「輕拼樂」以“低價拚大賞+最後機會人人有”為特色 開獎啦!!!   《塔比樂》輕拼樂： 美音...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (9, N'
四月感謝大回饋!! 花小錢，抽大獎！', N'https://static2.oneone.com.tw/98497f46-991c-4803-b02a-1edf79f1a7ec.png', CAST(N'2023-03-27T12:43:57.000' AS DateTime), N'好康一、四月感謝大回饋 ★活動時間：即日起~5/2 ★活動內容： 多套一番賞 降價回饋! 活動期間內，春日特價 特別優惠!! 立即前往 [ 春日特價 ]...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (10, N'尼爾:自動人形【最終套】即將完抽', N'https://static2.oneone.com.tw/d3c03a90-43c3-481e-a8e7-f803ede45633.png', CAST(N'2023-04-21T16:15:06.000' AS DateTime), N'尼爾:自動人形 Ver1.1a3/21 00:00限量開抽 https://www.oneone.com.tw/category/131 最後賞 2B模型公仔 Goggles OF...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description]) VALUES (11, N'LINEPAY服務暫停，請改用其他金流', N'https://static2.oneone.com.tw/d1801893-235c-44b8-b550-271eda59b1a8.png', CAST(N'2023-04-09T05:23:00.000' AS DateTime), N'
LINEPAY服務暫停，請改用其他金流
2023-03-16 17:22:00

即日起，暫停LINEPay服務付款方式，請用戶改用信用卡、APPLEPAY、超商代碼付款。 造成用戶們的不便還請見諒。 oneone商城營運團隊 敬上...')
SET IDENTITY_INSERT [dbo].[BreakingNews] OFF
GO
SET IDENTITY_INSERT [dbo].[Carousel] ON 

INSERT [dbo].[Carousel] ([CarouselID], [PictureName], [Url]) VALUES (1, N'貓', N'https://picsum.photos/1296/150?random=1')
INSERT [dbo].[Carousel] ([CarouselID], [PictureName], [Url]) VALUES (2, N'狗', N'https://picsum.photos/1296/150?random=2')
INSERT [dbo].[Carousel] ([CarouselID], [PictureName], [Url]) VALUES (3, N'貓2', N'https://picsum.photos/1296/150?random=3')
INSERT [dbo].[Carousel] ([CarouselID], [PictureName], [Url]) VALUES (4, N'狗2', N'https://picsum.photos/1296/150?random=4')
SET IDENTITY_INSERT [dbo].[Carousel] OFF
GO
SET IDENTITY_INSERT [dbo].[Commodity] ON 

INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (1, 1, 0, N'火影忍者01', 0, 99, CAST(N'2023-04-20T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (2, 2, 2, N'航海王01', 0, 10, CAST(N'2023-05-06T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (8, 3, 3, N'死神第2彈', 0, 320, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Commodity] OFF
GO
SET IDENTITY_INSERT [dbo].[Customerservice] ON 

INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (1, N'Account', N'如何成為會員?', N'需註冊成為會員才可以購買商品')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (2, N'Account', N'個人資料保密?', N'我們將善盡保管職責，會員資訊僅作為記錄與寄送紀錄。
')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (3, N'Account', N'註冊成會員有甚麼好處?', N'每次消費可以獲得紅利，用於重抽、續購扭蛋或折抵訂單金額。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (4, N'Consumption', N'如何購買?', N'商品加入購物車後完成訂單付款。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (5, N'Consumption', N'有哪些付款方式?', N'可使用代幣付款、LINE Pay、信用卡、WebATM、ATM轉帳、超商代碼繳費、超商貨到付款。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (6, N'Freight', N'運費如何計算?', N'運費計算請參考下列各家收件方式。2.免運不包含促銷、直購商品與一番賞。3.
其餘免運活動以官網&粉絲專頁發佈為準。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (7, N'Commodity', N'商品可退換貨標準?', N'玩具商品皆為工廠大量製造，盒損、溢色、掉漆、分模線…等皆非歸屬於瑕疵之列，無提供退換貨服務。
若因斷裂、缺件或商品有誤，請於到貨後七日內聯繫客服，逾期恕不予以處理。
不接受超取或親取逾期遭退回或未收到簡訊為由而退貨，需負起遲延責任，將向買家請求損害賠償，需賠償運費或其他損失。
退貨商品必須整筆訂單為全新狀態且完整包裝並附發票，除非雙方有共識可以針對某特定商品作處理。
退貨商品如已拆封、缺件、活動贈品或發票不齊全等情形，恕不可辦理退貨。（包含環保扭蛋封膜未拆、盒玩未拆、贈品...等）。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (8, N'Commodity', N'有商品疑慮嗎?', N'選者的東西皆為日貨全新正版。')
SET IDENTITY_INSERT [dbo].[Customerservice] OFF
GO
SET IDENTITY_INSERT [dbo].[Raward_items] ON 

INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 2, N'孫悟空模型', N'A賞', 1, 1, N'589')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 5, N'達爾模型', N'B賞', 1, 1, N'2')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 6, N'悟飯模型', N'C賞', 1, 1, N'3')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 8, N'小公仔(6款隨機)', N'D賞', 0, 10, N'4')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 9, N'吊飾(10款隨機)', N'E賞', 0, 25, N'5')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 10, N'毛巾(6款隨機)', N'F賞', 0, 25, N'6')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1, 11, N'角色壓克力牌', N'G賞', 0, 17, N'45')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (2, 1002, N'迷豆子模型', N'A賞', 1, 1, N'7')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1002, 1003, N'A賞模型', N'A賞', 1, 1, N'9')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (3002, 2002, N'利姆露', N'A賞', 1, 2, N'10')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (1002, 3002, N'B賞模型', N'B賞', 1, 1, N'11')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (5014, 4007, N'賽馬', N'A賞', 1, 1, N'78')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (2, 8002, N'炭志郎', N'B賞', 1, 2, N'8898')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (3, 8003, N'蜜莉姆', N'A賞', 1, 2, N'555')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (6030, 10003, N'遊戲', N'A賞', 1, 1, N'852')
SET IDENTITY_INSERT [dbo].[Raward_items] OFF
GO
SET IDENTITY_INSERT [dbo].[Raward_lib] ON 

INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (1, N'七龍珠', N'第1彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (2, N'鬼滅之刃', N'第2彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (3, N'關於我轉生成史萊姆這檔事', N'學園第1彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (1002, N'賽馬娘', N'第1彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (2002, N'海賊王', N'第1彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (3002, N'關於我轉生成史萊姆這檔事', N'學園第2彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (4002, N'賽馬娘', N'第2彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (5012, N'賽馬娘', N'第3彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (5014, N'賽馬娘', N'第5彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (6030, N'遊戲王', N'第1彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (6031, N'遊戲王', N'第2彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8035, N'七龍珠', N'第8彈')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8036, N'12', N'12')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8037, N'12', N'12')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8038, N'12', N'12')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8039, N'12', N'12')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8040, N'12', N'12')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (8041, N'12', N'12')
SET IDENTITY_INSERT [dbo].[Raward_lib] OFF
GO
SET IDENTITY_INSERT [dbo].[ShowRaward] ON 

INSERT [dbo].[ShowRaward] ([ShowRaward_id], [Name], [Series], [Firm], [AddProbability], [NowProbability], [AllowStoreDay], [Fare], [OneDrawMoney], [CreateTime], [Introduction]) VALUES (1, N'【多摩多摩】自製賞', N'自製賞', N'多摩多摩', 0, 1, 14, 120, 350, CAST(N'2023-04-24T23:08:40.687' AS DateTime), N'狂抽猛送，抽到爽爽爽')
SET IDENTITY_INSERT [dbo].[ShowRaward] OFF
GO
SET IDENTITY_INSERT [dbo].[ShowRaward_items] ON 

INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (1, 1, N'索龍GK公仔', N'SP賞', 1, 1, 1, N'5656')
SET IDENTITY_INSERT [dbo].[ShowRaward_items] OFF
GO
SET IDENTITY_INSERT [dbo].[TempStorage] ON 

INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (1, 2, 3, N'0         ', N'火影忍者', 29, N'NARUTO ', CAST(N'2023-04-15T00:00:00.000' AS DateTime), CAST(N'2023-04-21T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (2, 2, 3, N'1         ', N'航海王', 20, N'ONE PIECE', CAST(N'2023-04-28T20:26:00.000' AS DateTime), CAST(N'2023-05-06T20:26:00.000' AS DateTime), 1, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (3, 3, 4, N'2         ', N'死神', 19, N'BLEACH', CAST(N'2023-05-01T10:11:00.000' AS DateTime), CAST(N'2023-06-02T00:00:00.000' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[TempStorage] OFF
GO
ALTER TABLE [dbo].[Raward_items]  WITH CHECK ADD  CONSTRAINT [FK_Raward_items_Raward_items] FOREIGN KEY([Raward_id])
REFERENCES [dbo].[Raward_lib] ([Raward_id])
GO
ALTER TABLE [dbo].[Raward_items] CHECK CONSTRAINT [FK_Raward_items_Raward_items]
GO
ALTER TABLE [dbo].[ShowRaward_items]  WITH CHECK ADD  CONSTRAINT [FK_ShowRaward_items_ShowRaward] FOREIGN KEY([ShowRaward_id])
REFERENCES [dbo].[ShowRaward] ([ShowRaward_id])
GO
ALTER TABLE [dbo].[ShowRaward_items] CHECK CONSTRAINT [FK_ShowRaward_items_ShowRaward]
GO
