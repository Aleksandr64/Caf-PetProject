using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.DTOs.OrderDTOs.Response;
using Cafe.Application.Mappers;
using Cafe.Application.Services.Inteface;
using Cafe.Application.Validations.Order;
using Cafe.Domain;
using Cafe.Domain.ResultModels;
using Cafe.Infrustructure.Repositoriy.Interface;
using FluentValidation;
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
    
    public async Task<Result<string>> AddNewOrder(AddOrderRequest newOrder, string userName)
    {
        var validationResult = await new AddOrderValidation().ValidateAsync(newOrder);
        if (!validationResult.IsValid)
        {
            return new BadRequestResult<string>(validationResult.Errors);
        }

        var token = await _tokenRepository.FindTokenEntityByUserNameAsync(userName);
        
        Order order;
        if (token == null || token.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            order = newOrder.MapOrderAddRequest(null);
        }

        order = newOrder.MapOrderAddRequest(userName);
        
        await _orderRepository.AddOrder(order);
        
        return new SuccessResult<string>(null);
    }

    public async Task<Result<IEnumerable<OrderResponse>>> GetOrderByUserName(string userName, int page, int pageSize)
    {
        if (userName == null)
        {
            return new BadRequestResult<IEnumerable<OrderResponse>>("User Name is empty!");
        }

        var ordersUser = _orderRepository.FindUserOrders(userName);

        if (ordersUser.IsNullOrEmpty())
        {
            return new BadRequestResult<IEnumerable<OrderResponse>>("User don't have Orders or Invalid User!");
        }

        var totalProducts = await ordersUser.CountAsync();
        var orderPaginate = await ordersUser.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        
        IEnumerable<OrderResponse> orderResponse = orderPaginate.Select(x => x.MapOrderToResponse());
        
        return new SuccessResult<IEnumerable<OrderResponse>>(orderResponse);
    }
}