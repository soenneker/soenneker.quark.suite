using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers the Sonner-style toast notification services.
/// </summary>
public static class SonnerRegistrar
{
    /// <summary>
    /// Adds quark sonner as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddQuarkSonnerAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped();
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<ISonnerService, SonnerService>();
        services.TryAddScoped<ISonnerUtil, SonnerUtil>();
        services.TryAddScoped<ISonnerInterop, SonnerInterop>();

        return services;
    }
}
