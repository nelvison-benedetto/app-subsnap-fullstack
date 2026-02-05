using Microsoft.OpenApi;

namespace SubSnap.API.StartupExtensions.Swagger;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerConfiguration(
        this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SubSnap API",
                Version = "v1",
                Description = "Subscription management API"
            });
        });

        return services;
    }
    public static IApplicationBuilder UseSwaggerConfiguration(
        this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "SubSnap API v1");
        });

        return app;
    }
}
