using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Validation interop operations that manages CSS loading for validation styling.
/// </summary>
public interface IValidationInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the validation stylesheet is loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    /// <returns>A task that completes when the Validation is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}



