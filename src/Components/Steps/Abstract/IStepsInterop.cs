using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Steps component JavaScript interop operations.
/// </summary>
public interface IStepsInterop
{
    /// <summary>
    /// Initializes the Steps component CSS and JavaScript resources.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Awaitable task.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
