namespace Resources.Application.Exceptions;

public sealed class ResourceAlreadyLockedException : BusinessException
{
    public ResourceAlreadyLockedException() : base("Zasób jest już zablokowany")
    {
    }
}
