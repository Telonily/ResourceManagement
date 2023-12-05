using Microsoft.EntityFrameworkCore;
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

    public Task<ResourceBlockade> GetAsync(ResourceBlockadeId id)
        => _context.ResourceBlockades.SingleOrDefaultAsync(x => x.Id == id);


    public async Task AddAsync(ResourceBlockade resourceBlockade)
    {
        await _context.AddAsync(resourceBlockade);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ResourceBlockade>> GetAllAsync()
        => await _context.ResourceBlockades.ToListAsync();



    public async Task UpdateAsync(ResourceBlockade resourceBlockade)
    {
        _context.Update(resourceBlockade);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ResourceBlockade resourceBlockade)
    {
        _context.Remove(resourceBlockade);
        await _context.SaveChangesAsync();
    }
}
