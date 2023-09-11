using System.Text.Json;

namespace ChatBackend.Application.WebApi.Errors;

public class ApiException : Exception
{
    protected const int DefaultStatusCode = StatusCodes.Status400BadRequest;
    public int StatusCode { get; }
    public string Detail { get; }

    public ApiException(int statusCode = DefaultStatusCode)
    {
        StatusCode = statusCode;
    }
    
    public ApiException(object detail, int statusCode = DefaultStatusCode)
    {
        StatusCode = statusCode;
        Detail = JsonSerializer.Serialize(detail);
    }
    public ApiException(string detail, int statusCode = DefaultStatusCode)
    {
        StatusCode = statusCode;
        Detail = detail;
    }
}