
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 09/29/2011 16:48:18
-- Generated from EDMX file: T:\Codes\C#\mapitprices\Models\MapItPrices.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SQL2008R2_778484_mapitprices];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_StoreItems_Items]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_StoreItems_Items];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreItems_Stores]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_StoreItems_Stores];
GO
IF OBJECT_ID(N'[dbo].[FK_UserOpenID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OpenIDs] DROP CONSTRAINT [FK_UserOpenID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStoreItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreItems] DROP CONSTRAINT [FK_UserStoreItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Items] DROP CONSTRAINT [FK_UserItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserStore]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stores] DROP CONSTRAINT [FK_UserStore];
GO
IF OBJECT_ID(N'[dbo].[FK_BadgeUser_Badge]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeUser] DROP CONSTRAINT [FK_BadgeUser_Badge];
GO
IF OBJECT_ID(N'[dbo].[FK_BadgeUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeUser] DROP CONSTRAINT [FK_BadgeUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_UserShoppingList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingLists] DROP CONSTRAINT [FK_UserShoppingList];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemShoppingList_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemShoppingList] DROP CONSTRAINT [FK_ItemShoppingList_Item];
GO
IF OBJECT_ID(N'[dbo].[FK_ItemShoppingList_ShoppingList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ItemShoppingList] DROP CONSTRAINT [FK_ItemShoppingList_ShoppingList];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryItem_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryItem] DROP CONSTRAINT [FK_CategoryItem_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryItem_Item]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryItem] DROP CONSTRAINT [FK_CategoryItem_Item];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Items]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Items];
GO
IF OBJECT_ID(N'[dbo].[StoreItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreItems];
GO
IF OBJECT_ID(N'[dbo].[Stores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stores];
GO
IF OBJECT_ID(N'[dbo].[OpenIDs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OpenIDs];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Badges]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Badges];
GO
IF OBJECT_ID(N'[dbo].[BetaInviteCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BetaInviteCodes];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[ShoppingLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingLists];
GO
IF OBJECT_ID(N'[dbo].[BetaSignups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BetaSignups];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[BadgeUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadgeUser];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO
IF OBJECT_ID(N'[dbo].[ItemShoppingList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemShoppingList];
GO
IF OBJECT_ID(N'[dbo].[CategoryItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryItem];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [UPC] nvarchar(max)  NULL,
    [Size] nchar(20)  NULL,
    [UserID] int  NOT NULL,
    [Brand] nvarchar(max)  NULL,
    [Created] datetime  NOT NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'StoreItems'
CREATE TABLE [dbo].[StoreItems] (
    [ItemId] int  NOT NULL,
    [StoreId] int  NOT NULL,
    [Price] decimal(18,2)  NOT NULL,
    [LastUpdated] datetime  NOT NULL,
    [UserID] int  NOT NULL,
    [Created] datetime  NOT NULL,
    [Quantity] int  NULL
);
GO

-- Creating table 'Stores'
CREATE TABLE [dbo].[Stores] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Address] nvarchar(200)  NULL,
    [City] nvarchar(30)  NULL,
    [State] nvarchar(2)  NULL,
    [Zip] nvarchar(10)  NULL,
    [Latitude] float  NULL,
    [Longitude] float  NULL,
    [UserID] int  NOT NULL,
    [Created] datetime  NOT NULL,
    [FoursquareVenueID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OpenIDs'
CREATE TABLE [dbo].[OpenIDs] (
    [ClaimedIdentifier] nvarchar(200)  NOT NULL,
    [UserID] int  NOT NULL,
    [Created] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Email] nvarchar(max)  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL,
    [Created] datetime  NOT NULL,
    [LastUpdated] datetime  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [SessionToken] nvarchar(max)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Badges'
CREATE TABLE [dbo].[Badges] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BetaInviteCodes'
CREATE TABLE [dbo].[BetaInviteCodes] (
    [InviteCode] nvarchar(20)  NOT NULL,
    [IsUsed] bit  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ShoppingLists'
CREATE TABLE [dbo].[ShoppingLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL,
    [LastUpdated] datetime  NOT NULL
);
GO

-- Creating table 'BetaSignups'
CREATE TABLE [dbo].[BetaSignups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Created] datetime  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BadgeUser'
CREATE TABLE [dbo].[BadgeUser] (
    [Badges_ID] int  NOT NULL,
    [Users_ID] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Users_ID] int  NOT NULL,
    [Roles_ID] int  NOT NULL
);
GO

-- Creating table 'ItemShoppingList'
CREATE TABLE [dbo].[ItemShoppingList] (
    [Items_ID] int  NOT NULL,
    [ShoppingLists_Id] int  NOT NULL
);
GO

-- Creating table 'CategoryItem'
CREATE TABLE [dbo].[CategoryItem] (
    [Categories_Id] int  NOT NULL,
    [Items_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ItemId], [StoreId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [PK_StoreItems]
    PRIMARY KEY CLUSTERED ([ItemId], [StoreId] ASC);
GO

-- Creating primary key on [ID] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [PK_Stores]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ClaimedIdentifier], [UserID] in table 'OpenIDs'
ALTER TABLE [dbo].[OpenIDs]
ADD CONSTRAINT [PK_OpenIDs]
    PRIMARY KEY CLUSTERED ([ClaimedIdentifier], [UserID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Badges'
ALTER TABLE [dbo].[Badges]
ADD CONSTRAINT [PK_Badges]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [InviteCode] in table 'BetaInviteCodes'
ALTER TABLE [dbo].[BetaInviteCodes]
ADD CONSTRAINT [PK_BetaInviteCodes]
    PRIMARY KEY CLUSTERED ([InviteCode] ASC);
GO

-- Creating primary key on [ID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'ShoppingLists'
ALTER TABLE [dbo].[ShoppingLists]
ADD CONSTRAINT [PK_ShoppingLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BetaSignups'
ALTER TABLE [dbo].[BetaSignups]
ADD CONSTRAINT [PK_BetaSignups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Badges_ID], [Users_ID] in table 'BadgeUser'
ALTER TABLE [dbo].[BadgeUser]
ADD CONSTRAINT [PK_BadgeUser]
    PRIMARY KEY NONCLUSTERED ([Badges_ID], [Users_ID] ASC);
GO

-- Creating primary key on [Users_ID], [Roles_ID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY NONCLUSTERED ([Users_ID], [Roles_ID] ASC);
GO

-- Creating primary key on [Items_ID], [ShoppingLists_Id] in table 'ItemShoppingList'
ALTER TABLE [dbo].[ItemShoppingList]
ADD CONSTRAINT [PK_ItemShoppingList]
    PRIMARY KEY NONCLUSTERED ([Items_ID], [ShoppingLists_Id] ASC);
GO

-- Creating primary key on [Categories_Id], [Items_ID] in table 'CategoryItem'
ALTER TABLE [dbo].[CategoryItem]
ADD CONSTRAINT [PK_CategoryItem]
    PRIMARY KEY NONCLUSTERED ([Categories_Id], [Items_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ItemId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_StoreItems_Items]
    FOREIGN KEY ([ItemId])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [StoreId] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_StoreItems_Stores]
    FOREIGN KEY ([StoreId])
    REFERENCES [dbo].[Stores]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreItems_Stores'
CREATE INDEX [IX_FK_StoreItems_Stores]
ON [dbo].[StoreItems]
    ([StoreId]);
GO

-- Creating foreign key on [UserID] in table 'OpenIDs'
ALTER TABLE [dbo].[OpenIDs]
ADD CONSTRAINT [FK_UserOpenID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserOpenID'
CREATE INDEX [IX_FK_UserOpenID]
ON [dbo].[OpenIDs]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'StoreItems'
ALTER TABLE [dbo].[StoreItems]
ADD CONSTRAINT [FK_UserStoreItem]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStoreItem'
CREATE INDEX [IX_FK_UserStoreItem]
ON [dbo].[StoreItems]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [FK_UserItem]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserItem'
CREATE INDEX [IX_FK_UserItem]
ON [dbo].[Items]
    ([UserID]);
GO

-- Creating foreign key on [UserID] in table 'Stores'
ALTER TABLE [dbo].[Stores]
ADD CONSTRAINT [FK_UserStore]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserStore'
CREATE INDEX [IX_FK_UserStore]
ON [dbo].[Stores]
    ([UserID]);
GO

-- Creating foreign key on [Badges_ID] in table 'BadgeUser'
ALTER TABLE [dbo].[BadgeUser]
ADD CONSTRAINT [FK_BadgeUser_Badge]
    FOREIGN KEY ([Badges_ID])
    REFERENCES [dbo].[Badges]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_ID] in table 'BadgeUser'
ALTER TABLE [dbo].[BadgeUser]
ADD CONSTRAINT [FK_BadgeUser_User]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BadgeUser_User'
CREATE INDEX [IX_FK_BadgeUser_User]
ON [dbo].[BadgeUser]
    ([Users_ID]);
GO

-- Creating foreign key on [Users_ID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_ID] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Role]
    FOREIGN KEY ([Roles_ID])
    REFERENCES [dbo].[Roles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Role'
CREATE INDEX [IX_FK_UserRole_Role]
ON [dbo].[UserRole]
    ([Roles_ID]);
GO

-- Creating foreign key on [UserID] in table 'ShoppingLists'
ALTER TABLE [dbo].[ShoppingLists]
ADD CONSTRAINT [FK_UserShoppingList]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserShoppingList'
CREATE INDEX [IX_FK_UserShoppingList]
ON [dbo].[ShoppingLists]
    ([UserID]);
GO

-- Creating foreign key on [Items_ID] in table 'ItemShoppingList'
ALTER TABLE [dbo].[ItemShoppingList]
ADD CONSTRAINT [FK_ItemShoppingList_Item]
    FOREIGN KEY ([Items_ID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ShoppingLists_Id] in table 'ItemShoppingList'
ALTER TABLE [dbo].[ItemShoppingList]
ADD CONSTRAINT [FK_ItemShoppingList_ShoppingList]
    FOREIGN KEY ([ShoppingLists_Id])
    REFERENCES [dbo].[ShoppingLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ItemShoppingList_ShoppingList'
CREATE INDEX [IX_FK_ItemShoppingList_ShoppingList]
ON [dbo].[ItemShoppingList]
    ([ShoppingLists_Id]);
GO

-- Creating foreign key on [Categories_Id] in table 'CategoryItem'
ALTER TABLE [dbo].[CategoryItem]
ADD CONSTRAINT [FK_CategoryItem_Category]
    FOREIGN KEY ([Categories_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Items_ID] in table 'CategoryItem'
ALTER TABLE [dbo].[CategoryItem]
ADD CONSTRAINT [FK_CategoryItem_Item]
    FOREIGN KEY ([Items_ID])
    REFERENCES [dbo].[Items]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryItem_Item'
CREATE INDEX [IX_FK_CategoryItem_Item]
ON [dbo].[CategoryItem]
    ([Items_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------