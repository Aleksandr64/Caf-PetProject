using Cafe.Web.Attribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class TestController : BaseApiController
{
    [HttpGet]
    public IActionResult FirstTestEndPoint()
    {
        var userAgent = Request.Headers["User-Agent"].ToString();
        return Ok(new { message = userAgent });
    }


    [HttpGet]
    [Authorize]
    [InjectUserName]
    public IActionResult TestAuthAttribute(string? userName)
    {
        return Ok(new { message = $"User: {userName}  Authorize!" });
    }
}