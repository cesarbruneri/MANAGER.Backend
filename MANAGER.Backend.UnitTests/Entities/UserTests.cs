using FluentAssertions;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Users;
using Xunit;

namespace MANAGER.Backend.UnitTests.Entities;

public class UserTests
{
    [Fact]
    public void Constructor_UserEntityShouldBeCreated()
    {
        // Arrange
        var permissions = new List<Roles> { Roles.Admin };

        // Act
        var user = new User(
            "Test", "Test", "Test", "Test", permissions);

        // Assert
        user.Name.Should().Be("Test");
        user.LastName.Should().Be("Test");
        user.Email.Should().Be("Test");
        user.Password.Should().Be("Test");
        user.Permissions?.FirstOrDefault()?.Role.Should().Be(Roles.Admin);
        user.Permissions?.FirstOrDefault()?.UserId.Should().Be(user.Id);
    }
}
