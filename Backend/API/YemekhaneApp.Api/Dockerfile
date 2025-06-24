FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend/API/YemekhaneApp.Api/YemekhaneApp.Api.csproj", "Backend/API/YemekhaneApp.Api/"]
COPY ["YemekhaneApp.Application/YemekhaneApp.Application.csproj", "YemekhaneApp.Application/"]
COPY ["YemekhaneApp.Domain/YemekhaneApp.Domain.csproj", "YemekhaneApp.Domain/"]
COPY ["YemekhaneApp.Persistence/YemekhaneApp.Persistence.csproj", "YemekhaneApp.Persistence/"]
RUN dotnet restore "Backend/API/YemekhaneApp.Api/YemekhaneApp.Api.csproj"
COPY . .
WORKDIR "/src/Backend/API/YemekhaneApp.Api"
RUN dotnet build "./YemekhaneApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./YemekhaneApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YemekhaneApp.Api.dll"]
