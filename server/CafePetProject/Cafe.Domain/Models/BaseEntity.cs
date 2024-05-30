using System.ComponentModel.DataAnnotations;

namespace Cafe.Domain;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; init; }
    public DateTime DateCreate { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdate { get; set; } = DateTime.UtcNow;
}   