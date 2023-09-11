using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;

namespace ChatBackend.Infrastructure.Repositories;

public class RoomRepository : BaseRepository<Room>, IRoomRepository
{
    public RoomRepository(ApplicationContext context) : base(context) { }
}