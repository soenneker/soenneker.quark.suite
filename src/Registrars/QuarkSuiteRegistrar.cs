using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Quark.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for adding Quark Suite services to the dependency injection container.
/// </summary>
public static class QuarkSuiteRegistrar
{
    /// <summary>
    /// Adds all Quark Suite services to the service collection as scoped services.
    /// Automatically registers QuarkOptions if not already registered.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddQuarkSuiteAsScoped(this IServiceCollection services)
    {
        // Auto-register QuarkOptions if not already registered
        if (services.All(descriptor => descriptor.ServiceType != typeof(QuarkOptions)))
            services.AddDefaultQuarkOptionsAsScoped();

        services
            .AddQuarkValidationAsScoped()
            .AddQuarkBootstrapAsScoped()
            .AddQuarkTableAsScoped()
            .AddQuarkSnackbarAsScoped()
            .AddQuarkDatePickerAsScoped()
            .AddQuarkTreeViewAsScoped()
            .AddQuarkStepsAsScoped()
            .AddQuarkBarAsScoped()
            .AddQuarkOffcanvasAsScoped()
            .AddQuarkCheckAsScoped()
            .AddQuarkSwitchAsScoped()
            .AddQuarkRadioAsScoped()
            .AddQuarkCodeEditorAsScoped()
            .AddQuarkScoreAsScoped()
            .AddFontAwesomeAsScoped();

        return services;
    }

    /// <summary>
    /// Loads Quark resources early to prevent flicker
    /// </summary>
    /// <returns>Task representing the loading operation</returns>
    public static async Task LoadQuarkResources(this IServiceProvider serviceProvider)
    {
        var quarkOptions = serviceProvider.GetRequiredService<QuarkOptions>();

        if (quarkOptions.AutomaticBootstrapLoading)
        {
            var bootstrapInterop = serviceProvider.GetService<IBootstrapInterop>();

            if (bootstrapInterop != null)
                await bootstrapInterop.Initialize();
        }

        if (quarkOptions.AutomaticFontAwesomeLoading)
        {
            var fontAwesomeInterop = serviceProvider.GetService<IFontAwesomeInterop>();

            if (fontAwesomeInterop != null)
                await fontAwesomeInterop.Initialize();
        }
    }
}