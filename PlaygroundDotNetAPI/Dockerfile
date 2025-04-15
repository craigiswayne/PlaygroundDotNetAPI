#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj", "PlaygroundDotNetAPI/"]
RUN dotnet restore "PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj"
COPY . .
WORKDIR "/src/PlaygroundDotNetAPI"
RUN dotnet build "PlaygroundDotNetAPI.csproj" --configuration Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlaygroundDotNetAPI.csproj" --configuration Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaygroundDotNetAPI.dll"]