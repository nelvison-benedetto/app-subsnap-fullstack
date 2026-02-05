namespace SubSnap.API.StartupExtensions.HealthChecks;

//perfetto per Docker / K8s, zero coupling
public static class HealthChecksExtensions
{
    public static IServiceCollection AddHealthChecksConfiguration(
        this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }

    public static IApplicationBuilder UseHealthChecksConfiguration(
        this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health");
        return app;
    }
}