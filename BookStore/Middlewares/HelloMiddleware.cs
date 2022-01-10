namespace BookStore.Middlewares;

public class HelloMiddleware
{
    private readonly RequestDelegate _next;
    public HelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Hello World!");
        await _next.Invoke(context);
        Console.WriteLine("Bye World!");
    }
}