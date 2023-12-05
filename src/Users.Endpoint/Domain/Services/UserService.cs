using PublishedLanguage.Enums;
using Users.Endpoint.Domain.Models;

namespace Users.Endpoint.Domain.Services;

public interface IUserService
{
    bool Authorize(Guid id, string token, Permission permission);
}

public class UserService : IUserService
{
    public bool Authorize(Guid id, string token, Permission permission)
    {
        return GetUsers().Where(u => u.Id.Equals(id) && u.Token.Equals(token) && u.Role.Permissions.Contains(permission)).Any();
    }

    // Docelowo baza danych
    private ICollection<User> GetUsers()
    {
        Role adminRole = new Role()
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Permissions = new[] { Permission.ResourceManagement, Permission.ResourceLock }
        };

        Role userRole = new Role()
        {
            Id = Guid.NewGuid(),
            Name = "User",
            Permissions = new[] { Permission.ResourceLock }
        };


        List<User> users = new List<User>()
        {
            new User()
            {
                Id = Guid.Parse("e96bb66b-0732-4c2c-9359-250d51b92b67"),
                Login = "admin1",
                Role = adminRole,
                Token = "123123123123"
            },
            new User()
            {
                Id = Guid.Parse("274b8971-3947-456c-ac32-1526f8547c37"),
                Login = "user1",
                Role = userRole,
                Token = "234234234234"
            },
            new User()
            {
                Id = Guid.Parse("0b6ae747-33a7-4817-91cc-ad333e273a89"),
                Login = "user2",
                Role = userRole,
                Token = "345345345345"
            }
        };

        return users;
    }
}
