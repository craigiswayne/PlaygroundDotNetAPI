using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PlaygroundDotNetAPI.ActionFilters;

/**
* <summary>
* Checks if there is a bearer token in the headers by checking both:
*  > "Bearer"
*  > "bearer"
*
* If it doesn't exist, it will terminate execution prematurely with a 403 result
*
* If it exists, it will ensure the "Bearer" header is set so that it can be used
* confidently throughout the app
* </summary>
*
* <example>[MustHaveBearer]</example>
*/
public class MustHaveBearer : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Headers.ContainsKey("bearer"))
        {
            context.HttpContext.Request.Headers["Bearer"] = context.HttpContext.Request.Headers["bearer"];
        }

        if (string.IsNullOrEmpty(context.HttpContext.Request.Headers["Bearer"]))
        {
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        }
    }
}