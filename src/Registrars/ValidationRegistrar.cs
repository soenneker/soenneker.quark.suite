using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for validation interop.
/// </summary>
public static class ValidationRegistrar
{
    /// <summary>
    /// Adds <see cref="IValidationInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkValidationAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<IValidationInterop, ValidationInterop>();

        return services;
    }
}



