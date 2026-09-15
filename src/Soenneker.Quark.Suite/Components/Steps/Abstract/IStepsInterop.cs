using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for steps.
/// </summary>
public interface IStepsInterop : IAsyncDisposable
{

    /// <summary>
    /// Focuses the element with the specified ID without scrolling.
    /// </summary>
    ValueTask FocusById(string id, CancellationToken cancellationToken = default);
}
