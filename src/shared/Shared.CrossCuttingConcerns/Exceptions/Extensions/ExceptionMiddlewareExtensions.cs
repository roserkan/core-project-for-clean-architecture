using Microsoft.AspNetCore.Builder;

namespace Shared.CrossCuttingConcerns.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}