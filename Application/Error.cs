namespace Application;

public class Error
{
    public string Message { get; }
    public int StatusCode { get; }

    private Error(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public static Error NotFound(string message) => new Error(message, 404);
    public static Error BadRequest(string message) => new Error(message, 400);
    public static Error Unauthorized(string message) => new Error(message, 401);
    public static Error Forbidden(string message) => new Error(message, 403);
    public static Error InternalServerError(string message) => new Error(message, 500);
}
