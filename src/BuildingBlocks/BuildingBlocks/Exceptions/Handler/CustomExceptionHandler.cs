using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {Message}, Time of occurancy: {Time}", exception.Message, DateTime.UtcNow);

        (string Detail, string Title, int StatusCode) = exception switch
        {
            InternalServerException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError),
            BadRequestException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound),
            ValidationException => (exception.Message, exception.GetType().Name,
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest)
        };
        var problemDetails = new ProblemDetails()
        {
            Title = Title,
            Status = StatusCode,
            Detail = Detail,
            Instance = httpContext.Request.Path
        };
        
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
        
        if(exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationError", validationException.Errors);
        }
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}
