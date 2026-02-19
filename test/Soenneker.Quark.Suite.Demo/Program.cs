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
                Anchors = new AnchorOptions
                {
                    TextDecoration = TextDecoration.None
                }
            };

            var provider = new ThemeProvider();
            provider.AddTheme(theme);

            builder.Services.AddThemeProviderAsScoped(provider);

            var quarkOptions = new QuarkOptions
            {
                Debug = true,
                AutomaticFrameworkResourceLoading = false,
                AutomaticFontAwesomeLoading = true
            };

            builder.Services.AddQuarkOptionsAsScoped(quarkOptions);

            builder.Services.AddQuarkSuiteAsScoped();

            // Register demo services
            builder.Services.AddScoped<Services.EmployeeService>();

            WebAssemblyHost host = builder.Build();

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
        IConfigurationRoot configRoot = builder.Configuration.Build();

        return configRoot;
    }

    private static void ConfigureLogging(IServiceCollection services, IConfiguration configuration)
    {
        SelfLog.Enable(m => Console.Error.WriteLine(m));

        LogEventLevel logEventLevel = configuration.GetLogEventLevel();

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