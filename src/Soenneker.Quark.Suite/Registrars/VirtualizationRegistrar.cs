using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;
namespace Soenneker.Quark;

/// <summary>Registers measured collection virtualization.</summary>
public static class VirtualizationRegistrar
{
    /// <summary>Adds scoped viewport interop and its browser module dependencies.</summary>
    public static IServiceCollection AddQuarkVirtualizationAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().AddResourceLoaderAsScoped().TryAddScoped<IVirtualViewportInterop, VirtualViewportInterop>();
        return services;
    }
}
