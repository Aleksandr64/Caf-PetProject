using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.External.Interface;
using Cafe.Domain.ResultModels;
using Cafe.Infrastructure.Repository.Interface;

namespace Cafe.Application.Services.External;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserDataResponse>> GetUserByName(string userName)
    {
        var user = await _userRepository.FindByNameAsync(userName);
        if (user == null)
        {
            return new BadRequestResult<UserDataResponse>("User does not exist in the database");
        }
        var userResponse = user.MapUserDataResponse();
        return new SuccessResult<UserDataResponse>(userResponse);
    }
}