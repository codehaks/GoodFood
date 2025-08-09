# GoodFood Developer Guide

## Table of Contents

1. Overview
2. Architecture Summary
3. Prerequisites
4. Running Locally (Step-by-Step)
	- Using Docker
	- Manual Setup
5. Troubleshooting & Tips

---

## 1. Overview

GoodFood is a modern, layered web application for food ordering, built with ASP.NET Core, Entity Framework Core, MediatR, and Blazor. It supports authentication, real-time updates, and background email processing.

## 2. Architecture Summary

- **Domain Layer**: Core business entities, value objects, repository contracts.
- **Application Layer**: Service contracts, business logic, MediatR handlers.
- **Infrastructure Layer**: Data access (EF Core), repository implementations, migrations, external integrations.
- **Web Layer**: ASP.NET Core web app (controllers, hubs, Razor/Blazor pages, services).
- **Worker.EmailSender**: Background worker for email notifications.
- **Tests**: Unit and integration tests.

### Key Technologies

- ASP.NET Core, EF Core, MediatR, Serilog, Mapster, Identity
- Docker for containerized deployment
- Blazor & Razor Pages for UI

---

## 3. Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optional) SQL Server or PostgreSQL (if not using Docker)
- (Optional) Node.js (for front-end asset management)

---

## 4. Running Locally

### Option A: Using Docker (Recommended)

1. **Clone the repository**

	```sh
	git clone https://github.com/codehaks/GoodFood.git
	cd GoodFood
	```

2. **Build and run containers**

	```sh
	docker-compose up --build
	```
	- This will start the web app, database, and worker service.
	- Access the app at [http://localhost:5000](http://localhost:5000) (or as shown in Docker output).

3. **Stopping containers**

	```sh
	docker-compose down
	```

### Option B: Manual Setup

1. **Clone the repository**

	```sh
	git clone https://github.com/codehaks/GoodFood.git
	cd GoodFood
	```

2. **Set up the database**
	- Ensure SQL Server/PostgreSQL is running.
	- Update `appsettings.json` with your connection string in `src/GoodFood.Web` and `src/GoodFood.Worker.EmailSender`.

3. **Apply migrations**

	```sh
	dotnet ef database update --project src/GoodFood.Infrastructure
	```

4. **Run the web application**

	```sh
	dotnet run --project src/GoodFood.Web
	```

5. **Run the email worker**

	```sh
	dotnet run --project src/GoodFood.Worker.EmailSender
	```

6. **Access the app**
	- Open [http://localhost:5000](http://localhost:5000) in your browser.

---

## 5. Troubleshooting & Tips

- **Ports**: If ports are busy, change them in `docker-compose.yml` or `appsettings.json`.
- **Database Issues**: Ensure migrations are applied and connection strings are correct.
- **Front-End Assets**: If using Node.js, run `npm install` and `npm run build` in the relevant directories.
- **Logs**: Check Serilog output in the console for errors.
- **Tests**: Run tests with:

  ```sh
  dotnet test tests/GoodFood.Tests
  ```

---

## Need Help?

- Check the README.md for more details.
- Contact the maintainers or open an issue on GitHub.
# GoodFood