#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV PATH $PATH:/root/.dotnet/tools
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj", "PlaygroundDotNetAPI/"]
RUN dotnet restore "./PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj"
COPY . .
WORKDIR "/src/PlaygroundDotNetAPI"
RUN dotnet tool install --global dotnet-ef
RUN dotnet-ef database update -p ./PlaygroundDotNetAPI.csproj
RUN dotnet build "./PlaygroundDotNetAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PlaygroundDotNetAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ARG MY_SECRET="from-dockerfile"
WORKDIR /app
COPY --from=publish /app/publish .
ENV MY_SECRET="$MY_SECRET"
ENTRYPOINT ["dotnet", "PlaygroundDotNetAPI.dll"]