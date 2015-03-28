CREATE TABLE [dbo].[Donation] (
    [ID]        INT             IDENTITY (1, 1) NOT NULL,
    [AUBnet]    CHAR (8)        NOT NULL,
    [Kilos]     DECIMAL (18, 3) NOT NULL,
    [Processed] BIT             DEFAULT ((0)) NOT NULL,
    [Actor]     CHAR (8)        NOT NULL,
    [Timestamp] DATETIME        DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [Processed]
    ON [dbo].[Donation]([Processed] ASC);

