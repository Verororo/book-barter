using System.Reflection.Metadata;

namespace BookBarter.Domain.Entities;
public class UserWantsBook
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}