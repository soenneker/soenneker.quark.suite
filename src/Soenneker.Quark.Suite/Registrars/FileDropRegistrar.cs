using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers the lightweight file drop wrapper.</summary>
public static class FileDropRegistrar
{
    /// <summary>Adds the file drop interop as a scoped service.</summary>
    public static IServiceCollection AddQuarkFileDropAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IFileDropInterop, FileDropInterop>();
        return services;
    }
}
