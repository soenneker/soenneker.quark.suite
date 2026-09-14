using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides compatibility initialization for components with built-in styling.
/// </summary>
public interface IValidationInterop : IAsyncDisposable
{
    /// <summary>
    /// Completes initialization without loading external styles.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    /// <returns>A task that completes when the Validation is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}



