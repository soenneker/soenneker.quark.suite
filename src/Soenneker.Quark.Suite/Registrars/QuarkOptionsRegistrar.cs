using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for QuarkOptions configuration
/// </summary>
public static class QuarkOptionsRegistrar
{
    /// <summary>
    /// Adds QuarkOptions as a scoped service with a specific instance
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="quarkOptions">The QuarkOptions instance to register</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddQuarkOptionsAsScoped(this IServiceCollection services, QuarkOptions quarkOptions)
    {
        services.AddSingleton(quarkOptions);
        return services;
    }

    /// <summary>
    /// Adds QuarkOptions as a scoped service with default values
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddDefaultQuarkOptionsAsScoped(this IServiceCollection services)
    {
        services.AddSingleton(new QuarkOptions());
        return services;
    }
}
