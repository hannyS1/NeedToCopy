using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Services;

namespace ChatBackend.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUserRoomRepository _userRoomRepository;
    
    public RoomService(IRoomRepository roomRepository, IUserRoomRepository userRoomRepository)
    {
        _roomRepository = roomRepository;
        _userRoomRepository = userRoomRepository;
    }
    
    public async Task CreateAsync(int firstUserId, int secondUserId)
    {
        var room = new Room();
        room = await _roomRepository.AddAsync(room);
        // await _roomRepository.SaveChangesAsync();
        _userRoomRepository.Add(new UserRoom {RoomId = room.Id, UserId = firstUserId});
        _userRoomRepository.Add(new UserRoom {RoomId = room.Id, UserId = secondUserId});
        await _userRoomRepository.SaveChangesAsync();
    }
}