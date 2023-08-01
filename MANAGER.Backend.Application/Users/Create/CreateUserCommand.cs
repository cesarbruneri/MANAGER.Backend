using FluentResults;
using MediatR;

namespace MANAGER.Backend.Application.Users.Create;

public class CreateUserCommand : IRequest<Result>
{
    public CreateUserCommand(string name, string lastName, string email, string password)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
