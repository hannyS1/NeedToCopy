using ChatBackend.Core.Entities;

namespace ChatBackend.Core.Interfaces.Services;

public interface IUserService
{
    public Task<User> AuthenticateAsync(string login, string password);
    public Task<User> GetByIdAsync(int id);
}