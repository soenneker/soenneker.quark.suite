using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Soenneker.Quark;

/// <summary>
/// Service registrar for Quark.DataTable
/// </summary>
public static class DataTableRegistrar
{
    /// <summary>
    /// Adds DataTable services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddQuarkDataTableAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ITablesInterop, TablesInterop>();
        return services;
    }
}
