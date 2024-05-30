using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe.Domain;

public class Order : BaseEntity
{
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string EmailAddres { get; set; }
    public double TotalAmount { get; set; }
    
    public string? UserName { get; set; }
    public User User { get; set; }
    
    public ICollection<OrderItem>? OrderItems { get; set; }
}