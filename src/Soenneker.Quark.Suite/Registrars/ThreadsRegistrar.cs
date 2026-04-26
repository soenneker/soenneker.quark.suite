using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for thread interop services.
/// </summary>
public static class ThreadsRegistrar
{
    /// <summary>
    /// Adds <see cref="IThreadsInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkThreadsAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .TryAddScoped<IThreadsInterop, ThreadsInterop>();
        return services;
    }
}
