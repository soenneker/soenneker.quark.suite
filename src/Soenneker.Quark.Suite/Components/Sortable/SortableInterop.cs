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

    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/sortableinterop.js";
    private const string _sortableScriptPath = "./_content/Soenneker.Quark.Suite/js/vendor/sortable.min.js";

    public SortableInterop(IModuleImportUtil moduleImportUtil, IResourceLoader resourceLoader)
    {
        _moduleImportUtil = moduleImportUtil;
        _resourceLoader = resourceLoader;
        _initializer = new AsyncInitializer(InitializeResources);
    }

    private async ValueTask InitializeResources(CancellationToken token)
    {
        await _resourceLoader.LoadScript(_sortableScriptPath, cancellationToken: token);
        var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
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

    public async ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, bool forceFallback, string itemSelector,
        string? handleSelector, string? filterSelector, string? group, bool notifyOnReorder, DotNetObjectReference<SortableList> callbackReference,
        CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("initializeList", linked, element, disabled, sort, animation, forceFallback, itemSelector, handleSelector,
                filterSelector, group, notifyOnReorder, callbackReference);
        }
    }

    public async ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync("destroy", linked, element);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
        await _moduleImportUtil.DisposeContentModule(_modulePath);
    }
}
