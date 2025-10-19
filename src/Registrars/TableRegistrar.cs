using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;


namespace Soenneker.Quark.Registrars;

/// <summary>
/// Service registrar for Quark.Table
/// </summary>
public static class TableRegistrar
{
    /// <summary>
    /// Adds Table services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddQuarkTableAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<ITablesInterop, TablesInterop>();
        return services;
    }
}
