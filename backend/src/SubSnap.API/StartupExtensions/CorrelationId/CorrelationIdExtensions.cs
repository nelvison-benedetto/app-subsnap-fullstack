using SubSnap.API.Middleware.Correlation;

namespace SubSnap.API.StartupExtensions.CorrelationId;

public static class CorrelationIdExtensions
{
    public static IApplicationBuilder UseCorrelationId(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
}