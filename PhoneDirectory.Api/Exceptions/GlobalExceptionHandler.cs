using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PhoneDirectory.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{

    private readonly ILogger<GlobalExceptionHandler> logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger
    )
    {
        this.logger = logger;

    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        logger.LogError(exception, "Произошла ошибка");

        if (exception is DuplicateEmailException duplicateEmailException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;


            await httpContext.Response.WriteAsJsonAsync(
                new ProblemDetails
                {
                    Status = StatusCodes.Status409Conflict,
                    Title = "Конфликт данных",
                    Detail = duplicateEmailException.Message
                },
                cancellationToken
            );
            return true;
        }

        if (exception is PersonValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректные данные"
            };

            problemDetails.Extensions["errors"] =
                new Dictionary<string, string[]>
                {
                    [validationException.Field] =
                        new[] { validationException.Message }
                };

            await httpContext.Response.WriteAsJsonAsync(
                problemDetails,
                cancellationToken
            );
            return true;
        }
        return false;
    }




}