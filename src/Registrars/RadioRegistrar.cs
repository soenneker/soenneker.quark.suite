using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component for RadioInterop.
/// </summary>
public static class RadioRegistrar
{
    /// <summary>
    /// Adds <see cref="IRadioInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddQuarkRadioAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<IRadioInterop, RadioInterop>();

        return services;
    }
}