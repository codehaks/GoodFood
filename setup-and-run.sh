#!/bin/bash

echo "============================================"
echo "      GoodFood Application Setup Script"
echo "============================================"
echo

echo "[1/5] Stopping any existing containers..."
docker-compose down -v

echo
echo "[2/5] Starting PostgreSQL database..."
docker-compose up -d db

echo
echo "[3/5] Waiting for database to be healthy..."
while ! docker exec goodfood_db pg_isready -U postgres -d goodfood_db_pub >/dev/null 2>&1; do
    echo "Database not ready yet, waiting 5 seconds..."
    sleep 5
done
echo "Database is ready!"

echo
echo "[4/5] Installing EF Core tools and running database migrations..."
dotnet tool install --global dotnet-ef >/dev/null 2>&1 || true
echo "Running migrations against database..."
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web

if [ $? -ne 0 ]; then
    echo
    echo "ERROR: Failed to run migrations. Please check:"
    echo "1. .NET 8.0 SDK is installed"
    echo "2. Database connection string is correct"
    echo "3. Project builds successfully"
    echo
    exit 1
fi

echo
echo "[5/5] Starting the web application..."
echo "Building and starting the application container..."
docker-compose up --build webapp

echo
echo "============================================"
echo "    Setup complete! Application should be"
echo "    available at http://localhost:8090"
echo "============================================"
echo
echo "Press Ctrl+C to stop the application"
