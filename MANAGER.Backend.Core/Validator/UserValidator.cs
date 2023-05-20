using FluentValidation;
using MANAGER.Backend.Core.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.Backend.Core.Validator;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Email).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Age).GreaterThan(0);
    }
}
