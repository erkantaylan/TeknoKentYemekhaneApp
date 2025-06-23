# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AspireApp/AspireApp.AppHost/YemekhaneApp.AppHost.csproj", "AspireApp/AspireApp.AppHost/"]
RUN dotnet restore "AspireApp/AspireApp.AppHost/YemekhaneApp.AppHost.csproj"
COPY . .
WORKDIR "/src/AspireApp/AspireApp.AppHost"
RUN dotnet build "YemekhaneApp.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YemekhaneApp.AppHost.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YemekhaneApp.AppHost.dll"]
