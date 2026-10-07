using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Rrweb.Replay.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers the services required by session replay.</summary>
public static class SessionReplayRegistrar
{
    /// <summary>Adds the rrweb replay interop and its resource dependencies as scoped services.</summary>
    public static IServiceCollection AddQuarkSessionReplayAsScoped(this IServiceCollection services)
    {
        return services.AddRrwebReplayInteropAsScoped();
    }
}
