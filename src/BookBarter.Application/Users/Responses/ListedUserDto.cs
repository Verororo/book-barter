
using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Users.Responses;

public class ListedUserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string CityName { get; set; } = default!;
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBookDto> OwnedBooks { get; set; } = [];
    public ICollection<WantedBookDto> WantedBooks { get; set; } = [];
}
