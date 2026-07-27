using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Blazor.SignaturePads.Configuration;
using Soenneker.Blazor.SignaturePads.Dtos;

namespace Soenneker.Quark;

/// <summary>
/// Represents a signature pad that captures and exports drawn strokes.
/// </summary>
public interface ISignaturePad : IElement
{
    ValueTask Clear(CancellationToken cancellationToken = default);

    ValueTask<bool> IsEmpty(CancellationToken cancellationToken = default);

    ValueTask<string> ToDataUrl(string type = "image/png", double? encoderOptions = null, CancellationToken cancellationToken = default);

    ValueTask<string> ToSvg(SignaturePadSvgOptions? options = null, CancellationToken cancellationToken = default);

    ValueTask<IReadOnlyList<SignaturePadPointGroup>> ToData(CancellationToken cancellationToken = default);

    ValueTask FromData(IReadOnlyList<SignaturePadPointGroup> data, bool clear = true, CancellationToken cancellationToken = default);

    ValueTask FromDataUrl(string dataUrl, SignaturePadDataUrlOptions? options = null, CancellationToken cancellationToken = default);

    ValueTask Redraw(CancellationToken cancellationToken = default);

    ValueTask Enable(CancellationToken cancellationToken = default);

    ValueTask Disable(CancellationToken cancellationToken = default);
}
