using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for sortable interop services.
/// </summary>
public static class SortableRegistrar
{
    /// <summary>
    /// Adds <see cref="ISortableInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQuarkSortableAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().AddResourceLoaderAsScoped().TryAddScoped<ISortableInterop, SortableInterop>();
        return services;
    }
}
