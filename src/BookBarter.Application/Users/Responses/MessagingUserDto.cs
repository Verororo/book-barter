
namespace BookBarter.Application.Users.Responses;

public class MessagingUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? LastMessage { get; set; }
    public DateTime? LastMessageTime { get; set; }
}
