using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.Utils.ResourceLoader.Registrars;
using Soenneker.Quark.Registrars;

namespace Soenneker.Quark;

public static class QuarkSuiteRegistrar
{
    public static IServiceCollection AddQuarkSuiteAsScoped(this IServiceCollection services)
    {
        services.AddResourceLoaderAsScoped()
            .AddQuarkTable()
            .AddQuarkSnackbarAsScoped()
            .AddDatePickerAsScoped()
            .AddStepsAsScoped()
            .AddQuarkOffcanvasAsScoped();

        return services;
    }
}