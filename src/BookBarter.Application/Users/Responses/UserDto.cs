using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses;

public class UserDto
{
    public string UserName { get; set; } = default!;
    public string? About { get; set; }
    public string City { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBook> OwnedBooks { get; set; } = [];
    public ICollection<Book> WantedBooks { get; set; } = [];
}
