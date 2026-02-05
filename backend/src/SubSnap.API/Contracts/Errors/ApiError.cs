namespace SubSnap.API.Contracts.Errors;

public sealed record ApiError(
    int StatusCode,
    string Message
);