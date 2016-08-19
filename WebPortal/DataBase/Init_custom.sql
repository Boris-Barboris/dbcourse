-- Manually written to enforce logic constraints


SET QUOTED_IDENTIFIER OFF;
GO
USE [SkySharkDb];
GO

-- default admin
INSERT INTO [dbo].[UserSet] (Username, Password, Role, passwordChanged, EMail)
VALUES ('admin', '1234', 2, 1, 'superiskander@mail.ru');
GO

-- Role must be existing
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [RoleConstraint_UserSet]
CHECK ([Role] > 0 AND [Role] < 5);
GO

-- bulk insert
USE [SkySharkDb]
GO

BULK INSERT [dbo].[UserSet]
FROM 'C:\Coding\мгту\DB course\SkySharkPortal\WebPortal\DataBase\bulk_data\UserSet.txt'
WITH (
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '\r\n'
);
GO

BULK INSERT [dbo].[UserSet]
FROM 'C:\Coding\мгту\DB course\SkySharkPortal\WebPortal\DataBase\bulk_data'
WITH (DATAFILETYPE='char');
GO

