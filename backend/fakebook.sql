﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
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

IF SCHEMA_ID(N'Fakebook') IS NULL EXEC(N'CREATE SCHEMA [Fakebook];');
GO

CREATE TABLE [Fakebook].[User] (
    [Id] int NOT NULL IDENTITY,
    [ProfilePictureUrl] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [BirthDate] datetime2 NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Fakebook].[Follow] (
    [FollowerId] int NOT NULL,
    [FolloweeId] int NOT NULL,
    CONSTRAINT [Pk_FollowEntity] PRIMARY KEY ([FollowerId], [FolloweeId]),
    CONSTRAINT [Fk_Follow_Followee] FOREIGN KEY ([FolloweeId]) REFERENCES [Fakebook].[User] ([Id]),
    CONSTRAINT [Fk_Follow_Follower] FOREIGN KEY ([FollowerId]) REFERENCES [Fakebook].[User] ([Id])
);
GO

CREATE TABLE [Fakebook].[Post] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [Content] nvarchar(max) NOT NULL,
    [Picture] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL DEFAULT ((getdate())),
    CONSTRAINT [PK_Post] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Post_UserId] FOREIGN KEY ([UserId]) REFERENCES [Fakebook].[User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Fakebook].[Comment] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [PostId] int NOT NULL,
    [ParentId] int NULL,
    [CreatedAt] smalldatetime NOT NULL DEFAULT ((getdate())),
    [Content] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY ([Id]),
    CONSTRAINT [Fk_Comment_Comment] FOREIGN KEY ([ParentId]) REFERENCES [Fakebook].[Comment] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [Fk_Comment_Post] FOREIGN KEY ([PostId]) REFERENCES [Fakebook].[Post] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_COMMENT_USER] FOREIGN KEY ([UserId]) REFERENCES [Fakebook].[User] ([Id])
);
GO

CREATE TABLE [Fakebook].[Like] (
    [Id] int NOT NULL IDENTITY,
    [PostId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Like] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Like_Post] FOREIGN KEY ([PostId]) REFERENCES [Fakebook].[Post] ([Id]),
    CONSTRAINT [FK_Like_User] FOREIGN KEY ([UserId]) REFERENCES [Fakebook].[User] ([Id])
);
GO

CREATE INDEX [IX_Comment_ParentId] ON [Fakebook].[Comment] ([ParentId]);
GO

CREATE INDEX [IX_Comment_PostId] ON [Fakebook].[Comment] ([PostId]);
GO

CREATE INDEX [IX_Comment_UserId] ON [Fakebook].[Comment] ([UserId]);
GO

CREATE INDEX [IX_Follow_FolloweeId] ON [Fakebook].[Follow] ([FolloweeId]);
GO

CREATE INDEX [IX_Like_PostId] ON [Fakebook].[Like] ([PostId]);
GO

CREATE INDEX [IX_Like_UserId] ON [Fakebook].[Like] ([UserId]);
GO

CREATE INDEX [IX_Post_UserId] ON [Fakebook].[Post] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201212223519_DateTimeBugFix', N'5.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Fakebook].[Like] DROP CONSTRAINT [PK_Like];
GO

DROP INDEX [IX_Like_UserId] ON [Fakebook].[Like];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fakebook].[Like]') AND [c].[name] = N'Id');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Fakebook].[Like] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Fakebook].[Like] DROP COLUMN [Id];
GO

ALTER TABLE [Fakebook].[Like] ADD CONSTRAINT [Pk_LikeEntity] PRIMARY KEY ([UserId], [PostId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201213170020_LikeCompositeKey', N'5.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fakebook].[Post]') AND [c].[name] = N'Picture');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Fakebook].[Post] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Fakebook].[Post] ALTER COLUMN [Picture] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201215230810_PictureStringNullable', N'5.0.1');
GO

COMMIT;
GO

