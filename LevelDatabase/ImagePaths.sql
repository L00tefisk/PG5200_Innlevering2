CREATE TABLE [dbo].[ImagePaths]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Path] VARCHAR(128) NOT NULL, 
    [Description] VARCHAR(128) NULL 
)
