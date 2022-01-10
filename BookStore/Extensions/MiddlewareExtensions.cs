using BookStore.Middlewares;

namespace BookStore.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseHello(this IApplicationBuilder builder) => builder.UseMiddleware<HelloMiddleware>();
}