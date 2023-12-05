using Resources.Core.ValueObjects;

namespace Resources.Core.Entities;

public class Resource
{
    public ResourceId Id { get; }
    public string Name { get; private set; }

    /// <summary>
    /// Id użytkownika, który dodał zasób
    /// </summary>
    public Guid CreatorUserId { get; private set; }

    /// <summary>
    /// Flaga wycofania zasobu
    /// </summary>
    public bool Canceled { get; private set; }

    /// <summary>
    /// Id użytkownika, który zasób wycofał
    /// </summary>
    public Guid? CancelerUserId { get; private set; }

    /// <summary>
    /// Data wycofania zasobu
    /// </summary>
    public DateTime? CancelDate { get; private set; }

    public Resource(ResourceId id, string name, Guid creatorUserId)
    {
        Id = id;
        Name = name;
        CreatorUserId = creatorUserId;
    }

    public void Cancel(Guid cancelerUserId)
    {
        Canceled = true;
        CancelerUserId = cancelerUserId;
        CancelDate = DateTime.Now;
    }
}
