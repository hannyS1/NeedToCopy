using ChatBackend.Core.Interfaces.Services;
using ChatBackend.Infrastructure.Configs;
using ChatBackend.Infrastructure.Services;
using Microsoft.Extensions.Options;

namespace ChatBackend.Application.WebApi.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<JwtOptions> _jwtOptions;
    private ITokenService TokenService => new JwtTokenService(_jwtOptions);
    
    public JwtMiddleware(RequestDelegate next, IOptions<JwtOptions> jwtOptions)
    {
        _next = next;
        _jwtOptions = jwtOptions;
    }

    public async Task Invoke(HttpContext context, IUserService userService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            await AttachUserToContext(context, userService, token);
        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, IUserService userService, string token)
    {
        var userId = TokenService.GetUserIdByToken(token);
        var user = userId.HasValue ? await userService.GetByIdAsync(userId.Value) : null;
        context.Items["User"] = user;
    }

}