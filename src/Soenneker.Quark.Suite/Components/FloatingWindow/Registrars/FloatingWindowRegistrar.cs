using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Represents the floating window registrar.
/// </summary>
public static class FloatingWindowRegistrar
{
    /// <summary>
    /// Adds quark floating window as scoped.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The result of the operation.</returns>
    public static IServiceCollection AddQuarkFloatingWindowAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IFloatingWindowInterop, FloatingWindowInterop>();

        return services;
    }
}