using System.Net;
using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.External.Interface;
using Cafe.Application.Validations.User;
using Cafe.Domain.Exceptions;
using Cafe.Infrastructure.Repository.Interface;

namespace Cafe.Application.Services.External;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDataResponse> GetUserByName(string userName)
    {
        var user = await _userRepository.FindByNameAsync(userName);
        if (user == null)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "User does not exist in the database");
        }
        return user.MapUserDataResponse();
    }

    public async Task<UserDataResponse> ChangeUserData(ChangeUserDataRequest changeUserDate, string userName)
    {
        var validationResult = await new PutUserValidation().ValidateAsync(changeUserDate);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation Errors", validationResult.Errors);
        }
        var user = await _userRepository.FindByNameAsync(userName);
        var userUpdated = changeUserDate.MapChangeUserToUser(user);
        await _userRepository.UpdateUserAsync(userUpdated);
        return userUpdated.MapUserDataResponse();
    }
    
}