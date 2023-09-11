using ChatBackend.Application.WebApi.Errors;

namespace ChatBackend.Application.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException e)
        {
            context.Response.StatusCode = e.StatusCode;
            if (e.Detail != null)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(e.Detail);
            }
        }
    }
}