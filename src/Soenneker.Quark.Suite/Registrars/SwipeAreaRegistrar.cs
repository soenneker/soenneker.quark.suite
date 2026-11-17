using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers swipe area browser services.</summary>
public static class SwipeAreaRegistrar
{
    /// <summary>Adds the scoped interop required by SwipeArea.</summary>
    public static IServiceCollection AddQuarkSwipeAreaAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<ISwipeAreaInterop, SwipeAreaInterop>();
        return services;
    }
}
