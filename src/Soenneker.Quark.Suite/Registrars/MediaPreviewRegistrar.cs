using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>Registers media preview services.</summary>
public static class MediaPreviewRegistrar
{
    /// <summary>Adds the browser interop used by <see cref="MediaPreview"/>.</summary>
    public static IServiceCollection AddQuarkMediaPreviewAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IMediaPreviewInterop, MediaPreviewInterop>();
        return services;
    }
}
