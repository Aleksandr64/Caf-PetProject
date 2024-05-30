using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Cafe.Application.DTOs.UserDTOs;
using Cafe.Application.DTOs.UserDTOs.Request;
using Cafe.Application.DTOs.UserDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.Inteface;
using Cafe.Application.Services.Internal.Interface;
using Cafe.Application.Validations.Auth;
using Cafe.Domain;
using Cafe.Domain.ResultModels;
using Cafe.Infrastructure.Repository.Interface;
using Cafe.Infrustructure.Repositoriy.Interface;

namespace Cafe.Application.Services.External;

//TODO Authorization on more device
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

    public async Task<Result<TokenResponse>> LoginUser(UserLoginRequest loginRequest)
    {
        var validationResult = await new LoginValidator().ValidateAsync(loginRequest);
        if (!validationResult.IsValid)
        {
            return new BadRequestResult<TokenResponse>(validationResult.Errors);
        }
        
        var user = await _userRepository.FindByNameAsync(loginRequest.UserName);

        if (user == null)
        {
            return new BadRequestResult<TokenResponse>("UserName or Password incorrect!");
        }

        var userPasswordCheck = VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt);

        if (!userPasswordCheck)
        {
            return new BadRequestResult<TokenResponse>("UserName or Password incorrect!");
        }

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var accessToken = _tokenService.GenerateAccessToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var userRefreshToken = await _tokenRepository.FindTokenEntityByUserNameAsync(user.UserName);

        userRefreshToken.RefreshToken = refreshToken;
        userRefreshToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _tokenRepository.ChangeRefreshToken(userRefreshToken);

        return new SuccessResult<TokenResponse>(new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
    
    public async Task<Result<string>> RegisterUser(RegisterUserRequest userRegistration)
    {
        var validationResult = await new RegistrationValidator().ValidateAsync(userRegistration);
        if (!validationResult.IsValid)
        {
            return new BadRequestResult<string>(validationResult.Errors);
        }
        
        var userNameCheck = await _userRepository.CheckByNameAsync(userRegistration.UserName);

        if (userNameCheck)
        {
            return new BadRequestResult<string>("A User with such a Username exists.");
        }

        var emailCheck = await _userRepository.CheckByEmailAsync(userRegistration.Email);

        if (emailCheck)
        {
            return new BadRequestResult<string>("A User with such a Email exists.");
        }

        var password = HashPasswordCreate(userRegistration.Password);

        var user = userRegistration.MapUserRequest(password, UserRolesEnum.User);

        await _userRepository.CreateUserAsync(user);

        await _tokenRepository.CreateTokenEntity(new Token
        {
            UserName = user.UserName
        });
        
        return new SuccessResult<string>(null);
    }

    public async Task<Result<TokenResponse>> GetNewAccessToken(TokenRequest tokenRequest)
    {
        string accessToken = tokenRequest.AccessToken;
        string refreshToken = tokenRequest.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return new BadRequestResult<TokenResponse>("Invalid Token");
        }

        var userName = _tokenService.GetUsernameFromToken(principal);
        var userRefreshToken = await _tokenRepository.FindTokenEntityByUserNameAsync(userName);

        if (userRefreshToken == null)
        {
            return new NotFoundResult<TokenResponse>("User not found!");
        }
        
        if (userRefreshToken.RefreshToken != refreshToken || userRefreshToken.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            await LogoutOperation(principal);
            return new UnAuthorizedResult<TokenResponse>("The Refresh token has expired");
        }
        
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _tokenService.GetUsernameFromToken(principal)),
            new Claim(ClaimTypes.Role, _tokenService.GetRoleFromToken(principal))
        };

        var newAccessToken = _tokenService.GenerateAccessToken(authClaims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        userRefreshToken.RefreshToken = newRefreshToken;
        userRefreshToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        await _tokenRepository.ChangeRefreshToken(userRefreshToken);
        
        return new SuccessResult<TokenResponse>(new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    public async Task<Result<string>> Logout(string accessToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return new NotFoundResult<string>("Invalid Principal Token");
        }
        
        await LogoutOperation(principal);

        return new SuccessResult<string>(null);
    }

    private async Task LogoutOperation(ClaimsPrincipal principal)
    {
        var userName = _tokenService.GetUsernameFromToken(principal);

        var token = await _tokenRepository.FindTokenEntityByUserNameAsync(userName);

        token.RefreshToken = string.Empty;
        token.RefreshTokenExpiryTime = default;

        await _tokenRepository.ChangeRefreshToken(token);
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