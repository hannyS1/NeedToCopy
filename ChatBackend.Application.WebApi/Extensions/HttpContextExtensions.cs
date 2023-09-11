namespace ChatBackend.Application.WebApi.Extensions;

public static class HttpContextExtensions
{
    public static int GetUserId(this HttpContext context)
    {
        return context.User.RetrieveId();
    }
}