using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Soenneker.Quark.DatePickers;
using Soenneker.Quark.DatePickers.Abstract;

namespace Soenneker.Quark.Registrars;

/// <summary>
/// A Blazor Quark component, DatePicker.
/// </summary>
public static class DatePickerRegistrar
{
    /// <summary>
    /// Adds <see cref="IDatePickerInterop"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddDatePickerAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped().TryAddScoped<IDatePickerInterop, DatePickerInterop>();

        return services;
    }
}
