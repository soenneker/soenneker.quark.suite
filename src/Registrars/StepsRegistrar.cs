using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark.Registrars;

/// <summary>
/// A Blazor UI element for step indicators and navigation
/// </summary>
public static class StepsRegistrar
{
    /// <summary>
    /// Adds <see cref="IStepsInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkStepsAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IStepsInterop, StepsInterop>();

        return services;
    }
}
