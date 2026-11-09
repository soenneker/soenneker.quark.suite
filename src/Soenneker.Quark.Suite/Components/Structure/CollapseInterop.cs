using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Soenneker.Blazor.Extensions.IJSObjectReferences;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class CollapseInterop : ICollapseInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly string _modulePath;

    public CollapseInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? options = null)
    {
        _moduleImportUtil = moduleImportUtil;
        _modulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/collapseinterop.js", options);
    }

    public async ValueTask Update(ElementReference element, bool expanded, bool horizontal, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidWithContextAsync("update", cancellationToken, element, expanded, horizontal);
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, cancellationToken);
        await module.InvokeVoidWithContextAsync("destroy", cancellationToken, element);
    }
}
