using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor UI element for sidebar navigation components
/// </summary>
public static class BarRegistrar
{
    /// <summary>
    /// Adds <see cref="IBarInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkBarAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IBarInterop, BarInterop>();

        return services;
    }
}
