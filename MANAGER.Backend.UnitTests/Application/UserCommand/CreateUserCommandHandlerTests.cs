using FluentAssertions;
using FluentResults;
using FluentValidation.Results;
using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Core.Domain.Users;
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

        var validationResult = new ValidationResult();

        _validate.MockValidate(user, validationResult);

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
