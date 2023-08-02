using FluentAssertions;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.WebApi.IntegrationTests.Helper;
using MANAGER.Backend.WebApi.IntegrationTests.Infrastructure;
using MANAGER.Backend.WebApi.Model;
using System.Net;
using Xunit;

namespace MANAGER.Backend.WebApi.IntegrationTests.Scenarios;

public class UserScenarios : BaseFixture
{
    public UserScenarios(CustomWebApplicationFactory<Startup> factory)
    : base(factory) { }

    [Fact]
    public async Task Post_NewUser_ReturnOk()
    {
        // Arrange
        var userName = $"NewUserName_{Guid.NewGuid()}";
        var permissions = new List<Roles> { Roles.Admin };

        var userInput = new UserInput
        {
            Name = userName,
            Email = $"{userName}@test.com",
            LastName = "name",
            Password = "password",
            Permissions = permissions
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
        var permissions = new List<Roles> { Roles.Admin };

        var userInput = new UserInput
        {
            Name = userName,
            Email = userEmail,
            LastName = "lastName",
            Password = "password",
            Permissions = permissions
        };

        var user = new User
        (
            userName,
            "lastName",
            userEmail,
            "password",
            permissions
        );

        await TestManagerContext.AddAsync(user);
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

        var permissions = new List<Roles> { Roles.Admin };

        var users = new List<User>
        {
            new User
            (
                userNameOne,
                $"{userNameOne}@test.com",
                "cesar",
                "password",
                permissions
            ),
            new User
            (
                userNameTwo,
                $"{userNameTwo}@test.com",
                "bruneri",
                "password",
                permissions
            )
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
}
