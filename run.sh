# Clean
dotnet clean && dotnet nuget locals all --clear
# Restore dependencies
dotnet restore
# Build
dotnet build --no-restore
# Test
dotnet test --no-build --verbosity normal
# Run
PROJECT_FILE="PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj"
dotnet run --project=$PROJECT_FILE
# or for hot reload
dotnet watch run --project=$PROJECT_FILE