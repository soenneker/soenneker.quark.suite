using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for managing Font Awesome CSS resources.
/// </summary>
public interface IFontAwesomeInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes Font Awesome CSS resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}