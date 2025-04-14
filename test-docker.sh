docker build -t playground-dotnet-api -f PlaygroundDotNetAPI/Dockerfile .
docker rm playground-dotnet-api
# This will build prod
# docker run --name=playground-dotnet-api -p 7137:8080 playground-dotnet-api

docker run --name=playground-dotnet-api -p 7137:8080 playground-dotnet-api --build-arg BUILD_CONFIGURATION="Debug"