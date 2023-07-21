#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
# Install EF tool
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Restore packages
WORKDIR /src
COPY ./API/*.csproj             ./API/
COPY ./Domain/*.csproj          ./Domain/
COPY ./Infrastructure/*.csproj  ./Infrastructure/
RUN dotnet restore ./API/Proprette.API.csproj

# Build the projects
COPY ./API/             ./API/
COPY ./Domain/          ./Domain/
COPY ./Infrastructure/  ./Infrastructure/
RUN dotnet build API/Proprette.API.csproj -c Release --no-restore

# Build the migration bundle
RUN dotnet ef migrations bundle -p ./Infrastructure -s ./API --configuration Release --no-build --self-contained
#
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
#
COPY --from=build /src/efbundle .
#COPY ./API/appsettings.json .
COPY ./run_bundle.sh .

ENV PROPRETTE_CONNECTION_STRING="Database=proprettedb;Server=host.docker.internal;Port=3307;User=root;Password=@06@June@1981"

ENTRYPOINT ["./run_bundle.sh"]
#ENTRYPOINT ["./efbundle", "--connection"]
#ENTRYPOINT ["./efbundle", "--connection", "server=localhost;user=root;password=1;database=dockerdb"]