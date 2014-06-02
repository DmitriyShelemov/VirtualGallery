@echo on
REM: This command file updates database schema, creates stored procedures and views.
REM: Windows authentication is used to connect to SQL Server.
REM:
REM: Usage: CommandFilename [Server] [Database]
REM: Example: UpdateDatabase.cmd "(local)\sql2008" "VirtualGallery" 

if '%1' == '' goto usage
if '%2' == '' goto usage

sqlcmd -S %1 -d %2 -E -b -e -i "Update Scripts\UpdateSchema.sql"
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

sqlcmd -S %1 -d %2 -E -b -i "Update Scripts\UpdateDefaultData.sql"
if %ERRORLEVEL% NEQ 0 goto errors

goto finish

REM: How to use screen
:usage
echo.
echo Usage: MyScript Server Database
echo Server: the name of the target SQL Server
echo Database: the name of the target database
echo.
echo Example: UpdateDatabase.cmd "(local)\sql2008" "VirtualGallery" 
echo.
echo.
goto done

REM: error handler
:errors
echo.
echo WARNING! Error(s) were detected!
echo --------------------------------
echo.
pause
goto done

REM: finished execution
:finish
echo.
echo The script was successfully executed.
:done
@echo on
