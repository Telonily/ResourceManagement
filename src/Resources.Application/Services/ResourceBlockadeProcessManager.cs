using PublishedLanguage.Enums;
using Resources.Application.Exceptions;
using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;
using System.Collections.Concurrent;
using Users.Client.Abstract;
using Users.Client.Models;

namespace Resources.Application.Services;


public interface IResourceBlockadeProcessManager
{
    void LockResourceTemporary(ResourceId resourceId, Guid userId, string userToken);

    void LockResourcePermanently(ResourceId resourceId, Guid userId, string userToken);

    void ReleaseResource(ResourceId resourceId, Guid userId, string userToken);
}

public class ResourceBlockadeProcessManager : IResourceBlockadeProcessManager
{
    private readonly IResourceBlockadesRepository _resourceBlockadesRepository;
    private readonly IResourceManagementService _resourceManagementService;
    private readonly IUserService _userService;

    private static readonly ConcurrentDictionary<Guid, object> ResourceLocks = new();

    public ResourceBlockadeProcessManager(IResourceBlockadesRepository availabilityDbContext, IUserService userService, IResourceManagementService resourceManagementService)
    {
        _resourceBlockadesRepository = availabilityDbContext;
        _userService = userService;
        _resourceManagementService = resourceManagementService;
    }


    public void LockResourcePermanently(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailable(resourceId))
                throw new ResourceNotAvailableToLockException();

            CheckIfActiveBlockade(resourceId);

            ResourceBlockade newBlockade = new(Guid.NewGuid(), resourceId, userId, DateTime.Now, TimeSpan.Zero);
            _resourceBlockadesRepository.Add(newBlockade);
        }
    }

    public void LockResourceTemporary(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailable(resourceId))
                throw new ResourceNotAvailableToLockException();

            CheckIfActiveBlockade(resourceId);

            ResourceBlockade newBlockade = new
            (
                Guid.NewGuid(),
                resourceId,
                userId,
                DateTime.Now,
                TimeSpan.FromMinutes(60) //todo
            );

            _resourceBlockadesRepository.Add(newBlockade);
        }
    }

    public void ReleaseResource(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            var blockade = _resourceBlockadesRepository.GetAll().Where(b => b.ResourceId == resourceId && b.BlockadeOwnerId == userId && !b.ReleasedOnPurpose)
                .ToList()
                .Where(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now)
                .FirstOrDefault() ??
                    throw new ResourceCannotBeUnlockedException();

            blockade.Release();
            _resourceBlockadesRepository.Update(blockade);
        }
    }

    private object GetResourceLockObject(Guid resourceId)
    {
        return ResourceLocks.GetOrAdd(resourceId, new object { });
    }

    private void CheckIfActiveBlockade(ResourceId resourceId)
    {
        var activeResourceBlokades = _resourceBlockadesRepository
            .GetAll()
            .Where(b => b.ResourceId == resourceId && !b.ReleasedOnPurpose)
            .ToList();

        if (activeResourceBlokades.Any(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now))
            throw new ResourceAlreadyLockedException();
    }
}
