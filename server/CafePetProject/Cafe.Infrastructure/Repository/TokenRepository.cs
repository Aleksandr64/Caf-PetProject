using Cafe.Domain;
using Cafe.Infrastructure.Context;
using Cafe.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly CafeDbContext _dbContext;

    public TokenRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddToken(Token token)
    {
        await _dbContext.Tokens.AddAsync(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Token> FindTokenByUser(int userId)
    {
        var tokenEntity = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.UserId == userId);
        return tokenEntity;
    }
    
    public async Task<Token> FindTokenByRefreshToken(string refreshToken)
    {
        var token = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        return token;
    }

    public async Task UpdateRefreshToken(Token token)
    {
        _dbContext.Tokens.Update(token);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteTokenByRefreshToken(string refreshToken)
    {
        _dbContext.Tokens.Remove(_dbContext.Tokens.Single(t => t.RefreshToken == refreshToken));
        await _dbContext.SaveChangesAsync();
    }
}