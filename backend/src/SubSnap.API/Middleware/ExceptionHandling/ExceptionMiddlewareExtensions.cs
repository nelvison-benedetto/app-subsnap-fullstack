using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using SubSnap.API.Contracts.Errors;
using SubSnap.API.Contracts.Responses;
using SubSnap.Core.Domain.Exceptions;

namespace SubSnap.API.Middleware.ExceptionHandling;

public static class ExceptionMiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()?.Error;
                if (exception is null) return;
                var (statusCode, message) = exception switch
                {
                    DomainException => (
                        StatusCodes.Status400BadRequest,
                        exception.Message
                    ),
                    ValidationException => (
                        StatusCodes.Status400BadRequest,
                        exception.Message
                    ),
                    _ => (
                        StatusCodes.Status500InternalServerError,
                        "Unexpected server error"
                    )
                };
                context.Response.StatusCode = statusCode;
                var error = new ApiError(statusCode, message);
                var result = ApiResult<object>.Fail(error);
                await context.Response.WriteAsJsonAsync(result);
            });
        });
    }
}
