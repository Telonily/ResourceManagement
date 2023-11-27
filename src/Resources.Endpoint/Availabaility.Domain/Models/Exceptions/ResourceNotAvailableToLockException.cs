namespace Resources.Endpoint.Availabaility.Domain.Models.Exceptions
{
    public class ResourceNotAvailableToLockException : BusinessException
    {
        public ResourceNotAvailableToLockException() : base("Brak możliwości zablokowania tego zasobu")
        {
        }
    }
}
