namespace Resources.Endpoint.InputModels
{
    public class LockResourceInput
    {
        public required Guid ResourceId { get; set; }
        public required Guid UserId { get; set; }
        public required string UserToken { get; set; }
    }
}
