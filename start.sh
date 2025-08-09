#!/bin/bash

echo "Starting GoodFood Application with Docker..."
echo

echo "Stopping any existing containers..."
docker-compose down

echo
echo "Building and starting database and application..."
docker-compose up --build -d db

echo
echo "Waiting for database to be ready..."
while ! docker exec goodfood_db pg_isready -U postgres -d goodfood_db_pub >/dev/null 2>&1; do
    echo "Database not ready yet, waiting..."
    sleep 5
done

echo
echo "Database is ready! Running migrations..."
echo "First, let's install EF Core tools and run migrations locally..."
dotnet tool install --global dotnet-ef 2>/dev/null || true
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web

echo
echo "Starting the web application..."
docker-compose up --build webapp

echo
echo "Application should be available at http://localhost:8090"
echo "Press Ctrl+C to stop the application"
