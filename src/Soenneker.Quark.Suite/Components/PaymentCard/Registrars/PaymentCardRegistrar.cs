using Microsoft.Extensions.DependencyInjection;
using Soenneker.Blazor.CreditCards.Registrars;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;

namespace Soenneker.Quark;

/// <summary>
/// Registrar for payment card display services.
/// </summary>
public static class PaymentCardRegistrar
{
    /// <summary>
    /// Adds payment card display services as scoped services.
    /// </summary>
    public static IServiceCollection AddQuarkPaymentCardAsScoped(this IServiceCollection services)
    {
        services.AddModuleImportUtilAsScoped()
                .AddCreditCardsInteropAsScoped();

        return services;
    }
}
