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

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201212215251_AttemptFollowForeignKeyII', N'5.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201212215520_AttemptFollowForeignKeyIII', N'5.0.1');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Fakebook].[Follow] DROP CONSTRAINT [FK_Follow_FolloweeId];
GO

ALTER TABLE [Fakebook].[Follow] DROP CONSTRAINT [FK_Follow_FollowerId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fakebook].[Post]') AND [c].[name] = N'CreatedAt');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Fakebook].[Post] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Fakebook].[Post] ADD DEFAULT ((getdate())) FOR [CreatedAt];
GO

ALTER TABLE [Fakebook].[Follow] ADD CONSTRAINT [Fk_Follow_Followee] FOREIGN KEY ([FolloweeId]) REFERENCES [Fakebook].[User] ([Id]);
GO

ALTER TABLE [Fakebook].[Follow] ADD CONSTRAINT [Fk_Follow_Follower] FOREIGN KEY ([FollowerId]) REFERENCES [Fakebook].[User] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201212222711_DateTimeBugFix', N'5.0.1');
GO

COMMIT;
GO

