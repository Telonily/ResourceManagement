using Resources.Core.ValueObjects;

namespace Resources.Core.Entities;

public class ResourceBlockade
{
    public ResourceBlockadeId Id { get; private set; }

    public ResourceId ResourceId { get; private set; }

    /// <summary>
    /// Id użytkownika
    /// </summary>
    public Guid BlockadeOwnerId { get; private set; }

    /// <summary>
    /// Data zablokowania zasobu
    /// </summary>
    public DateTime BlockadeDate { get; private set; }

    /// <summary>
    /// Okres na który jest zablokowany zasób, 0 - permanentnie
    /// </summary>
    public TimeSpan BlockadeDuration { get; private set; }

    public bool ReleasedOnPurpose { get; private set; }

    public ResourceBlockade(ResourceBlockadeId id, ResourceId resourceId, Guid blockadeOwnerId, DateTime blockadeDate, TimeSpan blockadeDuration)
    {
        Id = id;
        ResourceId = resourceId;
        BlockadeOwnerId = blockadeOwnerId;
        BlockadeDate = blockadeDate;
        BlockadeDuration = blockadeDuration;
    }

    public void Release()
    {
        ReleasedOnPurpose = true;
    }
}
