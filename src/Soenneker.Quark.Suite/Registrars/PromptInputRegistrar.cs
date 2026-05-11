using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for prompt input interop services.
/// </summary>
public static class PromptInputRegistrar
{
    /// <summary>
    /// Adds <see cref="IPromptInputInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkPromptInputAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<IPromptInputInterop, PromptInputInterop>();
        return services;
    }
}
