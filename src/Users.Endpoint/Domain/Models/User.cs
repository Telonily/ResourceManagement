namespace Users.Endpoint.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Token { get; set; }
        public required Role Role { get; set; }
    }
}
