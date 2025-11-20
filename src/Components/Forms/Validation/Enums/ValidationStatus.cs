using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// An enumeration for Quark, representing validation status.
/// </summary>
[Intellenum<string>]
public sealed partial class ValidationStatus
{
    /// <summary>
    /// No validation has been performed yet.
    /// </summary>
    public static readonly ValidationStatus None = new(null);

    /// <summary>
    /// Validation passed successfully.
    /// </summary>
    public static readonly ValidationStatus Success = new("success");

    /// <summary>
    /// Validation failed with errors.
    /// </summary>
    public static readonly ValidationStatus Error = new("error");
}
