using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatBackend.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context) {}
    
    public async Task<User> GetByLoginPasswordAsync(string login, string password)
    {
        return await Items.SingleOrDefaultAsync(u => u.Name == login && u.Password == password);
    }
}