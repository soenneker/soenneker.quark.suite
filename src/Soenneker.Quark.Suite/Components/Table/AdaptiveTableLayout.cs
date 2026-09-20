using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

// Serializes attachment and cleanup across asynchronous renders, including replacement table elements.
internal sealed class AdaptiveTableLayout : IAsyncDisposable
{
    private readonly ITablesInterop _interop;
    private Task _work = Task.CompletedTask;
    private ElementReference _element;
    private TableColumnSizingOptions? _options;
    private bool _attached;
    private bool _disposed;

    public AdaptiveTableLayout(ITablesInterop interop) => _interop = interop;

    public Task Synchronize(ElementReference element, ElementReference columns, bool enabled, TableColumnSizingOptions options)
    {
        if (_disposed || _work.IsCompletedSuccessfully &&
            (enabled ? _attached && _element.Equals(element) && _options == options : !_attached))
            return Task.CompletedTask;

        return _work = SynchronizeCore(_work, element, columns, enabled, options);
    }

    private async Task SynchronizeCore(Task previous, ElementReference element, ElementReference columns, bool enabled, TableColumnSizingOptions options)
    {
        await previous;
        if (_disposed)
            return;

        if (_attached && (!enabled || !_element.Equals(element) || _options != options))
        {
            await _interop.StopAdaptiveLayout(_element);
            _attached = false;
        }

        if (enabled && !_attached)
        {
            _element = element;
            _options = options;
            await _interop.StartAdaptiveLayout(element, columns, options);
            _attached = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;
        _disposed = true;
        try
        {
            await _work;
            if (_attached)
                await _interop.StopAdaptiveLayout(_element);
        }
        catch (JSDisconnectedException)
        {
        }
    }
}
