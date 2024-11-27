using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PlaygroundDotNetAPI.Attributes;

namespace PlaygroundDotNetAPI.Controllers;

[ApiController]
[DisableApplicationInsightsActionFilter]
[Route("[controller]")]
public class EnvironmentController(IConfiguration configuration, IWebHostEnvironment hostEnvironment) : ControllerBase
{

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<object> Get()
    {
        var env = new
        {
            hostEnvironmentName = hostEnvironment.EnvironmentName,
            allowedOrigins = configuration.GetSection("AllowedOrigins").Get<List<string>>(),
            allowedHosts = configuration.GetValue<string>("AllowedHosts"),
            isProduction = hostEnvironment.IsProduction(),
            isDevelopment = hostEnvironment.IsDevelopment(),
            aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            dbType = configuration.GetRequiredSection("Db").GetValue<string>("Type"),
            defaultConnection = configuration.GetConnectionString("DefaultConnection")
        };
        return env;
    }
}
