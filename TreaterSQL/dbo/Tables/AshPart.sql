CREATE TABLE [dbo].[AshPart] (
    [AshPartID]      NVARCHAR (128) NOT NULL,
    [ResinMix]       NVARCHAR (MAX) NULL,
    [TreatingSpecID] NVARCHAR (128) NULL,
    [Paper]          NVARCHAR (MAX) NULL,
    [Size]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AshPart] PRIMARY KEY CLUSTERED ([AshPartID] ASC),
    CONSTRAINT [FK_dbo.AshPart_dbo.TreatingSpec_TreatingSpecID] FOREIGN KEY ([TreatingSpecID]) REFERENCES [dbo].[TreatingSpec] ([TreatingSpecID])
);


GO
CREATE NONCLUSTERED INDEX [IX_TreatingSpecID]
    ON [dbo].[AshPart]([TreatingSpecID] ASC);

