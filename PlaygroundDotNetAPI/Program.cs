using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using PlaygroundDotNetAPI.Data;
using PlaygroundDotNetAPI.Middleware;
using PlaygroundDotNetAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionStringSqlite = builder.Configuration.GetConnectionString("DefaultConnection");

var connectionType = builder.Configuration.GetSection("Db").GetValue<string>("Type");
if (connectionType == "sqlite")
{
    builder.Services.AddDbContext<MyDbContextSqLite>(options => options.UseSqlite(connectionStringSqlite));
}

builder.Services.AddScoped<IPokedexService, PokedexService>();

// Add services to the container.
builder.Services.AddControllers();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSecurityHeaders();
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
