using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Bradix;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;

namespace Soenneker.Quark;

public sealed class QuarkPortalInterop(IModuleImportUtil modules) : IPortalInterop
{
    private const string ModulePath = "./_content/Soenneker.Quark.Suite/js/portalinterop.js";

    public ValueTask<IJSObjectReference> Initialize(CancellationToken cancellationToken = default) =>
        modules.GetContentModuleReference(ModulePath, cancellationToken);

    public async ValueTask MountPortal(ElementReference element, string? containerSelector = null, ElementReference container = default,
        CancellationToken cancellationToken = default)
    {
        var module = await Initialize(cancellationToken);
        await module.InvokeQuarkVoidAsync("mountPortal", cancellationToken, element, containerSelector, container);
    }

    public async ValueTask UnmountPortal(ElementReference element, CancellationToken cancellationToken = default)
    {
        var module = await Initialize(cancellationToken);
        await module.InvokeQuarkVoidAsync("unmountPortal", cancellationToken, element);
    }

    public async ValueTask DisposeAsync() => await modules.DisposeContentModule(ModulePath);
}
