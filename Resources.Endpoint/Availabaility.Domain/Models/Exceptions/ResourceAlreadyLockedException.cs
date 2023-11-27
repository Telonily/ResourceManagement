namespace Resources.Endpoint.Availabaility.Domain.Models.Exceptions
{
    public class ResourceAlreadyLockedException : BusinessException
    {
        public ResourceAlreadyLockedException() : base("Zasób jest już zablokowany")
        {
        }
    }
}
