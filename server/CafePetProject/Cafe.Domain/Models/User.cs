namespace Cafe.Domain;

public class User : BaseEntity
{
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string UserName { get; set; } 
    public string Email { get; set; } 
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public string Role { get; set; }
    

    public ICollection<Order>? Orders { get; set; }
}