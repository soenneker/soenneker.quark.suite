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
    public static IServiceCollection AddQuarkValidationAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<IValidationInterop, ValidationInterop>();

        return services;
    }
}



