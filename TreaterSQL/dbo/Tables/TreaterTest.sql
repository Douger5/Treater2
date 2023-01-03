CREATE TABLE [dbo].[TreaterTest] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [TreatingLineID]         INT            NOT NULL,
    [TreatmentDateTime]      DATETIME       NOT NULL,
    [AshPartID]              NVARCHAR (128) NULL,
    [OperatorA]              INT            NOT NULL,
    [OperatorB]              INT            NULL,
    [RollSkidNum]            NVARCHAR (MAX) NULL,
    [JobNum]                 NVARCHAR (MAX) NULL,
    [Qty]                    INT            NULL,
    [UOM]                    INT            NULL,
    [DryRollNum]             NVARCHAR (MAX) NULL,
    [TestNum]                INT            NULL,
    [Zone1Temp]              INT            NULL,
    [Zone2Temp]              INT            NULL,
    [Zone3Temp]              INT            NULL,
    [Speed]                  INT            NULL,
    [PanConfig]              INT            NULL,
    [PanLevel]               INT            NULL,
    [BarAngle]               INT            NULL,
    [ResinTemp]              INT            NULL,
    [ResinSolids]            INT            NULL,
    [RawPaperWeight]         INT            NOT NULL,
    [DryPaperWeight]         INT            NOT NULL,
    [TreatedPaperWeight]     INT            NOT NULL,
    [VolatilesTreatedWeight] INT            NOT NULL,
    [VolatilesOvenDryWeight] INT            NOT NULL,
    [TreatedPressedWeight]   INT            NOT NULL,
    [ResinContentCustomer]   FLOAT (53)     NULL,
    [Notes]                  NVARCHAR (MAX) NULL,
    [BarConfigID]            NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.TreaterTest] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.TreaterTest_dbo.AshPart_AshPartID] FOREIGN KEY ([AshPartID]) REFERENCES [dbo].[AshPart] ([AshPartID]),
    CONSTRAINT [FK_dbo.TreaterTest_dbo.BarConfig_BarConfigID] FOREIGN KEY ([BarConfigID]) REFERENCES [dbo].[BarConfig] ([BarConfigID]),
    CONSTRAINT [FK_dbo.TreaterTest_dbo.TreatingLine_TreatingLineID] FOREIGN KEY ([TreatingLineID]) REFERENCES [dbo].[TreatingLine] ([TreatingLineID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TreatingLineID]
    ON [dbo].[TreaterTest]([TreatingLineID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AshPartID]
    ON [dbo].[TreaterTest]([AshPartID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BarConfigID]
    ON [dbo].[TreaterTest]([BarConfigID] ASC);

