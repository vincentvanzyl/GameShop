using System.Net;
using GamesGlobal.Core.Exceptions;
using GamesGlobal.Utilities.Config;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesGlobal.Api.Attributes;

public class AuthorizeAdminKey : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!IsAuthorized(context))
        {
            throw new ApiObjectException("Unauthorized", HttpStatusCode.Unauthorized);
        }
        base.OnActionExecuting(context);
    }

    private bool IsAuthorized(ActionExecutingContext context)
    {
        var adminKey = GamesGlobalSettings.Instance.Security.StrongKey;
        var headerKey = context.HttpContext.Request.Headers["X-API-SECRET"].ToString();

        return adminKey == headerKey;
    }
}