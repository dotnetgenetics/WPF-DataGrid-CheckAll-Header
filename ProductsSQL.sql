USE [TestDatabase]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 09/24/2012 15:31:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Products](
	[productID] [int] NOT NULL,
	[ProductName] [varchar](50) NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[QuantityPerUnit] [varchar](50) NULL,
	[Discontinue] [bit] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


