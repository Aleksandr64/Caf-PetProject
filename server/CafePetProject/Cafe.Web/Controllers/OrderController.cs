using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.Services.External.Interface;
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
    public async Task<IActionResult> GetAllUserOrderByUserName(string userName, int page, int pageSize)
    {
        var result = await _orderService.GetOrderByUserName(userName, page, pageSize);
        return Ok(result);
    }
    
    [HttpGet]
    [InjectUserName]
    public async Task<IActionResult> GetAllUserOrder(int page, int pageSize)
    {
        var userName = HttpContext.Items["userName"] as string;
        var result = await _orderService.GetOrderByUserName(userName, page, pageSize);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddNewOrder([FromBody] AddOrderRequest newOrder)
    {
        await _orderService.AddNewOrder(newOrder);
        return NoContent();
    }
}