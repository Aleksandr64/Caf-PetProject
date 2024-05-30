using Cafe.Domain;

namespace Cafe.Infrustructure.Repositoriy.Interface;

public interface ITokenRepository
{
    public Task CreateTokenEntity(Token tokenEntity);
    public Task<Token> FindTokenEntityByUserNameAsync(string userName);
    public Task ChangeRefreshToken(Token token);
}