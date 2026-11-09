using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class QueryInputTestInterop : IInputInterop
{
    public InputSelectionSnapshot? Selection { get; set; }
    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterNavigationKeys(ElementReference element, string[] keys, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterTagInputKeys(ElementReference input, string[] delimiters, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask UnregisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask<InputSelectionSnapshot?> GetSelection(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.FromResult(Selection);
    public ValueTask RestoreSelection(ElementReference input, int start, int end, string? value, CancellationToken cancellationToken = default)
    {
        Selection = new InputSelectionSnapshot { Start = start, End = end, Value = value };
        return ValueTask.CompletedTask;
    }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
