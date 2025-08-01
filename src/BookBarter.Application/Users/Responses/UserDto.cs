﻿
using BookBarter.Application.Cities.Responses;

namespace BookBarter.Application.Users.Responses;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string? About { get; set; }
    public CityDto City { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }

    public ICollection<OwnedBookDto> OwnedBooks { get; set; } = [];
    public ICollection<WantedBookDto> WantedBooks { get; set; } = [];
}
