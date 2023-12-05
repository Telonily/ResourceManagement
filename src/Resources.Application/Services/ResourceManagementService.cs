using Resources.Application.Exceptions;
using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;

namespace Resources.Application.Services;

public interface IResourceManagementService
{
    Task AddResourceAsync(ResourceId id, string resourceName, Guid userId);

    Task CancelResourceAsync(ResourceId id, Guid userId);

    Task<bool> IsResourceAvailableAsync(ResourceId id);
}

public class ResourceManagementService : IResourceManagementService
{
    private readonly IResourceRepository _repository;

    public ResourceManagementService(IResourceRepository dbContext)
    {
        _repository = dbContext;
    }

    public async Task AddResourceAsync(ResourceId id, string resourceName, Guid userId)
    {
        await _repository.AddAsync(new Resource(id, resourceName, userId));
    }

    public async Task CancelResourceAsync(ResourceId id, Guid userId)
    {
        var resources = await _repository.GetAllAsync();

        var resourceToCancel = resources
            .Where(x => x.Id == id).FirstOrDefault() ??
            throw new ResourceNotFoundException(id);

        resourceToCancel.Cancel(userId);

        await _repository.UpdateAsync(resourceToCancel);
    }

    public async Task<bool> IsResourceAvailableAsync(ResourceId id)
    {
        var resources = await _repository.GetAllAsync();
        return resources.Where(r => r.Id == id && !r.Canceled).Any();
    }
}
