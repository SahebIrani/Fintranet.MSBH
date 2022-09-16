#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/04 - Presentation/Presentation/Server/Presentation.Server.csproj", "src/04 - Presentation/Presentation/Server/"]
COPY ["src/00 - BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/00 - BuildingBlocks/BuildingBlocks/"]
COPY ["src/03 - Service/Service/Service.csproj", "src/03 - Service/Service/"]
COPY ["src/02 - Data/Data/Data.csproj", "src/02 - Data/Data/"]
COPY ["src/01 - Domain/Domain/Domain.csproj", "src/01 - Domain/Domain/"]
COPY ["src/04 - Presentation/Presentation/Client/Presentation.Client.csproj", "src/04 - Presentation/Presentation/Client/"]
COPY ["src/04 - Presentation/Presentation/Shared/Presentation.Shared.csproj", "src/04 - Presentation/Presentation/Shared/"]
RUN dotnet restore "src/04 - Presentation/Presentation/Server/Presentation.Server.csproj"
COPY . .
WORKDIR "/src/src/04 - Presentation/Presentation/Server"
RUN dotnet build "Presentation.Server.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation.Server.csproj" --no-restore -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.Server.dll"]
