namespace Resources.Core.Exceptions;
internal class InvalidEntityIdException : BusinessException
{
    public InvalidEntityIdException(object id) : base($"Nie można ustawić {id} jako klucz encji")
    {
    }
}
