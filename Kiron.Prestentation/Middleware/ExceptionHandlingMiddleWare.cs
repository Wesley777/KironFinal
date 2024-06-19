using Microsoft.AspNetCore.Mvc;

namespace Kiron.Presentation.Middleware;

public class ExceptionHandlingMiddleWare
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleWare> _logger;

    public ExceptionHandlingMiddleWare(ILogger<ExceptionHandlingMiddleWare> logger, 
                                       RequestDelegate requestDelegate)
    {
        _logger = logger;
        _next = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex) {

            _logger.LogError(ex, $"Exception occurred: {ex.Message}");

            var pd = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Something went wrong, please contact support for more info.",
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(pd);
        }

    }
}
