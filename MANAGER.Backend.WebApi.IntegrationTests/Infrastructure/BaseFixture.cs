using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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

        GenerateToken().GetAwaiter().GetResult();
    }

    public static StringContent? GenerateRequestContent<T>(T input) where T : class
    {
        var json = JsonSerializer.Serialize(input);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    public static async Task<T?> GetDeserealizeContent<T>(HttpContent resultContent)
    {
        var content = await resultContent.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(content, options);
    }

    public async Task GenerateToken()
    {
        var user = new User
        {
            Name = "Admin",
            Email = $"Admin@test.com",
            LastName = "Admin",
            Password = "password",
        };

        var result = await TestManagerContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

        if (result is null)
        {
            await TestManagerContext.AddRangeAsync(user);
            TestManagerContext.SaveChanges();
        }

        var login = new LoginInput
        {
            Email = user.Email,
            Password = user.Password,
        };

        HttpResponseMessage response = await _client
            .PostAsync("api/authentication", GenerateRequestContent(login));

        var token = await response.Content.ReadAsStringAsync();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
    