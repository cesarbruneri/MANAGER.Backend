using FluentResults;
using MANAGER.Backend.Core.Domain.Entities.Users;
using MediatR;

namespace MANAGER.Backend.Application.Users.Query;

public class GetAllUsersQuery : IRequest<Result<List<User>>>
{
}
