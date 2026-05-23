using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

public interface IColorPickerInterop : IAsyncDisposable
{
    ValueTask<bool> RegisterCanvas(ElementReference canvas, DotNetObjectReference<ColorPicker> callbackReference, bool disabled, CancellationToken cancellationToken = default);

    ValueTask UnregisterCanvas(ElementReference canvas, CancellationToken cancellationToken = default);
}
