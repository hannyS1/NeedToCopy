using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ChatBackend.Infrastructure
{
    public static class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            
            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            
            query = specification.Includes
                .Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}