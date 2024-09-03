using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace PlaygroundDotNetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EnvironmentController(IConfiguration configuration, IWebHostEnvironment hostEnvironment) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<object> Get()
    {
        var env = new
        {
            hostEnvironmentName = _hostEnvironment.EnvironmentName,
            allowedOrigins = _configuration.GetSection("AllowedOrigins").Get<List<string>>(),
            allowedHosts = _configuration.GetValue<string>("AllowedHosts"),
            isProduction = _hostEnvironment.IsProduction(),
            isDevelopment = _hostEnvironment.IsDevelopment(),
            aspNetCoreEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            dbType = _configuration.GetRequiredSection("Db").GetValue<string>("Type"),
            defaultConnection = _configuration.GetConnectionString("DefaultConnection")
        };
        return env;
    }
}
