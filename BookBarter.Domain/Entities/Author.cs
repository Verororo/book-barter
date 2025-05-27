using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Domain.Entities;

public class Author : Entity
{
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = default!;
    public ICollection<Book> Books { get; set; } = default!;
}
