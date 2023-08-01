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
        var user = new User
        {
            Name = "Test",
            LastName = "Test",
            Email = "test@test.com",
            Password = "password",
        };

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
        var user = new User
        {
            Name = string.Empty,
            LastName = "Test",
            Email = "Test",
            Password = "password",
        };

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
        var user = new User
        {
            Name = "Test",
            LastName = string.Empty,
            Email = "Test",
            Password = "password",
        };

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
