﻿using System.Collections.Specialized;

namespace playground_dotnet_api.Middleware;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        NameValueCollection headersToAdd = new NameValueCollection();
        headersToAdd["Access-Control-Allow-Origin"] = "*";
        headersToAdd["Content-Security-Policy"] = "default-src 'self';";
        headersToAdd["Permissions-Policy"] = "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()";
        headersToAdd["Referrer-Policy"] = "same-origin";
        headersToAdd["X-Content-Type-Options"] = "nosniff";
        headersToAdd["X-Frame-Options"] = "DENY";
        headersToAdd["X-Permitted-Cross-Domain-Policies"] = "none";
        headersToAdd["X-Xss-Protection"] = "1; mode=block";

        foreach (string header in headersToAdd)
        {
            if (context.Response.Headers.ContainsKey(header)){
                continue;
            }

            context.Response.Headers.Add(header, headersToAdd[header]);
        }

        string[] headersToRemove = new string[]{
            "X-Powered-By",
        };

        foreach (string header in headersToRemove)
        {
            if (!context.Response.Headers.ContainsKey(header)){
                continue;
            }
            context.Response.Headers.Remove(header);
        }


        await _next(context);
    }
}

public static class SecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityHeadersMiddleware>();
    }
}