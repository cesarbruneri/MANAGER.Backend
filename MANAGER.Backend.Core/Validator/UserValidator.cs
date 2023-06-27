using FluentValidation;
using MANAGER.Backend.Core.Domain.Entities.Users;

namespace MANAGER.Backend.Core.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Age)
            .GreaterThan(0);
    }
}
