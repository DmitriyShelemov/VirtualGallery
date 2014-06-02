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

COMMIT TRANSACTION

  