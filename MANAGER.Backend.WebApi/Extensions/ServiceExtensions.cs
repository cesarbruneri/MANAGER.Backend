using FluentResults;
using FluentValidation;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Validator;
using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.Sql.Repositories.Base;
using MANAGER.Backend.Sql.Repositories.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

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

    public static IServiceCollection AddValidatorDependenceInjection(this IServiceCollection services)
    {
        services.AddTransient<IValidator<User>, UserValidator>();

        return services;
    }

    public static IServiceCollection ConfigureMediatorR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddTransient<IRequestHandler<CreateUserCommand, Result>, CreateUserCommandHandler>();

        return services;
    }
}