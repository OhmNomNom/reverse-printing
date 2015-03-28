CREATE TABLE [dbo].[Reward] (
    [Id]     INT             IDENTITY (1, 1) NOT NULL,
    [Major]  NCHAR (16)      NOT NULL,
    [Factor] DECIMAL (18, 3)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Major_Index]
    ON [dbo].[Reward]([Major] ASC);

