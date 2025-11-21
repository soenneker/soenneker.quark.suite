using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component for CheckInterop.
/// </summary>
public static class CheckRegistrar
{
    /// <summary>
    /// Adds <see cref="ICheckInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkCheckAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<ICheckInterop, CheckInterop>();

        return services;
    }
}