using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers calendar keyboard and focus services.</summary>
public static class CalendarRegistrar
{
    /// <summary>Adds calendar interop to this service collection.</summary>
    public static IServiceCollection AddQuarkCalendarAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<ICalendarInterop, CalendarInterop>();
        return services;
    }
}
