CREATE TABLE [dbo].[TreatingSpec] (
    [TreatingSpecID] NVARCHAR (128) NOT NULL,
    [FlowTarget]     FLOAT (53)     NOT NULL,
    [FlowMax]        FLOAT (53)     NOT NULL,
    [FlowMin]        FLOAT (53)     NOT NULL,
    [RCTarget]       FLOAT (53)     NOT NULL,
    [RCMax]          FLOAT (53)     NOT NULL,
    [RCMin]          FLOAT (53)     NOT NULL,
    [NetRCTarget]    FLOAT (53)     NOT NULL,
    [VolTarget]      FLOAT (53)     NOT NULL,
    [VolMax]         FLOAT (53)     NOT NULL,
    [VolMin]         FLOAT (53)     NOT NULL,
    [Comment]        NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TreatingSpec] PRIMARY KEY CLUSTERED ([TreatingSpecID] ASC)
);

