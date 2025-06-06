# Setup
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FCG.API/FCG.API.csproj", "FCG.API/"]
COPY ["FCG.Application/FCG.Application.csproj", "FCG.Application/"]
COPY ["FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["FCG.Infra.Data/FCG.Infra.Data.csproj", "FCG.Infra.Data/"]
RUN dotnet restore "./FCG.API/FCG.API.csproj"
COPY . .
WORKDIR "/src/FCG.API"
RUN dotnet build "./FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Run API
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FCG.API.dll"]