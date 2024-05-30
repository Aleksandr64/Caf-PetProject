using Cafe.Web.Helper;
using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.Services.Inteface;
using Cafe.Web.Attribute;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Web.Controllers;

public class OrderController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [UserNameInjection]
    public async Task<IActionResult> GetAllUserOrder(string? userName, int page, int pageSize)
    {
        var result = await _orderService.GetOrderByUserName(userName, page, pageSize);
        return this.GetResponse(result);
    }
    
    [HttpPost]
    [UserNameInjection]
    public async Task<IActionResult> AddNewOrder([FromBody] AddOrderRequest newOrder, string? userName)
    {
        var result = await _orderService.AddNewOrder(newOrder, userName);
        return this.GetResponse(result);
    }
}