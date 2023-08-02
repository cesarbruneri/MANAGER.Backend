using FluentResults;
using MANAGER.Backend.Core.Constants;
using MediatR;

namespace MANAGER.Backend.Application.Users.Create;

public class CreateUserCommand : IRequest<Result>
{
    public CreateUserCommand(string name, string lastName, string email, string password, List<Roles> permissions)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        Permissions = permissions;
    }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<Roles> Permissions { get; set; }
}
