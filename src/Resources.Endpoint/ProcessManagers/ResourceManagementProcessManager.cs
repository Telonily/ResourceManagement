using Resources.Endpoint.Models.Exceptions;
using Resources.Endpoint.Resources.Domain.Services;
using Users.Client.Abstract;
using Users.Client.Models;
using Users.Public.Models.Enums;

namespace Resources.Endpoint.ProcessManagers
{
    public interface IResourceManagementProcessManager
    {
        void AddResource(Guid resourceId, string resourceName, Guid userId, string userToken);

        void CancelResource(Guid resourceId, Guid userId, string userToken);
    }

    public class ResourceManagementProcessManager : IResourceManagementProcessManager
    {
        private readonly IUserService UserService;
        private readonly IResourceManagementService ResourceManagementService;

        public ResourceManagementProcessManager(IUserService userService, IResourceManagementService resourceManagementService)
        {
            UserService = userService;
            ResourceManagementService = resourceManagementService;
        }

        public void AddResource(Guid resourceId, string resourceName, Guid userId, string userToken)
        {
            if (!UserService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceManagement }))
                throw new AccessDenied();

            ResourceManagementService.AddResource(resourceId, resourceName, userId);
        }

        public void CancelResource(Guid resourceId, Guid userId, string userToken)
        {
            if (!UserService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceManagement }))
                throw new AccessDenied();

            ResourceManagementService.CancelResource(resourceId, userId);
        }
    }
}
