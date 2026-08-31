using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Cloudflare.AiSearch.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for the Quark Cloudflare AI Search component.
/// </summary>
public static class CloudflareAiSearchRegistrar
{
    /// <summary>
    /// Adds the services required by the Quark Cloudflare AI Search component.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddQuarkCloudflareAiSearchAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareAiSearchInteropAsScoped();
        return services;
    }
}
