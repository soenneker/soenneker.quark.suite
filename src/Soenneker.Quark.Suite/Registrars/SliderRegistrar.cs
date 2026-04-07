using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for slider interop services.
/// </summary>
public static class SliderRegistrar
{
    /// <summary>
    /// Adds <see cref="ISliderInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkSliderAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<ISliderInterop, SliderInterop>();
        return services;
    }
}
