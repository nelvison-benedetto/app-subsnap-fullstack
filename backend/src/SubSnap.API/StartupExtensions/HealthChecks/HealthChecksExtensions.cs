namespace SubSnap.API.StartupExtensions.HealthChecks;

//utile x per Docker,K8s,Load Balancer,Azure,AWS,ect.. zero coupling
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