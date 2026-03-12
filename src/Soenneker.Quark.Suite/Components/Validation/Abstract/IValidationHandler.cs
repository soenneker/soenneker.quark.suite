using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Strategy that executes validation against a <see cref="Validation"/> context.
/// </summary>
public interface IValidationHandler
{
    /// <summary>
    /// Synchronously validates the provided value.
    /// </summary>
    void Validate(Validation ctx, object value);

    /// <summary>
    /// Asynchronously validates the provided value.
    /// </summary>
    Task<ValidationStatus> ValidateAsync(Validation ctx, object value, CancellationToken cancellationToken);
}
