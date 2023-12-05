using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;

namespace Resources.Infrastructure.DAL.Repositories;
internal sealed class SqlServerResourceBlockadesRepository : IResourceBlockadesRepository
{
    private readonly ResourceManagementDbContext _context;

    public SqlServerResourceBlockadesRepository(ResourceManagementDbContext context)
    {
        _context = context;
    }

    public ResourceBlockade Get(ResourceBlockadeId id)
        => _context.ResourceBlockades.SingleOrDefault(x => x.Id == id);


    public void Add(ResourceBlockade resourceBlockade)
    {
        _context.Add(resourceBlockade);
        _context.SaveChanges();
    }

    public IEnumerable<ResourceBlockade> GetAll()
        => _context.ResourceBlockades.ToList();


    public void Update(ResourceBlockade resourceBlockade)
    {
        _context.Update(resourceBlockade);
        _context.SaveChanges();
    }

    public void Delete(ResourceBlockade resourceBlockade)
    {
        _context.Remove(resourceBlockade);
        _context.SaveChanges();
    }
}
