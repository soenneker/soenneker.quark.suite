using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Soenneker.Quark;
/// <summary>Registers shared UI localization independently of browser services.</summary>
public static class LocalizationRegistrar
{
    /// <summary>Registers default scoped resources without replacing application implementations.</summary>
    public static IServiceCollection AddQuarkLocalizationAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<QuarkLocalizer>();
        services.TryAddScoped<IQuarkLocalizer>(provider => provider.GetRequiredService<QuarkLocalizer>());
        return services;
    }
}
