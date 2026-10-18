using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Blazor.Utils.ModuleImport.Registrars;
namespace Soenneker.Quark;
/// <summary>Registers localization, saved-view persistence, and rich-text browser interop.</summary>
public static class WorkflowRegistrar
{
    /// <summary>Adds workflow services while retaining application-provided implementations.</summary>
    public static IServiceCollection AddQuarkWorkflowsAsScoped(this IServiceCollection services)
    {
        services.TryAddSingleton<QuarkOptions>();
        services.AddQuarkVirtualizationAsScoped();
        services.AddModuleImportUtilAsScoped();
        services.AddQuarkInputAsScoped();
        services.AddQuarkLocalizationAsScoped();
        services.TryAddScoped<IWorkflowInteractionInterop, WorkflowInteractionInterop>();
        services.TryAddScoped<IQuarkViewStore, QuarkViewStore>();
        services.TryAddScoped<IRichTextEditorInterop, RichTextEditorInterop>();
        return services;
    }
}
