@echo off
echo ============================================
echo      GoodFood Application Setup Script
echo ============================================
echo.

echo [1/5] Stopping any existing containers...
docker-compose down -v

echo.
echo [2/5] Starting PostgreSQL database...
docker-compose up -d db

echo.
echo [3/5] Waiting for database to be healthy...
:waitloop
docker exec goodfood_db pg_isready -U postgres -d goodfood_db_pub >nul 2>&1
if %errorlevel% neq 0 (
    echo Database not ready yet, waiting 5 seconds...
    timeout /t 5 /nobreak >nul
    goto waitloop
)
echo Database is ready!

echo.
echo [4/5] Installing EF Core tools and running database migrations...
dotnet tool install --global dotnet-ef >nul 2>&1
echo Running migrations against database...
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web

if %errorlevel% neq 0 (
    echo.
    echo ERROR: Failed to run migrations. Please check:
    echo 1. .NET 8.0 SDK is installed
    echo 2. Database connection string is correct
    echo 3. Project builds successfully
    echo.
    pause
    exit /b 1
)

echo.
echo [5/5] Starting the web application...
echo Building and starting the application container...
docker-compose up --build webapp

echo.
echo ============================================
echo    Setup complete! Application should be
echo    available at http://localhost:8090
echo ============================================
echo.
echo Press Ctrl+C to stop the application
pause
