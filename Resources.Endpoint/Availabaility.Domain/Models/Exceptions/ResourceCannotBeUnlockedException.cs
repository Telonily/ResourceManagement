namespace Resources.Endpoint.Availabaility.Domain.Models.Exceptions
{
    public class ResourceCannotBeUnlockedException : BusinessException
    {
        public ResourceCannotBeUnlockedException() : base("Zasób nie może być odblokowany")
        {
        }
    }
}
