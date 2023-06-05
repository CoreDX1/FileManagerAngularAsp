CREATE DATABASE FileManager;
USE [FileManager];

CREATE TABLE [User](
    [Id] INT PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [PasswordSalt] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(100) NOT NULL,
    [CreatedDate] DATETIME NOT NULL,
    [ModifiedDate] DATETIME,
    [IsActive] BIT NOT NULL
)


CREATE TABLE [Folder](
    [Id] INT PRIMARY KEY,
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
    [Id] INT PRIMARY KEY,
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