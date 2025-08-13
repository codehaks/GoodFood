# TastePoint v2 - Technology Stack

## 🛠️ Core Technologies

### Backend Framework
- **.NET 8** - Latest LTS version with performance improvements
- **ASP.NET Core** - Modern web framework with built-in DI
- **Minimal APIs** - Lightweight API endpoints with source generators
- **Blazor Server** - Interactive UI components with SignalR backend

### Domain & Architecture
- **MediatR** - CQRS and in-process messaging pattern
- **EventStore DB** - Purpose-built event sourcing database
- **FluentValidation** - Fluent interface for validation rules
- **Mapster** - High-performance object mapping

### Data Persistence
- **PostgreSQL 15** - Primary relational database
- **Entity Framework Core 8** - Modern ORM with advanced features
- **EventStore DB** - Event sourcing and domain events
- **Redis 7** - Distributed caching and session storage

### UI & Frontend
- **Blazor Server** - Real-time interactive UI without JavaScript
- **Tailwind CSS** - Utility-first CSS framework
- **SignalR** - Real-time communication (built into Blazor Server)
- **Minimal JavaScript** - Only for essential browser APIs

### Testing Framework
- **xUnit** - Primary testing framework
- **Testcontainers** - Integration testing with real databases
- **ArchUnitNET** - Architecture and dependency rule testing
- **NBomber** - Load and performance testing
- **FluentAssertions** - Expressive assertion library

### DevOps & Deployment
- **Docker** - Containerization
- **Docker Compose** - Local development environment
- **GitHub Actions** - CI/CD pipeline
- **PostgreSQL** - Production database

## 📦 Detailed Package Dependencies

### Domain Layer (No External Dependencies)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  
  <!-- Domain layer should have NO external dependencies -->
  <!-- Only .NET BCL types allowed -->
</Project>
```

### Application Layer
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  
  <ItemGroup>
    <!-- CQRS and Messaging -->
    <PackageReference Include="MediatR" Version="12.2.0" />
    <PackageReference Include="MediatR.Extensions.Microsoft.DependencyInjection" Version="11.1.0" />
    
    <!-- Validation -->
    <PackageReference Include="FluentValidation" Version="11.8.1" />
    <PackageReference Include="FluentValidation.DependencyInjectionExtensions" Version="11.8.1" />
    
    <!-- Object Mapping -->
    <PackageReference Include="Mapster" Version="7.4.0" />
    <PackageReference Include="Mapster.DependencyInjection" Version="1.0.1" />
  </ItemGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\TastePoint.Domain\TastePoint.Domain.csproj" />
  </ItemGroup>
</Project>
```

### Infrastructure Layer
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  
  <ItemGroup>
    <!-- Database -->
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
    
    <!-- Event Sourcing -->
    <PackageReference Include="EventStore.Client.Grpc.Streams" Version="23.2.0" />
    <PackageReference Include="EventStore.Client.Grpc.ProjectionManagement" Version="23.2.0" />
    
    <!-- Caching -->
    <PackageReference Include="StackExchange.Redis" Version="2.7.10" />
    <PackageReference Include="Microsoft.Extensions.Caching.StackExchangeRedis" Version="8.0.0" />
    
    <!-- Logging -->
    <PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
    <PackageReference Include="Serilog.Sinks.PostgreSQL" Version="2.3.0" />
    <PackageReference Include="Serilog.Sinks.Seq" Version="7.0.0" />
    
    <!-- Email -->
    <PackageReference Include="MailKit" Version="4.3.0" />
    <PackageReference Include="MimeKit" Version="4.3.0" />
    
    <!-- File Storage -->
    <PackageReference Include="Azure.Storage.Blobs" Version="12.19.1" />
  </ItemGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\TastePoint.Domain\TastePoint.Domain.csproj" />
    <ProjectReference Include="..\TastePoint.Application\TastePoint.Application.csproj" />
  </ItemGroup>
</Project>
```

### Presentation Layer (Web)
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
  </PropertyGroup>
  
  <ItemGroup>
    <!-- Web Framework -->
    <PackageReference Include="Microsoft.AspNetCore.SignalR" Version="8.0.0" />
    
    <!-- Authentication -->
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="8.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
    
    <!-- API Documentation -->
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.0" />
    
    <!-- Health Checks -->
    <PackageReference Include="Microsoft.AspNetCore.Diagnostics.HealthChecks" Version="2.2.0" />
    <PackageReference Include="AspNetCore.HealthChecks.PostgreSql" Version="7.1.0" />
    <PackageReference Include="AspNetCore.HealthChecks.Redis" Version="7.0.1" />
  </ItemGroup>
  
  <ItemGroup>
    <ProjectReference Include="..\TastePoint.Application\TastePoint.Application.csproj" />
    <ProjectReference Include="..\TastePoint.Infrastructure\TastePoint.Infrastructure.csproj" />
  </ItemGroup>
</Project>
```

### Testing Projects
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>
  
  <ItemGroup>
    <!-- Testing Framework -->
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
    
    <!-- Test Utilities -->
    <PackageReference Include="FluentAssertions" Version="6.12.0" />
    <PackageReference Include="Moq" Version="4.20.69" />
    <PackageReference Include="AutoFixture" Version="4.18.0" />
    <PackageReference Include="AutoFixture.Xunit2" Version="4.18.0" />
    
    <!-- Integration Testing -->
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.0" />
    <PackageReference Include="Testcontainers.PostgreSql" Version="3.6.0" />
    <PackageReference Include="Testcontainers.Redis" Version="3.6.0" />
    
    <!-- Architecture Testing -->
    <PackageReference Include="ArchUnitNET.xUnit" Version="0.10.5" />
    
    <!-- Performance Testing -->
    <PackageReference Include="NBomber" Version="5.0.0" />
    
    <!-- Coverage -->
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
  </ItemGroup>
</Project>
```

## 🎯 Technology Decisions & Rationale

### Why Blazor Server over Client-Side SPA?
**Decision: Blazor Server**

✅ **Advantages:**
- **Simplified Development** - No separate frontend framework or build process
- **Real-time Updates** - Built-in SignalR integration for live kitchen updates
- **SEO Friendly** - Server-side rendering improves search indexing
- **Type Safety** - Shared C# models between server and client
- **Reduced Complexity** - Single technology stack (.NET)
- **Better Performance** - Smaller initial payload, server-side processing

⚠️ **Trade-offs:**
- Network dependency for UI interactions
- Requires persistent connection
- Less suitable for offline scenarios

**Verdict:** Perfect for restaurant operations where real-time updates and simplicity are prioritized over offline capability.

### Why EventStore DB over SQL Event Tables?
**Decision: EventStore DB**

✅ **Advantages:**
- **Purpose-Built** - Designed specifically for event sourcing
- **Projections** - Built-in read model generation
- **Event Streams** - Native support for aggregate event streams
- **Temporal Queries** - Point-in-time data access
- **Performance** - Optimized for append-only event storage
- **Clustering** - Built-in high availability

⚠️ **Trade-offs:**
- Additional infrastructure component
- Learning curve for team
- Less familiar than SQL databases

**Verdict:** Essential for proper event sourcing implementation and audit trail requirements.

### Why PostgreSQL over SQL Server?
**Decision: PostgreSQL**

✅ **Advantages:**
- **Open Source** - No licensing costs
- **JSON Support** - Native JSONB for flexible data storage
- **Performance** - Excellent for both OLTP and analytical workloads
- **Extensions** - Rich ecosystem (PostGIS for location data)
- **ACID Compliance** - Strong consistency guarantees
- **Scalability** - Proven at scale

⚠️ **Trade-offs:**
- Less integration with Microsoft tooling
- Team familiarity with SQL Server

**Verdict:** Cost-effective, performant, and feature-rich for restaurant operations.

### Why Minimal APIs over Controllers?
**Decision: Minimal APIs**

✅ **Advantages:**
- **Performance** - Reduced overhead and faster startup
- **Simplicity** - Less boilerplate code
- **Source Generators** - Compile-time optimizations
- **AOT Ready** - Native compilation support
- **Modern Approach** - Aligned with .NET direction

⚠️ **Trade-offs:**
- Less feature-rich than controllers
- Newer approach, less community examples

**Verdict:** Optimal for simple REST APIs with high performance requirements.

## 🏗️ Infrastructure Stack

### Development Environment
```yaml
# docker-compose.dev.yml
version: '3.8'
services:
  postgresql:
    image: postgres:15-alpine
    environment:
      POSTGRES_DB: tastepoint_dev
      POSTGRES_USER: tastepoint
      POSTGRES_PASSWORD: dev_password
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"
    command: redis-server --appendonly yes
    volumes:
      - redis_data:/data

  eventstore:
    image: eventstore/eventstore:23.10.0-buster-slim
    environment:
      EVENTSTORE_CLUSTER_SIZE: 1
      EVENTSTORE_RUN_PROJECTIONS: All
      EVENTSTORE_START_STANDARD_PROJECTIONS: true
      EVENTSTORE_ENABLE_ATOM_PUB_OVER_HTTP: true
      EVENTSTORE_INSECURE: true
    ports:
      - "1113:1113"
      - "2113:2113"
    volumes:
      - eventstore_data:/var/lib/eventstore

  seq:
    image: datalust/seq:2023.4
    environment:
      ACCEPT_EULA: Y
    ports:
      - "5341:80"
    volumes:
      - seq_data:/data

volumes:
  postgres_data:
  redis_data:
  eventstore_data:
  seq_data:
```

### Production Configuration
```yaml
# kubernetes/production.yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: tastepoint-web
  namespace: production
spec:
  replicas: 3
  selector:
    matchLabels:
      app: tastepoint-web
  template:
    metadata:
      labels:
        app: tastepoint-web
    spec:
      containers:
      - name: tastepoint-web
        image: tastepoint/web:latest
        ports:
        - containerPort: 8080
        env:
        - name: ConnectionStrings__DefaultConnection
          valueFrom:
            secretKeyRef:
              name: tastepoint-secrets
              key: database-connection
        - name: ConnectionStrings__Redis
          valueFrom:
            secretKeyRef:
              name: tastepoint-secrets
              key: redis-connection
        resources:
          requests:
            memory: "256Mi"
            cpu: "250m"
          limits:
            memory: "512Mi"
            cpu: "500m"
        readinessProbe:
          httpGet:
            path: /health/ready
            port: 8080
          initialDelaySeconds: 5
          periodSeconds: 10
        livenessProbe:
          httpGet:
            path: /health/live
            port: 8080
          initialDelaySeconds: 15
          periodSeconds: 20
```

## 🔧 Development Tools

### IDE & Extensions
- **Visual Studio 2022** or **JetBrains Rider**
- **C# Dev Kit** for VS Code
- **Entity Framework Core Power Tools**
- **SonarLint** for code quality

### Code Quality Tools
```xml
<!-- Directory.Build.props -->
<Project>
  <PropertyGroup>
    <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
    <Nullable>enable</Nullable>
    <AnalysisLevel>latest</AnalysisLevel>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" Version="8.0.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
    <PackageReference Include="SonarAnalyzer.CSharp" Version="9.14.0.81108">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

### CI/CD Pipeline
```yaml
# .github/workflows/ci.yml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main ]

jobs:
  test:
    runs-on: ubuntu-latest
    
    services:
      postgres:
        image: postgres:15
        env:
          POSTGRES_PASSWORD: postgres
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
    
    steps:
    - uses: actions/checkout@v4
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
    
    - name: Restore dependencies
      run: dotnet restore
    
    - name: Build
      run: dotnet build --no-restore --configuration Release
    
    - name: Test
      run: dotnet test --no-build --configuration Release --collect:"XPlat Code Coverage"
    
    - name: Upload coverage reports
      uses: codecov/codecov-action@v3
```

This technology stack provides a modern, performant, and maintainable foundation for TastePoint v2, optimized for single restaurant operations with strong domain modeling and real-time capabilities.
