CREATE DATABASE FileManager;
USE [FileManager];

CREATE TABLE [User](
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [PasswordSalt] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [ModifiedDate] DATETIME,
    [IsActive] BIT NOT NULL
)


CREATE TABLE [Folder](
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Path] NVARCHAR(MAX) NOT NULL,
    [Size] SMALLINT NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [UserId] INT NOT NULL,
    [IsDeleted] BIT NOT NULL,
    [DeletedDate] DATETIME,
    FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]),
)

CREATE TABLE [File](
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Path] NVARCHAR(MAX) NOT NULL,
    [CreateDate] DATETIME NOT NULL,
    [Size] SMALLINT NOT NULL,
    [FileFormat] NVARCHAR(50) NOT NULL,
    [UserId] INT NOT NULL,
    [FolderId] INT NOT NULL,
    [IsDeleted] BIT NOT NULL,
    [DeletedDate] DATETIME,
    FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]),
    FOREIGN KEY ([FolderId]) REFERENCES [Folder] ([Id])
)

INSERT INTO [User] ([Name],[PasswordHash],[PasswordSalt],[Email],[CreatedDate],[ModifiedDate],[IsActive])
VALUES ('christian','index','index','admin@gmail.com','2022-05-24','2022-05-24',1)

INSERT INTO [Folder] ([Id],[Name],[Path],[Size],[CreateDate],[UserId],[IsDeleted],[DeletedDate])
VALUES (1,'BBB','C:\Users\Christian\Desktop\Project\FileManagerAngularAsp',0,'2022-05-24',1,0,NULL)

select * from [Folder]

DELETE FROM [Folder] WHERE [Id] =4 