using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for popover interop services.
/// </summary>
public static class PopoverRegistrar
{
    /// <summary>
    /// Adds <see cref="IPopoverInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkPopoverAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped();
        services.TryAddScoped<IPopoverInterop, PopoverInterop>();
        services.TryAddScoped<ISelectInterop, SelectInterop>();
        return services;
    }
}
