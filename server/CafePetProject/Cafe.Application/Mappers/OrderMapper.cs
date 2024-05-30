using System.Collections.ObjectModel;
using Cafe.Application.DTOs.OrderDTOs.Request;
using Cafe.Application.DTOs.OrderDTOs.Response;
using Cafe.Domain;

namespace Cafe.Application.Mappers;

public static class OrderMapper
{
    public static Order MapOrderAddRequest(this AddOrderRequest addOrderRequest, string? userName)
    {
        
        Order order = new Order
        {
            CustomerName = addOrderRequest.CustomerName,
            PhoneNumber = addOrderRequest.PhoneNumber,
            Address = addOrderRequest.Address,
            EmailAddres = addOrderRequest.EmailAddress,
            UserName = userName,
            TotalAmount = addOrderRequest.TotalAmount,
            OrderItems = new List<OrderItem>()
        };
        order.OrderItems = MapOrderItemsAddRequest(addOrderRequest.OrderItems, order.Id);
        return order;
    }

    private static ICollection<OrderItem> MapOrderItemsAddRequest(IEnumerable<AddOrderItemRequest> orderItemRequests, int Id)
    {
        ICollection<OrderItem> orderItems = new List<OrderItem>();
        foreach (var addOrderItem in orderItemRequests) 
        {
            OrderItem orderItem = new OrderItem
            {
                Quantity = addOrderItem.Quantity,
                DishId = addOrderItem.DishId,
                OrderId = Id
                
            };
            orderItems.Add(orderItem);
        }
        return orderItems;
    }
    
    public static OrderResponse MapOrderToResponse(this Order orderResponse)
    {
        
        OrderResponse order = new OrderResponse
        {
            CustomerName = orderResponse.CustomerName,
            PhoneNumber = orderResponse.PhoneNumber,
            Address = orderResponse.Address,
            EmailAddress = orderResponse.EmailAddres,
            TotalAmount = orderResponse.TotalAmount,
            DateCreate = orderResponse.DateCreate,
            OrderItems = new List<OrderItemsResponse>()
        };
        order.OrderItems = MapOrderItemsToResponse(orderResponse.OrderItems);
        return order;
    }

    private static List<OrderItemsResponse> MapOrderItemsToResponse(IEnumerable<OrderItem> orderItemsResponse)
    {
        List<OrderItemsResponse> orderItems = new List<OrderItemsResponse>();
        foreach (var mapOrderItem in orderItemsResponse) 
        {
            OrderItemsResponse orderItem = new OrderItemsResponse
            {
                Quantity = mapOrderItem.Quantity,
                DishId = mapOrderItem.DishId,
            };
            orderItems.Add(orderItem);
        }
        return orderItems;
    }
}
