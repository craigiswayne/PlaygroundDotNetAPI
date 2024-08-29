# playground-dotnet-api

## Endpoints
* https://localhost:7068/swagger/index.html

---

## Getting Started
```
dotnet clean && dotnet nuget locals all --clear
dotnet restore
dotnet build --no-restore
PROJECT_FILE="playground-dotnet-api/playground-dotnet-api.csproj"
dotnet watch run --project=$PROJECT_FILE
```

```
# Clean
dotnet clean && dotnet nuget locals all --clear
# Restore dependencies
dotnet restore
# Build
dotnet build --no-restore
# Test
dotnet test --no-build --verbosity normal
# Run
PROJECT_FILE="playground-dotnet-api/playground-dotnet-api.csproj"
dotnet run --project=$PROJECT_FILE
# or for hot reload
dotnet watch run --project=$PROJECT_FILE
```

---

### Scaffolding
```shell
mkdir playground-dotnet-api
cd playground-dotnet-api
dotnet new webapi --framework net8.0
git init
dotnet new gitignore
mkdir -p playground-dotnet-api-tests
dotnet new nunit
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
using playground_dotnet_api.Middleware;
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

### Migration
Create Migration
```shell
cd playground-dotnet-api
dotnet tool install --global dotnet-ef
dotnet ef migrations add Initial -o Migrations --context MyDbContextSqLite -v
```

Update Migration
```shell
dotnet ef database update --context MyDbContextSqLite -v
```

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