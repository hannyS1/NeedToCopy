using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;

namespace ChatBackend.Infrastructure.Repositories;

public class UserRoomRepository : BaseRepository<UserRoom>, IUserRoomRepository
{
    public UserRoomRepository(ApplicationContext context) : base(context)
    {
    }
}