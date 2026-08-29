using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Strategy that executes validation against a <see cref="Validation"/> context.
/// </summary>
public interface IValidationHandler
{
    /// <summary>
    /// Validates the request Basic credentials against the configured username and password hash.
    /// </summary>
    /// <param name="ctx">Ctx for the validate operation.</param>
    /// <param name="value">CSS value used to construct the utility class.</param>
    void Validate(Validation ctx, object value);

    /// <summary>
    /// Validates the request Basic credentials against the configured username and password hash.
    /// </summary>
    /// <param name="ctx">Ctx for the validate operation.</param>
    /// <param name="value">CSS value used to construct the utility class.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested validation Status.</returns>
    Task<ValidationStatus> Validate(Validation ctx, object value, CancellationToken cancellationToken);
}
