namespace Resources.Endpoint.Commands;

public record AddResource(Guid Id, string Name, Guid UserId, string UserToken);