using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers infinite scroll browser services.</summary>
public static class InfiniteScrollRegistrar
{
    /// <summary>Adds the scoped interop required by InfiniteScroll.</summary>
    public static IServiceCollection AddQuarkInfiniteScrollAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IInfiniteScrollInterop, InfiniteScrollInterop>();
        return services;
    }
}
