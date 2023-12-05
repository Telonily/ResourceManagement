using Resources.Core.Entities;

namespace Resources.Core.Repositories;
public interface IResourcesRepository
{
    public ICollection<Resource> Resources { get; set; }
    int SaveChanges();
}
