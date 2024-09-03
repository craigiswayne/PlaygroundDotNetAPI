using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Middleware;
using PlaygroundDotNetAPI.Services;

var builder = WebApplication.CreateBuilder(args);

string[] allowedOrigins = builder.Configuration.GetRequiredSection("AllowedOrigins").Get<string[]>() ?? [];

//if (allowedOrigins.Length == 0)
//{
//    throw new Exception("No AllowedOrigins specified");
//}

var connectionStringSqlite = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionType = builder.Configuration.GetRequiredSection("Db").GetValue<string>("Type");
if (connectionType == "sqlite")
{
    builder.Services.AddDbContext<MyDbContextSqLite>(options => options.UseSqlite(connectionStringSqlite));
}

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

var corsPolicy = "DefaultPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
        policy =>
        {
            policy.WithOrigins(allowedOrigins);
        });
});

builder.Services.AddControllers();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecurityHeaders();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(corsPolicy);
app.UseAuthorization();

app.MapControllers();

app.Run();
