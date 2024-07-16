using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;

namespace Cafe.Application.Services.External.Interface;

public interface IUserService
{
    public Task<UserDataResponse> GetUserByName(string? userName);
    public Task<UserDataResponse> ChangeUserData(ChangeUserDataRequest changeUserDate, string userName);
}