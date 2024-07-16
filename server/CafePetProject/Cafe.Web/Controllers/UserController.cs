using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.Services.External.Interface;
using Cafe.Web.Attribute;
using Microsoft.AspNetCore.Authorization;
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
    [InjectUserName]
    public async Task<IActionResult> GetUserData(string? userName)
    {
        var result = await _userService.GetUserByName(userName);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    [InjectUserName]
    public async Task<IActionResult> ChangeUserData(ChangeUserDataRequest userDataRequest, string? userName)
    {
        var result = await _userService.ChangeUserData(userDataRequest, userName);
        return Ok(result);
    }
}