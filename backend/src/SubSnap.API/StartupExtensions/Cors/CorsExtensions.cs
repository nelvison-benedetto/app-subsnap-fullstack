namespace SubSnap.API.StartupExtensions.Cors;

public static class CorsExtensions
{
    private const string CorsPolicyName = "DefaultCorsPolicy";

    public static IServiceCollection AddCorsConfiguration( this IServiceCollection services)  //x registrazione
    {
        services.AddCors(options =>  
        {
            options.AddPolicy(CorsPolicyName, builder =>
            {
                builder
                    .AllowAnyOrigin()  //ovviamente in prod non va bene AllowAnyOrigin, ma usi e.g..WithOrigins("https://app.subsnap.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        return services;
    }
    public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app)  //x uso
    {
        app.UseCors(CorsPolicyName);
        return app;
    }

}
