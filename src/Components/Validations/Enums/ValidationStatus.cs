using Intellenum;

namespace Soenneker.Quark;

/// <summary>
/// An enumeration for Quark, representing validation status.
/// </summary>
[Intellenum<string>]
public sealed partial class ValidationStatus
{
    public static readonly ValidationStatus None = new(null);

    public static readonly ValidationStatus Success = new("success");

    public static readonly ValidationStatus Error = new("error");
}
