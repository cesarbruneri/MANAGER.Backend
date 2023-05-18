using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.Sql.Repositories.Base;
using MANAGER.Backend.Sql.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace MANAGER.Backend.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ManagerContext>(
            opt => 
                opt.UseSqlServer(configuration.GetConnectionString("Default"))
                .EnableSensitiveDataLogging());

        return services;
    }
    //https://www.youtube.com/watch?v=Bz5JCbWnaHo
    public static IServiceCollection AddDependenceInjection(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddMediatorR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));

        return services;
    }
}
