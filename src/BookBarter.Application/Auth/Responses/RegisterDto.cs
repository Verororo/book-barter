
namespace BookBarter.Application.Auth.Responses;

public class RegisterDto
{
    public bool Succeeded { get; set; }
    public List<string> Messages { get; set; } = [];
}