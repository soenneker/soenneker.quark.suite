using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers console panel services.
/// </summary>
public static class ConsolePanelRegistrar
{
    /// <summary>Adds console panel services as scoped services.</summary>
    public static IServiceCollection AddQuarkConsolePanelAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
                .TryAddScoped<IConsolePanelInterop, ConsolePanelInterop>();
        return services;
    }
}
