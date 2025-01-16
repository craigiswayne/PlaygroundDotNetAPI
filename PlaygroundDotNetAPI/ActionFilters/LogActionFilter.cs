using System.Globalization;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlaygroundDotNetAPI.ActionFilters;

/**
 * Logs to Azure Application Insights for all request to ANY controller
 * Logs before an after the API Call
 */
public class LogActionFilter(TelemetryClient telemetryClient, ILogger<LogActionFilter> logger) : ActionFilterAttribute
{
    /**
     * Log details before the action executes
     */
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (IsDisabled(context)) return;

        TelemetryTrack("OnActionExecuting", context);
        base.OnActionExecuting(context);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (IsDisabled(context)) return;
        
        // logger.LogInformation("Action method {ActionName} has executed at {Time}",
        // context.ActionDescriptor.DisplayName, DateTime.UtcNow);
        TelemetryTrack("OnActionExecuted", context, new Dictionary<string, string?> {
            { "StatusCode", context.HttpContext.Response.StatusCode.ToString() },
        });

        if (context.Exception != null)
        {
            telemetryClient.TrackException(context.Exception);
        }
        
        base.OnActionExecuted(context);
    }

    /**
     * Log before the result is executed
     */
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (IsDisabled(context)) return;
        
        TelemetryTrack("OnResultExecuting", context);
        base.OnResultExecuting(context);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {
        if (IsDisabled(context)) return;
        
        TelemetryTrack("OnResultExecuted", context);
        base.OnResultExecuted(context);
    }

    /**
     * Checks if a controller has the [DisableLogActionFilter] Attribute
     */
    private static bool IsDisabled(FilterContext context)
    {
        var hasDisableAttribute = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(DisableLogActionFilter));
        return hasDisableAttribute;
    }

    private void TelemetryTrack(string customEventName, FilterContext context, Dictionary<string, string?>? additionalProperties = null)
    {
        var defaultProperties = new Dictionary<string, string?>
        {
            { "Controller", context.ActionDescriptor.RouteValues["controller"] },
            { "Action", context.ActionDescriptor.RouteValues["action"] },
            { "Timestamp", DateTime.UtcNow.ToString(CultureInfo.InvariantCulture) }
        };
        
        var allProperties = additionalProperties != null
            ? defaultProperties.Concat(additionalProperties).ToDictionary(kvp => kvp.Key, kvp => kvp.Value) 
            : defaultProperties;

        logger.LogInformation("Action method {ActionName} is executing at {Time}",
            context.ActionDescriptor.DisplayName, DateTime.UtcNow);

        telemetryClient.TrackEvent(customEventName, allProperties);
    }
}
