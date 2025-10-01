using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

public static class TreeViewRegistrar
{
    public static IServiceCollection AddQuarkTreeViewAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<ITreeViewInterop, TreeViewInterop>();
        return services;
    }
}


