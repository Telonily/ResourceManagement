namespace Resources.Endpoint.Commands;

public record LockResourceBase(Guid ResourceId, Guid UserId, string UserToken);

public record LockResourceTemporarily : LockResourceBase
{
    public LockResourceTemporarily(Guid ResourceId, Guid UserId, string UserToken) : base(ResourceId, UserId, UserToken)
    {
    }
}

public record LockResourcePermanently : LockResourceBase
{
    public LockResourcePermanently(Guid ResourceId, Guid UserId, string UserToken) : base(ResourceId, UserId, UserToken)
    {
    }
}