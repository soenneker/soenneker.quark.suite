using System;
using System.Threading.Tasks;
using Soenneker.Blazor.C15t.Registrars;
using Soenneker.Blazor.SignaturePads.Registrars;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Utils.Clipboard.Registrars;
using Soenneker.Bradix;

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
        if (!HasQuarkOptionsRegistration(services))
            services.AddDefaultQuarkOptionsAsScoped();

        services.AddBradixSuiteAsScoped()
                .AddScoped<ICollapseCoordinator, CollapseCoordinator>()
                .AddQuarkOverlayAsScoped()
                .AddQuarkValidationAsScoped()
                .AddQuarkInputAsScoped()
                .AddQuarkDataTableAsScoped()
                .AddQuarkSortableAsScoped()
                .AddQuarkSonnerAsScoped()
                .AddQuarkThemeAsScoped()
                .AddQuarkCarouselAsScoped()
                .AddQuarkPromptInputAsScoped()
                .AddQuarkNodeEditorAsScoped()
                .AddQuarkFloatingWindowAsScoped()
                .AddQuarkResizableAsScoped()
                .AddQuarkThreadsAsScoped()
                .AddQuarkScrollspyAsScoped()
                .AddQuarkScrollRevealAsScoped()
                .AddQuarkCodeEditorAsScoped()
                .AddQuarkScoreAsScoped()
                .AddQuarkColorPickerAsScoped()
                .AddQuarkConsolePanelAsScoped()
                .AddQuarkOnThisPageAsScoped()
                .AddQuarkDateTimesAsScoped()
                .AddQuarkPaymentCardAsScoped()
                .AddSignaturePadAsScoped()
                .AddC15tAsScoped()
                .AddClipboardAsScoped();

        return services;
    }

    /// <summary>
    /// Loads Quark resources early to prevent flicker
    /// </summary>
    /// <param name="serviceProvider">Service Provider for the load quark resources operation.</param>
    /// <returns>Task representing the loading operation</returns>
    public static async Task LoadQuarkResources(this IServiceProvider serviceProvider)
    {
        var quarkOptions = serviceProvider.GetRequiredService<QuarkOptions>();

    }

    private static bool HasQuarkOptionsRegistration(IServiceCollection services)
    {
        foreach (var descriptor in services)
        {
            if (descriptor.ServiceType == typeof(QuarkOptions))
                return true;
        }

        return false;
    }
}
