namespace BookBarter.Application.Books.Responses;

public class BookRelationshipWithUserDto
{
    public CollapsedUserDto User { get; set; } = default!;
    public DateTime AddedDate { get; set; }
}
