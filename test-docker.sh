docker build -t playground-dotnet-api -f PlaygroundDotNetAPI/Dockerfile .
docker rm playground-dotnet-api
# Development --> appsettings.Development.json
docker run --name=playground-dotnet-api -p 7137:8080 --env DOTNET_ENVIRONMENT="Development" playground-dotnet-api
# Staging --> appsettings.Staging.json
#docker run --name=playground-dotnet-api -p 7137:8080 --env DOTNET_ENVIRONMENT="Staging" playground-dotnet-api
# Production --> appsettings.json
#docker run --name=playground-dotnet-api -p 7137:8080 playground-dotnet-api