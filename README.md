# GoodFood Developer Guide

## Table of Contents

1. Overview
2. Architecture Summary
3. Clean Architecture & DDD (🧭)
4. Tech Stack (🧰)
5. Mapping (🗺️)
6. Logging (🪵)
7. Developer Guide (🧑‍💻)
8. Prerequisites
9. Running Locally (Step-by-Step)
	- Using Docker
	- Manual Setup
10. Troubleshooting & Tips
11. Contributing (🤝)
12. License (📄)
13. Credits (💜)

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

## 3. Clean Architecture & DDD (🧭)

- **Entities (Domain) 🧱**: Business-centric types in `src/GoodFood.Domain/Entities` and value objects in `src/GoodFood.Domain/Values` encapsulate invariants and business rules.
- **Use Cases (Application) 🎯**: Application services in `src/GoodFood.Application/Services` orchestrate workflows and depend on domain contracts, not implementations. Interfaces live under `src/GoodFood.Application/Contracts`.
- **Infrastructure Adapters (Infrastructure) 🧩**: EF Core `DbContext`, repositories, migrations, and external services live in `src/GoodFood.Infrastructure`. They implement domain/application contracts.
- **Presentation (Web) 🖥️**: UI, controllers, SignalR hubs, and DI wiring reside in `src/GoodFood.Web`.
- **Bounded Contexts (DDD) 🗂️**: Ordering, Menu, Cart are separated in domain entities and repository contracts. Domain events (e.g., `OrderCreatedNotification`) decouple side effects from core flows.

Request flow (simplified):

```mermaid
flowchart LR
  UI[Web UI / Controllers] -->|calls| App[Application Services]
  App --> Dom[Domain Entities & Values]
  App -->|contracts| Repo[Repository Interfaces]
  Repo --> Infra[Infrastructure: EF Core, Email, Storage]
  UI <-->|realtime| Hub[SignalR Hub]
  App --> Events[Domain Notifications]
  Events --> Handlers[Infrastructure Handlers]
```

Folder structure (abridged):

```text
src/
  GoodFood.Domain/           # 🧱 Domain: Entities, ValueObjects, Contracts
  GoodFood.Application/      # 🎯 Application: Services, Contracts, Mappers, Notifications
  GoodFood.Infrastructure/   # 🧩 Infrastructure: EF Core, Repos, DbContext, Migrations
  GoodFood.Web/              # 🖥️ Web: UI, Controllers, Hubs, DI
  GoodFood.Worker.EmailSender/ # ⚙️ Worker: background processing
tests/
  GoodFood.Tests/            # 🧪 Tests
```

Design principles:

- **Dependency Rule**: Outer layers depend inward. Only `Web` references `Infrastructure`; domain has no external dependencies.
- **Persistence Ignorance**: Entities are POCOs; EF mappings are isolated in `Infrastructure`.
- **Ubiquitous Language**: Types like `Order`, `Cart`, `Money`, `CustomerInfo` mirror business terms.

---

## 4. Tech Stack (🧰)

- **Runtime**: .NET 8
- **Web**: ASP.NET Core (Razor Pages + Server-side Blazor)
- **Persistence**: EF Core + PostgreSQL (`Npgsql` provider)
- **AuthN/Z**: ASP.NET Core Identity (custom `ApplicationUser`)
- **Messaging/Realtime**: SignalR (order status hub), NetMQ for internal push notifications
- **Mediators/Events**: MediatR for notifications (`OrderCreatedNotification` → handler)
- **Mapping**: Mapster (lightweight object mapping)
- **Logging**: Serilog (console sink; configurable via `appsettings.*.json`)
- **Observability**: Microsoft.Extensions.Telemetry (available for metrics/tracing)
- **Testing**: xUnit, Moq
- **Containers**: Docker (multi-stage build)

---

## 5. Mapping (🗺️)

- **Library**: Mapster
- **Global config**: Mappings are registered at startup, e.g. converting value objects like `Money` to primitives for serialization:

```csharp
// src/GoodFood.Web/Program.cs
TypeAdapterConfig<Money, decimal>.NewConfig().MapWith(src => src.Value);
```

- **Custom mappers**: Pure mapping helpers live under `src/GoodFood.Application/Mappers`:
  - `UserMapper` transforms authenticated user info into domain `CustomerInfo`.
  - `CartMapper` projects rich domain `Cart` into DTOs for UI.

Guidelines:

- Keep mapping logic deterministic and side-effect free.
- Map domain to DTOs at boundaries; pass domain types within use cases.

---

## 6. Logging (🪵)

- **Library**: Serilog
- **Bootstrap**: Logger is configured early in `Program.cs` and bound to configuration:

```csharp
// src/GoodFood.Web/Program.cs
builder.Host.UseSerilog((ctx, logger) => logger.ReadFrom.Configuration(ctx.Configuration));
```

- **Configuration**: Controlled via `appsettings.*.json` under `serilog`:

```json
// src/GoodFood.Web/appsettings.Development.json
{
  "serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
      }
    },
    "WriteTo": [ { "Name": "Console" } ]
  }
}
```

- **Sinks**: Console by default. Production can route to PostgreSQL or other sinks (example code present and can be enabled).
- **Usage**: Prefer structured logs with contextual properties; Serilog enrichers can be added as needed.

---

## 7. Developer Guide (🧑‍💻)

- **Local DB**: PostgreSQL (via Docker or local install). Update `ConnectionStrings:DefaultConnection` in `src/GoodFood.Web/appsettings.*.json`.
- **Migrations**:
  - Add: `dotnet ef migrations add <Name> --project src/GoodFood.Infrastructure`
  - Update: `dotnet ef database update --project src/GoodFood.Infrastructure`
- **Run**:
  - Web: `dotnet run --project src/GoodFood.Web`
  - Worker: `dotnet run --project src/GoodFood.Worker.EmailSender`
- **Tests**: `dotnet test tests/GoodFood.Tests`
- **Debugging**:
  - Enable detailed errors via `appsettings.Development.json` → `DetailedErrors: true`.
  - Serilog console sink is enabled in Development.
- **Coding Standards**:
  - C# 12, nullable enabled, analyzers configured centrally (`Directory.Build.props`).
  - Prefer domain types internally; map to DTOs at boundaries.
  - Keep services/application layer free of EF Core types.
- **Branching & Commits**:
  - Branches: `feature/<short-description>`, `fix/<short-description>`
  - Conventional commits: `feat:`, `fix:`, `docs:`, `refactor:`, `test:`, `chore:`
- **PR Checklist**:
  - Tests passing, updated docs, no analyzer errors, migrations included when schema changes.

---

## 8. Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optional) SQL Server or PostgreSQL (if not using Docker)
- (Optional) Node.js (for front-end asset management)

---

## 9. Running Locally

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

## 10. Troubleshooting & Tips

---

## 11. Contributing (🤝)

- **Issues**: Open a descriptive issue with steps to reproduce or a proposal.
- **Fork & Branch**: Fork the repo and create a feature branch.
- **Code Style**: Follow the analyzers; ensure `dotnet format` is clean.
- **Tests**: Add/adjust unit tests in `tests/GoodFood.Tests`.
- **PRs**: Submit a PR linked to an issue, with a clear description and screenshots/logs where helpful.

See `CONTRIBUTING.md` for details.

---

## 12. License (📄)

This project is licensed under the MIT License. See the `LICENSE` file for details.

---

## 13. Credits (💜)

Developed by [codehaks.com](https://codehaks.com).

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