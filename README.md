# Playground DotNet API

* Endpoints
  * https://localhost:7137/swagger/index.html
  * https://localhost:7137/environment
  * https://localhost:7137/pokedex
* Middleware
  * [SecurityHeaders](PlaygroundDotNetAPI/Middleware/SecurityHeaders.cs)
    * Adds a bunch of recommended security headers
  * [VersionHeader](PlaygroundDotNetAPI/Middleware/VersionHeader.cs)
    * Adds `Version` header to ALL API Responses
* Telemetry Microsoft Azure App Insights

---

### Getting Started
```shell
dotnet clean
dotnet nuget locals all --clear
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal

#ENVIRONMENT_NAME="Development";
ENVIRONMENT_NAME="UAT"
PROJECT_FILE="PlaygroundDotNetAPI/PlaygroundDotNetAPI.csproj"
dotnet watch run --environment=$ENVIRONMENT_NAME --project=$PROJECT_FILE;
# see here: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0
```

### Testing a Compiled App
Testing out the DLL / compiled app

```bash
dotnet clean
#dotnet nuget locals all --clear

dotnet restore

# Will create a debug package
# see ./PlaygroundDotNetAPI/bin/Debug
dotnet build --no-restore

# Creates the release package
dotnet publish
DLL_PATH="PlaygroundDotNetAPI/bin/Release/net8.0/publish/PlaygroundDotNetAPI.dll"
ENVIRONMENT_NAME="Development";
#ENVIRONMENT_NAME="UAT";
#ENVIRONMENT_NAME="Release";
dotnet $DLL_PATH -- --no-build --environment=$ENVIRONMENT_NAME;
dotnet $DLL_PATH -- --no-build;
```

---

### Scaffolding
```shell
name="PlaygroundDotNetAPI"
# mkdir $name
dotnet new sln -o $name
cd $name
dotnet new webapi --framework net8.0
git init
dotnet new gitignore

# mkdir -p Controllers
# cd Controllers
# dotnet new class -n WeatherForecastController

# https://learn.microsoft.com/en-us/aspnet/core/fundamentals/tools/dotnet-aspnet-codegenerator?view=aspnetcore-8.0
dotnet tool install -g dotnet-aspnet-codegenerator

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet aspnet-codegenerator area Controller


# https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
#dotnet new xunit -o "$name".Tests
# create a controller
#dotnet add "./$name.Tests/$name.Tests.csproj" reference "./$name/$name.csproj"
#dotnet sln add "./$name.Tests/$name.Tests.csproj"

dotnet new nunit -o "$name".Tests
dotnet add "./$name.Tests/$name.Tests.csproj" reference "./$name/$name.csproj"
dotnet sln add "./$name.Tests/$name.Tests.csproj"
```

---

### Rate Limiting
Requires .NET 7

Resources:
* https://www.youtube.com/watch?v=bOfOo3Zsfx0&t=1396s
* https://www.infoworld.com/article/3696320/how-to-use-the-rate-limiting-algorithms-in-aspnet-core.html

Program.cs

```c#
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    rateLimiterOptions.AddFixedWindowLimiter("fixed-window", fixedWindowOptions =>
    {
        fixedWindowOptions.Window = TimeSpan.FromSeconds(5);
        fixedWindowOptions.PermitLimit = 5;
        fixedWindowOptions.QueueLimit = 10;
        fixedWindowOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

...

app.UseRateLimiter();
```

In your controller
```c#
using Microsoft.AspNetCore.RateLimiting;
...
[EnableRateLimiting("fixed-window")]
```

----

### Security Headers
Because there's a few headers we need to add, we'll create a middleware implementation

```shell
mkdir -p Middleware
touch Middleware/SecurityHeaders.cs
```

see `Middleware/SecurityHeaders.cs` for contents

In `Program.cs`

```c#
using PlaygroundDotNetAPI.Middleware;
...

var builder = WebApplication.CreateBuilder(args);
// add the below
builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

//

builder.Services.AddHsts(options =>
{
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(365);
});

//

// before app.MapControllers();
app.UseSecurityHeaders();
```

Resources:
* https://dotnetthoughts.net/implementing-content-security-policy-in-aspnetcore/
* https://blog.elmah.io/the-asp-net-core-security-headers-guide/
* https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-7.0

---

### GitHub Actions
```shell
mkdir -p .github/workflows
touch .github/workflows/build_and_test.yml
```
----

### Microsoft Azure Application Insights
See: https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcorenew

* add `builder.Services.AddApplicationInsightsTelemetry();` to `Program.cs`
* add the `<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.8" />` dependency in the project
* add the connection string to your `appsettings.json`
* done :)

----

### Database
* Connecting a database

----

### Migration
Create Migration
```shell
cd PlaygroundDotNetAPI
# you only need to do these once
# dotnet tool install --global dotnet-ef
# dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add Initial
# dotnet ef migrations remove
```

Update Database / Run migration
```shell
dotnet ef database update
```

#### Seeding data into database
https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding

see the `OnModelCreating` in the DB Context

When you run the database update, it will create the data specified in the `OnModelCreating`

If you've added more data to `OnModelCreating`, you'll need to create an additional migration

Then run the update again


#### Starting fresh
```shell
dotnet ef database drop
dotnet migrations remove
# this only removes the last run migration
# to remove multiple migrations, run it as many times until there aren't any left
# then create the migration again
```

Notes: You can create multiple migrations, then finally run the database update. 
This will ensure ALL the data in the `OnModelCreating` method is seeded to the DB

---

### Federated Login

---

### Terms
| Terms      | Short Description                                                                          |
|------------|--------------------------------------------------------------------------------------------|
| Migrations | Basically DB as Code. Allows you to use your code as the source of truth for the DB Schema |

----

### References:
* https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net
* [Project Structure](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-7.0&tabs=visual-studio-mac#add-a-model-class)
* [DBContext](https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli)
* [Custom Logging for Endpoints](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs)
* [Output Caching](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/improving-performance-with-output-caching-cs)
* [Getting Started with Entity Framework Core](https://www.youtube.com/watch?v=JzfWpiowtqI)
* [Migrations Explained](https://www.youtube.com/watch?v=fl6r-9rQjns)
* [Seed DB](https://www.youtube.com/watch?v=z-Hll4Xddjs)
* [Sqlite & Entity Framework Core](https://www.youtube.com/watch?v=z-Hll4Xddjs)
* [Registering Services](https://www.youtube.com/watch?v=sSq3GtriFuM)
* [CORS](https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-7.0#np)
* [Microsoft Azure Application Insights](https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcorenew)

### TODO:
* when saving test artifacts, save to the computed dotnet version
* deploy to azure web app
* run docker
* test cors
* use caching
* test caching
* restrict by ip
* no build warnings in pipeline
* use allowed hosts
* versioning from pipeline
* singular pipeline
* try catch when we cannot connect to a db
* custom events in app insights
* multiple environments / slots