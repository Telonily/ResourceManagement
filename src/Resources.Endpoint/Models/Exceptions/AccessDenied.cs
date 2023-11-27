namespace Resources.Endpoint.Models.Exceptions
{
    public class AccessDenied : BusinessException
    {
        public AccessDenied() : base("Brak uprawnień do wykonania tej operacji")
        {
        }
    }
}
