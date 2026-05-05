using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for docs On This Page interop services.
/// </summary>
public static class DocsOnThisPageRegistrar
{
    /// <summary>
    /// Adds <see cref="IOnThisPageInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkOnThisPageAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IOnThisPageInterop, OnThisPageInterop>();
        return services;
    }
}
