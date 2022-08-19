IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Sites] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Sites] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customers] (
    [Id] nvarchar(450) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customers_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Queues] (
    [Id] int NOT NULL IDENTITY,
    [IsCompleted] bit NOT NULL,
    [TicketNumber] uniqueidentifier NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    [CustomerId] nvarchar(450) NULL,
    [SiteId] int NOT NULL,
    CONSTRAINT [PK_Queues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Queues_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Queues_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [Index_SiteId] ON [Customers] ([SiteId]);
GO

CREATE INDEX [Index_SiteId1] ON [Queues] ([SiteId]);
GO

CREATE UNIQUE INDEX [IX_Queues_CustomerId] ON [Queues] ([CustomerId]) WHERE [CustomerId] IS NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220819124707_InitialMigration', N'5.0.17');
GO

COMMIT;
GO

