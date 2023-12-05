namespace Resources.Endpoint.Commands;

public record ReleaseResource(Guid ResourceId, Guid UserId, string UserToken);