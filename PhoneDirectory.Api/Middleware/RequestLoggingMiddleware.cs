namespace PhoneDirectory.Api.Middleware;


public class RequestLoggingMiddleware

{
    private readonly RequestDelegate next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Middleware ПОЙМАЛ: {ex.GetType().Name}");
            throw;
        }

    }
}