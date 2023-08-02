using Microsoft.AspNetCore.Authorization;

namespace MANAGER.Backend.WebApi.Infraestructure;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class CustomAuthorize : AuthorizeAttribute
{
    public CustomAuthorize(params object[] roles)
    {
        if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
            throw new ArgumentException(null, nameof(roles));

        this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
    }
}
