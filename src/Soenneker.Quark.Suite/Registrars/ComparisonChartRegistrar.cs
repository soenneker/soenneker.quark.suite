using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for ComparisonChart interop services.
/// </summary>
public static class ComparisonChartRegistrar
{
    /// <summary>
    /// Adds <see cref="IComparisonChartInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQuarkComparisonChartAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();

        services.AddModuleImportUtilAsScoped().TryAddScoped<IComparisonChartInterop, ComparisonChartInterop>();
        return services;
    }
}
