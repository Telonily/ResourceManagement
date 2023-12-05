using Resources.Core.Exceptions;

namespace Resources.Application.Exceptions;

public sealed class ResourceNotLockedException : BusinessException
{
    public ResourceNotLockedException() : base("Zasób nie jest zablokowany")
    {
    }
}
