using Microsoft.EntityFrameworkCore;
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

    public Task<Resource> GetAsync(ResourceId id)
        => _context.Resources.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<Resource>> GetAllAsync()
        => await _context.Resources.ToListAsync();

    public async Task AddAsync(Resource resource)
    {
        await _context.AddAsync(resource);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Resource resource)
    {
        _context.Update(resource);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Resource resource)
    {
        _context.Remove(resource);
        await _context.SaveChangesAsync();
    }
}
