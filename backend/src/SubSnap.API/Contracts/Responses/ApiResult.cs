using SubSnap.API.Contracts.Errors;

namespace SubSnap.API.Contracts.Responses;

public sealed record ApiResult<T>(
    bool Success,
    T? Data,
    ApiError? Error
)
{
    public static ApiResult<T> Ok(T data)  //SUCCESS!  lo trovi e.g. in UserController.cs
        => new(true, data, null);

    public static ApiResult<T> Fail(ApiError error)   //FAILURE!  lo trovi e.g. in API/Middleware/ExceptionHandlingMiddleware.cs
        => new(false, default, error);
}