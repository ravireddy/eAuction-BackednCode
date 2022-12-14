USE [Auction]
GO
/****** Object:  Table [dbo].[BidDetails]    Script Date: 20-10-2022 16:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BidDetails](
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Pin] [nvarchar](50) NULL,
	[Phone] [numeric](10, 0) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
	[BidAmount] [decimal](18, 2) NOT NULL,
	[BuyerId] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetails]    Script Date: 20-10-2022 16:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetails](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](30) NOT NULL,
	[ShortDescription] [nvarchar](50) NULL,
	[DetailedDescription] [nvarchar](500) NULL,
	[Category] [nvarchar](50) NOT NULL,
	[StartingPrice] [decimal](18, 2) NOT NULL,
	[BidEndDate] [date] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerDetails]    Script Date: 20-10-2022 16:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerDetails](
	[SellerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](25) NOT NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Pin] [nvarchar](10) NULL,
	[Phone] [numeric](10, 0) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_SellerDetails] PRIMARY KEY CLUSTERED 
(
	[SellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 20-10-2022 16:09:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[IsAdmin] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetails_Role]  DEFAULT (N'ROLE_USER') FOR [Role]
GO
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetails_IsAdmin]  DEFAULT (N'false') FOR [IsAdmin]
GO
ALTER TABLE [dbo].[BidDetails]  WITH CHECK ADD  CONSTRAINT [FK_BidDetails_ProductDetails] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductDetails] ([ProductId])
GO
ALTER TABLE [dbo].[BidDetails] CHECK CONSTRAINT [FK_BidDetails_ProductDetails]
GO
ALTER TABLE [dbo].[SellerDetails]  WITH CHECK ADD  CONSTRAINT [FK_SellerDetails_ProductDetails] FOREIGN KEY([ProductId])
REFERENCES [dbo].[ProductDetails] ([ProductId])
GO
ALTER TABLE [dbo].[SellerDetails] CHECK CONSTRAINT [FK_SellerDetails_ProductDetails]
GO
ALTER TABLE [dbo].[BidDetails]  WITH CHECK ADD  CONSTRAINT [CK_BidDetails_BidAmount] CHECK  (([BidAmount]>(0)))
GO
ALTER TABLE [dbo].[BidDetails] CHECK CONSTRAINT [CK_BidDetails_BidAmount]
GO
ALTER TABLE [dbo].[BidDetails]  WITH CHECK ADD  CONSTRAINT [CK_BidDetails_FirstName] CHECK  ((len([FirstName])>=(5) AND len([FirstName])<=(30)))
GO
ALTER TABLE [dbo].[BidDetails] CHECK CONSTRAINT [CK_BidDetails_FirstName]
GO
ALTER TABLE [dbo].[BidDetails]  WITH CHECK ADD  CONSTRAINT [CK_BidDetails_LastName] CHECK  ((len([LastName])>=(3) AND len([LastName])<=(25)))
GO
ALTER TABLE [dbo].[BidDetails] CHECK CONSTRAINT [CK_BidDetails_LastName]
GO
ALTER TABLE [dbo].[BidDetails]  WITH CHECK ADD  CONSTRAINT [CK_BidDetails_Phone] CHECK  ((len([Phone])=(10)))
GO
ALTER TABLE [dbo].[BidDetails] CHECK CONSTRAINT [CK_BidDetails_Phone]
GO
ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD  CONSTRAINT [CK_ProductDetails_Category] CHECK  (([Category]='Painting' OR [Category]='Sculpture' OR [Category]='Ornament'))
GO
ALTER TABLE [dbo].[ProductDetails] CHECK CONSTRAINT [CK_ProductDetails_Category]
GO
ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD  CONSTRAINT [CK_ProductDetails_ProductName] CHECK  ((len([ProductName])>=(5) AND len([ProductName])<=(30)))
GO
ALTER TABLE [dbo].[ProductDetails] CHECK CONSTRAINT [CK_ProductDetails_ProductName]
GO
ALTER TABLE [dbo].[ProductDetails]  WITH CHECK ADD  CONSTRAINT [CK_ProductDetails_StartingPrice] CHECK  (([StartingPrice]>(0)))
GO
ALTER TABLE [dbo].[ProductDetails] CHECK CONSTRAINT [CK_ProductDetails_StartingPrice]
GO
ALTER TABLE [dbo].[SellerDetails]  WITH CHECK ADD  CONSTRAINT [CK_SellerDetails_FirstName] CHECK  ((len([FirstName])>=(5) AND len([FirstName])<=(30)))
GO
ALTER TABLE [dbo].[SellerDetails] CHECK CONSTRAINT [CK_SellerDetails_FirstName]
GO
ALTER TABLE [dbo].[SellerDetails]  WITH CHECK ADD  CONSTRAINT [CK_SellerDetails_LastName] CHECK  ((len([LastName])>=(3) AND len([LastName])<=(25)))
GO
ALTER TABLE [dbo].[SellerDetails] CHECK CONSTRAINT [CK_SellerDetails_LastName]
GO
ALTER TABLE [dbo].[SellerDetails]  WITH CHECK ADD  CONSTRAINT [CK_SellerDetails_Phone] CHECK  ((len([Phone])=(10)))
GO
ALTER TABLE [dbo].[SellerDetails] CHECK CONSTRAINT [CK_SellerDetails_Phone]
GO
