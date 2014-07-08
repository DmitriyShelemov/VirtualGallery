SET XACT_ABORT ON;
GO

BEGIN TRANSACTION

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Details' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [Details]	NVARCHAR (MAX) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'PriceRouble' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [PriceRouble] smallmoney NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'PriceEuro' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [PriceEuro] smallmoney NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'PriceDollar' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [PriceDollar] smallmoney NOT NULL DEFAULT 0
END
GO

IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Preferences]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[Preferences] (    
		[Id]		INT				IDENTITY (1, 1) NOT NULL CONSTRAINT [PK_Preferences] PRIMARY KEY CLUSTERED ([Id] ASC),
		[Intro]		NVARCHAR (MAX)	NULL,
		[About]		NVARCHAR (MAX)	NULL,
		[PhotoId]	INT				NULL
	)

	ALTER TABLE [dbo].[Preferences] 
		ADD CONSTRAINT [FK_Preferences_PhotoId] FOREIGN KEY ([PhotoId])
		REFERENCES [dbo].[StoredFiles] ([Id]) 
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Phone' AND object_id = OBJECT_ID(N'[dbo].[Preferences]'))
BEGIN		
	ALTER TABLE [dbo].[Preferences] ADD [Phone] NVARCHAR (MAX)	NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Address' AND object_id = OBJECT_ID(N'[dbo].[Preferences]'))
BEGIN		
	ALTER TABLE [dbo].[Preferences] ADD [Address] NVARCHAR (MAX)	NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'WorkTime' AND object_id = OBJECT_ID(N'[dbo].[Preferences]'))
BEGIN		
	ALTER TABLE [dbo].[Preferences] ADD [WorkTime] NVARCHAR (MAX)	NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'MapUrl' AND object_id = OBJECT_ID(N'[dbo].[Preferences]'))
BEGIN		
	ALTER TABLE [dbo].[Preferences] ADD [MapUrl] NVARCHAR (MAX)	NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'About2' AND object_id = OBJECT_ID(N'[dbo].[Preferences]'))
BEGIN		
	ALTER TABLE [dbo].[Preferences] ADD [About2] NVARCHAR (MAX)	NULL
END
GO

IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
BEGIN	
	CREATE TABLE [dbo].[Orders] (
		[Id]			INT	IDENTITY (1, 1) NOT NULL,
		[From]			NVARCHAR (MAX) NULL,
		[Email]			NVARCHAR (MAX) NULL,
		[Phone]			NVARCHAR (MAX) NULL,
		[Details]		NVARCHAR (MAX) NULL,
		[DeliveryType]	INT			   NOT NULL,
		[Completed]		BIT            NOT NULL DEFAULT 0,
		[Deleted]		BIT            NOT NULL DEFAULT 0,
		[CreateDate]    DATETIME       NOT NULL,
		[UpdateDate]    DATETIME       NOT NULL,
		CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED ([Id] ASC)
	)

	CREATE TABLE [dbo].[Lots] (
		[Id]			INT	IDENTITY (1, 1) NOT NULL,
		[PictureId]		INT            NOT NULL,
		[OrderId]		INT            NOT NULL,
		CONSTRAINT [PK_dbo.Lots] PRIMARY KEY CLUSTERED ([Id] ASC),
		CONSTRAINT [FK_dbo.Lots_dbo.Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]) ON DELETE CASCADE,
		CONSTRAINT [FK_dbo.Lots_dbo.Pictures_PictureId] FOREIGN KEY ([PictureId]) REFERENCES [dbo].[Pictures] ([Id])
	)
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Reserved' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [Reserved] BIT NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Sold' AND object_id = OBJECT_ID(N'[dbo].[Pictures]'))
BEGIN		
	ALTER TABLE [dbo].[Pictures] ADD [Sold] BIT NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Deleted' AND object_id = OBJECT_ID(N'[dbo].[Categories]'))
BEGIN		
	ALTER TABLE [dbo].[Categories] ADD [Deleted] BIT NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Order' AND object_id = OBJECT_ID(N'[dbo].[Categories]'))
BEGIN		
	ALTER TABLE [dbo].[Categories] ADD [Order] INT NOT NULL DEFAULT 0
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = N'Decor' AND object_id = OBJECT_ID(N'[dbo].[Lots]'))
BEGIN		
	ALTER TABLE [dbo].[Lots] ADD [Decor] INT NOT NULL DEFAULT 0
END
GO

COMMIT TRANSACTION

  