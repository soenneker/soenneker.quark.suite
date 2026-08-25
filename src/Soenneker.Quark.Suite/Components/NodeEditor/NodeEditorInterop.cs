using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Initializers;
using Soenneker.Blazor.Utils.ModuleImport.Abstract;
using Soenneker.Extensions.CancellationTokens;
using Soenneker.Utils.CancellationScopes;
using Soenneker.Utils.Json;

namespace Soenneker.Quark;

/// <inheritdoc cref="INodeEditorInterop"/>
public sealed class NodeEditorInterop : INodeEditorInterop
{
    private const string _modulePath = "./_content/Soenneker.Quark.Suite/js/nodeeditorinterop.js";

    private readonly IModuleImportUtil _moduleImportUtil;
    private readonly AsyncInitializer _initializer;
    private readonly CancellationScope _cancellationScope = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="NodeEditorInterop"/> class.
    /// </summary>
    /// <param name="moduleImportUtil">The utility used to import the node editor JavaScript module.</param>
    public NodeEditorInterop(IModuleImportUtil moduleImportUtil)
    {
        _moduleImportUtil = moduleImportUtil;
        _initializer = new AsyncInitializer(InitializeModule);
    }

    private async ValueTask InitializeModule(CancellationToken token)
    {
        await _moduleImportUtil.GetContentModuleReference(_modulePath, token);
    }

    public async ValueTask Initialize(string id, NodeEditorOptions options, DotNetObjectReference<NodeEditor> callbackReference,
        CancellationToken cancellationToken = default)
    {
        await Invoke("initialize", cancellationToken, id, JsonUtil.Serialize(options)!, callbackReference);
    }

    public async ValueTask Refresh(string id, NodeEditorOptions options, string? selectedNodeId, IReadOnlyList<string> selectedNodeIds, string? selectedEdgeId,
        CancellationToken cancellationToken = default)
    {
        await Invoke("refresh", cancellationToken, id, JsonUtil.Serialize(options)!, selectedNodeId, selectedNodeIds, selectedEdgeId);
    }

    public async ValueTask ZoomBy(string id, double delta, CancellationToken cancellationToken = default)
    {
        await Invoke("zoomBy", cancellationToken, id, delta);
    }

    public async ValueTask FitView(string id, CancellationToken cancellationToken = default)
    {
        await Invoke("fitView", cancellationToken, id);
    }

    public async ValueTask ResetView(string id, CancellationToken cancellationToken = default)
    {
        await Invoke("resetView", cancellationToken, id);
    }

    public async ValueTask<NodeEditorGraphPoint> ClientToGraph(string id, double clientX, double clientY, CancellationToken cancellationToken = default)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            return await module.InvokeAsync<NodeEditorGraphPoint>("clientToGraphPoint", linked, id, clientX, clientY);
        }
    }

    public async ValueTask Destroy(string id, CancellationToken cancellationToken = default)
    {
        await Invoke("destroy", cancellationToken, id);
    }

    private async ValueTask Invoke(string identifier, CancellationToken cancellationToken, params object?[] args)
    {
        var linked = _cancellationScope.CancellationToken.Link(cancellationToken, out var source);

        using (source)
        {
            await _initializer.Init(linked);
            var module = await _moduleImportUtil.GetContentModuleReference(_modulePath, linked);
            await module.InvokeVoidAsync(identifier, linked, args);
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _moduleImportUtil.DisposeContentModule(_modulePath);
        await _initializer.DisposeAsync();
        await _cancellationScope.DisposeAsync();
    }
}
