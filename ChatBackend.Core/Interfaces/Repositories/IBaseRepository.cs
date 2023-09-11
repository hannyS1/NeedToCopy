using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Specifications;

namespace ChatBackend.Core.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAllAsync();
    public Task<List<T>> GetAllAsync(ISpecification<T> specification);
    public Task<T> GetByIdAsync(int id);
    public void Add(T entity);
    public Task<T> AddAsync(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}