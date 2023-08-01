using FluentAssertions;
using MANAGER.Backend.Core.Constants;
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
        var userName = $"NewUserName_{Guid.NewGuid()}";

        var userInput = new UserInput
        {
            Name = userName,
            Email = $"{userName}@test.com",
            LastName = "name",
            Password = "password",
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
        var userName = $"NewUserName_{Guid.NewGuid()}";
        var userEmail = $"{userName}@test.com";

        var userInput = new UserInput
        {
            Name = userName,
            Email = userEmail,
            LastName = "name",
            Password = "password",
        };

        var user = new User
        {
            Name = userName,
            Email = userEmail,
            LastName = "lastName",
            Password = "password",
        };

        TestManagerContext.Add(user);
        TestManagerContext.SaveChanges();

        // Act
        HttpResponseMessage response = await _client
            .PostAsync("api/user/create-user", GenerateRequestContent(userInput));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);

        var errorResult = await GetDeserealizeContent<ErrorResponseTest>(response.Content);
        errorResult.Should().NotBeNull();
        errorResult?.Errors?.FirstOrDefault()?.Reason.Should().BeEquivalentTo("useralreadyexists");
    }

    [Fact]
    public async Task Post_GetAllUsers_ReturnUsers()
    {
        // Arrange
        var userNameOne = $"NewUserName_{Guid.NewGuid()}";
        var userNameTwo = $"NewUserName_{Guid.NewGuid()}";

        var users = new List<User>
        {
            new User
            {
                Name = userNameOne,
                Email = $"{userNameOne}@test.com",
                LastName = "cesar",
                Password = "password",
            },
            new User
            {
                Name = userNameTwo,
                Email = $"{userNameTwo}@test.com",
                LastName = "bruneri",
            Password = "password",
            }
        };

        await TestManagerContext.AddRangeAsync(users);
        TestManagerContext.SaveChanges();

        // Act
        HttpResponseMessage response = await _client
            .GetAsync("api/user/all-users");

        // Assert

        var result = await GetDeserealizeContent<List<User>>(response.Content);
        result.Should().NotBeNull();
        result.Should().Contain(users);
    }

    private static async Task<T?> GetDeserealizeContent<T>(HttpContent resultContent)
    {
        var content = await resultContent.ReadAsStringAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(content, options);
    }

    private static StringContent? GenerateRequestContent(UserInput userInput)
    {
        var json = JsonSerializer.Serialize(userInput);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}
