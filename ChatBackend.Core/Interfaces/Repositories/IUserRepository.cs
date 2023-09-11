using ChatBackend.Core.Entities;

namespace ChatBackend.Core.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User> GetByLoginPasswordAsync(string login, string password);
}
