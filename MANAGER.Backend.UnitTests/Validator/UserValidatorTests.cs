using FluentAssertions;
using FluentValidation;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Email = "Test",
            Age = 1,
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
            Age = 1,
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
            Age = 1,
        };

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_UserHasAgeLessThanOne_IsValidFalse()
    {
        // Arrange 
        var userValidator = new UserValidator();
        var user = new User
        {
            Name = "Test",
            LastName = string.Empty,
            Email = "Test",
            Age = 0,
        };

        // Act
        var result = userValidator.Validate(user);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
