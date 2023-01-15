FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . /app
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

RUN dotnet restore
RUN dotnet publish -c Release -o out

ENTRYPOINT ["dotnet", "out/YourBestDestination.API.dll"]