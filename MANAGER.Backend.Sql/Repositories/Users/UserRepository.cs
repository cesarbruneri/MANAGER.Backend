using MANAGER.Backend.Application.IRepositories;
using MANAGER.Backend.Core.Domain.Users;
using MANAGER.Backend.Sql.Infrastructure.Context;
using MANAGER.Backend.Sql.Repositories.Base;

namespace MANAGER.Backend.Sql.Repositories.Users;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ManagerContext context) : base(context)
    {
    }
}
