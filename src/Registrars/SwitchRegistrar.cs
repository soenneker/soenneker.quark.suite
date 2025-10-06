using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Soenneker.Quark.Components.Switches;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component for SwitchInterop.
/// </summary>
public static class SwitchRegistrar
{
    /// <summary>
    /// Adds <see cref="ISwitchInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddQuarkSwitchAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<ISwitchInterop, SwitchInterop>();

        return services;
    }
}
