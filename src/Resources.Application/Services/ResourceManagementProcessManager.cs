using PublishedLanguage.Enums;
using Resources.Application.Exceptions;
using Users.Client.Abstract;
using Users.Client.Models;

namespace Resources.Application.Services;

public interface IResourceManagementProcessManager
{
    Task AddResourceAsync(Guid resourceId, string resourceName, Guid userId, string userToken);

    Task CancelResourceAsync(Guid resourceId, Guid userId, string userToken);
}

public class ResourceManagementProcessManager : IResourceManagementProcessManager
{
    private readonly IUserService _userService;
    private readonly IResourceManagementService _resourceManagementService;

    public ResourceManagementProcessManager(IUserService userService, IResourceManagementService resourceManagementService)
    {
        _userService = userService;
        _resourceManagementService = resourceManagementService;
    }

    public async Task AddResourceAsync(Guid resourceId, string resourceName, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceManagement }))
            throw new AccessDenied();

        await _resourceManagementService.AddResourceAsync(resourceId, resourceName, userId);
    }

    public async Task CancelResourceAsync(Guid resourceId, Guid userId, string userToken)
    {
        if (!_userService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceManagement }))
            throw new AccessDenied();

        await _resourceManagementService.CancelResourceAsync(resourceId, userId);
    }
}
