using Cafe.Web.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class TestController : BaseApiController
{
    [HttpGet]
    public IActionResult FirstTestEndPoint()
    {
        return Ok(new { message = "End point work!" });
    }


    [HttpGet]
    [AuthorizationRole]
    [UserNameInjection]
    public IActionResult TestAuthAttribute(string? userName)
    {
        return Ok(new { message = $"User: {userName}  Authorize!" });
    }
}