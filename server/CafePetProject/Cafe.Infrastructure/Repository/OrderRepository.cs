using Cafe.Domain;
using Cafe.Infrastructure.Context;
using Cafe.Infrustructure.Context;
using Cafe.Infrustructure.Repositoriy.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cafe.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly CafeDbContext _dbContext;

    public OrderRepository(CafeDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddOrder(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
    }
    public IQueryable<Order> FindUserOrders(string userName)
    {
        var orders = _dbContext.Orders.Include(o => o.OrderItems).Where(x => x.UserName == userName);
        
        
        
        return orders;
    }
    
}