@echo on
REM: This command file drops and creates database, creates database schema, creates stored procedures and insertes default data.
REM: Windows authentication is used to connect to SQL Server.
REM:
REM: Usage: CommandFilename [Server] [Database] 
REM: Example: RecreateDatabase.cmd "(local)\sql2008" "VirtualGallery" 

if '%1' == '' goto usage
if '%2' == '' goto usage

if '%1' == '/?' goto usage
if '%1' == '-?' goto usage
if '%1' == '?' goto usage
if '%1' == '/help' goto usage

sqlcmd -S %1 -d master -E -b -v DBNAME=%2 -i "RecreateDatabase.sql"
if %ERRORLEVEL% NEQ 0 goto errors

sqlcmd -S %1 -d %2 -E -b -i "Create Scripts\CreateSchema.sql"
if %ERRORLEVEL% NEQ 0 goto errors

REM: enable ERRORLEVEL to work in cycle
SetLocal EnableDelayedExpansion

for /R "Stored Procedures" %%f in ("*.sql") do (
	sqlcmd -S %1 -d %2 -E -b -i "%%f"
	if !ERRORLEVEL! NEQ 0 goto errors
)

for /R "Functions" %%f in ("*.sql") do (
	sqlcmd -S %1 -d %2 -E -b -i "%%f"
	if !ERRORLEVEL! NEQ 0 goto errors
)

EndLocal

sqlcmd -S %1 -d %2 -E -b -i "Create Scripts\InsertDefaultData.sql"
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

REM: How to use screen
:usage
echo.
echo Usage: MyScript Server Database
echo Server: the name of the target SQL Server
echo Database: the name of the target database
echo.
echo Example: RecreateDatabase.cmd "(local)\sql2008" "VirtualGallery" 
echo.
echo.
goto done

REM: error handler
:errors
echo.
echo WARNING! Error(s) were detected!
echo --------------------------------
echo Please evaluate the situation and, if needed,
echo restart this command file. You may need to
echo supply command parameters when executing
echo this command file.
echo.
pause
goto done

REM: finished execution
:finish
echo.
echo Script execution is complete!
:done
@echo on
