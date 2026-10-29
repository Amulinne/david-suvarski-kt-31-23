using System.Net;

namespace david_suvarski_kt_31_23.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            var responseModel = new ResponseModel<object>
            {
                Succeeded = false,
                Message = exception.Message,
                Errors = exception.InnerException is null
                    ? []
                    : [exception.InnerException.Message]
            };

            await context.Response.WriteAsJsonAsync(responseModel);
        }
    }
}

public class ResponseModel<T>
{
    public bool Succeeded { get; set; }

    public string? Message { get; set; }

    public List<string> Errors { get; set; } = [];

    public T? Data { get; set; }

    public ResponseModel()
    {
    }

    public ResponseModel(T data, string? message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public ResponseModel(string message)
    {
        Succeeded = true;
        Message = message;
    }
}
