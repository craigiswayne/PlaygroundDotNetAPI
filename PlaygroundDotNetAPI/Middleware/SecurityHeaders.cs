using System.Collections.Specialized;

namespace PlaygroundDotNetAPI.Middleware;

public class SecurityHeadersMiddleware(RequestDelegate next, IConfiguration configuration)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var headersToAdd = new NameValueCollection
        {
            ["Access-Control-Allow-Origin"] = configuration["AllowedOrigins"],
            ["Content-Security-Policy"] = "default-src 'self';",
            ["Permissions-Policy"] = "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()",
            ["Referrer-Policy"] = "same-origin",
            ["X-Content-Type-Options"] = "nosniff",
            ["X-Frame-Options"] = "DENY",
            ["X-Permitted-Cross-Domain-Policies"] = "none",
            ["X-Xss-Protection"] = "1; mode=block"
        };

        foreach (string header in headersToAdd)
        {
            if (context.Response.Headers.ContainsKey(header)){
                continue;
            }

            context.Response.Headers.Append(header, headersToAdd[header]);
        }

        string[] headersToRemove =
        [
            "X-Powered-By"
        ];

        foreach (var header in headersToRemove)
        {
            if (!context.Response.Headers.ContainsKey(header)){
                continue;
            }
            context.Response.Headers.Remove(header);
        }


        await next(context);
    }
}

public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityHeadersMiddleware>();
    }
}