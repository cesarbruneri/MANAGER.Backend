using FluentAssertions;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.WebApi.IntegrationTests.Helper;
using MANAGER.Backend.WebApi.IntegrationTests.Infrastructure;
using MANAGER.Backend.WebApi.Model;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace MANAGER.Backend.WebApi.IntegrationTests.Scenarios;

public class UserScenarios : BaseFixture
{
    public UserScenarios(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

    [Fact]
    public async Task Post_NewUser_ReturnOk()
    {
        // Arrange
        var userInput = new UserInput
        {
            Name = "name",
            Email = "test@test.com",
            LastName = "name",
            Age = 32,
        };

        // Act
        var response = await _client
            .PostAsync("api/user/create-user", GenerateRequestContent(userInput));

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_UserExists_ReturnError()
    {
        // Arrange
        var userInput = new UserInput
        {
            Name = "name",
            Email = "email",
            LastName = "name",
            Age = 32,
        };

        var user = new User
        {
            Name = "name",
            Email = "email",
            LastName = "name",
            Age = 32,
        };

        TestManagerContext.Add(user);
        TestManagerContext.SaveChanges();

        // Act
        HttpResponseMessage response = await _client
            .PostAsync("api/user/create-user", GenerateRequestContent(userInput));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);        

        var errorResult = await GetDeserealizeContent(response.Content);
        errorResult.Should().NotBeNull();
        errorResult?.Errors?.FirstOrDefault()?.Reason.Should().BeEquivalentTo("useralreadyexists");
    }

    private static async Task<ErrorResponseTest> GetDeserealizeContent(HttpContent resultContent)
    {
        var content = await resultContent.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<ErrorResponseTest>(content, options) ?? new ErrorResponseTest();
    }

    private static StringContent? GenerateRequestContent(UserInput userInput)
    {
        var json = JsonSerializer.Serialize(userInput);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
