namespace Resources.Endpoint.InputModels
{
    public class CancelResourceInput
    {
        public required Guid ResourceId { get; set; }
        public required Guid CancelerUserId { get; set; }
    }
}
