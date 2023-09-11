using System.Linq.Expressions;
using ChatBackend.Core.Entities;

namespace ChatBackend.Infrastructure.Queries;

public class MessageQueryBuilder
{
    public MessageQueryBuilder()
    {
        Query = BaseQuery;
    }

    public Expression<Func<Message, bool>> Query { get; private set; }
    
    private Expression<Func<Message, bool>> BaseQuery => (message) => true;

    public Expression<Func<Message, bool>> GetUserFilter(int userId) => (message) => message.UserId == userId;
    
    
}