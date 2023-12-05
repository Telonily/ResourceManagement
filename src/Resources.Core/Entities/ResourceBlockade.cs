namespace Resources.Core.Entities;

public class ResourceBlockade
{
    public required Guid Id { get; set; }

    public required Guid ResourceId { get; set; }

    /// <summary>
    /// Id użytkownika
    /// </summary>
    public required Guid BlockadeOwnerId { get; set; }

    /// <summary>
    /// Data zablokowania zasobu
    /// </summary>
    public required DateTime BlockadeDate { get; set; }

    /// <summary>
    /// Okres na który jest zablokowany zasób, 0 - permanentnie
    /// </summary>
    public required TimeSpan BlockadeDuration { get; set; }

    public bool ReleasedOnPurpose { get; set; }


    public void Release()
    {
        ReleasedOnPurpose = true;
    }
}
