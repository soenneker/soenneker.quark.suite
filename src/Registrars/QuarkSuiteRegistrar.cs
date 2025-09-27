using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;

namespace Soenneker.Quark;

public static class QuarkSuiteRegistrar
{
    public static IServiceCollection AddQuarkSuiteAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .AddQuarkTable()
            .AddSnackbarAsScoped()
            .AddDatePickerAsScoped()
            .AddStepsAsScoped()
            .AddOffcanvasAsScoped();

        return services;
    }
}