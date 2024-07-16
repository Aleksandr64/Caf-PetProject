using System.ComponentModel.DataAnnotations;

namespace Cafe.Domain;

public class Dish : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}