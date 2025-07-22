
namespace BookBarter.Application.Messages.Responses;

public class MessageDto
{
    public int Id { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string SenderName { get; set; } = default!;
    public string ReceiverName { get; set; } = default!;
    public string Body { get; set; } = default!;
    public DateTime SentTime { get; set; }
    public DateTime? EditTime { get; set; }
}
