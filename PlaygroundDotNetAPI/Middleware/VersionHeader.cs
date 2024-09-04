using System.Collections.Specialized;

namespace PlaygroundDotNetAPI.Middleware;

public class VersionHeaderMiddleware
{
    private readonly RequestDelegate _next;
    public IConfiguration _configuration;

    public VersionHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Version", _configuration["Version"]);
        await _next(context);
    }
}

public static class VersionHeaderMiddlewareExtensions
{
    public static IApplicationBuilder UseVersionHeader(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VersionHeaderMiddleware>();
    }
}