using Cafe.Application.DTOs.UserDTOs;
using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Domain;

namespace Cafe.Application.Mappers;

public static class UserMapper
{
    public static User MapUserRequest(this RegisterUserRequest userRequest, Password password, UserRolesEnum userRoles)
    {
        return new User
        {
            FirstName = userRequest.FirstName,
            LastName = userRequest.LastName,
            UserName = userRequest.UserName,
            Email = userRequest.Email,
            PhoneNumber = userRequest.PhoneNumber,
            PasswordHash = password.hashPassword,
            PasswordSalt = password.saltPassword,
            Role = userRoles.ToString(),
        };
    }

    public static UserDataResponse MapUserDataResponse(this User user)
    {
        return new UserDataResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }
}