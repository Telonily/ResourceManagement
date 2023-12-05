using Resources.Core.Entities;

namespace Resources.Core.Repositories;
public interface IResourcesDbContext
{
    public ICollection<Resource> Resources { get; set; }
    int SaveChanges();
}
