#!/bin/bash

echo "============================================"
echo "      GoodFood Application Setup Script"
echo "============================================"
echo

echo "[1/6] Stopping any existing containers..."
docker-compose down -v

echo
echo "[2/6] Starting PostgreSQL database..."
docker-compose up -d db

echo
echo "[3/6] Waiting for database to be healthy..."
while ! docker exec goodfood_db pg_isready -U postgres -d goodfood_db_pub >/dev/null 2>&1; do
    echo "Database not ready yet, waiting 5 seconds..."
    sleep 5
done
echo "Database is ready!"

echo
echo "[4/6] Running database migrations inside Docker container..."
echo "Building migrations container..."
docker-compose build migrations
echo "Running migrations..."
docker-compose run --rm migrations dotnet ef database update --project /src/src/GoodFood.Infrastructure --startup-project /src/src/GoodFood.Web

if [ $? -ne 0 ]; then
    echo
    echo "ERROR: Failed to run migrations. Please check:"
    echo "1. Docker containers are running properly"
    echo "2. Database connection string is correct"
    echo "3. Project builds successfully inside container"
    echo
    exit 1
fi

echo
echo "[5/6] Building and starting email worker service..."
docker-compose build emailworker
docker-compose up -d emailworker

echo
echo "[6/6] Starting the web application..."
echo "Starting the application container..."
docker-compose up -d webapp

echo
echo "============================================"
echo "    🎉 Setup Complete! 🎉"
echo "============================================"
echo
echo "✅ Services Started:"
echo "   📧 Email Worker: Running in background"
echo "   🗄️  Database: PostgreSQL on localhost:5432"
echo "   🌐 Web App: http://localhost:8090"
echo
echo "🚀 Application is ready at:"
echo "   👉 http://localhost:8090"
echo
echo "📋 To view logs:"
echo "   docker-compose logs webapp"
echo "   docker-compose logs emailworker"
echo "   docker-compose logs db"
echo
echo "🛑 To stop all services:"
echo "   docker-compose down"
echo
echo "Press Enter to open the application in your browser..."
read -p ""

# Try to open in browser (works on most Linux distributions and macOS)
if command -v xdg-open > /dev/null; then
    xdg-open http://localhost:8090
elif command -v open > /dev/null; then
    open http://localhost:8090
else
    echo "Please manually open http://localhost:8090 in your browser"
fi