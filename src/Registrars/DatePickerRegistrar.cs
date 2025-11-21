using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor Quark component, DatePicker.
/// </summary>
public static class DatePickerRegistrar
{
    /// <summary>
    /// Adds <see cref="IDatePickerInterop"/> as a scoped service.
    /// </summary>
    /// <param name="services">The service collection to add the service to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkDatePickerAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IDatePickerInterop, DatePickerInterop>();

        return services;
    }
}
