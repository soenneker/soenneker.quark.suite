using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Soenneker.Quark.Suite.Tests;
internal sealed class WorkflowViewportTestInterop : IVirtualViewportInterop
{
    public ValueTask Update(ElementReference element, DotNetObjectReference<VirtualItems> receiver, IReadOnlyList<string> keys, bool follow, bool before, bool after, bool parentScroll, IReadOnlyList<string> disabledKeys) => ValueTask.CompletedTask;
    public ValueTask ScrollToIndex(ElementReference element, int index, string? focusSelector) => ValueTask.CompletedTask;
    public ValueTask JumpToEnd(ElementReference element) => ValueTask.CompletedTask;
    public ValueTask Destroy(ElementReference element) => ValueTask.CompletedTask;
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
