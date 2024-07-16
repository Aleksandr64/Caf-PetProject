using Cafe.Domain;

namespace Cafe.Infrastructure.Repository.Interface;

public interface IUserRepository
{
    public Task<bool> CheckByNameAsync(string userName);
    public Task<bool> CheckByEmailAsync(string email);
    public Task<User> FindByNameAsync(string userName);
    public Task<User> FindUserById(int id);
    public Task CreateUserAsync(User user);
    public Task UpdateUserAsync(User user);
}