# GoodFood Application - Setup Guide

This guide provides setup instructions for running the GoodFood application with two different approaches: **Docker** (recommended for quick setup) and **Manual** (for developers who prefer local development).

## 🐳 Setup Option 1: Docker (Recommended)

This provides a complete Docker-based environment with PostgreSQL database and email worker service in containers.

### Prerequisites for Docker Setup

1. **Docker Desktop** - [Download here](https://www.docker.com/products/docker-desktop/)
2. **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0) *(Only needed for development)*
3. **Docker Compose** (usually included with Docker Desktop)

### Quick Start with Docker

#### For Windows Users
```cmd
setup-and-run.bat
```

#### For Linux/macOS Users
```bash
chmod +x setup-and-run.sh
./setup-and-run.sh
```

### What the Docker Setup Does
1. **Stops existing containers** - Cleans up any previous running instances
2. **Starts PostgreSQL database** - Runs PostgreSQL 16 in a Docker container
3. **Waits for database** - Ensures the database is ready before proceeding
4. **Runs migrations inside Docker** - Builds webapp container and runs EF migrations
5. **Starts email worker service** - Runs background email processing service
6. **Starts the web application** - Runs the GoodFood web application container
7. **Opens application in browser** - Automatically launches http://localhost:8090

### Docker Access Points
- **Web Application**: http://localhost:8090 (opens automatically)
- **PostgreSQL Database**: localhost:5432
  - Database: `goodfood_db_pub`
  - Username: `postgres`
  - Password: `postgres`
  - Environment: `Staging`
- **Email Worker**: Running in background container

### Docker Services
| Service | Container Name | Purpose | Status |
|---------|---------------|---------|---------|
| webapp | goodfood_web | Main web application | http://localhost:8090 |
| db | goodfood_db | PostgreSQL database | localhost:5432 |
| emailworker | goodfood_emailworker | Background email processing | Running |

---

## 🛠️ Setup Option 2: Manual (Local Development)

For developers who prefer to run PostgreSQL and the application locally without Docker.

### Prerequisites for Manual Setup

1. **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **PostgreSQL 12+** - [Download here](https://www.postgresql.org/download/)
3. **pgAdmin** (optional) - [Download here](https://www.pgadmin.org/download/)

### Required PostgreSQL Databases

You need to create the following database on your local PostgreSQL server:

| Database Name | Purpose | Username | Password |
|---------------|---------|----------|----------|
| `goodfood_db_pub` | Main application database | `postgres` | `postgres` |

### Quick Start with Manual Setup

#### For Windows Users
```cmd
setup-manual.bat
```

#### For Linux/macOS Users
```bash
chmod +x setup-manual.sh
./setup-manual.sh
```

### Manual Setup Steps (if scripts don't work)

1. **Create the database:**
   ```sql
   -- Connect to PostgreSQL as superuser and run:
   CREATE DATABASE goodfood_db_pub
       WITH OWNER = postgres
       ENCODING = 'UTF8';
   ```

2. **Or use the provided SQL script:**
   ```bash
   psql -h localhost -p 5432 -U postgres -f setup-database.sql
   ```

3. **Install EF Core tools:**
   ```bash
   dotnet tool install --global dotnet-ef
   ```

4. **Run database migrations:**
   ```bash
   dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web
   ```

5. **Run the web application (Terminal 1):**
   ```bash
   cd src/GoodFood.Web
   dotnet run
   ```

6. **Run the email worker (Terminal 2):**
   ```bash
   cd src/GoodFood.Worker.EmailSender
   dotnet run
   ```

### Manual Setup Access Points
- **Web Application**: 
  - https://localhost:7001 (HTTPS)
  - http://localhost:5000 (HTTP)
- **PostgreSQL Database**: localhost:5432
  - Database: `goodfood_db_pub`
  - Username: `postgres`
  - Password: `postgres`
  - Environment: `Development`
- **Email Worker**: Running in separate terminal

### Manual Services
| Service | Location | Purpose | Access |
|---------|----------|---------|--------|
| Web App | src/GoodFood.Web | Main application | https://localhost:7001 |
| Email Worker | src/GoodFood.Worker.EmailSender | Email processing | Background service |
| Database | Local PostgreSQL | Data storage | localhost:5432 |

---

## 🔧 Environment Configuration

The application uses different configurations based on the environment:

| Environment | Used For | Database Host | Database Password | Port |
|-------------|----------|---------------|-------------------|------|
| **Development** | Manual/Local setup | `localhost` | `postgres` | 5000/7001 |
| **Docker** | Docker container migrations | `db` (container) | `postgres` | N/A |
| **Staging** | Docker containers | `db` (container) | `postgres` | 8090 |
| **Production** | Production deployment | `localhost` | `postgres` | varies |

## 🔗 Connection Strings Configuration

### Understanding Connection Strings

The application uses different connection strings depending on how and where it's running:

#### 📁 Configuration Files Location
```
src/GoodFood.Web/
├── appsettings.json               # Base configuration
├── appsettings.Development.json   # Manual/Local development
├── appsettings.Docker.json        # Docker container migrations
├── appsettings.Staging.json       # Docker containers
└── appsettings.Production.json    # Production deployment
```

#### 🔄 Connection String Examples

**Development (Manual Setup):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=goodfood_db_pub;Username=postgres;Password=postgres"
  }
}
```

**Docker (Container Migrations):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=goodfood_db_pub;Username=postgres;Password=postgres"
  }
}
```

**Staging (Docker Containers):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=goodfood_db_pub;Username=postgres;Password=postgres"
  }
}
```

### 🛠️ How to Modify Connection Strings

#### For Manual Setup:
Edit `src/GoodFood.Web/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=YOUR_HOST;Port=YOUR_PORT;Database=goodfood_db_pub;Username=YOUR_USER;Password=YOUR_PASSWORD"
  }
}
```

#### For Docker Setup:
If you need different database credentials:

1. **Edit Docker Compose file** (`docker-compose.yml`):
```yaml
db:
  environment:
    POSTGRES_USER: your_user
    POSTGRES_PASSWORD: "your_password"
    POSTGRES_DB: goodfood_db_pub
```

2. **Update Docker configuration** (`src/GoodFood.Web/appsettings.Docker.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=goodfood_db_pub;Username=your_user;Password=your_password"
  }
}
```

3. **Update Staging configuration** (`src/GoodFood.Web/appsettings.Staging.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=goodfood_db_pub;Username=your_user;Password=your_password"
  }
}
```

### 🚨 Common Connection String Issues

| Issue | Cause | Solution |
|-------|-------|----------|
| "No such host is known" | Wrong host in connection string | Use `localhost` for local, `db` for containers |
| "Connection refused" | Database not running | Start PostgreSQL service or Docker container |
| "Password authentication failed" | Wrong password | Check password in appsettings matches database |
| "Database does not exist" | Database not created | Run setup scripts or create database manually |

## 📁 Project Structure

```
GoodFood/
├── src/
│   ├── GoodFood.Web/                    # Main web application
│   ├── GoodFood.Application/            # Application services
│   ├── GoodFood.Domain/                 # Domain entities and logic
│   ├── GoodFood.Infrastructure/         # Data access and external services
│   └── GoodFood.Worker.EmailSender/    # Background email worker
├── tests/                               # Unit and integration tests
├── docker-compose.yml                  # Docker configuration
├── Dockerfile                          # Web app container config
├── Dockerfile.EmailWorker              # Email worker container config
├── setup-and-run.bat/.sh              # Docker setup scripts
├── setup-manual.bat/.sh               # Manual setup scripts
└── setup-database.sql                 # Database creation script
```

## 🛠️ Development Commands

### Common EF Core Commands
```bash
# Create a new migration
dotnet ef migrations add MigrationName --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web

# Update database (Manual setup)
dotnet ef database update --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web

# Update database (Docker setup)
docker-compose run --rm -e ASPNETCORE_ENVIRONMENT=Docker webapp dotnet ef database update --project /src/src/GoodFood.Infrastructure --startup-project /src/src/GoodFood.Web

# Remove last migration
dotnet ef migrations remove --project src/GoodFood.Infrastructure --startup-project src/GoodFood.Web
```

### Docker Commands
```bash
# Start all services
docker-compose up --build

# Start only database
docker-compose up -d db

# Start only specific services
docker-compose up -d db webapp emailworker

# Stop all services
docker-compose down

# Stop and remove all data
docker-compose down -v

# View logs
docker-compose logs webapp
docker-compose logs emailworker
docker-compose logs db

# View real-time logs
docker-compose logs -f webapp

# Run migrations in Docker
docker-compose run --rm -e ASPNETCORE_ENVIRONMENT=Docker webapp dotnet ef database update --project /src/src/GoodFood.Infrastructure --startup-project /src/src/GoodFood.Web
```

### Manual Development Commands
```bash
# Start web application
cd src/GoodFood.Web
dotnet run

# Start email worker (separate terminal)
cd src/GoodFood.Worker.EmailSender
dotnet run

# Build all projects
dotnet build

# Run tests
dotnet test tests/GoodFood.Tests/
```

## 🚨 Troubleshooting

### Database Connection Issues

**For Docker Setup:**
1. Ensure Docker is running
2. Check container status: `docker ps`
3. Verify database health: `docker exec goodfood_db pg_isready -U postgres -d goodfood_db_pub`

**For Manual Setup:**
1. Ensure PostgreSQL service is running
2. Verify database exists: `psql -h localhost -p 5432 -U postgres -l`
3. Test connection: `psql -h localhost -p 5432 -U postgres -d goodfood_db_pub`

### Email Worker Issues

**For Docker Setup:**
- Check if email worker container is running: `docker ps | grep emailworker`
- View email worker logs: `docker-compose logs emailworker`

**For Manual Setup:**
- Ensure email worker is running in separate terminal
- Check NetMQ port 5556 is available

### Migration Issues

**For Docker Setup:**
Migrations now run inside Docker containers, so they automatically use the correct connection string (`Server=db`).

**For Manual Setup:**
1. Ensure .NET 8.0 SDK is installed: `dotnet --version`
2. Verify project builds: `dotnet build src/GoodFood.Web/GoodFood.Web.csproj`
3. Check connection strings match your setup (see [Connection Strings Configuration](#-connection-strings-configuration))
4. Verify database is running and accessible

### Port Conflicts
**Docker:** Modify ports in `docker-compose.yml`
**Manual:** The application will use the next available port automatically

### Application Not Opening
**Docker:** The setup script will automatically open http://localhost:8090 in your browser
**Manual:** Manually navigate to https://localhost:7001 or http://localhost:5000

## 📦 Additional Setup Files

- `setup-database.sql` - SQL script to create required databases
- `docker-compose.override.yml` - Development overrides for Docker
- `Dockerfile` - Web application container configuration
- `Dockerfile.EmailWorker` - Email worker container configuration

## 🎉 Success Indicators

### Docker Setup Success
```
✅ Services Started:
   📧 Email Worker: Running in background
   🗄️  Database: PostgreSQL on localhost:5432
   🌐 Web App: http://localhost:8090
```

### Manual Setup Success
```
✅ To run the FULL application you need to start:
1. 🌐 Web Application: https://localhost:7001
2. 📧 Email Worker: Background service running
```

Both setups will have a fully functional GoodFood application with email processing capabilities!