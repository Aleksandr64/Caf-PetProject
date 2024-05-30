using Cafe.Domain;

namespace Cafe.Infrastructure.Repository.Interface;

public interface IUserRepository
{
    public Task<bool> CheckByNameAsync(string userName);
    public Task<bool> CheckByEmailAsync(string email);
    public Task<User> FindByNameAsync(string userName);
    public Task CreateUserAsync(User user);
}