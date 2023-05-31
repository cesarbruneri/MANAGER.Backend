using Microsoft.EntityFrameworkCore;

namespace MANAGER.Backend.Sql.Infrastructure.Context;

public static class SeedContext
{
    public static void PrepareDatabase(ManagerContext managerContext)
    {
        SeedData(managerContext);
    }

    private static void SeedData(ManagerContext? managerContext)
    {
        if (managerContext is not null)
        {
            managerContext.Database.Migrate();
        };
    }
}
