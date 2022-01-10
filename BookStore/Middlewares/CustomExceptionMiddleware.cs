using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace BookStore.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            string message = "[Request] " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);
            await _next.Invoke(context);
            watch.Stop();
            message = "[Response] " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.ElapsedMilliseconds + "ms";
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleException(context, ex, watch);
        }
    }

    public Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        string message = "[Error] " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " Error Message: " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds + "ms";
        Console.WriteLine(message);

        var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
        return context.Response.WriteAsync(result);
    }
}