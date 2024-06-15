#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/MotorcycleRental.API/MotorcycleRental.API.csproj", "src/MotorcycleRental.API/"]
COPY ["src/MotorcycleRental.Application/MotorcycleRental.Application.csproj", "src/MotorcycleRental.Application/"]
COPY ["src/MotorcycleRental.Domain/MotorcycleRental.Domain.csproj", "src/MotorcycleRental.Domain/"]
COPY ["src/MotorcycleRental.Infrastructure/MotorcycleRental.Infrastructure.csproj", "src/MotorcycleRental.Infrastructure/"]
RUN dotnet restore "./src/MotorcycleRental.API/MotorcycleRental.API.csproj"
COPY . .
WORKDIR "/src/src/MotorcycleRental.API"
RUN dotnet build "./MotorcycleRental.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MotorcycleRental.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleRental.API.dll"]