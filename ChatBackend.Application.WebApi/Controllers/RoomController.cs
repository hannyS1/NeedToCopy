using ChatBackend.Application.WebApi.Extensions;
using ChatBackend.Application.WebApi.Dto;
using ChatBackend.Application.WebApi.Helpers;
using ChatBackend.Core.Interfaces.Repositories;
using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Core.Specifications.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatBackend.Application.WebApi.Controllers;

[Route("api/rooms")]
public class RoomController : Controller
{
    private readonly IMessageService _messageService;
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomService _roomService;
    private readonly HttpContext _context;
    
    public RoomController(
        IMessageService messageService, 
        IRoomRepository roomRepository,
        IRoomService roomService,
        IHttpContextAccessor contextAccessor)
    {
        
        _messageService = messageService;
        _roomRepository = roomRepository;
        _roomService = roomService;
        _context = contextAccessor.HttpContext;
    }
    
    [Authorize]
    [HttpGet("{roomId:int}/messages")]
    public async Task<ActionResult<Paginated<AnnotatedMessageDto>>> GetMessages(
        [FromRoute] int roomId,
        [FromQuery] MessageSpecParams specParams)
    {
        var currentUserId = _context.User.RetrieveId();
        var messages = await _messageService.GetByRoomIdAsync(roomId);
        var annotatedMessages = messages.Select(m => new AnnotatedMessageDto(m, currentUserId)).ToList();
        
        return Ok(new Paginated<AnnotatedMessageDto>(annotatedMessages));
    }

    [Authorize]
    [HttpPost("")]
    public async Task<ActionResult> Create([FromBody] RoomCreateDto dto)
    {
        var currentUserId = _context.User.RetrieveId();
        await _roomService.CreateAsync(currentUserId, dto.UserId);
        return NoContent();
    }

    [Authorize]
    [HttpPost("{roomId:int}/messages")]
    public async Task<ActionResult> SendMessage([FromRoute] int roomId, [FromBody] MessageCreateDto dto)
    {
        var userId = _context.User.RetrieveId();

        var room = await _roomRepository.GetByIdAsync(roomId);
        if (room == null)
            return BadRequest(new { message = "incorrect room id" });

        await _messageService.Create(room.Id, userId, dto.Text);
        return NoContent();
    }   
}