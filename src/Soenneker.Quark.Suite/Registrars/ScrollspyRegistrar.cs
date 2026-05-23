using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for Scrollspy interop services.
/// </summary>
public static class ScrollspyRegistrar
{
    /// <summary>
    /// Adds <see cref="IScrollspyInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkScrollspyAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IScrollspyInterop, ScrollspyInterop>();
        return services;
    }
}
