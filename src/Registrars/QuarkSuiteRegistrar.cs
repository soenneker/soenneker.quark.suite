using Microsoft.Extensions.DependencyInjection;
using Soenneker.Quark.Registrars;

namespace Soenneker.Quark;

public static class QuarkSuiteRegistrar
{
    public static IServiceCollection AddQuarkSuiteAsScoped(this IServiceCollection services)
    {
        services.AddQuarkTableAsScoped()
            .AddQuarkSnackbarAsScoped()
            .AddQuarkDatePickerAsScoped()
            .AddQuarkTreeViewAsScoped()
            .AddQuarkStepsAsScoped()
            .AddQuarkBarAsScoped()
            .AddQuarkOffcanvasAsScoped();

        return services;
    }
}
