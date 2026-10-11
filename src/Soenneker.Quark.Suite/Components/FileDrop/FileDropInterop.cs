using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

public sealed class FileDropInterop(IModuleImportUtil moduleImportUtil, QuarkOptions? quarkOptions = null) : IFileDropInterop
{
    private readonly string ModulePath = QuarkAssetPath.JavaScript("./_content/Soenneker.Quark.Suite/js/filedropinterop.js", quarkOptions);

    public async ValueTask Register(ElementReference target, string inputId, CancellationToken cancellationToken = default)
    {
        var module = await moduleImportUtil.GetContentModuleReference(ModulePath, cancellationToken);
        await module.InvokeVoidAsync("register", cancellationToken, target, inputId);
    }

    public async ValueTask Unregister(ElementReference target, CancellationToken cancellationToken = default)
    {
        var module = await moduleImportUtil.GetContentModuleReference(ModulePath, cancellationToken);
        await module.InvokeVoidAsync("unregister", cancellationToken, target);
    }
}
