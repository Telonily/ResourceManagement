namespace Resources.Application.Exceptions;

public sealed class ResourceNotAvailableToLockException : BusinessException
{
    public ResourceNotAvailableToLockException() : base("Brak możliwości zablokowania tego zasobu")
    {
    }
}
