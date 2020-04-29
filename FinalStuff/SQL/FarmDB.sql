/* Check whether the database exists and delete if so */

IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases 
		  WHERE name = 'FarmDB')
BEGIN
	DROP DATABASE [FarmDB]
	print '' print '*** dropping FarmDB'
END
GO

print '' print '*** creating FarmDB'
GO
CREATE DATABASE [FarmDB]
GO

print '' print '*** using FarmDB'
GO
USE [FarmDB]
GO




print '' print '*** creating table Users'
GO
CREATE TABLE [dbo].[Users](
	[UserID]		[int]IDENTITY(100,1)	NOT NULL,
	[FirstName]		[nvarchar](50)			NOT NULL,
	[LastName]		[nvarchar](50)			NOT NULL,
	[PhoneNumber]	[nvarchar](11)			NOT NULL,
	[Email]			[nvarchar](250)			NOT NULL,	
	[PasswordHash][nvarchar](100)			NOT NULL DEFAULT
	'9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]			[bit]				NOT NULL DEFAULT 1,
	CONSTRAINT [pk_Users_UserID] 
		PRIMARY KEY([UserID] ASC)
)
print '' print '*** adding index for LastName on the Users table'
GO
CREATE NONCLUSTERED INDEX [ix_Users_LastName]
	on [Users] ([LastName] ASC)

GO

print '' print '*** adding index for PhoneNumber on Users table'
GO
CREATE NONCLUSTERED INDEX [ix_Users_PhoneNumber]
	on [Users] ([PhoneNumber] ASC)
GO

print '' print '*** adding index for Email on Users table'
GO
CREATE NONCLUSTERED INDEX [ix_Users_Email]
	on [Users] ([Email] ASC)
GO

print '' print '*** adding index for Active on Users table'
GO
CREATE NONCLUSTERED INDEX [ix_Users_Active]
	on [Users] ([Active] ASC)
GO

print '' print '*** creating sample Users records'
GO
INSERT INTO [dbo].[Users]
([FirstName],[LastName],[PhoneNumber],[Email])
VALUES
('Admin','admin','5555555556','admin@farmco.com'),
('Jack','Jackson','5555555555','jack@farmco.com'),
('Landy','McOwnerson','1112124141','landy@farmco.com'),
('Fred','Durst','1111111111', 'fred@farmco.com'),
('Jenny','Fredson','2222222222', 'jenny@farmco.com'),
('Mac','Mattson','33333333333', 'mac@farmco.com'),
('Melony','Fredson','2222222222', 'melon@farmco.com'),
('Eh','Fredson','2222222222', 'eh@farmco.com')
GO

print '' print '*** creating Role Table'
GO
CREATE TABLE [dbo].[Role](
		[RoleID] 	[nvarchar](50) 			NOT NULL,
		[Description]	[nvarchar](150)		NULL,
		CONSTRAINT [pk_Role_RoleID] PRIMARY KEY([RoleID] ASC)
)
GO

print '' print '*** creating sample Roles records'
GO
INSERT INTO [dbo].[Role]
([RoleID],[Description])
VALUES
('Admin',"Company Admin"),
('LandOwner',"Person who owns land that the company farms"),
('Manager',"Manager of the company"),
('Employee',"Basic employee for the company"),
('Mechanic',"perosn who works on companies machines")
GO

print '' print '*** creating UserRole table'
GO
CREATE TABLE [dbo].[UserRole](
	[UserID]		[int]					NOT NULL,
	[RoleID]		[nvarchar](50)			NOT NULL,
	CONSTRAINT [pk_UserID_RoleID] 
		PRIMARY KEY([UserID] ASC, [RoleID] ASC),
	CONSTRAINT [fk_role_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_role_roleID] FOREIGN KEY([RoleID])
		REFERENCES [Role]([RoleID]) ON UPDATE CASCADE
)
GO

print '' print '*** creating sample UserRoles records'
GO
INSERT INTO [dbo].[UserRole]
([UserID],[RoleID])
VALUES
(100,"Admin"),
(101,"LandOwner"),
(102,"LandOwner"),
(103,"Manager"),
(104,"Employee"),
(105,"Mechanic"),
(106,"Employee"),
(107,"Employee")
GO


print '' print '*** creating Farm Table'
GO

CREATE TABLE [dbo].[Farm](
	[FarmID]		[nvarchar](50)		 	NOT NULL,
	[UserID]		[int]					NOT NULL,/*only if a land owner user*/
	[Address]		[nvarchar](500)			NOT NULL,
	[City]			[nvarchar](500)			NOT NULL,
	[State]	[nvarchar](11)				NOT NULL,
	[ZipCode][nvarchar](100)				NOT NULL,	
	[Active]		[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_FarmID] PRIMARY KEY([FarmID] ASC),
	CONSTRAINT [fk_Farm_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID]) ON UPDATE CASCADE
)
GO

print '' print '*** adding index for FarmState on Farm table'
GO
CREATE NONCLUSTERED INDEX [ix_FarmState]
	on [Farm] ([State] ASC)
GO

print '' print '*** adding index for zip code on Farm table'
GO
CREATE NONCLUSTERED INDEX [ix_ZipCode]
	on [Farm] ([ZipCode] ASC)
GO
print '' print '*** creating sample farm records'
GO
INSERT INTO [dbo].[Farm]
([FarmID],[UserID],[Address],[City],[State],[ZipCode])
VALUES
("TheFarm1",101,"111 place ave", "Cedar Rapids","Iowa","52404"),
("TheFarm2",101,"152 place ave", "Cedar Rapids","Iowa","52404")
GO


print '' print '*** creating Crops Table'
GO

CREATE TABLE [dbo].[Crops](
	[CropID]		[nvarchar](50)			NOT NULL,
	[SeedNumber]	[nvarchar](10)			NOT NULL,
	[Description]	[nvarchar](200)			NULL,
	[PricePerBag]	[money]					NOT NULL,
	CONSTRAINT [pk_CropID] PRIMARY KEY([CropID] ASC),
)
GO

print '' print '*** adding index for seedNumber on Crop table'
GO
CREATE NONCLUSTERED INDEX [ix_SeedNumb]
	on [Crops] ([SeedNumber] ASC)
GO

print '' print '*** adding index for PricePerBag on Crop table'
GO
CREATE NONCLUSTERED INDEX [ix_PricePerBag]
	on [Crops] ([PricePerBag] ASC)
GO

print '' print '*** creating sample crop records'
GO
INSERT INTO [dbo].[Crops]
([CropID],[SeedNumber],[Description],[PricePerBag])
VALUES
("NoCrop","NA","No Current Crops", 00.00 ),
("Corn","P9492AM","this is corn with high yeild potential and high resistance", 75.00 ),
("SoyBeans","P18A98X","this soybean seed good", 50.00)
GO


print '' print '*** creating FarmField Table'
GO

CREATE TABLE [dbo].[FarmField](
	[FarmFieldID]	[nvarchar](50)		NOT NULL,	
	[CropID]		[nvarchar](50)			NULL,
	[FarmID]		[nvarchar](50)		 	NOT NULL,
	[Acres]			[int]					NOT NULL,
	[PastYield]		[int]					NULL,
	[CurrentYield]	[int]					NULL,
	[PlantOnDate]	[date]					NULL,
	[HarvestDate]	[date]					NULL,
	[LastSprayedOn]	[date]					NULL,
	CONSTRAINT [pk_FarmFieldID] PRIMARY KEY([FarmFieldID] ASC),
	CONSTRAINT [fk_FarmField_CropID] FOREIGN KEY([CropID])
		REFERENCES [Crops]([CropID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_FarmField_FarmID] FOREIGN KEY([FarmID])
		REFERENCES [Farm]([FarmID]) ON UPDATE CASCADE,		
)
GO

print '' print '*** adding index for LastSprayedOn on Crop table'
GO
CREATE NONCLUSTERED INDEX [ix_LastSprayedOn]
	on [FarmField] ([LastSprayedOn] ASC)
GO

print '' print '*** creating sample farmfield records'
GO
INSERT INTO [dbo].[FarmField]
([FarmFieldID],[CropID],[FarmID],[Acres],[PastYield],[CurrentYield]
			  ,[PlantOnDate],[HarvestDate],[LastSprayedOn])
VALUES
("Farm1Field1","Corn","TheFarm1",50,75,80,'5/15/2019','10/20/2019', '8/20/2019'),
("Farm1Field2","Corn","TheFarm1",30,40,55,'5/13/2019','10/21/2019', '8/20/2019'),
("Farm2Field1","SoyBeans", "TheFarm2",80,65,67,'5/8/2019','10/30/2019', '8/21/2019'),
("Farm2Field2","NoCrop","TheFarm2",10,20,null,null,null,null)
GO


print '' print '*** creating MachineType table'
GO
CREATE TABLE [dbo].[MachineType](
	[MachineTypeID]	[nvarchar](50)			NOT NULL,
	[Description]	[nvarchar](150)			NULL,
	CONSTRAINT [pk_MachineType_MachineTypeID] 
		PRIMARY KEY([MachineTypeID] ASC)	
)
GO
print '' print '*** creating sample MachineType records'
GO
INSERT INTO [dbo].[MachineType]
([MachineTypeID],[Description])
VALUES
("Tractor","Standard Tractor"),
("Combine","The Bull from cars ya know"),
("Cultivator","standard Cultivator"),
("Planter","standard 12 row planter")
GO

print '' print '*** creating MachineStatus table'
GO
CREATE TABLE [dbo].[MachineStatus](
	[MachineStatusID]	[nvarchar](50)			NOT NULL,
	[Description]	[nvarchar](150)				NULL,
	CONSTRAINT [pk_MachineStatus_MachineStatusID] 
		PRIMARY KEY([MachineStatusID] ASC)	
)
GO

print '' print '*** creating sample MachineStatus records'
GO
INSERT INTO [dbo].[MachineStatus]
([MachineStatusID],[Description])
VALUES
("On Standby","Machine ready for use"),
("In Use","Machine is in use"),
("In Maintanence","Machine is in the maintanence shop")
GO

print '' print '*** creating Machine table'
GO
CREATE TABLE [dbo].[Machine](
	[MachineID]		[nvarchar](50)				NOT NULL,/* This is a serial number for the Machine */
	[Make]			[nvarchar](50)				NOT NULL,
	[Model]			[nvarchar](50)				NOT NULL,
	[MachineTypeID]	[nvarchar](50)				NOT NULL,
	[MachineStatusID][nvarchar](50)				NOT NULL,
	[Hours]			[int]						NOT NULL,
	[Active]		[bit]						NOT NULL  DEFAULT 1,
	CONSTRAINT [pk_Machine_MachineID] 
		PRIMARY KEY([MachineID] ASC),
	CONSTRAINT [fk_Machine_MachineTypeID] FOREIGN KEY([MachineTypeID])
		REFERENCES [MachineType]([MachineTypeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_Machine_MachineStatusID] FOREIGN KEY([MachineStatusID])
		REFERENCES [MachineStatus]([MachineStatusID]) ON UPDATE CASCADE
)
GO

print '' print '*** creating sample Machine records'
GO
INSERT INTO [dbo].[Machine]
([MachineID],[Make],[Model],[MachineTypeID],[MachineStatusID],[Hours])
VALUES
('WqVfpYrt','John Deere','7R 330',"Tractor","On Standby",80),
('K9GnvLmu','John Deere','7R 330',"Tractor","In Use",75),
('dTfHcgzE','John Deere','T670',"Combine","In Maintanence",100),
('NKV6vtaY','John Deere','2230FH',"Cultivator","In Use",50),
('uaxugfzy','John Deere','DR12',"Planter","In Maintanence",50)
GO


print '' print '*** adding index for Make on Machine table'
GO
CREATE NONCLUSTERED INDEX [ix_Make]
	on [Machine] ([Make] ASC)
GO

print '' print '*** adding index for Model on Machine table'
GO
CREATE NONCLUSTERED INDEX [ix_Model]
	on [Machine] ([Model] ASC)
GO

print '' print '*** adding index for Active on Machine table'
GO
CREATE NONCLUSTERED INDEX [ix_Machine_Active]
	on [Machine] ([Active] ASC)
GO


print '' print '*** creating UsageType table'
GO
CREATE TABLE [dbo].[UsageType](
	[UsageTypeID]		[nvarchar](50)			NOT NULL,
	[Description]		[nvarchar](150)			NULL,
	CONSTRAINT [pk_UsageType_UsageTypeID] 
		PRIMARY KEY([UsageTypeID] ASC)
)

print '' print '*** creating sample UsageType records'
GO
INSERT INTO [dbo].[UsageType]
([UsageTypeID],[Description])
VALUES
("Cultivate","Cultivate selected field"),
("Plant","Plant selected field"),
("Move Grain","Move grain from Combine at selected field"),
("Harvest","Harvest selected field")
GO

print '' print '*** creating MachineFieldUse table'
GO
CREATE TABLE [dbo].[MachineFieldUse](
	[MachineFieldUseID] [int]IDENTITY(100,1)	NOT NULL,
	[FarmFieldID]		[nvarchar](50)			NOT NULL,	
	[UsageTypeID]		[nvarchar](50)			NOT NULL,
	[MachineID]			[nvarchar](50)			NOT NULL,
	[UserID]			[int]					NOT NULL,
	[Description]		[nvarchar](150)			NOT NULL,
	[Completed]		[bit]						NOT NULL  DEFAULT 0,
	CONSTRAINT [pk_MachineFieldUse_MachineFieldUseID] 
		PRIMARY KEY([MachineFieldUseID] ASC),
	CONSTRAINT [fk_MachineFieldUse_FarmFieldID] FOREIGN KEY([FarmFieldID])
		REFERENCES [FarmField]([FarmFieldID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_MachineFieldUse_UsageTypeID] FOREIGN KEY([UsageTypeID])
		REFERENCES [UsageType]([UsageTypeID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_MachineFieldUse_MachineID] FOREIGN KEY([MachineID])
		REFERENCES [Machine]([MachineID]) ON UPDATE CASCADE,
	CONSTRAINT [fk_MachineFieldUse_UserId] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
)
GO

print '' print '*** creating sample MachineFieldUse records'
GO
INSERT INTO [dbo].[MachineFieldUse]
([FarmFieldID],[UsageTypeID],[MachineID],[UserID],[Description])
VALUES
("Farm2Field2",'Cultivate',"K9GnvLmu",104,"Jenny is Cultivateing TheFarm2 Field2"),
("Farm2Field2",'Cultivate',"NKV6vtaY",104,"Jenny is Cultivateing TheFarm2 Field2")
GO



/*Stored procedures below */
print '' print '*** creating sp_authenticate_user'
GO
CREATE PROCEDURE [sp_authenticate_user](
	@Email	[nvarchar](250),
	@PasswordHash [nvarchar](100)
)
AS
BEGIN
	SELECT  COUNT([UserID])
	FROM	[dbo].[Users]
	WHERE	[Email] = LOWER(@Email)
	AND		[PasswordHash] = @PasswordHash
	AND		[Active] = 1
END
GO

print '' print '*** creating sp_select_users_by_active'
GO
CREATE PROCEDURE [sp_select_users_by_active](
	@Active		[bit]
)
AS
BEGIN
	SELECT  [UserID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
	FROM	[dbo].[Users]
	WHERE	[Active] = @Active
END
GO

print '' print '*** creating sp_update_email'
GO

CREATE PROCEDURE [sp_update_email](
	@OldEmail			[nvarchar](250),
	@NewEmail			[nvarchar](250),
	@PasswordHash		[nvarchar](100)
)
AS
BEGIN
	UPDATE [dbo].[Users]
	SET [Email] = LOWER(@NewEmail)
	WHERE [Email] = LOWER(@OldEmail)
	AND [PasswordHash] = @PasswordHash
	AND [Active] = 1
END
GO



print '' print '*** creating sp_insert_user'
GO
CREATE PROCEDURE [sp_insert_user](
	@FirstName 	[nvarchar](50),
	@LastName	[nvarchar] (50),
	@PhoneNumber [nvarchar] (11),
	@Email		[nvarchar] (250)
)
AS
BEGIN	
	INSERT INTO [dbo].[Users]
		([FirstName],[LastName], [PhoneNumber],[Email])
		VALUES
			(@FirstName, @LastName, @PhoneNumber, LOWER(@Email))
			RETURN SCOPE_IDENTITY()
END
GO

print '' print '*** creating sp_select_user_by_email'
GO

CREATE PROCEDURE [sp_select_user_by_email](
	@Email			[nvarchar](250)
	
)
AS
BEGIN
	SELECT [UserID], [FirstName], [LastName],[PhoneNumber]
	FROM  [Users]
	WHERE [Email] = @Email
END
GO



print '' print '*** creating sp_update_password'
GO
CREATE PROCEDURE [sp_update_password](
	@UserID               [int],
    @OldPasswordHash		[nvarchar](100),
    @NewPasswordHash        [nvarchar](100)
    
)
AS
BEGIN
	UPDATE [dbo].[Users]
	SET [PasswordHash] = @NewPasswordHash
	WHERE [UserID] = @UserID
	AND [PasswordHash] = @OldPasswordHash
	AND [Active] = 1
    RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_update_user'
GO
CREATE PROCEDURE [sp_update_user](
	@UserID [int],
	
	@NewFirstName  [nvarchar](50),
	@NewLastName	[nvarchar](50),
	@NewPhoneNumber	[nvarchar](11),
	@NewEmail		[nvarchar](250),
	
	@OldFirstName  [nvarchar](50),
	@OldLastName	[nvarchar](50),
	@OldPhoneNumber	[nvarchar](11),
	@OldEmail		[nvarchar](250)
)
AS
BEGIN
	UPDATE [dbo].[Users]
		SET [FirstName] = @NewFirstName,
			[LastName] = @NewLastName,
			[PhoneNumber] = @NewPhoneNumber,
			[Email] = @NewEmail
		WHERE [UserID] = @UserID
		AND	[FirstName] = @OldFirstName
		AND [LastName] = @OldLastName
		AND[PhoneNumber] = @OldPhoneNumber
		AND [Email] = @OldEmail
		
		RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_deactivate_user'
GO
CREATE PROCEDURE [sp_deactivate_user](
		@UserID [int]
			
)
AS
BEGIN
	UPDATE [dbo].[Users]
		SET [Active]= 0
		WHERE [UserID] = @UserID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_reactivate_user'
GO
CREATE PROCEDURE [sp_reactivate_user](
		@UserID [int]
)
AS
BEGIN
UPDATE [dbo].[Users]
		SET [Active]= 1
		WHERE [UserID] = @UserID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_select_roles_by_userID'
GO

CREATE PROCEDURE [sp_select_roles_by_userID](
	@UserID			[int]
	
)
AS
BEGIN
	SELECT [RoleID]
	FROM  [UserRole]
	WHERE [UserID] = @UserID
END
GO

print '' print '*** creating sp_select_user_by_id'
GO
CREATE PROCEDURE [sp_select_user_by_id](
	@UserID [int]
)
AS
BEGIN
	SELECT [UserID],[FirstName],[LastName],[PhoneNumber],[Email],[Active]
	FROM	[dbo].[Users]
	WHERE [UserID] = @UserID
	
END
GO

print '' print '*** creating sp_insert_user_role'
GO
CREATE PROCEDURE [sp_insert_user_role](
	@UserID [int],
	@RoleID[nvarChar](50)
)
AS
BEGIN
	INSERT INTO [dbo].[UserRole]
		([UserID],[RoleID])
		VALUES
		(@UserID,@RoleID)
	
END
GO

print '' print '*** creating sp_delete_user_role'
GO
CREATE PROCEDURE [sp_delete_user_role](
	@UserID [int],
	@RoleID[nvarchar](50)
)
AS
BEGIN
	DELETE From [dbo].[UserRole]
	WHERE [UserID]= @UserID
	AND [RoleID] = @RoleID
	
END
GO

print '' print '*** creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[Role]
	ORDER BY [RoleID]
END
GO

print '' print '*** creating sp_select_users_by_role'
GO
CREATE PROCEDURE [sp_select_users_by_role](
	@RoleID		[nvarchar](50)
)
AS
BEGIN
	SELECT  [UserID]
	FROM	[dbo].[UserRole]
	WHERE	[RoleID] = @RoleID
	ORDER BY [UserID]
END
GO


print '' print '*** creating sp_insert_farm'
GO
CREATE PROCEDURE [sp_insert_farm](
	@FarmID [nvarChar](50),
	@UserID[int],/*Landowners only use a drop down list with only landowners*/
	@Address[nvarchar](500),
	@City[nvarchar](500),
	@State [nvarchar](11),
	@ZipCode[nvarchar](100)
)
AS
BEGIN
	INSERT INTO [dbo].[Farm]
		([FarmID],[UserID],[Address],[City],[State],[ZipCode])
		VALUES
		(@FarmID,@UserID,@Address,@City,@State,@ZipCode)
		RETURN SCOPE_IDENTITY()
	
END
GO

print '' print '*** creating sp_select_farms_by_active'
GO
CREATE PROCEDURE [sp_select_farms_by_active](
	@Active		[bit]
)
AS
BEGIN
	SELECT  [FarmID],[UserID],[Address],[City],[State],[ZipCode],[Active]
	FROM	[dbo].[Farm]
	WHERE	[Active] = @Active
END
GO

print '' print '*** creating sp_select_farms_by_FarmID'
GO
CREATE PROCEDURE [sp_select_farms_by_FarmID](
	@FarmID		[nvarChar](50)
)
AS
BEGIN
	SELECT  [FarmID],[UserID],[Address],[City],[State],[ZipCode],[Active]
	FROM	[dbo].[Farm]
	WHERE	[FarmID] = @FarmID
END
GO

	
	print '' print '*** creating sp_update_farm'
GO
CREATE PROCEDURE [sp_update_farm](
	@FarmID [nvarChar](50),
	
	@NewUserID  	[int],
	@NewAddress		[nvarchar](500),
	@NewCity		[nvarchar](500),
	@NewState		[nvarchar](11),
	@NewZipCode		[nvarchar](100),
	
	@OldUserID  	[int],
	@OldAddress		[nvarchar](500),
	@OldCity		[nvarchar](500),
	@OldState		[nvarchar](11),
	@OldZipCode		[nvarchar](100)
)
AS
BEGIN
	UPDATE [dbo].[Farm]
		SET [UserID] = @NewUserID,
			[Address] = @NewAddress,
			[City] = @NewCity,
			[State] = @NewState,
			[ZipCode] = @NewZipCode
		WHERE [FarmID] = @FarmID
		AND	[UserID] = @OldUserID
		AND [Address] = @OldAddress
		AND[City] = @OldCity
		AND [State] = @OldState
		AND[ZipCode] = @OldZipCode
		
		
		RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_deactivate_farm'
GO
CREATE PROCEDURE [sp_deactivate_farm](
		@FarmID [nvarchar](50)
			
)
AS
BEGIN
	UPDATE [dbo].[Farm]
		SET [Active]= 0
		WHERE [FarmId] = @FarmID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_reactivate_farm'
GO
CREATE PROCEDURE [sp_reactivate_farm](
		@FarmID [nvarchar](50)
			
)
AS
BEGIN
	UPDATE [dbo].[Farm]
		SET [Active]= 1
		WHERE [FarmId] = @FarmID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_select_fields_by_farmID'
GO

CREATE PROCEDURE [sp_select_fields_by_farmID](
	@FarmID			[nvarChar](50)
	
)
AS
BEGIN
	SELECT [FarmFieldID],[CropID],[Acres],[PastYield],
				[CurrentYield],[PlantOnDate],[HarvestDate],[LastSprayedOn]
	FROM  [FarmField]
	WHERE [FarmID] = @FarmID
END
GO

print '' print '*** creating sp_select_field_by_farmfieldID'
GO

CREATE PROCEDURE [sp_select_field_by_farmfieldID](
	@FarmFieldID			[nvarChar](50)
	
)
AS
BEGIN
	SELECT [CropID],[FarmID],[Acres],[PastYield],[CurrentYield],[PlantOnDate],[HarvestDate],[LastSprayedOn]
	FROM  [FarmField]
	WHERE [FarmFieldID] = @FarmFieldID
END
GO

print '' print '*** creating sp_select_all_fields'
GO

CREATE PROCEDURE [sp_select_all_fields]
AS
BEGIN
	SELECT [FarmFieldID]
	FROM  [FarmField]
	ORDER BY [FarmFieldID]
END
GO

print '' print '*** creating sp_insert_farm_field'
GO
CREATE PROCEDURE [sp_insert_farm_field](
	@FarmFieldID			[nvarChar](50),
	@CropID [nvarchar](50),
	@FarmID[nvarChar](50),
	@Acres[int],
	@PastYield[int],
	@CurrentYield[int],
	@PlantOnDate[date],
	@HarvestDate[date],
	@LastSprayedOn[date]
)
AS
BEGIN
	INSERT INTO [dbo].[FarmField]
		([FarmFieldID],[CropID],[FarmID],[Acres],[PastYield],[CurrentYield],[PlantOnDate],[HarvestDate],[LastSprayedOn])
		VALUES
		(@FarmFieldID,@CropID,@FarmID,@Acres,@PastYield,@CurrentYield,@PlantOnDate,@HarvestDate,@LastSprayedOn)
	
END
GO

print '' print '*** creating sp_update_farm_field'
GO
CREATE PROCEDURE [sp_update_farm_field](
	@FarmFieldID[nvarChar](50),
	
	@NewFarmFieldID[nvarChar](50),
	@NewCropID [nvarchar](50),
	@NewFarmID[nvarChar](50),
	@NewAcres[int],
	@NewPastYield[int],
	@NewCurrentYield[int],
	@NewPlantOnDate[date],
	@NewHarvestDate[date],
	@NewLastSprayedOn[date],
	
	@OldCropID [nvarchar](50),
	@OldFarmID[nvarChar](50),
	@OldAcres[int],
	@OldPastYield[int],
	@OldCurrentYield[int],
	@OldPlantOnDate[date],
	@OldHarvestDate[date],
	@OldLastSprayedOn[date]
)
AS
BEGIN
	UPDATE [dbo].[FarmField]
		SET [FarmFieldID] = @NewFarmFieldID,
			[CropID] = @NewCropID,
			[FarmID] = @NewFarmID,
			[Acres] = @NewAcres,
			[PastYield] = @NewPastYield,
			[CurrentYield] = @NewCurrentYield,
			[PlantOnDate] = @NewPlantOnDate,
			[HarvestDate] = @NewHarvestDate,
			[LastSprayedOn] = @NewLastSprayedOn
			
		WHERE [FarmFieldID] = @FarmFieldID
		AND	[CropID] = @OldCropID
		AND [FarmID] = @OldFarmID
		AND[Acres] = @OldAcres
		AND [PastYield] = @OldPastYield
		AND[CurrentYield] = @OldCurrentYield
		AND[PlantOnDate] = @OldPlantOnDate
		AND [HarvestDate] = @OldHarvestDate
		AND[LastSprayedOn] = @OldLastSprayedOn
		RETURN @@ROWCOUNT
		
END
GO
	 


print '' print '*** creating sp_select_crops'
GO
CREATE PROCEDURE [sp_select_crops]
AS
BEGIN
	SELECT  [CropID],[SeedNumber],[Description],[PricePerBag]
	FROM	[dbo].[Crops]	
END
GO

print '' print '*** creating sp_select_crop_by_id'
GO
CREATE PROCEDURE [sp_select_crop_by_id](
	@CropID [nvarChar](50)
)
AS
BEGIN
	SELECT  [CropID],[SeedNumber],[Description],[PricePerBag]
	FROM	[dbo].[Crops]	
END
GO

print '' print '*** creating sp_insert_crop'
GO
CREATE PROCEDURE [sp_insert_crop](
	@CropID [nvarChar](50),
	@SeedNumber[nvarchar](10),
	@Description[nvarchar](200),
	@PricePerBag[money]
	
)
AS
BEGIN
	INSERT INTO [dbo].[Crops]
		([CropID],[SeedNumber],[Description],[PricePerBag])
		VALUES
		(@CropID,@SeedNumber,@Description,@PricePerBag)
		RETURN SCOPE_IDENTITY()
	
END
GO

	print '' print '*** creating sp_update_crop'
GO
CREATE PROCEDURE [sp_update_crop](
	@CropID [nvarChar](50),
	
	@NewSeedNumber[nvarchar](10),
	@NewDescription[nvarchar](200),
	@NewPricePerBag[money],
	
	@OldSeedNumber[nvarchar](10),
	@OldDescription[nvarchar](200),
	@OldPricePerBag[money]
)
AS
BEGIN
	UPDATE [dbo].[Crops]
		SET [SeedNumber] = @NewSeedNumber,
			[Description] = @NewDescription,
			[PricePerBag] = @NewPricePerBag			
		WHERE [CropID] = @CropID
		AND	[SeedNumber] = @OldSeedNumber
		AND [Description] = @OldDescription
		AND[PricePerBag] = @OldPricePerBag
		
		RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_delete_crop'
GO
CREATE PROCEDURE [sp_delete_crop](
		@CropID [nvarchar](50)
			
)
AS
BEGIN
	DELETE FROM [dbo].[Crops]
	WHERE [CropID] = @CropID
	RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_select_machine_by_active'
GO
CREATE PROCEDURE [sp_select_machine_by_active](
	@Active		[bit]
)
AS
BEGIN
	SELECT  [MachineID],[Make],[Model],[MachineTypeID],[MachineStatusID],[Hours],[Active]
	FROM	[dbo].[Machine]
	WHERE	[Active] = @Active
END
GO

print '' print '*** creating sp_select_machine_by_ID'
GO
CREATE PROCEDURE [sp_select_machine_by_ID](
	@MachineID		[nvarChar](50)
)
AS
BEGIN
	SELECT  [MachineID],[Make],[Model],[MachineTypeID],[MachineStatusID],[Hours],[Active]
	FROM	[dbo].[Machine]
	WHERE	[MachineID] = @MachineID
END
GO


print '' print '*** creating sp_insert_machine'
GO
CREATE PROCEDURE [sp_insert_machine](
	@MachineID [nvarChar](50),
	@Make[nvarchar](50),
	@Model[nvarchar](50),
	@MachineTypeID[nvarchar](50),
	@MachineStatusID [nvarchar](50),
	@Hours[int]
)
AS
BEGIN
	INSERT INTO [dbo].[Machine]
		([MachineID],[Make],[Model],[MachineTypeID],[MachineStatusID],[Hours])
		VALUES
		(@MachineID,@Make,@Model,@MachineTypeID,@MachineStatusID,@Hours)
		RETURN SCOPE_IDENTITY()
	
END
GO

	print '' print '*** creating sp_update_machine'
GO
CREATE PROCEDURE [sp_update_machine](
	@MachineID [nvarChar](50),
	
	@NewMake[nvarchar](50),
	@NewModel[nvarchar](50),
	@NewMachineTypeID[nvarchar](50),
	@NewMachineStatusID [nvarchar](50),
	@NewHours[int],
	
	@OldMake[nvarchar](50),
	@OldModel[nvarchar](50),
	@OldMachineTypeID[nvarchar](50),
	@OldMachineStatusID [nvarchar](50),
	@OldHours[int]
)
AS
BEGIN
	UPDATE [dbo].[Machine]
		SET [Make] = @NewMake,
			[Model] = @NewModel,
			[MachineTypeID] = @NewMachineTypeID,
			[MachineStatusID] = @NewMachineStatusID,
			[Hours] = @NewHours
		WHERE [MachineID] = @MachineID
		AND	[Make] = @OldMake
		AND [Model] = @OldModel
		AND[MachineTypeID] = @OldMachineTypeID
		AND [MachineStatusID] = @OldMachineStatusID
		AND[Hours] = @OldHours
		
		
		RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_deactivate_machine'
GO
CREATE PROCEDURE [sp_deactivate_machine](
		@MachineID [nvarchar](50)
			
)
AS
BEGIN
	UPDATE [dbo].[Machine]
		SET [Active]= 0
		WHERE [MachineID] = @MachineID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_reactivate_machine'
GO
CREATE PROCEDURE [sp_reactivate_machine](
		@MachineID [nvarchar](50)
			
)
AS
BEGIN
	UPDATE [dbo].[Machine]
		SET [Active]= 1
		WHERE [MachineID] = @MachineID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_select_all_usages'
GO
CREATE PROCEDURE [sp_select_all_usages]
AS
BEGIN
	SELECT [UsageTypeID]
	FROM [dbo].[UsageType]
	ORDER BY [UsageTypeID]
END
GO

print '' print '*** creating sp_select_all_machine_status'
GO
CREATE PROCEDURE [sp_select_all_machine_status]
AS
BEGIN
	SELECT [MachineStatusID]
	FROM [dbo].[MachineStatus]
	ORDER BY [MachineStatusID]
END
GO

print '' print '*** creating sp_select_all_machine_types'
GO
CREATE PROCEDURE [sp_select_all_machine_types]
AS
BEGIN
	SELECT [MachineTypeID]
	FROM [dbo].[MachineType]
	ORDER BY [MachineTypeID]
END
GO

print '' print '*** creating sp_select_machine_field_use_by_complete'
GO
CREATE PROCEDURE [sp_select_machine_field_use_by_complete](
	@Completed		[bit]
)
AS
BEGIN
	SELECT  [MachineFieldUseID],[FarmFieldID],[UsageTypeID],[MachineID],[UserID],[Description],[Completed]
	FROM	[dbo].[MachineFieldUse]
	WHERE	[Completed] = @Completed
END
GO


print '' print '*** creating sp_select_machine_field_use_by_id'
GO
CREATE PROCEDURE [sp_select_machine_field_use_by_id](
	@MachineFieldUseID		[int]
)
AS
BEGIN
	SELECT  [MachineFieldUseID],[FarmFieldID],[UsageTypeID],[MachineID],[UserID],[Description],[Completed]
	FROM	[dbo].[MachineFieldUse]
	WHERE	[MachineFieldUseID] = @MachineFieldUseID
END
GO


print '' print '*** creating sp_insert_machine_field_use'
GO
CREATE PROCEDURE [sp_insert_machine_field_use](
	
	@FarmFieldID[nvarchar](50),
	@UsageTypeID[nvarchar](50),
	@MachineID[nvarchar](50),
	@UserID [int],
	@Description[nvarchar](150)
)
AS
BEGIN
	INSERT INTO [dbo].[MachineFieldUse]
		([FarmFieldID],[UsageTypeID],[MachineID],[UserID],[Description])
		VALUES
		(@FarmFieldID,@UsageTypeID,@MachineID,@UserID,@Description)
		RETURN SCOPE_IDENTITY()
	
END
GO

	print '' print '*** creating sp_update_machine_field_use'
GO
CREATE PROCEDURE [sp_update_machine_field_use](
	@MachineFieldUseID [int],
	
	@NewFarmFieldID[nvarchar](50),
	@NewUsageTypeID[nvarchar](50),
	@NewMachineID[nvarchar](50),
	@NewUserID [int],
	@NewDescription[nvarchar](150),
	
	@OldFarmFieldID[nvarchar](50),
	@OldUsageTypeID[nvarchar](50),
	@OldMachineID[nvarchar](50),
	@OldUserID [int],
	@OldDescription[nvarchar](150)
)
AS
BEGIN
	UPDATE [dbo].[MachineFieldUse]
		SET [FarmFieldID] = @NewFarmFieldID,
			[UsageTypeID] = @NewUsageTypeID,
			[MachineID] = @NewMachineID,
			[UserID] = @NewUserID,
			[Description] = @NewDescription
		WHERE [MachineFieldUseID] = @MachineFieldUseID
		AND	[FarmFieldID] = @OldFarmFieldID
		AND [UsageTypeID] = @OldUsageTypeID
		AND[MachineID] = @OldMachineID
		AND [UserID] = @OldUserID
		AND[Description] = @OldDescription	
		
		RETURN @@ROWCOUNT
END
GO

print '' print '*** creating sp_reopen_field_use'
GO
CREATE PROCEDURE [sp_reopen_field_use](
		@MachineFieldUseID [int]
			
)
AS
BEGIN
	UPDATE [dbo].[MachineFieldUse]
		SET [Completed]= 0
		WHERE [MachineFieldUseID] = @MachineFieldUseID
		
		RETURN @@ROWCOUNT
	
END
GO

print '' print '*** creating sp_complete_field_use'
GO
CREATE PROCEDURE [sp_complete_field_use](
		@MachineFieldUseID [int]
			
)
AS
BEGIN
	UPDATE [dbo].[MachineFieldUse]
		SET [Completed]= 1
		WHERE [MachineFieldUseID] = @MachineFieldUseID
		
		RETURN @@ROWCOUNT
	
END
GO





