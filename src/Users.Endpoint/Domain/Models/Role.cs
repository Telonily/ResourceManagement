using Users.Public.Models.Enums;

namespace Users.Endpoint.Domain.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
