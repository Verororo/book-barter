namespace BookBarter.Domain.Entities.Abstractions;

public interface IBookRelationship
{
    public int BookId { get; set; }
    public int UserId { get; set; }
}
