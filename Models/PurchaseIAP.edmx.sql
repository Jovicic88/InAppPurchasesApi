
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/12/2017 12:36:44
-- Generated from EDMX file: E:\Projects\InAppPurchasesApi\Models\PurchaseIAP.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [master];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Level_Level]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Level] DROP CONSTRAINT [FK_Level_Level];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGame_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGame] DROP CONSTRAINT [FK_UserGame_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGame_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGame] DROP CONSTRAINT [FK_UserGame_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMap_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMap] DROP CONSTRAINT [FK_UserMap_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_UserMap_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMap] DROP CONSTRAINT [FK_UserMap_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWeapon_Game]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserWeapon] DROP CONSTRAINT [FK_UserWeapon_Game];
GO
IF OBJECT_ID(N'[dbo].[FK_UserWeapon_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserWeapon] DROP CONSTRAINT [FK_UserWeapon_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Game]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Game];
GO
IF OBJECT_ID(N'[dbo].[Gun]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Gun];
GO
IF OBJECT_ID(N'[dbo].[Level]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Level];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserGame]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGame];
GO
IF OBJECT_ID(N'[dbo].[UserMap]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMap];
GO
IF OBJECT_ID(N'[dbo].[UserWeapon]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserWeapon];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Games'
CREATE TABLE [dbo].[Games] (
    [GameId] int  NOT NULL,
    [Version] int  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL
);
GO

-- Creating table 'UserGames'
CREATE TABLE [dbo].[UserGames] (
    [UserGameId] int IDENTITY(1,1) NOT NULL,
    [GameId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [Achivements] varchar(max)  NULL,
    [Coins] int  NULL,
    [Diamonds] int  NULL,
    [Premium] bit  NULL,
    [HasGame] bit  NOT NULL
);
GO

-- Creating table 'UserWeapons'
CREATE TABLE [dbo].[UserWeapons] (
    [UserWeaponId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [GameId] int  NOT NULL,
    [WeaponId] int  NOT NULL,
    [GunLevel] int  NULL,
    [Experience] int  NULL,
    [BoostActive] bit  NULL,
    [Unlocked] bit  NULL,
    [BoostEndTime] varchar(50)  NULL
);
GO

-- Creating table 'Guns'
CREATE TABLE [dbo].[Guns] (
    [GunId] int  NOT NULL,
    [Name] varchar(191)  NOT NULL,
    [NameStatic] varchar(191)  NOT NULL,
    [Type] int  NOT NULL,
    [Pause] float  NOT NULL,
    [GunLevel] int  NOT NULL,
    [BoostCost] int  NOT NULL,
    [Unlocked] bit  NOT NULL,
    [Description] varchar(191)  NOT NULL,
    [PathToPrefab] varchar(191)  NOT NULL,
    [PathToIcon] varchar(191)  NOT NULL,
    [PathToAchivementIcon] varchar(191)  NOT NULL,
    [PathToAchivementIconLocked] varchar(191)  NOT NULL,
    [PathToWeaponAudio] varchar(191)  NOT NULL,
    [TwoHanded] bit  NOT NULL
);
GO

-- Creating table 'Levels'
CREATE TABLE [dbo].[Levels] (
    [LevelId] int  NOT NULL,
    [GunId] int  NOT NULL,
    [GunLevel] int  NOT NULL,
    [Cost] int  NOT NULL,
    [Damage] int  NOT NULL,
    [BulletsInClip] int  NOT NULL,
    [BulletsMax] int  NOT NULL
);
GO

-- Creating table 'UserMaps'
CREATE TABLE [dbo].[UserMaps] (
    [UserMapId] int IDENTITY(1,1) NOT NULL,
    [MapId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [GameId] int  NOT NULL,
    [Unlocked] bit  NOT NULL,
    [Name] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GameId] in table 'Games'
ALTER TABLE [dbo].[Games]
ADD CONSTRAINT [PK_Games]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserGameId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [PK_UserGames]
    PRIMARY KEY CLUSTERED ([UserGameId] ASC);
GO

-- Creating primary key on [UserWeaponId] in table 'UserWeapons'
ALTER TABLE [dbo].[UserWeapons]
ADD CONSTRAINT [PK_UserWeapons]
    PRIMARY KEY CLUSTERED ([UserWeaponId] ASC);
GO

-- Creating primary key on [GunId] in table 'Guns'
ALTER TABLE [dbo].[Guns]
ADD CONSTRAINT [PK_Guns]
    PRIMARY KEY CLUSTERED ([GunId] ASC);
GO

-- Creating primary key on [LevelId] in table 'Levels'
ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [PK_Levels]
    PRIMARY KEY CLUSTERED ([LevelId] ASC);
GO

-- Creating primary key on [UserMapId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [PK_UserMaps]
    PRIMARY KEY CLUSTERED ([UserMapId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GameId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [FK_UserGame_Game]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGame_Game'
CREATE INDEX [IX_FK_UserGame_Game]
ON [dbo].[UserGames]
    ([GameId]);
GO

-- Creating foreign key on [GameId] in table 'UserWeapons'
ALTER TABLE [dbo].[UserWeapons]
ADD CONSTRAINT [FK_UserWeapon_Game]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserWeapon_Game'
CREATE INDEX [IX_FK_UserWeapon_Game]
ON [dbo].[UserWeapons]
    ([GameId]);
GO

-- Creating foreign key on [UserId] in table 'UserGames'
ALTER TABLE [dbo].[UserGames]
ADD CONSTRAINT [FK_UserGame_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGame_User'
CREATE INDEX [IX_FK_UserGame_User]
ON [dbo].[UserGames]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserWeapons'
ALTER TABLE [dbo].[UserWeapons]
ADD CONSTRAINT [FK_UserWeapon_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserWeapon_User'
CREATE INDEX [IX_FK_UserWeapon_User]
ON [dbo].[UserWeapons]
    ([UserId]);
GO

-- Creating foreign key on [GunId] in table 'Levels'
ALTER TABLE [dbo].[Levels]
ADD CONSTRAINT [FK_Level_Level]
    FOREIGN KEY ([GunId])
    REFERENCES [dbo].[Guns]
        ([GunId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Level_Level'
CREATE INDEX [IX_FK_Level_Level]
ON [dbo].[Levels]
    ([GunId]);
GO

-- Creating foreign key on [GameId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [FK_UserMap_Game]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Games]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMap_Game'
CREATE INDEX [IX_FK_UserMap_Game]
ON [dbo].[UserMaps]
    ([GameId]);
GO

-- Creating foreign key on [UserId] in table 'UserMaps'
ALTER TABLE [dbo].[UserMaps]
ADD CONSTRAINT [FK_UserMap_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserMap_User'
CREATE INDEX [IX_FK_UserMap_User]
ON [dbo].[UserMaps]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------