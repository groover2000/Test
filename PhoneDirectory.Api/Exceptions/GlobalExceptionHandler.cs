using System.Reflection.Metadata.Ecma335;
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
        return false;
    }


}