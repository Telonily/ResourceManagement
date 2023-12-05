using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;

namespace Resources.Infrastructure.DAL.Repositories;
internal sealed class SqlServerResourceRepository : IResourceRepository
{
    private readonly ResourceManagementDbContext _context;

    public SqlServerResourceRepository(ResourceManagementDbContext context)
    {
        _context = context;
    }

    public Resource Get(ResourceId id)
        => _context.Resources.SingleOrDefault(x => x.Id == id);

    public IEnumerable<Resource> GetAll()
        => _context.Resources.ToList();

    public void Add(Resource resource)
    {
        _context.Add(resource);
        _context.SaveChanges();
    }

    public void Update(Resource resource)
    {
        _context.Update(resource);
        _context.SaveChanges();
    }

    public void Delete(Resource resource)
    {
        _context.Remove(resource);
        _context.SaveChanges();
    }
}
