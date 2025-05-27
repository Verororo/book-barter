using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Domain.Entities;

public class Message : Entity
{
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public string Body { get; set; } = default!;
    public DateTime SentTime { get; set; }
    public DateTime? EditTime { get; set; } = null; // 'null' means that the message has not been edited at all
}
