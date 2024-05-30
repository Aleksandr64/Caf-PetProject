using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.DTOs.OrderDTOs.Response;
using Cafe.Domain.ResultModels;

namespace Cafe.Application.Services.Inteface;

public interface IOrderService
{
    public Task<Result<string>> AddNewOrder(AddOrderRequest newOrder, string userName);
    public Task<Result<IEnumerable<OrderResponse>>> GetOrderByUserName(string userName, int page, int pageSize);
}