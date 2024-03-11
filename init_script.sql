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

IF SCHEMA_ID(N'dto') IS NULL EXEC(N'CREATE SCHEMA [dto];');
GO

CREATE TABLE [dto].[games] (
    [Id] bigint NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Rating] int NOT NULL,
    [Image] varbinary(max) NOT NULL,
    [Genre] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_games] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dto].[users] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [EmailAddress] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] int NOT NULL,
    [OAuthProvider] nvarchar(max) NOT NULL,
    [OAuthId] nvarchar(max) NOT NULL,
    [TokenGuid] uniqueidentifier NOT NULL,
    [EmailSearchHash] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dto].[orders] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [TotalAmount] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_orders_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dto].[users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dto].[shopping_cart] (
    [Id] bigint NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_shopping_cart] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_shopping_cart_users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dto].[users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dto].[order_items] (
    [Id] bigint NOT NULL IDENTITY,
    [OrderId] bigint NOT NULL,
    [GameId] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [Price] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_order_items] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_order_items_games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dto].[games] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_order_items_orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dto].[orders] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [dto].[shopping_cart_items] (
    [Id] bigint NOT NULL IDENTITY,
    [CartId] bigint NOT NULL,
    [GameId] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [Price] float NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [LastUpdatedAt] datetime2 NOT NULL,
    CONSTRAINT [PK_shopping_cart_items] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_shopping_cart_items_games_GameId] FOREIGN KEY ([GameId]) REFERENCES [dto].[games] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_shopping_cart_items_shopping_cart_CartId] FOREIGN KEY ([CartId]) REFERENCES [dto].[shopping_cart] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_order_items_GameId] ON [dto].[order_items] ([GameId]);
GO

CREATE INDEX [IX_order_items_OrderId] ON [dto].[order_items] ([OrderId]);
GO

CREATE INDEX [IX_orders_UserId] ON [dto].[orders] ([UserId]);
GO

CREATE INDEX [IX_shopping_cart_UserId] ON [dto].[shopping_cart] ([UserId]);
GO

CREATE INDEX [IX_shopping_cart_items_CartId] ON [dto].[shopping_cart_items] ([CartId]);
GO

CREATE INDEX [IX_shopping_cart_items_GameId] ON [dto].[shopping_cart_items] ([GameId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240311205231_Initial', N'8.0.2');
GO

COMMIT;
GO

