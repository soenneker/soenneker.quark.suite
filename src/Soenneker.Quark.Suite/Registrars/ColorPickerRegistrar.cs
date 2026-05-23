using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for color picker interop services.
/// </summary>
public static class ColorPickerRegistrar
{
    /// <summary>
    /// Adds <see cref="IColorPickerInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkColorPickerAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IColorPickerInterop, ColorPickerInterop>();
        return services;
    }
}
