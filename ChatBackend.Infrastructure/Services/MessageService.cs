using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Core.Specifications.Message;

namespace ChatBackend.Infrastructure.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    
    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }
    
    public async Task<List<Message>> GetByRoomIdAsync(int roomId)
    {
        var specification = new MessageWithUserSpecification(r => r.RoomId == roomId);
        return await _messageRepository.GetAllAsync(specification);
    }

    public async Task Create(int roomId, int userId, string text)
    {
        var message = new Message { RoomId = roomId, UserId = userId, Text = text };
        _messageRepository.Add(message);
        await _messageRepository.SaveChangesAsync();
    }
}