using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for theme provider services.
/// </summary>
public static class ThemeProvidersRegistrar
{
    /// <summary>
    /// Adds a specific ThemeProvider instance as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <param name="themeProvider">The ThemeProvider instance to register.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddThemeProviderAsScoped(this IServiceCollection services, ThemeProvider themeProvider)
    {
        services.AddScoped<IThemeProvider>(_ => themeProvider);

        return services;
    }

    /// <summary>
    /// Adds an empty ThemeProvider as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddEmptyThemeProviderAsScoped(this IServiceCollection services)
    {
        services.AddScoped<IThemeProvider, ThemeProvider>();

        return services;
    }
}
