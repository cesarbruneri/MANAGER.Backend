using FluentAssertions;
using FluentResults;
using FluentValidation.Results;
using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Errors;
using MANAGER.Backend.Core.Extensions;
using MANAGER.Backend.UnitTests.Mocks;

using Xunit;

namespace MANAGER.Backend.UnitTests.Application.UserCommand;

public class CreateUserCommandHandlerTests
{
    private readonly CreateUserCommandHandler _handler;

    private readonly MockUserRepository _userRepository;
    private readonly MockValidator<User> _validate;

    public CreateUserCommandHandlerTests()
    {
        _userRepository = new();
        _validate = new();

        _handler = new(_validate.Object, _userRepository.Object);
    }

    [Fact]
    public async Task Handle_CreateUser_ReturnOk()
    {
        // Arrange 
        var user = new User
        {
            Name = "Test",
            LastName = "Test",
            Email = "Test@Test.com",
            Age = 1,
        };

        var request = new CreateUserCommand(user.Name, user.LastName, user.Email, user.Age);

        _validate.MockValidate(new());
        _userRepository.MockAddAsync(user);

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_CreateUserWhenInvalidUserData_ReturnError()
    {
        // Arrange 
        var user = new User
        {
            Name = "Test",
            LastName = "Test",
            Email = "Test@Test.com",
            Age = 0,
        };

        var request = new CreateUserCommand(user.Name, user.LastName, user.Email, user.Age);

        var error = new ValidationFailure();

        _validate.MockValidate(error.AsList());
        _userRepository.MockAddAsync(user);

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().BeEquivalentTo(BadRequestError.InvalidFields());
    }
}
