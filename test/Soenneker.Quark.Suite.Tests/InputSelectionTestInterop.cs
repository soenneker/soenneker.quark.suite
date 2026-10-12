using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark.Suite.Tests;

internal sealed class InputSelectionTestInterop : IInputInterop
{
    public ValueTask Initialize(CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterNavigationKeys(ElementReference element, string[] keys, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterTagInputKeys(ElementReference input, string[] delimiters, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask RegisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask UnregisterComboboxKeys(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask<InputSelectionSnapshot?> GetSelection(ElementReference input, CancellationToken cancellationToken = default) => ValueTask.FromResult<InputSelectionSnapshot?>(null);
    public ValueTask RestoreSelection(ElementReference input, int start, int end, string? value, CancellationToken cancellationToken = default) => ValueTask.CompletedTask;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
