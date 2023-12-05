using Resources.Core.Exceptions;

namespace Resources.Application.Exceptions;

public class AccessDenied : BusinessException
{
    public AccessDenied() : base("Brak uprawnień do wykonania tej operacji")
    {
    }
}
