using System.ComponentModel.DataAnnotations;

namespace Cafe.Domain;

public class Token
{
    [Key]
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; set; }
}