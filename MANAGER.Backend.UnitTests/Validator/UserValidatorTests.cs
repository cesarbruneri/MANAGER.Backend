using FluentAssertions;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Validator;
using Xunit;

namespace MANAGER.Backend.UnitTests.Validator;

public class UserValidatorTests
{
    [Fact]
    public void Validate_UserValid_IsValidTrue()
    {
        // Arrange 
        var userValidator = new UserValidator();
        var permissions = new List<Roles> { Roles.Admin };

        var user = new User(
            "Test",
            "Test",
            "Test@Test.com",
            "password",
            permissions
        );

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_UserHasNoName_IsValidFalse()
    {
        // Arrange 
        var userValidator = new UserValidator();
        var permissions = new List<Roles> { Roles.Admin };
        var user = new User
        (
            string.Empty,
            "Test",
            "Test",
            "password",
            permissions
        );

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UserHasNoLastName_IsValidFalse()
    {
        // Arrange 
        var userValidator = new UserValidator();
        var permissions = new List<Roles> { Roles.Admin };
        var user = new User
        (
            "Test",
            string.Empty,
            "Test",
            "password",
            permissions
        );

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UserHasNoPassword_IsValidFalse()
    {
        // Arrange 
        var userValidator = new UserValidator();
        var permissions = new List<Roles> { Roles.Admin };
        var user = new User
        (
            "Test",
            "password",
            "Test",
            string.Empty,
            permissions
        );

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
