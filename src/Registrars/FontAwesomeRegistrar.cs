using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for FontAwesome interop services
/// </summary>
public static class FontAwesomeRegistrar
{
    /// <summary>
    /// Adds <see cref="IFontAwesomeInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddFontAwesomeAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IFontAwesomeInterop, FontAwesomeInterop>();
        return services;
    }
}

