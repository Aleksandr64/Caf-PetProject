using System.Net;
using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.DTOs.OrderDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.External.Interface;
using Cafe.Application.Validations.Order;
using Cafe.Domain;
using Cafe.Domain.Exceptions;
using Cafe.Infrastructure.Repository.Interface;
using Cafe.Infrustructure.Repositoriy.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Cafe.Application.Services.External;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ITokenRepository _tokenRepository;

    public OrderService(IOrderRepository orderRepository, ITokenRepository tokenRepository)
    {
        _orderRepository = orderRepository;
        _tokenRepository = tokenRepository;
    }
    
    public async Task AddNewOrder(AddOrderRequest newOrder)
    {
        var validationResult = await new AddOrderValidation().ValidateAsync(newOrder);
        if (!validationResult.IsValid)
        {
            throw new ApiException(HttpStatusCode.BadRequest, "Validation error", validationResult.Errors);
        }

        var order = newOrder.MapOrderAddRequest();
        
        await _orderRepository.AddOrder(order);
    }

    public async Task<IEnumerable<OrderResponse>> GetOrderByUserName(string userName, int page, int pageSize)
    {
        if (userName == null)
        {
            throw new ApiException(HttpStatusCode.BadRequest,"User Name is empty!");
        }

        var ordersUser = _orderRepository.FindUserOrders(userName);

        if (ordersUser.IsNullOrEmpty())
        {
            throw new ApiException(HttpStatusCode.BadRequest,"User don't have Orders or Invalid User!");
        }

        var totalProducts = await ordersUser.CountAsync();
        var orderPaginate = await ordersUser.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        return orderPaginate.Select(x => x.MapOrderToResponse());
    }
}