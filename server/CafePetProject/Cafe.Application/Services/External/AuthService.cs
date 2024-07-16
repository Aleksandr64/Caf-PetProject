using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cafe.Application.DTOs.UserDTOs;
using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.External.Interface;
using Cafe.Application.Services.Internal.Interface;
using Cafe.Application.Validations.Auth;
using Cafe.Domain;
using Cafe.Domain.Constant;
using Cafe.Domain.Exceptions;
using Cafe.Infrastructure.Repository.Interface;
using Microsoft.IdentityModel.Tokens;

namespace Cafe.Application.Services.External;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenRepository tokenRepository,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> LoginUser(UserLoginRequest loginRequest)
    {
        var validationResult = await new LoginValidator().ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation Errors", validationResult.Errors);
        }
        
        var user = await _userRepository.FindByNameAsync(loginRequest.UserName);

        if (user == null)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "UserName or Password incorrect!");
        }

        var userPasswordCheck = VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt);

        if (!userPasswordCheck)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "UserName or Password incorrect!");
        }

        var authClaims = new List<Claim>
        {
            new Claim("userName", user.UserName),
            new Claim("role", user.Role)
        };

        var accessToken = _tokenService.GenerateAccessToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        
        var userToken = new Token()
        {
            RefreshToken = refreshToken,
            RefreshTokenExpiredTime = DateTime.UtcNow.AddDays(7),
            DateCreate = DateTime.UtcNow,
            DateUpdate = DateTime.UtcNow,
            UserId = user.Id
        };

        await _tokenRepository.AddToken(userToken);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
    
    public async Task RegisterUser(RegisterUserRequest userRegistration)
    {
        var validationResult = await new RegistrationValidator().ValidateAsync(userRegistration);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation Errors", validationResult.Errors);
        }
        
        var userNameCheck = await _userRepository.CheckByNameAsync(userRegistration.UserName);

        if (userNameCheck)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "A User with such a Username exists.");
        }

        var emailCheck = await _userRepository.CheckByEmailAsync(userRegistration.Email);

        if (emailCheck)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "A User with such a Email exists.");
        }

        var password = HashPasswordCreate(userRegistration.Password);

        var user = userRegistration.MapUserRequest(password, UserRoles.User);

        await _userRepository.CreateUserAsync(user);
    }

    public async Task<TokenResponse> GetNewAccessToken(string refreshToken)
    {
        var token = await _tokenRepository.FindTokenByRefreshToken(refreshToken);

        if (token == null)
        {
            throw new ApiException(HttpStatusCode.Unauthorized, "Token has Expired.");
        }
        
        if (token.RefreshTokenExpiredTime <= DateTime.UtcNow)
        {
            await _tokenRepository.DeleteTokenByRefreshToken(refreshToken);
            throw new ApiException(HttpStatusCode.Unauthorized, "Token has Expired.");
        }

        var user = await _userRepository.FindUserById(token.UserId);
        
        var authClaims = new List<Claim>
        {
            new Claim("userName", user.UserName),
            new Claim("role", user.Role)
        };

        var newAccessToken = _tokenService.GenerateAccessToken(authClaims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        token.RefreshToken = newRefreshToken;
        token.RefreshTokenExpiredTime = DateTime.UtcNow.AddDays(7);

        await _tokenRepository.UpdateRefreshToken(token);
        
        return new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }

    public async Task Logout(string refreshToken)
    {
        if (refreshToken.IsNullOrEmpty())
        {
            throw new ApiException(HttpStatusCode.Unauthorized, "Refresh token don't exist");
        }
        await _tokenRepository.DeleteTokenByRefreshToken(refreshToken);
    }
    
    private static Password HashPasswordCreate(string password)
    {
        const int keySize = 64;
        const int iterations = 350000;
        var hashAlgorithm = HashAlgorithmName.SHA512;

        var salt = RandomNumberGenerator.GetBytes(keySize);
        var bytePassword = Encoding.UTF8.GetBytes(password);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            bytePassword,
            salt,
            iterations,
            hashAlgorithm,
            keySize);

        return new Password
        {
            hashPassword = Convert.ToHexString(hash),
            saltPassword = Convert.ToHexString(salt)
        };
    }
    private bool VerifyPassword(string password, string hash, string salt)
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        var saltByte = Convert.FromHexString(salt);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, saltByte, iterations, hashAlgorithm, keySize);

        var hashPassword = CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));

        return hashPassword;
    }
}