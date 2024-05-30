using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Domain.ResultModels;

namespace Cafe.Application.Services.External.Interface;

public interface IUserService
{
    public Task<Result<UserDataResponse>> GetUserByName(string userName);
}