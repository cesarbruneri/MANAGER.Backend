using FluentResults;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MANAGER.Backend.Core.Errors;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MANAGER.Backend.Application.Users.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;

    private static string? _secret;

    public LoginCommandHandler(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _secret = configuration["Auth:Secret"] ?? string.Empty;
        if (string.IsNullOrWhiteSpace(_secret))
        {
            ArgumentException.ThrowIfNullOrEmpty(_secret);
        }
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByEmailIncludePermissionAsync(request.UserEmail);
        if (user is null)
        {
            return Result.Fail(NotFoundError.UserNotFound());
        }

        var token = GenerateToken(user);

        return Result.Ok(token);
    }

    private static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

        var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            });

        foreach (var Permission in user.Permissions)
        {
            var claim = new Claim(ClaimTypes.Role, Permission.Role.ToString());
            claims.AddClaim(claim);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = signingCredentials,
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
