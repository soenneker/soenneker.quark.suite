using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for overlay interop services.
/// </summary>
public static class OverlayRegistrar
{
    /// <summary>
    /// Adds <see cref="IOverlayInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkOverlayAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IOverlayInterop, OverlayInterop>();
        return services;
    }
}
