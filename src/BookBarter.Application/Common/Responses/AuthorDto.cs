namespace BookBarter.Application.Common.Responses;

public class AuthorDto
{
    public string? FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
}
