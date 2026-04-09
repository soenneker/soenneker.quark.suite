using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for accordion interop services.
/// </summary>
public static class AccordionRegistrar
{
    /// <summary>
    /// Adds <see cref="IAccordionInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkAccordionAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<IAccordionInterop, AccordionInterop>();
        return services;
    }
}
