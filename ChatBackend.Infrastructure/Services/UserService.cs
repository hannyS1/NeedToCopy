using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Services;

namespace ChatBackend.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> AuthenticateAsync(string login, string password)
    {
        return await _userRepository.GetByLoginPasswordAsync(login, password);
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}