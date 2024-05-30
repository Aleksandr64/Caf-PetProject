using Cafe.Domain;
using Cafe.Infrastructure.Context;
using Cafe.Infrustructure.Context;
using Cafe.Infrustructure.Repositoriy.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrustructure.Repositoriy;

public class TokenRepository : ITokenRepository
{
    private readonly CafeDbContext _dbContext;

    public TokenRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateTokenEntity(Token tokenEntity)
    {
        await _dbContext.Tokens.AddAsync(tokenEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Token> FindTokenEntityByUserNameAsync(string userName)
    {
        var tokenEntity = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.UserName == userName);
        return tokenEntity;
    }

    public async Task ChangeRefreshToken(Token token)
    {
        _dbContext.Tokens.Update(token);
        await _dbContext.SaveChangesAsync();
    }
}