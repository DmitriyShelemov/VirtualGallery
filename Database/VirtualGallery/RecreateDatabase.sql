IF EXISTS (SELECT * FROM sys.sysdatabases WHERE [name] = N'$(DBNAME)')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'$(DBNAME)'
	USE [master]
	ALTER DATABASE [$(DBNAME)] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE [$(DBNAME)]
END

CREATE DATABASE [$(DBNAME)] 
GO