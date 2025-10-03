using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A registrar for Bootstrap interop services
/// </summary>
public static class BootstrapRegistrar
{
    /// <summary>
    /// Adds <see cref="IBootstrapInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkBootstrapAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IBootstrapInterop, BootstrapInterop>();

        return services;
    }
}