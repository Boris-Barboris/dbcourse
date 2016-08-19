
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/11/2014 14:07:35
-- Generated from EDMX file: C:\Coding\мгту\DB course\SkySharkPortal\WebPortal\DataBase\SkySharkDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SkySharkDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ReservationFlightDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_ReservationFlightDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_ReservationCancellation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CancellationSet] DROP CONSTRAINT [FK_ReservationCancellation];
GO
IF OBJECT_ID(N'[dbo].[FK_CancellationUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CancellationSet] DROP CONSTRAINT [FK_CancellationUser];
GO
IF OBJECT_ID(N'[dbo].[FK_PassengerReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReservationSet] DROP CONSTRAINT [FK_PassengerReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_UserReservation_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserReservation] DROP CONSTRAINT [FK_UserReservation_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserReservation_Reservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserReservation] DROP CONSTRAINT [FK_UserReservation_Reservation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[FlightDetailsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FlightDetailsSet];
GO
IF OBJECT_ID(N'[dbo].[ReservationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReservationSet];
GO
IF OBJECT_ID(N'[dbo].[CancellationSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CancellationSet];
GO
IF OBJECT_ID(N'[dbo].[PassengerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PassengerSet];
GO
IF OBJECT_ID(N'[dbo].[UserReservation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserReservation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Username] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Role] tinyint  NOT NULL,
    [passwordChanged] bit  NOT NULL,
    [EMail] varchar(50)  NOT NULL
);
GO

-- Creating table 'FlightDetailsSet'
CREATE TABLE [dbo].[FlightDetailsSet] (
    [FlightNo] varchar(20)  NOT NULL,
    [Origin] nvarchar(max)  NOT NULL,
    [Destination] nvarchar(max)  NOT NULL,
    [DepTime] datetime  NOT NULL,
    [ArrTime] datetime  NOT NULL,
    [AircraftType] nvarchar(max)  NOT NULL,
    [SeatsEco] smallint  NOT NULL,
    [SeatsBn] smallint  NOT NULL,
    [FareEco] decimal(19,4)  NOT NULL,
    [FareBn] decimal(19,4)  NOT NULL,
    [EcoFree] smallint  NOT NULL,
    [BnFree] smallint  NOT NULL,
    [FareCollected] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'ReservationSet'
CREATE TABLE [dbo].[ReservationSet] (
    [TicketNo] int IDENTITY(1,1) NOT NULL,
    [FlightNo] varchar(20)  NOT NULL,
    [ReservedBy] varchar(50)  NOT NULL,
    [ClassOfRes] tinyint  NOT NULL,
    [Fare] decimal(19,4)  NOT NULL,
    [DateOfRes] datetime  NOT NULL,
    [Status] tinyint  NOT NULL,
    [EMail] varchar(50)  NULL
);
GO

-- Creating table 'CancellationSet'
CREATE TABLE [dbo].[CancellationSet] (
    [TicketNo] int  NOT NULL,
    [Refund] decimal(19,4)  NOT NULL,
    [UserServiced] varchar(50)  NOT NULL,
    [CancelDate] datetime  NOT NULL,
    [Comment] nvarchar(max)  NULL
);
GO

-- Creating table 'PassengerSet'
CREATE TABLE [dbo].[PassengerSet] (
    [ID] varchar(50)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [FareCollected] decimal(19,4)  NOT NULL,
    [TotalTimesFlown] int  NOT NULL,
    [Discount] real  NOT NULL
);
GO

-- Creating table 'UserReservation'
CREATE TABLE [dbo].[UserReservation] (
    [UserReservation_Reservation_Username] varchar(50)  NOT NULL,
    [Reservation_TicketNo] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Username] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [FlightNo] in table 'FlightDetailsSet'
ALTER TABLE [dbo].[FlightDetailsSet]
ADD CONSTRAINT [PK_FlightDetailsSet]
    PRIMARY KEY CLUSTERED ([FlightNo] ASC);
GO

-- Creating primary key on [TicketNo] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [PK_ReservationSet]
    PRIMARY KEY CLUSTERED ([TicketNo] ASC);
GO

-- Creating primary key on [TicketNo] in table 'CancellationSet'
ALTER TABLE [dbo].[CancellationSet]
ADD CONSTRAINT [PK_CancellationSet]
    PRIMARY KEY CLUSTERED ([TicketNo] ASC);
GO

-- Creating primary key on [ID] in table 'PassengerSet'
ALTER TABLE [dbo].[PassengerSet]
ADD CONSTRAINT [PK_PassengerSet]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserReservation_Reservation_Username], [Reservation_TicketNo] in table 'UserReservation'
ALTER TABLE [dbo].[UserReservation]
ADD CONSTRAINT [PK_UserReservation]
    PRIMARY KEY CLUSTERED ([UserReservation_Reservation_Username], [Reservation_TicketNo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FlightNo] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_ReservationFlightDetails]
    FOREIGN KEY ([FlightNo])
    REFERENCES [dbo].[FlightDetailsSet]
        ([FlightNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReservationFlightDetails'
CREATE INDEX [IX_FK_ReservationFlightDetails]
ON [dbo].[ReservationSet]
    ([FlightNo]);
GO

-- Creating foreign key on [TicketNo] in table 'CancellationSet'
ALTER TABLE [dbo].[CancellationSet]
ADD CONSTRAINT [FK_ReservationCancellation]
    FOREIGN KEY ([TicketNo])
    REFERENCES [dbo].[ReservationSet]
        ([TicketNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserServiced] in table 'CancellationSet'
ALTER TABLE [dbo].[CancellationSet]
ADD CONSTRAINT [FK_CancellationUser]
    FOREIGN KEY ([UserServiced])
    REFERENCES [dbo].[UserSet]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CancellationUser'
CREATE INDEX [IX_FK_CancellationUser]
ON [dbo].[CancellationSet]
    ([UserServiced]);
GO

-- Creating foreign key on [ReservedBy] in table 'ReservationSet'
ALTER TABLE [dbo].[ReservationSet]
ADD CONSTRAINT [FK_PassengerReservation]
    FOREIGN KEY ([ReservedBy])
    REFERENCES [dbo].[PassengerSet]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PassengerReservation'
CREATE INDEX [IX_FK_PassengerReservation]
ON [dbo].[ReservationSet]
    ([ReservedBy]);
GO

-- Creating foreign key on [UserReservation_Reservation_Username] in table 'UserReservation'
ALTER TABLE [dbo].[UserReservation]
ADD CONSTRAINT [FK_UserReservation_User]
    FOREIGN KEY ([UserReservation_Reservation_Username])
    REFERENCES [dbo].[UserSet]
        ([Username])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Reservation_TicketNo] in table 'UserReservation'
ALTER TABLE [dbo].[UserReservation]
ADD CONSTRAINT [FK_UserReservation_Reservation]
    FOREIGN KEY ([Reservation_TicketNo])
    REFERENCES [dbo].[ReservationSet]
        ([TicketNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserReservation_Reservation'
CREATE INDEX [IX_FK_UserReservation_Reservation]
ON [dbo].[UserReservation]
    ([Reservation_TicketNo]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------