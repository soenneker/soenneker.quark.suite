using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers Sidebar interop services.
/// </summary>
public static class SidebarRegistrar
{
    /// <summary>
    /// Adds <see cref="ISidebarInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddQuarkSidebarAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<ISidebarInterop, SidebarInterop>();
        return services;
    }
}
