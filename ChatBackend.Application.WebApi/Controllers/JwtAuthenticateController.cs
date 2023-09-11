using ChatBackend.Application.WebApi.Dto;
using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatBackend.Application.WebApi.Controllers;


[Route("api/auth/jwt")]
public class JwtAuthenticateController : Controller
{
    private readonly IUserService _userService;
    private readonly TokenServiceFactory _tokenServiceFactory;

    public JwtAuthenticateController(IUserService userService, TokenServiceFactory tokenServiceFactory)
    {
        _userService = userService;
        _tokenServiceFactory = tokenServiceFactory;
    }
    
    [HttpPost("login-password")]
    public async Task<ActionResult<JwtAuthResponseDto>> EmailPasswordAuth([FromBody] LoginPasswordAuthRequestDto dto)
    {
        var user = await _userService.AuthenticateAsync(dto.Username, dto.Password);
        if (user == null)
            return BadRequest(new { Message = "Invalid username or password" });
        
        var token = TokenService.CreateToken(user);
        return Ok(new JwtAuthResponseDto { AccessToken = token });
    }

    private ITokenService TokenService => _tokenServiceFactory.Create(TokenType.Jwt);
}