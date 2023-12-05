using PublishedLanguage.Enums;
using Resources.Application.Exceptions;
using Resources.Core.Repositories;
using System.Collections.Concurrent;
using Users.Client.Abstract;
using Users.Client.Models;

namespace Resources.Application.Services;


public interface IResourceBlockadeProcessManager
{
    void LockResourceTemporary(Guid resourceId, Guid userId, string userToken);

    void LockResourcePermanently(Guid resourceId, Guid userId, string userToken);

    void ReleaseResource(Guid resourceId, Guid userId, string userToken);
}

public class ResourceBlockadeProcessManager : IResourceBlockadeProcessManager
{
    private readonly IResourceBlockadesRepository _availabilityDbContext;
    private readonly IUserService _userService;
    private readonly IResourceManagementService _resourceManagementService;

    private static readonly ConcurrentDictionary<Guid, object> ResourceLocks = new();

    public ResourceBlockadeProcessManager(IResourceBlockadesRepository availabilityDbContext, IUserService userService, IResourceManagementService resourceManagementService)
    {
        _availabilityDbContext = availabilityDbContext;
        _userService = userService;
        _resourceManagementService = resourceManagementService;
    }


    public void LockResourcePermanently(Guid resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailable(resourceId))
                throw new ResourceNotAvailableToLockException();

            _availabilityDbContext.CreatePermanentBlockade(resourceId, userId);
        }
    }

    public void LockResourceTemporary(Guid resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailable(resourceId))
                throw new ResourceNotAvailableToLockException();

            _availabilityDbContext.CreateBlockade(resourceId, userId);
        }
    }

    public void ReleaseResource(Guid resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            _availabilityDbContext.ReleaseBlockade(resourceId, userId);
        }
    }

    private object GetResourceLockObject(Guid resourceId)
    {
        return ResourceLocks.GetOrAdd(resourceId, new object { });
    }
}
