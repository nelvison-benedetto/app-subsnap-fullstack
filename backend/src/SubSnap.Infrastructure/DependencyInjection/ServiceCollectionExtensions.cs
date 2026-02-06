using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SubSnap.Core.Contracts.Repositories;
using SubSnap.Core.Contracts.Services;
using SubSnap.Core.Contracts.UnitOfWork;
using SubSnap.Core.Services.Application;
using SubSnap.Infrastructure.Persistence.Context;
using SubSnap.Infrastructure.Persistence.UnitOfWork;
using SubSnap.Infrastructure.Repositories.Implementations;

namespace SubSnap.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(  // custom extension x IServiceCollection (la collezione di servizi della DI di ASP.NET Core)
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( configuration.GetConnectionString("sqlConnection")));

        //registrazione dei repositories!!
        //quando qualcuno chiede IUserRepository, la DI darà un’istanza concreta di UserRepository
        services.AddScoped<IUserRepository, UserRepository>();  //!!!repositories
        services.AddScoped<IUnitOfWork, EFUnitOfWork>();     //!!!unit of work
        services.AddScoped<IUserService, UserService>();     //!!!application services

        return services;
    }
}
