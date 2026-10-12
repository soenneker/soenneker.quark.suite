using Soenneker.Atomics.ValueBools;
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
    private ValueAtomicBool _attached;
    private ValueAtomicBool _disposed;

    public AdaptiveTableLayout(ITablesInterop interop) => _interop = interop;

    public Task Synchronize(ElementReference element, ElementReference columns, bool enabled, TableColumnSizingOptions options)
    {
        if (_disposed.Value || _work.IsCompletedSuccessfully &&
            (enabled ? _attached.Value && _element.Equals(element) && _options == options : !_attached.Value))
            return Task.CompletedTask;

        return _work = SynchronizeCore(_work, element, columns, enabled, options);
    }

    private async Task SynchronizeCore(Task previous, ElementReference element, ElementReference columns, bool enabled, TableColumnSizingOptions options)
    {
        await previous;
        if (_disposed.Value)
            return;

        if (_attached.Value && (!enabled || !_element.Equals(element) || _options != options))
        {
            await _interop.StopAdaptiveLayout(_element);
            _attached.Value = false;
        }

        if (enabled && !_attached.Value)
        {
            _element = element;
            _options = options;
            await _interop.StartAdaptiveLayout(element, columns, options);
            _attached.Value = true;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (!_disposed.TrySetTrue())
            return;
        try
        {
            await _work;
            if (_attached.Value)
                await _interop.StopAdaptiveLayout(_element);
        }
        catch (JSDisconnectedException)
        {
        }
    }
}
