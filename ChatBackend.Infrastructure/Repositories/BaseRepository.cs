using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ChatBackend.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationContext _context;
    protected readonly DbSet<T> Items;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        Items = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await Items.ToListAsync();
    }

    public async Task<List<T>> GetAllAsync(ISpecification<T> specification)
    {
        var itemsWithSpec = SpecificationEvaluator<T>.GetQuery(Items.AsQueryable(), specification);
        return await itemsWithSpec.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Items.FindAsync(id);
    }

    public void Add(T entity)
    {
        Items.Add(entity);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityEntry = await Items.AddAsync(entity);
        return entityEntry.Entity;
    }

    public void Delete(T entity)
    {
        Items.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

public class BaseQueryBuilder<T> where T : BaseEntity
{
    public BaseQueryBuilder(DbSet<T> items)
    {
        
    }
}