using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Core.Repositories;
public interface IResourceBlockadesRepository
{
    ResourceBlockade Get(ResourceBlockadeId id);
    IEnumerable<ResourceBlockade> GetAll();
    void Add(ResourceBlockade resource);
    void Update(ResourceBlockade resource);
    void Delete(ResourceBlockade resource);
}
