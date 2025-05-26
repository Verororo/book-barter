
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }

        public static UserDto FromUser(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
