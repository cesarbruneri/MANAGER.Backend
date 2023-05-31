using MANAGER.Backend.Sql.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MANAGER.Backend.WebApi.IntegrationTests.Infrastructure;

public class BaseFixture : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;

    protected readonly HttpClient _client;
    protected readonly ManagerContext TestManagerContext;

    public BaseFixture(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        var serviceProvider = _factory
            .Services
            .CreateScope()
            .ServiceProvider;

        TestManagerContext = serviceProvider
            .GetRequiredService<ManagerContext>();
    }
}
