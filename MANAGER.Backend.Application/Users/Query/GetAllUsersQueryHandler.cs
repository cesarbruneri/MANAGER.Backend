using FluentResults;
using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MediatR;

namespace MANAGER.Backend.Application.Users.Query;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<User>>>
{
    private readonly IUserRepository _userRepository;
    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return Result.Ok(users);
    }
}
