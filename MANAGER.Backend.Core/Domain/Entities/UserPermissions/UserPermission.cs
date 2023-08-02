using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Base;
using MANAGER.Backend.Core.Domain.Entities.Users;

namespace MANAGER.Backend.Core.Domain.Entities.UserPermissions;

public class UserPermission : EntityBase
{
    public Guid UserId { get; set; }

    public Roles Role { get; set; }
    public User? User { get; set; }
}
