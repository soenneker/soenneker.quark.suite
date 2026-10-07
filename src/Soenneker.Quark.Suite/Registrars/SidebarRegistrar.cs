using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Librarian.Browser;
using Soenneker.Librarian.LocalStorage;
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
        services.TryAddKeyedScoped<Func<IBrowserLibrarianDatabase>>(typeof(Sidebar), (provider, _) =>
        {
            var modules = provider.GetRequiredService<IModuleImportUtil>();
            var logger = provider.GetRequiredService<ILogger<LocalStorageLibrarianDatabase>>();
            return () => new LocalStorageLibrarianDatabase(modules, logger, "Soenneker.Quark.Sidebar");
        });
        services.AddModuleImportUtilAsScoped().TryAddScoped<ISidebarInterop, SidebarInterop>();
        return services;
    }
}
