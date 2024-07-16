using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.Services.External.Interface;
using Cafe.Web.Attribute;
using Cafe.Web.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
    {
        var result = await _authService.LoginUser(userLogin);
        
        var cookieOptions = HttpOptions.RefreshTokenCookieOptions();
        
        Response.Cookies.Append("RefreshToken", result.RefreshToken, cookieOptions);
        
        return Ok(result.AccessToken);
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest registerUser)
    {
        await _authService.RegisterUser(registerUser);
        return NoContent();
    }

    [HttpPost]
    [RefreshToken]
    public async Task<IActionResult> GetNewAccessToken()
    {
        var refreshToken = HttpContext.Items["RefreshToken"] as string;
        
        var result = await _authService.GetNewAccessToken(refreshToken);

        var cookieOptions = HttpOptions.RefreshTokenCookieOptions();
        
        Response.Cookies.Append("RefreshToken", result.RefreshToken, cookieOptions);
        
        return Ok(result.AccessToken);
    }

    [HttpPost]
    [RefreshToken]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = HttpContext.Items["RefreshToken"] as string;
        
        await _authService.Logout(refreshToken);
        
        return NoContent();
    }
}