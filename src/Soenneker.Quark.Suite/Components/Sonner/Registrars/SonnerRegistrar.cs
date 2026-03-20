using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers the Sonner-style toast notification services.
/// </summary>
public static class SonnerRegistrar
{
    public static IServiceCollection AddQuarkSonnerAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped();
        services.TryAddScoped<ISonnerService, SonnerService>();
        services.TryAddScoped<ISonnerUtil, SonnerUtil>();
        services.TryAddScoped<ISonnerInterop, SonnerInterop>();

        return services;
    }
}
