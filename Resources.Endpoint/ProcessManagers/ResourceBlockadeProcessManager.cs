using Resources.Endpoint.Availabaility.Domain.DbContexts;
using Resources.Endpoint.Availabaility.Domain.Models.Exceptions;
using Resources.Endpoint.Models.Exceptions;
using Resources.Endpoint.Resources.Domain.Services;
using System.Collections.Concurrent;
using Users.Client.Abstract;
using Users.Client.Models;
using Users.Public.Models.Enums;

namespace Resources.Endpoint.ProcessManagers
{


    public interface IResourceBlockadeProcessManager
    {
        void LockResourceTemporary(Guid resourceId, Guid userId, string userToken);

        void LockResourcePermanently(Guid resourceId, Guid userId, string userToken);

        void ReleaseResource(Guid resourceId, Guid userId, string userToken);
    }

    public class ResourceBlockadeProcessManager : IResourceBlockadeProcessManager
    {
        private readonly IAvailabilityDbContext AvailabilityDbContext;
        private readonly IUserService UserService;
        private readonly IResourceManagementService ResourceManagementService;

        private static ConcurrentDictionary<Guid, object> ResourceLocks = new();

        public ResourceBlockadeProcessManager(IAvailabilityDbContext availabilityDbContext, IUserService userService, IResourceManagementService resourceManagementService)
        {
            AvailabilityDbContext = availabilityDbContext;
            UserService = userService;
            ResourceManagementService = resourceManagementService;
        }


        public void LockResourcePermanently(Guid resourceId, Guid userId, string userToken)
        {
            if (!UserService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
                throw new AccessDenied();

            lock (GetResourceLockObject(resourceId))
            {
                if (!ResourceManagementService.IsResourceAvailable(resourceId))
                    throw new ResourceNotAvailableToLockException();

                AvailabilityDbContext.CreatePermanentBlockade(resourceId, userId);
            }
        }

        public void LockResourceTemporary(Guid resourceId, Guid userId, string userToken)
        {
            if (!UserService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
                throw new AccessDenied();

            lock (GetResourceLockObject(resourceId))
            {
                if (!ResourceManagementService.IsResourceAvailable(resourceId))
                    throw new ResourceNotAvailableToLockException();

                AvailabilityDbContext.CreateBlockade(resourceId, userId);
            }
        }

        public void ReleaseResource(Guid resourceId, Guid userId, string userToken)
        {
            if (!UserService.AuthorizeUser(new AuthorizeUserInput { UserId = userId, UserToken = userToken, RequestedPermission = Permission.ResourceLock }))
                throw new AccessDenied();

            lock (GetResourceLockObject(resourceId))
            {
                AvailabilityDbContext.ReleaseBlockade(resourceId, userId);
            }
        }

        private object GetResourceLockObject(Guid resourceId)
        {
            return ResourceLocks.GetOrAdd(resourceId, new object { });
        }
    }
}
