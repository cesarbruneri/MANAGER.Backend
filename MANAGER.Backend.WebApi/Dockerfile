#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MANAGER.Backend.WebApi/MANAGER.Backend.WebApi.csproj", "MANAGER.Backend.WebApi/"]
RUN dotnet restore "MANAGER.Backend.WebApi/MANAGER.Backend.WebApi.csproj"
COPY . .
WORKDIR "/src/MANAGER.Backend.WebApi"
RUN dotnet build "MANAGER.Backend.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MANAGER.Backend.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MANAGER.Backend.WebApi.dll"]