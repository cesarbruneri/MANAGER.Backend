using MANAGER.Backend.Sql.Infrastructure.Context;

namespace MANAGER.Backend.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication PrepareDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ManagerContext>();

        SeedContext.PrepareDatabase(context);

        return app;
    }
}
