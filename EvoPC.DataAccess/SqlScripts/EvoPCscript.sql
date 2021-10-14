IF (DB_ID(N'EvoPC') IS NULL)
	CREATE DATABASE EvoPC
GO

USE EvoPC
GO

IF OBJECT_ID ('Rol') IS NULL
	CREATE TABLE Rol
	(
	Id INT NOT NULL CONSTRAINT PK_Rol PRIMARY KEY,
	NumeRol NVARCHAR(255) NOT NULL,	
	)
GO

IF OBJECT_ID ('Users') IS NULL
	CREATE TABLE Users
	(
	Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_UserId DEFAULT NEWID(),
	UserName NVARCHAR(256) NOT NULL CONSTRAINT UK_UserName UNIQUE,
	[Password] NVARCHAR(256) NOT NULL,
	LastLogin DATETIME NULL,
	IsActiv BIT NOT NULL CONSTRAINT DF_IsActiv DEFAULT 1,
	RoleId INT NOT NULL,
	SurName NVARCHAR(256) NOT NULL,
	[Name] NVARCHAR(256) NOT NULL,
	Email NVARCHAR(500) NOT NULL,
	PhoneNumber NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_User PRIMARY KEY (Id)
	)
GO

IF OBJECT_ID ('SocketType') IS NULL
	CREATE TABLE SocketType
	(
	Id INT NOT NULL IDENTITY (1, 1) CONSTRAINT PK_SocketType PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL
	)
GO

IF OBJECT_ID ('SpecialTags') IS NULL
	CREATE TABLE SpecialTags
	(
	Id INT NOT NULL IDENTITY (1, 1) CONSTRAINT PK_SpecialTag PRIMARY KEY,
	[Name] NVARCHAR(255) NOT NULL
	)
GO

IF OBJECT_ID ('Procesor') IS NULL
	CREATE TABLE Procesor
	(
	Id INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_Procesor PRIMARY KEY,
	[Name] NVARCHAR(256) NOT NULL,
	[Description] NVARCHAR(2000) NOT NULL, 
	Pret NUMERIC(20, 2) NOT NULL,
	EsteInStoc BIT NOT NULL,
	ImagePath NVARCHAR(1000) NULL,
	SocketTypeId INT NOT NULL
	)
GO

IF OBJECT_ID ('ProcesorSpecialTags') IS NULL
	CREATE TABLE ProcesorSpecialTags
	(
	SpecialTagId INT NOT NULL,
	ProcesorId INT NOT NULL,
	CONSTRAINT PK_ProcesorSpecialTag PRIMARY KEY (SpecialTagId, ProcesorId)
	)
GO

IF OBJECT_ID ('Feedback') IS NULL 
	CREATE TABLE Feedback
	(
	Id INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_Feedback PRIMARY KEY,
	CommentTitle NVARCHAR(256) NOT NULL,
	Comment NVARCHAR(2000) NOT NULL,
	Rating INT NOT NULL,
	ProcesorId INT NOT NULL,
	UserId UNIQUEIDENTIFIER NULL
	)
GO

IF OBJECT_ID('Orders') IS NULL
	CREATE TABLE Orders
	(
	Id INT NOT NULL IDENTITY(1, 1) CONSTRAINT PK_Orders PRIMARY KEY,
	CustomerName NVARCHAR(256) NOT NULL,
	CustomerPhoneNumber NVARCHAR(256) NOT NULL,
	CustomerEmail NVARCHAR(256) NOT NULL,
	CustomerAddress NVARCHAR(500) NOT NULL,
	OrderDate DATETIME NOT NULL
	)
GO

IF OBJECT_ID('OrdersProcesor') IS NULL
	CREATE TABLE OrdersProcesor
	(
	ProcesorId INT NOT NULL,
	OrderId INT NOT NULL,
	CONSTRAINT PK_OrdersProcesor PRIMARY KEY (ProcesorId, OrderId)
	)
GO

IF OBJECT_ID('USP_CreateFK') IS NOT NULL
	DROP PROCEDURE USP_CreateFK
GO

CREATE PROCEDURE USP_CreateFK
@FKName NVARCHAR(512),
@ChildTableName NVARCHAR(256),
@ChildColumnName NVARCHAR(256),
@ParentTableName NVARCHAR(256),
@ParentColumnName NVARCHAR(256),
@CascadeDelete BIT = 0
AS
BEGIN
	DECLARE @sql NVARCHAR(4000)
	SET @sql =  'IF (OBJECT_ID(''' + @FKName +''', ''F'') IS NULL)' +
				'ALTER TABLE ' + @ChildTableName + ' ' +
				'ADD CONSTRAINT ' + @FKName + ' ' +
				'FOREIGN KEY (' + @ChildColumnName + ')' +
				'REFERENCES ' + @ParentTableName + '(' + @ParentColumnName + ')'
	IF (@CascadeDelete = 1)
		SET @sql = @sql + 'ON DELETE CASCADE '
    EXEC sp_executesql @sql
END
GO

EXEC USP_CreateFK 'FK_Users_Roles', 'Users', 'RoleId', 'Rol', 'Id'
GO

EXEC USP_CreateFK 'FK_Procesor_ProcesorTypes', 'Procesor', 'SocketTypeId', 'SocketType', 'Id'
GO

EXEC USP_CreateFK 'FK_ProcesorSpecialTags_SpecialTags', 'ProcesorSpecialTags', 'SpecialTagId', 'SpecialTags', 'Id'
GO

EXEC USP_CreateFK 'FK_ProcesorSpecialTags_Procesor', 'ProcesorSpecialTags', 'ProcesorId', 'Procesor', 'Id', 1
GO

EXEC USP_CreateFK 'FK_Feedback_Procesor', 'Feedback', 'ProcesorId', 'Procesor', 'Id'
GO

EXEC USP_CreateFK 'FK_Feedback_Users', 'Feedback', 'UserId', 'Users', 'Id'
GO

EXEC USP_CreateFK 'FK_OrdersProcesor_Orders', 'OrdersProcesor', 'OrderId', 'Orders', 'Id', 1
GO

EXEC USP_CreateFK 'FK_OrdersProcesor_Procesor', 'OrdersProcesor', 'ProcesorId', 'Procesor', 'Id'
GO

IF NOT EXISTS (SELECT 1 FROM Rol WHERE Id = 1)
INSERT INTO Rol (Id, NumeRol)
SELECT 1, 'SuperAdmin'
GO

IF NOT EXISTS (SELECT 1 FROM Rol WHERE Id = 2)
INSERT INTO Rol (Id, NumeRol)
SELECT 2, 'Admin'
GO

IF NOT EXISTS (SELECT 1 FROM Rol WHERE Id = 3)
INSERT INTO Rol(Id, NumeRol)
SELECT 3, 'Director'
GO

IF NOT EXISTS (SELECT 1 FROM Rol WHERE Id = 4)
INSERT INTO Rol (Id, NumeRol)
SELECT 4, 'SalesManager'
GO

IF NOT EXISTS (SELECT 1 FROM Rol WHERE Id = 5)
INSERT INTO Rol (Id, NumeRol)
SELECT 5, 'SalesAgent'
GO

DECLARE @UserId UNIQUEIDENTIFIER = '414D6C6F-4E20-46F7-848F-D3FCF0AF2A05'
IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
INSERT INTO Users(Id, UserName, [Password], RoleId, SurName, [Name], Email, PhoneNumber)
SELECT @UserId, 'Admin', 'YWRtaW4=', 1, 'Admin', 'Admin', 'admin@app.com', '0723045984'
GO


--INSERT INTO dbo.SocketType(Name)
--VALUES ('AM4');
--INSERT INTO dbo.SocketType(Name)
--VALUES ('LGA 2066');

--INSERT INTO Procesor(Name, Description,Pret,EsteInStoc,SocketTypeId)
--VALUES ('Ryzen 5 3600', 'Best Performance/Value', 300, 1,1);

--select * from SocketType;