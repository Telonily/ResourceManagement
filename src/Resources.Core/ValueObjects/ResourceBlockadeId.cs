using Resources.Core.Exceptions;

namespace Resources.Core.ValueObjects;

public sealed record ResourceBlockadeId
{
    public Guid Value { get; }

    public ResourceBlockadeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static ResourceBlockadeId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ResourceBlockadeId resourceId)
        => resourceId.Value;

    public static implicit operator ResourceBlockadeId(Guid value)
        => new(value);
}
