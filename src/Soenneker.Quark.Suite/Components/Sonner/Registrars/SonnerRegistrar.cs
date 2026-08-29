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
    /// Registers Quark Sonner with a scoped lifetime.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
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
