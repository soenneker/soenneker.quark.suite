using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registers console services.
/// </summary>
public static class ConsoleRegistrar
{
    /// <summary>Adds console services as scoped services.</summary>
    public static IServiceCollection AddQuarkConsoleAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
                .TryAddScoped<IConsoleInterop, ConsoleInterop>();
        return services;
    }
}
