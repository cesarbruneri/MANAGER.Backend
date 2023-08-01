using FluentResults;
using FluentValidation;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Application.Users.Login;
using MANAGER.Backend.Application.Users.Query;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Validator;
using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.Sql.Repositories.Base;
using MANAGER.Backend.Sql.Repositories.Users;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Auth:Secret"]);

        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        return services;
    }

    public static IServiceCollection AddDependenceInjection(this IServiceCollection services)
    {
        services.RepositoryDependenceInjection();
        services.MediatorDependenceInjection();

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

        return services;
    }

    private static IServiceCollection MediatorDependenceInjection(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateUserCommand, Result>, CreateUserCommandHandler>();
        services.AddTransient<IRequestHandler<GetAllUsersQuery, Result<List<User>>>, GetAllUsersQueryHandler>();
        services.AddTransient<IRequestHandler<LoginCommand, Result<string>>, LoginCommandHandler>();

        return services;
    }

    private static IServiceCollection RepositoryDependenceInjection(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}