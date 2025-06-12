namespace BookBarter.Domain.Entities;

public class Message : Entity
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Body { get; set; } = default!;
    public DateTime SentTime { get; set; }
    public DateTime? EditTime { get; set; } = null;
    public User Sender { get; set; } = default!;
    public User Receiver { get; set; } = default!;
}
