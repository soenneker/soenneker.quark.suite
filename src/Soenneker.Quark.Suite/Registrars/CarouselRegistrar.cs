using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for carousel interop services.
/// </summary>
public static class CarouselRegistrar
{
    /// <summary>
    /// Adds <see cref="ICarouselInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkCarouselAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<ICarouselInterop, CarouselInterop>();
        return services;
    }
}
