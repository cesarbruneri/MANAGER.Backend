using FluentResults;
using MediatR;

namespace MANAGER.Backend.Application.Users.Create;

public class CreateUserCommand : IRequest<Result>
{
    public CreateUserCommand(string name, string lastName, string email, int age)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Age = age;
    }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }
}
