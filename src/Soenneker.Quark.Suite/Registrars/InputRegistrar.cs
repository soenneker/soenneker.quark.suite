using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for input interop services.
/// </summary>
public static class InputRegistrar
{
    /// <summary>
    /// Adds <see cref="IInputInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkInputAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<IInputInterop, InputInterop>();
        return services;
    }
}
