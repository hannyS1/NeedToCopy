using System.Linq.Expressions;

namespace ChatBackend.Core.Specifications.Message;

public class MessageWithUserSpecification : BaseSpecification<Core.Entities.Message>
{
    public MessageWithUserSpecification(Expression<Func<Core.Entities.Message, bool>> criteria) : base(criteria)
    {
        AddInclude(m => m.User);
    }
}