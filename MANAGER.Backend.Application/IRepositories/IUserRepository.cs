using MANAGER.Backend.Core.Domain.Entities.Users;

namespace MANAGER.Backend.Application.IRepositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email);
}
