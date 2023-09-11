namespace ChatBackend.Core.Interfaces.Services;

public interface IRoomService
{
    Task CreateAsync(int firstUserId, int secondUserId);
}