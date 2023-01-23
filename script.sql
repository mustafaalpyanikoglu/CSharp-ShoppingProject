USE [master]
GO
/****** Object:  Database [ShoppingProjectDB]    Script Date: 23.01.2023 14:27:44 ******/
CREATE DATABASE [ShoppingProjectDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShoppingProjectDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ShoppingProjectDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShoppingProjectDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ShoppingProjectDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ShoppingProjectDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShoppingProjectDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShoppingProjectDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ShoppingProjectDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShoppingProjectDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShoppingProjectDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ShoppingProjectDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShoppingProjectDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [ShoppingProjectDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShoppingProjectDB] SET  MULTI_USER 
GO
ALTER DATABASE [ShoppingProjectDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShoppingProjectDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShoppingProjectDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShoppingProjectDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShoppingProjectDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShoppingProjectDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ShoppingProjectDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [ShoppingProjectDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ShoppingProjectDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationClaims]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_OperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[TotalPrice] [real] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserCartId] [int] NOT NULL,
	[OrderNumber] [nvarchar](50) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ApprovalDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [real] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purses]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Money] [real] NOT NULL,
 CONSTRAINT [PK_Purses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCarts]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCarts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserCarts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOperationClaims]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOperationClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OperationClaimId] [int] NOT NULL,
 CONSTRAINT [PK_UserOperationClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.01.2023 14:27:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[PasswordHash] [varbinary](500) NOT NULL,
	[PasswordSalt] [varbinary](500) NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230121123940_CreateDb', N'6.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230122164829_Update_Users_Table', N'6.0.13')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Teknoloji')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Giyim')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Eşya')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Bilgisayar Parçaları')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (5, N'Temizlik Malzemesi')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[OperationClaims] ON 

INSERT [dbo].[OperationClaims] ([Id], [Name], [Description]) VALUES (1, N'admin', N'Bütün işlemleri yapabilir.')
INSERT [dbo].[OperationClaims] ([Id], [Name], [Description]) VALUES (2, N'customer', N'Alışveriş yapan müşteri')
SET IDENTITY_INSERT [dbo].[OperationClaims] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [TotalPrice]) VALUES (8, 2, 13, 8, 40)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [TotalPrice]) VALUES (9, 2, 12, 8, 40)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [TotalPrice]) VALUES (10, 2, 16, 27, 108)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [TotalPrice]) VALUES (13, 2, 11, 15, 45)
INSERT [dbo].[OrderDetails] ([Id], [OrderId], [ProductId], [Quantity], [TotalPrice]) VALUES (16, 3, 20, 4, 28)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [UserCartId], [OrderNumber], [OrderDate], [ApprovalDate], [Status]) VALUES (2, 3, N'F24F54', CAST(N'2023-01-22T21:18:56.0000000' AS DateTime2), CAST(N'2023-01-22T21:20:23.0000000' AS DateTime2), 1)
INSERT [dbo].[Orders] ([Id], [UserCartId], [OrderNumber], [OrderDate], [ApprovalDate], [Status]) VALUES (3, 3, N'481F9A', CAST(N'2023-01-22T21:20:34.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (5, 1, N'Telefon', 100, 10)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (6, 1, N'Tablet', 100, 8)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (7, 1, N'Leptop', 100, 12)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (8, 2, N'Kazak', 100, 3)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (9, 2, N'Tisort', 100, 3)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (10, 2, N'Pantolon', 100, 3)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (11, 2, N'Sweet', 100, 3)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (12, 3, N'Tabak', 96, 5)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (13, 3, N'Masa', 92, 5)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (14, 3, N'Sandalye', 100, 4)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (15, 5, N'Deterjan', 100, 4)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (16, 5, N'Piril', 75, 4)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (17, 5, N'Cif', 100, 4)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (18, 5, N'Çamaşır Suyu', 100, 4)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (19, 4, N'Klavye', 100, 7)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (20, 4, N'Mouse', 100, 7)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (21, 4, N'Kulaklık', 100, 7)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (23, 4, N'Kasa', 100, 7)
INSERT [dbo].[Products] ([Id], [CategoryId], [Name], [Quantity], [Price]) VALUES (24, 4, N'Ekran Kartı', 100, 7)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Purses] ON 

INSERT [dbo].[Purses] ([Id], [UserId], [Money]) VALUES (1, 1, 10)
INSERT [dbo].[Purses] ([Id], [UserId], [Money]) VALUES (3, 3, 839)
SET IDENTITY_INSERT [dbo].[Purses] OFF
GO
SET IDENTITY_INSERT [dbo].[UserCarts] ON 

INSERT [dbo].[UserCarts] ([Id], [UserId]) VALUES (1, 1)
INSERT [dbo].[UserCarts] ([Id], [UserId]) VALUES (3, 3)
SET IDENTITY_INSERT [dbo].[UserCarts] OFF
GO
SET IDENTITY_INSERT [dbo].[UserOperationClaims] ON 

INSERT [dbo].[UserOperationClaims] ([Id], [UserId], [OperationClaimId]) VALUES (1, 1, 1)
INSERT [dbo].[UserOperationClaims] ([Id], [UserId], [OperationClaimId]) VALUES (3, 3, 2)
SET IDENTITY_INSERT [dbo].[UserOperationClaims] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [PasswordSalt], [RegistrationDate], [Status]) VALUES (1, N'admin', N'admin', N'admin@gmail.com', N'admin', N'admin', 0x0AD6C8A4867FE5B0F0C05DA1558FDB1CC507F4B43CCFCAA3CB5720E52109311D58C0C9AB869EB0BC6964C1AFFD837AF397935ABB60DE723D01B4698915E81ABC, 0x78D2273C7498E207C35C077FDF07F9CA47D5F8CD0CD9B9F9A9F69533CCC72FA1A9ABA531DFED9C2508ABF9BA53DC37BB4D4DE6F4C47852A86E1A3C701057B63F186CCC7A7D168FAD6C81BB1706F1636136EC47100B6546801BEDC5AC35003BAE7DB33F4BB82CA47CC3EF101C7DA904CDD235D71BA4D485A0CED8738B76F692C5, CAST(N'2023-01-22T19:52:17.0000000' AS DateTime2), 1)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Email], [PhoneNumber], [Address], [PasswordHash], [PasswordSalt], [RegistrationDate], [Status]) VALUES (3, N'Mustafa Alp', N'Yanıkoğlu', N'malpyanikoglu@gmail.com', N'5310128953', N'Turgutlu/Manisa', 0xD1EE08B01877D22ECF4316AE8E505AA9D20F2489C8E1F703687F73AC7695986555578B873E42A2D524A788782246ABEC7BF61F1DE8EAA02B2F9CB16F9A397570, 0xEF98B6B5CD56DE054A2C3413DC30692C75E10EAABE370626F6E6DCE1A740B01A56EB141931CCFDC9AE4CCD77EE1E46D22CAFD8866FD87D93A21D02EA21F7AD6296C69C2203439F148E4F818594423D6904CB1BA1C3FEA983050D1CF656C2760D10E1BAF7BD82D14938E253E61C7D2780E1A43D9270021FFC0AE2956FAB6D917E, CAST(N'2023-01-22T19:54:14.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_OrderDetails_OrderId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrderId] ON [dbo].[OrderDetails]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_ProductId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ProductId] ON [dbo].[OrderDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserCartId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserCartId] ON [dbo].[Orders]
(
	[UserCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purses_UserId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_Purses_UserId] ON [dbo].[Purses]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserCarts_UserId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserCarts_UserId] ON [dbo].[UserCarts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserOperationClaims_OperationClaimId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserOperationClaims_OperationClaimId] ON [dbo].[UserOperationClaims]
(
	[OperationClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserOperationClaims_UserId]    Script Date: 23.01.2023 14:27:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserOperationClaims_UserId] ON [dbo].[UserOperationClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(1))) FOR [Status]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_UserCarts_UserCartId] FOREIGN KEY([UserCartId])
REFERENCES [dbo].[UserCarts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_UserCarts_UserCartId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Purses]  WITH CHECK ADD  CONSTRAINT [FK_Purses_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purses] CHECK CONSTRAINT [FK_Purses_Users_UserId]
GO
ALTER TABLE [dbo].[UserCarts]  WITH CHECK ADD  CONSTRAINT [FK_UserCarts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCarts] CHECK CONSTRAINT [FK_UserCarts_Users_UserId]
GO
ALTER TABLE [dbo].[UserOperationClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserOperationClaims_OperationClaims_OperationClaimId] FOREIGN KEY([OperationClaimId])
REFERENCES [dbo].[OperationClaims] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserOperationClaims] CHECK CONSTRAINT [FK_UserOperationClaims_OperationClaims_OperationClaimId]
GO
ALTER TABLE [dbo].[UserOperationClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserOperationClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserOperationClaims] CHECK CONSTRAINT [FK_UserOperationClaims_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [ShoppingProjectDB] SET  READ_WRITE 
GO
