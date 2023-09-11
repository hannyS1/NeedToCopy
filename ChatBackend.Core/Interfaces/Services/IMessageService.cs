using ChatBackend.Core.Entities;

namespace ChatBackend.Core.Interfaces.Services;

public interface IMessageService
{
    Task<List<Message>> GetByRoomIdAsync(int roomId);
    Task Create(int roomId, int userId, string text);
}