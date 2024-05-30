using Cafe.Domain;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe.Web.Attribute;

public class UserNameInjectionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ActionArguments.ContainsKey("userName") || context.ActionArguments["userName"] == null)
        {
            if (context.HttpContext.Items.TryGetValue(HttpContextItems.USERNAME_ITEM, out var userName))
            {
                context.ActionArguments["userName"] = userName;
            }
        }
        base.OnActionExecuting(context);
    }
}
