using ChatBackend.Application.WebApi.Extensions;
using ChatBackend.Application.WebApi.Dto;
using ChatBackend.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatBackend.Application.WebApi.Controllers;


[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _contextAccessor;
    
    public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
    {
        _userService = userService;
        _contextAccessor = contextAccessor;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserViewDto>> GetCurrentUser()
    {
        var user = await _userService.RetrieveFromHttpContext(_contextAccessor.HttpContext);
        if (user == null)
            return Unauthorized(new { message = "invalid token" });
        
        return Ok(new UserViewDto { Id = user.Id, Name = user.Name });
    }
    
}