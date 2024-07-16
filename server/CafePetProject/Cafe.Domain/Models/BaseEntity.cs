using System.ComponentModel.DataAnnotations;

namespace Cafe.Domain;

public abstract class BaseEntity
{
    public int Id { get; init; }
    public DateTime DateCreate { get; set; }
    public DateTime DateUpdate { get; set; }
}   