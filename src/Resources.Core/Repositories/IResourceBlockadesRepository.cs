using Resources.Core.Entities;
using Resources.Core.ValueObjects;

namespace Resources.Core.Repositories;
public interface IResourceBlockadesRepository
{
    Task<ResourceBlockade> GetAsync(ResourceBlockadeId id);
    Task<IEnumerable<ResourceBlockade>> GetAllAsync();
    Task AddAsync(ResourceBlockade resource);
    Task UpdateAsync(ResourceBlockade resource);
    Task DeleteAsync(ResourceBlockade resource);
}
