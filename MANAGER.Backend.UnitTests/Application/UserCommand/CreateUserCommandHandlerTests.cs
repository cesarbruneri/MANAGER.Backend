﻿using FluentAssertions;
using FluentResults;
using FluentValidation.Results;
using MANAGER.Backend.Application.Users.Create;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.UserPermissions;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Errors;
using MANAGER.Backend.Core.Extensions;
using MANAGER.Backend.UnitTests.Mocks;
using Moq;
using Xunit;

namespace MANAGER.Backend.UnitTests.Application.UserCommand;

public class CreateUserCommandHandlerTests
{
    private readonly CreateUserCommandHandler _handler;

    private readonly MockUserRepository _mockUserRepository;
    private readonly MockValidator<User> _mockValidate;

    public CreateUserCommandHandlerTests()
    {
        _mockUserRepository = new();
        _mockValidate = new();

        _handler = new(_mockValidate.Object, _mockUserRepository.Object);
    }

    [Fact]
    public async Task Handle_CreateUser_ReturnOk()
    {
        // Arrange 
        var permissions = new List<Roles> { Roles.Admin };

        var user = new User(
            "Test",
            "Test",
            "Test@Test.com",
            "password",
            permissions
        );

        var request = new CreateUserCommand(user.Name, user.LastName, user.Email, user.Password, permissions);

        _mockValidate.MockValidate(new());
        _mockUserRepository
            .MockAddAsync(user)
            .MockFindByEmailAsync(user.Email, null);

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _mockUserRepository
            .VerifyFindByEmailAsync(user.Email, Times.Once())
            .VerifyAll();
        _mockValidate.VerifyAll();
    }

    [Fact]
    public async Task Handle_CreateUserWhenInvalidUserData_ReturnError()
    {
        // Arrange 
        var permissions = new List<Roles> { Roles.Admin };

        var user = new User(
            "Test",
            "Test",
            "Test@Test.com",
            "password",
            permissions
        );

        var request = new CreateUserCommand(user.Name, user.LastName, user.Email, user.Password, permissions);

        var error = new ValidationFailure();

        _mockValidate.MockValidate(error.AsList());
        _mockUserRepository
            .MockFindByEmailAsync(user.Email, null); 

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().BeEquivalentTo(BadRequestError.InvalidFields());
        _mockUserRepository
            .VerifyFindByEmailAsync(user.Email, Times.Once())
            .VerifyAll();
        _mockValidate.VerifyAll();
    }

    [Fact]
    public async Task Handle_UserAlreadyExists_ReturnError()
    {
        // Arrange 
        var permissions = new List<Roles> { Roles.Admin };

        var user = new User(
            "Test",
            "Test",
            "Test@Test.com",
            "password",
            permissions
        );

        var request = new CreateUserCommand(user.Name, user.LastName, user.Email, user.Password, permissions);

        var error = new ValidationFailure();

        _mockValidate.MockValidate(error.AsList());
        _mockUserRepository
            .MockFindByEmailAsync(user.Email, user);

        // Act
        Result result = await _handler.Handle(request, default);

        // Assert
        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().BeEquivalentTo(ConflictError.UserAlreadyExists());
        _mockUserRepository
            .VerifyFindByEmailAsync(user.Email, Times.Once())
            .VerifyAll();
    }
}
