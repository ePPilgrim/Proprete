#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DataSeeding/Proprette.DataSeeding.csproj", "DataSeeding/"]
COPY ["API/Proprette.API.csproj", "API/"]
COPY ["Domain/Proprette.Domain.csproj", "Domain/"]
COPY ["Infrastructure/Proprette.Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "DataSeeding/Proprette.DataSeeding.csproj"
COPY . .
WORKDIR "/src/DataSeeding"
RUN dotnet build "Proprette.DataSeeding.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proprette.DataSeeding.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proprette.DataSeeding.dll"]