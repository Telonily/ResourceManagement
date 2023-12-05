using Resources.Core.Exceptions;

namespace Resources.Core.ValueObjects;

public sealed record ResourceBlokadeId
{
    public Guid Value { get; }

    public ResourceBlokadeId(Guid value)
    {
        if (value == Guid.Empty)
            throw new InvalidEntityIdException(value);

        Value = value;
    }

    public static ResourceBlokadeId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ResourceBlokadeId resourceId)
        => resourceId.Value;

    public static implicit operator ResourceBlokadeId(Guid value)
        => new(value);
}
