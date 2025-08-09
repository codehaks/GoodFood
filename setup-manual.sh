#!/bin/bash

echo "============================================"
echo "    GoodFood Manual Setup Script (No Docker)"
echo "============================================"
echo
echo "This script helps you set up GoodFood application"
echo "for development without Docker."
echo

echo "[Prerequisites Check]"
echo "Checking for required tools..."

if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET 8.0 SDK is not installed or not in PATH"
    echo "Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0"
    exit 1
fi
echo "✓ .NET SDK found"

if ! command -v psql &> /dev/null; then
    echo "WARNING: PostgreSQL client tools (psql) not found in PATH"
    echo "You may need to create the database manually"
    echo
else
    echo "✓ PostgreSQL client tools found"
fi

echo
echo "[Database Setup]"
echo "Please ensure PostgreSQL server is running on localhost:5432"
echo "Default credentials: username=postgres, password=postgres"
echo

read -p "Continue with database setup? (y/n): " continue
if [[ ! "$continue" =~ ^[Yy]$ ]]; then
    echo "Setup cancelled."
    exit 0
fi

echo
echo "Creating database 'goodfood_db_pub'..."
psql -h localhost -p 5432 -U postgres -f setup-database.sql
if [ $? -ne 0 ]; then
    echo
    echo "WARNING: Database creation failed or database already exists"
    echo "You can create it manually using pgAdmin or run:"
    echo "  createdb -h localhost -p 5432 -U postgres goodfood_db_pub"
    echo
fi

echo
echo "[EF Core Tools Setup]"
echo "Installing Entity Framework Core tools..."
dotnet tool install --global dotnet-ef 2>/dev/null || true

echo
echo "[Database Migration]"
echo "Running database migrations..."
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web
if [ $? -ne 0 ]; then
    echo
    echo "ERROR: Migration failed. Please check:"
    echo "1. PostgreSQL server is running"
    echo "2. Database 'goodfood_db_pub' exists"
    echo "3. Connection string in appsettings.Development.json is correct"
    echo
    exit 1
fi

echo
echo "============================================"
echo "         Setup Complete!"
echo "============================================"
echo
echo "To run the application:"
echo "  cd src/GoodFood.Web"
echo "  dotnet run"
echo
echo "The application will be available at:"
echo "  https://localhost:7001 (HTTPS)"
echo "  http://localhost:5000 (HTTP)"
echo
echo "Database connection details:"
echo "  Host: localhost"
echo "  Port: 5432"
echo "  Database: goodfood_db_pub"
echo "  Username: postgres"
echo "  Password: postgres"
echo
