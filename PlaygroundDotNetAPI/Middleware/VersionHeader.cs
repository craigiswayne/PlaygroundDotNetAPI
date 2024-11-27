namespace PlaygroundDotNetAPI.Middleware;

public class VersionHeaderMiddleware(RequestDelegate next, IConfiguration configuration)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Version", configuration.GetValue<string>("Version"));
        await next(context);
    }
}

public static class VersionHeaderMiddlewareExtensions
{
    public static IApplicationBuilder AddVersionHeaderToResponses(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<VersionHeaderMiddleware>();
    }
}