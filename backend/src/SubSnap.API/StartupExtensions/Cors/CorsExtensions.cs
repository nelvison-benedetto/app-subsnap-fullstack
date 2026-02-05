namespace SubSnap.API.StartupExtensions.Cors;

public static class CorsExtensions
{
    private const string CorsPolicyName = "DefaultCorsPolicy";

    public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, builder =>
            {
                builder
                    .AllowAnyOrigin()  //ovviamente in prod non va bene AllowAnyOrigin
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
    public static IApplicationBuilder UseCorsConfiguration(
        this IApplicationBuilder app)
    {
        app.UseCors(CorsPolicyName);
        return app;
    }
}
