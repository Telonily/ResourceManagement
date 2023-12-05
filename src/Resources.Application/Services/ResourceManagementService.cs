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
    private readonly IResourcesRepository _context;

    public ResourceManagementService(IResourcesRepository dbContext)
    {
        _context = dbContext;
    }

    public void AddResource(ResourceId id, string resourceName, Guid userId)
    {
        _context.Resources.Add(new Resource(id, resourceName, userId));
        _context.SaveChanges();
    }

    public void CancelResource(ResourceId id, Guid userId)
    {
        var resourceToCancel = _context.Resources.Where(x => x.Id == id).FirstOrDefault() ??
            throw new ResourceNotFoundException(id);

        resourceToCancel.Cancel(userId);

        _context.SaveChanges();
    }

    public bool IsResourceAvailable(ResourceId id)
    {
        return _context.Resources.Where(r => r.Id == id && !r.Canceled).Any();
    }
}
