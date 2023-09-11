using System.Linq.Expressions;
using ChatBackend.Core.Entities;

namespace ChatBackend.Core.QuerySpecifications;


public abstract class QuerySpecification<TEntity> where TEntity : BaseEntity
{
    public abstract Expression<Func<TEntity , bool>> ToExpression();
 
    public bool IsSatisfiedBy(TEntity entity)
    {
        Func<TEntity , bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public QuerySpecification<TEntity> And(QuerySpecification<TEntity> other)
    {
        return new AndSpecification<TEntity>(this, other);
    }
}           


public class AndSpecification<TEntity> : QuerySpecification<TEntity> where TEntity : BaseEntity
{
    private readonly QuerySpecification<TEntity> _left;
    private readonly QuerySpecification<TEntity> _right;
 
    public AndSpecification(QuerySpecification<TEntity> left, QuerySpecification<TEntity> right)
    {
        _right = right;
        _left = left;
    }
 
    public override Expression<Func<TEntity , bool>> ToExpression()
    {
        Expression<Func<TEntity , bool>> leftExpression = _left.ToExpression();
        Expression<Func<TEntity , bool>> rightExpression = _right.ToExpression();
 
        BinaryExpression andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
 
        return Expression.Lambda<Func<TEntity , bool>>(andExpression, leftExpression.Parameters.Single());
    }
}

public class OrSpecification<TEntity> : QuerySpecification<TEntity> where TEntity : BaseEntity
{
    private readonly QuerySpecification<TEntity> _left;
    private readonly QuerySpecification<TEntity> _right;


    public OrSpecification(QuerySpecification<TEntity> left, QuerySpecification<TEntity> right) {
        _right = right;
        _left = left;
    }


    public override Expression<Func<TEntity, bool>> ToExpression() 
    {
        Expression<Func<TEntity , bool>> leftExpression = _left.ToExpression();
        Expression<Func<TEntity , bool>> rightExpression = _right.ToExpression();
 
        BinaryExpression andExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);
 
        return Expression.Lambda<Func<TEntity , bool>>(andExpression, leftExpression.Parameters.Single());
    }
}

public class NotSpecification<TEntity> : QuerySpecification<TEntity> where TEntity : BaseEntity 
{
    private readonly QuerySpecification<TEntity> _specification;


    public NotSpecification(QuerySpecification<TEntity> specification)
    {
        _specification = specification;
    }


    public override Expression<Func<TEntity, bool>> ToExpression() 
    {
        Expression<Func<TEntity , bool>> expression = _specification.ToExpression();
        return Expression.Lambda<Func<TEntity , bool>>(Expression.Not(expression));
    }
}