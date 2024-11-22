FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN rm -rf publish
RUN dotnet restore
RUN dotnet build --no-restore
RUN dotnet publish -c Debug PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj -o publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "PlaygroundDotNetAPI.dll"]