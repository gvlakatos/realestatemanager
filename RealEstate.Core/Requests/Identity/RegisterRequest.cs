namespace RealEstate.Core.Requests.Identity;

public class RegisterRequest : Request
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}