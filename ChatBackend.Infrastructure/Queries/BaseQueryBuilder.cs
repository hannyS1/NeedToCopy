using System.Linq.Expressions;
using ChatBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatBackend.Infrastructure.Queries;

public class BaseQueryBuilder<T> where T : BaseEntity
{
    private IQueryable<T> _entities;

    public BaseQueryBuilder() { }

    public BaseQueryBuilder<T> Start(DbSet<T> entities)
    {
        _entities = entities;
        return this;
    }

    public BaseQueryBuilder<T> ById(int entityId)
    {
        _entities = _entities.Where((entity) => entity.Id == entityId);
        return this;
    }

    public Expression Query => _entities.Expression;

    public void A()
    {
        
    }

}