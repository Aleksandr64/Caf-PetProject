namespace Cafe.Application.DTOs.UserDTOs.Request;

public class ChangeUserDataRequest
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}