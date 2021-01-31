
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/16/2020 12:37:16
-- Generated from EDMX file: C:\Users\Sava-PC\Desktop\SDA\Homework 2\tech prototype\tech-proto-tastes-of-macedonia\WebApplication7\WebApplication7\Models\Restaurants.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Restaurant-Finder-DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[mytable]', 'U') IS NOT NULL
    DROP TABLE [dbo].[mytable];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'mytables'
CREATE TABLE [dbo].[mytables] (
    [id] bigint  NOT NULL,
    [lon] decimal(18,15)  NOT NULL,
    [lat] decimal(18,15)  NOT NULL,
    [name] nvarchar(16)  NOT NULL,
    [cuisine] nvarchar(10)  NOT NULL,
    [opening_hours] nvarchar(33)  NULL,
    [website] nvarchar(33)  NULL,
    [phone] nvarchar(16)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'mytables'
ALTER TABLE [dbo].[mytables]
ADD CONSTRAINT [PK_mytables]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------