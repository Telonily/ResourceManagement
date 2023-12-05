namespace Resources.Application.Exceptions;

public sealed class ResourceNotFoundException : BusinessException
{
    public ResourceNotFoundException(Guid id) : base($"Nie znaleziono zasobu o id {id}")
    {
    }
}
