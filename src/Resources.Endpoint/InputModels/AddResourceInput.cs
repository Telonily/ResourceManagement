using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Resources.Endpoint.InputModels
{
    public class AddResourceInput
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }

        public required Guid UserId { get; set; }

        public required string UserToken { get; set;}
    }
}
