using Cafe.Domain;
using Cafe.Infrastructure.Context;
using Cafe.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly CafeDbContext _dbContext;

    public UserRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CheckByNameAsync(string userName)
    {
        return await _dbContext.Users.AnyAsync(x => x.UserName == userName);
    }

    public async Task<bool> CheckByEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(x => x.Email == email);
    }

    public async Task<User> FindByNameAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
        return user;
    }

    public async Task<User> FindUserById(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        return user;
    }
    
    public async Task CreateUserAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}