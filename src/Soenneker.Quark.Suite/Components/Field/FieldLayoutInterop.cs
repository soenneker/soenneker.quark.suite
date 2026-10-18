using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class FieldLayoutInterop(IModuleImportUtil modules) : IFieldLayoutInterop
{
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/fieldlayoutinterop.js";

    public async ValueTask Observe(ElementReference fieldset, CancellationToken cancellationToken = default)
    {
        var module = await modules.GetContentModuleReference(ModulePath, cancellationToken);
        await module.InvokeVoidAsync("observe", cancellationToken, fieldset);
    }

    public async ValueTask Unobserve(ElementReference fieldset, CancellationToken cancellationToken = default)
    {
        var module = await modules.GetContentModuleReference(ModulePath, cancellationToken);
        await module.InvokeVoidAsync("unobserve", cancellationToken, fieldset);
    }

    public async ValueTask DisposeAsync() => await modules.DisposeContentModule(ModulePath);
}
