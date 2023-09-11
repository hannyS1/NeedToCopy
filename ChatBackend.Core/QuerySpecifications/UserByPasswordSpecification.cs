using System.Linq.Expressions;
using ChatBackend.Core.Entities;

namespace ChatBackend.Core.QuerySpecifications;

public class UserByPasswordSpecification : QuerySpecification<User>
{
    private readonly string _password;
    
    public UserByPasswordSpecification(string password)
    {
        _password = password;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return u => u.Password == _password;
    }
}