using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISelectInterop"/>
public sealed class SelectInterop : ISelectInterop
{
    private readonly IModuleImportUtil moduleImportUtil;
    private readonly CancellationScope cancellationScope = new();

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/selectinterop.js";

    public SelectInterop(IModuleImportUtil moduleImportUtil)
    {
        this.moduleImportUtil = moduleImportUtil;
    }

    public async ValueTask AttachScrollButtonsAsync(string attachmentId, ElementReference viewport, ElementReference? scrollUp, ElementReference? scrollDown,
        CancellationToken cancellationToken = default)
    {
        var linked = cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("attachSelectScrollButtons", linked, attachmentId, viewport, scrollUp, scrollDown);
        }
    }

    public async ValueTask DetachScrollButtonsAsync(string attachmentId, CancellationToken cancellationToken = default)
    {
        var linked = cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("detachSelectScrollButtons", linked, attachmentId);
        }
    }

    public async ValueTask ScrollIntoViewNearestAsync(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("scrollIntoViewNearest", linked, element);
        }
    }

    public async ValueTask ScrollViewportByAsync(ElementReference viewport, double deltaY, CancellationToken cancellationToken = default)
    {
        var linked = cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            IJSObjectReference module = await moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("scrollViewportBy", linked, viewport, deltaY);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await cancellationScope.DisposeAsync();
        await moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
