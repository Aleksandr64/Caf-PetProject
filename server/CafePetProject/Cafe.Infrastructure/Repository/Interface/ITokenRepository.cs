using Cafe.Domain;

namespace Cafe.Infrastructure.Repository.Interface;

public interface ITokenRepository
{
    public Task AddToken(Token token);
    public Task<Token> FindTokenByUser(int userId);
    public Task<Token> FindTokenByRefreshToken(string refreshToken);
    public Task UpdateRefreshToken(Token token);
    public Task DeleteTokenByRefreshToken(string refreshToken);
}