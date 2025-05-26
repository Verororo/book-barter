using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Domain.Entities;

public class Message : Entity
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public required string Body { get; set; }
    public DateTime SentTime { get; set; }
    public DateTime? EditTime { get; set; } = null; // 'null' means that the message has not been edited at all

    public Message() { }
    public Message(int id, int senderId, int receiverId, string body, DateTime sentTime)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Body = body;
        SentTime = sentTime;
    }
}
