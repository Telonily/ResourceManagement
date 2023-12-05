using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Core.Repositories;
public interface IResourceRepository
{
    Task<Resource> GetAsync(ResourceId id);
    Task<IEnumerable<Resource>> GetAllAsync();
    Task AddAsync(Resource resource);
    Task UpdateAsync(Resource resource);
    Task DeleteAsync(Resource resource);
}
