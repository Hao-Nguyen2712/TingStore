# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Backend/Services/Category/Category.API/Category.API.csproj", "Backend/Services/Category/Category.API/"]
COPY ["Backend/Services/Category/Category.Application/Category.Application.csproj", "Backend/Services/Category/Category.Application/"]
COPY ["Backend/Services/Category/Category.Core/Category.Core.csproj", "Backend/Services/Category/Category.Core/"]
COPY ["Backend/Services/Category/Category.Infrastructure/Category.Infrastructure.csproj", "Backend/Services/Category/Category.Infrastructure/"]
RUN dotnet restore "./Backend/Services/Category/Category.API/Category.API.csproj"
COPY . .
WORKDIR "/src/Backend/Services/Category/Category.API"
RUN dotnet build "./Category.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Category.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Category.API.dll"]
