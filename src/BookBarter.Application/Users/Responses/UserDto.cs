
using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Users.Responses;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? About { get; set; }
    public string City { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBookDto> OwnedBooks { get; set; } = [];
    public ICollection<BookDto> WantedBooks { get; set; } = [];
}
