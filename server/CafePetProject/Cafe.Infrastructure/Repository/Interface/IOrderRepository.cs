using Cafe.Domain;

namespace Cafe.Infrustructure.Repositoriy.Interface;

public interface IOrderRepository
{
    public Task AddOrder(Order order);
    public IQueryable<Order> FindUserOrders(string userName);
}