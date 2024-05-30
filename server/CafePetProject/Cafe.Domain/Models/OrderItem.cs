namespace Cafe.Domain;

public class OrderItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Quantity { get; set; }
    public int DishId { get; set; }
    public DateTime DateCreate { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdate { get; set; } = DateTime.UtcNow;
    
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}