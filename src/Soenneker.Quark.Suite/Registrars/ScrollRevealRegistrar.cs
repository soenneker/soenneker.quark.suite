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
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQuarkScrollRevealAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IScrollRevealInterop, ScrollRevealInterop>();
        return services;
    }
}
