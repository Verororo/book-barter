
namespace BookBarter.Application.Auth.Responses;

public class LoginDto
{
    public bool Succeeded { get; set; } // change to exception or change controller
    public string? AccessToken { get; set; }
}
