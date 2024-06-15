#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/GoodFood.Web/GoodFood.Web.csproj", "src/GoodFood.Web/"]
COPY ["src/GoodFood.Application/GoodFood.Application.csproj", "src/GoodFood.Application/"]
COPY ["src/GoodFood.Domain/GoodFood.Domain.csproj", "src/GoodFood.Domain/"]
COPY ["src/GoodFood.Infrastructure/GoodFood.Infrastructure.csproj", "src/GoodFood.Infrastructure/"]
RUN dotnet restore "./src/GoodFood.Web/GoodFood.Web.csproj"
COPY . .
WORKDIR "/src/src/GoodFood.Web"
RUN dotnet build "./GoodFood.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GoodFood.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodFood.Web.dll"]
