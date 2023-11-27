namespace Resources.Endpoint.Resources.Domain.Models.Exceptions
{
    public class ResourceNotFoundException : BusinessException
    {
        public ResourceNotFoundException(string? message) : base(message)
        {
        }
    }
}
