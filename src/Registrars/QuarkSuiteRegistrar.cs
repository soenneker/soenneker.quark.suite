using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Quark.Registrars;

namespace Soenneker.Quark;

public static class QuarkSuiteRegistrar
{
    public static IServiceCollection AddQuarkSuiteAsScoped(this IServiceCollection services)
    {
        // Auto-register QuarkOptions if not already registered
        if (services.All(descriptor => descriptor.ServiceType != typeof(QuarkOptions)))
            services.AddDefaultQuarkOptionsAsScoped();

        // Auto-register ThemeProvider if not already registered
        if (services.All(descriptor => descriptor.ServiceType != typeof(IThemeProvider)))
            services.AddEmptyThemeProviderAsScoped();

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
        var bootstrapInterop = serviceProvider.GetService<IBootstrapInterop>();
        var fontAwesomeInterop = serviceProvider.GetService<IFontAwesomeInterop>();

        if (quarkOptions.AutomaticBootstrapLoading && bootstrapInterop != null)
            await bootstrapInterop.Initialize();

        if (quarkOptions.AutomaticFontAwesomeLoading && fontAwesomeInterop != null)
            await fontAwesomeInterop.Initialize();
    }
}