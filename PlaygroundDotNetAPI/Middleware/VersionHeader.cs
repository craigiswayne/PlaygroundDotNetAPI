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
        NameValueCollection headersToAdd = new NameValueCollection();
        headersToAdd["X-Version"] = _configuration["Version"];

        foreach (string header in headersToAdd)
        {
            if (context.Response.Headers.ContainsKey(header)){
                continue;
            }

            context.Response.Headers.Append(header, headersToAdd[header]);
        }

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