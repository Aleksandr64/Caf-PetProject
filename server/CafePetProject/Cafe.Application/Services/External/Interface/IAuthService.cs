using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;

namespace Cafe.Application.Services.External.Interface;

public interface IAuthService
{
    public Task RegisterUser(RegisterUserRequest user);
    public Task<TokenResponse> LoginUser(UserLoginRequest loginRequest);
    public Task<TokenResponse> GetNewAccessToken(string refreshToken);
    public Task Logout(string refreshToken);
}