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
        services.AddMediatR(typeof(Startup).Assembly);
        services.AddTransient<IRequestHandler<CreateUserCommand, Result>, CreateUserCommandHandler>();

        return services;
    }
}
/*
 System.AggregateException
  HResult=0x80131500
  Message=Some services are not able to be constructed (Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[MANAGER.Backend.Application.Users.Create.CreateUserCommand,FluentResults.Result] Lifetime: Scoped ImplementationType: MANAGER.Backend.Application.Users.Create.CreateUserCommandHandler': Unable to resolve service for type 'FluentValidation.IValidator`1[MANAGER.Backend.Core.Domain.Entities.Users.User]' while attempting to activate 'MANAGER.Backend.Application.Users.Create.CreateUserCommandHandler'.)
  Source=Microsoft.Extensions.DependencyInjection
  StackTrace:
   at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection`1 serviceDescriptors, ServiceProviderOptions options)
   at Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection services, ServiceProviderOptions options)
   at Microsoft.Extensions.Hosting.HostApplicationBuilder.Build()
   at Microsoft.AspNetCore.Builder.WebApplicationBuilder.Build()
   at Program.<Main>$(String[] args) in E:\Projects\ManagerUser\MANAGER.Backend\MANAGER.Backend.WebApi\Program.cs:line 9

  This exception was originally thrown at this call stack:
    [External Code]

Inner Exception 1:
InvalidOperationException: Error while validating the service descriptor 'ServiceType: MediatR.IRequestHandler`2[MANAGER.Backend.Application.Users.Create.CreateUserCommand,FluentResults.Result] Lifetime: Scoped ImplementationType: MANAGER.Backend.Application.Users.Create.CreateUserCommandHandler': Unable to resolve service for type 'FluentValidation.IValidator`1[MANAGER.Backend.Core.Domain.Entities.Users.User]' while attempting to activate 'MANAGER.Backend.Application.Users.Create.CreateUserCommandHandler'.

Inner Exception 2:
InvalidOperationException: Unable to resolve service for type 'FluentValidation.IValidator`1[MANAGER.Backend.Core.Domain.Entities.Users.User]' while attempting to activate 'MANAGER.Backend.Application.Users.Create.CreateUserCommandHandler'.

 */