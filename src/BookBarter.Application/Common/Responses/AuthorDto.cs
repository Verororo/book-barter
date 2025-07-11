namespace BookBarter.Application.Common.Responses;

public class AuthorDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}
