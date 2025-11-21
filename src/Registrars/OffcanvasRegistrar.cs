using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component for OffcanvasInterop.
/// </summary>
public static class OffcanvasRegistrar
{
    /// <summary>
    /// Adds <see cref="IOffcanvasInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkOffcanvasAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<IOffcanvasInterop, OffcanvasInterop>();
        services.TryAddScoped<IOffcanvasCoordinator, OffcanvasCoordinator>();

        return services;
    }
}
