namespace Cafe.Application.DTOs.OrderDTOs.Request;

public class AddOrderItemRequest
{
    public int Quantity { get; set; }
    public int DishId { get; set; }
}