USE [FinalProject]
GO
/****** Object:  Table [dbo].[BreakingNews]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
	[IndexDescription] [ntext] NULL,
 CONSTRAINT [PK_BreakingNews] PRIMARY KEY CLUSTERED 
(
	[BreakingNewsID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carousel]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[Commodity]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[Customerservice]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[Image_Store]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image_Store](
	[Image_id] [int] IDENTITY(1,1) NOT NULL,
	[Series] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ImageName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Image_Store_1] PRIMARY KEY CLUSTERED 
(
	[Image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MemberInfo]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberInfo](
	[MemberId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[MemberName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[encryptedResetCode] [nvarchar](200) NULL,
	[newPassword] [nvarchar](50) NULL,
	[RegisterDate] [date] NOT NULL,
	[ResetDateTime] [datetime] NULL,
	[Address] [nvarchar](200) NULL,
	[Birthday] [date] NULL,
	[Point] [int] NOT NULL,
 CONSTRAINT [PK_MemberInfo] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[MerchantOrderNo] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Amt] [nvarchar](50) NOT NULL,
	[TradeNo] [nvarchar](50) NOT NULL,
	[IP] [nvarchar](50) NULL,
	[ItemDesc] [nvarchar](200) NULL,
	[PaymentType] [nvarchar](50) NOT NULL,
	[ExpireDate] [nvarchar](50) NULL,
	[BankCode] [nvarchar](50) NULL,
	[CodeNo] [nvarchar](50) NULL,
	[PayTime] [nvarchar](50) NULL,
	[Card6No] [nvarchar](50) NULL,
	[Card4No] [nvarchar](50) NULL,
	[Exp] [nvarchar](50) NULL,
	[MemberId] [int] NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[MerchantOrderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionHistory]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionHistory](
	[QuestionHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionReportID] [int] NULL,
	[Message] [nvarchar](max) NULL,
	[WhoAnswer] [int] NULL,
	[DateTimeSecond] [datetime] NULL,
 CONSTRAINT [PK_QuestionHistory] PRIMARY KEY CLUSTERED 
(
	[QuestionHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionReport]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionReport](
	[QuestionReportID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[MemberID] [int] NOT NULL,
	[QuestionTitle] [nvarchar](50) NULL,
	[QuestionType] [nvarchar](50) NULL,
	[DateTime] [datetime] NULL,
	[State] [nvarchar](50) NULL,
 CONSTRAINT [PK_ProblemReport] PRIMARY KEY CLUSTERED 
(
	[QuestionReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Raward_items]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[Raward_lib]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[ShowRaward]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
	[IsOpen] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Introduction] [nvarchar](100) NULL,
	[Image] [nvarchar](50) NOT NULL,
	[HasSelectNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_ShowRaward] PRIMARY KEY CLUSTERED 
(
	[ShowRaward_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShowRaward_items]    Script Date: 2023/5/23 上午 10:05:48 ******/
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
/****** Object:  Table [dbo].[TempStorage]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempStorage](
	[TempStorageID] [int] IDENTITY(1,1) NOT NULL,
	[MemberID] [int] NOT NULL,
	[PrizePool_ID] [int] NOT NULL,
	[Prize_ID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[wishlist]    Script Date: 2023/5/23 上午 10:05:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wishlist](
	[itemID] [int] IDENTITY(1,1) NOT NULL,
	[PraductID] [int] NOT NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK_wishlist] PRIMARY KEY CLUSTERED 
(
	[itemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BreakingNews] ON 

INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (1, N'本週新品推薦《海賊王》+《JOJO》', N'https://static2.oneone.com.tw/upload/img_up/31640/518/e87f8b86973907f14b3725785a3bc6ba_L.png', CAST(N'2022-11-09T18:34:41.000' AS DateTime), N'我是海賊王Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'本週新品推薦《海賊王》難以攻陷的心腹+《JOJO的奇妙冒險》石之海STAND’S ASSEMBLE 《海賊王》難以攻陷的心腹 4月14日(六)凌晨00:00與日本同步開...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (2, N'【新玩法】超級福袋! 公仔加量! 加倍爽!', N'https://static2.oneone.com.tw/upload/img_up/31640/389/9ae163a90601de56c1c55af5b724b3d2_L.png', CAST(N'2022-11-02T16:38:10.000' AS DateTime), N'我是超級福袋Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'斜角巷《超級福袋》：第一彈 https://www.oneone.com.tw/product/7323 地方爸爸《超級福袋》爽撈熊 https://www.oneone.com.t...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (3, N'《七龍珠》基紐特戰隊!!來襲即刻開抽!', N'https://static2.oneone.com.tw/upload/img_up/31640/32a/8b502607dba461a960fbc951a32da252_L.png', CAST(N'2022-11-18T14:17:46.000' AS DateTime), N'我是七龍珠Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'【日本金證】《七龍珠》基紐特戰隊!!00:00來襲 https://www.oneone.com.tw/category/37 集結弗利沙麾下、由宇宙精銳戰是中挑選出來的超級特種部隊「...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (4, N'春日限定！消費滿額再送公仔', N'https://static2.oneone.com.tw/upload/img_up/3163f/90b/ef5a3c3c7614d2cd461530af1f761de6_L.png', CAST(N'2022-10-13T19:38:51.000' AS DateTime), N'我是公仔Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'❤4月春節回饋~限時開跑~消費滿額再送公仔 ❤好康一、連續假期-低價不斷 ★活動時間：即日起~4月底 ★活動內容： 活動期間內，春日限定 低價優惠!! 立即前往[...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (5, N'秒數大進化，抽賞有保障', N'https://static2.oneone.com.tw/upload/img_up/31640/581/063caee76648d32a370310d8e2500ec9_L.png', CAST(N'2022-09-28T13:07:12.000' AS DateTime), N'進化了Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'秒數大進化，抽賞有保障 3/29(三) 12:00~15:00 oneone商城將進行線上優化作業，為讓用戶有更舒服的抽賞體驗，以下調整介紹: 進化一、商品剩餘30抽(含)以上，保...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (6, N'本月可愛推薦《布丁狗》(全開箱)', N'https://static2.oneone.com.tw/upload/img_up/3163f/a39/517c475e0c9a309282913a7b75604f36_L.png', CAST(N'2023-05-08T17:57:59.000' AS DateTime), N'我是布丁狗Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'本月可愛推薦三麗鷗賞《布丁狗》(全開箱)點擊連結購買：
https://www.oneone.com.tw/category/120
&nbsp;
..')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (7, N'BLUE LOCK 藍色監獄，唯一套搶抽', N'https://static2.oneone.com.tw/upload/img_up/3163f/8a4/313224d465aa8ac413d73e03ea1c1d62_L.png', CAST(N'2023-05-07T09:47:31.000' AS DateTime), N'我是藍色監獄Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'本週新品：BLUE LOCK 藍色監獄
202/03/25 00:00 凌晨與日本同步開抽!!

A賞 潔世一 角色立牌 約19cm


B賞 蜂樂迴 角色立牌 約16cm

...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (8, N'
塔比樂 輕拼樂 5/2 & 5/3 中獎號碼 大公開', N'https://static2.oneone.com.tw/009d228f-a6b7-4dca-93c6-233ca77c08df.png', CAST(N'2023-05-04T16:49:44.000' AS DateTime), N'中獎號碼Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'「輕拼樂」首次開獎，[最後]才來[對] ★新系列商品「輕拼樂」以“低價拚大賞+最後機會人人有”為特色 開獎啦!!!   《塔比樂》輕拼樂： 美音...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (9, N'
四月感謝大回饋!! 花小錢，抽大獎！', N'https://static2.oneone.com.tw/98497f46-991c-4803-b02a-1edf79f1a7ec.png', CAST(N'2023-03-27T12:43:57.000' AS DateTime), N'大回饋Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'好康一、四月感謝大回饋 ★活動時間：即日起~5/2 ★活動內容： 多套一番賞 降價回饋! 活動期間內，春日特價 特別優惠!! 立即前往 [ 春日特價 ]...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (10, N'尼爾:自動人形【最終套】即將完抽', N'https://static2.oneone.com.tw/d3c03a90-43c3-481e-a8e7-f803ede45633.png', CAST(N'2023-04-21T16:15:06.000' AS DateTime), N'我是尼爾Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'尼爾:自動人形 Ver1.1a3/21 00:00限量開抽 https://www.oneone.com.tw/category/131 最後賞 2B模型公仔 Goggles OF...')
INSERT [dbo].[BreakingNews] ([BreakingNewsID], [Title], [ActivityUrl], [Date], [Description], [IndexDescription]) VALUES (11, N'LINEPAY服務暫停，請改用其他金流', N'https://static2.oneone.com.tw/d1801893-235c-44b8-b550-271eda59b1a8.png', CAST(N'2023-04-09T05:23:00.000' AS DateTime), N'暫停服務Lorem ipsum dolor sit amet consectetur, adipisicing elit. Itaque sint veritatis adipisci, ab pariatur nostrum ex cupiditate ratione nulla accusamus, illo expedita blanditiis praesentium esse sequi ipsum atque ea deleniti.', N'
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

INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (1, 1, 2, N'火影忍者01', 0, 99, CAST(N'2023-04-20T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (2, 2, 2, N'航海王01', 0, 10, CAST(N'2023-05-06T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (8, 3, 5, N'死神第2彈', 10, 320, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (9, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (10, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (11, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (12, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (13, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (14, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (15, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (16, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (17, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (18, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (19, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (20, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (21, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (22, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (23, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (24, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (25, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (26, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (27, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (28, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (29, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (30, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (31, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (32, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (33, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (34, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (35, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (36, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (37, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (38, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (39, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (40, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (41, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (42, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (43, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (44, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (45, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (46, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (47, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (48, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (49, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (50, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Commodity] ([CommodityID], [TempStorageID], [MemberID], [CommodityName], [CommodityQuantity], [CommodityUnitPrice], [Deadline], [OnShelves]) VALUES (51, 7, 1, N'測試', 3, 450, CAST(N'2023-06-02T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Commodity] OFF
GO
SET IDENTITY_INSERT [dbo].[Customerservice] ON 

INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (1, N'Account', N'如何成為會員?', N'需註冊成為會員才可以購買商品')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (2, N'Account', N'個人資料保密?', N'我們將善盡保管職責，會員資訊僅作為記錄與寄送紀錄。
')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (3, N'Account', N'註冊成會員有甚麼好處?', N'每次消費可以獲得紅利，用於重抽、續購扭蛋或折抵訂單金額。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (4, N'Consumption', N'如何購買?', N'商品加入購物車後完成訂單付款。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (5, N'Consumption', N'有哪些付款方式?', N'可使用代幣付款、LINE Pay、信用卡、WebATM、ATM轉帳、超商代碼繳費、超商貨到付款。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (6, N'Freight', N'運費如何計算?', N'運費計算請參考下列各家收件方式。2.免運不包含促銷、直購商品與一番賞。
3.
其餘免運活動以官網&粉絲專頁發佈為準。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (7, N'Commodity', N'商品可退換貨標準?', N'玩具商品皆為工廠大量製造，盒損、溢色、掉漆、分模線…等皆非歸屬於瑕疵之列，無提供退換貨服務。
若因斷裂、缺件或商品有誤，請於到貨後七日內聯繫客服，逾期恕不予以處理。
不接受超取或親取逾期遭退回或未收到簡訊為由而退貨，需負起遲延責任，將向買家請求損害賠償，需賠償運費或其他損失。
退貨商品必須整筆訂單為全新狀態且完整包裝並附發票，除非雙方有共識可以針對某特定商品作處理。
退貨商品如已拆封、缺件、活動贈品或發票不齊全等情形，恕不可辦理退貨。（包含環保扭蛋封膜未拆、盒玩未拆、贈品...等）。')
INSERT [dbo].[Customerservice] ([CustomerserviceID], [Class], [QuestionTitle], [AnswerTitle]) VALUES (8, N'Commodity', N'有商品疑慮嗎?', N'選者的東西皆為日貨全新正版。')
SET IDENTITY_INSERT [dbo].[Customerservice] OFF
GO
SET IDENTITY_INSERT [dbo].[Image_Store] ON 

INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (2, N'間諜家家酒', N'MISSION START!', N'20230516211450安妮亞萬年曆公仔.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (3, N'間諜家家酒', N'MISSION START!', N'20230516211518安妮亞娃娃.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (4, N'間諜家家酒', N'MISSION START!', N'20230516211554造型浴巾.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (5, N'間諜家家酒', N'MISSION START!', N'20230516211626安妮亞造型浴巾.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (6, N'間諜家家酒', N'MISSION START!', N'20230516211644家庭插畫版.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (7, N'間諜家家酒', N'MISSION START!', N'20230516211657家庭插畫版2.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (8, N'間諜家家酒', N'MISSION START!', N'20230516211715立牌.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (9, N'間諜家家酒', N'MISSION START!', N'20230516211733吊飾.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (10, N'間諜家家酒', N'MISSION START!', N'20230516211745盤子.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (11, N'間諜家家酒', N'MISSION START!', N'20230517092142間諜家家酒banner.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (12, N'海賊王', N'難以攻陷的心腹', N'20230517094008海賊王banner.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (13, N'海賊王', N'難以攻陷的心腹', N'20230517102100索隆公仔.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (14, N'海賊王', N'難以攻陷的心腹', N'20230517102125貝克曼公仔.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (15, N'海賊王', N'難以攻陷的心腹', N'20230517102148馬爾科公仔.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (16, N'海賊王', N'難以攻陷的心腹', N'20230517102204夏洛特·卡塔克利.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (17, N'海賊王', N'難以攻陷的心腹', N'20230517102217KING模型公仔.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (18, N'海賊王', N'難以攻陷的心腹', N'20230517102234120cm大毛巾.png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (19, N'海賊王', N'難以攻陷的心腹', N'20230517102248造型毛巾(全8種隨機).png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (20, N'海賊王', N'難以攻陷的心腹', N'20230517102312A4資料夾貼紙與口罩夾套組(全8種隨機).png')
INSERT [dbo].[Image_Store] ([Image_id], [Series], [Name], [ImageName]) VALUES (21, N'海賊王', N'難以攻陷的心腹', N'20230517102329圓形小盤(全9種隨機).png')
SET IDENTITY_INSERT [dbo].[Image_Store] OFF
GO
SET IDENTITY_INSERT [dbo].[MemberInfo] ON 

INSERT [dbo].[MemberInfo] ([MemberId], [Email], [MemberName], [Phone], [Password], [encryptedResetCode], [newPassword], [RegisterDate], [ResetDateTime], [Address], [Birthday], [Point]) VALUES (1, N'j42036910@gmail.com', N'j42036910@gmail.com', N'0123456789', N'D291D91C1FC6A231F4F251E3C102DB8251DD7E4331940496DF52C13D535EADEA', NULL, NULL, CAST(N'2023-05-18' AS Date), NULL, NULL, NULL, 0)
INSERT [dbo].[MemberInfo] ([MemberId], [Email], [MemberName], [Phone], [Password], [encryptedResetCode], [newPassword], [RegisterDate], [ResetDateTime], [Address], [Birthday], [Point]) VALUES (2, N'test02@gmail.com', N'test02@gmail.com', N'0123456789', N'D291D91C1FC6A231F4F251E3C102DB8251DD7E4331940496DF52C13D535EADEA', NULL, NULL, CAST(N'2023-05-18' AS Date), NULL, NULL, NULL, 0)
INSERT [dbo].[MemberInfo] ([MemberId], [Email], [MemberName], [Phone], [Password], [encryptedResetCode], [newPassword], [RegisterDate], [ResetDateTime], [Address], [Birthday], [Point]) VALUES (3, N'test03@gmail.com', N'test03@gmail.com', N'0123456789', N'D291D91C1FC6A231F4F251E3C102DB8251DD7E4331940496DF52C13D535EADEA', NULL, NULL, CAST(N'2023-05-18' AS Date), NULL, NULL, NULL, 0)
INSERT [dbo].[MemberInfo] ([MemberId], [Email], [MemberName], [Phone], [Password], [encryptedResetCode], [newPassword], [RegisterDate], [ResetDateTime], [Address], [Birthday], [Point]) VALUES (4, N't44@gmail.com', N't44@gmail.com', N'0123456789', N'D291D91C1FC6A231F4F251E3C102DB8251DD7E4331940496DF52C13D535EADEA', NULL, NULL, CAST(N'2023-05-18' AS Date), NULL, NULL, NULL, 0)
INSERT [dbo].[MemberInfo] ([MemberId], [Email], [MemberName], [Phone], [Password], [encryptedResetCode], [newPassword], [RegisterDate], [ResetDateTime], [Address], [Birthday], [Point]) VALUES (5, N'uiojilk777@gmail.com', N'test', N'0912345678', N'B9E5DB57F1499C59B6B8DC0E3E440DFB61E6C4D67B3DD4E430878AEBEB16AA4F', NULL, NULL, CAST(N'2023-05-22' AS Date), NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[MemberInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionHistory] ON 

INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (1, 15, N'問題1', 1, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (2, 15, N'我回答了問題1', 2, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (3, 16, N'問題2', 1, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (4, 16, N'我回答了問題2', 2, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (5, 17, N'問題3', 1, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (6, 17, N'問題4', 1, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (7, 17, N'我回答了問題3和4', 2, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (8, 18, N'問題5', 1, NULL)
INSERT [dbo].[QuestionHistory] ([QuestionHistoryID], [QuestionReportID], [Message], [WhoAnswer], [DateTimeSecond]) VALUES (9, 18, N'我回答問題5', 2, NULL)
SET IDENTITY_INSERT [dbo].[QuestionHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionReport] ON 

INSERT [dbo].[QuestionReport] ([QuestionReportID], [MemberID], [QuestionTitle], [QuestionType], [DateTime], [State]) VALUES (15, 1, N'標題1', N'帳號問題', CAST(N'2023-05-16T04:03:41.220' AS DateTime), NULL)
INSERT [dbo].[QuestionReport] ([QuestionReportID], [MemberID], [QuestionTitle], [QuestionType], [DateTime], [State]) VALUES (16, 1, N'標題2', N'帳號問題', CAST(N'2023-05-16T05:56:01.290' AS DateTime), NULL)
INSERT [dbo].[QuestionReport] ([QuestionReportID], [MemberID], [QuestionTitle], [QuestionType], [DateTime], [State]) VALUES (17, 2, N'我問了問題3和問題4', N'運費問題', CAST(N'2023-05-16T06:54:19.677' AS DateTime), NULL)
INSERT [dbo].[QuestionReport] ([QuestionReportID], [MemberID], [QuestionTitle], [QuestionType], [DateTime], [State]) VALUES (18, 3, N'我問了問題5', N'其他問題', CAST(N'2023-05-16T06:54:25.020' AS DateTime), NULL)
INSERT [dbo].[QuestionReport] ([QuestionReportID], [MemberID], [QuestionTitle], [QuestionType], [DateTime], [State]) VALUES (33, 1, N'我是會員4', N'帳號問題', CAST(N'2023-05-19T16:06:14.240' AS DateTime), N'True')
SET IDENTITY_INSERT [dbo].[QuestionReport] OFF
GO
SET IDENTITY_INSERT [dbo].[Raward_items] ON 

INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11004, N'安妮亞‧佛傑 萬年曆', N'A賞', 1, 2, N'20230516211450安妮亞萬年曆公仔.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11005, N'安妮亞‧佛傑 絨毛玩偶', N'B賞', 1, 2, N'20230516211518安妮亞娃娃.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11006, N'SPY×FAMILY120cm大浴巾', N'C賞', 1, 2, N'20230516211554造型浴巾.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11007, N'安妮亞×同學款 120cm大浴巾', N'D賞', 1, 2, N'20230516211626安妮亞造型浴巾.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11008, N'新繪製插畫板-Misson Start!', N'E賞', 1, 3, N'20230516211644家庭插畫版.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11009, N'新繪製插畫板-安妮亞的廚房- A3size', N'F賞', 1, 3, N'20230516211657家庭插畫版2.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11010, N'角色壓克力立牌(全10種隨機) ', N'G賞', 0, 25, N'20230516211715立牌.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11011, N'橡膠吊飾(全11種隨機)', N'H賞', 0, 25, N'20230516211733吊飾.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9030, 11012, N'美耐皿盤子(全4種隨機)', N'I賞', 0, 16, N'20230516211745盤子.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11013, N'羅羅亞﹒索隆 模型公仔', N'A賞', 1, 2, N'20230517102100索隆公仔.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11014, N'班﹒貝克曼 模型公仔', N'B賞', 1, 1, N'20230517102125貝克曼公仔.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11015, N'馬爾科 模型公仔', N'C賞', 1, 1, N'20230517102148馬爾科公仔.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11016, N'夏洛特·卡塔克利 模型公仔', N'D賞', 1, 2, N'20230517102204夏洛特·卡塔克利.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11017, N'KING 模型公仔', N'E賞', 1, 1, N'20230517102217KING模型公仔.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11018, N'120cm 大毛巾', N'F賞', 1, 1, N'20230517102234120cm大毛巾.png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11019, N'造型毛巾(全8種隨機)', N'G賞', 0, 24, N'20230517102248造型毛巾(全8種隨機).png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11020, N'A4資料夾貼紙與口罩夾套組(全8種隨機)', N'H賞', 0, 24, N'20230517102312A4資料夾貼紙與口罩夾套組(全8種隨機).png')
INSERT [dbo].[Raward_items] ([Raward_id], [Raward_item_id], [Name], [Raward_level], [IsJackpot], [Num], [Image]) VALUES (9031, 11021, N'圓形小盤(全9種隨機)', N'I賞', 0, 24, N'20230517102329圓形小盤(全9種隨機).png')
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
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (9030, N'間諜家家酒', N'MISSION START!')
INSERT [dbo].[Raward_lib] ([Raward_id], [Series], [Name]) VALUES (9031, N'海賊王', N'難以攻陷的心腹')
SET IDENTITY_INSERT [dbo].[Raward_lib] OFF
GO
SET IDENTITY_INSERT [dbo].[ShowRaward] ON 

INSERT [dbo].[ShowRaward] ([ShowRaward_id], [Name], [Series], [Firm], [AddProbability], [NowProbability], [AllowStoreDay], [Fare], [OneDrawMoney], [IsOpen], [CreateTime], [Introduction], [Image], [HasSelectNumber]) VALUES (1003, N'MISSION START!', N'間諜家家酒', N'公司自營', 0, 0, 14, 100, 350, 1, CAST(N'2023-05-17T10:01:31.427' AS DateTime), N'間諜家家酒最新系列', N'20230517092142間諜家家酒banner.png', N'1,8,15,')
SET IDENTITY_INSERT [dbo].[ShowRaward] OFF
GO
SET IDENTITY_INSERT [dbo].[ShowRaward_items] ON 

INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3010, 1003, N'安妮亞‧佛傑 萬年曆', N'A賞', 1, 2, 2, N'20230516211450安妮亞萬年曆公仔.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3011, 1003, N'安妮亞‧佛傑 絨毛玩偶', N'B賞', 1, 2, 2, N'20230516211518安妮亞娃娃.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3012, 1003, N'SPY×FAMILY120cm大浴巾', N'C賞', 1, 2, 2, N'20230516211554造型浴巾.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3013, 1003, N'安妮亞×同學款 120cm大浴巾', N'D賞', 1, 2, 2, N'20230516211626安妮亞造型浴巾.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3014, 1003, N'新繪製插畫板-Misson Start!', N'E賞', 1, 3, 3, N'20230516211644家庭插畫版.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3015, 1003, N'新繪製插畫板-安妮亞的廚房- A3size', N'F賞', 1, 3, 3, N'20230516211657家庭插畫版2.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3016, 1003, N'角色壓克力立牌(全10種隨機) ', N'G賞', 0, 25, 24, N'20230516211715立牌.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3017, 1003, N'橡膠吊飾(全11種隨機)', N'H賞', 0, 25, 24, N'20230516211733吊飾.png')
INSERT [dbo].[ShowRaward_items] ([ShowRaward_item_id], [ShowRaward_id], [Name], [Raward_level], [IsJackpot], [Num], [LaveNum], [Image]) VALUES (3018, 1003, N'美耐皿盤子(全4種隨機)', N'I賞', 0, 16, 15, N'20230516211745盤子.png')
SET IDENTITY_INSERT [dbo].[ShowRaward_items] OFF
GO
SET IDENTITY_INSERT [dbo].[TempStorage] ON 

INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (1, 1, 1003, 3010, N'火影忍者', 29, N'NARUTO ', CAST(N'2023-04-15T00:00:00.000' AS DateTime), CAST(N'2023-04-21T00:00:00.000' AS DateTime), 1, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (2, 1, 1003, 3010, N'航海王', 20, N'ONE PIECE', CAST(N'2023-04-28T20:26:00.000' AS DateTime), CAST(N'2023-05-06T20:26:00.000' AS DateTime), 1, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (3, 5, 1003, 3010, N'死神', 19, N'20230516211450安妮亞萬年曆公仔', CAST(N'2023-05-01T10:11:00.000' AS DateTime), CAST(N'2023-06-02T00:00:00.000' AS DateTime), 0, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (4, 3, 1003, 3010, N'死神', 19, N'20230516211450安妮亞萬年曆公仔', CAST(N'2023-05-01T10:11:00.000' AS DateTime), CAST(N'2023-06-02T00:00:00.000' AS DateTime), 0, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (5, 3, 1003, 3010, N'死神', 19, N'20230516211450安妮亞萬年曆公仔', CAST(N'2023-05-01T10:11:00.000' AS DateTime), CAST(N'2023-06-02T00:00:00.000' AS DateTime), 0, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (6, 3, 1003, 3010, N'死神', 19, N'20230516211450安妮亞萬年曆公仔', CAST(N'2023-05-01T10:11:00.000' AS DateTime), CAST(N'2023-06-02T00:00:00.000' AS DateTime), 0, 1)
INSERT [dbo].[TempStorage] ([TempStorageID], [MemberID], [PrizePool_ID], [Prize_ID], [PrizeName], [PrizeQuantity], [PrizePicture], [AwardDate], [Deadline], [Receive], [Created]) VALUES (7, 1, 1003, 3011, N'測試', 8, N'20230516211518安妮亞娃娃', CAST(N'2023-05-27T00:23:00.000' AS DateTime), CAST(N'2023-06-02T00:23:00.000' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[TempStorage] OFF
GO
ALTER TABLE [dbo].[MemberInfo] ADD  CONSTRAINT [DEFAULT_MemberInfo_ResetDateTime]  DEFAULT (NULL) FOR [ResetDateTime]
GO
ALTER TABLE [dbo].[MemberInfo] ADD  CONSTRAINT [DEFAULT_MemberInfo_Point]  DEFAULT ((0)) FOR [Point]
GO
ALTER TABLE [dbo].[Commodity]  WITH CHECK ADD  CONSTRAINT [FK_Commodity_MemberInfo] FOREIGN KEY([MemberID])
REFERENCES [dbo].[MemberInfo] ([MemberId])
GO
ALTER TABLE [dbo].[Commodity] CHECK CONSTRAINT [FK_Commodity_MemberInfo]
GO
ALTER TABLE [dbo].[Commodity]  WITH CHECK ADD  CONSTRAINT [FK_Commodity_TempStorage] FOREIGN KEY([TempStorageID])
REFERENCES [dbo].[TempStorage] ([TempStorageID])
GO
ALTER TABLE [dbo].[Commodity] CHECK CONSTRAINT [FK_Commodity_TempStorage]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_MemberId] FOREIGN KEY([MemberId])
REFERENCES [dbo].[MemberInfo] ([MemberId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_MemberId]
GO
ALTER TABLE [dbo].[QuestionHistory]  WITH CHECK ADD  CONSTRAINT [FK_QuestionHistory_QuestionReport] FOREIGN KEY([QuestionReportID])
REFERENCES [dbo].[QuestionReport] ([QuestionReportID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionHistory] CHECK CONSTRAINT [FK_QuestionHistory_QuestionReport]
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
ALTER TABLE [dbo].[TempStorage]  WITH CHECK ADD  CONSTRAINT [FK_TempStorage_MemberInfo] FOREIGN KEY([MemberID])
REFERENCES [dbo].[MemberInfo] ([MemberId])
GO
ALTER TABLE [dbo].[TempStorage] CHECK CONSTRAINT [FK_TempStorage_MemberInfo]
GO
ALTER TABLE [dbo].[TempStorage]  WITH CHECK ADD  CONSTRAINT [FK_TempStorage_ShowRaward] FOREIGN KEY([PrizePool_ID])
REFERENCES [dbo].[ShowRaward] ([ShowRaward_id])
GO
ALTER TABLE [dbo].[TempStorage] CHECK CONSTRAINT [FK_TempStorage_ShowRaward]
GO
ALTER TABLE [dbo].[TempStorage]  WITH CHECK ADD  CONSTRAINT [FK_TempStorage_ShowRaward_items] FOREIGN KEY([Prize_ID])
REFERENCES [dbo].[ShowRaward_items] ([ShowRaward_item_id])
GO
ALTER TABLE [dbo].[TempStorage] CHECK CONSTRAINT [FK_TempStorage_ShowRaward_items]
GO
ALTER TABLE [dbo].[wishlist]  WITH CHECK ADD  CONSTRAINT [FK_wishlist_MemberInfo] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[MemberInfo] ([MemberId])
GO
ALTER TABLE [dbo].[wishlist] CHECK CONSTRAINT [FK_wishlist_MemberInfo]
GO
ALTER TABLE [dbo].[wishlist]  WITH CHECK ADD  CONSTRAINT [FK_wishlist_ShowRaward] FOREIGN KEY([PraductID])
REFERENCES [dbo].[ShowRaward] ([ShowRaward_id])
GO
ALTER TABLE [dbo].[wishlist] CHECK CONSTRAINT [FK_wishlist_ShowRaward]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'foreign key from registerinfo' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Payment', @level2type=N'COLUMN',@level2name=N'Status'
GO
