using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor UI element for step indicators and navigation
/// </summary>
public static class StepsRegistrar
{
    /// <summary>
    /// Adds <see cref="IStepsInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddStepsAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IStepsInterop, StepsInterop>();

        return services;
    }
}
