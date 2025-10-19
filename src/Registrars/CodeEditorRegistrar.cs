using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

public static class CodeEditorRegistrar
{
    /// <summary>
    /// Adds <see cref="ICodeEditorInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkCodeEditorAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<ICodeEditorInterop, CodeEditorInterop>();
        return services;
    }
}


