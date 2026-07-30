using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers node editor browser services.
/// </summary>
public static class NodeEditorRegistrar
{
    /// <summary>
    /// Registers node editor browser interop with a scoped lifetime.
    /// </summary>
    /// <param name="services">The service collection to update.</param>
    /// <returns>The supplied service collection.</returns>
    public static IServiceCollection AddQuarkNodeEditorAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<INodeEditorInterop, NodeEditorInterop>();
        return services;
    }
}
