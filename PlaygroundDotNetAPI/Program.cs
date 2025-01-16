using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using PlaygroundDotNetAPI.ActionFilters;
using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Middleware;
using PlaygroundDotNetAPI.Services;

var builder = WebApplication.CreateBuilder(args);
// HTTP Logging Part 1/2
builder.Services.AddHttpLogging(o => { });

var allowedOrigins = builder.Configuration.GetRequiredSection("AllowedOrigins").Get<string[]>() ?? [];
if (allowedOrigins.Length == 0)
{
    throw new Exception("No AllowedOrigins specified");
}

var connectionStringSqlite = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Default' not found.");
var connectionType = builder.Configuration.GetRequiredSection("Db").GetValue<string>("Type");
if (connectionType == "sqlite")
{
    builder.Services.AddDbContext<MyDbContextSqLite>(options => options.UseSqlite(connectionStringSqlite));
}

builder.Services.Configure<RouteOptions>(options =>
{
   options.LowercaseUrls = true;
});

builder.Services.AddScoped<IPokedexService, PokedexService>();

builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

const string corsPolicy = "DefaultPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins(allowedOrigins);
        });
});

// The following line enables Application Insights telemetry collection.
var appInsightsConnectionString = builder.Configuration.GetRequiredSection("ApplicationInsights").GetValue<string>("ConnectionString");
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = appInsightsConnectionString;
    options.EnableDebugLogger = true;  // Get real-time logs
    options.EnableAdaptiveSampling = false;  // Keep all dat data, no sampling here
});


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApplicationInsightsActionFilter>(); // Globally applying the filter
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
    options.MaxAge = TimeSpan.FromDays(180);
});

var app = builder.Build();
// HTTP Logging Part 2/2
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecurityHeaders();
app.AddVersionHeaderToResponses();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseRouting();
// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
app.UseCors(corsPolicy);
app.UseAuthorization();

app.MapControllers();

// Split main translation file into individual files

app.Run();
