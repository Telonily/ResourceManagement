namespace Resources.Endpoint.Commands;

public record CancelResource(Guid ResourceId, Guid UserId, string UserToken);