using MANAGER.Backend.Core.Domain.Entities.Base;

namespace MANAGER.Backend.Core.Domain.Entities.Users;

public class User : EntityBase
{
    public required string Name { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public int Age { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        User otherUser = (User)obj;

        return Name == otherUser.Name &&
               LastName == otherUser.LastName &&
               Email == otherUser.Email &&
               Age == otherUser.Age;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, LastName, Email, Age);
    }
}
