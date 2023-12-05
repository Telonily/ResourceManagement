using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Core.Repositories;
public interface IResourceBlockadesRepository
{
    public IEnumerable<ResourceBlockade> ResourceBlockades { get; set; }

    int SaveChanges();
    void CreateBlockade(ResourceId resourceId, Guid ownerId);
    void CreatePermanentBlockade(ResourceId resourceId, Guid ownerId);
    void ReleaseBlockade(ResourceId resourceId, Guid ownerId);
}
