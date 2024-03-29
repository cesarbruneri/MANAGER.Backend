﻿using FluentResults;
using FluentValidation;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.UserPermissions;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Errors;
using MediatR;

namespace MANAGER.Backend.Application.Users.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly IValidator<User> _validator;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IValidator<User> validator, IUserRepository userRepository)
    {
        _validator = validator;
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                return Result.Fail(ConflictError.UserAlreadyExists());
            }

            var permissions = request.Permissions ?? new List<Roles> { Roles.Employee };

            var userToAdd = new User(
                request.Name, 
                request.LastName, 
                request.Email, 
                request.Password,
                permissions);

            var valid = _validator.Validate(userToAdd);

            if (!valid.IsValid)
            {
                return Result.Fail(BadRequestError.InvalidFields());
            }

            await _userRepository.AddAsync(userToAdd);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            ArgumentException.ThrowIfNullOrEmpty(ex.Message);
            throw;
        }
        
    }
}
