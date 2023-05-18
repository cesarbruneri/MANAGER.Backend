using FluentResults;
using FluentValidation;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Errors;
using MANAGER.Backend.Core.Domain.Users;
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
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            LastName = request.LastName,
            Age = request.Age,
        };

        var valid = _validator.Validate(user);

        if (valid.IsValid)
        {
            return Result.Fail(BadRequestError.InvalidFields());
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return Result.Fail(BadRequestError.NameIsNotEmpty());
        };
        if (string.IsNullOrWhiteSpace(request.LastName))
        {
            return Result.Fail(new Error("Teste"));
        };
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return Result.Fail(new Error("Teste"));
        };
        if (request.Age is 0)
        {
            return Result.Fail(new Error("Teste"));
        };

        await _userRepository.AddAsync(user);

        return Result.Ok();
    }
}
