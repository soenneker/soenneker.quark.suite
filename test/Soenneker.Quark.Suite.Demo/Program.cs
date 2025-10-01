using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Debugging;
using Soenneker.Serilog.Sinks.Browser.Blazor.Registrars;

namespace Soenneker.Quark.Suite.Demo;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            ConfigureLogging(builder.Services);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            //builder.Services.AddEmptyThemeProviderAsScoped();

            // Modern Professional Theme Configuration
            var theme = new Theme
            {
                // Modern Color Palette - Professional yet Contemporary
                BootstrapColors = new BootstrapColorsCssVariables
                {
                    // Primary Colors - Deep Professional Blue
                    Primary = "#2563eb",
                    PrimaryRgb = "37, 99, 235",
                    PrimaryTextEmphasis = "#1e40af",
                    PrimaryBgSubtle = "#dbeafe",
                    PrimaryBorderSubtle = "#93c5fd",

                    // Secondary Colors - Sophisticated Purple
                    Secondary = "#7c3aed",
                    SecondaryRgb = "124, 58, 237",
                    SecondaryTextEmphasis = "#5b21b6",
                    SecondaryBgSubtle = "#ede9fe",
                    SecondaryBorderSubtle = "#c4b5fd",

                    // Success - Modern Green
                    Success = "#059669",
                    SuccessRgb = "5, 150, 105",
                    SuccessTextEmphasis = "#047857",
                    SuccessBgSubtle = "#d1fae5",
                    SuccessBorderSubtle = "#6ee7b7",

                    // Danger - Refined Red
                    Danger = "#dc2626",
                    DangerRgb = "220, 38, 38",
                    DangerTextEmphasis = "#b91c1c",
                    DangerBgSubtle = "#fee2e2",
                    DangerBorderSubtle = "#fca5a5",

                    // Warning - Warm Orange
                    Warning = "#ea580c",
                    WarningRgb = "234, 88, 12",
                    WarningTextEmphasis = "#c2410c",
                    WarningBgSubtle = "#fed7aa",
                    WarningBorderSubtle = "#fdba74",

                    // Info - Vibrant Cyan
                    Info = "#0891b2",
                    InfoRgb = "8, 145, 178",
                    InfoTextEmphasis = "#0e7490",
                    InfoBgSubtle = "#cffafe",
                    InfoBorderSubtle = "#67e8f9",

                    // Neutral Colors - Modern Grays
                    Dark = "#1f2937",
                    Light = "#f9fafb",
                    Gray100 = "#f3f4f6",
                    Gray200 = "#e5e7eb",
                    Gray300 = "#d1d5db",
                    Gray400 = "#9ca3af",
                    Gray500 = "#6b7280",
                    Gray600 = "#4b5563",
                    Gray700 = "#374151",
                    Gray800 = "#1f2937",
                    Gray900 = "#111827"
                },

                // Modern Typography
                BootstrapTypography = new BootstrapTypographyCssVariables
                {
                    // Modern Font Stack - Inter for better readability
                    FontSansSerif = "Inter, system-ui, -apple-system, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif",
                    BodyFontFamily = "var(--bs-font-sans-serif)",
                    BodyFontSize = "1rem",
                    BodyFontWeight = "400",
                    BodyLineHeight = "1.6",
                    
                    // Refined Colors
                    BodyColor = "#1f2937",
                    BodyBg = "#ffffff",
                    EmphasisColor = "#111827",
                    SecondaryColor = "#6b7280",
                    TertiaryColor = "#9ca3af",
                    
                    // Link Styling
                    LinkColor = "#2563eb",
                    LinkHoverColor = "#1e40af",
                    LinkDecoration = "none",
                    
                    // Code Styling
                    CodeColor = "#dc2626",
                    HighlightBg = "#fef3c7"
                },

                // Modern Borders and Shadows
                BootstrapBorders = new BootstrapBordersCssVariables
                {
                    BorderRadius = "0.5rem",
                    BorderRadiusSm = "0.375rem",
                    BorderRadiusLg = "0.75rem",
                    BorderRadiusXl = "1rem",
                    BorderRadius2xl = "1.5rem",
                    BorderColor = "#e5e7eb",
                    BorderColorTranslucent = "rgba(229, 231, 235, 0.8)",
                    BorderOpacity = "0.1"
                },

                BootstrapShadows = new BootstrapShadowsCssVariables
                {
                    BoxShadow = "0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)",
                    BoxShadowSm = "0 1px 2px 0 rgba(0, 0, 0, 0.05)",
                    BoxShadowLg = "0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)",
                    BoxShadowInset = "inset 0 2px 4px 0 rgba(0, 0, 0, 0.06)"
                },

                // Elegant Card Styling
                BootstrapCards = new BootstrapCardsCssVariables
                {
                    CardSpacerY = "1.5rem",
                    CardSpacerX = "1.5rem",
                    CardBorderRadius = "0.75rem",
                    CardBorderColor = "rgba(229, 231, 235, 0.8)",
                    CardBorderWidth = "1px",
                    CardCapPaddingY = "1rem",
                    CardCapPaddingX = "1.5rem",
                    CardCapBg = "rgba(249, 250, 251, 0.8)"
                },

                // Component Options for Enhanced Styling
                Buttons = new ButtonOptions(),
                Cards = new CardOptions()
            };

            var provider = new ThemeProvider { CurrentTheme = "Default", Themes = new Dictionary<string, Theme> { { "Default", theme } } };

            builder.Services.AddThemeProviderAsScoped(provider);

            // Register all Quark services using the suite registrar
            builder.Services.AddQuarkSuiteAsScoped();

            // Register demo services
            builder.Services.AddScoped<Soenneker.Quark.Suite.Demo.Services.EmployeeService>();

            var host = builder.Build();

            var jsRuntime = (IJSRuntime)host.Services.GetService(typeof(IJSRuntime))!;

            SetGlobalLogger(jsRuntime);

            await host.RunAsync();
        }
        catch (Exception e)
        {
            Log.Error(e, "Stopped program because of exception");
            throw;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void ConfigureLogging(IServiceCollection services)
    {
        SelfLog.Enable(m => Console.Error.WriteLine(m));

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddFilter("Microsoft.AspNetCore.Components.RenderTree.*", LogLevel.None);

            builder.AddSerilog(dispose: false);
        });
    }

    private static void SetGlobalLogger(IJSRuntime jsRuntime)
    {
        var loggerConfig = new LoggerConfiguration();

        loggerConfig.WriteTo.BlazorConsole(jsRuntime: jsRuntime);

        Log.Logger = loggerConfig.CreateLogger();
    }
}