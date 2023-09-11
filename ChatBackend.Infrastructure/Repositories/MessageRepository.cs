using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;

namespace ChatBackend.Infrastructure.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(ApplicationContext context) : base(context) { }
}
