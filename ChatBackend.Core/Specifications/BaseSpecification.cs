using System.Linq.Expressions;
using ChatBackend.Core.Interfaces.Specifications;

namespace ChatBackend.Core.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification() {}

    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }
    public int? Limit { get; set; }
    public int? Offset { get; set; }

    protected void AddInclude(Expression<Func<T, object>> expression)
    {
        Includes.Add(expression);
    }

}