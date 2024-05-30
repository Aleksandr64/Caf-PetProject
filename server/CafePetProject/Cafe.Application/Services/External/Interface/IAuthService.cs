using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Domain;
using Cafe.Domain.ResultModels;

namespace Cafe.Application.Services.Inteface;

public interface IAuthService
{
    public Task<Result<string>> RegisterUser(RegisterUserRequest user);
    public Task<Result<TokenResponse>> LoginUser(UserLoginRequest loginRequest);
    public Task<Result<TokenResponse>> GetNewAccessToken(TokenRequest tokenRequest);
    public Task<Result<string>> Logout(string accessToken);
}