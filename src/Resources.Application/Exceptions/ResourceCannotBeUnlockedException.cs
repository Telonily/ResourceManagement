namespace Resources.Application.Exceptions;

public sealed class ResourceCannotBeUnlockedException : BusinessException
{
    public ResourceCannotBeUnlockedException() : base("Zasób nie może być odblokowany")
    {
    }
}
