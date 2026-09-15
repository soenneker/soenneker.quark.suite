using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers Steps interop services.
/// </summary>
public static class StepsRegistrar
{
    /// <summary>
    /// Adds <see cref="IStepsInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddQuarkStepsAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IStepsInterop, StepsInterop>();
        return services;
    }
}
