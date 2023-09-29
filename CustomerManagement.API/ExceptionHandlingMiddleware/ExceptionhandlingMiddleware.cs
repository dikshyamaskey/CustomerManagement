using System.Net;
using System.Text.Json;
using CustomerManagement.Application.Common.Exceptions;
using CustomerManagement.Core.Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.API.ExceptionHandlingMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var exceptionDetails = GetExceptionDetails(exception);

            var problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Detail = exceptionDetails.Detail,
            };
            
            var error = new Error((HttpStatusCode)exceptionDetails.Status, exceptionDetails.Detail);

            var result = Result.Failure(error);

            if (exceptionDetails.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            }

            context.Response.StatusCode = exceptionDetails.Status;

            await context.Response.WriteAsJsonAsync(result);
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "Validation error",
                 string.Join(", ", validationException.Errors.Select(error => new { error.PropertyName, error.ErrorMessage })),
                validationException.Errors),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Server error",
                $"An unexpected error has occurred : {exception.Message}",
                null)
        };
    }

    private record ExceptionDetails(
        int Status,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}