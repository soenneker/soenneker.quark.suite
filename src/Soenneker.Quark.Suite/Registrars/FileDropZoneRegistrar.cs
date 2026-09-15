using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers the file drop zone's browser interop services.</summary>
public static class FileDropZoneRegistrar
{
    /// <summary>Adds <see cref="IFileDropZoneInterop"/> and its module import dependency as scoped services.</summary>
    public static IServiceCollection AddQuarkFileDropZoneAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IFileDropZoneInterop, FileDropZoneInterop>();
        return services;
    }
}
