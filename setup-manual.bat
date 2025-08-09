@echo off
echo ============================================
echo    GoodFood Manual Setup Script (No Docker)
echo ============================================
echo.
echo This script helps you set up GoodFood application
echo for development without Docker.
echo.

echo [Prerequisites Check]
echo Checking for required tools...

where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: .NET 8.0 SDK is not installed or not in PATH
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)
echo ✓ .NET SDK found

where psql >nul 2>&1
if %errorlevel% neq 0 (
    echo WARNING: PostgreSQL client tools (psql) not found in PATH
    echo You may need to create the database manually
    echo.
) else (
    echo ✓ PostgreSQL client tools found
)

echo.
echo [Database Setup]
echo Please ensure PostgreSQL server is running on localhost:5432
echo Default credentials: username=postgres, password=2385
echo.

set /p continue="Continue with database setup? (y/n): "
if /i not "%continue%"=="y" (
    echo Setup cancelled.
    pause
    exit /b 0
)

echo.
echo Creating database 'goodfood_db_pub'...
psql -h localhost -p 5432 -U postgres -f setup-database.sql
if %errorlevel% neq 0 (
    echo.
    echo WARNING: Database creation failed or database already exists
    echo You can create it manually using pgAdmin or run:
    echo   createdb -h localhost -p 5432 -U postgres goodfood_db_pub
    echo.
)

echo.
echo [EF Core Tools Setup]
echo Installing Entity Framework Core tools...
dotnet tool install --global dotnet-ef
if %errorlevel% neq 0 (
    echo EF Core tools may already be installed
)

echo.
echo [Database Migration]
echo Running database migrations...
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web
if %errorlevel% neq 0 (
    echo.
    echo ERROR: Migration failed. Please check:
    echo 1. PostgreSQL server is running
    echo 2. Database 'goodfood_db_pub' exists
    echo 3. Connection string in appsettings.Development.json is correct
    echo.
    pause
    exit /b 1
)

echo.
echo ============================================
echo         Setup Complete!
echo ============================================
echo.
echo To run the application:
echo   cd src/GoodFood.Web
echo   dotnet run
echo.
echo The application will be available at:
echo   https://localhost:7001 (HTTPS)
echo   http://localhost:5000 (HTTP)
echo.
echo Database connection details:
echo   Host: localhost
echo   Port: 5432
echo   Database: goodfood_db_pub
echo   Username: postgres
echo   Password: 2385
echo.
pause
