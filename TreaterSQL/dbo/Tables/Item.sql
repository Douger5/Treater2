CREATE TABLE [dbo].[Item] (
    [ItemID]      NVARCHAR (128) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Item] PRIMARY KEY CLUSTERED ([ItemID] ASC)
);

