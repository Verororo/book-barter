using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Domain.Entities;

public class Author : Entity
{
    public string FirstName { get; set; }
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; }

    public Author(string firstName, string middleName, string lastName)
    {
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
    }
}
