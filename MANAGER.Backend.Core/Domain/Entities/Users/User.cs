using MANAGER.Backend.Core.Constants;
using MANAGER.Backend.Core.Domain.Entities.Base;
using MANAGER.Backend.Core.Domain.Entities.UserPermissions;

namespace MANAGER.Backend.Core.Domain.Entities.Users;

public class User : EntityBase
{
    public User(string name, string lastName, string email, string password, List<Roles> permissions)
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;
        Permissions = new List<UserPermission>();

        AddPermission(permissions);
    }

    public User() { }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public ICollection<UserPermission> Permissions { get; private set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        User otherUser = (User)obj;

        return Id == otherUser.Id &&
               Name == otherUser.Name &&
               LastName == otherUser.LastName &&
               Email == otherUser.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, LastName, Email);
    }

    private void AddPermission(List<Roles>? permissions)
    {
        permissions?.ForEach(role =>
        {
            var permission = new UserPermission
            {
                UserId = Id,
                User = this,
                Role = role,
            };
            Permissions.Add(permission);
        });
    }
}
