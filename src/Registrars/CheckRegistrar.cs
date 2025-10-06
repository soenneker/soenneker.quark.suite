using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component for CheckInterop.
/// </summary>
public static class CheckRegistrar
{
    /// <summary>
    /// Adds <see cref="ICheckInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddQuarkCheckAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .TryAddScoped<ICheckInterop, CheckInterop>();

        return services;
    }
}