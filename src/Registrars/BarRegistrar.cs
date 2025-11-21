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
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkBarAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IBarInterop, BarInterop>();

        return services;
    }
}
