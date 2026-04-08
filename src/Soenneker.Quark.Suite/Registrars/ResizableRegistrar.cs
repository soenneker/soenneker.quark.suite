using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for resizable interop services.
/// </summary>
public static class ResizableRegistrar
{
    /// <summary>
    /// Adds <see cref="IResizableInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkResizableAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<IResizableInterop, ResizableInterop>();
        return services;
    }
}
