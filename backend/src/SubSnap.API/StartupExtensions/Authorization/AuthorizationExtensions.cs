namespace SubSnap.API.StartupExtensions.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationConfiguration(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("UserOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
            });
        });
        return services;
    }

    //e poi la usi cosi  [Authorize(Policy = "UserOnly")]
}
