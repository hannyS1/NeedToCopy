using System.Linq.Expressions;
using ChatBackend.Core.Entities;

namespace ChatBackend.Core.QuerySpecifications;

public class UserByNameSpecification : QuerySpecification<User>
{
    private readonly string _name;
    
    public UserByNameSpecification(string name)
    {
        _name = name;
    }
    
    public override Expression<Func<User, bool>> ToExpression()
    {
        return u => u.Name == _name;
    }
}