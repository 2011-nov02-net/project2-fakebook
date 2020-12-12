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

