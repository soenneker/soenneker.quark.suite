using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for Quark date/time component services.
/// </summary>
public static class DateTimeRegistrar
{
    /// <summary>
    /// Adds Quark date/time formatter and browser time zone services as scoped services.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkDateTimesAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped();
        services.TryAddScoped<IQuarkDateTimeFormatter, QuarkDateTimeFormatter>();
        services.TryAddScoped<IQuarkBrowserTimeZoneInterop, QuarkBrowserTimeZoneInterop>();
        services.TryAddScoped<IQuarkBrowserTimeZoneService, QuarkBrowserTimeZoneService>();

        return services;
    }
}
