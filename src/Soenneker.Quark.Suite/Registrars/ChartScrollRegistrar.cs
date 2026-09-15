using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers ChartScroll interop services.
/// </summary>
public static class ChartScrollRegistrar
{
    /// <summary>
    /// Adds <see cref="IChartScrollInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddQuarkChartScrollAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IChartScrollInterop, ChartScrollInterop>();
        return services;
    }
}
