using ChatBackend.Core.Entities;
using ChatBackend.Core.Interfaces.Services;

namespace ChatBackend.Application.WebApi.Extensions;

public static class UserServiceExtensions
{
    public static async Task<User> RetrieveFromHttpContext(this IUserService service, HttpContext context)
    {
        if (context == null)
            return null;
        
        return await service.GetByIdAsync(context.GetUserId());
    }
}