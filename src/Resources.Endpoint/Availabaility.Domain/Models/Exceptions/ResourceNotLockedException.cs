namespace Resources.Endpoint.Availabaility.Domain.Models.Exceptions
{
    public class ResourceNotLockedException : BusinessException
    {
        public ResourceNotLockedException() : base("Zasób nie jest zablokowany")
        {
        }
    }
}
