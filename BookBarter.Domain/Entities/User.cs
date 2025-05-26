namespace BookBarter.Domain.Entities;
public class User : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string About { get; set; } = string.Empty;
    public required string City { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime LastOnlineDate { get; set; }
    

    public User() { }
    public User(string name, string email, string city)
    {
        Name = name;
        Email = email;
        City = city;
    }
    public User(int id, string name, string email, string city)
    {
        Id = id;
        Name = name;
        Email = email;
        City = city;
    }
}