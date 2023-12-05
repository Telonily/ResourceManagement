using Resources.Core.Exceptions;

namespace Resources.Core.ValueObjects;
public sealed record ResourceId
{
    public Guid Value { get; }

    public ResourceId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static ResourceId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ResourceId resourceId)
        => resourceId.Value;

    public static implicit operator ResourceId(Guid value)
        => new(value);
}
