using Users.Endpoint.Domain.Models;
using Users.Public.Models.Enums;
using System.Linq;

namespace Users.Endpoint.Domain.Services
{
    public interface IUserService
    {
        bool Authorize(string login, string token, Permission permission);
    }

    public class UserService : IUserService
    {
        public bool Authorize(string login, string token, Permission permission)
        {
            return GetUsers().Where(u => u.Login.Equals(login) && u.Token.Equals(token) && u.Role.Permissions.Contains(permission)).Any();
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
                    Id = Guid.NewGuid(),
                    Login = "admin1",
                    Role = adminRole,
                    Token = "123123123123"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "user1",
                    Role = userRole,
                    Token = "234234234234"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "user2",
                    Role = userRole,
                    Token = "345345345345"
                }
            };

            return users;
        }
    }
}
