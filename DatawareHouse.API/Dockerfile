# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY DatawareHouse.Models/DatawareHouse.Models.csproj ./DatawareHouse.Models/
COPY DatawareHouse.API/DatawareHouse.API.csproj ./DatawareHouse.API/
RUN dotnet restore ./DatawareHouse.API/DatawareHouse.API.csproj

# Copy everything and build
COPY . .
WORKDIR /src/DatawareHouse.API
RUN dotnet publish -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Logging (optional)
COPY ./DatawareHouse.API/log4net.config ./log4net.config

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "DatawareHouse.API.dll"]