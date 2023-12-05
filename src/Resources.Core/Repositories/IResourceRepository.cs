using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Core.Repositories;
public interface IResourceRepository
{
    Resource Get(ResourceId id);
    IEnumerable<Resource> GetAll();
    void Add(Resource resource);
    void Update(Resource resource);
    void Delete(Resource resource);
}
