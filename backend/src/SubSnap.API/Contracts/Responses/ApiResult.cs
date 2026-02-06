using SubSnap.API.Contracts.Errors;

namespace SubSnap.API.Contracts.Responses;

//envelope standard per risposte HTTP!!!top!
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

/*
struttura se va bene 
{
  "success": true,
  "data": { ... },  // i dati reali della tua API
  "error": null
}
struttura se va male 
{
  "success": false,
  "data": null,
  "error": {
    "statusCode": 400,
    "message": "Email already registered"
  }
}

ottimo! coerenza totale nelle risposte della tua API
 */