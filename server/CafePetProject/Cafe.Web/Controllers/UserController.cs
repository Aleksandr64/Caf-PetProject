using Cafe.Application.Services.External.Interface;
using Cafe.Web.Attribute;
using Cafe.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class UserController : BaseApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [AuthorizationRole]
    [UserNameInjection]
    public async Task<IActionResult> GetUserData(string? userName)
    {
        var result = await _userService.GetUserByName(userName);
        return this.GetResponse(result);
    }
}