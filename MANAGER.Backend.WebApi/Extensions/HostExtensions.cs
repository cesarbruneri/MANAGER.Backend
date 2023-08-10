using Serilog;
using Serilog.Events;

namespace MANAGER.Backend.WebApi.Extensions;

public static class HostExtensions
{
    public static ConfigureHostBuilder AddLog(this ConfigureHostBuilder host)
    {
        host.UseSerilog((ctx, lc) => 
            lc.WriteTo.Console(LogEventLevel.Debug));

        return host;
    }
}
