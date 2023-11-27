namespace Resources.Endpoint.InputModels
{
    public class AddResourceInput
    {
        public required Guid Id { get; set; }

        public required string Name { get; set; }
    }
}
