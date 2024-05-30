using Cafe.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cafe.Web.Attribute;

public class AuthorizationRoleAttribute : ActionFilterAttribute, IAuthorizationFilter
{
    private readonly string[] _roles;

    public AuthorizationRoleAttribute()
    {
        _roles = Enum.GetNames(typeof(UserRolesEnum));
    }

    public AuthorizationRoleAttribute(params UserRolesEnum[] rolesEnums)
    {
        List<string> roles = rolesEnums.Select(role => role.ToString()).ToList();
        _roles = roles.ToArray();
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var alloAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (alloAnonymous)
        {
            return;
        }

        var userRole = context.HttpContext.Items[HttpContextItems.ROLES_ITEM];

        if (userRole != null)
        {
            bool checkUserRole = _roles.Any(x => x == (string)userRole);
            if (!checkUserRole)
            {
                context.Result = new JsonResult(new { message = "This user does not have access to this Data." })
                    { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }

        if (userRole == null)
        {
            context.Result = new JsonResult(new { message = "This user don't have access token or token is expired." })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

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