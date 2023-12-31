﻿using Resources.Endpoint.Resources.Domain.DbContexts;
using Resources.Endpoint.Resources.Domain.Models.Exceptions;

namespace Resources.Endpoint.Resources.Domain.Services
{
    public interface IResourceManagementService
    {
        void AddResource(Guid id, string resourceName, Guid userId);

        void CancelResource(Guid id, Guid userId);

        bool IsResourceAvailable(Guid id);
    }

    public class ResourceManagementService : IResourceManagementService
    {
        private readonly IResourcesDbContext DbContext;

        public ResourceManagementService(IResourcesDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void AddResource(Guid id, string resourceName, Guid userId)
        {
            DbContext.Resources.Add(new Models.Resource { Id = id, Name = resourceName, CreatorUserId = userId });
            DbContext.SaveChanges();
        }

        public void CancelResource(Guid id, Guid userId)
        {
            var resourceToCancel = DbContext.Resources.Where(x => x.Id == id).FirstOrDefault() ??
                throw new ResourceNotFoundException($"Nie znaleziono zasobu o Id: {id}");

            resourceToCancel.Cancel(userId);

            DbContext.SaveChanges();
        }

        public bool IsResourceAvailable(Guid id)
        {
            return DbContext.Resources.Where(r => r.Id == id && !r.Canceled).Any();
        }
    }
}
