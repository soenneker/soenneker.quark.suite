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
    /// <summary>
    /// Removes all entries managed by the Signature Pad.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Signature Pad has been cleared.</returns>
    ValueTask Clear(CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether the Signature Pad empty.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if the Signature Pad empty; otherwise, false.</returns>
    ValueTask<bool> IsEmpty(CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts to data URL.
    /// </summary>
    /// <param name="type">Runtime type to inspect or construct.</param>
    /// <param name="encoderOptions">Encoder Options for the to data url operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by to Data URL.</returns>
    ValueTask<string> ToDataUrl(string type = "image/png", double? encoderOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts to svg.
    /// </summary>
    /// <param name="options">Options to configure for the Signature Pad.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the text returned by to Svg.</returns>
    ValueTask<string> ToSvg(SignaturePadSvgOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts to data.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the collection returned by to Data.</returns>
    ValueTask<IReadOnlyList<SignaturePadPointGroup>> ToData(CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates from data.
    /// </summary>
    /// <param name="data">data to process.</param>
    /// <param name="clear">Whether clear.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the from data operation is complete.</returns>
    ValueTask FromData(IReadOnlyList<SignaturePadPointGroup> data, bool clear = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates from data URL.
    /// </summary>
    /// <param name="dataUrl">URL of the data to target.</param>
    /// <param name="options">Options to configure for the Signature Pad.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the from data url operation is complete.</returns>
    ValueTask FromDataUrl(string dataUrl, SignaturePadDataUrlOptions? options = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Redraws signature Pad.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the redraw operation is complete.</returns>
    ValueTask Redraw(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the value produced by enable.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the enable operation is complete.</returns>
    ValueTask Enable(CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables signature Pad.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the disable operation is complete.</returns>
    ValueTask Disable(CancellationToken cancellationToken = default);
}
