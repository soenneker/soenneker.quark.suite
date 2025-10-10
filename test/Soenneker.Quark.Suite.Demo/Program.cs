using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Serilog;
using Serilog.Debugging;
using Soenneker.Serilog.Sinks.Browser.Blazor.Registrars;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog.Events;
using Soenneker.Extensions.Configuration.Logging;
using Soenneker.Extensions.Serilog.LogEventLevels;

namespace Soenneker.Quark.Suite.Demo;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            IConfiguration config = BuildConfig(builder);

            ConfigureLogging(builder.Services, config);

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            var theme = new Theme
            {
                BootstrapCssVariables = new BootstrapCssVariables
                {
                    Colors = new BootstrapColorsCssVariables
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
                    }
                }
            };

            var provider = new ThemeProvider();
            provider.AddTheme(theme);

            builder.Services.AddThemeProviderAsScoped(provider);

            var quarkOptions = new QuarkOptions
            {
                Debug = true,
                AutomaticBootstrapLoading = true,
                AutomaticFontAwesomeLoading = true
            };

            builder.Services.AddQuarkOptionsAsScoped(quarkOptions);

            builder.Services.AddQuarkSuiteAsScoped();

            // Register demo services
            builder.Services.AddScoped<Services.EmployeeService>();

            var host = builder.Build();

            var jsRuntime = (IJSRuntime)host.Services.GetService(typeof(IJSRuntime))!;

            SetGlobalLogger(jsRuntime);

            await host.Services.LoadQuarkResources();

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

    private static IConfigurationRoot BuildConfig(WebAssemblyHostBuilder builder)
    {
        var configRoot = builder.Configuration.Build();

        return configRoot;
    }

    private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
    {
        SelfLog.Enable(m => Console.Error.WriteLine(m));

        var logEventLevel = configuration.GetLogEventLevel();

        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddFilter("Microsoft.AspNetCore.Components.RenderTree.*", LogLevel.None);
            builder.SetMinimumLevel(logEventLevel.ToMicrosoftLogLevel());
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