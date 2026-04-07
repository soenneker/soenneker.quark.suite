using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Blazor.Utils.ResourceLoader.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;

namespace Soenneker.Quark;

/// <inheritdoc cref="ISortableInterop"/>
public sealed class SortableInterop : ISortableInterop
{
    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly IResourceLoader _resourceLoader;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    private const string _modulePath = "/_content/Soenneker.Quark.Suite/js/sortableinterop.js";
    private const string _cdnScriptUrl = "https://cdn.jsdelivr.net/npm/sortablejs@1.15.7/Sortable.min.js";
    private const string _cdnScriptIntegrity = "sha256-v0JBvHP+9/EcWaKDpp/oBRzdMcbY/1orm6IZ54Mfz3Y=";

    public SortableInterop(IModuleImportUtil moduleImportUtil, IResourceLoader resourceLoader)
    {
        _moduleImportUtil = moduleImportUtil;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        await _resourceLoader.LoadScript(_cdnScriptUrl, integrity: _cdnScriptIntegrity, cancellationToken: token);
        IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
        await module.InvokeVoidAsync("ensureAvailable", token);
    }

    public async ValueTask Initialize(CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
        }
    }

    public async ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, string itemSelector, string? handleSelector,
        string? filterSelector, string? group, DotNetObjectReference<SortableList> callbackReference, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("initializeList", linked, element, disabled, sort, animation, itemSelector, handleSelector,
                filterSelector, group, callbackReference);
        }
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            IJSObjectReference module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
