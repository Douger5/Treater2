CREATE TABLE [dbo].[ReleaseSheet] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [ItemID]          NVARCHAR (128) NULL,
    [ReleaseDateTime] DATETIME       NOT NULL,
    [Job]             NVARCHAR (MAX) NULL,
    [Board]           NVARCHAR (MAX) NULL,
    [Width]           INT            NULL,
    [Length]          INT            NULL,
    [Lbs]             INT            NULL,
    [Sheets]          INT            NULL,
    [Roll]            NVARCHAR (MAX) NULL,
    [Rejected]        INT            NULL,
    [Notes]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.ReleaseSheet] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.ReleaseSheet_dbo.Item_ItemID] FOREIGN KEY ([ItemID]) REFERENCES [dbo].[Item] ([ItemID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ItemID]
    ON [dbo].[ReleaseSheet]([ItemID] ASC);

