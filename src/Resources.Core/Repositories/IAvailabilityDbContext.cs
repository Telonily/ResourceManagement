using Resources.Core.Entities;

namespace Resources.Core.Repositories;
public interface IAvailabilityDbContext
{
    public IEnumerable<ResourceBlockade> ResourceBlockades { get; set; }

    int SaveChanges();
    void CreateBlockade(Guid resourceId, Guid ownerId);
    void CreatePermanentBlockade(Guid resourceId, Guid ownerId);
    void ReleaseBlockade(Guid resourceId, Guid ownerId);
}
