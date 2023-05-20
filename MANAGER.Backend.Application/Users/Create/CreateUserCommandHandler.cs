using FluentResults;
using FluentValidation;
using MANAGER.Backend.Application.IRepositories;
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
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            LastName = request.LastName,
            Age = request.Age,
        };

        var valid = _validator.Validate(user);

        if (!valid.IsValid)
        {
            return Result.Fail(BadRequestError.InvalidFields());
        }

        await _userRepository.AddAsync(user);

        return Result.Ok();
    }
}
