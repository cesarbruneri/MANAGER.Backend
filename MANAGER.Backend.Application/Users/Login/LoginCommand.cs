using FluentResults;
using MediatR;

namespace MANAGER.Backend.Application.Users.Login;

public class LoginCommand : IRequest<Result<string>>
{
    public LoginCommand(string userEmail, string password)
    {
        UserEmail = userEmail;
        Password = password;
    }

    public string UserEmail { get; }

    public string Password { get; }
}
