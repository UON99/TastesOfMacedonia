CREATE TABLE [dbo].[reservations] (
    [Id]              INT            IDENTITY(1,1) NOT NULL,
    [user]            NVARCHAR (256) NULL,
    [datetime]        DATETIME       NULL,
    [time]            TIME (7)       NULL,
    [restaurant_name] NVARCHAR (16)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

