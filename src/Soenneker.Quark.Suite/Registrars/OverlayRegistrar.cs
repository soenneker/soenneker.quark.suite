using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

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
        services.AddModuleImportUtilAsScoped();
        services.TryAddScoped<IOverlayInterop, OverlayInterop>();
        services.TryAddScoped<IOverlayPortalService, OverlayPortalService>();
        return services;
    }
}
