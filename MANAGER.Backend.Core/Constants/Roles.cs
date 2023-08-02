using System.ComponentModel;

namespace MANAGER.Backend.Core.Constants;

[Flags]
public enum Roles
{
    [Description("None")]
    None,
    [Description("Admin")]
    Admin,
    [Description("Manager")]
    Manager,
    [Description("Employee")]
    Employee,
}
