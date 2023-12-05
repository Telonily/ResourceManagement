using Resources.Application.Exceptions;
using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;

namespace Resources.Application.Services;

public interface IResourceManagementService
{
    void AddResource(ResourceId id, string resourceName, Guid userId);

    void CancelResource(ResourceId id, Guid userId);

    bool IsResourceAvailable(ResourceId id);
}

public class ResourceManagementService : IResourceManagementService
{
    private readonly IResourceRepository _repository;

    public ResourceManagementService(IResourceRepository dbContext)
    {
        _repository = dbContext;
    }

    public void AddResource(ResourceId id, string resourceName, Guid userId)
    {
        _repository.Add(new Resource(id, resourceName, userId));
    }

    public void CancelResource(ResourceId id, Guid userId)
    {
        var resourceToCancel = _repository.GetAll().Where(x => x.Id == id).FirstOrDefault() ??
            throw new ResourceNotFoundException(id);

        resourceToCancel.Cancel(userId);

        _repository.Update(resourceToCancel);
    }

    public bool IsResourceAvailable(ResourceId id)
    {
        return _repository.GetAll().Where(r => r.Id == id && !r.Canceled).Any();
    }
}
