using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.DTOs.OrderDTOs.Response;

namespace Cafe.Application.Services.External.Interface;

public interface IOrderService
{
    public Task AddNewOrder(AddOrderRequest newOrder);
    public Task<IEnumerable<OrderResponse>> GetOrderByUserName(string? userName, int page, int pageSize);
}