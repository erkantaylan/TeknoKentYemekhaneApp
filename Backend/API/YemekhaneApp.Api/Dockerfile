# .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Proje dosyalarını kopyala
COPY ./Backend/API/YemekhaneApp.Api/YemekhaneApp.Api.csproj ./Backend/API/YemekhaneApp.Api/
COPY ./YemekhaneApp.Application/YemekhaneApp.Application.csproj ./YemekhaneApp.Application/
COPY ./YemekhaneApp.Domain/YemekhaneApp.Domain.csproj ./YemekhaneApp.Domain/
COPY ./YemekhaneApp.Persistence/YemekhaneApp.Persistence.csproj ./YemekhaneApp.Persistence/

# Bağımlılıkları indir
RUN dotnet restore ./Backend/API/YemekhaneApp.Api/YemekhaneApp.Api.csproj

# Tüm source'ları kopyala
COPY . .

# Build et
WORKDIR /src/Backend/API/YemekhaneApp.Api
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 5000
EXPOSE 5001
 
ENTRYPOINT ["dotnet", "YemekhaneApp.Api.dll"]
