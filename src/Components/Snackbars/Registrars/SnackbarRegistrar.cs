using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor UI element for snackbar toast notifications
/// </summary>
public static class SnackbarRegistrar
{
    /// <summary>
    /// Adds <see cref="ISnackbarInterop"/> as a scoped service.
    /// </summary>
    public static IServiceCollection AddQuarkSnackbarAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<ISnackbarInterop, SnackbarInterop>();

        return services;
    }
}
