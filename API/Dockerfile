#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API/Proprette.API.csproj", "API/"]
COPY ["Domain/Proprette.Domain.csproj", "Domain/"]
COPY ["Infrastructure/Proprette.Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "API/Proprette.API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "Proprette.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proprette.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV PROPRETTE_CONNECTION_STRING="Database=proprettedb;Server=host.docker.internal;Port=3307;User=Pavlo;Password=@06@June@1981"

ENTRYPOINT ["dotnet", "Proprette.API.dll"]