using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Soenneker.Quark;

/// <summary>Registers the file drop zone's browser interop services.</summary>
public static class FileDropZoneRegistrar
{
    /// <summary>Adds <see cref="IFileDropZoneInterop"/> and its file drop dependencies as scoped services.</summary>
    public static IServiceCollection AddQuarkFileDropZoneAsScoped(this IServiceCollection services)
    {
        services.AddQuarkSpinnerAsScoped().AddQuarkFileDropAsScoped().TryAddScoped<IFileDropZoneInterop, FileDropZoneInterop>();
        return services;
    }
}
