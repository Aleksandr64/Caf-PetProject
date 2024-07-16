using System.ComponentModel.DataAnnotations;

namespace Cafe.Domain;

public class Token : BaseEntity
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredTime { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}