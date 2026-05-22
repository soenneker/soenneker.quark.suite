using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

public static class FloatingWindowRegistrar
{
    public static IServiceCollection AddQuarkFloatingWindowAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped().TryAddScoped<IFloatingWindowInterop, FloatingWindowInterop>();

        return services;
    }
}