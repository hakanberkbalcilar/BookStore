using BookStore.Middlewares;

namespace BookStore.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomException(this IApplicationBuilder builder) => builder.UseMiddleware<CustomExceptionMiddleware>();
}