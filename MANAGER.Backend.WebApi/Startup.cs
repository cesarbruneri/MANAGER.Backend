using MANAGER.Backend.WebApi.Extensions;

namespace MANAGER.Backend.WebApi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigurationServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddPersistence(Configuration);
        services.ConfigureMediatorR();
        services.AddValidatorDependenceInjection();
        services.AddDependenceInjection();
        services.AddEndpointsApiExplorer();
        services.ConfigureAuthentication(Configuration);
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}
