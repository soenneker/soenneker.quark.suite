using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark;

public static class ToastRegistrar
{
    public static IServiceCollection AddQuarkToastAsScoped(this IServiceCollection services)
    {
        services.AddScoped<IToastUtil, ToastUtil>();
        return services;
    }
}
