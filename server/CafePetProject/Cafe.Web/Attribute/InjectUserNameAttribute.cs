using Cafe.Application.Services.Internal.Interface;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe.Web.Attribute;

public class InjectUserNameAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();

        if (tokenService != null)
        {
            var httpContext = context.HttpContext;

            if (!context.ActionArguments.ContainsKey("userName") || context.ActionArguments["userName"] == null)
            {
                if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
                {
                    var token = authHeaderValue.ToString().Replace("Bearer ", "");

                    if (!string.IsNullOrEmpty(token))
                    {
                        var principal = tokenService.GetPrincipalFromExpiredToken(token);
                        var userName = tokenService.GetUsernameFromToken(principal);

                        if (!string.IsNullOrEmpty(userName))
                        {
                            context.ActionArguments["userName"] = userName;
                        }
                    }
                }
            }
        }

        base.OnActionExecuting(context);
    }
}