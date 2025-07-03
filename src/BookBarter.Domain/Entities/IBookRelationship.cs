
namespace BookBarter.Domain.Entities;

public interface IBookRelationship
{
    public int BookId { get; set; }
    public int UserId { get; set; }
}
