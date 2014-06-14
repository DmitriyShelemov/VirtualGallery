------------------------------------------------------------------------------
-- TABLES
------------------------------------------------------------------------------

CREATE TABLE [dbo].[Categories] (
    [Id]			INT	IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (MAX) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [CreateDate]    DATETIME       NOT NULL,
    [UpdateDate]    DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[StoredFiles] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [OriginalFileName] NVARCHAR (MAX) NULL,
    [PhysicalPath]     NVARCHAR (MAX) NULL,
    [CreateDate]       DATETIME       NOT NULL,
    [SizeInBytes]      BIGINT         NOT NULL,
    CONSTRAINT [PK_dbo.StoredFiles] PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[Pictures] (
    [Id]			INT	IDENTITY (1, 1) NOT NULL,
    [Name]			NVARCHAR (MAX) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [Details]		NVARCHAR (MAX) NULL,
    [CategoryId]	INT            NOT NULL,
    [FileId]		INT			   NOT NULL,
    [ThumbnailId]	INT            NOT NULL,
    [Topic]			BIT            NOT NULL,
    [Width]			INT            NOT NULL,
    [Height]		INT            NOT NULL,
	[PriceRouble]	smallmoney	   NOT NULL DEFAULT 0,
	[PriceEuro]		smallmoney	   NOT NULL DEFAULT 0,
	[PriceDollar]	smallmoney	   NOT NULL DEFAULT 0,
    CONSTRAINT [PK_dbo.Pictures] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.Pictures_dbo.Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Pictures_dbo.StoredFiles_FileId] FOREIGN KEY ([FileId]) REFERENCES [dbo].[StoredFiles] ([Id]),
    CONSTRAINT [FK_dbo.Pictures_dbo.StoredFiles_ThumbnailId] FOREIGN KEY ([ThumbnailId]) REFERENCES [dbo].[StoredFiles] ([Id])
)

------------------------------------------------------------------------------

CREATE TABLE [dbo].[Preferences] (    
	[Id]		INT				IDENTITY (1, 1) NOT NULL CONSTRAINT [PK_Preferences] PRIMARY KEY CLUSTERED ([Id] ASC),
	[Intro]		NVARCHAR (MAX)	NULL,
	[About]		NVARCHAR (MAX)	NULL,
	[PhotoId]	INT				NULL
)

ALTER TABLE [dbo].[Preferences] 
	ADD CONSTRAINT [FK_Preferences_PhotoId] FOREIGN KEY ([PhotoId])
	REFERENCES [dbo].[StoredFiles] ([Id]) 