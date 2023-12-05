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
    Task LockResourceTemporaryAsync(ResourceId resourceId, Guid userId, string userToken);

    Task LockResourcePermanentlyAsync(ResourceId resourceId, Guid userId, string userToken);

    Task ReleaseResourceAsync(ResourceId resourceId, Guid userId, string userToken);
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


    public Task LockResourcePermanentlyAsync(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailableAsync(resourceId).ConfigureAwait(false).GetAwaiter().GetResult())
                throw new ResourceNotAvailableToLockException();

            CheckIfActiveBlockade(resourceId).ConfigureAwait(false).GetAwaiter().GetResult();

            ResourceBlockade newBlockade = new(Guid.NewGuid(), resourceId, userId, DateTime.Now, TimeSpan.Zero);
            _resourceBlockadesRepository.AddAsync(newBlockade);
        }

        return Task.CompletedTask;
    }

    public async Task LockResourceTemporaryAsync(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            if (!_resourceManagementService.IsResourceAvailableAsync(resourceId).ConfigureAwait(false).GetAwaiter().GetResult())
                throw new ResourceNotAvailableToLockException();

            CheckIfActiveBlockade(resourceId).ConfigureAwait(false).GetAwaiter().GetResult(); ;

            ResourceBlockade newBlockade = new
            (
                Guid.NewGuid(),
                resourceId,
                userId,
                DateTime.Now,
                TimeSpan.FromMinutes(60) //todo
            );

            _resourceBlockadesRepository.AddAsync(newBlockade);
        }
    }

    public Task ReleaseResourceAsync(ResourceId resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
            throw new AccessDenied();

        lock (GetResourceLockObject(resourceId))
        {
            var blockades = _resourceBlockadesRepository.GetAllAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            var blockade = blockades.Where(b => b.ResourceId == resourceId && b.BlockadeOwnerId == userId && !b.ReleasedOnPurpose)
            .ToList()
            .Where(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now)
            .FirstOrDefault() ??
                throw new ResourceCannotBeUnlockedException();

            blockade.Release();
            _resourceBlockadesRepository.UpdateAsync(blockade).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        return Task.CompletedTask;
    }

    private object GetResourceLockObject(Guid resourceId)
    {
        return ResourceLocks.GetOrAdd(resourceId, new object { });
    }

    private async Task CheckIfActiveBlockade(ResourceId resourceId)
    {
        var blokades = await _resourceBlockadesRepository.GetAllAsync();

        var activeResourceBlokades = blokades
            .Where(b => b.ResourceId == resourceId && !b.ReleasedOnPurpose)
            .ToList();

        if (activeResourceBlokades.Any(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now))
            throw new ResourceAlreadyLockedException();
    }
}
