using Cafe.Domain;

namespace Cafe.Application.DTOs.OrderDTOs.Response;

public class OrderResponse
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string EmailAddress { get; set; }
    public double TotalAmount { get; set; }
    public DateTime DateCreate { get; set; }
    public List<OrderItemsResponse> OrderItems { get; set; }
}