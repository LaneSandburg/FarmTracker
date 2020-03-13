echo off

rem batch file to run a script to create a db
rem 9/11/2019

sqlcmd -S localhost -E -i FarmDB.sql
rem sqlcmd -S localhost\mssqlserver -E -i FarmDB.sql
rem sqlcmd -S localhost\sqlexpress -E -i FarmDB.sql

ECHO .
ECHO if no error messages appear DB was created
PAUSE


