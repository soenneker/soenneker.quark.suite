using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for ScrollReveal interop services.
/// </summary>
public static class ScrollRevealRegistrar
{
    /// <summary>
    /// Adds <see cref="IScrollRevealInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkScrollRevealAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IScrollRevealInterop, ScrollRevealInterop>();
        return services;
    }
}
