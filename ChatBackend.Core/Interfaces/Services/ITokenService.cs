using ChatBackend.Core.Entities;

namespace ChatBackend.Core.Interfaces.Services;

public interface ITokenService
{
    public string CreateToken(User user);
    public int? GetUserIdByToken(string token);
}