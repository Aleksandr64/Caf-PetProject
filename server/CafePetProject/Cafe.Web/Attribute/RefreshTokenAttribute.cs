using System.Net;
using Cafe.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe.Web.Attribute;

public class RefreshTokenAttribute: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Cookies.TryGetValue("RefreshToken", out string refreshToken))
        {
            context.HttpContext.Items["RefreshToken"] = refreshToken;
        }
        else
        {
            throw new ApiException(HttpStatusCode.Unauthorized, "Refresh token Not Found!");
        }
            
        base.OnActionExecuting(context);
    }
}